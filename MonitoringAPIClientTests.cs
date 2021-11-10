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
        public APIClient api = new APIClient();
        
        [Fact]
        public async Task TestGetDistinctCategories()
        {
            IEnumerable<string> result = await api._api.GetDistinctCategories();
            
            //In range of 8 to 9 due to "Test" category being added and removed
            result.Count().Should().BeInRange(8, 9);
        }

        [Fact]
        public async Task TestGetMerchandiseFilter()
        {
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseFilter();
            
            result.Count().Should().BeInRange(0, 500);
        }

        [Fact]
        public async Task TestGetMerchandisByCategory()
        {
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseByCategory("Dog");
            
            result.First().Category.Should().Be("Dog");
            result.Last().Category.Should().Be("Dog");
        }

        [Fact]
        public async Task TestGetMerchandisByMonth()
        {
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseByMonth(10);
            
            result.First().DateAndTime.Month.Should().Be(10);
            result.Last().DateAndTime.Month.Should().Be(10);
        }

        [Fact]
        public async Task TestGetMerchandiseSearchCountByMonthAndCategory()
        {
            int result = await api._api.GetMerchandiseSearchCountByMonthAndCategory(11, "Test");

            result.Should().BeInRange(0, 4);

            result = await api._api.GetMerchandiseSearchCountByMonthAndCategory(10, "Dog");

            result.Should().NotBeInRange(0, 4);
        }

        [Fact]
        public async Task TestGetMerchandiseByValueAndTimeSpan()
        {
            IEnumerable<MerchandiseFilter> result = await api._api.GetMerchandiseByValueAndTimeSpan("Test",  new DateTime(2021,11,1) ,DateTime.Now );

            result.First().DateAndTime.Month.Should().Be(11);
            result.Last().DateAndTime.Month.Should().Be(11);
            result.Count().Should().Be(4);
        }

        [Fact]
        public async Task TestGetDistinctPetFilterCriteria() 
        {
            IEnumerable<string> result = await api._api.GetDistinctPetFilterCriteria();
            result.Count().Should().Be(4);
        }

        [Fact]
        public async Task TestGetDistinctPetFilterValues()
        {
            IEnumerable<string> result = await api._api.GetDistinctPetFilterValues();
            result.First().Should().Be("Bird");
            result.Last().Should().Be("1");
        }

        [Fact]
        public async Task TestGetPetFilter()
        {
            IEnumerable<PetFilter> result = await api._api.GetPetFilter();
            result.First().Should().BeOfType<PetFilter>();
        }

        [Fact]
        public async Task TestGetFilterByValue()
        {
            IEnumerable<PetFilter> result = await api._api.GetPetFilterByValue("Male");
            result.First().Value.Should().Be("Male");
            result.Last().Value.Should().Be("Male");
        }

        [Fact]
        public async Task TestGetFilterByCriteria()
        {
            IEnumerable<PetFilter> result = await api._api.GetPetFilterByFilterCriteria("Gender");
            result.First().FilterCriteria.Should().Be("Gender");
            result.Last().FilterCriteria.Should().Be("Gender");
        }

        [Fact]
        public async Task TestGetPetFilterByMonth() 
        {
            IEnumerable<PetFilter> result = await api._api.GetPetFilterByMonth(10);
            result.First().DateAndTime.Month.Should().Be(10);
            result.Last().DateAndTime.Month.Should().Be(10);
        }

        [Fact]
        public async Task TestGetPetFilterSearchCountByMonthAndCriteria() 
        {
            int result = await api._api.GetPetFilterSearchCountByMonthAndCriteria(11, "Age");
            result.Should().Be(1);
        }

        [Fact]
        public async Task TestGetPetFilterSearchCountByMonthAndValue()
        {
            int result = await api._api.GetPetFilterSearchCountByMonthAndValue(11, "1");
            result.Should().Be(1);
        }

        [Fact]
        public async Task TestGetPetFilterByCriteriaAndTimeSpan() 
        {
            IEnumerable<PetFilter> result = await api._api.GetPetFilterByCriteriaAndTimeSpan("Gender", new DateTime(2021,10,1), new DateTime(2021, 10, 31));
            result.First().DateAndTime.Month.Should().Be(10);
            result.Last().DateAndTime.Month.Should().Be(10);
            result.First().FilterCriteria.Should().Be("Gender");
            result.Last().FilterCriteria.Should().Be("Gender");
        }

        [Fact]
        public async Task TestGetPetFilterByValueAndTimeSpan()
        {
            IEnumerable<PetFilter> result = await api._api.GetPetFilterByValueAndTimeSpan("Dog", new DateTime(2021, 10, 1), new DateTime(2021, 10, 31));
            result.First().DateAndTime.Month.Should().Be(10);
            result.Last().DateAndTime.Month.Should().Be(10);
            result.First().Value.Should().Be("Dog");
            result.Last().Value.Should().Be("Dog");
            result.Last().FilterCriteria.Should().Be("Category");
        }
    }
}
