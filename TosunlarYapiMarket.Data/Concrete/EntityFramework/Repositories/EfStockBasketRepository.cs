using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories
{
    public class EfStockBasketRepository : BaseRepository<StockBasket>, IStockBasketRepository
    {
        public EfStockBasketRepository(TosunlarYapiMarketDbContext context) : base(context)
        {
        }
    }
}
