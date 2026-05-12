using System;
using System.Net.Sockets;
using System.Text;

public class TupleSpaceWorker
{
    public static void HandleClient(object clientObject)
    {
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

        TcpClient client = (TcpClient)clientObject;

        NetworkStream stream = client.GetStream();

        //reads clients header and body and then gets a response
        try
        {
            while(client.Connected)
            {
                byte[] headerBuffer = new byte[4];
                
                int totalHeaderRead = 0;

                while(totalHeaderRead < 4)
                {
                    int bytesRead = stream.Read(headerBuffer, totalHeaderRead, 4 - totalHeaderRead);//Getx header
                    
                    if(bytesRead == 0)
                    {
                        break;
                    }
                    
                    totalHeaderRead += bytesRead;
                }

                string header = Encoding.ASCII.GetString(headerBuffer);
                int totalLength = int.Parse(header.Substring(0,3));
                int bodyLength = totalLength - 4;

                byte[] bodyBuffer = new byte[bodyLength];

                int totalBytesRead = 0;

                while(totalBytesRead < bodyLength)
                {
                    int bytesRead = stream.Read(bodyBuffer, totalBytesRead, bodyLength - totalBytesRead);//Gets body

                    if(bytesRead == 0)
                    {
                        break;
                    }

                    totalBytesRead += bytesRead;
                }

                string requestBody = Encoding.ASCII.GetString(bodyBuffer);
                string responseBody = TupleSpaceServer.HandleRequest(requestBody); //get request from turple space server
                int responseLength = responseBody.Length + 4;

                string framedResponse = responseLength.ToString("D3") + " " + responseBody;
                byte[] responseBytes = Encoding.ASCII.GetBytes(framedResponse);

                stream.Write(responseBytes, 0, responseBytes.Length);
                stream.Flush();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        stream.Close();
        client.Close();

    }
}