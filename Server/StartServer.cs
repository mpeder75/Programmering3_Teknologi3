using Server.Handler;

StartServer();
return 0;

static void StartServer()
{
    // Create an instance of ServerSocketHandler and start the server
    ServerSocketHandler serverSocketHandler = new ServerSocketHandler("127.0.0.1", 5000);
    serverSocketHandler.Start();

    Console.WriteLine("Server started...");
    Console.WriteLine("Waiting for incoming connections...");
    Console.WriteLine(new string('_', 50));
    Console.ReadKey();

    serverSocketHandler.Stop();
}