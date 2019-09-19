using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NetworkStreamSampleSender
{
    class Program
    {
        static void Main(string[] args)
        {
            string ServerName = "127.0.0.1";
            int Port = 20000;
            Socket ClientSocket = null;
            IPEndPoint ServerEndPoint = null;
            NetworkStream ClientNetworkStream = null;
            byte[] Buffer = new byte[1];

            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    if (String.Compare(args[i], "/server", true) == 0)
                    {
                        ServerName = args[++i].ToString();
                    }
                    else if (String.Compare(args[i], "/port", true) == 0)
                    {
                        Port = Convert.ToInt32(args[++i].ToString());
                    }
                }
                catch(IndexOutOfRangeException)
                {
                    Console.WriteLine("Usage: " + args[0] + " [/server <server IP>] [/port <port>]");
                    return;
                }
            }


            try
            {
                try
                {
                    Console.WriteLine("Creating a client Socket...");
                    ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                }
                catch (Exception ex)
                {
                    Console.Write("Failed to create client Socket: ");
                    throw new Exception(ex.Message);
                }

                ServerEndPoint = new IPEndPoint(IPAddress.Parse(ServerName), Convert.ToInt16(Port));

                try
                {
                    ClientSocket.Connect(ServerEndPoint);
                    Console.WriteLine("Connect() is OK, connectin to " + ServerName + " at " + Port + "...");
                }
                catch (Exception ex)
                {
                    Console.Write("Failed to create client Socket: ");
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Closing the Connect()...");
                ClientSocket.Close();
                return;
            }

            try
            {
                try
                {
                    Console.WriteLine("Instantiate NetworkStream object for communication...");
                    ClientNetworkStream = new NetworkStream(ClientSocket, true);
                }
                catch (Exception ex)
                {
                    ClientSocket.Close();
                    throw new Exception("Failed to create a NetworkStream with error: " + ex.Message);
                }

                try
                {
                    Console.WriteLine("Wrinting/sending integers 0 - 200...");
                    for (int i = 0; i < 200; i++)
                    {
                        Buffer[0] = (byte)i;
                        ClientNetworkStream.Write(Buffer, 0, Buffer.Length);
                    }
                    Console.WriteLine("We wrote 200 bytes to the server.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to write to client NetworkStream with error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Closing the NetworkStream...");
                ClientNetworkStream.Close();
                Console.ReadKey();
            }
        }
    }
}
