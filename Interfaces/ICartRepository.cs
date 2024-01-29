using Models;

namespace Interfaces;

public interface ICartRepository
{
	void CreateCart(Cart cart);

	Task<Cart?> GetCartByUserId(Guid UserId);
	Task Save();
}