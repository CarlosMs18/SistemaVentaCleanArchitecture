using SistemaVenta.Domain.Common;

namespace SistemaVenta.Domain
{
    public class SaleDetail : BaseDomainModel
    {
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public int ProductId { get; set; }
        public Product Product  { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
