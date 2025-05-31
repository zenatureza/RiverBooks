using System.Text.Json.Serialization;
using Ardalis.Result;

namespace RiverBooks.OrderProcessing.Integrations;

internal interface IOrderAddressCache
{
  Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
}
