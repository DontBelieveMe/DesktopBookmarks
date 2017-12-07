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
            _view.OpenBookmark += OpenBookmarkInBrowser;
            _view.RemoveNode += RemoveNode;
            _view.FilterTree += FilterTree;
            _view.SearchFocusLost += FocusTextBoxLost;

            _bookmarksTree = new BookmarksTree();

            string filename = "bookmarks.xml";
            _bookmarksTree.Read(filename);
            LoadTreeIntoView(_bookmarksTree);
        }

        private void FocusTextBoxLost(object sender, EventArgs e)
        {
            _view.ClearTree();
            LoadTreeIntoView(_bookmarksTree);
        }

        private void FilterTree(object sender, FilterTreeEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(e.SearchText))
            {
                _view.ClearTree();
                LoadTreeIntoView(_bookmarksTree);
                return;
            }

            BookmarksTree filterdTree = (BookmarksTree) _bookmarksTree.Clone();
            foreach(IModelType node in filterdTree.Bookmarks.ToList())
            {
                FilterChildren(e.SearchText, node, ref filterdTree);
            }
            _view.ClearTree();
            LoadTreeIntoView(filterdTree);
        }

        private void FilterChildren(string query, IModelType node, ref BookmarksTree tree)
        {
            bool isFolder = node.GetType() == typeof(Folder);
            if(isFolder)
            {
                if (node.Label.ToLower().Contains(query.ToLower()))
                    return;

                Folder folder = (Folder)node;
                foreach(IModelType child in folder.Children.ToList())
                {
                    FilterChildren(query, child, ref tree);
                }

                if (folder.Children.Count != 0) return;
            }

            if(!node.Label.ToLower().Contains(query.ToLower()))
            {
                if(string.IsNullOrEmpty(node.ParentId))
                {
                    tree.Bookmarks.Remove(node);
                } else
                {
                    Folder parentFolder = (Folder)GetModelTypeById(node.ParentId, tree);
                    parentFolder.Children.Remove(node);
                }
            }
        }

        private void LoadTreeIntoView(BookmarksTree tree)
        {
            foreach (IModelType item in tree.Bookmarks)
            {
                LoadNode(item);
            }
        }

        private void LoadNode(IModelType type)
        {
            if(type.GetType() == typeof(Folder))
            {
                _view.AddFolderTreeNode((Folder)type, type.ParentId);
                foreach(IModelType child in ((Folder)type).Children)
                {
                    LoadNode(child);
                }
            } else if(type.GetType() == typeof(Bookmark))
            {
                _view.AddBookmarkTreeNode((Bookmark)type, type.ParentId);
            }
        }
        
        private void RemoveNode(object sender, RemoveNodeEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.IdToRemove))
                return;

            IModelType nodeToRemove = GetModelTypeById(e.IdToRemove, _bookmarksTree);
            bool isNodeToRemoveAFolder = nodeToRemove.GetType() == typeof(Folder);
            IModelType parentNode = GetModelTypeById(nodeToRemove.ParentId, _bookmarksTree);
            bool isParentAFolder = parentNode == null ? true : parentNode.GetType() == typeof(Folder);
            
            if (!isParentAFolder) return;


            if(isNodeToRemoveAFolder)
            {
                bool isFolderEmpty = ((Folder)nodeToRemove).Children.Count() == 0;
                if (!isFolderEmpty)
                {

                    var diagResult = _dialogService.ShowMessageBox(
                        "Remove folder?", "Deleting this folder will delete all children. Continue?",
                        System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning
                    );

                    if (diagResult != System.Windows.Forms.DialogResult.OK) return;
                } 
            } else
            {
                var diagResult = _dialogService.ShowMessageBox(
                        "Delete node?", "There is no going back. Continue?",
                        System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning);
                if (diagResult != System.Windows.Forms.DialogResult.OK) return;
            }

            if (parentNode == null)
            {
                _bookmarksTree.Bookmarks.Remove(nodeToRemove);
            }
            else
            {
                Folder parentFolder = (Folder)GetModelTypeById(nodeToRemove.ParentId, _bookmarksTree);
                parentFolder.Children.Remove(nodeToRemove);
            }

            _view.RemoveNodeFromTree(e.IdToRemove);
            _bookmarksTree.WriteToFile("bookmarks.xml");
        }

        private void OpenBookmarkInBrowser(object sender, OpenBookmarkEventArgs e)
        {
            IModelType typeToOpen = GetModelTypeById(e.IdToOpen, _bookmarksTree);
            if (typeToOpen.GetType() != typeof(Bookmark)) return;

            URLOpener opener = new URLOpener(Browser.Chrome);
            opener.Open(((Bookmark)typeToOpen).Url);
        }

        private void AddNewBookmark(object sender, AddBookmarkEventArgs e)
        {
            IModelType parentNode = GetModelTypeById(e.ParentID, _bookmarksTree);
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

            _bookmarksTree.WriteToFile("bookmarks.xml");
        }

        private void AddNewFolder(object sender, AddFolderEventArgs e)
        {
            IModelType parentNode = GetModelTypeById(e.ParentId, _bookmarksTree);
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
            _bookmarksTree.WriteToFile("bookmarks.xml");
        }
        
        private IModelType GetModelTypeById(string id, BookmarksTree tree)
        {
            if (id == null)
                return null;

            foreach(IModelType type in tree.Bookmarks)
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
