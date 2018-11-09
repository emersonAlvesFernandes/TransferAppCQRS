using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Infra.Data.Mappings
{
    public class TransferMap : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("uid_transfer");

            //builder.Property(c => c.Origin.Id)
            //    .HasColumnName("uid_conta_origem");

            //builder.Property(c => c.Recipient.Id)
            //    .HasColumnName("uid_conta_destino");
            builder.Ignore(c => c.Origin);
            builder.Ignore(c => c.Recipient);
                
            builder.Property(c => c.Description)
                .HasColumnName("ds_descricao");

            builder.Property(c => c.Value)
                .HasColumnName("ScheduledDate");

            builder.Property(c => c.Value)
                .HasColumnName("dt_transacao");
            
            builder.ToTable("tbl_transfer");
        }
    }
}
