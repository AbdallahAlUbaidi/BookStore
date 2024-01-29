using System.Net.Http.Headers;
using System.Security.Claims;
using AutoMapper;
using DTOs;
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

public class AddItemRequest()
{
	public Guid BookId { get; set; }
	public int Quantity { get; set; }
}


[ApiController]
[Route("/api/v1/cart")]
public class CartController(
	IUnitOfWork unitOfWork,
	IBookRepository bookRepository,
	IHttpContextAccessor httpContextAccessor,
	ICartRepository cartRepository,
	IMapper mapper
	) : ControllerBase
{
	private readonly IUnitOfWork _UnitOfWork = unitOfWork;
	private readonly IBookRepository _BookRepo = bookRepository;
	private readonly IHttpContextAccessor _HttpContextAccessor = httpContextAccessor;
	private readonly ICartRepository _CartRepo = cartRepository;
	private readonly IMapper _Mapper = mapper;

	[HttpPost]
	[Route("items")]
	[Authorize]
	public async Task<IActionResult> AddItem(AddItemRequest req)
	{
		Cart cart = await GetCartFromContext();
		Book book = await _BookRepo.GetBookById(req.BookId)
			?? throw new NotFoundError($"Book with id of {req.BookId} was not found");

		CartItem? cartItem = GetCartItemWithBook((Guid)book.Id!, cart);

		if (cartItem is null)
		{
			cartItem = new((Guid)book.Id!, req.Quantity);

			cart!.Items!.Add(cartItem);
		}
		else
		{
			cartItem.Quantity += req.Quantity;
		}

		await _UnitOfWork.SaveChangesAsync();

		return StatusCode(201);
	}

	[HttpGet]
	[Route("items")]
	[Authorize]
	public async Task<IActionResult> GetItems()
	{
		CartDto cart = _Mapper.Map<CartDto>(await GetCartFromContext());

		return Ok(new
		{
			success = true,
			data = cart
		});

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

	private static CartItem? GetCartItemWithBook(Guid BookId, Cart cart)
	{
		if (cart.Items is null)
			return null;

		foreach (CartItem item in cart.Items)
		{
			if (item.BookId == BookId) return item;
		}

		return null;
	}

}