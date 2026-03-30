using FinanceFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceFlow.Infrastructure.Persistence.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.Ticker)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(a => a.Quantity)
            .IsRequired();

        builder.Property(a => a.AddedAt)
            .IsRequired();

        builder.OwnsOne(a => a.PurchasePrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("PurchasePrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(3)
                .IsRequired();
        });
    }
}