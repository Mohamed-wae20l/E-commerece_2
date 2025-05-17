using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shareds;
using Shareds.DTo.s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServicesManger servicesManger):ControllerBase
    {
        //  
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTo>>> GetAllProuducts([FromQuery]ProductQueryParams productQuery)//filt brandID,typeID)
        {
            var products = await servicesManger.productServices.GetAllProductsAsync(productQuery );
            return Ok(products);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetAllBrands()
        {
            var Brands = await servicesManger.productServices.GetAllBrandsAsync();
            return Ok(Brands); 
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetAllTypes()
        {
            var Types = await servicesManger.productServices.GetAllTypesAsync();
            return Ok(Types);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTo>> GetproductById(int id)
        {
            var Product = await servicesManger.productServices.GetProductByIdAsync(id);
            return Ok(Product);
        }

    }
}
