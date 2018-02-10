using System.IO;
using Microsoft.Extensions.Configuration;

namespace Dockka.ActorOne
{
    internal class Configuration
    {
        private static readonly object padlock = new object();
        private static Configuration _instance;
        private Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Settings = builder.Build();
        }

        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                            _instance = new Configuration();
                    }
                }

                return _instance;
            }
        }

        public IConfigurationRoot Settings { get; }
    }
}