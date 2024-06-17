using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Repositories;
using Loja.Infra.Data.Repositories;

namespace Loja.Test.Unit.Repositories
{
    public class OrderRepositoryTests : IClassFixture<TestsFixture>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRepositoryTests(TestsFixture fixture)
        {
            _orderRepository = new OrderRepository(fixture.DbContext);
        }

        [Fact]
        public async void Read_Should_IncludeOrderItems()
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

            order = await _orderRepository.Create(order);

            // Act
            await _orderRepository.Read(order.Id);

            // Assert
            Assert.NotNull(order);
            Assert.NotEqual(0, order.Id);
            Assert.NotEmpty(order.OrderItems);
            Assert.Equal(2, order.OrderItems.Count);

            foreach (var item in order.OrderItems)
            {
                Assert.NotEmpty(item.Product);
                Assert.NotEqual(0, item.Amount);
                Assert.NotEqual(0, item.Price);
            }
        }
    }
}
