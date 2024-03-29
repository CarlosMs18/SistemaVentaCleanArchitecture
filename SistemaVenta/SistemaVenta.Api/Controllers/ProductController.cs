﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.Application.Features.Products.Commands;
using SistemaVenta.Application.Features.Products.Queries;
using System.Net;

namespace SistemaVenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateProductCommandResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            return Ok(await mediator.Send(request));
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetProductQueryResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetProduct(int id)
        {
            return Ok(await mediator.Send(new GetProductQuery { Id = id}));
        }

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<GetAllProductQueryResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(await mediator.Send(new GetAllProductQuery { }));
        }

        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UpdateProductCommandResponse>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
        {
            return Ok(await mediator.Send(request));   
        }

        [HttpDelete("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<DeleteProductCommand>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteProduct([FromBody] DeleteProductCommand request)
        {
            return Ok(await mediator.Send(request));
        }
    }
}
