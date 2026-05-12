using System;
using System.Net.Sockets;
using System.Text;

public class TupleSpaceWorker
{
    public static void HandleClient(object clientObject)
    {
        TcpClient client = (TcpClient)clientObject;

        NetworkStream stream = client.GetStream();

        try
        {
            while(client.Connected)
            {
                byte[] headerBuffer = new byte[4];
                int headerBytesRead = stream.Read(headerBuffer, 0, 4);

                if (headerBytesRead == 0)
                {
                    break;
                }

                string header = Encoding.ASCII.GetString(headerBuffer);
                int totalLength = int.Parse(header.Substring(0,3));
                int bodyLength = totalLength - 4;

                byte[] bodyBuffer = new byte[bodyLength];

                int totalBytesRead = 0;

                while(totalBytesRead < bodyLength)
                {
                    int bytesRead = stream.Read(bodyBuffer, totalBytesRead, bodyLength - totalBytesRead);

                    if(bytesRead == 0)
                    {
                        break;
                    }

                    totalBytesRead += bytesRead;
                }

                string requestBody = Encoding.ASCII.GetString(bodyBuffer);
                string responseBody = TupleSpaceServer.HandleRequest(requestBody);
                int responseLength = responseBody.Length + 4;

                string framedResponse = responseLength.ToString("D3") + " " + responseBody;
                byte[] responseBytes = Encoding.ASCII.GetBytes(framedResponse);

                stream.Write(responseBytes, 0, responseBytes.Length);

            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        stream.Close();
        client.Close();

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