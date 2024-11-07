using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specification.Contracts;

namespace Talabat.Repository
{
    internal static class SpecificationsEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> dbSet, ISpecifications<T> specifications)
        {
            var Query = dbSet;

            if(specifications.Criteria is not null) 
                Query = Query.Where(specifications.Criteria);

            if(specifications.OrderBy is not null)
                Query = Query.OrderBy(specifications.OrderBy);

            else if (specifications.OrderByDescending is not null)
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            else
                Query = Query.OrderBy(P => P.Id);

            if (specifications.Include?.Count > 0)   
                Query = specifications.Include.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));

            if(specifications.IsPaginationEnabled)
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);

            return Query;
        }
    }
}
