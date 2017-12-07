using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.View
{
    public class FilterTreeEventArgs
    {
        public string SearchText { get; private set; }

        public FilterTreeEventArgs(string query)
        {
            SearchText = query;
        }
    }
}
