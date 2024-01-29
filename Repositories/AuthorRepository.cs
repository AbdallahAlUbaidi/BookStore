using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class AuthorRepository(DataContext context) : IAuthorRepository
{

	private readonly DataContext _Context = context;

	public bool CreateAuthor(Author author)
	{
		_Context.Authors.Add(author);

		return true;
	}

	public async Task<Author?> GetAuthorById(Guid? id)
	{
		Author? author = await _Context.Authors
					.Include(a => a.Books)
					.Select(a => new Author
					{
						Id = a.Id,
						Name = a.Name,
						Books = a.Books.Select(b => new Book
						{
							Id = b.Id,
							Title = b.Title,
							Price = b.Price,
							Stock = b.Stock,
							Genres = b.Genres.Select(g => new Genre
							{
								Id = g.Id,
								Name = g.Name
							}).ToList()
						}).ToList()
					})
					.FirstOrDefaultAsync<Author>(a => a.Id == id);


		if (author is not null)
			_Context.Entry<Author>(author).State = EntityState.Detached;

		return author;
	}

	public async Task<ICollection<Author>> GetAuthors()
	{
		return await _Context.Authors
							.Include(a => a.Books)
							.Select(a => new Author
							{
								Id = a.Id,
								Name = a.Name,
								Books = a.Books.Select(b => new Book
								{
									Id = b.Id,
									Title = b.Title,
									Price = b.Price,
									Stock = b.Stock,
									Genres = b.Genres.Select(g => new Genre
									{
										Id = g.Id,
										Name = g.Name
									}).ToList()
								}).ToList()
							})
							.ToListAsync();
	}

	public async Task Save()
	{
		await _Context.SaveChangesAsync();
	}
}