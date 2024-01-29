namespace Models;

public class OrderItem
{
	public OrderItem() { }

	public OrderItem(Guid bookId, int quantity, double subTotal)
	{
		Id = Guid.NewGuid();
		BookId = bookId;
		Quantity = quantity;
		SubTotal = subTotal;
	}

	public OrderItem(Guid id, Guid bookId, int quantity, double subTotal)
	{
		Id = id;
		BookId = bookId;
		Quantity = quantity;
		SubTotal = subTotal;
	}

	public Guid? Id { get; set; }
	public Guid? BookId { get; set; }
	public Book? Book { get; set; }
	public int? Quantity { get; set; }
	public double SubTotal { get; set; }

}