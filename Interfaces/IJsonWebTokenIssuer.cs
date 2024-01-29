using Models;

namespace Interfaces;

public interface IJsonWebTokenIssuer
{
	public string Issue(Guid userId, int expiresInMinutes, UserRole role);

}