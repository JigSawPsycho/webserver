using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace WebServer
{
    public class TcpWebServer
    {
        TcpListener listener;
        int port = 8098;

        public TcpWebServer()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(ip, port);
            listener.Start();

            Console.WriteLine("Listening for connection...");

            var listeningThread = new Thread(new ThreadStart(OnListenerStart));
            listeningThread.Start();
        }

        void OnListenerStart()
        {
            listener.BeginAcceptTcpClient(HandleConnection, listener.AcceptTcpClient());
        }

        void HandleConnection(IAsyncResult ar)
        {
            try
            {
                TcpClient client = (TcpClient)ar.AsyncState;
                Console.WriteLine("Connection established");

                NetworkStream stream = client.GetStream();

                byte[] data = new byte[512];

                int bytes = stream.Read(data, 0, data.Length);

                string response = Encoding.ASCII.GetString(data, 0, bytes);

                Console.WriteLine("Response: {0}", response);

            } catch (Exception e)
            {
                Console.WriteLine($"Error establishing connection: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}
