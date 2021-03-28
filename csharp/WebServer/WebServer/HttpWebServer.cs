using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace WebServer
{
    public class HttpWebServer
    {
        HttpListener listener;
        Thread[] activeThreads = new Thread[5];
        int usedThreads = 0;

        public HttpWebServer(string ip = "127.0.0.1", int port = 80)
        {
            Thread listenerThread = new Thread(SetupListenerThread);
            activeThreads[usedThreads] = listenerThread;
            usedThreads++;

            listenerThread.Start();
        }

        void SetupListenerThread()
        {
            listener = new HttpListener();

            listener.Prefixes.Add("http://127.0.0.1:8696/");

            listener.Start();

            while (true)
            {
                HandleIncomingConnections();
            }
        }

        void HandleIncomingConnections()
        {
            HttpListenerContext incomingConnection = listener.GetContext();

            HttpListenerRequest request = incomingConnection.Request;
            HttpListenerResponse response = incomingConnection.Response;

            Console.WriteLine("Connection established with {0}", request.UserHostAddress);
        }
    }
}
