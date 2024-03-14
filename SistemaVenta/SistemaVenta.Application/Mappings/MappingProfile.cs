using AutoMapper;
using SistemaVenta.Application.Features.Categories.Commands;

namespace SistemaVenta.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AddMapCreateCategoryCommand();
        }
    }
}
