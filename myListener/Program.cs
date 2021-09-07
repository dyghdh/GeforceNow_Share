using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace myListener
{

    class Program
    {
        static void Main(string[] args)
        {

            TcpListener server = new TcpListener(IPAddress.Any, 9999);

            server.Start();

            while (true)
            {
                Console.WriteLine("신호 대기중 (상대 : 미접속) \n");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("상대 : 사용중");

            while (true)
            {
                byte[] byteData = new byte[1024];
                try
                {
                    client.GetStream().Read(byteData, 0, byteData.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("상대 : 지포스나우OFF");
                    break;
                }
                
                string strData = Encoding.Default.GetString(byteData);
                int endPoint = strData.IndexOf('\0'); string parsedMessage = strData.Substring(0, endPoint + 1);
                Console.WriteLine(parsedMessage);

                Thread.Sleep(1000);

                }
            }
        }
    }
}