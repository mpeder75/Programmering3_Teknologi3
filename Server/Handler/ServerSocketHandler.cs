using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Handler
{
    public class ServerSocketHandler
    {
        private readonly IPEndPoint _localEndPoint;
        // Liste af client sockets
        private readonly List<Socket> _clientSockets = new List<Socket>();
        
        private Socket _listeningPort;
        private CancellationTokenSource _cancellationTokenSource;

        public ServerSocketHandler(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            _localEndPoint = new IPEndPoint(ip, port);
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            // opretter ny thread for at oprete serveren i baggrunden og ikke blokere main thread
            Thread serverThread = new Thread(() => RunServer(_cancellationTokenSource.Token));
            serverThread.Start();
        }


        public void Stop()
        {
            _cancellationTokenSource.Cancel();

            // Close the listener socket to stop accepting new clients
            _listeningPort?.Close();

            // Close all client sockets
            lock (_clientSockets)
            {
                foreach (var clientSocket in _clientSockets)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
                _clientSockets.Clear();
            }
            Console.WriteLine("Server stopped.");
        }


        private void RunServer(CancellationToken cancellationToken)
        {
            // Opretter en socket til at lytte på indkommende connections
            _listeningPort = new Socket(_localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // Binder socket til local endpoint
            _listeningPort.Bind(_localEndPoint);

            Console.WriteLine("Venter på indkommendne connections...");

            
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Socket clientSocket = _listeningPort.Accept();
                    Console.WriteLine("Client connected.");

                    lock (_clientSockets)
                    {
                        _clientSockets.Add(clientSocket);
                    }

                    // Når client er accepteret, oprettes en ny tråd til at håndtere clienten
                    ClientHandler clientHandler = new ClientHandler(clientSocket, cancellationToken, BroadcastToAll);
                    Thread clientThread = new Thread(clientHandler.HandleClient);
                    clientThread.Start();
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }

        // Metode der sender besked vidre til alle clienter
        private void BroadcastToAll(string message, Socket excludeSocket)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);
            lock (_clientSockets)
            {
                foreach (var clientSocket in _clientSockets)
                {
                    if (clientSocket != excludeSocket)
                    {
                        try
                        {
                            clientSocket.Send(msg);
                        }
                        catch (SocketException)
                        {
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                        }
                    }
                }
            }
        }
    }
}
