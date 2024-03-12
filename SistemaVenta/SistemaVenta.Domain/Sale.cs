using SistemaVenta.Domain.Common;

namespace SistemaVenta.Domain
{
    public class Sale : BaseDomainModel
    {
        public string? DocumentNumber { get; set; }
        public string? PaymentType { get; set; }
        public decimal? Total { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
