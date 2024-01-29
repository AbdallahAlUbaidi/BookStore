using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class CartRepository(DataContext context) : ICartRepository
{
	private readonly DataContext _Context = context;

	public void CreateCart(Cart cart)
	{
		_Context.Carts.Add(cart);
	}

	public async Task<Cart?> GetCartByUserId(Guid UserId)
	{
		var cart = await _Context.Carts
				.Include(c => c.User)
				.Include(c => c.Items)!
					.ThenInclude(ci => ci.Book)
				.FirstOrDefaultAsync(c => c.UserId == UserId);

		return cart;
	}

	public async Task Save()
	{
		await _Context.SaveChangesAsync();
	}
}