using System.Linq.Expressions;

namespace JoeDevSharp.RepositoryFactory.EntityFramework.Interfaces
{
    /// <summary>
    /// Generic contract for repository operations supporting basic CRUD operations,
    /// pagination, filtering, eager loading and async workflows using Entity Framework.
    /// </summary>
    public interface IGenericRepository<E> where E : class, IEntityBase
    {
        // -------------------- CREATE --------------------

        /// <summary>
        /// Adds a new entity to the context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(E entity);

        /// <summary>
        /// Adds a collection of entities to the context.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        void AddRange(IEnumerable<E> entities);

        /// <summary>
        /// Asynchronously adds a new entity to the context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(E entity);

        /// <summary>
        /// Asynchronously adds a collection of entities to the context.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<E> entities);

        // -------------------- READ --------------------

        /// <summary>
        /// Retrieves a single entity matching the given filter, with optional eager loading.
        /// </summary>
        /// <param name="filter">An optional expression to filter the entity.</param>
        /// <param name="include">An optional function to include related entities.</param>
        /// <returns>The matched entity or null.</returns>
        E? Find(Expression<Func<E, bool>>? filter = null, Func<IQueryable<E>, IQueryable<E>>? include = null);

        /// <summary>
        /// Retrieves a paginated list of entities, with optional filtering and eager loading.
        /// </summary>
        /// <param name="pageNumber">The page number (starting at 1).</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="filter">An optional expression to filter the entities.</param>
        /// <param name="include">An optional function to include related entities.</param>
        /// <returns>A list of entities for the specified page.</returns>
        IEnumerable<E> Get(Expression<Func<E, bool>>? filter = null, int pageNumber = 1, int pageSize = 10, Func<IQueryable<E>, IQueryable<E>>? include = null);

        /// <summary>
        /// Asynchronously retrieves a single entity matching the given filter, with optional eager loading.
        /// </summary>
        /// <param name="filter">An expression to filter the entity.</param>
        /// <param name="include">An optional function to include related entities.</param>
        /// <returns>A task that returns the matched entity or null.</returns>
        Task<E?> FindAsync(Expression<Func<E, bool>> filter, Func<IQueryable<E>, IQueryable<E>>? include = null);

        /// <summary>
        /// Asynchronously retrieves a paginated list of entities, with optional filtering and eager loading.
        /// </summary>
        /// <param name="pageNumber">The page number (starting at 1).</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="filter">An optional expression to filter the entities.</param>
        /// <param name="include">An optional function to include related entities.</param>
        /// <returns>A task that returns a list of entities.</returns>
        Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null, int pageNumber = 1, int pageSize = 10, Func<IQueryable<E>, IQueryable<E>>? include = null);

        /// <summary>
        /// Counts the total number of entities that match the filter.
        /// </summary>
        /// <param name="filter">An optional filter to count matching entities.</param>
        /// <returns>The number of matching entities.</returns>
        int Count(Expression<Func<E, bool>>? filter = null);

        /// <summary>
        /// Asynchronously counts the total number of entities that match the filter.
        /// </summary>
        /// <param name="filter">An optional filter to count matching entities.</param>
        /// <returns>A task that returns the number of matching entities.</returns>
        Task<int> CountAsync(Expression<Func<E, bool>>? filter = null);

        /// <summary>
        /// Determines whether any entity exists that matches the given filter.
        /// </summary>
        /// <param name="filter">A filter to match against entities.</param>
        /// <returns>True if any match exists, otherwise false.</returns>
        bool Exists(Expression<Func<E, bool>> filter);

        /// <summary>
        /// Asynchronously determines whether any entity exists that matches the given filter.
        /// </summary>
        /// <param name="filter">A filter to match against entities.</param>
        /// <returns>A task that returns true if a match exists, otherwise false.</returns>
        Task<bool> ExistsAsync(Expression<Func<E, bool>> filter);

        // -------------------- UPDATE --------------------

        /// <summary>
        /// Updates an existing entity in the context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(E entity);

        /// <summary>
        /// Asynchronously updates an existing entity in the context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(E entity);

        // -------------------- DELETE --------------------

        /// <summary>
        /// Removes a single entity from the context.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(E entity);

        /// <summary>
        /// Removes a collection of entities from the context.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        void RemoveRange(IEnumerable<E> entities);

        /// <summary>
        /// Asynchronously removes a single entity from the context.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveAsync(E entity);

        /// <summary>
        /// Asynchronously removes a collection of entities from the context.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveRangeAsync(IEnumerable<E> entities);

        // -------------------- SAVE / UNIT OF WORK --------------------

        /// <summary>
        /// Commits all pending changes to the database.
        /// </summary>
        void Save();

        /// <summary>
        /// Asynchronously commits all pending changes to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task SaveAsync();
    }
}
