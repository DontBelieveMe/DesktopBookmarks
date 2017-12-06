using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.View
{
    public class RemoveNodeEventArgs : EventArgs
    {
        public string IdToRemove { get; private set; }

        public RemoveNodeEventArgs(string id)
        {
            IdToRemove = id;
        }
    }
}
