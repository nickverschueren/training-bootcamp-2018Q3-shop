using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Api.Data.Model.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product.HasKey(p => p.Id);

            product.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            product.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            product.Property(p => p.Description)
                .HasMaxLength(1000);

            product.Property(p => p.BasePrice)
                .IsRequired();

            product.Property(p => p.Price)
                .IsRequired();

            product.Property(p => p.ImageUri)
                .HasMaxLength(500);

            var stock = product.OwnsOne(p => p.Stock);

            stock.Property(p => p.Total)
                .IsRequired();

            stock.Property(p => p.Reserved)
                .IsRequired();
        }
    }
}
