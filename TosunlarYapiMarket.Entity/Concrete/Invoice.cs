using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class Invoice : EntityBase, IEntity
    {
        public string? InvoiceCode { get; set; }
        public string? InvoiceDescription { get; set; } = "-";
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public int Quantity { get; set; } = 0;
        public double Ton { get; set; } = 0;
        public double Meter { get; set; } = 0;
        public double SquareMeter { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public decimal DiscountedTotalPrice { get; set; } = 0;
        public decimal DiscountPrice { get; set; } = 0;
        public bool IsPaid { get; set; } = false;

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public List<StockBasket>? StockBaskets { get; set; }
    }
}
