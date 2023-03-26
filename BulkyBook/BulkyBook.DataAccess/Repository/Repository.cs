using BulkyBook.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _db = context;
            this.dbSet = _db.Set<T>();

        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
            
        }

        public IEnumerable<T> GetAll(string? includeRelatedEntityes)
        {
            IQueryable<T> query = dbSet;
            if(includeRelatedEntityes!=null)
            {
                foreach(var entityType in includeRelatedEntityes.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(entityType);
                }
            }
            return query.ToList();
        }

        //public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        //{
        //    IQueryable<T> query = dbSet;
        //    query = query.Where(filter);
        //    return query.FirstOrDefault();
        //}
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<T> query = dbSet;

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<T> query = dbSet.AsNoTracking();

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }

        }


        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
