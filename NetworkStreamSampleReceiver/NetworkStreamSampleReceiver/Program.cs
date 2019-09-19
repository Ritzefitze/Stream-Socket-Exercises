using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace NetworkStreamSampleReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string ServerName = "127.0.0.1";
            int Port = 20000;
            Socket ServerSocket = null;
            Socket ListeningSocket = null;
            IPEndPoint ListeningEndPoint = null;
            NetworkStream ServerNetworkStream = null;
            int BytesRead = 0;
            byte[] ReadBuffer = new byte[4096];


            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    if (String.Compare(args[i], "/port", true) == 0)
                    {
                        Port = System.Convert.ToInt32(args[++i].ToString());
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Usage: " + args[0] + "[/port<port number>]");
                    return;
                }
            }


            try
            {
                ListeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                ListeningEndPoint = new IPEndPoint(IPAddress.Parse(ServerName), Port);
                ListeningSocket.Bind(ListeningEndPoint);
                ListeningSocket.Listen(5);

                Console.WriteLine("Awaiting a TCP connection on IP: "
                    + ListeningEndPoint.Address.ToString()
                    + " Port: "
                    + ListeningEndPoint.Port.ToString()
                    + "...");

                ServerSocket = ListeningSocket.Accept();
                Console.WriteLine("Received a connection - awaiting data...");
            }
            catch (SocketException se)
            {
                Console.WriteLine("Failure to create Sockets: " + se.Message);
                return;
            }
            finally
            {
                ListeningSocket.Close();
            }


            try
            {
                try
                {
                    ServerNetworkStream = new NetworkStream(ServerSocket, true);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to create a Network Stream with error: " + ex.Message);
                }


                try
                {
                    do
                    {
                        BytesRead = ServerNetworkStream.Read(ReadBuffer, 0, ReadBuffer.Length);
                        Console.WriteLine("We read " + BytesRead.ToString() + " byte(s) from a peer socket:");
                        for (int i = 0; i < BytesRead; i++)
                        {
                            Console.WriteLine("The byte #" + i.ToString() + " contains " + ReadBuffer[i].ToString());
                        }
                    }
                    while (BytesRead > 0);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to read from a network stream with error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ServerNetworkStream.Close();
                Console.ReadKey();
            }

        }
    }
}
