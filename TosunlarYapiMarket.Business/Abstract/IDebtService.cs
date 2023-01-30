using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface IDebtService
    {
        void Add(Debt debt);
        void Update(Debt debt);
        void Delete(Debt debt);
        void HardDelete(Debt debt);
        Task<int> AddAsync(Debt debt);
        Task<int> UpdateAsync(Debt debt);
        Task<int> DeleteAsync(Debt debt);
        Task<int> HardDeleteAsync(Debt debt);
        List<Debt>? GetAll(Expression<Func<Debt, bool>>? filter = null);
        List<Debt>? GetAllWithFilter(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties);
        Task<List<Debt>>? GetAllWithFilterAsync(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties);
        Debt? GetWithFilter(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties);
        Task<Debt?> GetWithFilterAsync(Expression<Func<Debt, bool>>? predicate = null, params Expression<Func<Debt, object>>[] includeProperties);
        Debt? GetById(int debtId);
        Task<Debt?> GetByIdAsync(int debtId);
        int GetCount(Expression<Func<Debt, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<Debt, bool>>? filter = null);
    }
}
