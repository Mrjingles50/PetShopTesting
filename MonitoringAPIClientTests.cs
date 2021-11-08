
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using PetShopMetrics.Models;
using System;

namespace PetShopTesting 
{
    public class MonitoringAPIClientTests 
    {     
        [Fact]
        public async Task TestGetDistinctCategories()
        {
            using var api = new APIClient();
            IEnumerable<string> result = await api._api.GetDistinctCategories();
            
            //In range of 8 to 9 due to "Test" category being added and removed
            result.Count().Should().BeInRange(8, 9);
        }

        [Fact]
        public async Task TestGetMerchandiseFilter()
        {
            using var api = new APIClient();
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseFilter();
            
            result.Count().Should().BeInRange(0, 500);
        }

        [Fact]
        public async Task TestGetMerchandisByCategory()
        {
            using var api = new APIClient();
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseByCategory("Dog");
            
            result.First().Category.Should().Be("Dog");
            result.Last().Category.Should().Be("Dog");
        }

        [Fact]
        public async Task TestGetMerchandisByMonth()
        {
            using var api = new APIClient();
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseByMonth(10);
            
            result.First().DateAndTime.Month.Should().Be(10);
            result.Last().DateAndTime.Month.Should().Be(10);
        }

        [Fact]
        public async Task TestGetMerchandiseSearchCountByMonthAndCategory()
        {
            using var api = new APIClient();
            int result = await api._api.GetMerchandiseSearchCountByMonthAndCategory(11, "Test");

            result.Should().BeInRange(0, 4);

            result = await api._api.GetMerchandiseSearchCountByMonthAndCategory(10, "Dog");

            result.Should().NotBeInRange(0, 4);
        }

        [Fact]
        public async Task TestGetMerchandiseByValueAndTimeSpan()
        {
            using var api = new APIClient();
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseByValueAndTimeSpan("Test",  new DateTime(2021,11,1) ,DateTime.Now );

            result.First().DateAndTime.Month.Should().Be(11);
            result.Last().DateAndTime.Month.Should().Be(11);
            result.Count().Should().Be(4);
        }
        

    }
}
