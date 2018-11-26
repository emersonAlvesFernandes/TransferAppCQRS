using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Infra.Data.Mappings
{
    public class TransferMap : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("uid_transfer");

            builder.Property(c => c.OriginGuid)
                .HasColumnName("uid_originAccountId");
            builder.Ignore(c => c.Origin);

            builder.Property(c => c.RecipientGuid)
                .HasColumnName("uid_recipientAccountId");
            builder.Ignore(c => c.Recipient);
                
            builder.Property(c => c.Description)
                .HasColumnName("ds_description");

            builder.Property(c => c.ScheduledDate)
                .HasColumnName("ScheduledDate");

            builder.Property(c => c.Value)
                .HasColumnName("value");

            builder.ToTable("tbl_transfer");
        }
    }
}
