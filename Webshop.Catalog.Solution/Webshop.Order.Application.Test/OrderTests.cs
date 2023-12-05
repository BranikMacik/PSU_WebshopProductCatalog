using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Webshop.Data.Persistence;
using Webshop.Domain.AggregateRoots;
using Webshop.Domain.Common;
using Webshop.Order.Application.Contracts.Persistence;
using Webshop.Order.Application.Features.Order.Commands.CreateOrder;
using Webshop.Order.Application.Features.Order.Commands.DeleteOrder;
using Webshop.Order.Application.Features.Order.Queries.GetOrder;
using Xunit;

namespace Webshop.Order.Application.Test
{
    public class OrderTests
    {
        [Fact]
        public void TestConnectionToDatabase()
        {
            string serverName = "LAPTOP-GNG53TOB\\SQLEXPRESS";
            string databaseName = "webshopdatabase";
            string connectionString = $"Server={serverName};Database={databaseName};Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Database connection successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database connection failed. Error: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Test is expected to fail as Customer property is missing
        /// </summary>
        [Fact]
        public void CreateOrderCommand_InValidCustomer_ExpectFailure()
        {
            DateTime dateOfIssue = DateTime.Today;
            DateTime dueDate = DateTime.Today.AddDays(21);
            int discount = 0;
            Dictionary<int, int> orderedProducts = new Dictionary<int, int>()
            {
                {1, 1}
            };
            
            Action a = () => new CreateOrderCommand(0, dateOfIssue, dueDate, discount, orderedProducts);
            Assert.Throws<ArgumentNullException>(a);
        }

        /// <summary>
        /// Test is expected to fail due to the wrong dateOfIssue being passed
        /// </summary>
        [Fact]
        public void CreateOrderCommand_InValidDateOfIssue_ExpectFailure()
        {
            int customerId = 15;
            DateTime dateOfIssue = DateTime.MinValue;
            DateTime dueDate = DateTime.Today.AddDays(21);
            int discount = 0;
            Dictionary<int, int> orderedProducts = new Dictionary<int, int>()
            {
                {1, 1}
            };

            Action a = () => new CreateOrderCommand(customerId, dateOfIssue, dueDate, discount, orderedProducts);
            Assert.Throws<ArgumentException>(a);
        }

        /// <summary>
        /// Test is expected to fail due to the wrong dueDate being passed
        /// </summary>
        [Fact]
        public void CreateOrderCommand_InValidDueDate_ExpectFailure()
        {
            int customerId = 15;
            DateTime dateOfIssue = DateTime.Today;
            DateTime dueDate = DateTime.MinValue;
            int discount = 0;
            Dictionary<int, int> orderedProducts = new Dictionary<int, int>()
            {
                {1, 1}
            };

            Action a = () => new CreateOrderCommand(customerId, dateOfIssue, dueDate, discount, orderedProducts);
            Assert.Throws<ArgumentException>(a);
        }

        /// <summary>
        /// Test is expected to fail due to Discount being too low
        /// </summary>
        [Fact]
        public void CreateOrderCommand_InvalidDiscountLow_ExpectFailure()
        {
            int customerId = 15;
            DateTime dateOfIssue = DateTime.Today;
            DateTime dueDate = DateTime.Today.AddDays(21);
            int discount = -1;
            Dictionary<int, int> orderedProducts = new Dictionary<int, int>()
            {
                {1, 1}
            };

            Action a = () => new CreateOrderCommand(customerId, dateOfIssue, dueDate, discount, orderedProducts);
            Assert.Throws<ArgumentOutOfRangeException>(a);
        }

        /// <summary>
        /// Test is expected to fail due to Discount being too high
        /// </summary>
        [Fact]
        public void CreateOrderCommand_InvalidDiscountHigh_ExpectFailure()
        {
            int customerId = 15;
            DateTime dateOfIssue = DateTime.Today;
            DateTime dueDate = DateTime.Today.AddDays(21);
            int discount = 16;
            Dictionary<int, int> orderedProducts = new Dictionary<int, int>()
            {
                {1, 1}
            };

            Action a = () => new CreateOrderCommand(customerId, dateOfIssue, dueDate, discount, orderedProducts);
            Assert.Throws<ArgumentOutOfRangeException>(a);
        }

        /// <summary>
        /// Test is expected to fail due to no products being ordered
        /// </summary>
        [Fact]
        public void CreateOrderCommand_InvalidDictionaryEmpty_ExpectFailure()
        {
            int customerId = 15;
            DateTime dateOfIssue = DateTime.Today;
            DateTime dueDate = DateTime.Today.AddDays(21);
            int discount = 10;
            Dictionary<int, int> noOrderedProducts = new Dictionary<int, int>();

            Action a = () => new CreateOrderCommand(customerId, dateOfIssue, dueDate, discount, noOrderedProducts);
            Assert.Throws<ArgumentException>(a);
        }

        /// <summary>
        /// Test is expected to fail due to no dictionary being ordered
        /// </summary>
        [Fact]
        public void CreateOrderCommand_InvalidDictionaryNull_ExpectFailure()
        {
            int customerId = 15;
            DateTime dateOfIssue = DateTime.Today;
            DateTime dueDate = DateTime.Today.AddDays(21);
            int discount = 10;

            Action a = () => new CreateOrderCommand(customerId, dateOfIssue, dueDate, discount, null);
            Assert.Throws<ArgumentNullException>(a);
        }

        [Fact]
        public void DeleteOrderCommand_Invalid_ExpectFailure()
        {
            Action a = () => new DeleteOrderCommand(0);
            Assert.Throws<ArgumentOutOfRangeException>(a);
        }

        [Fact]
        public void GetOrderQuery_Invalid_ExpectFailure()
        {
            Action a = () => new GetOrderQuery(0);
            Assert.Throws<ArgumentOutOfRangeException>(a);
        }

        [Fact]
        public void CreateOrderCommand_Valid_ExpectSuccess() 
        {
            int customerId = 15;
            DateTime dateOfIssue = DateTime.Today;
            DateTime dueDate = DateTime.Today.AddDays(21);
            int discount = 0;
            Dictionary<int, int> orderedProducts = new Dictionary<int, int>()
            {
                {1, 1}
            };

            Action a = () => new CreateOrderCommand(customerId, dateOfIssue, dueDate, discount, orderedProducts);
        }

        [Fact]
        public async Task CreateOrderCommandHandler_Invoke_repositorycreate_expect_success()
        {
            //arrange
            var loggerMock = new Mock<Microsoft.Extensions.Logging.ILogger<CreateOrderCommandHandler>>();
            var orderRepositoryMock = new Mock<IOrderRepository>();

            Dictionary<int, int> orderedProducts = new Dictionary<int, int>()
            {
                {1, 1}
            };
            Domain.AggregateRoots.Order order = new Domain.AggregateRoots.Order(15, DateTime.Today, DateTime.Today.AddDays(21), 0, orderedProducts);
            CreateOrderCommand command = new CreateOrderCommand(order.CustomerId, order.DateOfIssue, order.DueDate, order.Discount, order.OrderedProductIdsAndAmounts);

            // Setup the mock repository
            orderRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<Domain.AggregateRoots.Order>())).ReturnsAsync(Result.Ok());
            CreateOrderCommandHandler handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object);
            //act
            Result result = await handler.Handle(command);
            //assert
            orderRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Domain.AggregateRoots.Order>()), Times.Once);
            Assert.True(result.Success);
        }
    }
}
