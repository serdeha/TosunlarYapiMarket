using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class StockDetail:EntityBase,IEntity
    {
        public string? StockDetailName { get; set; }        
        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
        public List<Stock>? Stocks { get; set; }
    }
}
