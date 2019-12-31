namespace NBTSTAT_CS
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
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelHost = new System.Windows.Forms.Label();
            this.labelGroup = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelMAC = new System.Windows.Forms.Label();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.textBoxGroup = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxMAC = new System.Windows.Forms.TextBox();
            this.listBoxError = new System.Windows.Forms.ListBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(9, 12);
            this.labelIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(20, 13);
            this.labelIP.TabIndex = 0;
            this.labelIP.Text = "IP:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIP.Location = new System.Drawing.Point(52, 10);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(567, 20);
            this.textBoxIP.TabIndex = 0;
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(9, 35);
            this.labelHost.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(32, 13);
            this.labelHost.TabIndex = 2;
            this.labelHost.Text = "Host:";
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(9, 58);
            this.labelGroup.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(39, 13);
            this.labelGroup.TabIndex = 2;
            this.labelGroup.Text = "Group:";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(9, 80);
            this.labelUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(32, 13);
            this.labelUser.TabIndex = 2;
            this.labelUser.Text = "User:";
            // 
            // labelMAC
            // 
            this.labelMAC.AutoSize = true;
            this.labelMAC.Location = new System.Drawing.Point(9, 103);
            this.labelMAC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMAC.Name = "labelMAC";
            this.labelMAC.Size = new System.Drawing.Size(33, 13);
            this.labelMAC.TabIndex = 2;
            this.labelMAC.Text = "MAC:";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHost.Location = new System.Drawing.Point(52, 32);
            this.textBoxHost.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.ReadOnly = true;
            this.textBoxHost.Size = new System.Drawing.Size(628, 20);
            this.textBoxHost.TabIndex = 2;
            // 
            // textBoxGroup
            // 
            this.textBoxGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGroup.Location = new System.Drawing.Point(52, 55);
            this.textBoxGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxGroup.Name = "textBoxGroup";
            this.textBoxGroup.ReadOnly = true;
            this.textBoxGroup.Size = new System.Drawing.Size(628, 20);
            this.textBoxGroup.TabIndex = 3;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUser.Location = new System.Drawing.Point(52, 78);
            this.textBoxUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.ReadOnly = true;
            this.textBoxUser.Size = new System.Drawing.Size(628, 20);
            this.textBoxUser.TabIndex = 4;
            // 
            // textBoxMAC
            // 
            this.textBoxMAC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMAC.Location = new System.Drawing.Point(52, 101);
            this.textBoxMAC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxMAC.Name = "textBoxMAC";
            this.textBoxMAC.ReadOnly = true;
            this.textBoxMAC.Size = new System.Drawing.Size(628, 20);
            this.textBoxMAC.TabIndex = 5;
            // 
            // listBoxError
            // 
            this.listBoxError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxError.FormattingEnabled = true;
            this.listBoxError.Items.AddRange(new object[] {
            "Errors:"});
            this.listBoxError.Location = new System.Drawing.Point(11, 124);
            this.listBoxError.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxError.Name = "listBoxError";
            this.listBoxError.Size = new System.Drawing.Size(346, 407);
            this.listBoxError.TabIndex = 3;
            // 
            // buttonGet
            // 
            this.buttonGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGet.Location = new System.Drawing.Point(623, 10);
            this.buttonGet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(56, 19);
            this.buttonGet.TabIndex = 1;
            this.buttonGet.Text = "Get!";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // listBoxResult
            // 
            this.listBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.Items.AddRange(new object[] {
            "Result:"});
            this.listBoxResult.Location = new System.Drawing.Point(361, 124);
            this.listBoxResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(319, 407);
            this.listBoxResult.TabIndex = 3;
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonGet;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 557);
            this.Controls.Add(this.buttonGet);
            this.Controls.Add(this.listBoxResult);
            this.Controls.Add(this.listBoxError);
            this.Controls.Add(this.labelMAC);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.labelHost);
            this.Controls.Add(this.textBoxMAC);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.textBoxGroup);
            this.Controls.Add(this.textBoxHost);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labelIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelMAC;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.TextBox textBoxGroup;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxMAC;
        private System.Windows.Forms.ListBox listBoxError;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.ListBox listBoxResult;
    }
}

