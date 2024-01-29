using System.IdentityModel.Tokens.Jwt;
using Errors;
using Interfaces;

namespace Middleware;

public class PopulateMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next;
	public async Task Invoke(HttpContext context, IUserRepository userRepo)
	{
		var userIdClaim = context.User?.FindFirst(JwtRegisteredClaimNames.Sub);

		if (userIdClaim != null)
		{
			if (Guid.TryParse(userIdClaim.Value, out Guid userId))
			{
				var user = await userRepo.GetUserById(userId!)
					?? throw new NotFoundError($"User with id {userId} does not exists");

				context.Items["CurrentUser"] = user;
			}
			else
			{
				throw new InvalidGuidError($"{userIdClaim.Value} is not valid user id");
			}

		}

		await _next(context);
	}
}