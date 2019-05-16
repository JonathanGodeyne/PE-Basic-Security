using System;
using Crypt_Lib;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
public class EncryptCommand : ICommand
{
    public static void Configure(CommandLineApplication command)
    {
        command.Description = "Encrypts a message for a given persons name";
        command.HelpOption("-?|-h|--help");

        var senderArgument = command.Argument("[sender]", "Name of the sender of the message.");

        var receiverOption = command.Option("-r|--receiver <receiver>",
                                "name of the receiver of the message",
                                CommandOptionType.SingleValue);
        command.OnExecute(() =>
        {
            (new EncryptCommand(senderArgument.Value, receiverOption.Value())).Run();
        });

    }

    private readonly string _senderName;
    private readonly string _receiverName;

    public EncryptCommand(string senderName, string receiverName)
    {
        _senderName = senderName;
        _receiverName = receiverName;
    }

    public void Run()
    {
        var sender = _senderName != null
            ? _senderName
            : "Alice";
        var receiver = _receiverName != null
            ? _senderName
            : "Bob";

        encryt(sender, receiver);


    }

    private void encryt(string sender, string receiver)
    {
        string input = string.Empty;
        FileUtil fileUtil = new FileUtil();
        using (AesUtil aesUtil = new AesUtil())
        {
            using (RsaUtil rsaA = new RsaUtil(),
                          rsaB = new RsaUtil())
            {
                rsaA.writekeyToFile(_senderName);
                rsaB.writekeyToFile(_receiverName);
                Console.WriteLine("Geef de tekst die geëncrypteerd moet worden: ");
                Console.Write("--> ");

                input = Console.ReadLine();

                // encrypteer de input met de symetrische key
                fileUtil.WriteBytesToFile("encrypted_input", aesUtil.EncryptStringToBytes_Aes(input));

                // Programma encrypteert de symmetric key met de public key van Bob
                fileUtil.WriteBytesToFile("encrypted_symetric_key", rsaB.RsaEncrypt(aesUtil.getKey()));

                var encoding = new UnicodeEncoding();
                var hash = rsaA.RsaSign(encoding.GetBytes(input));


                fileUtil.WriteBytesToFile("encrypted_hash", rsaA.RsaEncrypt(hash));
            }
        }


    }



}