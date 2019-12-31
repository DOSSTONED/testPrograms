namespace 电影字幕文件重命名
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
            this.listBoxMovie = new System.Windows.Forms.ListBox();
            this.listBoxTitle = new System.Windows.Forms.ListBox();
            this.buttonRename = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxMovie
            // 
            this.listBoxMovie.FormattingEnabled = true;
            this.listBoxMovie.HorizontalScrollbar = true;
            this.listBoxMovie.Location = new System.Drawing.Point(12, 12);
            this.listBoxMovie.Name = "listBoxMovie";
            this.listBoxMovie.Size = new System.Drawing.Size(316, 407);
            this.listBoxMovie.TabIndex = 0;
            // 
            // listBoxTitle
            // 
            this.listBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxTitle.FormattingEnabled = true;
            this.listBoxTitle.HorizontalScrollbar = true;
            this.listBoxTitle.Location = new System.Drawing.Point(384, 12);
            this.listBoxTitle.Name = "listBoxTitle";
            this.listBoxTitle.Size = new System.Drawing.Size(316, 407);
            this.listBoxTitle.TabIndex = 1;
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(314, 438);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(91, 34);
            this.buttonRename.TabIndex = 2;
            this.buttonRename.Text = "开始重命名";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 484);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.listBoxTitle);
            this.Controls.Add(this.listBoxMovie);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMovie;
        private System.Windows.Forms.ListBox listBoxTitle;
        private System.Windows.Forms.Button buttonRename;
    }
}

