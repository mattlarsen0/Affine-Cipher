using System;
using System.IO;

namespace Affine_Cipher
{
    class Program
    {
        private const string ENCRYPTED_FILE_NAME = "encryptedResults.txt";
        private const string DECRYPTED_FILE_NAME = "decryptedResults.txt";    

        static void Main(string[] args)
        {    
            int key = -1;
            string fileName = null;

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
                if (args[i].Length > 1)
                {
                    if (args[i][0] == '-' && i + 1 < args.Length)
                    {
                        switch (args[i][1])
                        {
                            case 'k':
                                key = int.Parse(args[i+1]);
                                i++;
                                break;
                            case 'f':
                                fileName = args[i+1];
                                i++;
                                break;
                        }
                    }
                }
            }

            if (key == -1)
            {
                Console.WriteLine("Key was not provided.");
                return;
            }
            else if (fileName == null)
            {
                Console.WriteLine("Filename was not provided.");
                return;
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine("Filename was not found.");
                return;
            }
            else if (!AffineCipherEncryptor.IsKeyValid(key))
            {
                Console.WriteLine("Key is not coprime with " + LetterConstants.NUM_CHARS);
                return;
            }

            string text = File.ReadAllText(fileName);

            // Encrypt the text
            string encryptedText = AffineCipherEncryptor.Encrypt(text, key);

            // Decrypt the text
            string decryptedText = AffineCipherEncryptor.Decrypt(encryptedText, key);
            
            // Write encrypted text to file
            File.WriteAllText(ENCRYPTED_FILE_NAME, encryptedText);

            // Write decrypted text to file
            File.WriteAllText(DECRYPTED_FILE_NAME, decryptedText);

            Console.WriteLine("Finished Encryption and Decryption. Press Enter to Exit.");
            Console.ReadLine();
        }
    }
}
