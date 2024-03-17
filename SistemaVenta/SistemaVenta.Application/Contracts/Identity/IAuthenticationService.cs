using SistemaVenta.Application.Models.Identity;

namespace SistemaVenta.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
