using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
//TODO implementeer singnalIR voor chat
//TODO maak modellen voor channels/onderwerpen. Elk chanel heeft een uniek paswoord
//TODO Database houdt geincrypteerde versie van gesprek bij
//TODO private keys worden lokaal bij de client bijgehouden en kunnen worden ingeladen
//Database houdt de public keys van de gebruikers bij
//


namespace Hybrid_Crypt_IRC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}