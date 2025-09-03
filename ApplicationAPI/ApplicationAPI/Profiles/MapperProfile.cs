using ApplicationAPI.DTO;
using ApplicationAPI.Modelo;
using AutoMapper;

namespace ApplicationAPI.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        { 
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoDTO, Producto>();

        }
    }
}
