using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Interfaces;
using RiverBooks.Users.UsersEndpoints;

namespace RiverBooks.Users.UseCases.User.ListAddresses;

internal class ListAddressesQueryHandler(IApplicationUserRepository userRepository) : IRequestHandler<ListAddressesQuery, Result<List<UserAddressDto>>>
{
  private readonly IApplicationUserRepository _userRepository = userRepository;

  public async Task<Result<List<UserAddressDto>>> Handle(ListAddressesQuery request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.Email);
    if (user is null)
    {
      return Result.Unauthorized();
    }

    return user!.Addresses!
      .Select(ua => new UserAddressDto(
        ua.Id, 
        ua.StreetAddress.Street1, 
        ua.StreetAddress.Street2,
        ua.StreetAddress.City,
        ua.StreetAddress.State,
        ua.StreetAddress.PostalCode,
        ua.StreetAddress.Country))
      .ToList();
  }
}
