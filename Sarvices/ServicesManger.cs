using Abstraction;
using AutoMapper;
using Domain.ContractInterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarvices
{
    public class ServicesManger(IUnitOfWork unitOfWork,IMapper mapper) : IServicesManger
    {
        //ServicesMangerمن خلال IproductServicesبتحكم في كل 
        public readonly Lazy<IProductServices> _productServices = new Lazy<IProductServices>(()=>new ProductServices(unitOfWork,mapper));
        public IProductServices productServices => _productServices.Value;
    }
}
