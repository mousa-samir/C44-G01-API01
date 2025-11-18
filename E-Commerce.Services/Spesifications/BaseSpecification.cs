using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal abstract class BaseSpecification<TEntity, TKey> : ISpecifictions<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        public Expression<Func<TEntity, bool>> Criteria {  get; }
        protected BaseSpecification(Expression<Func<TEntity,bool>>criteriaEpression)
        {
            Criteria = criteriaEpression;
        }


        #region Includes
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includdeExp)
        {
            IncludeExpressions.Add(includdeExp);
        }
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrdeyBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrdeyBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByExpressionDescendingExpression)
        {
            OrderByDescending = orderByExpressionDescendingExpression;
        }


        #endregion

        #region Pagination

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

        #endregion

    }
}
