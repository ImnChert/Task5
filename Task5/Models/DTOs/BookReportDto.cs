namespace Task5
{
	public class BookReportDto
	{
		public int Id { get; set; }

		public DateTime DateOfGiving { get; set; }

		public bool ReturnSatus { get; set; }

		public string StateOfBook { get; set; }

		public BookDto Book { get; set; }

		public SubscriberDto Subscriber { get; set; }
	}
}
