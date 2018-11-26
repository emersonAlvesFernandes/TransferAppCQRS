using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Infra.Data.Mappings
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("uid_account")
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.Agency)
                .HasColumnName("agency");

            builder.Property(x => x.Number)
                .HasColumnName("number");

            builder.Property(x => x.Address)
                .HasColumnName("address");

            builder.Property(x => x.CustomerGuId)
                .HasColumnName("uid_customer");

            builder
                .HasOne(a => a.Customer)
                .WithOne(b => b.Account)
                .HasForeignKey<Account>(a => a.CustomerGuId);

            builder.ToTable("tbl_account");

        }
    }
}
