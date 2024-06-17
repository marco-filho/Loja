namespace Loja.Domain.Entities
{
    public class OrderItem : Entity
    {
        /// <summary>
        /// Identificador do pedido.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Quantidade do produto.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Preço do produto.
        /// </summary>
        public decimal Price { get; set; }

        #region Navigation

        public Order Order { get; set; }

        #endregion
    }
}
