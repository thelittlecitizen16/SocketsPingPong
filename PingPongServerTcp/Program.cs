using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PingPongServerTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];

            TcpListener listener = new TcpListener(ipAddr, 8888);
            Console.WriteLine("Listening...");
            listener.Start();

            Console.WriteLine(" >> " + "Server Started, wait to connection...");

            while (true)
            {
                Console.WriteLine(" >> " + "Client started!");
                TcpClient tcpClient = listener.AcceptTcpClient();

                HandleClient client = new HandleClient();
                client.StartClient(tcpClient);
            }

          
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();

        }
    }
    public class HandleClient
    {
        TcpClient clientSocket;

        public void StartClient(TcpClient inClientSocket)
        {
            clientSocket = inClientSocket;
            Thread ctThread = new Thread(DoChat);
            ctThread.Start();
        }

        private void DoChat()
        {
            while (true)
            {
                try
                {
                    NetworkStream nwStream = clientSocket.GetStream();
                    byte[] buffer = new byte[clientSocket.ReceiveBufferSize];

                    //---read incoming stream---
                    int bytesRead = nwStream.Read(buffer, 0, clientSocket.ReceiveBufferSize);

                    //---convert the data received into a string---
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received : " + dataReceived);

                    //---write back the text to the client---
                    Console.WriteLine("Sending back : " + dataReceived);
                    nwStream.Write(buffer, 0, bytesRead);
                }
                catch (SocketException)
                {
                    clientSocket.Close();
                }
                catch (ObjectDisposedException)
                {
                    clientSocket.Close();
                }
                catch (Exception)
                {
                    clientSocket.Close();
                }
            }

            //byte[] bytesFrom = new byte[10025];
            //string dataFromClient = null;
            //Byte[] sendBytes = null;

            //while ((true))
            //{
            //    try
            //    {
            //        byte[] buffer = new byte[clientSocket.ReceiveBufferSize];
            //        int bytesRead = networkStream.Read(buffer, 0, clientSocket.ReceiveBufferSize);

            //        networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
            //        dataFromClient = Encoding.ASCII.GetString(bytesFrom);
            //      //  dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
            //        Console.WriteLine(" >> " + "From client-" + dataFromClient);

            //        sendBytes = Encoding.ASCII.GetBytes(dataFromClient);

            //        networkStream.Write(sendBytes, 0, sendBytes.Length);
            //        networkStream.Flush();
            //        Console.WriteLine(" >> " + dataFromClient);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(" >> " + ex.ToString());
            //    }
            //}
        }
    }
}
