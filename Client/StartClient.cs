using System.Net.Sockets;
using System.Net;
using System.Text;

StartClient();
return 0;

static void StartClient()
{
    byte[] bytes = new byte[1024];

    try
    {
        /// <summary>
        /// Connect to a Remote server
        /// Get Host IP Address that is used to establish a connection
        /// In this case, we get one IP address of localhost that is IP : 127.0.0.1
        /// If a host has multiple addresses, you will get a list of addresses
        /// </summary>
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        // Create a TCP/IP  socket.
        Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sender.Connect(remoteEP);
            Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

            // Start a new thread to continuously receive messages from the server
            Thread receiveThread = new Thread(() => ReceiveMessages(sender));
            receiveThread.Start();

            while (true)
            {
                Console.Write("Input besked (tryk e for exit): ");
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
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        catch (ArgumentNullException ane)
        {
            Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}

static void ReceiveMessages(Socket sender)
{
    // container for beskeder der streames til socket
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
        Console.WriteLine("SocketException : {0}", se.ToString());
    }
    catch (Exception e)
    {
        Console.WriteLine("Unexpected exception : {0}", e.ToString());
    }
}

