using MediatR;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Application.Exceptions;

namespace SistemaVenta.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandResponse
    {
        public bool Success { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = (await unitOfWork.ProductRepository.GetAsync(x => x.Id == request.Id && x.Active == true)).FirstOrDefault();

            if (product == null)
            {
                throw new BadRequestException("the product doesnt exists");
            }

            try
            {
                product.Active = false;
                await unitOfWork.Complete();
                return new DeleteProductCommandResponse
                {
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An error ocurred while deleting a new product");
            }
        }
    }
}
