using System;
using System.Collections.Generic;
using System.Threading;

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
    
    public static void Main(string[] args)
    {
        // TODO:
        // Check that exactly one command-line argument was given.
        // Convert it to an integer port number.
        // Check that the port is in the range 50000 to 59999.
        // If invalid, print:
        //      Usage: mono TupleSpaceServer.exe <port>
        //    and stop.


        // Create and start a TCP listener on the port.
        // Start a background thread that runs PrintStatsLoop().
        //
        // STAGE 1:
        // Accept one client connection.
        // Increase totalClients safely.
        // Create a worker thread for that client.
        // Pass the accepted TcpClient into the worker thread.
        // Wait for the worker thread to finish.
        //
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