namespace Models;

public class Book
{
    public Book(string title, int stock, double price, ICollection<Author> authors, ICollection<Genre> genres)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        Stock = stock;
        Authors = authors;
        Genres = genres;
    }

    public Book(string title, int stock, double price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        Stock = stock;
    }

    public Book() { }
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public double? Price { get; set; }
    public int? Stock { get; set; }
    public ICollection<Author> Authors { get; set; } = [];
    public ICollection<Genre> Genres { get; set; } = [];
}