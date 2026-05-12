using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TupleSpaceServer
{

    #region  Global Variables

    private static readonly Dictionary<string, string> tupleSpace = new Dictionary<string, string>();
    private static readonly object stateLock = new object();

    private static int totalClients = 0;
    private static int totalOperations = 0;
    private static int readCount = 0;
    private static int getCount = 0;
    private static int putCount = 0;
    private static int errorCount = 0;

    #endregion

    #region Getters and Setters
    #endregion
    
    private static TcpListener listener;

    public static void Main(string[] args)
    {
        // TODO:
        // Check that exactly one command-line argument was given.
        // Convert it to an integer port number.
        // Check that the port is in the range 50000 to 59999.
        // If invalid, print:
        //      Usage: mono TupleSpaceServer.exe <port>
        //    and stop.
        if(args.Length != 1){
            Console.WriteLine("Usage: mono TupleSpaceServer.exe <port>");
            return;
        }

        int port = 0;

        if(int.TryParse(args[0], out port)){
            Console.WriteLine("Port set to: " + port);
        }else{
            Console.WriteLine("Error with input");
            return;
        }
        
        if(port < 50000 || port > 59999){
            Console.WriteLine("port is out of range " + port);
            return;
        }

        // Create and start a TCP listener on the port.
        // Start a background thread that runs PrintStatsLoop().
        //

        var hostAddress = IPAddress.Parse("127.0.0.1");
        listener = new TcpListener(hostAddress, port);
        listener.Start();

        Thread printStatsThread = new Thread(PrintStatsLoop);
        printStatsThread.Start();

        // STAGE 1:
        // Accept one client connection.
        // Increase totalClients safely.
        // Create a worker thread for that client.
        // Pass the accepted TcpClient into the worker thread.
        // Wait for the worker thread to finish.
        //

        TcpClient client = listener.AcceptTcpClient();

        lock(stateLock){
            totalClients ++;
        }

        Thread workerThread = new Thread(new ParameterizedThreadStart(TupleSpaceWorker.HandleClient));
        workerThread.Start(client);
        
        workerThread.Join();

        // STAGE 2:
        // Change the server so it accepts clients in a loop.
        // Start a new worker thread for each client.
        // Do not wait immediately for each worker thread.
    }

    public static string HandleRequest(string requestBody)
    {
        // TODO:
        // Parse the request body.
        // Work out whether the request is READ, GET, or PUT.
        // Access tupleSpace.
        // Update the counters.
        // Return the correct response string.
        // When multiple worker threads are running, shared state must be protected.
        return "ERR not implemented";
    }

    private static void PrintStatsLoop()
    {
        while (true)
        {
            Thread.Sleep(10000);
            PrintStats();
        }
    }

    private static void PrintStats()
    {
        // TODO:
        // Print the current tuple space statistics.
        // This method should read shared data safely.
    }
}