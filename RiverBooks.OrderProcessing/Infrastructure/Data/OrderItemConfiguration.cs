﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Infrastructure.Data;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.Property(oi => oi.Id)
      .ValueGeneratedNever();

    builder.Property(x => x.Description)
      .HasMaxLength(100)
      .IsRequired();
  }
}
