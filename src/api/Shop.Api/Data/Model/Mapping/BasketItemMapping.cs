using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Api.Data.Model.Mapping
{
    public class BasketItemMapping : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> basketItem)
        {
            basketItem.HasKey(bi => bi.Id);

            basketItem.Property(bi => bi.Id)
                .ValueGeneratedOnAdd();

            basketItem.Property(bi => bi.BasketId)
                .IsRequired();

            basketItem.Property(bi => bi.ProductId)
                .IsRequired();

            basketItem.Property(bi => bi.Quantity)
                .IsRequired();

            basketItem.HasOne(bi => bi.Product).WithMany()
                .HasForeignKey(bi => bi.ProductId);

            basketItem.HasIndex(b => new { b.BasketId, b.Id });
        }
    }
}
