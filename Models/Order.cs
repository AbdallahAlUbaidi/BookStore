namespace Models;

public class Order
{
	public Order() { }

	public Order(ICollection<OrderItem> items)
	{
		Items = items;
	}

	public Order(Guid userId, ICollection<OrderItem> items, double totalPrice)
	{
		Id = Guid.NewGuid();
		UserId = userId;
		TotalPrice = totalPrice;
		Items = items;
	}

	public Order(Guid id, Guid userId, ICollection<OrderItem> items, double totalPrice)
	{
		Id = id;
		UserId = userId;
		TotalPrice = totalPrice;
		Items = items;
	}

	public Guid? Id { get; set; }
	public ICollection<OrderItem>? Items { get; set; } = [];
	public User? User { get; set; }
	public Guid? UserId { get; set; }
	public double? TotalPrice { get; set; }

}