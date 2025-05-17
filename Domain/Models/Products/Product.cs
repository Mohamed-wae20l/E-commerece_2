using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Products
{
   public class Product:ModelBase<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;//Description
        public decimal Price { get; set; }//Price
        public string PictureUrl { get; set; } = null!;//PictureUrl
        public ProductBrand Brand { get; set; }//Relation 1
        public int BrandId { get; set; }//ForenKey
        public ProductType Type { get; set; }//Relation 1
        public int TypeId { get; set; }//ForenKey
    }
}
