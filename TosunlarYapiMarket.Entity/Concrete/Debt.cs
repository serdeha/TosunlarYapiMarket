using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class Debt:EntityBase,IEntity
    {
        public decimal TotalAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public int? InvoiceId { get; set; }
        
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }

        public List<PayOffDebt>? PayOffDebts { get; set; }
    }
}
