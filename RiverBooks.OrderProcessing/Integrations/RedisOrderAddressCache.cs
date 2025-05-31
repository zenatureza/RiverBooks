using System.Text.Json;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace RiverBooks.OrderProcessing.Integrations;

internal class RedisOrderAddressCache : IOrderAddressCache
{
  private readonly ILogger<RedisOrderAddressCache> _logger;
  private readonly IDatabase _db;

  public RedisOrderAddressCache(ILogger<RedisOrderAddressCache> logger)
  {
    var redis = ConnectionMultiplexer.Connect("localhost"); // TODO: get from iconfiguration
    _logger = logger;
    _db = redis.GetDatabase();
  }

  public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
  {
    string? fetchedJson = await _db.StringGetAsync(addressId.ToString());
    if (fetchedJson is null)
    {
      _logger.LogWarning("Address {id} not found in {db}", addressId, "REDIS");
      return Result.NotFound();
    } 

    var address = JsonSerializer.Deserialize<OrderAddress>(fetchedJson);
    if (address is null) return Result.NotFound();

    _logger.LogInformation("Address {id} returned from {db}", addressId, "REDIS");
    return Result.Success(address);
  }
}
