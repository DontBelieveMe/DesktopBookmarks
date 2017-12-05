using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.Model
{
    public class Bookmark : IModelType
    {
        public string Url { get; set; }
        public string Label { get; set; }
        public string Id { get; set; }

        
        public Bookmark(string url, string label, string id)
        {
            this.Url = url;
            this.Label = label;
            this.Id = id;
        }
    }
}
