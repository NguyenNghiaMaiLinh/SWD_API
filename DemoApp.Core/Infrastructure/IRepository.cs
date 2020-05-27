using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DemoApp.Core.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        void Add(T entity);
        void AddRangeAsync(IEnumerable<T> entitys);

        //void AddBulk(IEnumerable<T> entitys);
        void Add(ICollection<T> entities);
        // Marks an entity as modified
        void Update(T entity);
        // Marks an entity to be removed
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        // Get an entity by int id
        //T GetById(Guid id);
        T GetById(String id);
        // Get an entity using delegate
        IQueryable<T> GetAll();
        // Gets entities using delegate
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        string GetUsername();
        int Count(Expression<Func<T, bool>> predicate);
        string GetCurrentUserId();
    }
}
