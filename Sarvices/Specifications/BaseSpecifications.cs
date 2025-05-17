using Domain.ContractInterFaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sarvices.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey>
        where TEntity : ModelBase<Tkey>
    {
        #region Criteria
        public BaseSpecifications(Expression<Func<TEntity, bool>>? PassedExpression)
        {
            Criteria = PassedExpression;//Set Value
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>> IncLodeExpressions { get; } = new List<Expression<Func<TEntity, Object>>>();

        protected void AddInclude(Expression<Func<TEntity, object>> InludeExp)
        {
            IncLodeExpressions.Add(InludeExp);
        }
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> Orderby { get; private set; }

        public Expression<Func<TEntity, object>> OrderbyDesc { get; private set; }

       
        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)=>Orderby=OrderByExpression;
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExpression)=>Orderby= OrderByDescExpression;
        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; set; }
        //total40
        //pagesize=10
        //pageindex=3
        // Result=  العشره رقم 3
        protected void ApplyPagination(int PageSize,int PageIndex)
        {
            IsPaginated= true;
            Take= PageSize;
            Skip= (PageIndex-1)*PageSize;
        }

        #endregion
    }
}
