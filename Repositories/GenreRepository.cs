namespace Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class GenreRepository(DataContext context) : IGenreRepository
{

	private readonly DataContext _Context = context;

	public bool CreateGenre(Genre genre)
	{
		_Context.Add(genre);

		return true;
	}

	public async Task<Genre?> GetGenreById(Guid? id)
	{
		Genre? genre = await _Context.Genres
					.Include(g => g.Books)
					.Select(g => new Genre
					{
						Id = g.Id,
						Name = g.Name,
						Books = g.Books.Select(b => new Book
						{
							Id = b.Id,
							Title = b.Title,
							Price = b.Price,
							Stock = b.Stock,
							Authors = b.Authors.Select(a => new Author
							{
								Id = a.Id,
								Name = a.Name,
							}).ToList()
						}).ToList()
					}).FirstOrDefaultAsync<Genre>(g => g.Id == id);


		if (genre is not null)
			_Context.Entry<Genre>(genre).State = EntityState.Detached;

		return genre;
	}

	public async Task<ICollection<Genre>> GetGenres()
	{
		return await _Context.Genres
							.Include(g => g.Books)
							.Select(g => new Genre
							{
								Id = g.Id,
								Name = g.Name,
								Books = g.Books.Select(b => new Book
								{
									Id = b.Id,
									Title = b.Title,
									Price = b.Price,
									Stock = b.Stock,
									Authors = b.Authors.Select(a => new Author
									{
										Id = a.Id,
										Name = a.Name,
									}).ToList()
								}).ToList()
							}).ToListAsync();
	}
	public async Task Save()
	{
		await _Context.SaveChangesAsync();
	}
}