using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class PayOffDebtManager : IPayOffDebtService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayOffDebtManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(PayOffDebt payOffDebt)
        {
            if (payOffDebt != null)
            {
                _unitOfWork.PayOffDebts.Add(payOffDebt);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> AddAsync(PayOffDebt payOffDebt)
        {
            if(payOffDebt != null)
            {
                await _unitOfWork.PayOffDebts.AddAsync(payOffDebt);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public void Delete(PayOffDebt payOffDebt)
        {
            if(payOffDebt != null)
            {
                payOffDebt.IsDeleted = true;
                payOffDebt.IsActive = false;
                _unitOfWork.PayOffDebts.Update(payOffDebt);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> DeleteAsync(PayOffDebt payOffDebt)
        {
            if(payOffDebt != null)
            {
                payOffDebt.IsActive = false;
                payOffDebt.IsDeleted = true;
                await _unitOfWork.PayOffDebts.UpdateAsync(payOffDebt);
                return await _unitOfWork.SaveChangesAsync();
            }
            return 0;
        }

        public List<PayOffDebt>? GetAll(Expression<Func<PayOffDebt, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.PayOffDebts.GetAll() : _unitOfWork.PayOffDebts.GetAll(filter);
        }

        public List<PayOffDebt>? GetAllWithFilter(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties)
        {
            return _unitOfWork.PayOffDebts.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<PayOffDebt>>? GetAllWithFilterAsync(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties)
        {
            return await _unitOfWork.PayOffDebts.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public PayOffDebt? GetById(int payOffDebtId)
        {
            return _unitOfWork.PayOffDebts.GetById(x=>x.Id == payOffDebtId);
        }

        public async Task<PayOffDebt?> GetByIdAsync(int payOffDebtId)
        {
            return await _unitOfWork.PayOffDebts.GetByIdAsync(x=>x.Id == payOffDebtId);
        }

        public int GetCount(Expression<Func<PayOffDebt, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.PayOffDebts.GetCount() : _unitOfWork.PayOffDebts.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<PayOffDebt, bool>>? filter = null)
        {
            return await _unitOfWork.PayOffDebts.GetCountAsync(filter);
        }

        public PayOffDebt? GetWithFilter(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties)
        {
            return _unitOfWork.PayOffDebts.GetWithFilter(predicate, includeProperties); 
        }

        public async Task<PayOffDebt?> GetWithFilterAsync(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties)
        {
            return await _unitOfWork.PayOffDebts.GetWithFilterAsync(predicate, includeProperties);
        }

        public void HardDelete(PayOffDebt payOffDebt)
        {
            if(payOffDebt != null)
            {
                _unitOfWork.PayOffDebts.HardDelete(payOffDebt);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> HardDeleteAsync(PayOffDebt payOffDebt)
        {
            if(payOffDebt != null)
            {
                await _unitOfWork.PayOffDebts.HardDeleteAsync(payOffDebt);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public void Update(PayOffDebt payOffDebt)
        {
            if(payOffDebt != null)
            {
                _unitOfWork.PayOffDebts.Update(payOffDebt);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> UpdateAsync(PayOffDebt payOffDebt)
        {
            if(payOffDebt != null)
            {
                await _unitOfWork.PayOffDebts.UpdateAsync(payOffDebt);
                return await _unitOfWork.SaveChangesAsync();
            }
            return 0;
        }
    }
}
