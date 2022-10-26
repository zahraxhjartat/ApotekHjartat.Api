
using Xunit;
using ApotekHjartat.Api.Integration.Test.AppStartup;
using ApotekHjartat.Api.IntegrationTest.Stubs;
using ApotekHjartat.Api.IntegrationTest.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using ApotekHjartat.DbAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using ApotekHjartat.DbAccess.Context;
using System;
using System.Net;
using ApotekHjartat.Api.Models;
using ApotekHjartat.Api.Models.v1;
using System.Threading.Tasks;
using System.Net.Http;
using ApotekHjartat.Api.IntegrationTest.Extentions;
using System.Linq;

namespace ApotekHjartat.Api.Integration.Test.Controllers
{
    public class CustomerOrderControllerTests :
            IClassFixture<CustomWebApplicationFactory<StartupStub>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<StartupStub> _factory;
        private string BaseUrl => "api/v1/customerorder/";
        public CustomerOrderControllerTests(CustomWebApplicationFactory<StartupStub> factory)
        {
            _factory = factory.InitializeWithHostFromCustomFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetCustomerOrdersByFilter_FilterByDate_ReturnOnlyMatchingOrder()
        {
            // arrange
            var match = new CustomerOrder()
            {
               Created = DateTime.Now
            };
            var notMatch = new CustomerOrder()
            {
             Created = DateTime.Now.AddDays(100)
            };
            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.CustomerOrder.Add(match);
            dbContext.CustomerOrder.Add(notMatch);
            dbContext.SaveChanges();
            var filter = new CustomerOrderFilter()
            {
                FromDate = DateTime.Now.AddDays(-10),
                ToDate = DateTime.Now.AddDays(10),
                Skip = 0,
                Take = 500
            };
            // act
            var response = await _client.PostAsync($"{BaseUrl}filter/", filter.ToStringContent());
            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var filterResult = response.SerializeResponseContent<PaginatedResponseDto<CustomerOrderDto>>();
            Assert.Contains(match.CustomerOrderId, filterResult.Page.Select(x => x.CustomerOrderId));
            Assert.DoesNotContain(notMatch.CustomerOrderId, filterResult.Page.Select(x => x.CustomerOrderId));
        }

    }
}