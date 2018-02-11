using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Dockka.Config
{
    public class Configuration
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

        public static string DockkaSqlConnection => Environment.GetEnvironmentVariable("SQL_CONN") ??
                                                    Instance.Settings.GetConnectionString("DockkaSqlConnection");

        public IConfigurationRoot Settings { get; }
    }
}