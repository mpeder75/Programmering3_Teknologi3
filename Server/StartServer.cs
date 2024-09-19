using Server.Handler;

StartServer();
return 0;
    
static void StartServer()
{
    // Create an instance of ServerSocketHandler and start the server
    ServerSocketHandler serverSocketHandler = new ServerSocketHandler("127.0.0.1", 11000);
    serverSocketHandler.Start();

    Console.WriteLine("Server startet...");
    Console.WriteLine("Venter på indkommende connections...");
    Console.WriteLine(new String('_',50));
    Console.ReadKey();

    serverSocketHandler.Stop();
}
