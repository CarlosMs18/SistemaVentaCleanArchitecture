using SistemaVenta.Domain.Common;

namespace SistemaVenta.Domain
{
    public class Category : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}
