using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Specification.Contracts;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext dbcontext;

        public GenericRepository(StoreDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IReadOnlyList<T>)await dbcontext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync();
            return await dbcontext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int Id)
        {
            if (typeof(T) == typeof(Product))
                return await dbcontext.Set<Product>().Where(E => E.Id == Id).Include(E => E.Brand).Include(E => E.Category).FirstOrDefaultAsync() as T;

            return await dbcontext.Set<T>().FindAsync(Id);
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> specifications)
        {
            return await SpecificationsEvaluator<T>.GetQuery(dbcontext.Set<T>(), specifications).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications)
        {
            return await SpecificationsEvaluator<T>.GetQuery(dbcontext.Set<T>(), specifications).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            return await SpecificationsEvaluator<T>.GetQuery(dbcontext.Set<T>(), specifications).CountAsync();
        }
    
        public async Task AddAsync(T Entity)
        {
            await dbcontext.Set<T>().AddAsync(Entity);
        }

        public void Update(T Entity)
        {
            dbcontext.Set<T>().Update(Entity);
        }
        public void Delete(T Entity)
        {
            dbcontext.Set<T>().Remove(Entity);
        }



    }
}
