using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SistemaVenta.Application.Features.Categories.Commands;
using SistemaVenta.Application.Features.Categories.Queries;
using SistemaVenta.Domain;

namespace SistemaVenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateCategoryCommandResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetAllCategoryQueryResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllCategories()
        {
            return Ok(await mediator.Send(new GetAllCategoriesQuery { }));
        }
    }
}
