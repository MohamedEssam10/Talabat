using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext DbContext;
        private  Hashtable Repositories;

        public UnitOfWork(StoreDbContext DbContext)
        {
            Repositories = new Hashtable();
            this.DbContext = DbContext;
        }
        public async Task<int> CompleteAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await DbContext.DisposeAsync();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if(!Repositories.ContainsKey(type))
            {
                IGenericRepository<T> Repo = new GenericRepository<T>(DbContext);
                Repositories.Add(type, Repo);
            }

            return (IGenericRepository<T>) Repositories[type];
        }
    }
}
