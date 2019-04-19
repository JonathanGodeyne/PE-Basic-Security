using System;
using Crypt_Lib;
using System.Text;


namespace Hybrid_Crypt_Basis_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string optie = string.Empty;    
            Console.WriteLine("Encrypteren: 0");
            Console.WriteLine("Decrypteren: 1");

            while (!optie.Equals("0") )
            {
                Console.Write("Kies hier een optie: ");
                optie = Console.ReadLine();    
            }


            if (optie == "0")
                encrytie();
            else
                decryptie();






        }

        private static void decryptie()
        {
            
            

            
            
        }

        private static void encrytie()
        {
            string input = string.Empty;
            FileUtil fileUtil = new FileUtil();
            using (AesUtil aesUtil = new AesUtil())
            {
                using(RsaUtil rsaAlice = new RsaUtil(),
                              rsaBob = new RsaUtil())
                {
                    rsaAlice.writekeyToFile("Alice");
                    rsaBob.writekeyToFile("Bob");
                    Console.WriteLine("Geef de tekst die geencryteerd moet worden: ");
                    Console.Write("--> ");

                    input = Console.ReadLine();

                    // encrypteer de input met de symetrische key
                    fileUtil.WriteBytesToFile("File_1", aesUtil.EncryptStringToBytes_Aes(input));

                    // Programma encrypteert de symmetric key met de public key van Bob
                    fileUtil.WriteBytesToFile("File_2", rsaBob.RsaEncrypt(aesUtil.getKey()));

                    var encoding = new UnicodeEncoding();

                    fileUtil.WriteBytesToFile("File_3", rsaAlice.RsaSign(encoding.GetBytes(input)));

                    

                    




                }
            }

            
        }
    }
}