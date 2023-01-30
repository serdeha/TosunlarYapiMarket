using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories
{
    public class EfStockRepository:BaseRepository<Stock>,IStockRepository
    {
        public EfStockRepository(TosunlarYapiMarketDbContext context) : base(context)
        {
        }
    }
}
