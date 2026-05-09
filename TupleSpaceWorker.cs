using System;
using System.Net.Sockets;
using System.Text;

public class TupleSpaceWorker
{
    public static void HandleClient(object clientObject)
    {
        TcpClient client = (TcpClient)clientObject;

        // TODO:
        // 1. Cast the object to TcpClient.
        // 2. Get the NetworkStream from the client.
        // 3. Repeatedly read one request from the client using the framed protocol.
        // 4. Call TupleSpaceServer.HandleRequest(...) to process that request.
        // 5. Send the response back to the client using the framed protocol.
        // 6. Stop when the client disconnects.
        //
        // You may create extra helper methods if you want,
        // but they are not required.
    }
}