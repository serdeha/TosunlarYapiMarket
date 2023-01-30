using Microsoft.EntityFrameworkCore;
using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories
{
    public class EfPayOfDebtRepository : BaseRepository<PayOffDebt>, IPayOffDebtRepository
    {
        public EfPayOfDebtRepository(TosunlarYapiMarketDbContext context) : base(context)
        {
        }
    }
}
