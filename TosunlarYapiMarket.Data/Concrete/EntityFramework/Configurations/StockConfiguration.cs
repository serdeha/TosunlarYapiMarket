using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StockName).IsRequired();
            builder.HasOne(x => x.AppUser).WithMany(x => x.Stocks).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.StockDetail).WithMany(x => x.Stocks).HasForeignKey(x => x.StockDetailId);
            builder.HasMany(x => x.StockBaskets).WithOne(x => x.Stock).HasForeignKey(x => x.StockId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
