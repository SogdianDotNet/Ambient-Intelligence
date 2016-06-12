using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SD.Repositories.Interfaces
{
    /// <summary>
    /// IGenericRepository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        IEnumerator<T> GetEnumerator();
        IEnumerable<T> GetNoTracking(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        IEnumerable<T> GetList();
        T Get(object id);
        T Get(Expression<Func<T, bool>> filter);
        bool Insert(T obj);
        bool Delete(object id);
        bool Delete(T entityToDelete);
        bool Update(T entityToUpdate);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool useNoTracking = false);
    }
}
