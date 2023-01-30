using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class StockDetailManager : IStockDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockDetailManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                _unitOfWork.StockDetails.Add(stockDetail);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> AddAsync(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                await _unitOfWork.StockDetails.AddAsync(stockDetail);
                return await _unitOfWork.SaveChangesAsync();
            }
            
            return 0;
        }

        public void Delete(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                stockDetail.IsActive = false;
                stockDetail.IsDeleted = true;
                _unitOfWork.StockDetails.Delete(stockDetail);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> DeleteAsync(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                stockDetail.IsActive = false;
                stockDetail.IsDeleted = true;
                await _unitOfWork.StockDetails.DeleteAsync(stockDetail);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public List<StockDetail>? GetAll(Expression<Func<StockDetail, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.StockDetails.GetAll() : _unitOfWork.StockDetails.GetAll(filter);
        }

        public List<StockDetail>? GetAllWithFilter(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties)
        {
            return _unitOfWork.StockDetails.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<StockDetail>>? GetAllWithFilterAsync(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties)
        {
            return await _unitOfWork.StockDetails.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public StockDetail? GetById(int stockDetailId)
        {
            return _unitOfWork.StockDetails.GetById(x=>x.Id == stockDetailId);
        }

        public async Task<StockDetail?> GetByIdAsync(int stockDetailId)
        {
            return await _unitOfWork.StockDetails.GetByIdAsync(x => x.Id == stockDetailId);
        }

        public int GetCount(Expression<Func<StockDetail, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.StockDetails.GetCount() : _unitOfWork.StockDetails.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<StockDetail, bool>>? filter = null)
        {
            return filter == null ? await _unitOfWork.StockDetails.GetCountAsync() : await _unitOfWork.StockDetails.GetCountAsync(filter);
        }

        public StockDetail? GetWithFilter(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties)
        {
            return _unitOfWork.StockDetails.GetWithFilter(predicate, includeProperties);
        }

        public async Task<StockDetail?> GetWithFilterAsync(Expression<Func<StockDetail, bool>>? predicate = null, params Expression<Func<StockDetail, object>>[] includeProperties)
        {
            return await _unitOfWork.StockDetails.GetWithFilterAsync(predicate, includeProperties);
        }

        public void HardDelete(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                _unitOfWork.StockDetails.HardDelete(stockDetail);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> HardDeleteAsync(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                await _unitOfWork.StockDetails.HardDeleteAsync(stockDetail);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public void Update(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                _unitOfWork.StockDetails.Update(stockDetail);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> UpdateAsync(StockDetail stockDetail)
        {
            if(stockDetail != null)
            {
                await _unitOfWork.StockDetails.UpdateAsync(stockDetail);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }
    }
}
