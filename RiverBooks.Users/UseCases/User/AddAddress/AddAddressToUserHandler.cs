using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.UseCases.User.AddAddress;

internal class AddAddressToUserHandler(
  IApplicationUserRepository userRepository, 
  ILogger<AddAddressToUserHandler> logger) : IRequestHandler<AddAddressToUserCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;
  private readonly ILogger<AddAddressToUserHandler> _logger = logger;

  public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.Email);
    if (user is null)
    {
      return Result.Unauthorized();
    }

    var addressToAdd = new Address(
      request.Street1,
      request.Street2,
      request.City,
      request.State,
      request.PostalCode,
      request.Country);
    var userAddress = user.AddAddress(addressToAdd);
    await _userRepository.SaveChangesAsync();

    _logger.LogInformation("[UseCase] Added address {address} to user {email} (Total: {total})",
      userAddress.StreetAddress,
      request.Email,
      user.Addresses.Count);

    return Result.Success();
  }
}
