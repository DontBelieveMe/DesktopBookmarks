﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopBookmarks.Model;

namespace DesktopBookmarks.View
{
    interface IClientView
    {
        event EventHandler<AddFolderEventArgs> AddNewFolder;
        event EventHandler<AddBookmarkEventArgs> AddNewBookmark;
        event EventHandler<OpenBookmarkEventArgs> OpenBookmark;
        event EventHandler<RemoveNodeEventArgs> RemoveNode;

        string LabelText { get; set; }
        string UrlText { get; set; }

        void AddFolderTreeNode(Folder bookmark, string parentId);
        void AddBookmarkTreeNode(Bookmark bookmark, string parentId);
        void RemoveNodeFromTree(string id);
    }
}
