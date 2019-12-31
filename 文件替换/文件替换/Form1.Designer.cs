namespace 文件替换
{
    partial class Form1
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
            this.listBoxFrom = new System.Windows.Forms.ListBox();
            this.listBoxTo = new System.Windows.Forms.ListBox();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxFrom
            // 
            this.listBoxFrom.AllowDrop = true;
            this.listBoxFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFrom.FormattingEnabled = true;
            this.listBoxFrom.HorizontalScrollbar = true;
            this.listBoxFrom.Location = new System.Drawing.Point(12, 12);
            this.listBoxFrom.Name = "listBoxFrom";
            this.listBoxFrom.Size = new System.Drawing.Size(304, 368);
            this.listBoxFrom.TabIndex = 0;
            this.listBoxFrom.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxFrom_DragDrop);
            this.listBoxFrom.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxFrom_DragEnter);
            // 
            // listBoxTo
            // 
            this.listBoxTo.AllowDrop = true;
            this.listBoxTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTo.FormattingEnabled = true;
            this.listBoxTo.Location = new System.Drawing.Point(390, 12);
            this.listBoxTo.Name = "listBoxTo";
            this.listBoxTo.Size = new System.Drawing.Size(486, 368);
            this.listBoxTo.TabIndex = 1;
            this.listBoxTo.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxTo_DragDrop);
            this.listBoxTo.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxFrom_DragEnter);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReplace.Location = new System.Drawing.Point(322, 12);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(62, 51);
            this.buttonReplace.TabIndex = 2;
            this.buttonReplace.Text = "Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(322, 69);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(62, 51);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 397);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.listBoxFrom);
            this.Controls.Add(this.listBoxTo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFrom;
        private System.Windows.Forms.ListBox listBoxTo;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonClear;
    }
}

