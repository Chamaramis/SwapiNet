using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwapiNet.Core.Models;

namespace SwapiNet.Core.Services
{
    public class ConfigurationService
    {
        private static ConfigurationService _instance;
        private const string defaultSwapiUrl = "https://swapi.dev/api/";
        public string BaseSwapiUrl;

        private ConfigurationService(string baseSwapiUrl)
        {
            BaseSwapiUrl = baseSwapiUrl;
        }

        public static ConfigurationService Instance
        {
            get => _instance;
            private set => _instance = value;
        }

        public Root Root { get; private set; }

        public static async Task InitializeAsync(string baseSwapiUrl = defaultSwapiUrl)
        {
            Instance = new ConfigurationService(baseSwapiUrl);
            Instance.Root = await new RootService().GetRootResponse();
        }
    }
}
