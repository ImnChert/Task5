using Task5;

namespace Task5Test
{
	[TestClass]
	public class BookReportDtoTests
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
		/// Tests GetAllBookReport method
		/// </summary>
		[TestMethod]
		public void GetAllBookReport_ShoudReturnListOfBookReports()
		{
			// Arrange
			bool expected = true;
			bool actual = true;

			// Act
			List<BookReportDto> bookDtos = BookReportDtoService.GetAllReports();
			if (bookDtos == null || bookDtos.Count != 4)
				actual = false;
			// Assert
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		/// Tests GroupBooksOfEachSubscriberByGenre method
		/// </summary>
		[TestMethod]
		public void GroupBooksOfEachSubscriberByGenre_ShouldReturnDictionaryWithSubscribersAndHisBooksThatWasGivenInGivenTimeLineGroupedByGenre()
		{
			// Arrange
			List<BookDto> bookDtos = BookDtoService.GetAllBooks();
			List<SubscriberDto> subscribers = SubscriberDtoService.GetAllSubcribers();
			List<(SubscriberDto, (string, List<BookDto>))> expectedToList = new List<(SubscriberDto, (string, List<BookDto>))> { (subscribers[0], ("Novel", new List<BookDto> { bookDtos[0], bookDtos[1] })) };
			bool actual = true;
			bool expected = true;
			int i = 0;

			// Act
			Dictionary<SubscriberDto, IEnumerable<IGrouping<string, BookDto>>> actualDict = BookReportDtoService.GroupBooksOfEachSubscriberByGenre(new DateTime(2016, 1, 1), new DateTime(2017, 6, 17));
			foreach (var elem in actualDict)
			{
				if (elem.Key.Id != expectedToList[0].Item1.Id)
					actual = false;
				foreach (var group in elem.Value)
				{
					if (group.Key != expectedToList[0].Item2.Item1)
						actual = false;
					foreach (var book in group)
					{
						if (book.Id != expectedToList[0].Item2.Item2[i].Id)
							actual = false;
						i++;
					}
					i = 0;
				}
			}
			// Assert
			Assert.AreEqual(expected, actual);

		}
	}
}
