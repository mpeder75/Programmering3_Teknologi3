using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Handler
{
    public class ServerSocketHandler
    {
        private readonly IPEndPoint _localEndPoint;
        private readonly List<Socket> _clientSockets = new List<Socket>();    // Liste af client sockets
        private Socket _listeningPort;

        public ServerSocketHandler(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            _localEndPoint = new IPEndPoint(ip, port);
        }

        public void Start()
        {
            Thread serverThread = new Thread(RunServer);        // Opretter ny thread for at oprette serveren på, så main thread ikke blokeres
            serverThread.Start();
        }

        public void Stop()
        {
            _listeningPort?.Close();                          // Lukker listening socket

            lock (_clientSockets)                             // Locks client sockets, så der ikke kan tilgås samtidig
            {
                foreach (var clientSocket in _clientSockets)
                {
                    try
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                    }
                    catch (SocketException) { }
                    clientSocket.Close();
                }
                _clientSockets.Clear();                     // Tømmer listen a client sockets
            }
            Console.WriteLine("Server stopped.");
        }


        private void RunServer()
        {
            _listeningPort = new Socket(_localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  // Opretter ny socket til at lytte på port
            _listeningPort.Bind(_localEndPoint);                                                             // Binder socket til local endpoint
            _listeningPort.Listen(10);                                                                // connection nr. 11 bliver afvist

            Console.WriteLine("Venter på indkommende connections...");

            while (true)
            {
                try
                {
                    Socket clientSocket = _listeningPort.Accept();
                    Console.WriteLine("Client connected :-)");

                    lock (_clientSockets)
                    {
                        _clientSockets.Add(clientSocket);
                    }

                    // Opretter ny client handler og thread til at håndtere client beskeder og broadcaste til alle clienter
                    ClientHandler clientHandler = new ClientHandler(clientSocket, BroadcastToAll);
                    Thread clientThread = new Thread(clientHandler.HandleClient);
                    clientThread.Start();
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }

        // Metode der sender besked videre til alle clienter
        private void BroadcastToAll(string message, Socket excludeSocket)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);
            lock (_clientSockets)
            {
                List<Socket> disconnectedSockets = new List<Socket>();
                foreach (var clientSocket in _clientSockets)
                {
                    if (clientSocket != excludeSocket)
                    {
                        try
                        {
                            clientSocket.Send(msg);
                        }
                        catch (Exception)
                        {
                            disconnectedSockets.Add(clientSocket);
                            Console.WriteLine("Client disconnected abruptly.");
                        }
                    }
                }
                RemoveDisconnectedSockets(disconnectedSockets);
            }
        }

        // Metode der fjerner afbrudte sockets fra listen
        // Hvorfor? HVIS vi afbryder en client fra serveren, og vi forsøger at broadcaste en ny besked til alle client
        // vil vi få en exception, da clienten ikke længere er tilsluttet
        private void RemoveDisconnectedSockets(List<Socket> disconnectedSockets)
        {
            foreach (var socket in disconnectedSockets)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException) { }
                catch (ObjectDisposedException) { }
                socket.Close();
                _clientSockets.Remove(socket);
            }
        }
    }
}


