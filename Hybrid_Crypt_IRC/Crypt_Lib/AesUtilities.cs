using System;
using System.IO;
using System.Security.Cryptography;

namespace Crypt_Lib
{
    public class AesUtilities : IDisposable
    {
        private const int Aes256BitKeySize = 256;
        private AesManaged _aesManaged;
        private readonly FileUtil _fileUtil;

        public AesUtilities(byte[] aesKey)
        {
            _fileUtil = new FileUtil();
            _aesManaged.GenerateIV();
            _aesManaged.KeySize = Aes256BitKeySize;
            if (_aesManaged.ValidKeySize(aesKey.ToString().Length))
                _aesManaged.Key = aesKey;
        }

        public void Dispose()
        {
            _aesManaged.Dispose();
        }

        private void WriteKeyToFile()
        {
            _fileUtil.WriteKeyToFile("Aes_key.aes", _aesManaged.Key);
        }

        public static byte[] Generate256BitKey()
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = Aes256BitKeySize;
                aes.GenerateKey();
                return aes.Key;
            }
        }

        
        private byte[] EncryptStringToBytes_Aes(byte plainText)
        {
            // Check arguments.
            if (plainText == null || plainText <= 0)
                throw new ArgumentNullException("byte");

            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = _aesManaged.Key;
                aesAlg.IV = _aesManaged.IV;

                // Create an encryptor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        //todo vorm methode om zodat alleen de lokatie van de file moet gegeven worden
        private string DecryptStringFromBytes_Aes(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");


            // Declare the string used to hold
            // the decrypted text.
            var plaintext = string.Empty;

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = _aesManaged.Key;
                aesAlg.IV = _aesManaged.IV;

                // Create a decryptor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}