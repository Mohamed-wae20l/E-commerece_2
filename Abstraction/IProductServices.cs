using Shareds;
using Shareds.DTo.s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
  public interface IProductServices
    {
        Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams productQuery);//filt brandID,typeID);
        Task<ProductDTo> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDTo>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTo>> GetAllTypesAsync();
    }
}
