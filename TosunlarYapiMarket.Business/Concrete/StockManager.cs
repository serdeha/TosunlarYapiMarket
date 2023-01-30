using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class StockManager:IStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Stock? stock)
        {
            if (stock != null)
            {
                _unitOfWork.Stocks.Add(stock);
                _unitOfWork.SaveChanges();
            }
        }

        public void Update(Stock? stock)
        {
            if (stock != null)
            {
                _unitOfWork.Stocks.Update(stock);
                _unitOfWork.SaveChanges();
            }
        }

        public void Delete(Stock? stock)
        {
            if (stock != null)
            {
                stock.IsActive = false;
                stock.IsDeleted = true;
                _unitOfWork.Stocks.Delete(stock);
                _unitOfWork.SaveChanges();
            }
        }

        public void HardDelete(Stock? stock)
        {
            if (stock != null)
            {
                _unitOfWork.Stocks.HardDelete(stock);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> AddAsync(Stock? stock)
        {
            if (stock != null)
            {
                await _unitOfWork.Stocks.AddAsync(stock);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> UpdateAsync(Stock? stock)
        {
            if (stock != null)
            {
                await _unitOfWork.Stocks.UpdateAsync(stock);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteAsync(Stock? stock)
        {
            if (stock != null)
            {
                stock.IsActive = false;
                stock.IsDeleted = true;
                await _unitOfWork.Stocks.DeleteAsync(stock);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> HardDeleteAsync(Stock? stock)
        {
            if (stock != null)
            {
                await _unitOfWork.Stocks.HardDeleteAsync(stock);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public List<Stock>? GetAll(Expression<Func<Stock, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.Stocks.GetAll() : _unitOfWork.Stocks.GetAll(filter);
        }

        public List<Stock>? GetAllWithFilter(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties)
        {
            return _unitOfWork.Stocks.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<Stock>>? GetAllWithFilterAsync(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties)
        {
            return await _unitOfWork.Stocks.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public Stock? GetWithFilter(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties)
        {
            return _unitOfWork.Stocks.GetWithFilter(predicate, includeProperties);
        }

        public async Task<Stock?> GetWithFilterAsync(Expression<Func<Stock, bool>>? predicate = null, params Expression<Func<Stock, object>>[] includeProperties)
        {
            return await _unitOfWork.Stocks.GetWithFilterAsync(predicate, includeProperties);
        }

        public Stock? GetById(int stockId)
        {
            return _unitOfWork.Stocks.GetById(x => x.Id == stockId);
        }

        public async Task<Stock?> GetByIdAsync(int stockId)
        {
            return await _unitOfWork.Stocks.GetByIdAsync(x => x.Id == stockId);
        }

        public int GetCount(Expression<Func<Stock, bool>>? filter = null)
        {
            return _unitOfWork.Stocks.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<Stock, bool>>? filter = null)
        {
            return await _unitOfWork.Stocks.GetCountAsync(filter);
        }
    }
}
