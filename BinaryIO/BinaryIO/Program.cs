using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BinaryIO
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream MyFileStream = null;
            BinaryWriter MyBinaryWriter = null;
            BinaryReader MyBinaryReader = null;
            int Number;

            try
            {
                try
                {
                    MyFileStream = new FileStream(".\\JohnDoe.dat", FileMode.Create, FileAccess.ReadWrite);
                    Console.WriteLine("JohnDoe.dat file created/opened successfully");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to create/open filestream with error: " + ex.Message);
                }

                try
                {
                    MyBinaryWriter = new BinaryWriter(MyFileStream);
                    Console.WriteLine("Instantiate BinaryWriter object...");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to create binary writer with error: " + ex.Message);
                }

                try
                {
                    Console.WriteLine("Writing 4 integers using Write()...");
                    MyBinaryWriter.Write(456);
                    MyBinaryWriter.Write(457);
                    MyBinaryWriter.Write(458);
                    MyBinaryWriter.Write(459);
                    MyBinaryWriter.Flush();
                }
                catch (Exception ex)
                {
                    throw new Exception("Write failed with error: " + ex.Message);
                }

                Console.WriteLine();
                try
                {
                    MyFileStream.Seek(0, SeekOrigin.Begin);
                    Console.WriteLine("Seek is OK...");
                }
                catch (Exception ex)
                {
                    throw new Exception("Seek failed with error: " + ex.Message);
                }

                try
                {
                    MyBinaryReader = new BinaryReader(MyFileStream);
                    Console.WriteLine("Instantiate new BinaryRader object...");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to create binary reader with error: " + ex.Message);
                }

                Console.WriteLine("Reading binary using ReadInt32()...");

                while (true)
                {
                    try
                    {
                        Number = MyBinaryReader.ReadInt32();
                        Console.WriteLine("We read number -> " + Number.ToString());
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to read using binary reader with error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                MyFileStream.Close();
                Console.WriteLine("Closing FileStream...");
                Console.ReadKey();
            }


        }
    }
}
