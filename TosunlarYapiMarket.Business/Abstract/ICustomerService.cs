using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface ICustomerService
    {
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        void HardDelete(Customer customer);
        Task<int> AddAsync(Customer customer);
        Task<int> UpdateAsync(Customer customer);
        Task<int> DeleteAsync(Customer customer);
        Task<int> HardDeleteAsync(Customer customer);
        List<Customer>? GetAll(Expression<Func<Customer, bool>>? filter = null);
        List<Customer>? GetAllWithFilter(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties);
        Task<List<Customer>>? GetAllWithFilterAsync(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties);
        Customer? GetWithFilter(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties);
        Task<Customer?> GetWithFilterAsync(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties);
        Customer? GetById(int customerId);
        Task<Customer?> GetByIdAsync(int customerId);
        int GetCount(Expression<Func<Customer, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<Customer, bool>>? filter = null);
    }
}
