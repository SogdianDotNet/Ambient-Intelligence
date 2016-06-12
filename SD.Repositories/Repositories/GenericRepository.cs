using SD.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SD.Repositories.Repositories
{
    /// <summary>
    /// class GenericRepository TEntity>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext dbContext;
        internal DbSet<TEntity> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GenericRepository(DbContext context)
        {
            if (context != null)
            {
                this.dbContext = context;
                this.dbSet = context.Set<TEntity>();
            }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dbSet.ToList().GetEnumerator();
        }

        public virtual IEnumerable<TEntity> GetNoTracking(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return GetList(filter, orderBy, includeProperties, true);
        }

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return GetList(filter, orderBy, includeProperties, false);
        }

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetList(filter, null, "", false);
        }

        /// <summary>
        /// Deletes the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        public virtual void Delete(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                TEntity entityToDelete = dbSet.Find(item);
                Delete(entityToDelete);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual bool Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
            {
                return Delete(entityToDelete);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public virtual bool DeleteAll(List<Guid> list)
        {
            foreach (Guid item in list)
            {
                Delete(item);
            }
            return true;
        }

        /// <summary>
        /// Deletes the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public virtual bool DeleteAll(List<int> list)
        {
            foreach (int item in list)
            {
                Delete(item);
            }
            return true;
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <returns></returns>
        public virtual bool Delete(TEntity entityToDelete)
        {
            if (entityToDelete != null)
            {
                if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
                dbContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns></returns>
        public virtual bool Update(TEntity entityToUpdate)
        {
            if (dbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                dbSet.Attach(entityToUpdate);
            }
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            return true;
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="useNoTracking">if set to <c>true</c> [use no tracking].</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool useNoTracking = false)
        {
            IQueryable<TEntity> query = (useNoTracking) ? dbSet.AsNoTracking() : dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            try
            {
                return (orderBy != null) ? orderBy(query).ToList() : query.ToList();
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Inserts the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public bool Insert(TEntity obj)
        {
            dbSet.Add(obj);
            return true;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>can be null</returns>
        public TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public TEntity Get(object id, string includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dbSet.Include(includeProperty);
                }
            }
            return dbSet.Find(id);
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                return dbSet.SingleOrDefault(filter);
            }
            catch (Exception)
            {
                throw new Exception("Database connection: SingleOrDefault failed");
            }
        }

        /// <summary>
        /// Gets the first or default.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.FirstOrDefault(filter);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList()
        {
            return GetList(null, null, null, false);
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="includeproperties">The includeproperties.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(string includeproperties)
        {
            return GetList(null, null, includeproperties, false);
        }
    }
}
