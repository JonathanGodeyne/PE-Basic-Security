using System;
using System.Security.Cryptography;
using System.Text;
using RSACryptoServiceProviderExtensions;

namespace Crypt_Lib
{
    public class RsaUtil : IDisposable
    {
        private readonly FileUtil _fileUtill;
        private readonly RSACryptoServiceProvider rsa;
        private const int Rsa2048BitKeysize = 2048;

        
        // configureer dependency injection voor FileUtil
        public RsaUtil()
        {
            
            rsa = new RSACryptoServiceProvider();
            rsa.KeySize = Rsa2048BitKeysize;
            
            
            _fileUtill = new FileUtil();
            
        }

        public byte[] RsaSign(Byte[] dataToSign)
        {
            try
            {
                return rsa.SignData(dataToSign, new SHA256Managed());
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private void importKey(string personName)
        {
            try
            {
                var key = _fileUtill.getKeyFromPerson(personName);
                rsa.FromXmlString(key);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            

        }
        

        public byte[] RsaEncrypt(byte[] DataToEncrypt)
        {
            try
            {
                byte[] encryptedData;

                encryptedData = rsa.Encrypt(DataToEncrypt, false);

                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public bool RsaVerify(byte[] encryptedData, byte[] signature)
        {
            return rsa.VerifyData(encryptedData,new SHA256Managed(), signature);

        }

        public byte[] RsaDecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo)
        {
            try
            {
                byte[] decryptedData;
                decryptedData = rsa.Decrypt(DataToDecrypt, false);

                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }


        public void Dispose()
        {
            rsa?.Dispose();
        }

        public void writekeyToFile(string personName)
        {
            personName = personName.ToLower();
            string privateKey = rsa.ToXmlStringExtension(true);
            string publicKey = rsa.ToXmlStringExtension(false);
            //save the private Key to private_filename
            UnicodeEncoding encoding = new UnicodeEncoding();
            
            
            _fileUtill.WriteKeyToFile(personName+"_rsa", encoding.GetBytes(privateKey), personName);
            _fileUtill.WriteKeyToFile(personName+"_rsa.pub", encoding.GetBytes(publicKey), personName);



        }
    }
}