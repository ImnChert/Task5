namespace Task5
{
	public class DataBaseLauncher
	{
		private static string _stringConnection;

		public DataBaseLauncher(string connectionString)
		{
			StringConnection = connectionString;
		}

		public static string StringConnection { get => _stringConnection; set => _stringConnection = value; }
	}
}
