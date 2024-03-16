using AutoMapper;
using MediatR;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Application.Mappings;
using SistemaVenta.Domain;

namespace SistemaVenta.Application.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<GetAllCategoryQueryResponse>>
    {
    }

    public class GetAllCategoryQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }

    public static class GetAllCategoryQueryMapping
    {
        public static void AddMappingGetAllCategoryQuery(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<Category, GetAllCategoryQueryResponse>();
        }
    }
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetAllCategoryQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GetAllCategoryQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {

            var categories = await unitOfWork.CategoryRepository.GetAsync();
            return mapper.Map<IEnumerable<GetAllCategoryQueryResponse>>(categories);
        }

       
    }
}
