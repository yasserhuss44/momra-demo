using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace  Helpers.Generics

{
    public interface IGenericRepository<T> where T : class
    {
        

        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        T FindSingle(int id);

        T FindBy(Expression<Func<T, bool>> predicate, string includeProperties = "");

        void Add(T toAdd);

        void Update(T toUpdate);

        void Delete(int id);

        void Delete(T entity);

        DbSet<T> DbSet { get;  }
         void Save();
    }
}
