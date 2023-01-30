using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Configurations;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Context
{
    public class TosunlarYapiMarketDbContext : IdentityDbContext<AppUser, AppRole, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public TosunlarYapiMarketDbContext(DbContextOptions<TosunlarYapiMarketDbContext> options) : base(options)
        {

        }

        public DbSet<Customer>? Customer { get; set; }
        public DbSet<Debt>? Debt { get; set; }
        public DbSet<PayOffDebt>? PayOffDebt { get; set; }
        public DbSet<Invoice>? Invoice { get; set; }
        public DbSet<Note>? Note { get; set; }
        public DbSet<Stock>? Stock { get; set; }
        public DbSet<StockDetail>? StockDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new DebtConfiguration());
            builder.ApplyConfiguration(new InvoiceConfiguration());
            builder.ApplyConfiguration(new NoteConfiguration());
            builder.ApplyConfiguration(new StockConfiguration());
            builder.ApplyConfiguration(new RoleClaimConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserLoginConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserTokenConfiguration());
        }
    }
}
