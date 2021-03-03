using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServer3.Helpers.Parsing
{
	public class ByteParser
	{
		public int Position { get; set; } // Current position in byte parser
		private readonly List<Byte> bytes; // Mutable list of bytes for appending bytes at start

		public ByteParser(byte[] array)
		{
			bytes = new List<byte>(array); // Setup list
			Position = 0; // Set position to 0
		}

		public int GetInt(bool shiftPos = true)
		{
			var b1 = bytes[Position];
			var b2 = bytes[Position + 1];
			var b3 = bytes[Position + 2];
			var b4 = bytes[Position + 3];

			if(shiftPos)
			{
				Position += 4;
			}

			// Convert bytes to Int
			return BitConverter.ToInt32(new byte[] { b1, b2, b3, b4 }, 0);
		}
		public ulong GetUlong(bool shiftPos = true)
		{
			var b = BitConverter.ToUInt64(GetBytes(Position, 8), 0);

			if (shiftPos)
			{
				Position += 8;
			}

			return b;
		}
		public ulong GetUlongStart()
		{
			return BitConverter.ToUInt64(GetBytes(0, 8), 0);
		}
		public byte[] GetBytes(int indexStart, int count)
		{
			return bytes.GetRange(indexStart, count).ToArray();
		}
		public byte[] GetBytes(int count, bool shiftPos = true)
		{
			var b = GetBytes(Position, count);

			if (shiftPos)
				Position += count;

			return b;
		}

		public void AddToStart(byte[] arr, bool setPos = true)
		{
			bytes.InsertRange(0, arr);

			if(setPos)
				Position += arr.Length;
		}

		public byte[] GetRemainingBytes()
		{
			var l = bytes.GetRange(Position, bytes.Count - Position - 1);

			return l.ToArray();
		}

		public byte[] ToByteArray()
		{
			return bytes.ToArray();
		}

		public void AddToStart(short[] arr, bool setPos = true)
		{
			if (setPos)
				Position += arr.Length << 1;

			bytes.InsertRange(0, FromShortArray(arr));
		}

		public static short[] ToShortArray(byte[] arr)
		{
			if (arr.Length == 0)
				return new short[0];

			var l = new List<short>();

			var ind = arr.Length - 1;

			for(int i = 0; i < arr.Length; i += 2)
			{
				if (i > ind)
					break;

				var a = arr[i];

				if (i + 1 > ind)
				{
					l.Add(a);
					break;
				}

				var b = arr[i + 1];

				var s = BitConverter.ToInt16(new byte[] { a, b }, 0);
				l.Add(s);
			}

			return l.ToArray();
		}

		public static byte[] FromShortArray(short[] arr)
		{
			if (arr.Length == 0)
				return new byte[0];

			var l = new List<byte>();

			for(int i = 0; i < arr.Length; i++)
			{
				var a = arr[i];

				var b1 = (byte)((a & 0b1111111100000000) >> 8);
				var b2 = (byte)(a & 0b0000000011111111);

				l.Add(b1);
				l.Add(b2);
			}

			return l.ToArray();
		}
	}
}
