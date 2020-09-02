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

            TcpListener serverSocket = new TcpListener(ipAddr, 8888);
            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started, wait to connection...");
            var counter = 0;

            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();

                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                // handleClinet client = new handleClinet();
                //client.startClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();

        }
    }
    public class HandleClient
    {
        TcpClient clientSocket;

        public void StartClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(DoChat);
            ctThread.Start();
        }

        private void DoChat()
        {
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            Byte[] sendBytes = null;

            while ((true))
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();

                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                  //  dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> " + "From client-" + dataFromClient);

                    sendBytes = Encoding.ASCII.GetBytes(dataFromClient);

                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + dataFromClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }
}
