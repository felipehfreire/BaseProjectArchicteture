using Core.Infra.Data.Extensions;
using Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Infra.Data.Mappings
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public override void Map(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CATEGORY_TB");
            builder.HasKey(e => e.Id);
                

            builder.Property(e => e.Name)
              .HasColumnType("varchar(200)")
              .IsRequired();
            // not map
            builder.Ignore(e => e.ValidationResult);
            //to igoner cascade mode validantions
            builder.Ignore(e => e.CascadeMode);
            
           

        }
    }
}
