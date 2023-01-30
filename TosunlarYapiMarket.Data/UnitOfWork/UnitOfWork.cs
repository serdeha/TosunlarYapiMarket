using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories;

namespace TosunlarYapiMarket.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TosunlarYapiMarketDbContext _context;
        private EfCustomerRepository? _customerRepository;
        private EfDebtRepository? _debtRepository;
        private EfInvoiceRepository? _invoiceRepository;
        private EfNoteRepository? _noteRepository;
        private EfStockRepository? _stockRepository;
        private EfPayOfDebtRepository? _payOfDebtRepository;
        private EfStockDetailRepository? _stockDetailRepository;
        private EfStockBasketRepository? _stockBasketRepository;

        public UnitOfWork(TosunlarYapiMarketDbContext context)
        {
            _context = context;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        public ICustomerRepository Customers => _customerRepository ??= new EfCustomerRepository(_context);
        public IDebtRepository Debts => _debtRepository ??= new EfDebtRepository(_context);
        public IInvoiceRepository Invoices => _invoiceRepository ??= new EfInvoiceRepository(_context);
        public INoteRepository Notes => _noteRepository ??= new EfNoteRepository(_context);
        public IStockRepository Stocks => _stockRepository ??= new EfStockRepository(_context);
        public IPayOffDebtRepository PayOffDebts => _payOfDebtRepository ??= new EfPayOfDebtRepository(_context);
        public IStockDetailRepository StockDetails => _stockDetailRepository ??= new EfStockDetailRepository(_context);
        public IStockBasketRepository StockBaskets => _stockBasketRepository ??= new EfStockBasketRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
