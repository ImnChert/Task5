namespace Task5
{
	public class DAOFactory : ICreateDaoObject
	{
		public DAOFactory()
		{

		}
		/// <summary>
		/// Return single instance of the Dao object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="stringConnection"></param>
		/// <returns></returns>
		public IDao<T> Create<T>(string stringConnection) where T : class
		{
			return GenericDao<T>.GetDao(stringConnection);
		}
	}
}
