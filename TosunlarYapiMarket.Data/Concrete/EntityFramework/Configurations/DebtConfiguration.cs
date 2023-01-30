using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Configurations
{
    public class DebtConfiguration:IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PaymentAmount).IsRequired();
            builder.Property(x => x.PaymentDate).IsRequired();
            builder.HasOne(x => x.Customer).WithMany(x => x.Debts).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Debts).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(x => x.PayOffDebts).WithOne(x => x.Debt).HasForeignKey(x => x.DebtId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
