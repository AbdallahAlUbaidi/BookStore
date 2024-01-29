using Models;

namespace Interfaces;

public interface IOrderRepository
{
	void CreateOrder(Order order);
	Task<List<Order>> GetOrders(Guid userId);
	Task<Order?> GetOrderById(Guid id);
	Task Save();
}