using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MemoryStreamSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream MyMemoryStream = null;
            byte[] MyByteArray = new byte[4000];
            byte[] MyReadBuffer = new byte[1];
            int TotalBytesWritten = 0;
            int ByteTotal = 0;
            int BytesRead;


            try
            {
                try
                {
                    MyMemoryStream = new MemoryStream();
                    Console.WriteLine("Instantiating new MemoryStream()...");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to create a memory stream with error: " + ex.Message);
                }

                Console.WriteLine("The memory stream has allocated " + MyMemoryStream.Capacity.ToString() + " bytes.");
                Console.WriteLine("Writing 9 x 4000 bytes to memory...");

                for (int i = 0; i < MyByteArray.Length; i++)
                {
                    MyByteArray[i] = 1;
                }

                Console.WriteLine("Writing using Write()...");
                for (int j = 0; j < 9; j++)
                {
                    try
                    {
                        MyMemoryStream.Write(MyByteArray, 0, MyByteArray.Length);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Write failed with error: " + ex.Message);
                    }
                    TotalBytesWritten = TotalBytesWritten + MyByteArray.Length;
                }

                Console.WriteLine("We wrote " + TotalBytesWritten.ToString() + " bytes to the memory stream.");
                Console.WriteLine("The memory stream has allocated " + MyMemoryStream.Capacity.ToString() + " bytes.");
                Console.WriteLine();

                try
                {
                    MyMemoryStream.Seek(0, SeekOrigin.Begin);
                    Console.WriteLine("Seek() is OK");
                }
                catch (Exception ex)
                {
                    throw new Exception("Seek() failed with error: " + ex.Message);
                }

                Console.WriteLine("Reading using Read()...");
                while (true)
                {
                    try
                    {
                        BytesRead = MyMemoryStream.Read(MyReadBuffer, 0, MyReadBuffer.Length);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Read() failed with error: " + ex.Message);
                    }

                    if (BytesRead == 0)
                    {
                        break;
                    }

                    ByteTotal = ByteTotal + BytesRead;
                }
                Console.WriteLine("We read " + ByteTotal.ToString() + " bytes from the memory stream.");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                Console.WriteLine("Closing the MemoryStream...");
                MyMemoryStream.Close();
                Console.ReadKey();

            }
        }
    }
}
