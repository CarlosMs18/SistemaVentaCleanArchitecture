namespace SistemaVenta.Application.Models.Identity
{
    public class AuthenticationRequest
    {      
        public string? Email { get; set; }  
        public string? Password { get; set; }
        public string? Token2FA { get; set; }
    }
}
