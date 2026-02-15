using AutoMapper;
using Prueba.Entities.Entities;
using Prueba.Models;

namespace Prueba.Extensions
{
    public class MappingProfileExtensions : Profile
    {
        public MappingProfileExtensions()
        {
            CreateMap<Cliente, ClienteViewModel  >().ReverseMap();
            CreateMap<Producto, ProductoViewModel>().ReverseMap();
            CreateMap<Orden, OrdenViewModel>().ReverseMap();
        }

    }
}
