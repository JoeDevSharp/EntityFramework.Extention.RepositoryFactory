using JoeDevSharp.RepositoryFactory.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JoeDevSharp.RepositoryFactory.EntityFramework.Core
{
    /// <summary>
    /// Generic repository implementation for Entity Framework, supporting both sync and async operations.
    /// </summary>
    /// <typeparam name="E">Entity type that implements IEntityBase.</typeparam>
    public class GenericRepository<E> : IGenericRepository<E> where E : class, IEntityBase
    {
        private readonly DbContext _context;
        private readonly DbSet<E> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<E>();
        }

        public void Add(E entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
        }

        public async Task AddAsync(E entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<E> entities)
        {
            if (entities == null || !entities.Any()) 
                throw new ArgumentException("Entities collection is null or empty.", nameof(entities));

            _dbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<E> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentException("Entities collection is null or empty.", nameof(entities));

            await _dbSet.AddRangeAsync(entities);
        }

        public int Count(Expression<Func<E, bool>>? filter = null)
        {
            return filter == null 
                ? _dbSet.Count() 
                : _dbSet.Count(filter);
        }

        public async Task<int> CountAsync(Expression<Func<E, bool>>? filter = null)
        {
            return filter == null 
                ? await _dbSet.CountAsync() 
                : await _dbSet.CountAsync(filter);
        }

        public bool Exists(Expression<Func<E, bool>> filter)
        {
            if (filter == null) 
                throw new ArgumentNullException(nameof(filter));

            return _dbSet.Any(filter);
        }

        public async Task<bool> ExistsAsync(Expression<Func<E, bool>> filter)
        {
            if (filter == null) 
                throw new ArgumentNullException(nameof(filter));

            return await _dbSet.AnyAsync(filter);
        }

        public E? Find(Expression<Func<E, bool>>? filter = null, Func<IQueryable<E>, IQueryable<E>>? include = null)
        {
            IQueryable<E> query = _dbSet;

            if (include != null) 
                query = include(query);

            return filter != null 
                ? query.SingleOrDefault(filter) 
                : null;
        }

        public async Task<E?> FindAsync(Expression<Func<E, bool>> filter, Func<IQueryable<E>, IQueryable<E>>? include = null)
        {
            if (filter == null) 
                throw new ArgumentNullException(nameof(filter));

            IQueryable<E> query = _dbSet;

            if (include != null) 
                query = include(query);

            return await query.SingleOrDefaultAsync(filter);
        }

        public IEnumerable<E> Get(Expression<Func<E, bool>>? filter = null, int pageNumber = 1, int pageSize = 10, Func<IQueryable<E>, IQueryable<E>>? include = null)
        {
            if (pageNumber < 1) 
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            IQueryable<E> query = _dbSet;

            if (filter != null) 
                query = query.Where(filter);

            if (include != null) 
                query = include(query);

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null, int pageNumber = 1, int pageSize = 10, Func<IQueryable<E>, IQueryable<E>>? include = null)
        {
            if (pageNumber < 1) 
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            IQueryable<E> query = _dbSet;
            if (filter != null) 
                query = query.Where(filter);

            if (include != null)
                query = include(query);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Remove(E entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        public async Task RemoveAsync(E entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));

            await Task.Run(() => _dbSet.Remove(entity));
        }

        public void RemoveRange(IEnumerable<E> entities)
        {
            if (entities == null || !entities.Any()) 
                throw new ArgumentException("Entities collection is null or empty.", nameof(entities));

            _dbSet.RemoveRange(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<E> entities)
        {
            if (entities == null || !entities.Any()) 
                throw new ArgumentException("Entities collection is null or empty.", nameof(entities));

            await Task.Run(() => _dbSet.RemoveRange(entities));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(E entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
        }

        public async Task UpdateAsync(E entity)
        {
            if (entity == null) 
                throw new ArgumentNullException(nameof(entity));

            await Task.Run(() => _dbSet.Update(entity));
        }
    }
}
