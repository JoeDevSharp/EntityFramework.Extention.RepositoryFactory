using JoeDevSharp.RepositoryFactory.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoeDevSharp.RepositoryFactory.EntityFramework.Core
{
    public class RepositoryFactory<C> : IDisposable where C : DbContext, new()
    {
        private readonly DbContext _context;
        private bool _disposed = false;

        public RepositoryFactory()
        {
            _context = new C();
        }

        public IGenericRepository<E> CreateRepository<E>() where E : class, IEntityBase
        {
            return new GenericRepository<E>(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _context.Dispose();
                _disposed = true;
            }
        }
    }
}
