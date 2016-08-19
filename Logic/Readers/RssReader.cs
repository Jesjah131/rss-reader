using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Logic.Entities;
using System.Linq;
using Logic.Service.Validation;

namespace Logic.Readers
{
    internal class RssReader : IReader
    {
        public IEnumerable<FeedItem> Read(string url)
        {
            //SyndicationFeed syndicationFeed = SyndicationFeed.Load(XmlReader.Create(url));

            //syndicationFeed.Items.First().Links.First(l => l.BaseUri.ToString().EndsWith("mp3")).BaseUri.ToString();

            //WebClient client = new WebClient();

            //client.DownloadFileAsync();

                XDocument xDoc = XDocument.Load(url);

                var document = from x in xDoc.Descendants("item")
                    select new FeedItem
                    {
                        Title = x.Element("title").Value,
                        Description = x.Element("description").Value,
                        Enclosure = x.Element("enclosure").Attribute("url").Value,
                        Uppspelad = "Nej"
                    };

                return document;
            

        }
    }
}
