using Microsoft.AspNetCore.Identity;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class AppUser:IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }

        public List<Customer>? Customers { get; set; }
        public List<Stock>? Stocks { get; set; }
        public List<Debt>? Debts { get; set; }
        public List<Note>? Notes { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public List<StockDetail>? StockDetails { get; set; }
    }
}
