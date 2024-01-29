using Models;

namespace Interfaces;

public interface IAuthorRepository
{
	public Task Save();

	public bool CreateAuthor(Author author);
	public Task<ICollection<Author>> GetAuthors();
	public Task<Author?> GetAuthorById(Guid? id);
}