using AutoMapper;
using MediatR;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Application.Mappings;
using SistemaVenta.Domain;

namespace SistemaVenta.Application.Features.Products.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<GetAllProductQueryResponse>>
    {
      
    }

    public class GetAllProductQueryResponse
    {
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public bool? Active { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }

    public static class GetAllProductQueryMapping
    {
        public static void AddMappingGetAllProductQuery(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<Product, GetAllProductQueryResponse>()
                .ForMember(x => x.CategoryName , y => y.MapFrom(o => o.Category.Name));
        }
    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<GetAllProductQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GetAllProductQueryResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.ProductRepository.GetProductsByUserId();
            return mapper.Map<IEnumerable<GetAllProductQueryResponse>>(products);   
        }
    }

}
