using AutoMapper;
using SistemaVenta.Application.Features.Categories.Commands;
using SistemaVenta.Application.Features.Categories.Queries;
using SistemaVenta.Application.Features.Products.Commands;

namespace SistemaVenta.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AddMapCreateCategoryCommand();
            this.AddMappingGetAllCategoryQuery();
            this.AddMapCreateProductCommand();
        }
    }
}
