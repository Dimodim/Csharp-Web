using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback,12345);

            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                using (NetworkStream stream = client.GetStream())
                {
                    string NewLine = "\r\n";

                    var requestByts = new byte[100000];

                    int readBytes = stream.Read(requestByts, 0, requestByts.Length);
                    var stringRequest = Encoding.UTF8.GetString(requestByts, 0, readBytes);

                    Console.WriteLine(new string ('=', 80));
                    Console.WriteLine(stringRequest);
                    
                    string html = "<h1>Welcom to this magnificant creaton</h1>"; 

                    string response = "HTTP/1.0 201 Created" + NewLine +
                                      "Content-Type: text/html" + NewLine +
                                      "Server: MyCustomServer/1.0" + NewLine+
                                      $"Content-Length:{html.Length}" + NewLine + NewLine +
                                      html;
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }

            }

        }
        

    }
}
