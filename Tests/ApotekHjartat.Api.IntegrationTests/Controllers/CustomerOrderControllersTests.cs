
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
using System.Collections.Generic;
using ApotekHjartat.DbAccess.Enums;
using ApotekHjartat.Api.Enums;

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
        public async Task GetCustomerOrderById_OrderDoesNotExist_ReturnNotFound()
        {
            // arrange
            var customerOrderId = 999;
            // act
            var response = await _client.GetAsync($"{BaseUrl}{customerOrderId}");
            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetCustomerOrderById_ContainsRx_ReturnObjectWithPrescriptionBag()
        {
            // arrange
            var rxOrder = new CustomerOrder()
            {
                Created = DateTime.Now,
                CustomerOrderRows = new List<CustomerOrderRow>() {
                    new CustomerOrderRow() { OrderRowType = CustomerOrderRowType.Prescription, OrderedAmount = 1, PriceExclVat = 50M  },
                    new CustomerOrderRow() {OrderRowType = CustomerOrderRowType.Prescription, OrderedAmount = 2, PriceExclVat = 69M}
                    }
                };
            // act

            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.CustomerOrder.Add(rxOrder);
            dbContext.SaveChanges();
            var response = await _client.GetAsync($"{BaseUrl}{rxOrder.CustomerOrderId}");
            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedOrder = response.SerializeResponseContent<CustomerOrderDto>();
            Assert.Equal(188M, returnedOrder.CustomerOrderRows.Select(x => x.PriceExclVat).FirstOrDefault());
            Assert.Equal("Prescription Bag", returnedOrder.CustomerOrderRows.FirstOrDefault().ProductName);
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


        [Fact]
        public async Task CancelCustomerOrderById_Success_ReturnOrderWithStatusCancelled()
        {
            // arrange
            var customerOrder = new CustomerOrder()
            {
                Created = DateTime.Now,
                OrderStatus = CustomerOrderStatus.NotYetProccessed
            };

            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.CustomerOrder.Add(customerOrder);
            dbContext.SaveChanges();
            // act
            var response = await _client.PutAsync($"{BaseUrl}cancel/{customerOrder.CustomerOrderId}", HttpExtensions.EmptyBody);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedOrder = response.SerializeResponseContent<CustomerOrderDto>();
            Assert.Equal(CustomerOrderStatusDto.Cancelled, returnedOrder.OrderStatus);

        }

        [Fact]
        public async Task CancelCustomerOrderById_OrderStatusIsPacking_ReturnNotAllowed()
        {
            // arrange
            var customerOrder = new CustomerOrder()
            {
                Created = DateTime.Now,
                OrderStatus = CustomerOrderStatus.Packing
            };

            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.CustomerOrder.Add(customerOrder);
            dbContext.SaveChanges();
            // act
            var response = await _client.PutAsync($"{BaseUrl}cancel/{customerOrder.CustomerOrderId}", HttpExtensions.EmptyBody);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }


        [Fact]
        public async Task DeleteCustomerOrderCustomerDataById_InactiveOrder_ReturnOrderWithNoCustomerData()
        {
            // arrange
            var customerOrder = new CustomerOrder()
            {
                Created = DateTime.Now,
                CustomerAddress = "Dathomir Street 11",
                CustomerEmailAddress = "darthmaul@gmail.com",
                CustomerFirstName = "Darth",
                CustomerSurname = "Maul",
                OrderStatus = CustomerOrderStatus.Archived
            };

            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.CustomerOrder.Add(customerOrder);
            dbContext.SaveChanges();
            // act
            var response = await _client.PutAsync($"{BaseUrl}deletecustomerdata/{customerOrder.CustomerOrderId}", HttpExtensions.EmptyBody);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedOrder = response.SerializeResponseContent<CustomerOrderDto>();
            Assert.Null(returnedOrder.CustomerAddress);
            Assert.Null(returnedOrder.CustomerEmailAddress);
            Assert.Null(returnedOrder.CustomerFirstName);
            Assert.Null(returnedOrder.CustomerSurname);

        }

        [Fact]
        public async Task DeleteCustomerOrderCustomerDataById_ActiveOrder_ReturnBadRequest()
        {
            // arrange
            var customerOrder = new CustomerOrder()
            {
                Created = DateTime.Now,
                CustomerAddress = "Kashyyyk Street 1",
                CustomerEmailAddress = "chewbacca@gmail.com",
                CustomerFirstName = "Chewbacca",
                CustomerSurname = "von Kashyyyk",
                OrderStatus = CustomerOrderStatus.Picking
            };

            var dbContext = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
            dbContext.CustomerOrder.Add(customerOrder);
            dbContext.SaveChanges();
            // act
            var response = await _client.PutAsync($"{BaseUrl}deletecustomerdata/{customerOrder.CustomerOrderId}", HttpExtensions.EmptyBody);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}