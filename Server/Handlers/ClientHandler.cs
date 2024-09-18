using System.Net.Sockets;
using System.Text;

namespace Server.Handler
{
    public class ClientHandler
    {
        private readonly Socket _clientSocket;
        private readonly Action<string, Socket> _broadcastMessage;

        public ClientHandler(Socket clientSocket, Action<string, Socket> broadcastMessage)
        {
            _clientSocket = clientSocket;
            _broadcastMessage = broadcastMessage;
        }

        public void HandleClient()
        {
            try
            {
                while (true)
                {
                    byte[] lengthBytes = new byte[4];
                    int bytesRec = _clientSocket.Receive(lengthBytes);
                    if (bytesRec == 0) break; 

                    int messageLength = BitConverter.ToInt32(lengthBytes, 0);          // Konverterer byte array til int

                    byte[] messageBytes = new byte[messageLength];                     // Modtager besked fra client og konverterer til byte array
                    bytesRec = _clientSocket.Receive(messageBytes);                    // Modtager besked fra client og lægger i messageBytes
                    string data = Encoding.ASCII.GetString(messageBytes, 0, bytesRec); // Konverterer byte array til string

                    Console.WriteLine("Text received : {0}", data);
                    
                    _broadcastMessage(data, _clientSocket);
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Client disconnected abruptly.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                try
                {
                    _clientSocket.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException) { }
                _clientSocket.Close();
            }
        }
    }
}
