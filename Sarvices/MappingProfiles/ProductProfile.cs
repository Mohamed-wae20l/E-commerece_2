using AutoMapper;
using Domain.Models.Products;
using Shareds.DTo.s;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarvices.MappingProfiles
{
   public class ProductProfile:Profile
    {

        //AtoMapperخاص 
        public ProductProfile()
        {
            CreateMap<Product, ProductDTo>()
                .ForMember(Dist => Dist.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(Dist => Dist.TypeName, options => options.MapFrom(src => src.Type.Name))
                .ForMember(Dist => Dist.PicturUrl, options => options.MapFrom<ProductReslover>());
                

            CreateMap<ProductBrand, BrandDTo>();
            CreateMap<ProductType, TypeDTo>();
        }
    };
}
