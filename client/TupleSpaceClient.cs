using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class TupleSpaceClient
{
    public static void Main(string[] args)
    {
        // TODO:
        // 1. Check that exactly three command-line arguments were given:
        //      - server hostname
        //      - server port
        //      - input file
        //
        // 2. Convert the port number to an integer.
        // 3. Check that the input file exists.
        // 4. Connect to the server using TcpClient.
        // 5. Open the input file and read each request line.
        // 6. Convert each request into the short protocol form.
        // 7. Send the request to the server using the framed protocol.
        // 8. Read the framed response from the server.
        // 9. Print the original request and the server response.
        //
        // You may write extra helper methods if you want,
        // but they are not required.

        if(args.Length != 3){
            Console.WriteLine("3 command lines required");
            return;
        }

        string host = args[0];

        int port = 0;

        if(int.TryParse(args[1], out port)){
            Console.WriteLine("Client port set to: " + port);
        }
        else{
            Console.WriteLine("Error with input");
            return;
        }

        string filePath = args[2];

        if(!File.Exists(filePath))
        {
            Console.WriteLine("input file does not exist");
            return;
        }
        
        try
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            string[] lines = File.ReadAllLines(filePath);

            foreach(string line in lines)
            {
                string request = line.Trim();

                if(string.IsNullOrWhiteSpace(request))
                    continue ;

                string[] parts = request.Split(' ');

                bool valid = false;

                if (parts.Length > 0 &&(parts[0]=="R" || parts[0]=="G")&&parts.Length==2)
                {
                    valid = true;
                }
                else if(parts[0] == "P" && parts.Length == 3)
                {
                    int value;

                    if(int.TryParse(parts[2], out value) && value >= 0)
                    {
                        valid = true;
                    }
                }

                if(!valid)
                {
                    Console.WriteLine("Invalid request skipped:"+ request);
                    continue;
                }

                bool success = SendRequest(stream, request);

                if(!success)
                {
                    Console.WriteLine("Failed to send request");
                    continue;
                }

                byte[] headerBuffer=new byte[4];
                int totalHeaderRead = 0;

                while(totalHeaderRead < 4)
                {
                    int bytesRead = stream.Read(headerBuffer, totalHeaderRead, 4 - totalHeaderRead);

                    if(bytesRead == 0)
                        return;

                    totalHeaderRead += bytesRead;
                }

                string header=Encoding.ASCII.GetString(headerBuffer);

                int responseLength = int.Parse(header.Substring(0,3));
                int bodyLength = responseLength - 4;

                byte[] bodyBuffer = new byte[bodyLength];
                int totalBodyRead = 0;

                while (totalBodyRead < bodyLength)
                {
                    int bytesRead = stream.Read(bodyBuffer, totalBodyRead, bodyLength- totalBodyRead);

                    if(bytesRead == 0)
                        break;

                    totalBodyRead += bytesRead;
                }

                string response = Encoding.ASCII.GetString(bodyBuffer);

                Console.WriteLine("Request: " + request);
                Console.WriteLine("Response: " + response);
                Console.WriteLine();

                
            }

            stream.Close();
            client.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }


    }



    private static bool SendRequest(NetworkStream stream, string requestBody)
    {
        // TODO:
        // 1. Frame the message as "NNN body".
        // 2. Convert it to ASCII bytes.
        // 3. Write it to the stream.
        // 4. Flush the stream.
        // 5. Return true if successful, otherwise false.
        string message = requestBody;
        int length = message.Length + 4;

        string framed = length.ToString("D3") + " " + message;
        byte[] data = Encoding.ASCII.GetBytes(framed);

        try
        {
            stream.Write(data, 0, data.Length);
            stream.Flush();
            return true;
        }
        catch
        {
            return false;
        }
    }
}