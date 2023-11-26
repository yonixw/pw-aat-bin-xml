using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTextData_bin_cho_mh
{
	public class MessageHeader
	{
		public MessageHeader() { }
		public MessageHeader(byte[] bytes)
		{
			Encoding ascii = Encoding.ASCII;
			this.dictionary_ = new Dictionary<string, ushort>();
			int i = 0;
			while (i < bytes.Length)
			{
				int num = i;
				while (i < bytes.Length && bytes[i] != 0)
				{
					i++;
				}
				string @string = ascii.GetString(bytes, num, i - num);
				i++;
				if (i + 2 <= bytes.Length)
				{
					ushort value = BitConverter.ToUInt16(bytes, i);
					i += 2;
					if (!this.dictionary_.ContainsKey(@string))
					{
						this.dictionary_.Add(@string, value);
					}
				}
			}
		}

		public byte[] ToBytes()
        {
			// Yonixw Debug

			List<byte> _b = new List<byte>();

			Encoding ascii = Encoding.ASCII;

			foreach(KeyValuePair<string,ushort> kv in dictionary_)
            {
				_b.AddRange(ascii.GetBytes(kv.Key));
				_b.Add((byte)0);
				_b.AddRange(BitConverter.GetBytes((UInt16)kv.Value));
            }

			return _b.ToArray();
		}

	

		public Dictionary<string, ushort> dictionary_;
	}
}
