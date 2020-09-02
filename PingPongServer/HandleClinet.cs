using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PingPongServer
{
    public class HandleClinet
    {
        Socket clientSocket = null;
        public void Connect(Socket clientSockett)
        {
            clientSocket = clientSockett;
            Thread ctThread = new Thread(startClient);
            ctThread.Start();
        }
        public void startClient()
        {
            while (true)
            {
                byte[] bytesReceive = new Byte[1024];
                string data = null;

                try
                {
                    int numByte = clientSocket.Receive(bytesReceive);
                    data = Encoding.ASCII.GetString(bytesReceive,
                                          0, numByte);

                    Console.WriteLine("Text received -> {0} ", data);

                    clientSocket.Send(bytesReceive);
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
