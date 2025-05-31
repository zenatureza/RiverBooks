using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Infrastructure;

// Materialized view data model
internal record OrderAddress(Guid Id, Address Address);
