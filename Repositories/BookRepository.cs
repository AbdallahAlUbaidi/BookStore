namespace Repositories;

using System.Collections.Generic;
using Data;
using Errors;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class BookRepository(DataContext context) : IBookRepository
{
	private readonly DataContext _DataContext = context;

	public async Task<ICollection<Book>> GetBooks()
	{

		return await _DataContext.Books
								.Include(b => b.Authors)
								.Include(b => b.Genres)
								.Select(b => new Book
								{
									Id = b.Id,
									Title = b.Title,
									Price = b.Price,
									Stock = b.Stock,
									Authors = b.Authors.Select(a => new Author { Id = a.Id, Name = a.Name }).ToList(),
									Genres = b.Genres.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToList()
								})
								.ToListAsync();
	}

	public async Task<Book?> GetBookById(Guid? id)
	{

		if (id == null) throw new InvalidDataException("Book Id is null");

		return await _DataContext.Books
								.Include(b => b.Authors)
								.Include(b => b.Genres)
								.Select(b => new Book
								{
									Id = b.Id,
									Title = b.Title,
									Price = b.Price,
									Stock = b.Stock,
									Authors = b.Authors.Select(a => new Author { Id = a.Id, Name = a.Name }).ToList(),
									Genres = b.Genres.Select(g => new Genre { Id = g.Id, Name = g.Name }).ToList()
								})
								.FirstOrDefaultAsync<Book>(b => b.Id == id);
	}

	public bool CreateBook(Book book, List<Guid> authorsIds, List<Guid> genresId)
	{
		_DataContext.Add(book);
		List<Author> authors = _DataContext.Authors.Where<Author>(a => authorsIds.Contains((Guid)a.Id!)).ToList();
		List<Genre> genres = _DataContext.Genres.Where<Genre>(g => genresId.Contains((Guid)g.Id!)).ToList();

		if (authors.Count < 1)
			throw new BadRequestError("Book must have at least one Author");

		if (genres.Count < 1)
			throw new BadRequestError("Book must have at least one Genre");

		book.Authors = authors;
		book.Genres = genres;

		return true;
	}

	public async Task Save()
	{
		await _DataContext.SaveChangesAsync();
	}


}