using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DesktopBookmarks.Presenter;
using DesktopBookmarks.Model;

namespace DesktopBookmarks.View
{
    public partial class Client : Form, IClientView
    {
        public string LabelText { get { return txtLabel.Text; } set { txtLabel.Text = value; } }
        public string UrlText { get { return txtURL.Text; } set { txtURL.Text = value; } }

        public event EventHandler<AddFolderEventArgs> AddNewFolder;
        public event EventHandler<AddBookmarkEventArgs> AddNewBookmark;
        public event EventHandler<OpenBookmarkEventArgs> OpenBookmark;

        public Client()
        {
            InitializeComponent();

            btnNewFolder.Click += BtnAddFolder;
            btnAdd.Click += BtnAdd_Click;

            ImageList images = new ImageList();
            images.Images.Add(Properties.Resources.folder);
            images.Images.Add(Properties.Resources.internet);

            treeBookmarks.ImageList = images;

            new ClientPresenter(this);
        }

        private void AddBookmark()
        {
            TreeNode parentNode = treeBookmarks.SelectedNode;
            string parentId = parentNode == null ? null : (string)parentNode.Tag;

            AddBookmarkEventArgs args = new AddBookmarkEventArgs(UrlText, LabelText, parentId);

            AddNewBookmark?.Invoke(this, args);
        }

        // Add a bookmark
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddBookmark();
        }

        private void BtnAddFolder(object sender, EventArgs e)
        {
            NewFolderDialog newFolderDialog = new NewFolderDialog();
            newFolderDialog.ShowDialog();

            if (newFolderDialog.DialogResult != DialogResult.OK)
                return;

            TreeNode parentNode = treeBookmarks.SelectedNode;
            string parentId = parentNode == null ? null : (string)parentNode.Tag;

            AddFolderEventArgs args = new AddFolderEventArgs(newFolderDialog.FolderName, parentId);

            AddNewFolder?.Invoke(this, args);
        }

        public void AddFolderTreeNode(Folder bookmark, string parentId)
        {
            TreeNode node = new TreeNode();
            node.Text = bookmark.Label;
            node.Tag = bookmark.Id;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
            if(string.IsNullOrEmpty(parentId))
            {
                treeBookmarks.Nodes.Add(node);
            } else
            {
                TreeNode parentNode = FindFromAll(parentId);
                parentNode.Nodes.Add(node);
                parentNode.Expand();
            }
        }

        private TreeNode FindFromAll(string id)
        {
            foreach(TreeNode node in treeBookmarks.Nodes)
            {
                TreeNode pot = FindNodeWithId(node, id);
                if (pot != null) return pot;
            }

            return null;
        }

        private TreeNode FindNodeWithId(TreeNode root, string id)
        {
            if((string)root.Tag == id)
            {
                return root;
            }

            foreach(TreeNode child in root.Nodes)
            {
                TreeNode pot = FindNodeWithId(child, id);
                if(pot != null)
                {
                    return pot;
                }
            }

            return null;
        }

        public void AddBookmarkTreeNode(Bookmark bookmark, string parentId)
        {
            TreeNode node = new TreeNode();
            node.Text = bookmark.Label;
            node.Tag = bookmark.Id;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            if (string.IsNullOrEmpty(parentId))
            {
                treeBookmarks.Nodes.Add(node);
            }
            else
            {
                TreeNode parentNode = FindFromAll(parentId);
                parentNode.Nodes.Add(node);
                parentNode.Expand();
            }
        }

        private void treeBookmarks_MouseDown(object sender, MouseEventArgs e)
        {
            var hit = treeBookmarks.HitTest(e.X, e.Y);

            if (hit.Node == null)
            {
                treeBookmarks.SelectedNode = null;
            }
        }

        private void treeBookmarks_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selected = treeBookmarks.SelectedNode;
            if (selected == null) return;
            string id = (string)selected.Tag;

            OpenBookmark?.Invoke(this, new OpenBookmarkEventArgs(id));
        }

        private void txtLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                AddBookmark();
                txtURL.Select();
            }
        }

        private void txtURL_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // Pasting!
            if(e.Control && e.KeyCode == Keys.V)
            {
                txtLabel.Focus();
            }
        }
    }
}
