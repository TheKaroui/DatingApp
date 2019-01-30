using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatingApp.API.Core.Paging;

namespace DatingApp.API.DataProviders.Repository.GenericRepository {
    public interface IGenericRepository<TEntity> where TEntity : class {

        #region ReadOnly Repository

        Task<TEntity> GetByIdAsync (int id);
        Task<ICollection<TEntity>> GetAllAsync ();
        Task<ICollection<TEntity>> QueryAsync (Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<PagedModel<TEntity>> QueryAsync (Expression<Func<TEntity, bool>> expression, SortOptions sortOptions, PaginateOptions paginateOptions, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<bool> ExistsAsync (Expression<Func<TEntity, bool>> expr);
        Task<int> CountAsync();

        #endregion

        #region PersistRepository

        Task AddAsync (TEntity entity);
        void Add (TEntity entity);
        void AddRangeAsync (IEnumerable<TEntity> items);
        void UpdateAsync (TEntity entity);
        void DeleteAsync (int id);
        Task<int> SaveAsync();
        int Save();
        void Dispose();

        #endregion

    }
}