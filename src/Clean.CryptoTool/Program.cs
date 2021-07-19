using System;
using Clean.Core.Utils.Crypto;
using Microsoft.Extensions.Configuration;

namespace Clean.CryptoTool
{
    class Program
    {
        static void Main()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var cryptoKey = config["CryptoKey"];

            while (true)
            {
                Console.Write("Would you like to encrypt or decrypt (e/d): ");
                var operation = Console.ReadKey();
                Console.WriteLine(string.Empty);

                switch (operation.KeyChar)
                {
                    case 'e':
                        Console.Write("Enter your plaintext: ");
                        var plainText = Console.ReadLine();
                        Encrypt(key: cryptoKey, plainText: plainText);
                        break;
                    case 'd':
                        Console.Write("Enter your encrypted text: ");
                        var encryptedText = Console.ReadLine();
                        Decrypt(key: cryptoKey, encryptedText: encryptedText);
                        break;
                    default:
                        break;
                }
            }
        }

        static void Encrypt(string key, string plainText)
        {
            Console.WriteLine($"Plaintext: {plainText}");
            Console.WriteLine($"Encrypted Text: {AES.Encrypt(key, plainText)}");
            Console.WriteLine(string.Empty);
        }

        static void Decrypt(string key, string encryptedText)
        {
            Console.WriteLine($"Encrypted Text: {encryptedText}");
            Console.WriteLine($"Decrypted Text: {AES.Decrypt(key, encryptedText)}");
            Console.WriteLine(string.Empty);
        }
    }
}
