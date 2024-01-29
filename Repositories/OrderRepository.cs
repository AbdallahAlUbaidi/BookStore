using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class OrderRepository(DataContext context) : IOrderRepository
{

	private readonly DataContext _Context = context;

	public void CreateOrder(Order order)
	{
		_Context.Orders.Add(order);
	}

	public async Task<Order?> GetOrderById(Guid id)
	{
		var order = await _Context.Orders
			.Include(o => o.User)
			.Include(o => o.Items)!
				.ThenInclude(oi => oi.Book)
			.FirstOrDefaultAsync(o => o.Id == id);

		return order;
	}

	public async Task<List<Order>> GetOrders(Guid userId)
	{
		var orders = await _Context.Orders
			.Include(o => o.User)
			.Include(o => o.Items)!
				.ThenInclude(oi => oi.Book)
			.Where(o => o.UserId == userId)
			.ToListAsync();

		return orders;
	}

	public async Task Save()
	{
		await _Context.SaveChangesAsync();
	}
}