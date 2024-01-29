using AutoMapper;
using DTOs;
using Errors;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

public class GenreRequest(string name)
{
	public string Name { get; set; } = name;
}

[ApiController]
[Route("/api/v1/genres")]
public class GenreController(IGenreRepository genreRepository, IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{

	private readonly IGenreRepository _GenreRepo = genreRepository;
	private readonly IUnitOfWork _UnitOfWork = unitOfWork;
	private readonly IMapper _Mapper = mapper;

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetGenres()
	{
		ICollection<GenreDto> genres = _Mapper.Map<List<GenreDto>>(await _GenreRepo.GetGenres());

		return Ok(new { success = true, data = genres });
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> GetOneGenre(Guid id)
	{
		GenreDto? genre = _Mapper.Map<GenreDto>(await _GenreRepo.GetGenreById(id))
			?? throw new NotFoundError("Genre with an Id of {id} does not exists");

		return Ok(new { success = true, data = genre });

	}

	[HttpPost]
	[Authorize(Roles = "ADMIN")]
	public async Task<IActionResult> CreateGenre(GenreRequest req)
	{
		Genre genre = new(req.Name);

		bool GenreCreated = _GenreRepo.CreateGenre(genre);

		if (GenreCreated)
			await _GenreRepo.Save();

		return StatusCode(201);
	}
}