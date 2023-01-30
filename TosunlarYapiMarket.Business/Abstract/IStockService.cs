using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface IStockService
    {
        void Add(Stock stock);
        void Update(Stock stock);
        void Delete(Stock stock);
        void HardDelete(Stock stock);
        Task<int> AddAsync(Stock stock);
        Task<int> UpdateAsync(Stock stock);
        Task<int> DeleteAsync(Stock stock);
        Task<int> HardDeleteAsync(Stock stock);
        List<Stock>? GetAll(Expression<Func<Stock, bool>>? filter = null);
        List<Stock>? GetAllWithFilter(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties);
        Task<List<Stock>>? GetAllWithFilterAsync(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties);
        Stock? GetWithFilter(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties);
        Task<Stock?> GetWithFilterAsync(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties);
        Stock? GetById(int stockId);
        Task<Stock?> GetByIdAsync(int stockId);
        int GetCount(Expression<Func<Stock, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<Stock, bool>>? filter = null);
    }
}
