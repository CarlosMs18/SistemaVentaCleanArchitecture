using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.Application.Contracts.Identity;
using SistemaVenta.Application.Models.Identity;
using System.Net;

namespace SistemaVenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService authentication;

        public AccountController(IAuthenticationService authentication)
        {
            this.authentication = authentication;
        }


        [HttpPost("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody]RegistrationRequest request)
        {
            return Ok(await authentication.Register(request));  
        }
    }
}
