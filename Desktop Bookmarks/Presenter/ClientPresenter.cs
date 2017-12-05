using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DesktopBookmarks.View;
using DesktopBookmarks.Model;

namespace DesktopBookmarks.Presenter
{
    class ClientPresenter
    {
        private IClientView _view;
        private BookmarksTree _bookmarksTree;

        public ClientPresenter(IClientView view)
        {
            _view = view;
            _view.AddNewFolder += AddNewFolder;
            _view.AddNewBookmark += AddNewBookmark;

            _bookmarksTree = new BookmarksTree();
        }

        private void AddNewBookmark(object sender, AddBookmarkEventArgs e)
        {
            string id = Guid.NewGuid().ToString();
            Bookmark bookmark = new Bookmark(e.URL, e.Label, id);
            if (string.IsNullOrEmpty(e.ParentID))
            {
                _bookmarksTree.Bookmarks.Add(bookmark);
            }
            else
            {
                IModelType parentNode = GetModelTypeById(e.ParentID);
                if (parentNode.GetType() == typeof(Folder))
                {
                    ((Folder)parentNode).Children.Add(bookmark);
                }
                else
                {
                    return;
                }
            }

            _view.AddBookmarkTreeNode(bookmark, e.ParentID);

            _view.LabelText = "";
            _view.UrlText = "";
        }

        private void AddNewFolder(object sender, AddFolderEventArgs e)
        {
            string id = Guid.NewGuid().ToString();
            Folder folder = new Folder(e.Label, id);

            if(string.IsNullOrEmpty(e.ParentId))
            {
                _bookmarksTree.Bookmarks.Add(folder);
            } else
            {
                IModelType parentNode = GetModelTypeById(e.ParentId);
                if(parentNode.GetType() == typeof(Folder))
                {
                    ((Folder)parentNode).Children.Add(folder);
                } else
                {
                    throw new InvalidOperationException();
                }
            }

            _view.AddFolderTreeNode(folder, e.ParentId);
        }
        
        private IModelType GetModelTypeById(string id)
        {
            foreach(IModelType type in _bookmarksTree.Bookmarks)
            {
                IModelType t = GetModelTypeById(type, id);
                if (t != null) return t;
            }

            return null;
        }

        private IModelType GetModelTypeById(IModelType type, string id)
        {
            if (type.Id == id) return type;

            if (type.GetType() == typeof(Bookmark))
                return null;

            foreach(IModelType t in ((Folder)type).Children.Where(a => a.GetType() == typeof(Folder)))
            {
                IModelType t2 = GetModelTypeById(t, id);
                if (t2 != null) return t2;
            }

            return null;
        }
    }
}
