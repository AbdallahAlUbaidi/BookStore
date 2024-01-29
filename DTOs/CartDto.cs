namespace DTOs;

public class CartItemDto
{
	public Guid? Id { get; set; }
	public BookCartItemDto? Book { get; set; }
	public int? Quantity { get; set; }
	public double? SubTotal { get; set; }
}

public class CartDto
{
	public Guid? CartId { get; set; }
	public ICollection<CartItemDto>? Items { get; set; }
	public double? TotalPrice { get; set; }

}

public class BookCartItemDto
{
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public double? Price { get; set; }
}