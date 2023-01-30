using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Configurations
{
    public class PayOffDebtConfiguration : IEntityTypeConfiguration<PayOffDebt>
    {
        public void Configure(EntityTypeBuilder<PayOffDebt> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.AmountPaid).IsRequired();
            builder.Property(x => x.PaidDate).IsRequired();
            builder.HasOne(x => x.Debt).WithMany(x => x.PayOffDebts).HasForeignKey(x=>x.DebtId);
        }
    }
}
