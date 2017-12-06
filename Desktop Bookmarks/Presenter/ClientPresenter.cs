using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DesktopBookmarks.View;
using DesktopBookmarks.Model;
using DesktopBookmarks.Classes;

namespace DesktopBookmarks.Presenter
{
    class ClientPresenter
    {
        private IClientView _view;
        private IDialogService _dialogService;
        private BookmarksTree _bookmarksTree;

        public ClientPresenter(IClientView view, IDialogService dialogService)
        {
            _view = view;
            _dialogService = dialogService;

            _view.AddNewFolder += AddNewFolder;
            _view.AddNewBookmark += AddNewBookmark;
            _view.OpenBookmark += _view_OpenBookmark;
            _view.RemoveNode += _view_RemoveNode;

            _bookmarksTree = new BookmarksTree();
        }

        private void _view_RemoveNode(object sender, RemoveNodeEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.IdToRemove))
                return;

            IModelType nodeToRemove = GetModelTypeById(e.IdToRemove);
            bool isNodeToRemoveAFolder = nodeToRemove.GetType() == typeof(Folder);
            IModelType parentNode = GetModelTypeById(nodeToRemove.ParentId);
            bool isParentAFolder = parentNode == null ? true : parentNode.GetType() == typeof(Folder);
            
            if (!isParentAFolder) return;


            if(isNodeToRemoveAFolder)
            {
                bool isFolderEmpty = ((Folder)nodeToRemove).Children.Count() == 0;
                if (!isFolderEmpty)
                {

                    var diagResult = _dialogService.ShowMessageBox(
                        "Remove folder?", "Deleting this folder will delete all children.",
                        System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning
                    );

                    if (diagResult != System.Windows.Forms.DialogResult.OK) return;
                }
            }

            if (parentNode == null)
            {
                _bookmarksTree.Bookmarks.Remove(nodeToRemove);
            }
            else
            {
                Folder parentFolder = (Folder)GetModelTypeById(nodeToRemove.ParentId);
                parentFolder.Children.Remove(nodeToRemove);
            }

            _view.RemoveNodeFromTree(e.IdToRemove);
        }

        private void _view_OpenBookmark(object sender, OpenBookmarkEventArgs e)
        {
            IModelType typeToOpen = GetModelTypeById(e.IdToOpen);
            if (typeToOpen.GetType() != typeof(Bookmark)) return;

            URLOpener opener = new URLOpener(Browser.Chrome);
            opener.Open(((Bookmark)typeToOpen).Url);
        }

        private void AddNewBookmark(object sender, AddBookmarkEventArgs e)
        {
            IModelType parentNode = GetModelTypeById(e.ParentID);
            string parentId = parentNode == null ? null : parentNode.Id;

            string id = Guid.NewGuid().ToString();
            Bookmark bookmark = new Bookmark(e.URL, e.Label, id, parentId);
            if (string.IsNullOrEmpty(e.ParentID))
            {
                _bookmarksTree.Bookmarks.Add(bookmark);
            }
            else
            {
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
            IModelType parentNode = GetModelTypeById(e.ParentId);
            string parentId = parentNode == null ? null : parentNode.Id;

            string id = Guid.NewGuid().ToString();
            Folder folder = new Folder(e.Label, id, parentId);

            if(string.IsNullOrEmpty(e.ParentId))
            {
                _bookmarksTree.Bookmarks.Add(folder);
            } else
            {
                if(parentNode.GetType() == typeof(Folder))
                {
                    ((Folder)parentNode).Children.Add(folder);
                } else
                {
                    return;
                }
            }

            _view.AddFolderTreeNode(folder, e.ParentId);
        }
        
        private IModelType GetModelTypeById(string id)
        {
            if (id == null)
                return null;

            foreach(IModelType type in _bookmarksTree.Bookmarks)
            {
                IModelType t = GetModelTypeById(type, id);
                if (t != null) return t;
            }

            return null;
        }

        private IModelType GetModelTypeById(IModelType type, string id)
        {
            if (id == null)
                return null;

            if (type.Id == id) return type;

            if (type.GetType() == typeof(Bookmark))
                return null;

            foreach(IModelType t in ((Folder)type).Children)
            {
                IModelType t2 = GetModelTypeById(t, id);
                if (t2 != null) return t2;
            }

            return null;
        }
    }
}
