using DemoApp.Core.Data.Enity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using DemoApp.Core.Constants;

namespace DemoApp.Core.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private DataContext dataContext;
        private readonly DbSet<T> dbSet;
        private IServiceProvider _serviceProvider;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected DataContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory, IServiceProvider serviceProvider)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
            _serviceProvider = serviceProvider;
        }

        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void AddRangeAsync(IEnumerable<T> entitys)
        {
            dbSet.AddRangeAsync(entitys);

        }
        //public virtual void AddBulk(IEnumerable<T> entitys)
        //{
        //    dataContext.BulkInsert(entitys.ToList());
        //}

        public virtual void Add(ICollection<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(String id)
        {
            return dbSet.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }
        public string GetUsername()
        {
            try
            {
                var accessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return accessor?.HttpContext?.User?.FindFirst(Constant.CLAIM_USERNAME)?.Value ?? Constant.USER_ANONYMOUS;
            }
            catch
            {
                return "SYSTEM";
            }
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.Count();
        }
        public string GetCurrentUserId()
        {
            var accessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            return accessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        #endregion
    }
}
