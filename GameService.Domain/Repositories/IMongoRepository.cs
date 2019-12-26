using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameService.Domain.Repositories
{
    public interface IMongoRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression);
        T GetOne(string identifier);
        T CreateOne(T entity);
        bool Update(string identifier, T newEntity);
        bool Delete(string identifier);
    }
}