using McMaster.Extensions.CommandLineUtils;

namespace Hybrid_Crypt_Basis_Console.Commands
{
    public class RootCommand : ICommand
    {
        public static void Configure(CommandLineApplication app)
        {
            app.Name = "encryptor";
            app.HelpOption("-?|-h|--help");

            // Register commands
            app.Command("encrypt", EncryptCommand.Configure);
            app.Command("decrypt", DecryptCommand.Configure);

            app.OnExecute(() =>
            {
                (new RootCommand(app)).Run();
                return 0;
            });
        }

        private readonly CommandLineApplication _app;

        public RootCommand(CommandLineApplication app)
        {
            _app = app;
        }

        public void Run()
        {
            _app.ShowHelp();
        }

    }
}