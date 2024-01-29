namespace Interfaces;

using Models;
public interface IBookRepository
{
	Task Save();
	Task<ICollection<Book>> GetBooks();

	bool CreateBook(Book book, List<Guid> authorsIds, List<Guid> genresIds);
	Task<Book?> GetBookById(Guid? id);
}