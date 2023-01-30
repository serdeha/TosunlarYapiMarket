using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Data.Concrete.EntityFramework.Configurations
{
    public class NoteConfiguration:IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
           builder.HasKey(x => x.Id);
           builder.Property(x => x.NoteTitle).IsRequired();
           builder.Property(x => x.NoteDescription).IsRequired();
           builder.Property(x => x.NoteDate).IsRequired();
           builder.HasOne(x => x.Customer).WithMany(x => x.Notes).HasForeignKey(x => x.CustomerId);
           builder.HasOne(x => x.AppUser).WithMany(x => x.Notes).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
