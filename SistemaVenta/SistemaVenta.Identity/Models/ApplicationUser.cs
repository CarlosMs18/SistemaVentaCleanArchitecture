using Microsoft.AspNetCore.Identity;

namespace SistemaVenta.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public string? Avatar_large { get; set; } = string.Empty;
        public int? CountrieId { get; set; }
        public int? CityId { get; set; }
        public string? Company { get; set; } = string.Empty;
        public string? CompanySite { get; set; } = string.Empty;
        public int? LanguageId { get; set; }
        public int? TimeZoneId { get; set; }
        public int? CurrencyId { get; set; }
        public int? Communication { get; set; }
        public bool? AllowMarketing { get; set; }

    }
}
