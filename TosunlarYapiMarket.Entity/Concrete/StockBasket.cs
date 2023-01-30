using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class StockBasket:IEntity
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock? Stock { get; set; }
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public string? StockName { get; set; }
        public double StockAnyDetail { get; set; } = 0;
        public decimal StockPrice { get; set; } = 0;
    }
}
