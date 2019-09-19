using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication
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
                Console.WriteLine("Failed to create file with the following error: " + ex.Message);
            }
            finally
            {
                MyFileStream.Close();
                Console.ReadKey();
            }
        }
    }
}
