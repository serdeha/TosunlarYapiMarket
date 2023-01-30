using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface IStockDetailService
    {
        void Add(StockDetail stockDetail);
        void Update(StockDetail stockDetail);
        void Delete(StockDetail stockDetail);
        void HardDelete(StockDetail stockDetail);
        Task<int> AddAsync(StockDetail stockDetail);
        Task<int> UpdateAsync(StockDetail stockDetail);
        Task<int> DeleteAsync(StockDetail stockDetail);
        Task<int> HardDeleteAsync(StockDetail stockDetail);
        List<StockDetail>? GetAll(Expression<Func<StockDetail, bool>>? filter = null);
        List<StockDetail>? GetAllWithFilter(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties);
        Task<List<StockDetail>>? GetAllWithFilterAsync(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties);
        StockDetail? GetWithFilter(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties);
        Task<StockDetail?> GetWithFilterAsync(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties);
        StockDetail? GetById(int stockDetailId);
        Task<StockDetail?> GetByIdAsync(int stockDetailId);
        int GetCount(Expression<Func<StockDetail, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<StockDetail, bool>>? filter = null);
    }
}
