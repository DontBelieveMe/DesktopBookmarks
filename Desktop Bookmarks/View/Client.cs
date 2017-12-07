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
using DesktopBookmarks.Classes;

namespace DesktopBookmarks.View
{
    public partial class Client : Form, IClientView
    {
        public string LabelText { get { return txtLabel.Text; } set { txtLabel.Text = value; } }
        public string UrlText { get { return txtURL.Text; } set { txtURL.Text = value; } }

        public event EventHandler<AddFolderEventArgs> AddNewFolder;
        public event EventHandler<AddBookmarkEventArgs> AddNewBookmark;
        public event EventHandler<OpenBookmarkEventArgs> OpenBookmark;
        public event EventHandler<RemoveNodeEventArgs> RemoveNode;
        public event EventHandler Save;
        public event EventHandler<FilterTreeEventArgs> FilterTree;
        public event EventHandler SearchFocusLost;

        public Client()
        {
            InitializeComponent();
            treeContextMenu.VisibleChanged += TreeContextMenu_VisibleChanged;
            btnNewFolder.Click += BtnAddFolder;
            btnAdd.Click += BtnAdd_Click;
            btnRemove.Click += BtnRemove_Click;
            btnCtxtRemoveNode.Click += BtnRemove_Click;

            txtURL.PreviewKeyDown += TxtURL_PreviewKeyDown;
            
            ImageList images = new ImageList();
            images.Images.Add(Properties.Resources.folder);
            images.Images.Add(Properties.Resources.internet);

            treeBookmarks.ImageList = images;

            treeBookmarks.PreviewKeyDown += TreeBookmarks_PreviewKeyDown;
            new ClientPresenter(this, new UIDialogService());

            txtSearchQuery.Text = SharedConstants.SearchText;
            txtSearchQuery.Click += TxtSearchQuery_Click;
            txtSearchQuery.LostFocus += TxtSearchQuery_LostFocus;
            txtSearchQuery.TextChanged += TxtSearchQuery_TextChanged;
            txtSearchQuery.PreviewKeyDown += TxtSearchQuery_PreviewKeyDown;
            treeBookmarks.ShowNodeToolTips = true;
        }

        private void TreeBookmarks_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                RemoveCurrentNode();
            }
        }

        private void TxtSearchQuery_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                txtSearchQuery.Text = SharedConstants.SearchText;
                SearchFocusLost?.Invoke(this, new EventArgs());
                treeBookmarks.Invalidate();
                treeBookmarks.Focus();
            }
        }

        private void TxtSearchQuery_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchQuery.Text == SharedConstants.SearchText)
                return;
            
            FilterTree?.Invoke(this, new FilterTreeEventArgs(txtSearchQuery.Text));
            treeBookmarks.Invalidate();
        }

        private void TxtSearchQuery_LostFocus(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtSearchQuery.Text))
            {
                txtSearchQuery.Text = SharedConstants.SearchText;
                SearchFocusLost?.Invoke(this, new EventArgs());
                treeBookmarks.Invalidate();
            }
        }

        private void OnSearchClick()
        {
            if (txtSearchQuery.Text == SharedConstants.SearchText)
            {
                txtSearchQuery.TextChanged -= TxtSearchQuery_TextChanged;
                txtSearchQuery.Text = "";
                txtSearchQuery.TextChanged += TxtSearchQuery_TextChanged;
            }
        }

        private void TxtSearchQuery_Click(object sender, EventArgs e)
        {
            OnSearchClick();
        }

        private void TreeContextMenu_VisibleChanged(object sender, EventArgs e)
        {
            btnCtxtRemoveNode.Enabled = treeBookmarks.SelectedNode != null;
        }
        
        private void TxtURL_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtLabel.Focus();
            }
        }
        
        private void RemoveCurrentNode()
        {
            TreeNode selected = treeBookmarks.SelectedNode;
            string id;
            if (selected == null) id = null;
            else id = (string)selected.Tag;

            RemoveNode?.Invoke(this, new RemoveNodeEventArgs(id));
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            RemoveCurrentNode();
        }

        private void AddBookmark()
        {
            TreeNode parentNode = treeBookmarks.SelectedNode;
            string parentId = parentNode == null ? null : (string)parentNode.Tag;

            AddBookmarkEventArgs args = new AddBookmarkEventArgs(UrlText, LabelText, parentId);

            AddNewBookmark?.Invoke(this, args);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddBookmark();
        }

        private void AddFolder()
        {
            NewFolderDialog newFolderDialog = new NewFolderDialog();
            TreeNode selectedNode = treeBookmarks.SelectedNode;
            newFolderDialog.ShowDialog();

            if (newFolderDialog.DialogResult != DialogResult.OK)
                return;

            TreeNode parentNode = selectedNode;
            string parentId = parentNode == null ? null : (string)parentNode.Tag;

            AddFolderEventArgs args = new AddFolderEventArgs(newFolderDialog.FolderName, parentId);

            AddNewFolder?.Invoke(this, args);
        }

        private void BtnAddFolder(object sender, EventArgs e)
        {
            AddFolder();
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
            treeBookmarks.SelectedNode = node;
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
            node.ToolTipText = bookmark.Url;
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

            treeBookmarks.SelectedNode = hit.Node;

            if(e.Button == MouseButtons.Right)
            {
                
                treeContextMenu.Show(MousePosition);
            }
        }

        private void FollowLink()
        {
            TreeNode selected = treeBookmarks.SelectedNode;
            if (selected == null) return;
            string id = (string)selected.Tag;

            OpenBookmark?.Invoke(this, new OpenBookmarkEventArgs(id));
        }

        private void treeBookmarks_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FollowLink();
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
            if(e.Control && e.KeyCode == Keys.V)
            {
                txtLabel.Focus();
            }
        }

        public void RemoveNodeFromTree(string id)
        {
            TreeNode node = FindFromAll(id);
            if (node.Parent == null)
            {
                treeBookmarks.Nodes.Remove(node);
            }
            else
            {
                node.Parent.Nodes.Remove(node);
            }
            treeBookmarks.Select();
        }

        private void btnCtxAddBookmark_Click(object sender, EventArgs e)
        {
            txtURL.Focus();
        }

        private void btnCtxAddFolder_Click(object sender, EventArgs e)
        {
            AddFolder();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save?.Invoke(this, new EventArgs());
        }

        public void ClearTree()
        {
            treeBookmarks.Nodes.Clear();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                txtSearchQuery.Select();
                OnSearchClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOpenLink_Click(object sender, EventArgs e)
        {
            FollowLink();
        }
    }
}
