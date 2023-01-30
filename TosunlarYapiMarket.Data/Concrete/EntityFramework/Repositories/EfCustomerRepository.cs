using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories
{
    public class EfCustomerRepository:BaseRepository<Customer>,ICustomerRepository
    {
        public EfCustomerRepository(TosunlarYapiMarketDbContext context) : base(context)
        {
        }
    }
}
