using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TextIO
{
    class Program
    {

        // <summary>

        // The main entry point for the application.

        // </summary>

        [STAThread]

        static void Main(string[] args)

        {

            StreamWriter MyStreamWriter = null;

            // Let's write a string to a file named Jim.txt

            try

            {

                try

                {

                    MyStreamWriter = new StreamWriter(".\\Jim.txt");

                    Console.WriteLine("StreamWriter() is OK, ready for writing");

                }

                catch (Exception e)

                {

                    throw new Exception("Failed to create a stream writer with error: " + e.Message);

                }

 

                try

                {

                    Console.WriteLine("Writing some text into a file using StreamWriter()...");

                    MyStreamWriter.WriteLine("Using stream writers is easy!");

                    MyStreamWriter.WriteLine("Using stream readers is also easy!");

                    MyStreamWriter.WriteLine("Yes it is true!");

                }

                catch (Exception e)

                {

                    throw new Exception("Failed to write using a stream writer with error: " + e.Message);

                }

                Console.WriteLine("Finished writing text to a file");

            }

            catch (Exception e)

            {

                Console.WriteLine(e.Message);

                return;

            }

            finally

            {

                MyStreamWriter.Close();

                Console.WriteLine("Close() is OK");

            }

 

            StreamReader MyStreamReader = null;

 

            try

            {

                try

                {

                    MyStreamReader = new StreamReader(".\\Jim.txt");

                    Console.WriteLine("StreamReader() is OK, ready for reading");

                }

                catch (Exception e)

                {

                    throw new Exception("Failed to open stream reader with error: " + e.Message);

                }

 

                string FileData = null;

 

                do

                {

                    try

                    {

                        FileData = MyStreamReader.ReadLine();

                        Console.WriteLine("Reading...");

                    }

                    catch (Exception e)

                    {

                        throw new Exception("Failed to read from stream with error: " + e.Message);

                    }

 

                    if (FileData != null)

                    {

                        Console.WriteLine("We read -> " + FileData);

                    }

                }

                while (FileData != null);

 

                Console.WriteLine("Finished reading text from a file.");

            }

            catch (Exception e)

            {

                Console.WriteLine(e.Message);

                return;

            }

            finally

            {

                MyStreamReader.Close();
                Console.ReadKey();

            }

        }
    }
    
}
