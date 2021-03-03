using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChatServer3
{
	public class JsonDatabase<T, TKey> where T : IDBType<TKey>
	{
		private readonly string filePath;
		private Dictionary<TKey, T> dic;

		public JsonDatabase(string path)
		{
			filePath = path;
			dic = new Dictionary<TKey, T>();
		}

		public void Load()
		{
			if(!File.Exists(filePath))
			{
				return;
			}

			var json = File.ReadAllText(filePath);

			dic = JsonConvert.DeserializeObject<Dictionary<TKey, T>>(json);
		}

		public void Save()
		{
			var json = JsonConvert.SerializeObject(dic);

			File.WriteAllText(filePath, json);
		}

		public T GetOrAdd(TKey key, Func<TKey, T> valueFactory)
		{
			if(!dic.ContainsKey(key))
			{
				dic.Add(key, valueFactory(key));
			}

			return dic[key];
		}
	}
}