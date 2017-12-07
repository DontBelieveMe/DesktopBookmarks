namespace DesktopBookmarks.View
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.treeBookmarks = new System.Windows.Forms.TreeView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnNewFolder = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.treeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCtxtRemoveNode = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCtxAddBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCtxAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.treeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Label";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(72, 8);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(314, 20);
            this.txtURL.TabIndex = 2;
            this.txtURL.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtURL_PreviewKeyDown);
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(72, 33);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(314, 20);
            this.txtLabel.TabIndex = 3;
            this.txtLabel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtLabel_PreviewKeyDown);
            // 
            // treeBookmarks
            // 
            this.treeBookmarks.CausesValidation = false;
            this.treeBookmarks.HideSelection = false;
            this.treeBookmarks.Location = new System.Drawing.Point(12, 88);
            this.treeBookmarks.Name = "treeBookmarks";
            this.treeBookmarks.Size = new System.Drawing.Size(374, 343);
            this.treeBookmarks.TabIndex = 4;
            this.treeBookmarks.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeBookmarks_NodeMouseDoubleClick);
            this.treeBookmarks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeBookmarks_MouseDown);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(311, 59);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnNewFolder
            // 
            this.btnNewFolder.Location = new System.Drawing.Point(230, 59);
            this.btnNewFolder.Name = "btnNewFolder";
            this.btnNewFolder.Size = new System.Drawing.Size(75, 23);
            this.btnNewFolder.TabIndex = 6;
            this.btnNewFolder.Text = "New Folder";
            this.btnNewFolder.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(12, 59);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // treeContextMenu
            // 
            this.treeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCtxtRemoveNode,
            this.addToolStripMenuItem});
            this.treeContextMenu.Name = "treeContextMenu";
            this.treeContextMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // btnCtxtRemoveNode
            // 
            this.btnCtxtRemoveNode.Name = "btnCtxtRemoveNode";
            this.btnCtxtRemoveNode.Size = new System.Drawing.Size(117, 22);
            this.btnCtxtRemoveNode.Text = "Remove";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCtxAddBookmark,
            this.btnCtxAddFolder});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // btnCtxAddBookmark
            // 
            this.btnCtxAddBookmark.Name = "btnCtxAddBookmark";
            this.btnCtxAddBookmark.Size = new System.Drawing.Size(128, 22);
            this.btnCtxAddBookmark.Text = "Bookmark";
            this.btnCtxAddBookmark.Click += new System.EventHandler(this.btnCtxAddBookmark_Click);
            // 
            // btnCtxAddFolder
            // 
            this.btnCtxAddFolder.Name = "btnCtxAddFolder";
            this.btnCtxAddFolder.Size = new System.Drawing.Size(128, 22);
            this.btnCtxAddFolder.Text = "Folder";
            this.btnCtxAddFolder.Click += new System.EventHandler(this.btnCtxAddFolder_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(398, 443);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnNewFolder);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.treeBookmarks);
            this.Controls.Add(this.txtLabel);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Client";
            this.Text = "Bookmarks";
            this.treeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.TreeView treeBookmarks;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnNewFolder;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ContextMenuStrip treeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnCtxtRemoveNode;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnCtxAddBookmark;
        private System.Windows.Forms.ToolStripMenuItem btnCtxAddFolder;
    }
}

