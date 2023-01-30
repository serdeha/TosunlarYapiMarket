using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface IPayOffDebtService
    {
        void Add(PayOffDebt payOffDebt);
        void Update(PayOffDebt payOffDebt);
        void Delete(PayOffDebt payOffDebt);
        void HardDelete(PayOffDebt payOffDebt);
        Task<int> AddAsync(PayOffDebt payOffDebt);
        Task<int> UpdateAsync(PayOffDebt payOffDebt);
        Task<int> DeleteAsync(PayOffDebt payOffDebt);
        Task<int> HardDeleteAsync(PayOffDebt payOffDebt);
        List<PayOffDebt>? GetAll(Expression<Func<PayOffDebt, bool>>? filter = null);
        List<PayOffDebt>? GetAllWithFilter(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties);
        Task<List<PayOffDebt>>? GetAllWithFilterAsync(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties);
        PayOffDebt? GetWithFilter(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties);
        Task<PayOffDebt?> GetWithFilterAsync(Expression<Func<PayOffDebt, bool>>? predicate = null, params Expression<Func<PayOffDebt, object>>[] includeProperties);
        PayOffDebt? GetById(int payOffDebtId);
        Task<PayOffDebt?> GetByIdAsync(int payOffDebtId);
        int GetCount(Expression<Func<PayOffDebt, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<PayOffDebt, bool>>? filter = null);
    }
}
