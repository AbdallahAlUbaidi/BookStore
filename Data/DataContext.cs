using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
	public DbSet<Book> Books { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<Genre> Genres { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Book>()
					.HasKey(book => book.Id);

		modelBuilder.Entity<Author>()
					.HasKey(author => author.Id);

		modelBuilder.Entity<Genre>()
					.HasKey(genre => genre.Id);

		modelBuilder.Entity<User>()
					.HasKey(user => user.Id);

		modelBuilder.Entity<Cart>()
					.HasKey(cart => cart.Id);

		modelBuilder.Entity<CartItem>()
					.HasKey(CartItem => CartItem.Id);

		modelBuilder.Entity<Order>()
					.HasKey(Order => Order.Id);

		modelBuilder.Entity<OrderItem>()
					.HasKey(orderItem => orderItem.Id);


		modelBuilder.Entity<Book>()
					.HasMany(b => b.Authors)
					.WithMany(a => a.Books)
					.UsingEntity(j => j.ToTable("BookAuthors"));

		modelBuilder.Entity<Book>()
					.HasMany(b => b.Genres)
					.WithMany(a => a.Books)
					.UsingEntity(j => j.ToTable("BookGenres"));


		modelBuilder.Entity<User>()
					.HasOne(user => user.Cart)
					.WithOne(cart => cart.User)
					.HasForeignKey<Cart>(cart => cart.UserId);


		modelBuilder.Entity<Cart>()
					.HasMany(cart => cart.Items);

		modelBuilder.Entity<CartItem>()
					.HasOne(ci => ci.Book);

		modelBuilder.Entity<Order>()
					.HasMany(o => o.Items);

		modelBuilder.Entity<OrderItem>()
					.HasOne(oi => oi.Book);

	}

}