using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace CryptoStreamSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Let's create a private key that will be used to encrypt and decrypt the data stored in the file Jim.dat.
            byte[] DESKey = { 200, 5, 78, 232, 9, 6, 0, 4 };
            byte[] DESInitializationVector = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CryptoStream MyStreamEncrypter = null;
            FileStream MyFileStream = null;
            byte[] MyByteArray = new byte[10];
            byte[] MyReadBuffer = new byte[1];
            CryptoStream MyStreamDecrypter = null;
            DES DESAlgorithm = null;
            int BytesRead;

            try
            {
                // Let's create a file named Jim.dat in the current working directory 
                try
                {
                    MyFileStream = new FileStream(".\\Jim.dat", FileMode.Create, FileAccess.Write);
                    Console.WriteLine("Jim.dat file created/opened successfully");
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to create/open filestream with error: " + e.Message);
                }

                // Let's create a Symmetric crypto stream using the DES algorithm to encode all the bytes written to the file Jim.
                try
                {
                    Console.WriteLine("Instantiate DESCryptoServiceProvider & CryptoStream, encrypting the file...");
                    DESAlgorithm = new DESCryptoServiceProvider();
                    MyStreamEncrypter = new CryptoStream(MyFileStream, DESAlgorithm.CreateEncryptor(DESKey, DESInitializationVector),
                        CryptoStreamMode.Write);
                }
                catch (Exception e)
                {
                    MyFileStream.Close();
                    throw new Exception("Failed to create DES Symmetric CryptoStream with error: " + e.Message);
                }

                // Let's write 10 bytes to our crypto stream. For simplicity
                // we will write an array of 10 bytes where each byte contains a numeric value 0 - 9. 
                for (short i = 0; i < MyByteArray.Length; i++)
                {
                    MyByteArray[i] = (byte)i;
                }

                try
                {
                    MyStreamEncrypter.Write(MyByteArray, 0, MyByteArray.Length);
                    Console.WriteLine("Writing 10 bytes to the crypto stream...");
                }
                catch (Exception e)
                {
                    throw new Exception("Write failed with error: " + e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            finally
            {
                // Let's close the crypto stream now that we are finished writing data.
                MyStreamEncrypter.Close();
            }

            // Now let's open the encrypted file Jim.dat and decrypt the contents.
            Console.WriteLine();
            Console.WriteLine("Opening the encrypted file and decrypting it...");
            



            try
            {   
                try
                {
                    MyFileStream = new FileStream(".\\Jim.dat", FileMode.Open, FileAccess.Read);
                    Console.WriteLine("Decrypted file opened successfully");
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to open filestream with error: " + e.Message);
                }

                try
                {
                    Console.WriteLine("Instantiate DESCryptoServiceProvider & CryptoStream, decrypting the file...");
                    DESAlgorithm = new DESCryptoServiceProvider();
                    MyStreamDecrypter = new CryptoStream(MyFileStream, DESAlgorithm.CreateDecryptor(DESKey, DESInitializationVector),
                        CryptoStreamMode.Read);
                }
                catch (Exception e)
                {
                    MyFileStream.Close();
                    throw new Exception("Failed to create DES Symmetric CryptoStream with error: " + e.Message);
                }                

                Console.WriteLine("Reading the decrypted file content...");

                while (true)
                {  
                    try
                    {
                        BytesRead = MyStreamDecrypter.Read(MyReadBuffer, 0, MyReadBuffer.Length);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Read failed with error: " + e.Message);
                    }

                    if (BytesRead == 0)
                    {
                        Console.WriteLine("No more bytes to read");
                        break;
                    }
                    Console.WriteLine("Read byte -> " + MyReadBuffer[0].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // We are finished performing IO on the file. We need to close the file to release operating system resources related to the file.
                MyStreamDecrypter.Close();
            }
        }
    }
}
