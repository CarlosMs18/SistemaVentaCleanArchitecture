namespace SistemaVenta.Application.Models.Identity
{
    public class RegistrationRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool AgreeToTerms { get; set; }
        public bool SignUp { get; set; }
    }
}
