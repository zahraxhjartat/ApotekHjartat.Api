using ApotekHjartat.Order.Api.Services;
using ApotekHjartat.Order.DbAccess.DataAccess.v1;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ApotekHjartat.Order.Api.Models;
using ApotekHjartat.Order.DbAccess.Models;

namespace ApotekHjartat.Order.Api.UnitTest.Services
{
    public class CustomerOrderServiceTests
    {
        new Mock<ICustomerOrderDataAccess> CustomerOrderDataAccess;
        CustomerOrderService CustomerOrderService => new CustomerOrderService(
            CustomerOrderDataAccess.Object
            );

        public CustomerOrderServiceTests()
        {
            CustomerOrderDataAccess = new Mock<ICustomerOrderDataAccess>();
        }

        [Fact]
        public async Task GetCustomerOrderById()
        {
            var order1 = new CustomerOrder() { CustomerOrderId = 1, OrderNumber = "100" };
            CustomerOrderDataAccess.Setup(x => x.GetCustomerOrderById(It.Is<int>(x => x == 1))).Returns(Task.FromResult(order1));

            var returnedOrder = await CustomerOrderService.GetCustomerOrderById(1);
            Assert.Equal(order1.CustomerOrderId, returnedOrder.CustomerOrderId);
        }
    }
}
