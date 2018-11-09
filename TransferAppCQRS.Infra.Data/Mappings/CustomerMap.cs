using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Infra.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("uid_customer");

            builder.Property(c => c.Name)
                .HasColumnName("name");

            builder.Property(c => c.Email)
                .HasColumnName("email");

            builder.Property(c => c.BirthDate)
                .HasColumnName("birthdate");
            
            builder.Ignore(c => c.Account);
            
            builder.ToTable("tbl_customer");
        }
    }
}
