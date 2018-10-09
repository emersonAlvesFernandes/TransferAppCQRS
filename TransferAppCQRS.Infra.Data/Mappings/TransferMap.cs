using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Infra.Data.Mappings
{
    public class TransferMap : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("uid_movimentacao_transferencia")
                .HasColumnType("uniqueidentifier")
                .IsRequired(); ;

            builder.Property(c => c.Origin.Id)
                .HasColumnName("uid_conta_origem")
                .HasColumnType("uniqueidentifier")                
                .IsRequired();

            builder.Property(c => c.Recipient.Id)
                .HasColumnName("uid_conta_destino")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Value)                                
                .IsRequired();

            builder.Property(c => c.ScheduledDate)
                .IsRequired();

            builder.ToTable("tbl_movimentacao_transferencia");
        }
    }
}
