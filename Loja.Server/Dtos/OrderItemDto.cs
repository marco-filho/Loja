namespace Loja.Server.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
    }
}
