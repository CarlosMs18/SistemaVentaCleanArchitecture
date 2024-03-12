using SistemaVenta.Domain.Common;
using SistemaVenta.Domain.Enums;

namespace SistemaVenta.Domain
{
    public class Sale : BaseDomainModel
    {
        public string? DocumentNumber { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal? Total { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
