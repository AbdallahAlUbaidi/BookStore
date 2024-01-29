namespace Models;

public class CartItem
{
	public CartItem() { }

	public CartItem(Guid bookId, int quantity)
	{
		Id = Guid.NewGuid();
		BookId = bookId;
		Quantity = quantity;

	}

	public CartItem(Guid id, Guid bookId, int quantity)
	{
		Id = id;
		BookId = bookId;
		Quantity = quantity;
	}

	public Guid? Id { get; set; }
	public Guid? BookId { get; set; }
	public Book? Book { get; set; }
	public int? Quantity { get; set; }

	public double GetSubTotal()
	{
		return Book!.Price * Quantity ?? 0;
	}

}