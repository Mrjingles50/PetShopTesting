using Microsoft.AspNetCore.Hosting;
using PetShopMetrics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using System;

namespace PetShopTesting 
{
    public class MonitoringAPIClientTests 
    {
        public static MonitoringAPIClient _api;
        public static HttpClient client = new HttpClient();
        Uri path = new Uri("https://api20211105132600.azurewebsites.net/api/");
        public MonitoringAPIClientTests()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();
            client.BaseAddress = path;
            _api = new MonitoringAPIClient(client);
        }

        [Fact]
        public async Task TestGetDistinctCategories()
        {
            IEnumerable<string> result = await _api.GetDistinctCategories();
            Assert.Equal(7, result.Count());
        }
    }
}
