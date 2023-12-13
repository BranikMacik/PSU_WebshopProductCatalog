using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QueueServices.Contracts;
using QueueServices.Features.Dtos;
using QueueServices.Features.MessagingServices;
using Webshop.Application.Contracts;
using Webshop.Domain.AggregateRoots;
using Webshop.Domain.Common;
using Webshop.Order.Api.Controllers;
using Webshop.Order.Application.Features.Order.Requests;

namespace Webshop.Order.Api.Tests
{
    public class OrderApiTests
    {
        [Fact]
        public async Task CreateOrder_withProperInput_shouldWorkProperly()
        {
            // Arrange
            var mockDispatcher = new Mock<IDispatcher>();
            mockDispatcher
                .Setup(m => m.Dispatch(It.IsAny<ICommand>()))
                .ReturnsAsync(Result.Ok());

            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<OrderController>>();
            var mockOrderPublisher = new Mock<OrderPublisher<OrderDataTransferObject>>();

            var orderController = new OrderController(mockDispatcher.Object, mockMapper.Object, mockLogger.Object, mockOrderPublisher.Object);

            var orderedProducts = new Dictionary<int, int>()
            {
                {5, 1}
            };

            // Act
            var actionResult = await orderController.CreateOrder(new CreateOrderRequest
            {
                CustomerId = 5,
                DateOfIssue = DateTime.Today,
                DueDate = DateTime.Today.AddDays(21),
                Discount = 0,
                OrderedProductIdsAndAmounts = orderedProducts
            });

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}