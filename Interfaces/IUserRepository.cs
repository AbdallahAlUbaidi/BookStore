using Models;

namespace Interfaces;

public interface IUserRepository
{
	Task Save();
	bool CreateUser(User user);
	Task<User?> GetUserById(Guid id);
	Task<User?> GetUserByName(string name);
}