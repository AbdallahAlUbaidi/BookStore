namespace DTOs;

public class BookDto
{
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public double? Price { get; set; }
	public int? Stock { get; set; }

	public List<BookAuthorDto>? Authors { get; set; }
	public List<BookGenreDto>? Genres { get; set; }
}

public class BookAuthorDto
{
	public Guid? Id { get; set; }
	public string? Name { get; set; }
}

public class BookGenreDto
{
	public Guid? Id { get; set; }
	public string? Name { get; set; }
}