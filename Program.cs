using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTextData_bin_cho_mh
{
    class Program
    {
        static void Main(string[] args)
        {
            Language _lang = getLanguage(args);
            Console.WriteLine("Using language: " + _lang.ToString());

            string dbin = getFlagFileNameIfExist("--dbin=", args);
            string dxml = getFlagFileNameIfExist("--dxml=", args);

            string dmh = getFlagFileNameIfExist("--mh=", args);
            string mhxml = getFlagFileNameIfExist("--mhxml=", args);

            // Nice drag and drop:
            if (String.IsNullOrEmpty(dbin) || String.IsNullOrEmpty(dxml) 
                || String.IsNullOrEmpty(dmh) || String.IsNullOrEmpty(mhxml))
            {
                string last = args.LastOrDefault();
                if (!string.IsNullOrEmpty(last) && File.Exists(last))
                {
                    Console.WriteLine("[Found file, auto try...]");

                    if (last.EndsWith(".bin"))
                        dbin = last;
                    else if (last.EndsWith(".cho"))
                        dbin = last;
                    else if (last.EndsWith(".mh"))
                        dmh = last;
                    else if (last.EndsWith(".bin.xml"))
                        dxml = last;
                    else if (last.EndsWith(".cho.xml"))
                        dxml = last;
                    else if (last.EndsWith(".mh.xml"))
                        mhxml = last;
                }
            }

            Console.WriteLine("[Start...]");

            if (!string.IsNullOrEmpty(dbin))
            {
                Console.WriteLine("[In mode bin/cho->xml...]");

                File.WriteAllText(
                    dbin + ".xml",
                    XML.toXML((
                        new ConvertTextData(File.ReadAllBytes(dbin), _lang)).data_
                    )
                );
            }
            else if (!string.IsNullOrEmpty(dmh))
            {
                Console.WriteLine("[In mode mh->xml...]");

                File.WriteAllText(
                    dmh + ".xml",
                    XML.toXML(
                       DictionaryPairUtils.ToPairs(
                            (new MessageHeader(File.ReadAllBytes(dmh)).dictionary_)
                )));
            }
            else if (!string.IsNullOrEmpty(dxml))
            {
                Console.WriteLine("[In mode xml->bin/cho...]");

                ConvertTextData _ctd = new ConvertTextData();
                _ctd.data_ =
                    XML.fromXML<ConvertTextData.Data[]>(File.ReadAllText(dxml));
                File.WriteAllBytes(
                    dxml.Replace(".xml",""),
                    _ctd.ToBytes(_lang)) ;
            }
            else if (!string.IsNullOrEmpty(mhxml))
            {
                Console.WriteLine("[In mode xml->mh...]");

                MessageHeader _mh = new MessageHeader();
                _mh.dictionary_ = DictionaryPairUtils.FromPairs(
                        XML.fromXML<List<Pair<string,ushort>>>(File.ReadAllText(mhxml))
                    );
                File.WriteAllBytes(mhxml.Replace(".xml", ""), _mh.ToBytes());
            }
            else
            {
                Help();
                Console.ReadKey();
            }

            Console.WriteLine("[DONE!]");
        }

        public static Language getLanguage(string[] args)
        {
            string langCodeStr = args.FirstOrDefault((s) => s.StartsWith("--lang"));
            int langCode = -1;
            if (langCodeStr != null)
            {
                int.TryParse(langCodeStr.Substring("--lang".Length), out langCode);
            }
            return ConvertTextData._debug_int2lang(langCode);
        }

        public static string getFlagFileNameIfExist(string prefix, string[] args)
        {
            string flagFileName = args.FirstOrDefault((s) => s.StartsWith(prefix));
            if (flagFileName != null)
            {
                string file = flagFileName.Substring(prefix.Length);
                if (File.Exists(file))
                {
                    Console.WriteLine("[" + prefix + "] Input file: '" + file + "'");
                    return file;
                }
                else 
                {
                    Console.WriteLine("[" + prefix + "] File not found: '" + file + "'");
                }
            }
            return "";
        }

        public static void Help ()
        {
            string[] help = new string[]
            {
                "HELP (AAT Unity Trilogy - Bin/Cho/Mh tool)",
                "by github/yonixw",
                "",
                "Default language is USA, you can change it with --langX",
                "(language replace some known chars in the game)",
                "Where X is:",
                "\tJAPAN=0",
                "\tUSA=1 (default)",
		        "\tFRANCE=2",
                "\tGERMAN=3",
                "\tKOREA=4",
                "\tCHINA_S=5",
                "\tCHINA_T=6",
                "",
                "Convert *.bin/cho with '--dbin=$FILE.*' to .xml in same folder",
                "Convert back with '--dxml=$FILE.xml'",
                "",
                "Convert *.mh with '--mh=$FILE.*' to .xml in same folder",
                "Convert back with '--mhxml=$FILE.xml'",
                "",
                "You can also pass $FILE as last argument for auto try",
                "",
                "In some places a comma (,) is used to split lines. To print a comma",
                "\tin game, use 'Full width comma(U+FF0C)' char. See: https://symbl.cc/en/FF0C/"
            };
            Console.WriteLine(String.Join("\n", help));
        }
    }
}
