using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer3.Helpers.Messages
{
	class HelloMessageResponse
	{
		[JsonProperty("userId")]
		public ulong UserId { get; private set; }
	}
}
