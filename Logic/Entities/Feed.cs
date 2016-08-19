using System;
using System.Collections.Generic;
using Logic.Readers;

namespace Logic.Entities
{
    public class Feed : IEntity
    {
        public string url;
        public Guid Id { get; set; }
        public List<FeedItem> Items { get; set; }
    }

    
    
}