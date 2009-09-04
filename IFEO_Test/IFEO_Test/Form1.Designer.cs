namespace IFEO_Test
{
    partial class IFEO
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Image File Execution Options");
            this.IFEOTree = new System.Windows.Forms.TreeView();
            this.IFEONameLable = new System.Windows.Forms.Label();
            this.IFEOImageLable = new System.Windows.Forms.Label();
            this.IFEONameTextBox = new System.Windows.Forms.TextBox();
            this.IFEOImageTextBox = new System.Windows.Forms.TextBox();
            this.IFEOExit = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // IFEOTree
            // 
            this.IFEOTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.IFEOTree.Location = new System.Drawing.Point(12, 12);
            this.IFEOTree.Name = "IFEOTree";
            treeNode2.Name = "IFEORoot";
            treeNode2.Text = "Image File Execution Options";
            this.IFEOTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.IFEOTree.Size = new System.Drawing.Size(234, 301);
            this.IFEOTree.TabIndex = 0;
            // 
            // IFEONameLable
            // 
            this.IFEONameLable.AutoSize = true;
            this.IFEONameLable.Location = new System.Drawing.Point(252, 12);
            this.IFEONameLable.Name = "IFEONameLable";
            this.IFEONameLable.Size = new System.Drawing.Size(90, 13);
            this.IFEONameLable.TabIndex = 1;
            this.IFEONameLable.Text = "Original File name";
            // 
            // IFEOImageLable
            // 
            this.IFEOImageLable.AutoSize = true;
            this.IFEOImageLable.Location = new System.Drawing.Point(249, 51);
            this.IFEOImageLable.Name = "IFEOImageLable";
            this.IFEOImageLable.Size = new System.Drawing.Size(109, 13);
            this.IFEOImageLable.TabIndex = 1;
            this.IFEOImageLable.Text = "Image File destination";
            // 
            // IFEONameTextBox
            // 
            this.IFEONameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.IFEONameTextBox.Location = new System.Drawing.Point(252, 28);
            this.IFEONameTextBox.Name = "IFEONameTextBox";
            this.IFEONameTextBox.Size = new System.Drawing.Size(186, 20);
            this.IFEONameTextBox.TabIndex = 2;
            // 
            // IFEOImageTextBox
            // 
            this.IFEOImageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.IFEOImageTextBox.Location = new System.Drawing.Point(252, 67);
            this.IFEOImageTextBox.Name = "IFEOImageTextBox";
            this.IFEOImageTextBox.Size = new System.Drawing.Size(186, 20);
            this.IFEOImageTextBox.TabIndex = 2;
            // 
            // IFEOExit
            // 
            this.IFEOExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.IFEOExit.Location = new System.Drawing.Point(363, 290);
            this.IFEOExit.Name = "IFEOExit";
            this.IFEOExit.Size = new System.Drawing.Size(75, 23);
            this.IFEOExit.TabIndex = 3;
            this.IFEOExit.Text = "Exit";
            this.IFEOExit.UseVisualStyleBackColor = true;
            this.IFEOExit.Click += new System.EventHandler(this.IFEOExit_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(252, 93);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(186, 95);
            this.listBox1.TabIndex = 4;
            // 
            // IFEO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 325);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.IFEOExit);
            this.Controls.Add(this.IFEOImageTextBox);
            this.Controls.Add(this.IFEONameTextBox);
            this.Controls.Add(this.IFEOImageLable);
            this.Controls.Add(this.IFEONameLable);
            this.Controls.Add(this.IFEOTree);
            this.Name = "IFEO";
            this.Text = "IFEO V1.0.0.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView IFEOTree;
        private System.Windows.Forms.Label IFEONameLable;
        private System.Windows.Forms.Label IFEOImageLable;
        private System.Windows.Forms.TextBox IFEONameTextBox;
        private System.Windows.Forms.TextBox IFEOImageTextBox;
        private System.Windows.Forms.Button IFEOExit;
        private System.Windows.Forms.ListBox listBox1;
    }
}

