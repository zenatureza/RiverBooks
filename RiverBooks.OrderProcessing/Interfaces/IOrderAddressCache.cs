﻿using System.Text.Json.Serialization;
using Ardalis.Result;
using RiverBooks.OrderProcessing.Infrastructure;

namespace RiverBooks.OrderProcessing.Interfaces;

internal interface IOrderAddressCache
{
  Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
  Task<Result> StoreAsync(OrderAddress orderAddress);
}
