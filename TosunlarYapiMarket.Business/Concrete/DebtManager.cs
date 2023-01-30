using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class DebtManager:IDebtService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DebtManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Debt? debt)
        {
            if (debt != null)
            {
                _unitOfWork.Debts.Add(debt);
                _unitOfWork.SaveChanges();
            }
        }

        public void Update(Debt? debt)
        {
            if (debt != null)
            {
                _unitOfWork.Debts.Update(debt);
                _unitOfWork.SaveChanges();
            }
        }

        public void Delete(Debt? debt)
        {
            if (debt != null)
            {
                debt.IsActive = false;
                debt.IsDeleted = true;
                _unitOfWork.Debts.Delete(debt);
                _unitOfWork.SaveChanges();
            }
        }

        public void HardDelete(Debt? debt)
        {
            if (debt != null)
            {
                _unitOfWork.Debts.HardDelete(debt);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> AddAsync(Debt? debt)
        {
            if (debt != null)
            {
                await _unitOfWork.Debts.AddAsync(debt);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> UpdateAsync(Debt? debt)
        {
            if (debt != null)
            {
                await _unitOfWork.Debts.UpdateAsync(debt);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteAsync(Debt? debt)
        {
            if (debt != null)
            {
                debt.IsActive = false;
                debt.IsDeleted = true;
                await _unitOfWork.Debts.DeleteAsync(debt);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> HardDeleteAsync(Debt? debt)
        {
            if (debt != null)
            {
                await _unitOfWork.Debts.HardDeleteAsync(debt);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public List<Debt>? GetAll(Expression<Func<Debt, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.Debts.GetAll() : _unitOfWork.Debts.GetAll(filter);
        }

        public List<Debt>? GetAllWithFilter(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties)
        {
            return _unitOfWork.Debts.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<Debt>>? GetAllWithFilterAsync(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties)
        {
            return await _unitOfWork.Debts.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public Debt? GetWithFilter(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties)
        {
            return _unitOfWork.Debts.GetWithFilter(predicate, includeProperties);
        }

        public async Task<Debt?> GetWithFilterAsync(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties)
        {
            return await _unitOfWork.Debts.GetWithFilterAsync(predicate, includeProperties);
        }

        public Debt? GetById(int debtId)
        {
            return _unitOfWork.Debts.GetById(x => x.Id == debtId);
        }

        public async Task<Debt?> GetByIdAsync(int debtId)
        {
            return await _unitOfWork.Debts.GetByIdAsync(x => x.Id == debtId);
        }

        public int GetCount(Expression<Func<Debt, bool>>? filter = null)
        {
            return _unitOfWork.Debts.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<Debt, bool>>? filter = null)
        {
            return await _unitOfWork.Debts.GetCountAsync(filter);
        }
    }
}
