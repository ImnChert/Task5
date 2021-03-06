using Task5;

namespace Task5Test
{
	[TestClass]
	public class BookDtoServiceTests
	{
		string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FifthTaskDbb;Integrated Security=True";
		DataBaseLauncher launcher;
		Book bookOne;
		Book bookTwo;
		Book bookThree;
		Subscriber subscriberOne;
		Subscriber subscriberTwo;
		Subscriber subscriberThree;
		BookReport bookReportOne;
		BookReport bookReportTwo;
		BookReport bookReportThree;
		BookReport bookReportFour;

		[TestInitialize]
		public void TestInitialize()
		{
			launcher = new DataBaseLauncher(connectionString);
			DAOFactory _daoCreate = new DAOFactory();
			List<BookReportDto> bookReportDtos = BookReportDtoService.GetAllReports();
			foreach (var report in bookReportDtos)
			{
				_daoCreate.Create<BookReport>(connectionString).Delete(report.Id);
			}
			List<BookDto> bookDtos = BookDtoService.GetAllBooks();
			foreach (var book in bookDtos)
			{
				_daoCreate.Create<Book>(connectionString).Delete(book.Id);
			}
			List<SubscriberDto> subscriberDtos = SubscriberDtoService.GetAllSubcribers();
			foreach (var subscriber in subscriberDtos)
			{
				_daoCreate.Create<Subscriber>(connectionString).Delete(subscriber.Id);
			}
			bookOne = new Book();
			bookOne.Author = "Dostoevksky";
			bookOne.Genre = "Novel";
			bookOne.Title = "Brothers Karamazov";
			bookOne.Id = _daoCreate.Create<Book>(connectionString).Create(bookOne);
			bookTwo = new Book();
			bookTwo.Author = "Dostoevsky";
			bookTwo.Genre = "Novel";
			bookTwo.Title = "Crime and Punishment";
			bookTwo.Id = _daoCreate.Create<Book>(connectionString).Create(bookTwo);
			bookThree = new Book();
			bookThree.Author = "Platon";
			bookThree.Genre = "Dialogue";
			bookThree.Title = "The Republic";
			bookThree.Id = _daoCreate.Create<Book>(connectionString).Create(bookThree);
			subscriberOne = new Subscriber();
			subscriberOne.FirstName = "Michael";
			subscriberOne.LastName = "Jordan";
			subscriberOne.MiddleName = "Eliot";
			subscriberOne.Sex = true;
			subscriberOne.DateOfBirth = new DateTime(2002, 5, 16);
			subscriberOne.Id = _daoCreate.Create<Subscriber>(connectionString).Create(subscriberOne);
			subscriberTwo = new Subscriber();
			subscriberTwo.FirstName = "Alex";
			subscriberTwo.LastName = "Celiot";
			subscriberTwo.MiddleName = "Cile";
			subscriberTwo.Sex = true;
			subscriberTwo.DateOfBirth = new DateTime(2000, 4, 13);
			subscriberTwo.Id = _daoCreate.Create<Subscriber>(connectionString).Create(subscriberTwo);
			subscriberThree = new Subscriber();
			subscriberThree.FirstName = "Jersey";
			subscriberThree.LastName = "Diana";
			subscriberThree.MiddleName = "Cristina";
			subscriberThree.Sex = false;
			subscriberThree.DateOfBirth = new DateTime(1998, 4, 13);
			subscriberThree.Id = _daoCreate.Create<Subscriber>(connectionString).Create(subscriberThree);
			bookReportOne = new BookReport();
			bookReportOne.DateOfGiving = new DateTime(2017, 5, 14);
			bookReportOne.ReturnStatus = true;
			bookReportOne.StateOfBook = "Good";
			bookReportOne.SubscriberId = subscriberOne.Id;
			bookReportOne.BookId = bookOne.Id;
			bookReportOne.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportOne);
			bookReportTwo = new BookReport();
			bookReportTwo.ReturnStatus = true;
			bookReportTwo.DateOfGiving = new DateTime(2016, 5, 11);
			bookReportTwo.StateOfBook = "Bad";
			bookReportTwo.SubscriberId = subscriberOne.Id;
			bookReportTwo.BookId = bookTwo.Id;
			bookReportTwo.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportTwo);
			bookReportThree = new BookReport();
			bookReportThree.ReturnStatus = true;
			bookReportThree.DateOfGiving = new DateTime(2019, 6, 14);
			bookReportThree.StateOfBook = "Medium";
			bookReportThree.SubscriberId = subscriberTwo.Id;
			bookReportThree.BookId = bookTwo.Id;
			bookReportThree.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportThree);
			bookReportFour = new BookReport();
			bookReportFour.ReturnStatus = true;
			bookReportFour.DateOfGiving = new DateTime(2020, 6, 14);
			bookReportFour.StateOfBook = "Good";
			bookReportFour.SubscriberId = subscriberThree.Id;
			bookReportFour.BookId = bookThree.Id;
			bookReportFour.Id = _daoCreate.Create<BookReport>(connectionString).Create(bookReportFour);
		}

		/// <summary>
		/// Tests method GetTheMostPopularAuthor
		/// </summary>
		[TestMethod]
		public void GetTheMostPopularAuthor_ShouldReturnRightAuthor()
		{
			// Arrange
			string expected = "Dostoevsky";

			// Act
			string actual = BookDtoService.GetTheMostPopularAuthor();

			//Assert
			Assert.AreEqual(expected, actual);

		}
		/// <summary>
		/// Tests method GetTheMostPopularGenre
		/// </summary>
		[TestMethod]
		public void GetTheMostPopularGenre_ShouldReturnRightGenre()
		{
			// Arrange
			string expected = "Novel";

			// Act
			string actual = BookDtoService.GetTheMostPopularGenre();

			// Assert
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// Tests method get all books 
		/// </summary>
		[TestMethod]
		public void GetAllBooks_ShouldNotReturnNull()
		{
			// Arrange
			bool expected = true;
			bool actual = true;

			// Act
			List<BookDto> bookDtos = BookDtoService.GetAllBooks();
			if (bookDtos == null || bookDtos.Count != 3)
				actual = false;
			// Assert
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// Tests method GroupCerainBooksByGenre
		/// </summary>
		[TestMethod]
		public void GroupCertainBooksByGenre_ShouldGroupedBooksByGenre()
		{
			// Arrange
			List<BookDto> bookDtos = BookDtoService.GetAllBooks();
			List<(string, List<BookDto>)> expectedList = new List<(string, List<BookDto>)> { ("Novel", new List<BookDto> { bookDtos[0], bookDtos[1] }), ("Dialogue", new List<BookDto> { bookDtos[2] }) };
			List<(string, List<BookDto>)> actualList = new List<(string, List<BookDto>)> { };
			bool expected = true;
			bool actual = true;
			int i = 0;
			int j = 0;

			// Act
			foreach (var elem in BookDtoService.GroupCertainBooksByGenre(bookDtos))
			{
				List<BookDto> books = new List<BookDto> { };
				string genre = elem.Key;
				if (genre != expectedList[i].Item1)
					actual = false;
				foreach (var book in elem)
				{
					if (book != expectedList[i].Item2[j])
						actual = false;
					j++;
				}
				j = 0;
				i++;
			}
			// Assert
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// Tests method CalculateHowManyTimesBooksWasTaken
		/// </summary>
		[TestMethod]
		public void CalculateHowManyTimesBooksWasTaken_ShouldReturnLinqRequestWithGroupedBooksUsage()
		{
			// Arrange
			List<BookDto> bookDtos = BookDtoService.GetAllBooks();
			List<BookWithUsageDto> expectedList = new List<BookWithUsageDto> { new BookWithUsageDto { Book = bookDtos[0], Count = 1 }, new BookWithUsageDto { Book = bookDtos[1], Count = 2 }, new BookWithUsageDto { Book = bookDtos[2], Count = 1 } };
			List<BookWithUsageDto> actualList = new List<BookWithUsageDto> { };
			bool expected = true;
			bool actual = true;
			int i = 0;


			// Act
			actualList = BookDtoService.CalculateHowManyTimesBooksWasTaken().ToList();
			foreach (var elem in actualList)
			{
				if (elem.Book != expectedList[i].Book && elem.Count != expectedList[i].Count)
					actual = false;
				i++;
			}

			// Assert
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// Tests method BooksToRecover
		/// </summary>
		[TestMethod]
		public void BooksToRecover_ShouldReturnAllBooksThatNeedToRecover()
		{
			// Arrange
			List<BookDto> bookDtos = BookDtoService.GetAllBooks();
			BookDto expectedBook = bookDtos[1];
			bool expected = true;
			bool actual = true;


			// Act
			List<BookDto> actualList = BookDtoService.BooksToRecover();
			BookDto actualBook = actualList[0];
			if (actualBook.Author != expectedBook.Author || actualBook.Genre != expectedBook.Genre || actualBook.Id != expectedBook.Id || actualBook.Title != expectedBook.Title)
				actual = false;

			// 
			Assert.AreEqual(expected, actual);
		}

	}
}