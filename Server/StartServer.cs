using Server.Handler;

StartServer();
return 0;

    
static void StartServer()
{
    // Create an instance of ServerSocketHandler and start the server
    ServerSocketHandler serverSocketHandler = new ServerSocketHandler("127.0.0.1", 11000);
    serverSocketHandler.Start();

    Console.WriteLine("Press any key to stop the server...");
    Console.ReadKey();

    serverSocketHandler.Stop();
}
