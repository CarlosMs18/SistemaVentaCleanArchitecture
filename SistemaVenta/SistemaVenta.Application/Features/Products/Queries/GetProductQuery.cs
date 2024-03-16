using AutoMapper;
using MediatR;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Application.Exceptions;
using SistemaVenta.Application.Mappings;
using SistemaVenta.Domain;

namespace SistemaVenta.Application.Features.Products.Queries
{
    public class GetProductQuery : IRequest<GetProductQueryResponse>
    {
        public int Id { get; set; }
    } 

    public class GetProductQueryResponse
    {
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }

    public static class GetProductQueryMapping
    {
        public static void AddMappingGetProductQuery(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<Product, GetProductQueryResponse>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(o => o.Category.Name));
        }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetProductQueryHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = (await unitOfWork.ProductRepository.GetAsync(
                x => x.Id == request.Id && x.Active == true,
                orderBy : null,
                includeString : "Category"
                )).FirstOrDefault();
            if (product is null)
            {
                throw new BadRequestException("The product doesnt exists");
            }

            try
            {
                return mapper.Map<GetProductQueryResponse>(product);
            }catch (Exception ex)
            {
                throw new BadRequestException("An error ocurred a problem");
            }
        }
    }
}
