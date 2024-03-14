using AutoMapper;
using FluentValidation;
using MediatR;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Application.Exceptions;
using SistemaVenta.Application.Mappings;
using SistemaVenta.Domain;

namespace SistemaVenta.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }
    }

    public class CreateCategoryCommandResponse
    {
        public bool Success { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidation()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Name cannot be null");
            RuleFor(x => x.Description)
                 .NotEmpty().WithMessage("Description cannot be null");
            
        }
    }

    public static class CreateCategoryCommandMapping
    {
        public static void AddMapCreateCategoryCommand(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<CreateCategoryCommand, Category>();
        }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;   
        }
        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = (await unitOfWork.CategoryRepository.GetAsync(x => x.Name!.Equals(request.Name.ToLower()))).FirstOrDefault();

            if (category is not null)
            {
                throw new BadRequestException("The entered category exists");
            }
            try
            {
                //var categoryRequest = new Category
                //{
                //    Name = request.Name,
                //    Description = request.Description,
                //};
                var categoryRequest = mapper.Map<Category>(request);
                unitOfWork.CategoryRepository.AddEntity(categoryRequest);

                await unitOfWork.Complete();

                return new CreateCategoryCommandResponse
                {
                    Success = true,
                    CategoryId = categoryRequest.Id

                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new BadRequestException("An error ocurred while inserting a new category");
            }
        }
    }
}
