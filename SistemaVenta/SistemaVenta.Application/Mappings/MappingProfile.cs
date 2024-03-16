using AutoMapper;
using SistemaVenta.Application.Features.Categories.Commands;
using SistemaVenta.Application.Features.Categories.Queries;

namespace SistemaVenta.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AddMapCreateCategoryCommand();
            this.AddMappingGetAllCategoryQuery();
        }
    }
}
