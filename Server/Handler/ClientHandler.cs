﻿using System.Net.Sockets;
using System.Text;

namespace Server.Handler
{
    public class ClientHandler
    {
        private readonly Socket _clientSocket;
        private readonly CancellationToken _cancellationToken;
        private readonly Action<string, Socket> _broadcastMessage;

        public ClientHandler(Socket clientSocket, CancellationToken cancellationToken,
            Action<string, Socket> broadcastMessage)
        {
            _clientSocket = clientSocket;
            _cancellationToken = cancellationToken;
            _broadcastMessage = broadcastMessage;
        }

        public void HandleClient()
        {
            try
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    // Receive the length of the incoming message
                    byte[] lengthBytes = new byte[4];
                    int bytesRec = _clientSocket.Receive(lengthBytes);
                    if (bytesRec == 0) break; // Connection closed

                    int messageLength = BitConverter.ToInt32(lengthBytes, 0);

                    // Receive the actual message
                    byte[] messageBytes = new byte[messageLength];
                    bytesRec = _clientSocket.Receive(messageBytes);
                    string data = Encoding.ASCII.GetString(messageBytes, 0, bytesRec);

                    Console.WriteLine("Text received : {0}", data);

                    // Broadcast the message to all other clients
                    _broadcastMessage(data, _clientSocket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                // Shutdown and close the client socket
                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
            }
        }
    }
}
