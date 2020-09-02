using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PingPongServerTcp
{
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

                    int bytesRead = nwStream.Read(buffer, 0, clientSocket.ReceiveBufferSize);

                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received and Sending back: " + dataReceived);

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
        }
    }
}
