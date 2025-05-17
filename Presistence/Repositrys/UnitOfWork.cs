using Domain.ContractInterFaces;
using Domain.Models;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositrys
{
    public class UnitOfWork(StoreDBContext context) : IUnitOfWork
    {
        #region 
        //بشوف اسم الكلص لو موجود رجع النوع لو مش موجوداعمل واحد ورجعه 
        private readonly Dictionary<string, object> _Repositry = new Dictionary<string, object>();
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : ModelBase<Tkey>
        {
            var typeName = typeof(TEntity).Name;//بتشوف النوع

            if (_Repositry.ContainsKey(typeName))//بتشوفه موجود
            {
                return (IGenericRepository<TEntity, Tkey>)_Repositry[typeName];
            }

            var Repo = new GenericRepositry<TEntity, Tkey>(context);
            _Repositry.Add(typeName, Repo);
            return Repo;
        }

        #endregion
        public async Task<int> SaveChangesAsync()
        {
        return await context.SaveChangesAsync();
        }
    }  
}
