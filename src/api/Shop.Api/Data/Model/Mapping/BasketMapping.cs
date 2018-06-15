using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Api.Data.Model.Mapping
{
    public class BasketMapping : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> basket)
        {
            basket.HasKey(b => b.Id);

            basket.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            basket.Property(b => b.Created)
                .IsRequired();

            basket.Property(b => b.LastUpdated)
                .IsRequired();

            basket.Property(b => b.UserId)
                .HasMaxLength(200)
                .IsRequired();

            basket.HasMany(b => b.Items).WithOne()
                .HasForeignKey(b => b.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            basket.HasAlternateKey(b => b.UserId);
            basket.HasIndex(b => new { b.UserId });
        }
    }
}
