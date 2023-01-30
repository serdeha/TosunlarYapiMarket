using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Configurations
{
    public class StockDetailConfiguration : IEntityTypeConfiguration<StockDetail>
    {
        public void Configure(EntityTypeBuilder<StockDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StockDetailName).IsRequired();
            builder.HasIndex(x => x.StockDetailName).IsUnique();
            builder.HasMany(x => x.Stocks).WithOne(x => x.StockDetail).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
