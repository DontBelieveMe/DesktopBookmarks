using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.View
{
    public class OpenBookmarkEventArgs : EventArgs
    {
        public string IdToOpen { get; private set; }

        public OpenBookmarkEventArgs(string idToOpen)
        {
            IdToOpen = idToOpen;
        }
    }
}
