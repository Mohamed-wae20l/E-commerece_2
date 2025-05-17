using Domain.ContractInterFaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, Tkey> Spec)
            where TEntity : ModelBase<Tkey>
        {
            var Query = InputQuery;

            if(Spec.Criteria is not null)
                Query = Query.Where(Spec.Criteria);
            //context.Set<Product>().where(p=>(p.BrandId==brandID)&&(p.TypeId==typeID)

            if(Spec.Orderby is not null)
                Query= Query.OrderBy(Spec.Orderby);

            if(Spec.OrderbyDesc is not null)
                Query=Query.OrderByDescending(Spec.OrderbyDesc);
           

            if (Spec.IncLodeExpressions is not null && Spec.IncLodeExpressions.Count > 0)
                Query = Spec.IncLodeExpressions.Aggregate(Query, (curredtQuery, Exp) => curredtQuery.Include(Exp));
            //context.Set<Product>().where(p//context.Set<Product>().where(p=>(p.BrandId==brandID)&&(p.TypeId==typeID).Includ(p=>p.BrandId).Includ(p=>p.TypeId)

            if(Spec.IsPaginated==true)
            {
                Query=Query.Skip(Spec.Skip).Take(Spec.Take);
            }

            return Query;
        }
    }
}
