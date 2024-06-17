using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Services;
using Loja.Domain.Interfaces.Repositories;

namespace Loja.Domain.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBaseService<OrderItem> _orderItemService;

        public OrderService(
            IOrderRepository orderRepository,
            IBaseService<OrderItem> orderItemService) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _orderItemService = orderItemService;
        }

        public override async Task<Order> Get(int id) =>
            await _orderRepository.Read(id);

        public override async Task<Order> Add(Order model)
        {
            var entity = await _orderRepository.Create(model);

            foreach (var item in entity.OrderItems)
            {
                item.OrderId = entity.Id;
                await _orderItemService.Add(item);
            }

            return entity;
        }

        public override async Task Edit(Order model)
        {
            var entity = await Get(model.Id);

            entity.ClientName = model.ClientName;

            var deletedItems = entity.OrderItems
                .ExceptBy(model.OrderItems.Select(x => x.Id), x => x.Id);

            foreach (var item in model.OrderItems)
            {
                if (item.Id == default)
                    await _orderItemService.Add(item);
                else
                {
                    var orderItem = await _orderItemService.Get(item.Id);

                    orderItem.Product = item.Product;
                    orderItem.Amount = item.Amount;
                    orderItem.Price = item.Price;

                    await _orderItemService.Edit(orderItem);
                }
            }

            foreach (var item in deletedItems)
                await _orderItemService.Delete(item.Id);
        }
    }
}
