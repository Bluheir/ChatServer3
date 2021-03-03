using Newtonsoft.Json;

namespace ChatServer3.Helpers.Messages
{
	public class HelloMessage
	{
		[JsonProperty("username")]
		public string Username { get; private set; }

		[JsonProperty("password")]
		public string Password { get; private set; }
	}
}
