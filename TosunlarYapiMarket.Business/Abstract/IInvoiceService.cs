using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface IInvoiceService
    {
        void Add(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(Invoice invoice);
        void HardDelete(Invoice invoice);
        Task<int> AddAsync(Invoice invoice);
        Task<int> UpdateAsync(Invoice invoice);
        Task<int> DeleteAsync(Invoice invoice);
        Task<int> HardDeleteAsync(Invoice invoice);
        List<Invoice>? GetAll(Expression<Func<Invoice, bool>>? filter = null);
        List<Invoice>? GetAllWithFilter(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties);
        Task<List<Invoice>>? GetAllWithFilterAsync(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties);
        Invoice? GetWithFilter(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties);
        Task<Invoice?> GetWithFilterAsync(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties);
        Invoice? GetById(int invoiceId);
        Task<Invoice?> GetByIdAsync(int invoiceId);
        int GetCount(Expression<Func<Invoice, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<Invoice, bool>>? filter = null);
    }
}
