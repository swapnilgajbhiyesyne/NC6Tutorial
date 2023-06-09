﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll(string? includeRelatedEntityes=null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        T GetFirstOrDefault(Expression<Func<T,bool>> filter,string? includeProperties = null, bool tracked = true);

    }
}
