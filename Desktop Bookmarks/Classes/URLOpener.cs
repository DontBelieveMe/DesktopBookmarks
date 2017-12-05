using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.Classes
{
    public enum Browser
    {
        Chrome, IE, Firefox
    }

    public class URLOpener
    {
        public Browser OpenWith { get; private set; }

        public URLOpener(Browser openWith)
        {
            OpenWith = openWith;
        }

        public void Open(string url)
        {
            switch (OpenWith)
            {
                case Browser.Chrome:
                    OpenWithChrome(url);
                    break;
                case Browser.IE:
                    OpenWithIE(url);
                    break;
                default:
                    OpenWithDefault(url);
                    break;
            }
        }

        private void OpenWithChrome(string url)
        {
            try
            {
                Process.Start("chrome.exe", url);
            } catch(Exception)
            {
                OpenWithDefault(url);
            }
        }
        
        private void OpenWithIE(string url)
        {
            throw new NotImplementedException();

            try
            {
                Process.Start(@"c:\windows\servicepackfiles\i386\iexplore.exe", url);
            }
            catch (Exception)
            {
                OpenWithDefault(url);
            }
        }

        private void OpenWithDefault(string url)
        {
            OpenWithIE(url);
        }
    } 
}
