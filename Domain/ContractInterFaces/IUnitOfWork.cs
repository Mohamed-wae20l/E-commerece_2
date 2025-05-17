using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ContractInterFaces
{
   public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity,Tkey> GetRepository<TEntity,Tkey>()
            where TEntity : ModelBase<Tkey>;
    }
}
