using System.Security.Cryptography;

namespace Crypt_Lib
{
    public static class HashUtil
    {
        public static string calculateHash(byte[] file)
        {
            
            using (SHA256Managed hasher = new SHA256Managed())
            {
                return hasher.ComputeHash(file).ToString();
            }
        }

        public static SHA256Managed getHasher()
        {
            return new SHA256Managed();
        }
    }
}