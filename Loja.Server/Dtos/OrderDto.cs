namespace Loja.Server.Dtos
{
    public class OrderDto
    {
        public int? Id { get; set; }

        public string ClientName { get; set; }

        public DateTime? OrderedAt { get; set; }

        public decimal? Total { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }
    }
}
