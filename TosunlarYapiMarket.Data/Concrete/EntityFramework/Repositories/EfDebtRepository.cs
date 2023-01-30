using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TosunlarYapiMarket.Data.Abstract;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Repositories
{
    public class EfDebtRepository:BaseRepository<Debt>,IDebtRepository
    {
        public EfDebtRepository(TosunlarYapiMarketDbContext context) : base(context)
        {
        }
    }
}
