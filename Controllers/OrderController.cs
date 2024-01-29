using System.Security.Claims;
using AutoMapper;
using DTOs;
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

[ApiController]
[Route("api/v1/orders")]
[Authorize]
public class OrderController(
		IUnitOfWork unitOfWork,
		IBookRepository bookRepository,
		IHttpContextAccessor httpContextAccessor,
		ICartRepository cartRepository,
		IOrderRepository orderRepository,
		IMapper mapper
	) : ControllerBase
{
	private readonly IUnitOfWork _UnitOfWork = unitOfWork;
	private readonly IBookRepository _BookRepo = bookRepository;
	private readonly IHttpContextAccessor _HttpContextAccessor = httpContextAccessor;
	private readonly ICartRepository _CartRepo = cartRepository;
	private readonly IOrderRepository _OrderRepo = orderRepository;
	private readonly IMapper _Mapper = mapper;

	[HttpPost]
	public async Task<IActionResult> PlaceOrder()
	{
		Cart cart = await GetCartFromContext();

		ICollection<OrderItem> orderItems = [];
		foreach (CartItem cartItem in cart.Items!)
		{
			Book book = cartItem.Book!;
			OrderItem orderItem = new(
				(Guid)cartItem.BookId!,
				(int)cartItem.Quantity!,
				cartItem.GetSubTotal()
			);

			orderItems.Add(orderItem);
			if (book.Stock < orderItem.Quantity)
				throw new InsufficientQuantityError($"The book \"{book.Title}\" has {book.Stock} copies and you are trying to purchase {orderItem.Quantity} copy");
			book.Stock -= orderItem.Quantity;
		}

		Order order = new((Guid)cart.UserId!, orderItems, cart.GetTotalPrice());

		cart.Clear();

		_OrderRepo.CreateOrder(order);

		await _UnitOfWork.SaveChangesAsync();

		return StatusCode(201);
	}

	[HttpGet]
	public async Task<IActionResult> GetOrders()
	{
		Guid userId = GetUserIdFromContext();

		var orders = _Mapper.Map<ICollection<OrderDto>>(await _OrderRepo.GetOrders(userId));

		return Ok(new { success = true, data = orders });
	}

	private Guid GetUserIdFromContext()
	{
		string userIdStr = _HttpContextAccessor.HttpContext!.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

		if (!Guid.TryParse(userIdStr, out Guid userId))
			throw new InvalidGuidError($"{userIdStr} is not a valid User Id");

		return userId;
	}

	private async Task<Cart> GetCartFromContext()
	{
		Guid userId = GetUserIdFromContext();

		Cart? cart = await _CartRepo.GetCartByUserId(userId);

		if (cart is null)
		{
			cart = new Cart
			{
				UserId = userId
			};
			_CartRepo.CreateCart(cart);
		}

		return cart;
	}

}