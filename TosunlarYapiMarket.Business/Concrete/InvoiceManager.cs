using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class InvoiceManager:IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Invoice? invoice)
        {
            if (invoice != null)
            {
                _unitOfWork.Invoices.Add(invoice);
                _unitOfWork.SaveChanges();
            }
        }

        public void Update(Invoice? invoice)
        {
            if (invoice != null)
            {
                _unitOfWork.Invoices.Update(invoice);
                _unitOfWork.SaveChanges();
            }
        }

        public void Delete(Invoice? invoice)
        {
            if (invoice != null)
            {
                invoice.IsDeleted = true;
                invoice.IsActive = false;
                _unitOfWork.Invoices.Delete(invoice);
                _unitOfWork.SaveChanges();
            }
        }

        public void HardDelete(Invoice? invoice)
        {
            if (invoice != null)
            {
                _unitOfWork.Invoices.HardDelete(invoice);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> AddAsync(Invoice? invoice)
        {
            if (invoice != null)
            {
                await _unitOfWork.Invoices.AddAsync(invoice);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> UpdateAsync(Invoice? invoice)
        {
            if (invoice != null)
            {
                await _unitOfWork.Invoices.UpdateAsync(invoice);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteAsync(Invoice? invoice)
        {
            if (invoice != null)
            {
                invoice.IsActive = false;
                invoice.IsDeleted = true;
                await _unitOfWork.Invoices.DeleteAsync(invoice);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> HardDeleteAsync(Invoice? invoice)
        {
            if (invoice != null)
            {
                await _unitOfWork.Invoices.HardDeleteAsync(invoice);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public List<Invoice>? GetAll(Expression<Func<Invoice, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.Invoices.GetAll() : _unitOfWork.Invoices.GetAll(filter);
        }

        public List<Invoice>? GetAllWithFilter(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties)
        {
            return _unitOfWork.Invoices.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<Invoice>>? GetAllWithFilterAsync(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties)
        {
            return await _unitOfWork.Invoices.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public Invoice? GetWithFilter(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties)
        {
            return _unitOfWork.Invoices.GetWithFilter(predicate, includeProperties);
        }

        public async Task<Invoice?> GetWithFilterAsync(Expression<Func<Invoice, bool>>? predicate = null, params Expression<Func<Invoice, object>>[] includeProperties)
        {
            return await _unitOfWork.Invoices.GetWithFilterAsync(predicate, includeProperties);
        }

        public Invoice? GetById(int invoiceId)
        {
            return _unitOfWork.Invoices.GetById(x => x.Id == invoiceId);
        }

        public async Task<Invoice?> GetByIdAsync(int invoiceId)
        {
            return await _unitOfWork.Invoices.GetByIdAsync(x => x.Id == invoiceId);
        }

        public int GetCount(Expression<Func<Invoice, bool>>? filter = null)
        {
            return _unitOfWork.Invoices.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<Invoice, bool>>? filter = null)
        {
            return await _unitOfWork.Invoices.GetCountAsync(filter);
        }
    }
}
