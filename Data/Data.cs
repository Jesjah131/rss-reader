using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Data
{
    public class Data<T>
    {
        public void save(T item, string namn)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter textWriter = new StreamWriter(namn + ".xml");

            xmlSerializer.Serialize(textWriter, item);
            textWriter.Close();
        }

        public List<string> load(string namn)
        {
            var xml = XDocument.Load(@"\bin\Debug\" + namn);
            var list = (from c in xml.Descendants("FeedItem") select c.Value).ToList();
            return list;
        }
    }
}
