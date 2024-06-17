namespace Loja.Domain.Entities
{
    public class Order : Entity
    {
        /// <summary>
        /// Nome do cliente que fez o pedido.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Data do pedido.
        /// </summary>
        public DateTime OrderedAt { get; set; }

        /// <summary>
        /// Valor total do pedido.
        /// </summary>
        public decimal Total { get; set; }

        #region Navigation

        public ICollection<OrderItem> OrderItems { get; set; } = [];

        #endregion
    }
}
