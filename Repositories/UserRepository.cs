using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
	private readonly DataContext _Context = context;

	public bool CreateUser(User user)
	{
		_Context.Users.Add(user);
		
		return true;
	}

	public async Task<User?> GetUserById(Guid id)
	{
		return await _Context.Users.FirstOrDefaultAsync(user => user.Id == id);
	}

	public async Task<User?> GetUserByName(string name)
	{
		return await _Context.Users.FirstOrDefaultAsync(user => user.UserName == name);
	}

	public async Task Save()
	{
		await _Context.SaveChangesAsync();
	}
}