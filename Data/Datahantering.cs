using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Data
{
    public class Datahantering
    {

        public List<string> load(string namn)
        {
            var xml = XDocument.Load(namn + ".xml");
            var list = (from c in xml.Descendants("Title") select c.Value).ToList();
            return list;
        }

        public IEnumerable<string> loadSpecific(string namn, string titel)
        {
            XDocument xmlDoc = XDocument.Load(namn + ".xml");
            var items = from item in xmlDoc.Descendants("FeedItem")
                where item.Element("Title").Value == titel
                select item.Element("Description").Value;

            return items;
        }

        public IEnumerable<string> spelaUppValdPodcast(string namn, string titel)
        {
            XDocument xmlDoc = XDocument.Load(namn + ".xml");
            var items = from item in xmlDoc.Descendants("FeedItem")
                where item.Element("Title").Value == titel
                select item.Element("Enclosure").Value;

            return items;
        }

        public void uppdateraSpelad(string namn, string titel)
        {
            XDocument xmlDoc = XDocument.Load(namn + ".xml");
            var items = from item in xmlDoc.Descendants("FeedItem")
                        where item.Element("Title").Value == titel
                        select item;

            foreach (XElement itemElement in items)
            {
                itemElement.SetElementValue("Uppspelad", "Ja");
            }

            xmlDoc.Save(namn + ".xml");
        }

        public IEnumerable<string> ListaUppspeladEllerEj(string namn, string titel)
        {
            XDocument xmlDoc = XDocument.Load(namn + ".xml");
            var items = from item in xmlDoc.Descendants("FeedItem")
                        where item.Element("Title").Value == titel
                        select item.Element("Uppspelad").Value;

            return items;
        }

        public List<string> GetAllFeeds()
        {
            List<string> namn = new List<string>();
            string name = "";
            foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.xml"))
            {
                var content = Path.GetFileName(file);
                var längd = content.Length;
                name = content.Substring(0, längd - 4);
                namn.Add(name);
            }

            return namn;
        } 

        public void skapaFilenKategori()
        {
            XmlWriterSettings settingss = new XmlWriterSettings();
            settingss.Indent = true;
            settingss.IndentChars = ("\t");
            settingss.OmitXmlDeclaration = true;

            if (!Directory.Exists(@"\Kategorimap"))
            {
                Directory.CreateDirectory(@"\Kategorimap");
            }
            if (!File.Exists(@"\Kategorimap\kategorier.xml"))
            {

                using (XmlWriter writer = XmlWriter.Create(@"\Kategorimap\kategorier.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("podcasts");
                    writer.WriteStartElement("kategorierna");
                    writer.WriteElementString("kategori", "Övrigt");
                    writer.WriteEndElement();


                    writer.WriteEndElement();
                }
            }
        }

        public void InmatningKategoriFilen(string input)
        {
            XDocument xDoc = XDocument.Load(@"\Kategorimap\kategorier.xml");

            xDoc.Root.Element("kategorierna").Add(new XElement("kategori", input));

            xDoc.Save(@"\Kategorimap\kategorier.xml");
        }

        public void skapaNamnKategoriFilen()
        {
            XmlWriterSettings settingss = new XmlWriterSettings();
            settingss.Indent = true;
            settingss.IndentChars = ("\t");
            settingss.OmitXmlDeclaration = true;

            if (!File.Exists(@"\Kategorimap\namnKategori.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create(@"\Kategorimap\namnKategori.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("podcasts");
                    writer.WriteStartElement("kategorierna");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
            }
        }

        public void InmatningNamnKategoriFilen(string valdKategori, string namn)
        {
            XDocument xdoccet = XDocument.Load(@"\Kategorimap\namnKategori.xml");

            xdoccet.Root.Element("kategorierna").Add(new XElement("Item",
                new XElement("Kategori", valdKategori),
                new XElement("Namn", namn)));

            xdoccet.Save(@"\Kategorimap\namnKategori.xml");
        }

        public void DeleteFeed(string selectedFeedNamn)
        {
            string fileName = selectedFeedNamn + ".xml";
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            try
            {
                File.Delete(fileName);
            }
            catch (FormatException)
            {


            }
        }

        public void DeleteNameCategory(string namn)
        {
            XDocument doc = XDocument.Load(@"\Kategorimap\namnKategori.xml");
            foreach (XElement xe in doc.Elements("podcasts").Elements("kategorierna").Elements("Item"))
            {
                if ((string)xe.Element("Namn") == namn)
                {
                    xe.Remove();
                    doc.Save(@"\Kategorimap\namnKategori.xml");                                      
                }
            }
        }

        public void taBortKategori(string kategori)
        {
            XDocument doc = XDocument.Load(@"\Kategorimap\namnKategori.xml");
            foreach (XElement xe in doc.Elements("podcasts").Elements("kategorierna").Elements("Item"))
            {
                if (xe.Element("Kategori").Value == kategori)
                {
                    xe.Remove();
                    doc.Save(@"\Kategorimap\namnKategori.xml");
                }
            }
            XDocument xdoc = XDocument.Load(@"\Kategorimap\kategorier.xml");
            foreach (XElement xe in xdoc.Element("podcasts").Element("kategorierna").Elements("kategori"))
            {
                if (xe.Value == kategori)
                {
                    xe.Remove();
                    xdoc.Save(@"\Kategorimap\kategorier.xml");
                }
            }
        }

        public void BytNamnPaKategori(string kategori, string namn)
        {
            XDocument xDoc = XDocument.Load(@"\Kategorimap\namnKategori.xml");
            foreach (XElement xe in xDoc.Element("podcasts").Element("kategorierna").Elements("Item").Elements("Kategori"))
            {
                if (xe.Value == kategori)
                {
                    xe.SetValue(namn);
                    xDoc.Save(@"\Kategorimap\namnKategori.xml");
                }
            }

            XDocument xmlDoc = XDocument.Load(@"\Kategorimap\kategorier.xml");
            foreach (XElement xe in xmlDoc.Element("podcasts").Element("kategorierna").Elements("kategori"))
            {
                if (xe.Value == kategori)
                {
                    xe.SetValue(namn);
                    xmlDoc.Save(@"\Kategorimap\kategorier.xml");
                }
            }

     
        }


    }
}
