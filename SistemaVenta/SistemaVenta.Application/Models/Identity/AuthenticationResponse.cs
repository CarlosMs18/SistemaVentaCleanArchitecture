namespace SistemaVenta.Application.Models.Identity
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool Require2FA { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public DateTime RefreshTokenTime { get; set; }
        public string RefreshToken { get; set; }
    }
}
