namespace SistemaVenta.Application.Models.Identity
{
    public class UserSession
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ClientIP { get; set; }
    }
}
