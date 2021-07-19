using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Clean.Core.Utils.Crypto
{
    //https://www.c-sharpcorner.com/article/encryption-and-decryption-using-a-symmetric-key-in-c-sharp/
    public static class AES
    {
        public static string Encrypt(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new ();
                using CryptoStream cryptoStream = new (memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new (cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new (buffer);
            using CryptoStream cryptoStream = new ((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new ((Stream)cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}
