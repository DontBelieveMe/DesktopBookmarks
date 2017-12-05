using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.View
{
    public class AddBookmarkEventArgs : EventArgs
    {
        public string URL { get; private set; }
        public string Label { get; private set; }
        public string ParentID { get; private set; }

        public AddBookmarkEventArgs(string url, string label, string parentId)
        {
            this.URL = url;
            this.Label = label;
            this.ParentID = parentId;
        }
    }
}
