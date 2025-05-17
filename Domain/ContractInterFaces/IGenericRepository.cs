using Domain.Models;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ContractInterFaces
{
    public interface IGenericRepository<IEntity,Tkey> where IEntity : ModelBase<Tkey>
    {
        //getall,getbyid,add,update,delete
      Task<IEnumerable<IEntity>> GetAllAsync();  

      Task<IEntity> GetByIdAsync(Tkey id);

      Task<IEnumerable<IEntity>> GetAllAsync(ISpecifications<IEntity, Tkey> Spec);//Specifications

        Task<IEntity> GetByIdAsync(ISpecifications<IEntity, Tkey> Spec);//Specifications

        Task<int> CountAsync(ISpecifications <IEntity, Tkey> Spec);//CountPagination

        void Add(IEntity entity);

      void Update(IEntity entity);

      void Delete(IEntity entity);
    }

   
}
