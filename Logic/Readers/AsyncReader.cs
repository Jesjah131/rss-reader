using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;
using Logic.Readers;

namespace Logic.Readers
{
    public class AsyncReader
    {
        public async Task ReadAsync()
        {
            var reader = new RssReader();
            string url = (@"jesper.xml");
            reader.Read(url);

            await Task.Delay(30000);
        }
    }
}
