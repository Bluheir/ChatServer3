using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer3.Helpers.Messages
{
	public class MessageBase
	{
		
		[JsonProperty("u")]
		public int ResponseType { get; private set; }
		
		[JsonProperty("d")]
		public JObject ResponseData { get; private set; }

		public bool TryConvert<T>(out T data)
		{
			try
			{
				data = ResponseData.ToObject<T>();
				return true;
			}
			catch
			{
				data = default;
				return false;
			}
		}

		public MessageBase(object value, int responseType)
		{
			ResponseType = responseType;
			ResponseData = new JObject(value);
		}
		public MessageBase(string json)
		{
			var b = JsonConvert.DeserializeObject<MessageBase>(json);

			ResponseType = b.ResponseType;
			ResponseData = b.ResponseData;
		}

		/// <summary>
		/// Serialize the current object to a JSON string
		/// </summary>
		/// <returns>Returns json value of this object</returns>
		public string Serialize()
		{
			// Convert to json string
			var s = JsonConvert.SerializeObject(this);

			return s;
		}
	}
}
