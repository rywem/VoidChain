using System;
namespace VoidChainLib.Objects
{
	public static class Extensions
	{
		public static string ToHex(this byte[] bytes)
		{
			char[] c = new char[bytes.Length * 2];

			byte b;

			for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
			{
				b = ((byte)(bytes[bx] >> 4));
				c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

				b = ((byte)(bytes[bx] & 0x0F));
				c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
			}
			return new string(c);
		}

		public static byte[] HexToBytes(this string str)
		{
			if (str.Length == 0 || str.Length % 2 != 0)
				return new byte[0];

			byte[] buffer = new byte[str.Length / 2];
			char c;
			for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
			{
				// Convert first half of byte
				c = str[sx];
				buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

				// Convert second half of byte
				c = str[++sx];
				buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
			}
			return buffer;
		}

		/// <summary>
		/// This might crash. I don't fully understand its purpose yet. 
		/// </summary>
		/// <param name="buf">Buffer.</param>
		public static void ByteSwap(this byte[] buf)
		{
			int i;
			byte temp;
			int length = buf.Length;

			for (i = 0; i < (length / 2); i++)
			{
				temp = buf[i];
				buf[i] = buf[length - i - 1];
				buf[length - i - 1] = temp;
			}
		}

		public static byte ToByte(this uint number)
		{
			return Convert.ToByte(number);
		}

		public static uint ToUint32(this byte _byte)
		{
			return Convert.ToUInt32(_byte);
		}

		public static ulong ToULong(this byte _byte)
		{
			return Convert.ToUInt64(_byte);
		}

	}
}
