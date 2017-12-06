using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.Classes
{
    class UrlChecker
    {
        public static bool IsValidUrl(string url)
        {
            return url.StartsWith("http://") || url.StartsWith("https://");
        }
    }
}
