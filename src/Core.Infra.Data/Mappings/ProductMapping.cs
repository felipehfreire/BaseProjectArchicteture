using Core.Infra.Data.Extensions;
using Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;

namespace Core.Infra.Data.Mappings
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public override void Map(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCT_TB");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Description)
              .HasColumnType("varchar(500)")
              .IsRequired();
            // not map
            builder.Ignore(e => e.ValidationResult);
            //to igoner cascade mode validantions
            builder.Ignore(e => e.CascadeMode);
            builder.Ignore(p => p.Type);
            //one-to-mnay
            builder.HasOne(e => e.Category)
             .WithMany(o => o.Products)
             .HasForeignKey(e => e.CategoryId);

        }
    }
}
