
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

public class LoginRequest(string username, string password)
{
	public string Username { get; init; } = username;
	public string Password { get; init; } = password;
}

public class RegisterRequest(string username, string password, string confirmPassword, string role)
{
	public string Username { get; init; } = username;
	public string Password { get; init; } = password;
	public string ConfirmPassword { get; init; } = confirmPassword;
	public string Role { get; init; } = role;
}


[ApiController]
[Route("/api/v1/auth")]
public class AuthController(IUserRepository userRepository, IPasswordHasher passwordHasher, IJsonWebTokenIssuer jwtIssuer) : ControllerBase
{

	private readonly IUserRepository _UserRepo = userRepository;
	private readonly IPasswordHasher _PasswordHasher = passwordHasher;
	private readonly IJsonWebTokenIssuer _JwtIssuer = jwtIssuer;

	private readonly int JWTLifeInMins = 60 * 12;

	[HttpPost]
	[Route("login")]
	public async Task<IActionResult> Login(LoginRequest req)
	{
		User? user = await _UserRepo.GetUserByName(req.Username)
			?? throw new InvalidCredentialsError("Invalid Username or password");

		if (!_PasswordHasher.Compare(req.Password, user.PasswordHash!))
			throw new InvalidCredentialsError("Invalid Username or password");

		string token = _JwtIssuer.Issue((Guid)user.Id!, JWTLifeInMins, user.Role);

		return Ok(new
		{
			success = true,
			data = new
			{
				token,
				user = new
				{
					id = user.Id,
					username = user.UserName,
					role = user.Role.ToString()
				}
			}
		});
	}

	[HttpPost]
	[Route("register")]
	public async Task<IActionResult> Register(RegisterRequest req)
	{
		User? user = await _UserRepo.GetUserByName(req.Username);

		if (user is not null)
			throw new DuplicateResourceError($"User with username {req.Username} already exists");

		if (req.Password != req.ConfirmPassword)
			throw new ValidationError([
				new ValidationIssue("ConfirmPassword", "Passwords do not match")
			]);

		string passwordHash = _PasswordHasher.Hash(req.Password);

		UserRole role = ParseUserRole(req.Role);

		user = new(req.Username, passwordHash, role);

		bool UserCreated = _UserRepo.CreateUser(user);

		if (UserCreated)
			await _UserRepo.Save();

		string token = _JwtIssuer.Issue((Guid)user.Id!, JWTLifeInMins, user.Role);

		return StatusCode(201, new
		{
			success = true,
			data = new
			{
				token,
				user = new
				{
					id = user.Id,
					username = user.UserName,
					role = user.Role.ToString()
				}
			}
		});
	}

	private static UserRole ParseUserRole(string? roleStr)
	{
		if (roleStr is null)
			throw new ValidationError([
				new ValidationIssue(
					"Role", "You need to specify user role"
					)
				]);

		if (!Enum.TryParse<UserRole>(roleStr.ToUpper(), out UserRole role))
			throw new ValidationError([
				new ValidationIssue(
								"Role", $"{roleStr} is not a valid user role use either Admin or Regular"
								)
				]);

		return role;
	}
}