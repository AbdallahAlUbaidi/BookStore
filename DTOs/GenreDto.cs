namespace DTOs;

public class GenreDto
{
	public Guid? Id { get; set; }
	public string? Name { get; set; }
	public ICollection<GenreBookDto>? Books { get; set; }
}

public class GenreBookDto
{
	public Guid? Id { get; set; }
	public string? Title { get; set; }
	public double? Price { get; set; }
	public int? Stock { get; set; }
	public ICollection<BookAuthorDto>? Authors { get; set; }

}