using Microsoft.AspNetCore.Hosting;
using PetShopMetrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetShopTesting
{
    public class APIClient 
    {
        public MonitoringAPIClient _api { get; private set; }
        private HttpClient client;
        Uri path = new Uri("https://api20211105132600.azurewebsites.net/api/");

        public APIClient()
        {
            client = new();
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();
            client.BaseAddress = path;
            _api = new MonitoringAPIClient(client);
        }
    }
}
