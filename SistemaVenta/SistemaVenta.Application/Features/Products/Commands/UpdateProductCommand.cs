using AutoMapper;
using FluentValidation;
using MediatR;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Application.Exceptions;
using SistemaVenta.Application.Mappings;
using SistemaVenta.Domain;

namespace SistemaVenta.Application.Features.Products.Commands
{
    public class UpdateProductCommand: IRequest<UpdateProductCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stock { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductCommandResponse
    {
        public bool Success { get; set; }
    }

    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Name cannot be null");
            RuleFor(x => x.Stock)
                  .NotNull().WithMessage("Stock cannot be null")
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be a non-negative number");
            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Category cannot be null");
            RuleFor(x => x.Price)
               .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public static class UpdateProductCommandMapping
    {
        public static void AddMapUpdateProductCommand(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<UpdateProductCommand, Product>()
                .ForMember(x => x.Active, y => y.MapFrom(o => true))
                .ForMember(x => x.RegistrationDate, y => y.MapFrom(o => DateTime.Now));
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = (await unitOfWork.ProductRepository.GetAsync(
                x => x.Id == request.Id,
                orderBy : null,
                disableTracking : true
                )).FirstOrDefault();
            if (product == null)
            {
                throw new BadRequestException("the product doesnt exists");
            }

            var category = (await unitOfWork.CategoryRepository.GetAsync(x => x.Id == request.CategoryId)).FirstOrDefault();
            if (category == null)
            {
                throw new BadRequestException("The category doesn't exists");
            }

            try
            {
                var productRequest = mapper.Map<Product>(request);
                unitOfWork.ProductRepository.UpdateEntity(productRequest);
                await unitOfWork.Complete();

                return new UpdateProductCommandResponse
                {
                    Success = true,
                };
            }catch (Exception ex)
            {
                throw new BadRequestException("An error ocurred while updating a new product");
            }

        }
    }
}
