using AutoMapper;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;
using Shareds.DTo.s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarvices.MappingProfiles
{
    public class ProductReslover(IConfiguration configuration ) : IValueResolver<Product, ProductDTo, string>
    {
        // Microsoft.Extensions.Configuration تنزيل
        //  لصورconfigar بعمل 
        public string Resolve(Product source, ProductDTo destination, string destMember, ResolutionContext context)
        {
           if (string.IsNullOrEmpty(source.PictureUrl))
            {

                return string.Empty;
            }

            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl }";
                return Url;
            }
        }
    }
}
