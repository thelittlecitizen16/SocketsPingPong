using System;
using System.Net;
using System.Net.Sockets;

namespace PingPongServerTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];

            TcpListener listener = new TcpListener(ipAddr, 8888);
            listener.Start();

            Console.WriteLine("wait to connection...");

            while (true)
            {
                TcpClient tcpClient = listener.AcceptTcpClient();

                HandleClient client = new HandleClient();
                client.StartClient(tcpClient);
            }
        }
    }
}
