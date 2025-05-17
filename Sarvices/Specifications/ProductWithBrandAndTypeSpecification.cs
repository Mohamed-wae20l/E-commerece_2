using Domain.Models.Products;
using Shareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarvices.Specifications
{
    public  class ProductWithBrandAndTypeSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQuery)
            : base(P => (!productQuery.brandID.HasValue || P.BrandId == productQuery.brandID)
                     && (!productQuery.typeID.HasValue || P.TypeId == productQuery.typeID)
                        && (string.IsNullOrEmpty(productQuery.SearchValue) || P.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {
            AddInclude(p=> p.Brand);
            AddInclude(p => p.Type);

            switch(productQuery.sortingOptions)
            {
                case ProductSortingOptions.NameAsc: 
                    AddOrderBy(p => p.Name);
                        break;
                
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions .PriceDesc:
                    AddOrderByDesc(p=> p.Price);
                    break;
                defult:
                    break;


            }

            ApplyPagination(productQuery.PageSize, productQuery.PageIdnex);
        }

        //flter
        public ProductWithBrandAndTypeSpecification(int id) : base(p=>p.Id==id) 
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
