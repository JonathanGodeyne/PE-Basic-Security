using System.Text;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Crypt_Lib
{
    public class AesUtil : IDisposable
    {
        private const int Aes256BitKeySize = 256;
        private AesManaged _aesManaged;
        private readonly FileUtil _fileUtil;

        public AesUtil(byte[] aesKey)
        {
            _aesManaged = new AesManaged();
            _fileUtil = new FileUtil();
            _aesManaged.GenerateIV();
            _aesManaged.KeySize = Aes256BitKeySize;
            if (_aesManaged.ValidKeySize(aesKey.ToString().Length))
                _aesManaged.Key = aesKey;
        }

        public AesUtil()
        {
            _aesManaged = new AesManaged();
            _fileUtil =  new FileUtil();
            _aesManaged.GenerateIV();
            _aesManaged.KeySize = Aes256BitKeySize;
            _aesManaged.GenerateKey();
        }

        public void Dispose()
        {
            _aesManaged.Dispose();
        }

        public void setKey(byte[] key)
        {
            _aesManaged.Key = key;

        }

        
        public byte[] getKey()
        {
            return _aesManaged.Key;
        }

        
        public byte[] EncryptStringToBytes_Aes(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Equals(String.Empty))
                throw new ArgumentNullException("empty string");

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

        //TODO vorm methode om zodat alleen de lokatie van de file moet gegeven worden
        public string DecryptStringFromBytes_Aes(string fileLocation)
        {
            if (!File.Exists(fileLocation))
            {
                throw new FileNotFoundException("The location submitted does not contain a valid file");
            }
            byte[] cipherText = File.ReadAllBytes(fileLocation);
            
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
                        using (var srDecrypt = new StreamReader(csDecrypt, new UnicodeEncoding()))
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