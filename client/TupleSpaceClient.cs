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
    }



    private static bool SendRequest(NetworkStream stream, string requestBody)
    {
        // TODO:
        // 1. Frame the message as "NNN body".
        // 2. Convert it to ASCII bytes.
        // 3. Write it to the stream.
        // 4. Flush the stream.
        // 5. Return true if successful, otherwise false.

        return false;
    }
}