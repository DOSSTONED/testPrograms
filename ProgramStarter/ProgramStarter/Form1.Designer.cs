namespace ProgramStarter
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
            this.EditMode = new System.Windows.Forms.CheckBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSample = new System.Windows.Forms.Button();
            this.TxtBxName = new System.Windows.Forms.TextBox();
            this.LbName = new System.Windows.Forms.Label();
            this.TxtBxPath = new System.Windows.Forms.TextBox();
            this.LbPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EditMode
            // 
            this.EditMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EditMode.AutoSize = true;
            this.EditMode.Checked = true;
            this.EditMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EditMode.Location = new System.Drawing.Point(12, 225);
            this.EditMode.Name = "EditMode";
            this.EditMode.Size = new System.Drawing.Size(74, 17);
            this.EditMode.TabIndex = 0;
            this.EditMode.Text = "Edit Mode";
            this.EditMode.UseVisualStyleBackColor = true;
            this.EditMode.CheckedChanged += new System.EventHandler(this.EditMode_CheckedChanged);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAdd.Location = new System.Drawing.Point(92, 221);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "Add New";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExit.Location = new System.Drawing.Point(284, 221);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 23);
            this.BtnExit.TabIndex = 2;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSample
            // 
            this.BtnSample.Location = new System.Drawing.Point(12, 12);
            this.BtnSample.Name = "BtnSample";
            this.BtnSample.Size = new System.Drawing.Size(155, 23);
            this.BtnSample.TabIndex = 3;
            this.BtnSample.Text = "This is a Sample";
            this.BtnSample.UseVisualStyleBackColor = true;
            // 
            // TxtBxName
            // 
            this.TxtBxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBxName.Location = new System.Drawing.Point(252, 17);
            this.TxtBxName.Name = "TxtBxName";
            this.TxtBxName.Size = new System.Drawing.Size(107, 20);
            this.TxtBxName.TabIndex = 4;
            // 
            // LbName
            // 
            this.LbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LbName.AutoSize = true;
            this.LbName.BackColor = System.Drawing.Color.Transparent;
            this.LbName.Location = new System.Drawing.Point(173, 17);
            this.LbName.Name = "LbName";
            this.LbName.Size = new System.Drawing.Size(73, 13);
            this.LbName.TabIndex = 5;
            this.LbName.Text = "Display name:";
            // 
            // TxtBxPath
            // 
            this.TxtBxPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBxPath.Location = new System.Drawing.Point(252, 43);
            this.TxtBxPath.Name = "TxtBxPath";
            this.TxtBxPath.Size = new System.Drawing.Size(107, 20);
            this.TxtBxPath.TabIndex = 4;
            // 
            // LbPath
            // 
            this.LbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LbPath.AutoSize = true;
            this.LbPath.BackColor = System.Drawing.Color.Transparent;
            this.LbPath.Location = new System.Drawing.Point(173, 43);
            this.LbPath.Name = "LbPath";
            this.LbPath.Size = new System.Drawing.Size(69, 13);
            this.LbPath.TabIndex = 5;
            this.LbPath.Text = "Source Path:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 254);
            this.Controls.Add(this.LbPath);
            this.Controls.Add(this.LbName);
            this.Controls.Add(this.TxtBxPath);
            this.Controls.Add(this.TxtBxName);
            this.Controls.Add(this.BtnSample);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.EditMode);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox EditMode;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnSample;
        private System.Windows.Forms.TextBox TxtBxName;
        private System.Windows.Forms.Label LbName;
        private System.Windows.Forms.TextBox TxtBxPath;
        private System.Windows.Forms.Label LbPath;
    }
}

