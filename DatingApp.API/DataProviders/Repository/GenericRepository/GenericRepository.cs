using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DatingApp.API.Core.Extensions;
using DatingApp.API.Core.Paging;
using DatingApp.API.DataProviders.Domain;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.DataProviders.Repository.GenericRepository {
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class {

        private readonly DatingAppContext _context;
        private readonly DbSet<TEntity> _entitySet;

        public GenericRepository (DatingAppContext context) {
            this._context = context;
            _entitySet = _context.Set<TEntity> ();
        }

        public async Task AddAsync (TEntity entity) {
            await _entitySet.AddAsync (entity);
        }
        public void AddRangeAsync (IEnumerable<TEntity> items) {
            Parallel.ForEach (items, async (currentItem) => await AddAsync (currentItem));
        }
        public async void DeleteAsync (int id) {
            _entitySet.Remove (await _entitySet.FindAsync (id));
        }
        public async void UpdateAsync (TEntity entity) {
            _context.Entry (entity).State = EntityState.Modified;
            await _context.SaveChangesAsync ();
        }




        public async Task<ICollection<TEntity>> GetAllAsync () {
            return await _entitySet.ToListAsync ();
        }
        public async Task<TEntity> GetByIdAsync (int id) {
            return await _entitySet.FindAsync (id);
        }
        private IQueryable<TEntity> LoadProperties (IEnumerable<Expression<Func<TEntity, object>>> includeProperties) {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>> (_entitySet, (current, includeProperty) => current.Include (includeProperty));
        }
        public async Task<ICollection<TEntity>> QueryAsync (Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties) {
            var query = LoadProperties (includeProperties);
            return await query.Where(expression).ToListAsync();
        }
        public async Task<PagedModel<TEntity>> QueryAsync (Expression<Func<TEntity, bool>> expression, SortOptions sortOptions, PaginateOptions paginateOptions, params Expression<Func<TEntity, object>>[] includeProperties) {

            var query = _entitySet.AsQueryable ().Where (expression);
            var count = await query.CountAsync ();

            //Unfortunatly includes can't be covered with a UT and Mocked DbSets...
            if (includeProperties.Length != 0)
                query = includeProperties.Aggregate(query, (current, prop) => current.Include(prop));

            if (paginateOptions == null || paginateOptions.PageSize <= 0 || paginateOptions.CurrentPage <= 0)
                return new PagedModel<TEntity> {
                    Results = await query.ToListAsync(),
                    TotalNumberOfRecords = count
                };

            if (sortOptions != null)
                query = query.OrderByPropertyOrField(sortOptions.OrderByProperty, sortOptions.IsAscending);

            var skipAmount = paginateOptions.PageSize * (paginateOptions.CurrentPage - 1);
            query = query.Skip(skipAmount).Take(paginateOptions.PageSize);
            return new PagedModel<TEntity> {
                Results = await query.ToListAsync(),
                TotalNumberOfRecords = count,
                CurrentPage = paginateOptions.CurrentPage,
                TotalNumberOfPages = (count / paginateOptions.PageSize) + (count % paginateOptions.PageSize == 0 ? 0 : 1)
            };
        }
        public async Task<bool> ExistsAsync (Expression<Func<TEntity, bool>> expr) {
            return await _entitySet.AnyAsync(expr);
        }

        public async Task<int> CountAsync () {
           return await _entitySet.CountAsync();
        }




        public async Task<int> SaveAsync () {
            return await _context.SaveChangesAsync();
        }



        private bool disposed = false;  
        protected void Dispose(bool disposing)  
        {  
            if (!this.disposed)  
            {  
                if (disposing)  
                {  
                _context.Dispose();  
                }  
                this.disposed = true;  
            }  
        } 
  
        public void Dispose()
        {  
            Dispose(true);  
            GC.SuppressFinalize(this);  
        }



    }
}