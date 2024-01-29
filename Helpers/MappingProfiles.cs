using AutoMapper;
using DTOs;
using Models;

namespace Helpers;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<Book, BookDto>()
			.ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(author => new BookAuthorDto { Id = author.Id, Name = author.Name })))
			.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(genre => new BookGenreDto { Id = genre.Id, Name = genre.Name })));

		CreateMap<Author, AuthorDto>()
			.ForMember(dest => dest.Books
			, opt => opt.MapFrom(src => src.Books
				.Select(book => new AuthorBookDto
				{
					Id = book.Id,
					Title = book.Title,
					Price = book.Price,
					Stock = book.Stock,
					Genres = book.Genres.Select(g => new BookGenreDto
					{
						Id = g.Id,
						Name = g.Name
					}).ToList()
				}
			)
		));

		CreateMap<Genre, GenreDto>()
			.ForMember(dest => dest.Books
				, opt => opt.MapFrom(src => src.Books
						.Select(book => new GenreBookDto
						{
							Id = book.Id,
							Title = book.Title,
							Price = book.Price,
							Stock = book.Stock,
							Authors = book.Authors.Select(a => new BookAuthorDto
							{
								Id = a.Id,
								Name = a.Name
							}).ToList()
						})
					)
				);

		CreateMap<CartItem, CartItemDto>()
			.ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.GetSubTotal()))
			.ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

		CreateMap<Cart, CartDto>()
			.ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
			.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.GetTotalPrice()));

		CreateMap<OrderItem, OrderItemDto>()
			.ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

		CreateMap<Order, OrderDto>()
			.ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

		CreateMap<Book, BookCartItemDto>();

	}
}