using Models;

namespace Interfaces;

public interface IGenreRepository
{
	public Task Save();

	public bool CreateGenre(Genre genre);
	public Task<ICollection<Genre>> GetGenres();
	public Task<Genre?> GetGenreById(Guid? id);
}