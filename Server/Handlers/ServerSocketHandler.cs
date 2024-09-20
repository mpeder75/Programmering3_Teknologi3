using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Handler
{
    public class ServerSocketHandler
    {
        private readonly IPEndPoint _localEndPoint;
        private readonly List<Socket> _clientSockets = new List<Socket>();
        private Socket _listeningPort;

        public ServerSocketHandler(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            _localEndPoint = new IPEndPoint(ip, port);
        }

        public void Start()
        {
            Thread serverThread = new Thread(RunServer);
            serverThread.Start();
        }

        public void Stop()
        {
            _listeningPort?.Close();

            lock (_clientSockets)
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
                _clientSockets.Clear();
            }
            Console.WriteLine("Server stopped.");
        }

        private void RunServer()
        {
            try
            {
                _listeningPort = new Socket(_localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _listeningPort.Bind(_localEndPoint);
                _listeningPort.Listen(10);

                Console.WriteLine($"Server listening on {_localEndPoint.Address}:{_localEndPoint.Port}");

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

                        ClientHandler clientHandler = new ClientHandler(clientSocket, BroadcastToAll);
                        Thread clientThread = new Thread(clientHandler.HandleClient);
                        clientThread.Start();
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine($"SocketException: {se.Message}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected exception: {ex.Message}");
                        break;
                    }
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine($"SocketException during server setup: {se.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception during server setup: {ex.Message}");
            }
        }

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