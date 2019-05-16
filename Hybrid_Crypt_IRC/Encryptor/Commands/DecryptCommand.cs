using System.Text;
using Crypt_Lib;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System;



public class DecryptCommand : ICommand
{
    public static void Configure(CommandLineApplication command)
    {
        command.Description = "Decrypts a message for a given persons name";
        command.HelpOption("-?|-h|--help");

        var receiverArgument = command.Argument("[person]", "The name of the person for wich the message is intended")
                                .IsRequired();

        var senderOption = command.Option("-s|--sender <sender>",
                                "name of the sender of the message",
                                CommandOptionType.SingleValue);
        var messageOption = command.Option("-m|--message <message>",
                                "Path of the encrypted message",
                                CommandOptionType.SingleValue);
        var keyOption = command.Option("-k|--key <key>",
                                "Path of the encrypted message",
                                CommandOptionType.SingleValue);
        var hashOption = command.Option("--hash <hash>",
                                "Path of the encrypted message",
                                CommandOptionType.SingleValue);


        //todo maak optie voor encrypted_symetric_key, encrypted_hash


        command.OnExecute(() =>
        {
            (new DecryptCommand(receiverArgument.Value, senderOption.Value(), messageOption.Value(),keyOption.Value(),hashOption.Value() ) ).Run();
        });
    }

    private readonly string _receiverName;
    private readonly string _hashPath;
    private readonly string _keyPath;
    private readonly string _messagePath;
    private readonly string _senderName;





    public DecryptCommand(string receiverName, string senderName, string messagePath, string keyPath, string hashPath)
    {
        _receiverName = receiverName;
        _messagePath = messagePath;
        _senderName = senderName;
        _keyPath = keyPath;
        _hashPath = hashPath;
    }
    public void Run()
    {
        Decrypt(_receiverName, _senderName, _messagePath, _keyPath, _hashPath);
    }

    private void Decrypt(string receiverName, string senderName,string messagePath, string keyPath, string hashPath)
    {

        FileUtil fileUtil = new FileUtil();
        using (AesUtil aesUtil = new AesUtil())
        {
            using (RsaUtil rsaA = new RsaUtil(),
                          rsaB = new RsaUtil())
            {
                
                rsaA.importKey(senderName);
                rsaB.importKey(receiverName);
                UnicodeEncoding encoding = new UnicodeEncoding();
                

                var decryptedSymKey= rsaB.RsaDecrypt(File.ReadAllBytes(keyPath));
                aesUtil.setKey(decryptedSymKey);
                var decryptedMessage = aesUtil.DecryptStringFromBytes_Aes(messagePath);
                Console.WriteLine("Het originele bericht was");
                Console.WriteLine(decryptedMessage);

                var sign = rsaA.RsaVerify(encoding.GetBytes(decryptedMessage), File.ReadAllBytes(hashPath));
                Console.WriteLine("Zijn de hashes hetzelfde?: " + sign);
            }
        }
        


    }
}