﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PingPongServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
          //  int port =int.Parse(args[0]);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine("Waiting for first connection ... ");

                while (true)
                {
                    Socket clientSocket = listener.Accept();

                    HandleClinet handleClinet = new HandleClinet();
                    handleClinet.Connect(clientSocket);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
      
    }
} 
