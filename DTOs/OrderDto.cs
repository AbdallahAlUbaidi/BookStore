namespace DTOs;

public class OrderItemDto
{
	public Guid? Id { get; set; }
	public BookCartItemDto? Book { get; set; }
	public int? Quantity { get; set; }
	public double? SubTotal { get; set; }
}

public class OrderDto
{
	public Guid? OrderId { get; set; }
	public ICollection<OrderItemDto>? Items { get; set; }
	public double? TotalPrice { get; set; }

}
