using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface IStockBasketService
    {
        void Add(StockBasket stockBasket);
        void Update(StockBasket stockBasket);
        void Delete(StockBasket stockBasket);
        void HardDelete(StockBasket stockBasket);
        Task<int> AddAsync(StockBasket stockBasket);
        Task<int> UpdateAsync(StockBasket stockBasket);
        Task<int> DeleteAsync(StockBasket stockBasket);
        Task<int> HardDeleteAsync(StockBasket stockBasket);
        List<StockBasket>? GetAll(Expression<Func<StockBasket, bool>>? filter = null);
        List<StockBasket>? GetAllWithFilter(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties);
        Task<List<StockBasket>>? GetAllWithFilterAsync(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties);
        StockBasket? GetWithFilter(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties);
        Task<StockBasket?> GetWithFilterAsync(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties);
        StockBasket? GetById(int stockBasketId);
        Task<StockBasket?> GetByIdAsync(int stockBasketId);
        int GetCount(Expression<Func<StockBasket, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<StockBasket, bool>>? filter = null);
    }
}
