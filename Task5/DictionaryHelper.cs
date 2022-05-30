namespace Task5
{
	public class DictionaryHelper
	{
		/// <summary>
		/// Add to the dictionary values that represent lists
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="K"></typeparam>
		/// <param name="dict"></param>
		/// <param name="keyToAdd"></param>
		/// <param name="elemToAdd"></param>
		/// <returns></returns>
		public static Dictionary<T, List<K>> AddToDictWithValueList<T, K>(Dictionary<T, List<K>> dict, T keyToAdd, K elemToAdd)
		{
			if (dict.ContainsKey(keyToAdd))
			{
				dict[keyToAdd].Add(elemToAdd);
			}
			else
			{
				List<K> newValue = new List<K> { };
				dict[keyToAdd] = newValue;
				dict[keyToAdd].Add(elemToAdd);
			}
			return dict;
		}

	}
}
