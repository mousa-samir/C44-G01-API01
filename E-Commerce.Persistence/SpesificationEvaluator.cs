using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal abstract class SpesificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EnterPoint,
            ISpecifictions<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = EnterPoint;
            if (specifications is not null)
            {
                if (specifications.Criteria is not null)
                {
                    Query = Query.Where(specifications.Criteria);
                }
                if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    //foreach (var includeExp in specifictions.IncludeExpressions)
                    //{
                    //    Query =Query.Include(includeExp);
                    //}
                    Query = specifications.IncludeExpressions
                        .Aggregate(Query ,(CurrentQuery , includeExp) => CurrentQuery.Include(includeExp));
                }
                if(specifications.OrdeyBy is not null )
                {
                    Query = Query.OrderBy(specifications.OrdeyBy);
                }
                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderByDescending(specifications.OrderByDescending);
                }
                if(specifications is not null)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }
            }
            return Query;
        }
    }
}
