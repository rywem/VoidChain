using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VoidChainLib.Objects
{
	public static class Extensions
	{
        public static string GetSHA256(this string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return bytes.GetSHA256AsString();
        }

        public static string GetSHA256AsString(this byte[] input)
        {
            byte[] hash = input.GetSHA256();
			string hashString = string.Empty;
			foreach (byte x in hash)
			{
				hashString += String.Format("{0:x2}", x);
			}
			return hashString;
        }
        public static byte[] GetSHA256(this byte[] bytes)
        {
			SHA256Managed hashstring = new SHA256Managed();
			return hashstring.ComputeHash(bytes);
        }
        public static IEnumerable<byte> ToBytes(this uint number)
        {
            foreach (var item in BitConverter.GetBytes(number))
            {
                yield return item;
            }
        }

		public static IEnumerable<byte> ToBytes(this ulong number)
		{
			//var x = number.SelectMany(BitConverter.GetBytes);
			foreach (var item in BitConverter.GetBytes(number))
			{
				yield return item;
			}
		}
		public static uint ToUInt32(this IEnumerable<byte> _bytes)
		{
			return ToUInt32(_bytes.ToArray());
		}
		public static uint ToUInt32(this List<byte> _bytes)
		{
			return ToUInt32(_bytes.ToArray());
		}

        public static uint ToUInt32(this byte[] _bytes)
        {
            return BitConverter.ToUInt32(_bytes, 0);
        }
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

        public static List<string> ToHexList(this byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;
            List<string> output = new List<string>();
            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                List<char> input = new List<char>();
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
                input.Add(c[cx]);
                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
                input.Add(c[cx]);
                output.Add(new string(input.ToArray()));
            }
            return output;
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
        public static byte[] ByteSwap(this byte[] buf)
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
            return buf;
        }

        public static uint ToUnixTime(this DateTime time)
        {
			return (UInt32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;	
        }

        public static DateTime FromUnixTime(this uint unixTimeStamp, bool AsLocal = false)
        {
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            if (AsLocal == true)
            {
                
                return dtDateTime.ToLocalTime();
            }
            else
            {
                return dtDateTime;
            }
        }
	}
}
