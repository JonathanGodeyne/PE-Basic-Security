using System.IO;
using System.Text;
namespace Crypt_Lib
{
    public class FileUtil
    {
        private readonly string currentDirectory = string.Empty;
        private readonly string keyStorageDirectory = string.Empty;

        public FileUtil()
        {
            currentDirectory = Directory.GetCurrentDirectory();
            keyStorageDirectory = Path.Combine(currentDirectory, "Key_Storage");
            DirectorySetup();

        }

        private bool checkFileExistence(string pathName)
        {
            return File.Exists(pathName);
        }

        private bool checkDirectoryExistence(string pathName)
        {
            return Directory.Exists(pathName);
        }

        private void DirectorySetup()
        {
            if (!checkDirectoryExistence(keyStorageDirectory))
                Directory.CreateDirectory(keyStorageDirectory);
        }


        public string getKeyFromPerson(string personName)
        {
            var personDirectoryPath = Path.Combine(keyStorageDirectory, "Keys_"+personName);
            var keyFilePath = Path.Combine(personDirectoryPath, personName+"_rsa");

            UnicodeEncoding encoding = new UnicodeEncoding();
            return encoding.GetString(File.ReadAllBytes(keyFilePath));
           

        }

        public void WriteKeyToFile(string fileName, byte[] key, string personName)
        {
            var personDirectoryPath = Path.Combine(keyStorageDirectory, "Keys_"+personName);
            var filePath = Path.Combine(personDirectoryPath, fileName);
            if (!checkDirectoryExistence(personDirectoryPath))
            {
                Directory.CreateDirectory(personDirectoryPath);
                if (!checkFileExistence(filePath))
                    using (var writer = File.Create(filePath))
                    {
                        writer.Write(key);
                    }

            }
        }

        public void WriteBytesToFile(string fileName, byte[] data)
        {
            var filePath = Path.Combine(keyStorageDirectory, fileName);
            if (!checkFileExistence(filePath))
                using (var writer = File.Create(filePath))
                {
                    writer.Write(data);
                }
        }

        //public string GetByteFromFile(string )



        public string GetCurrentDirectory()
        {
            return currentDirectory;
        }


        public string GetKeyStorageFolder()
        {
            return keyStorageDirectory;
        }
    }
}