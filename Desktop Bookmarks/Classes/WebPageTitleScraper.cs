using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace DesktopBookmarks.Classes
{
    class WebPageTitleScraper
    {
        public string PageTitle { get; private set; }

        private string _url;
        
        public WebPageTitleScraper(string url)
        {
            _url = url;
        }
    }
}
