using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace ConvertTextData_bin_cho_mh
{
    public static class XML
    {
        public static string toXML<T>(T s)
        {
            XmlSerializer serializer =
                 new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, s);
                return textWriter.ToString();
            }
        }

        public static T fromXML<T>(string s)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(s))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

    }
}
