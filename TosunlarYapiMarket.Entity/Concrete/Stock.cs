using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class Stock : EntityBase, IEntity
    {
        public string? StockName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal KDV { get; set; } = 0;
        public double StockAnyDetail { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
        public StockDetail? StockDetail { get; set; }
        public int StockDetailId { get; set; }
        public List<StockBasket>? StockBaskets { get; set; }
    }
}
