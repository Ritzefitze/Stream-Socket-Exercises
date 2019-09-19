using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace BufferedStreamSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // The following is the default values
            string ServerName = "127.0.0.1";
            int Port = 20000;
            Socket ClientSocket = null;
            IPEndPoint ServerEndPoint = null;
            NetworkStream ClientNetworkStream = null;
            BufferedStream ClientBufferedStream = null;


            // Parse command line arguments if any
            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    if (String.Compare(args[i], "/server", true) == 0)
                    {
                        // The server's name we will connect to
                        ServerName = args[++i].ToString();
                    }
                    else if (String.Compare(args[i], "/port", true) == 0)
                    {
                        // The port on which the server is listening
                        Port = System.Convert.ToInt32(args[++i].ToString());
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("Usage: Executable_file_name [/server <server IP>] [/port <port>]");
                    return;
                }
            }        



            try
            {
                // Let's connect to a listening server
                try
                {
                    ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    Console.WriteLine("Socket() is OK...");
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to create client Socket: " + e.Message);
                }

                ServerEndPoint = new IPEndPoint(IPAddress.Parse(ServerName), Convert.ToInt16(Port));

                try
                {
                    ClientSocket.Connect(ServerEndPoint);
                    Console.WriteLine("Connect() is OK...");
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to connect client Socket: " + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ClientSocket.Close();
                return;
            }

            
            // Let's create a network stream to communicate over the connected Socket. 
            try
            {
                try
                {
                    // Setup a network stream on the client Socket
                    Console.WriteLine("Instantiate NetworkStream object...");
                    ClientNetworkStream = new NetworkStream(ClientSocket, true);
                }
                catch (Exception e)
                {
                    // We have to close the client socket here because the network stream did not take ownership of the socket.
                    ClientSocket.Close();
                    throw new Exception("Failed to create a NetworkStream with error: " + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ClientNetworkStream.Close();
                return;
            }  


            try
            {
                try
                {
                    // Setup a network stream on the client Socket
                    Console.WriteLine("Instantiate BufferedStream object...");
                    ClientBufferedStream = new BufferedStream(ClientNetworkStream);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to create a BufferedStream with error: " + e.Message);
                }


                try
                {
                    Console.WriteLine("Writing/sending data...");
                    byte[] Buffer = new byte[1];
                    for (int i = 0; i < 200; i++)
                    {
                        Buffer[0] = (byte)i;
                        ClientBufferedStream.Write(Buffer, 0, Buffer.Length);
                    }
                    Console.WriteLine("We wrote 200 bytes one byte at a time to the server.");
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to write to client BufferedStream with error: " + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // We are finished with the Stream so we will close it.
                ClientBufferedStream.Close();
                Console.ReadKey();
            }
        }
    }
}
