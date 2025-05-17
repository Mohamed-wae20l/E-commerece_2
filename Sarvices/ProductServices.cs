using Abstraction;
using AutoMapper;
using Domain.ContractInterFaces;
using Domain.Exceptions;
using Domain.Models.Products;
using Sarvices.Specifications;
using Shareds;
using Shareds.DTo.s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarvices
{
    public class ProductServices(IUnitOfWork unitOfWork,IMapper mapper) : IProductServices
    {
      
        public async Task<IEnumerable<BrandDTo>> GetAllBrandsAsync()
        {
           var _Repositry= unitOfWork.GetRepository<ProductBrand,int>();
            var Brands=await _Repositry.GetAllAsync();
            var MappedBrands=mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDTo>>(Brands);
            return MappedBrands;
        }

        public async Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams productQuery)
        {
            var _Repositry = unitOfWork.GetRepository<Product, int>();
            var Spec = new ProductWithBrandAndTypeSpecification(productQuery);//Specification
            var Products = await _Repositry.GetAllAsync(Spec);
            var MappedProduct = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(Products);

            var CountProducts = Products.Count();
            var CountSpec = new ProductCountSpecification(productQuery);
            var TotaCount = await _Repositry.CountAsync(CountSpec);
            return new PaginatedResult<ProductDTo>(productQuery.PageIdnex, CountProducts,TotaCount,MappedProduct);
        }

        public async Task<IEnumerable<TypeDTo>> GetAllTypesAsync()
        {
            var _Repositry = unitOfWork.GetRepository<ProductType, int>();
            var Types = await _Repositry.GetAllAsync();
            var MappedTypes = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTo>>(Types);
            return MappedTypes;
        }

        public async Task<ProductDTo> GetProductByIdAsync(int id)
        {
            var Spec= new ProductWithBrandAndTypeSpecification(id);//filtration Spec
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(Spec);
            if(Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            return mapper.Map<Product, ProductDTo>(Product);
        }
    }
}
