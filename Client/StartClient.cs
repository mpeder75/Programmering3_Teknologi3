using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

StartClient();
return 0;

static void StartClient()
{
    byte[] bytes = new byte[1024];

    try
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

        Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sender.Connect(remoteEP);
            Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

            Thread receiveThread = new Thread(() => ReceiveMessages(sender));
            receiveThread.Start();

            while (true)
            {
                Console.Write("Input message (press 'e' to exit): ");
                string message = Console.ReadLine();

                if (message.ToLower() == "e")
                {
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    break;
                }

                byte[] msg = Encoding.ASCII.GetBytes(message);
                byte[] msgLength = BitConverter.GetBytes(msg.Length);

                sender.Send(msgLength);
                sender.Send(msg);
            }
        }
        catch (SocketException se)
        {
            Console.WriteLine($"SocketException: {se.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected exception: {ex.Message}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception: {e.Message}");
    }
}

static void ReceiveMessages(Socket sender)
{
    byte[] bytes = new byte[1024];
    try
    {
        while (true)
        {
            int bytesRec = sender.Receive(bytes);
            if (bytesRec > 0)
            {
                Console.WriteLine("Broadcasted message = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
            }
        }
    }
    catch (SocketException se)
    {
        Console.WriteLine($"SocketException: {se.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected exception: {ex.Message}");
    }
}