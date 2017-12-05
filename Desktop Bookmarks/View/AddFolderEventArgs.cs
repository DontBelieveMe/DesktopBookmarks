using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.View
{
    public class AddFolderEventArgs : EventArgs
    {
        public string Label { get; private set; }
        public string ParentId { get; private set; }

        public AddFolderEventArgs(string label, string parentId)
        {
            this.Label = label;
            this.ParentId = parentId;
        }
    }
}
