using Moq;
using Loja.Domain.Services;
using Loja.Domain.Interfaces.Services;
using Loja.Domain.Interfaces.Repositories;
using Loja.Domain.Entities;

namespace Loja.Test.Unit.Services
{
    public class OrderServiceTests : IClassFixture<TestsFixture>
    {
        private readonly IOrderService _orderService;

        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IBaseService<OrderItem>> _orderItemService;

        public OrderServiceTests()
        {
            _orderRepository = new();
            _orderItemService = new();

            _orderService = new OrderService(_orderRepository.Object, _orderItemService.Object);
        }

        [Fact]
        public async void Add_Should_AttemptToPersistOrderItems()
        {
            // Arrange
            Order order = new()
            {
                ClientName = "Test Client",
                OrderedAt = DateTime.UtcNow,
                OrderItems = [
                    new OrderItem() { Product = "Test Product", Amount = 2, Price = 10.35M },
                    new OrderItem() { Product = "Test Product 2", Amount = 1, Price = 20.67M }
                ]
            };

            _orderRepository.Setup(x => x.Create(It.IsAny<Order>())).ReturnsAsync(order);
            _orderItemService.Setup(x => x.Add(It.IsAny<OrderItem>()));

            // Act
            await _orderService.Add(order);

            // Assert
            _orderRepository.Verify(x => x.Create(It.IsAny<Order>()), Times.Once());
            _orderItemService.Verify(x => x.Add(It.IsAny<OrderItem>()), Times.Exactly(2));
        }

        [Fact]
        public async void Edit_Should_UpdateOnlyOrder()
        {
            // Arrange
            Order updatedOrder = new()
            {
                Id = 1,
                ClientName = "Test Client Updated",
                OrderedAt = DateTime.UtcNow,
                OrderItems = [
                    new OrderItem() { Id = 1, Product = "Test Product", Amount = 2, Price = 10.35M },
                    new OrderItem() { Id = 2, Product = "Test Product 2", Amount = 1, Price = 20.67M }
                ]
            };

            Order olderOrder = new()
            {
                Id = 1,
                ClientName = "Test Client",
                OrderedAt = DateTime.UtcNow.AddDays(-1),
                OrderItems = [
                    new OrderItem() { Id = 1, Product = "Test Product", Amount = 2, Price = 10.35M },
                    new OrderItem() { Id = 2, Product = "Test Product 2", Amount = 1, Price = 20.67M }
                ]
            };

            _orderRepository.Setup(x => x.Read(1)).ReturnsAsync(olderOrder);
            _orderItemService.Setup(x => x.Get(1)).ReturnsAsync(olderOrder.OrderItems.ElementAt(0));
            _orderItemService.Setup(x => x.Get(2)).ReturnsAsync(olderOrder.OrderItems.ElementAt(1));

            // Act
            await _orderService.Edit(updatedOrder);

            // Assert
            _orderRepository.Verify(x => x.Read(1), Times.Once());
            _orderItemService.Verify(x => x.Add(It.IsAny<OrderItem>()), Times.Never());
            _orderItemService.Verify(x => x.Get(It.IsAny<int>()), Times.Exactly(2));
            Assert.Equal(olderOrder.ClientName, updatedOrder.ClientName);
            Assert.Equivalent(olderOrder.OrderItems, updatedOrder.OrderItems);
            _orderItemService.Verify(x => x.Delete(It.IsAny<int>()), Times.Never());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void Edit_Should_UpdateOrderItems(bool delete)
        {
            // Arrange
            Order updatedOrder = new()
            {
                Id = 1,
                ClientName = "Test Client"
            };

            if (delete)
            {
                updatedOrder.OrderItems = [
                    new OrderItem() { Id = 1, Product = "Test Product", Amount = 2, Price = 10.35M },
                ];
            }
            else
            {
                updatedOrder.OrderItems = [
                    new OrderItem() { Id = 1, Product = "Test Product", Amount = 3, Price = 10.35M },
                    new OrderItem() { Id = 2, Product = "Test Product 2", Amount = 1, Price = 20.67M },
                    new OrderItem() { Product = "Test Product 3", Amount = 3, Price = 1.23M }
                ];
            }

            Order olderOrder = new()
            {
                Id = 1,
                ClientName = "Test Client",
                OrderedAt = DateTime.UtcNow.AddDays(-1),
                OrderItems = [
                    new OrderItem() { Id = 1, Product = "Test Product", Amount = 2, Price = 10.35M },
                    new OrderItem() { Id = 2, Product = "Test Product 2", Amount = 1, Price = 20.67M }
                ]
            };

            _orderRepository.Setup(x => x.Read(1)).ReturnsAsync(olderOrder);
            _orderItemService.Setup(x => x.Get(1)).ReturnsAsync(olderOrder.OrderItems.ElementAt(0));
            _orderItemService.Setup(x => x.Get(2)).ReturnsAsync(olderOrder.OrderItems.ElementAt(1));

            // Act
            await _orderService.Edit(updatedOrder);

            // Assert
            _orderRepository.Verify(x => x.Read(1), Times.Once());

            if (delete)
            {
                _orderItemService.Verify(x => x.Add(It.IsAny<OrderItem>()), Times.Never());
                _orderItemService.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
                _orderItemService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
            }
            else
            {
                _orderItemService.Verify(x => x.Add(It.IsAny<OrderItem>()), Times.Once());
                _orderItemService.Verify(x => x.Get(It.IsAny<int>()), Times.Exactly(2));
                _orderItemService.Verify(x => x.Delete(It.IsAny<int>()), Times.Never());
            }
        }
    }
}
