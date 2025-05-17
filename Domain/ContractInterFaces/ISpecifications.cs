using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ContractInterFaces
{
   public interface ISpecifications<TEntity,Tkey> where TEntity:ModelBase<Tkey>
    {
        //Specifications Design Pattern
       Expression<Func<TEntity, bool>>? Criteria { get; }
       List<Expression<Func<TEntity,object>>> IncLodeExpressions {  get; }   
        Expression<Func<TEntity, object>> Orderby { get; }
        Expression<Func<TEntity, object>> OrderbyDesc { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; set; }
    }
}
