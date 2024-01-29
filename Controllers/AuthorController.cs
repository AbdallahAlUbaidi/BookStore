namespace Controllers;

using AutoMapper;
using DTOs;
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

public class AuthorRequest(string name)
{
	public string Name { get; set; } = name;
}

[ApiController]
[Route("/api/v1/authors")]
public class AuthorController(IAuthorRepository authorRepository, IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
	private readonly IAuthorRepository _AuthorRepo = authorRepository;
	private readonly IUnitOfWork _UnitOfWork = unitOfWork;
	private readonly IMapper _Mapper = mapper;

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetAuthors()
	{
		ICollection<AuthorDto> authors = _Mapper.Map<List<AuthorDto>>(await _AuthorRepo.GetAuthors());

		return Ok(new { success = true, data = authors });
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> GetOneAuthor(Guid id)
	{
		AuthorDto? author = _Mapper.Map<AuthorDto>(await _AuthorRepo.GetAuthorById(id))
			?? throw new NotFoundError($"Author with an id of {id} does not exists");

		return Ok(new { success = true, data = author });

	}

	[HttpPost]
	[Authorize(Roles = "ADMIN")]
	public async Task<IActionResult> CreateAuthor(AuthorRequest req)
	{
		Author author = new(req.Name);

		bool AuthorCreated = _AuthorRepo.CreateAuthor(author);
		if (AuthorCreated)
			await _AuthorRepo.Save();

		return StatusCode(201);
	}
}