using EcreoLibraryStandard;
using EcreoUserAPI.Data.Contexts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EcreoUserAPI.Repositories.Common
{
    public interface IRepository<T> 
    {
        public Task<T> Get(string id);
        public Task<IReadOnlyCollection<T>> GetAll();
        public Task<T> Add(T @object);
        public Task Update(T @object);
        public Task Delete(T @object);
        public Task<IReadOnlyCollection<T>> Where(Expression<Func<T, bool>> condition);
        public Task<T> FirstOrDefault(Expression<Func<T, bool>> condition);
    }
}
