using System.IO;
using Microsoft.Extensions.Configuration;

namespace RestFS.Console.Config
{
    public class Config
    {
        public Config(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            ReadConfig(builder.Build());
        }

        public string LoggerName    { get; private set; }
        public string RootDirectory { get; private set; }
        public string Uri           { get; private set; }

        private void ReadConfig(IConfiguration configuration)
        {
            LoggerName    = configuration["LoggerName"];
            RootDirectory = configuration["RootDirectory"];
            Uri           = configuration["Uri"];
        }
    }
}