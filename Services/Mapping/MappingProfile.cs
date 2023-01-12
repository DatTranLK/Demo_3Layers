using AutoMapper;
using Enities.Dtos;
using Enities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Category.CategoryName));

        }
    }
}
