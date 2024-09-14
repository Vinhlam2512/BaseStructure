using System.Linq.Expressions;
using ERP.Domain.Abstractions.Entities;
using ERP.Domain.Abstractions.Repositories;
using ERP.Share.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace ERP.Persistence.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
        where TEntity : EntityBase<TKey>
{

    private readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
        => _context = context;


    public void Dispose()
        => _context?.Dispose();

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
       params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = _context.Set<TEntity>()
                                        .AsNoTracking()
                                        .Where(e => !EF.Property<bool>(e, "IsDeleted"));
        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties)
            {
                items = items.Include(includeProperty);
            }
        }

        if (predicate is not null)
        {
            items = items.Where(predicate);
        }

        return items;
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);

    public async Task Add(TEntity entity)
        => await _context.AddAsync(entity);

    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

    public void RemoveMultiple(List<TEntity> entities)
        => _context.Set<TEntity>().RemoveRange(entities);

    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public async Task<string> GenerateCode(Func<TEntity, string> codeSelector, Prefix prefix)
    {
        string prefixString = prefix.ToString();
        string datePart = DateTime.Now.ToString("yyyyMMdd");
        string fullPrefix = prefixString + datePart;
        int counter = 1;

        var allRecords = await _context.Set<TEntity>().ToListAsync();

        var lastRecord = allRecords
            .Where(e => codeSelector(e).StartsWith(prefixString + datePart))
            .OrderByDescending(e => codeSelector(e))
            .FirstOrDefault();

        if (lastRecord != null)
        {
            string lastCode = codeSelector(lastRecord);
            string lastCounterStr = lastCode.Substring(prefixString.Length + datePart.Length);
            if (int.TryParse(lastCounterStr, out int lastCounter))
            {
                counter = lastCounter + 1;
            }
        }

        return $"{prefix}{datePart}{counter:D4}";
    }
}

