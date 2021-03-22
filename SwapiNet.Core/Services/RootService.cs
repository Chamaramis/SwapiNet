using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwapiNet.Core.Models;

namespace SwapiNet.Core.Services
{
    public class RootService
    {
        public async Task<Root> GetRootResponse()
        {
            var response = await BasicHttpClientService.Instance.Get<Root>(string.Empty);
            return response;
        }
    }
}
