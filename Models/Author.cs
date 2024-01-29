
namespace Models;

public class Author
{

    public Author(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    public Author() { }
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Book> Books { get; set; } = [];

}