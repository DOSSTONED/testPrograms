namespace DOSSTONED_FrontEnd_V0._1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBoxLoadedPlugins = new System.Windows.Forms.ListBox();
            this.fileSystemWatcherDLL = new System.IO.FileSystemWatcher();
            this.notifyIconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMainFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDLL)).BeginInit();
            this.contextMenuStripIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxLoadedPlugins
            // 
            this.listBoxLoadedPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLoadedPlugins.FormattingEnabled = true;
            this.listBoxLoadedPlugins.Location = new System.Drawing.Point(643, 12);
            this.listBoxLoadedPlugins.Name = "listBoxLoadedPlugins";
            this.listBoxLoadedPlugins.Size = new System.Drawing.Size(234, 160);
            this.listBoxLoadedPlugins.TabIndex = 0;
            // 
            // fileSystemWatcherDLL
            // 
            this.fileSystemWatcherDLL.EnableRaisingEvents = true;
            this.fileSystemWatcherDLL.SynchronizingObject = this;
            this.fileSystemWatcherDLL.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcherDLL_Renamed);
            // 
            // notifyIconTray
            // 
            this.notifyIconTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIconTray.BalloonTipText = "This is DOSSTONED program.";
            this.notifyIconTray.BalloonTipTitle = "Hi";
            this.notifyIconTray.ContextMenuStrip = this.contextMenuStripIcon;
            this.notifyIconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconTray.Icon")));
            this.notifyIconTray.Text = "DOSSTONED FrontEnd V0.1";
            this.notifyIconTray.Visible = true;
            this.notifyIconTray.DoubleClick += new System.EventHandler(this.showMainFormToolStripMenuItem_Click);
            // 
            // contextMenuStripIcon
            // 
            this.contextMenuStripIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMainFormToolStripMenuItem,
            this.exitxToolStripMenuItem});
            this.contextMenuStripIcon.Name = "contextMenuStripIcon";
            this.contextMenuStripIcon.Size = new System.Drawing.Size(163, 48);
            // 
            // showMainFormToolStripMenuItem
            // 
            this.showMainFormToolStripMenuItem.Name = "showMainFormToolStripMenuItem";
            this.showMainFormToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.showMainFormToolStripMenuItem.Text = "Show main form";
            this.showMainFormToolStripMenuItem.Click += new System.EventHandler(this.showMainFormToolStripMenuItem_Click);
            // 
            // exitxToolStripMenuItem
            // 
            this.exitxToolStripMenuItem.Name = "exitxToolStripMenuItem";
            this.exitxToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitxToolStripMenuItem.Text = "Exit(&x)";
            this.exitxToolStripMenuItem.Click += new System.EventHandler(this.exitxToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(625, 431);
            this.tabControl1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 455);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listBoxLoadedPlugins);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherDLL)).EndInit();
            this.contextMenuStripIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLoadedPlugins;
        private System.IO.FileSystemWatcher fileSystemWatcherDLL;
        private System.Windows.Forms.NotifyIcon notifyIconTray;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripIcon;
        private System.Windows.Forms.ToolStripMenuItem exitxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMainFormToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
    }
}

