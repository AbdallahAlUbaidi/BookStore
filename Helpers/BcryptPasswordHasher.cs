namespace Helpers;

using Interfaces;
using BCrypt.Net;

public class BcryptPasswordHasher : IPasswordHasher
{
	public bool Compare(string password, string hash)
	{
		return BCrypt.Verify(password, hash, true);
	}

	public string Hash(string password)
	{
		return BCrypt.HashPassword(password, 10, true);
	}
}