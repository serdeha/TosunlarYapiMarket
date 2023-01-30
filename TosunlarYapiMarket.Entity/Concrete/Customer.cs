using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class Customer:EntityBase,IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CustomerNo { get; set; }
        public string? TelephoneNumber { get; set; } = "-";
        public string? HomeAddress { get; set; } = "-";
        public string? BusinessAddress { get; set; } = "-";
        public decimal CustomerDebt { get; set; } = 0;

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public List<Debt>? Debts { get; set; }
        public List<Note>? Notes { get; set; }
    }
}
