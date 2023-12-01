using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace ConvertTextData_bin_cho_mh
{	public enum Language
	{
		JAPAN,
		USA, // default
		FRANCE,
		GERMAN,
		KOREA,
		CHINA_S,
		CHINA_T,

		LAST_LANG
	}


	public class ConvertTextData
	{
		public static Language _debug_int2lang(int l)
        {
			if(l >= 0 && l < (int)Language.LAST_LANG)
            {
				return (Language)l;
            }
			return Language.USA;
        }


		public ConvertTextData() {}

		public byte[] ToBytes(Language language)
        {
			// Yonixw Debug
			List<byte> _b = new List<byte>();

			foreach (Data _d in this.data_)
            {
				_b.AddRange(BitConverter.GetBytes((UInt16)_d.id));

				string source = String.Join(",", _d.text.Select((s)=>s.Replace(",", "，"))) ;

				if (language != Language.JAPAN)
                {
					source = EnToHalf(source, language, reverse: true);
                }

				foreach(char c in source)
                {
					_b.AddRange(BitConverter.GetBytes(c));
                }
				_b.AddRange(BitConverter.GetBytes('\0'));
            }

			return _b.ToArray();
        }

		public ConvertTextData(byte[] bytes, Language language)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ConvertTextData.Data> list = new List<ConvertTextData.Data>();
			int num = 0;
			while (num + 2 <= bytes.Length)
			{
				ConvertTextData.Data item = default(ConvertTextData.Data);
				item.id = BitConverter.ToUInt16(bytes, num);
				num += 2;

				while (num + 2 <= bytes.Length)
				{
					char c = BitConverter.ToChar(bytes, num);
					num += 2;
					if (c == '\0')
					{
						break;
					}
					stringBuilder.Append(c);
				}

				string text;
				switch (language)
				{
					case Language.JAPAN:
						text = stringBuilder.ToString();
						break;
					case Language.USA:
					case Language.FRANCE:
					case Language.GERMAN:
					case Language.KOREA:
					case Language.CHINA_S:
					case Language.CHINA_T:
						goto IL_9E;
					default:
						goto IL_9E;
				}
			IL_B1:
				text = text.Replace('φ', ' ');
				item.text = text.Split(',').Select((s)=>s.Replace( "，", ",")).ToArray();
				stringBuilder.Length = 0;
				list.Add(item);
				continue;
			IL_9E:
				text = EnToHalf(stringBuilder.ToString(), language);
				goto IL_B1;
			}
			this.data_ = list.ToArray();
		}

		public ConvertTextData.Data[] data_;

		public struct Data
		{
			[XmlAttribute]
			public ushort id;

			public string[] text;
		}

		public static string EnToHalf(string in_text, Language in_language, bool reverse = false)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{
				"１",
				"1"
			},
			{
				"２",
				"2"
			},
			{
				"３",
				"3"
			},
			{
				"４",
				"4"
			},
			{
				"５",
				"5"
			},
			{
				"６",
				"6"
			},
			{
				"７",
				"7"
			},
			{
				"８",
				"8"
			},
			{
				"９",
				"9"
			},
			{
				"０",
				"0"
			},
			{
				"Ａ",
				"A"
			},
			{
				"Ｂ",
				"B"
			},
			{
				"Ｃ",
				"C"
			},
			{
				"Ｄ",
				"D"
			},
			{
				"Ｅ",
				"E"
			},
			{
				"Ｆ",
				"F"
			},
			{
				"Ｇ",
				"G"
			},
			{
				"Ｈ",
				"H"
			},
			{
				"Ｉ",
				"I"
			},
			{
				"Ｊ",
				"J"
			},
			{
				"Ｋ",
				"K"
			},
			{
				"Ｌ",
				"L"
			},
			{
				"Ｍ",
				"M"
			},
			{
				"Ｎ",
				"N"
			},
			{
				"Ｏ",
				"O"
			},
			{
				"Ｐ",
				"P"
			},
			{
				"Ｑ",
				"Q"
			},
			{
				"Ｒ",
				"R"
			},
			{
				"Ｓ",
				"S"
			},
			{
				"Ｔ",
				"T"
			},
			{
				"Ｕ",
				"U"
			},
			{
				"Ｖ",
				"V"
			},
			{
				"Ｗ",
				"W"
			},
			{
				"Ｘ",
				"X"
			},
			{
				"Ｙ",
				"Y"
			},
			{
				"Ｚ",
				"Z"
			},
			{
				"ａ",
				"a"
			},
			{
				"ｂ",
				"b"
			},
			{
				"ｃ",
				"c"
			},
			{
				"ｄ",
				"d"
			},
			{
				"ｅ",
				"e"
			},
			{
				"ｆ",
				"f"
			},
			{
				"ｇ",
				"g"
			},
			{
				"ｈ",
				"h"
			},
			{
				"ｉ",
				"i"
			},
			{
				"ｊ",
				"j"
			},
			{
				"ｋ",
				"k"
			},
			{
				"ｌ",
				"l"
			},
			{
				"ｍ",
				"m"
			},
			{
				"ｎ",
				"n"
			},
			{
				"ｏ",
				"o"
			},
			{
				"ｐ",
				"p"
			},
			{
				"ｑ",
				"q"
			},
			{
				"ｒ",
				"r"
			},
			{
				"ｓ",
				"s"
			},
			{
				"ｔ",
				"t"
			},
			{
				"ｕ",
				"u"
			},
			{
				"ｖ",
				"v"
			},
			{
				"ｗ",
				"w"
			},
			{
				"ｘ",
				"x"
			},
			{
				"ｙ",
				"y"
			},
			{
				"ｚ",
				"z"
			},
			{
				"\u3000",
				" "
			},
			{
				"．",
				"."
			},
			{
				"，",
				","
			},
			{
				"＇",
				"'"
			},
			{
				"！",
				"!"
			},
			{
				"（",
				"("
			},
			{
				"）",
				")"
			},
			{
				"－",
				"-"
			},
			{
				"／",
				"/"
			},
			{
				"？",
				"?"
			},
			{
				"∠",
				"_"
			},
			{
				"［",
				"["
			},
			{
				"］",
				"]"
			},
			{
				"“",
				"\""
			},
			{
				"”",
				"\""
			},
			{
				"＂",
				"\""
			},
			{
				"―",
				"-"
			},
			{
				"‘",
				"'"
			},
			{
				"’",
				"'"
			},
			{
				"：",
				":"
			},
			{
				"＊",
				"*"
			},
			{
				"；",
				";"
			},
			{
				"＄",
				"$"
			},
			{
				"Ы",
				"©"
			},
			{
				"∋",
				"è"
			},
			{
				"∈",
				"é"
			},
			{
				"∀",
				"á"
			},
			{
				"∧",
				"à"
			},
			{
				"⊆",
				"ç"
			},
			{
				"⊂",
				"Ç"
			},
			{
				"Ц",
				"û"
			},
			{
				"↑",
				"î"
			},
			{
				"α",
				"â"
			},
			{
				"л",
				"ñ"
			},
			{
				"↓",
				"ï"
			},
			{
				"ε",
				"ê"
			}
		};
			if (in_language == Language.KOREA)
			{
				dictionary.Add("＜", "<");
				dictionary.Add("＞", ">");
				dictionary.Add("・", ".");
				dictionary.Add("‥", "..");
				dictionary.Add("…", "...");
				dictionary.Add("～", "~");
			}
			else if (in_language == Language.CHINA_S || in_language == Language.CHINA_T)
			{
				dictionary = new Dictionary<string, string>
			{
				{
					"φ",
					" "
				},
				{
					"‧",
					" ・ "
				}
			};
				if (in_language == Language.CHINA_S)
				{
					dictionary.Add("？", " ?");
					dictionary.Add("！", " !");
				}
				if (in_language == Language.CHINA_T)
				{
					dictionary.Add("／", " ／ ");
				}
			}

			if (reverse)
			{
				dictionary = DictionaryPairUtils.ReverseDict(dictionary);
			}
			dictionary.Remove(","); // to keep line spliting
			dictionary.Remove("，"); // to keep line spliting

			string text = string.Empty;
			switch (in_language)
			{
				case Language.JAPAN:
					text = in_text;
					break;
				case Language.USA:

				case Language.FRANCE:
				case Language.GERMAN:
				case Language.KOREA:
				case Language.CHINA_S:
				case Language.CHINA_T:
					for (int i = 0; i < in_text.Length; i++)
					{
						if (dictionary.ContainsKey(in_text[i].ToString()))
						{
							text += dictionary[in_text[i].ToString()];
						}
						else
						{
							text += in_text[i].ToString();
						}
					}
					break;
			}
			return text;
		}
	
	}

}