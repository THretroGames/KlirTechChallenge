using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Klir.TechChallenge.Web.Api.Abstracts;
using Klir.TechChallenge.Web.Api.DTOs;
using Klir.TechChallenge.Web.Api.Entities;

namespace Klir.TechChallenge.Web.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ForMember(dest => dest.Promotion, opt => opt.MapFrom(src => src.Promotion.Name));

            CreateMap<CartProduct, CartProductDto>().
            ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name)).
            ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price)).
            ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id)).
            ForMember(dest => dest.Promotion, opt => opt.MapFrom(src => src.Product.Promotion.Name));
        }
    }
}