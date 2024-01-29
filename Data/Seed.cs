using Models;

namespace Data
{
	public class Seed(DataContext dataContext)
	{
		private readonly DataContext _dataContext = dataContext;

		public void SeedDB()
		{
			SeedAuthors();
			SeedGenres();
			SeedBooks();
		}

		private void SeedAuthors()
		{
			if (_dataContext.Authors.Any<Author>())
				return;

			var authors = new List<Author>
			{
				new("J.K. Rowling"),
				new("George R.R. Martin"),
				new("Stephen King"),
				new("Agatha Christie")
			};

			_dataContext.Authors.AddRange(authors);
			_dataContext.SaveChanges();
		}

		private void SeedGenres()
		{
			if (_dataContext.Genres.Any<Genre>())
				return;

			var genres = new List<Genre>
			{
				new("Fantasy Fiction"),
				new("Crime"),
				new("Science Fiction"),
				new("Horror")
			};

			_dataContext.Genres.AddRange(genres);
			_dataContext.SaveChanges();

		}


		private void SeedBooks()
		{

			if (_dataContext.Books.Any<Book>())
				return;

			Author JKRowling = _dataContext.Authors.FirstOrDefault(a => a.Name == "J.K. Rowling") ?? new Author("121");
			Author GeorgeRRMartin = _dataContext.Authors.FirstOrDefault(a => a.Name == "George R.R. Martin") ?? JKRowling;
			Author StephenKing = _dataContext.Authors.FirstOrDefault(a => a.Name == "Stephen King") ?? JKRowling;
			Author AgathaChristie = _dataContext.Authors.FirstOrDefault(a => a.Name == "Agatha Christie") ?? JKRowling;

			Genre fantasy = _dataContext.Genres.FirstOrDefault(g => g.Name == "Fantasy Fiction") ?? new Genre("123");
			Genre crime = _dataContext.Genres.FirstOrDefault(g => g.Name == "Crime") ?? fantasy;
			Genre si_fi = _dataContext.Genres.FirstOrDefault(g => g.Name == "Science Fiction") ?? fantasy;
			Genre horror = _dataContext.Genres.FirstOrDefault(g => g.Name == "Horror") ?? fantasy;


			Book harryPotter = new("Harry Potter and the Philosopher's Stone", 31, 19.99);
			harryPotter.Authors.Add(JKRowling);
			harryPotter.Genres.Add(fantasy);
			_dataContext.Books.Add(harryPotter);


			Book theRunningGrave = new("The Running Grave", 59, 12.99);
			theRunningGrave.Authors.Add(JKRowling);
			theRunningGrave.Genres.Add(crime);

			_dataContext.Books.Add(theRunningGrave);

			Book gameOfThrones = new("A Game of Thrones", 14, 9.99);
			gameOfThrones.Authors.Add(GeorgeRRMartin);
			gameOfThrones.Genres.Add(fantasy);

			_dataContext.Books.Add(gameOfThrones);

			Book theShining = new("The Shining", 55, 7.48);
			theShining.Authors.Add(StephenKing);
			theShining.Genres.Add(horror);
			_dataContext.Books.Add(theShining);

			Book deathOnTheNile = new("Death on the Nile", 44, 5.14);
			deathOnTheNile.Authors.Add(AgathaChristie);
			deathOnTheNile.Genres.Add(crime);
			_dataContext.Books.Add(deathOnTheNile);


			_dataContext.SaveChanges();
		}

	}
}
