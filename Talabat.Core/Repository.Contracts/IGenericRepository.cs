using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification.Contracts;

namespace Talabat.Core.Repository.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T?> GetAsync(int Id);
        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications);
        public Task<T?> GetWithSpecAsync(ISpecifications<T> specifications);
        public Task<int> GetCountAsync(ISpecifications<T> specifications);
        public  Task AddAsync(T Entity);
        public void Update(T Entity);
        public void Delete(T Entity);

    }
}
