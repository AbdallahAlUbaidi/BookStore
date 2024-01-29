using System.ComponentModel.DataAnnotations;

namespace Models;

public class Genre
{

	public Genre() { }
	public Genre(string name)
	{
		Id = Guid.NewGuid();
		Name = name;
	}

	public Guid? Id { get; set; }
	public string? Name { get; set; }
	public ICollection<Book> Books { get; set; } = [];

}