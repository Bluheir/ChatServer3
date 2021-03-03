using System;

namespace ChatServer3
{
	public class UserCredentials : IDBType<String>
	{
		
		public ulong UserId { get; set; }
		public string Username { get; private set; }
		public string Password { get; private set; }

		string IDBType<string>.Key => Username;

		public UserCredentials(string username, string password, ulong userId)
		{
			Username = username;
			Password = password;
			UserId = userId;
		}
	}
}
