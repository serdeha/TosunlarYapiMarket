using TosunlarYapiMarket.Data.Abstract;

namespace TosunlarYapiMarket.Data.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IStockDetailRepository StockDetails { get; }
        ICustomerRepository Customers { get; }
        IDebtRepository Debts { get; }
        IPayOffDebtRepository PayOffDebts { get; }
        IInvoiceRepository Invoices { get; }
        INoteRepository Notes { get; }
        IStockRepository Stocks { get; }
        IStockBasketRepository StockBaskets { get; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
