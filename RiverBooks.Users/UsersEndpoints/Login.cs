using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UsersEndpoints;

internal class Login(UserManager<ApplicationUser> userManager) : Endpoint<UserLoginRequest>
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;

  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
  {
    var user = await _userManager.FindByEmailAsync(req.Email);
    if (user == null)
    {
      await SendUnauthorizedAsync(cancellation: ct);
      return;
    }
    var result = await _userManager.CheckPasswordAsync(user, req.Password);
    if (!result)
    {
      await SendUnauthorizedAsync(cancellation: ct);
      return;
    }
    
    var jwtSecret = Config["Auth:JwtSecret"]!;
    var token = JWTBearer.CreateToken(jwtSecret,
      p => p["EmailAddress"] = user.Email!);

    await SendAsync(token, cancellation: ct);
  }
}
