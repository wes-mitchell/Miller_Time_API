using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace MillerTime.API.Repositories
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        protected readonly DbContext _context;

        public BaseRepository(IConfiguration configuration, DbContext context)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            this._context = context;
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual void ChangeEntryState<T>(T Entity, EntityState State)
        {
            _context.Entry((object)Entity).State = State;
        }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public virtual async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            await transaction.CommitAsync();
        }

        public virtual async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }

        public virtual void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public virtual void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
        }

        public virtual void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public virtual void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public virtual async Task<T> GetByIdAsync<T>(object id, bool tracking = true) where T : class
        {
            T entity = await _context.FindAsync<T>(new object[1] { id });
            if (!tracking)
            {
                ChangeEntryState(entity, EntityState.Detached);
            }

            return entity;
        }

        public virtual async Task<IList<T>> GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<IList<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate, bool tracking = true, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await query(predicate, tracking, includes).ToListAsync();
        }

        public virtual async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, bool tracking = true, params Expression<Func<T, object>>[] includes) where T : class
        {
            return await query(predicate, tracking, includes).FirstOrDefaultAsync();
        }

        private IQueryable<T> query<T>(Expression<Func<T, bool>> predicate, bool tracking = true, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> queryable = _context.Set<T>().Where(predicate);
            if (!tracking)
            {
                queryable = queryable.AsNoTracking();
            }

            return includes.Aggregate(queryable, (IQueryable<T> current, Expression<Func<T, object>> includeProperty) => current.Include(includeProperty));
        }

        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
