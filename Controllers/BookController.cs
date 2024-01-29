namespace Controllers;

using AutoMapper;
using DTOs;
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;


public class BookRequest
{
	public required string Title { get; set; }
	public required int Stock { get; set; }
	public required double Price { get; set; }
	public List<Guid> AuthorsIds { get; set; } = [];
	public List<Guid> GenresIds { get; set; } = [];

}

[ApiController]
[Route("/api/v1/books")]
public class BookController(
	IBookRepository bookRepo,
	IUnitOfWork unitOfWork,
	IMapper mapper) : ControllerBase
{

	private readonly IBookRepository _BookRepo = bookRepo;

	private readonly IUnitOfWork _UnitOfWork = unitOfWork;
	private readonly IMapper _Mapper = mapper;


	[HttpPost]
	[Authorize(Roles = "ADMIN")]
	public async Task<IActionResult> Create(BookRequest req)
	{

		Book book = new(req.Title, req.Stock, req.Price);

		bool BookCreated = _BookRepo.CreateBook(book, req.AuthorsIds, req.GenresIds);

		if (BookCreated)
			await _BookRepo.Save();

		return StatusCode(201);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAllBooks()
	{
		ICollection<BookDto> books = _Mapper.Map<List<BookDto>>(await _BookRepo.GetBooks());

		return Ok(new { success = true, data = books });
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> GetOneBook(Guid id)
	{
		BookDto? book = _Mapper.Map<BookDto>(await _BookRepo.GetBookById(id))
			?? throw new NotFoundError($"Book with id of {id} does not exists");

		return Ok(new { success = true, data = book });
	}

}