using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PingPongServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Waiting connection ... ");
                    Socket clientSocket = listener.Accept();

                    byte[] bytesReceive = new Byte[1024];
                    string data = null;

                    int numByte = clientSocket.Receive(bytesReceive);
                    data += Encoding.ASCII.GetString(bytesReceive,
                                               0, numByte);

                    Console.WriteLine("Text received -> {0} ", data);

                    clientSocket.Send(bytesReceive);
                   // clientSocket.Shutdown(SocketShutdown.Both);
                   // clientSocket.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
} 

        }
    }
}
