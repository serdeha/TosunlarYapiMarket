using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class CustomerManager:ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Customer? customer)
        {
            if (customer != null)
            {
                _unitOfWork.Customers.Add(customer);
                _unitOfWork.SaveChanges();
            }
        }

        public void Update(Customer? customer)
        {
            if (customer != null)
            {
                _unitOfWork.Customers.Update(customer);
                _unitOfWork.SaveChanges();
            }
        }

        public void Delete(Customer? customer)
        {
            if (customer != null)
            {
                customer.IsActive = false;
                customer.IsDeleted = true;
                _unitOfWork.Customers.Delete(customer);
                _unitOfWork.SaveChanges();
            }
        }

        public void HardDelete(Customer? customer)
        {
            if (customer != null)
            {
                _unitOfWork.Customers.HardDelete(customer);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> AddAsync(Customer? customer)
        {
            if (customer != null)
            {
                await _unitOfWork.Customers.AddAsync(customer);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> UpdateAsync(Customer? customer)
        {
            if (customer != null)
            {
                await _unitOfWork.Customers.UpdateAsync(customer);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteAsync(Customer? customer)
        {
            if (customer != null)
            {
                customer.IsActive = false;
                customer.IsDeleted = true;
                await _unitOfWork.Customers.DeleteAsync(customer);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public  async Task<int> HardDeleteAsync(Customer? customer)
        {
            if (customer != null)
            {
                await _unitOfWork.Customers.HardDeleteAsync(customer);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public List<Customer>? GetAll(Expression<Func<Customer, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.Customers.GetAll() : _unitOfWork.Customers.GetAll(filter);
        }

        public List<Customer>? GetAllWithFilter(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties)
        {
            return _unitOfWork.Customers.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<Customer>>? GetAllWithFilterAsync(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties)
        {
            return await _unitOfWork.Customers.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public Customer? GetWithFilter(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties)
        {
            return _unitOfWork.Customers.GetWithFilter(predicate, includeProperties);
        }

        public async Task<Customer?> GetWithFilterAsync(Expression<Func<Customer, bool>>? predicate = null, params Expression<Func<Customer, object>>[] includeProperties)
        {
            return await _unitOfWork.Customers.GetWithFilterAsync(predicate, includeProperties);
        }

        public Customer? GetById(int customerId)
        {
            return _unitOfWork.Customers.GetById(x=>x.Id == customerId);
        }

        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            return await _unitOfWork.Customers.GetByIdAsync(x => x.Id == customerId);
        }

        public int GetCount(Expression<Func<Customer, bool>>? filter = null)
        {
            return _unitOfWork.Customers.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<Customer, bool>>? filter = null)
        {
            return await _unitOfWork.Customers.GetCountAsync(filter);
        }
    }
}
