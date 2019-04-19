using System.IO;

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
        }

        private bool checkForFileExistence(string pathName)
        {
            return File.Exists(pathName);
        }

        public void DirectorySetup()
        {
            if (!Directory.Exists(keyStorageDirectory))
                Directory.CreateDirectory(keyStorageDirectory);
        }

        

        public void WriteBytesToFile(string fileName, byte[] key)
        {
            var filePath = Path.Combine(keyStorageDirectory, fileName);
            if (!checkForFileExistence(filePath))
                using (var writer = File.Create(filePath))
                {
                    writer.Write(key);
                }
        }

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