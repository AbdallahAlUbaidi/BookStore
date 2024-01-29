namespace Models;

public class Cart
{
	public Cart() { }

	public Cart(ICollection<CartItem> items)
	{
		Items = items;
	}


	public Guid? Id { get; set; }
	public ICollection<CartItem>? Items { get; set; } = [];
	public User? User { get; set; }
	public Guid? UserId { get; set; }


	public double GetTotalPrice()
	{
		return Items?.Sum(item => item.GetSubTotal()) ?? 0;
	}

	public void Clear()
	{
		Items?.Clear();
	}

}