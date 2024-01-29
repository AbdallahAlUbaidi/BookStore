namespace DTOs;

public class AuthorDto
{
	public Guid? Id { get; set; }
	public string? Name { get; set; }
	public ICollection<AuthorBookDto>? Books { get; set; }
}

public class AuthorBookDto
{
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public double? Price { get; set; }
	public int? Stock { get; set; }
	public ICollection<BookGenreDto>? Genres { get; set; }

}