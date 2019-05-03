
using Crypt_Lib;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
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
        var hashOption = command.Option("-h|--hash <hash>",
                                "Path of the encrypted message",
                                CommandOptionType.SingleValue);


        //todo maak optie voor encrypted_symetric_key, encrypted_hash


        command.OnExecute(() =>
        {
            if (senderOption.HasValue() && messageOption.HasValue())
                (new DecryptCommand(messageOption.Value(), senderOption.Value())).Run();
            else

                return 0;



        });
    }

    private readonly string _receiverName;
    private readonly string _hashPath;
    private readonly string _keyPath;
    private readonly string _messagePath;
    private readonly string _senderName;

    private readonly RsaUtil _RsaUtil;



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
                rsaB.importKey();
            }
        }

    }
}