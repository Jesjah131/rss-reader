using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;
using Logic.Readers;
using Data; 
using Logic.Service.Validation;

namespace Logic
{
    public class FeedHelper
    {
        public void addFeed(Feed item, string namn)
        {
            //Validering        
            var reader = new RssReader();

            IEnumerable<FeedItem> feedItems = reader.Read(item.url);

            item.Items = feedItems.ToList();

            var data = new Data<Feed>();
            data.save(item, namn);
        }

        public List<string> loadFeed(Feed item, string namn)
        {
            var data = new Data<Feed>();
            return data.load(namn);
            var Feed = new Feed();
        }

        public void deleteFeed(String namn)
        {
            var data = new Datahantering();
            data.DeleteFeed(namn);
        }
    }
}
