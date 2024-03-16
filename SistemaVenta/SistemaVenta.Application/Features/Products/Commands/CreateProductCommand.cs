using AutoMapper;
using FluentValidation;
using MediatR;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Application.Exceptions;
using SistemaVenta.Application.Mappings;
using SistemaVenta.Domain;

namespace SistemaVenta.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<CreateProductCommandResponse>
    {
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public bool? Active { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }


    public class CreateProductCommandResponse
    {
        public bool Success { get; set; }
        public int ProductId { get; set; }
    }

    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidation()
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

    public static class CreateProductCommandMapping
    {
        public static void AddMapCreateProductCommand(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<CreateProductCommand, Product>();
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = (await unitOfWork.CategoryRepository.GetAsync(x => x.Id == request.CategoryId)).FirstOrDefault();  
            if(category == null)
            {
                throw new BadRequestException("The category doesn't exists");
            }

            var product = (await unitOfWork.ProductRepository.GetAsync(x => x.Name!.Equals(request.Name.ToLower()))).FirstOrDefault();

            try
            {
                var productRequest = mapper.Map<Product>(request);
                unitOfWork.ProductRepository.AddEntity(productRequest);
                await unitOfWork.Complete();

                return new CreateProductCommandResponse
                {
                    Success = true,
                    ProductId = productRequest.Id,
                };
            }catch (Exception ex)
            {
                throw new BadRequestException("An error ocurred while inserting a new product");
            }
        }
    }

}
