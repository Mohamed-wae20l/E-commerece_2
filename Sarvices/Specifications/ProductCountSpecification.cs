using Domain.Models.Products;
using Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarvices.Specifications
{
    public class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams productQuery)
            : base(P => (!productQuery.brandID.HasValue || P.BrandId == productQuery.brandID)
                     && (!productQuery.typeID.HasValue || P.TypeId == productQuery.typeID)
                        && (string.IsNullOrEmpty(productQuery.SearchValue) || P.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {

        }
    }
}
