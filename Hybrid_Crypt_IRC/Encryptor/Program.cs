using System;
using Crypt_Lib;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Hybrid_Crypt_Basis_Console.Commands;

namespace Hybrid_Crypt_Basis_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var encryptionApp = new CommandLineApplication();
            RootCommand.Configure(encryptionApp);
            encryptionApp.Execute(args);
        }
    }
}