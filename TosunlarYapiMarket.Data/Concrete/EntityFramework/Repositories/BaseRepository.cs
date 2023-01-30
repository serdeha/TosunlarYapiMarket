using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity:class,IEntity,new()
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void HardDelete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _context.Set<TEntity>().Update(entity);
            });
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _context.Set<TEntity>().Update(entity);
            });
        }

        public async Task HardDeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _context.Set<TEntity>().Remove(entity);
            });
        }

        public List<TEntity>? GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null ? _context.Set<TEntity>().ToList() : _context.Set<TEntity>().Where(filter).ToList();
        }

        public List<TEntity>? GetAllWithFilter(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.ToList();
        }

        public async Task<List<TEntity>>? GetAllWithFilterAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public TEntity? GetWithFilter(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }

            return query.SingleOrDefault();
        }

        public async Task<TEntity?> GetWithFilterAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }

            return await query.SingleOrDefaultAsync();
        }

        public TEntity? GetById(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null
                ? _context.Set<TEntity>().SingleOrDefault()
                : _context.Set<TEntity>().Where(filter).SingleOrDefault();
        }

        public async Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null
                ? await _context.Set<TEntity>().SingleOrDefaultAsync()
                : await _context.Set<TEntity>().Where(filter).SingleOrDefaultAsync();
        }

        public int GetCount(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null ? _context.Set<TEntity>().Count() : _context.Set<TEntity>().Where(filter).Count();
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null
                ? await _context.Set<TEntity>().CountAsync()
                : await _context.Set<TEntity>().Where(filter).CountAsync();
        }
    }
}
