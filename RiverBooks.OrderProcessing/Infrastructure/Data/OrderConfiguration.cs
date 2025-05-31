using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Infrastructure.Data;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.HasKey(o => o.Id);

    builder.Property(o => o.Id)
      .ValueGeneratedNever();
    
    builder.ComplexProperty(o => o.ShippingAddress, address =>
    {
      address.Property(a => a.Street1).HasMaxLength(Constants.STREET_MAX_LENGTH);
      address.Property(a => a.Street2).HasMaxLength(Constants.STREET_MAX_LENGTH);
      address.Property(a => a.City).HasMaxLength(Constants.CITY_MAX_LENGTH);
      address.Property(a => a.State).HasMaxLength(Constants.STATE_MAX_LENGTH);
      address.Property(a => a.PostalCode).HasMaxLength(Constants.POSTAL_CODE_MAX_LENGTH);
      address.Property(a => a.Country).HasMaxLength(Constants.COUNTRY_MAX_LENGTH);
    });

    builder.ComplexProperty(o => o.BillingAddress, address =>
    {
      address.Property(a => a.Street1).HasMaxLength(Constants.STREET_MAX_LENGTH);
      address.Property(a => a.Street2).HasMaxLength(Constants.STREET_MAX_LENGTH);
      address.Property(a => a.City).HasMaxLength(Constants.CITY_MAX_LENGTH);
      address.Property(a => a.State).HasMaxLength(Constants.STATE_MAX_LENGTH);
      address.Property(a => a.PostalCode).HasMaxLength(Constants.POSTAL_CODE_MAX_LENGTH);
      address.Property(a => a.Country).HasMaxLength(Constants.COUNTRY_MAX_LENGTH);
    });
  }
}
