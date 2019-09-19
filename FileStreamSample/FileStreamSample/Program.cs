using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileStreamSample
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream MyFileStream = null;
            try
            {
                MyFileStream = new FileStream(".\\JohnDoe.dat", FileMode.Create, FileAccess.ReadWrite);
                Console.WriteLine("JohnDoe.dat successfully created, check the local folder!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to create(open filestream with error: " + ex.Message);
            }

            byte[] MyByteArray = new byte[10];
            int i;

            for (i = 0; i < MyByteArray.Length; i++)
            {
                MyByteArray[i] = (byte)i;
            }

            try
            {
                MyFileStream.Write(MyByteArray, 0, MyByteArray.Length);
                Console.WriteLine("Write() is OK!");
            }
            catch (Exception ex)
            {
                throw new Exception("Write failed with error: " + ex.Message);
            }

            try
            {
                MyFileStream.Seek(0, SeekOrigin.Begin);
                Console.WriteLine("Seek() is OK");
            }
            catch (Exception ex)
            {
                throw new Exception("Seek failed with error: " + ex.Message);
            }

            byte[] MyReadBuffer = new byte[1];

            try
            {
                while (true)
                {
                    int BytesRead;
                    try
                    {
                        BytesRead = MyFileStream.Read(MyReadBuffer, 0, MyReadBuffer.Length);
                        Console.WriteLine("Read() is OK");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Read failed with error: " + ex.Message);
                    }

                    if (BytesRead == 0)
                    {
                        Console.WriteLine("No more bytes to read!");
                        break;
                    }
                    Console.WriteLine("Read byte -> " + MyReadBuffer[0].ToString());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                MyFileStream.Close();
                Console.WriteLine("Close is OK");
                Console.ReadKey();
            }

        }
    }
}
