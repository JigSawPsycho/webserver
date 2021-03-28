using System;
using System.Net;
using System.Net.Sockets;

namespace WebServer
{
    class WebServerManager
    {
        static void Main()
        {
            var server = new HttpWebServer();

            Console.ReadKey();
        }

    }
}
