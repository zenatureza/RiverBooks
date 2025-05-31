using MediatR;
using Ardalis.Result;

namespace RiverBooks.Users.Contracts;

public record UserAddressDetailsByIdQuery(Guid AddressId) :
  IRequest<Result<UserAddressDetails>>;
