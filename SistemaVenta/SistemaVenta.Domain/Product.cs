using SistemaVenta.Domain.Common;

namespace SistemaVenta.Domain
{
    public class Product: BaseDomainModel 
    {
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public bool? Active { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
