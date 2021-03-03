using EasyWebsockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer3
{
	public class ChatClient : WebSocketInstance
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public ulong UserId { get; set; }
		public int RoomId { get; set; }
		public int VoiceId { get; set; }
		public bool In { get; set; }
	}
}
