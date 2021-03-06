﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBookmarks.Classes
{
    public enum Browser
    {
        Chrome, IE, Firefox, Edge
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
                case Browser.Edge:
                    OpenWithEdge(url);
                    break;
                default:
                    throw new NotImplementedException("Cannot open in this browser yet as it has not been implemented yet");
            }
        }

        private bool OpenWithEdge(string url)
        {
            try
            {
                Process.Start("microsoft-edge:" + url);
                return true;
            }
            catch (Exception)
            {
                OpenWithDefault(url);
                return false;
            }
        }

        private bool OpenWithChrome(string url)
        {
            try
            {
                Process.Start("chrome.exe", url);
                return true;
            }
            catch (Exception)
            {
                OpenWithDefault(url);
                return false;
            }
        }
        
        private bool OpenWithIE(string url)
        {
            try
            {
                Process.Start("\"C:\\Program Files\\Internet Explorer\\iexplore.exe\"", url);
                return true;
            }
            catch (Exception)
            {
                OpenWithDefault(url);
                return false;
            }
        }

        private bool OpenWithDefault(string url)
        {
            return OpenWithIE(url);
        }
    } 
}
