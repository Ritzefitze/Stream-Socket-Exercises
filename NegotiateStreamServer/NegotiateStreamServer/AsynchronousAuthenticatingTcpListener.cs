using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text;
using System.IO;
using System.Threading;

namespace NegotiateStreamServer
{
    public class AsynchronousAuthenticatingTcpListener
    {
        public static void Main(string[] args)
        {
            // Create an IPv4 TCP/IP socket.
            String Addr = "127.0.0.1";
            TcpListener listener = new TcpListener(IPAddress.Parse(Addr), 20000);
            // Listen for incoming connections
            listener.Start();
            Console.WriteLine("I'm listening for client connection...");
            while (true)
            {
                TcpClient clientRequest = null;
                // Application blocks while waiting for an incoming connection. Press CNTL-C to terminate the server.
                clientRequest = listener.AcceptTcpClient();
                Console.WriteLine("Client connected...");
                Console.WriteLine("IP: {0}, Port: 20000...", Addr);
                // A client has connected.
                try
                {
                    AuthenticateClient(clientRequest);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }
        }

        public static void AuthenticateClient(TcpClient clientRequest)
        {
            NetworkStream stream = clientRequest.GetStream();
            // Create the NegotiateStream.
            NegotiateStream authStream = new NegotiateStream(stream, false);
            // Save the current client and NegotiateStream instance in a ClientState object.
            ClientState cState = new ClientState(authStream, clientRequest);
            // Listen for the client authentication request
            authStream.BeginAuthenticateAsServer(new AsyncCallback(EndAuthenticateCallback), cState);
            // Wait until the authentication completes.
            cState.Waiter.WaitOne();
            cState.Waiter.Reset();
            authStream.BeginRead(cState.Buffer, 0, cState.Buffer.Length, new AsyncCallback(EndReadCallback), cState);
            cState.Waiter.WaitOne();
            // Finished with the current client.
            authStream.Close();
            clientRequest.Close();
        }

        // The following method is invoked by the BeginAuthenticateAsServer callback delegate.
        public static void EndAuthenticateCallback(IAsyncResult ar)
        {

        }
    }
}
