using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaVenta.Application.Constants;
using SistemaVenta.Application.Contracts.Identity;
using SistemaVenta.Application.Exceptions;
using SistemaVenta.Application.Models;
using SistemaVenta.Application.Models.Identity;
using SistemaVenta.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaVenta.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;
        private readonly GeneralSettings generalSettings;
        private readonly UserSession userSession;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings,
            IOptions<GeneralSettings> generalSettings,
            UserSession userSession
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
            this.generalSettings = generalSettings.Value;
            this.userSession = userSession;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid login attempt.");
            }
            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var initialDuration = result.RequiresTwoFactor ? jwtSettings.DurationInMinutesFor2FA :
                    jwtSettings.DurationInMinutesForQR2FA;
                initialDuration = initialDuration > 0 ? initialDuration : 5;

                var token = await GenerateToken(user, initialDuration);
                return new AuthenticationResponse
                {
                    Id = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Email = user.Email,
                    Username = user.UserName,
                    Require2FA = result.RequiresTwoFactor
                };
            }

            throw new BadRequestException("Invalid login attempt.");
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var user_exists = await userManager.FindByEmailAsync(request.Email);   
            if(user_exists != null )
            {
                throw new Exception($"Username '{request.Email}' is already taken");
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true,
                TwoFactorEnabled = false
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if(!result.Succeeded)
            {
                throw new ValidationException(result.Errors.Select(e => new KeyValuePair<string, string>(e.Code, e.Description)));
            }

            var token = await GenerateToken(user);

            return new RegistrationResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Username = user.UserName,
                TokenConfirm = "",
                UrlConfirm = "",
            };
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user, double durationInMinutes = 0)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.UID, user.Id)
            }.Union(userClaims).Union(roleClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(durationInMinutes > 0 ? durationInMinutes : jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);


            return jwtSecurityToken;
        }
    }
}
