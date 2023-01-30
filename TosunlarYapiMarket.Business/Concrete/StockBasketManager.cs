using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class StockBasketManager : IStockBasketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockBasketManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }

        public void Delete(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }

        public List<StockBasket>? GetAll(Expression<Func<StockBasket, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.StockBaskets.GetAll() : _unitOfWork.StockBaskets.GetAll(filter);
        }

        public List<StockBasket>? GetAllWithFilter(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties)
        {
            return _unitOfWork.StockBaskets.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<StockBasket>>? GetAllWithFilterAsync(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties)
        {
            return await _unitOfWork.StockBaskets.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public StockBasket? GetById(int stockBasketId)
        {
            return _unitOfWork.StockBaskets.GetById(x=>x.Id == stockBasketId);
        }

        public async Task<StockBasket?> GetByIdAsync(int stockBasketId)
        {
            return await _unitOfWork.StockBaskets.GetByIdAsync(x => x.Id == stockBasketId);
        }

        public int GetCount(Expression<Func<StockBasket, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.StockBaskets.GetCount() : _unitOfWork.StockBaskets.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<StockBasket, bool>>? filter = null)
        {
            return filter == null ? await _unitOfWork.StockBaskets.GetCountAsync() : await _unitOfWork.StockBaskets.GetCountAsync(filter);
        }

        public StockBasket? GetWithFilter(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties)
        {
            return _unitOfWork.StockBaskets.GetWithFilter(predicate, includeProperties);
        }

        public async Task<StockBasket?> GetWithFilterAsync(Expression<Func<StockBasket, bool>>? predicate = null, params Expression<Func<StockBasket, object>>[] includeProperties)
        {
            return await _unitOfWork.StockBaskets.GetWithFilterAsync(predicate, includeProperties);
        }

        public void HardDelete(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }

        public Task<int> HardDeleteAsync(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }

        public void Update(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(StockBasket stockBasket)
        {
            throw new NotImplementedException();
        }
    }
}
