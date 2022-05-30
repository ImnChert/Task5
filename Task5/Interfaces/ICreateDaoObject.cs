namespace Task5
{
	public interface ICreateDaoObject
	{
		IDao<T> Create<T>(string stringConnection) where T : class;
	}
}
