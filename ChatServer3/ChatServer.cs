using EasyWebsockets;
using System.Net.WebSockets;
using EasyWebsockets.Classes;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using ChatServer3.Helpers.Messages;

namespace ChatServer3
{
	public class ChatServer
	{
		private readonly GWebSocketServer<ChatClient> server;
		private readonly JsonDatabase<UserCredentials, string> jdb;

		public ChatServer()
		{
			// Init server
			server = new GWebSocketServer<ChatClient>("192.168.1.108", 443);

			jdb = new JsonDatabase<UserCredentials, string>("C:\\Users\\slimc\\Desktop\\db.json");

			// Init events
			server.OnConnect += OnClientConnect;
			server.OnReceive += OnMessage;
		}

		private async Task OnMessage(ChatClient client, byte[] data, WSOpcodeType opcode)
		{
			if(opcode == WSOpcodeType.Binary)
			{
				await client.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Cannot send bytes to this server");
				return;
			}
			
			if(opcode != WSOpcodeType.Text)
			{
				return;
			}

			var s = Encoding.UTF8.GetString(data);

			var obj = new MessageBase(s);

			if(!client.In)
			{
				if(!obj.TryConvert<HelloMessage>(out var h))
				{
					await client.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid hello object");
					return;
				}

				if(!h.IsValid())
				{
					await client.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Invalid hello object");
				}

				jdb.GetOrAdd(h.Username, x => new UserCredentials(x, h.Password, DateTime.Now.Ticks))
			}
		}

		private static unsafe ulong GetTicks()
		{
			
		}

		private async Task OnClientConnect(ChatClient arg)
		{
			
		}
	}
}
