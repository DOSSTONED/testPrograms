namespace CS_ChangeIP_Route
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
            this.comboBoxAdapter1 = new System.Windows.Forms.ComboBox();
            this.comboBoxAdapter2 = new System.Windows.Forms.ComboBox();
            this.groupBoxAdapter1 = new System.Windows.Forms.GroupBox();
            this.checkBoxAdapter1_IPAuto = new System.Windows.Forms.CheckBox();
            this.checkBoxAdapter1_DNSAuto = new System.Windows.Forms.CheckBox();
            this.textBoxAdapter1_DNS2 = new System.Windows.Forms.TextBox();
            this.textBoxAdapter1_DNS1 = new System.Windows.Forms.TextBox();
            this.textBoxAdapter1_Gateway = new System.Windows.Forms.TextBox();
            this.textBoxAdapter1_Mask = new System.Windows.Forms.TextBox();
            this.labelAdapter1_DNS2 = new System.Windows.Forms.Label();
            this.labelAdapter1_DNS1 = new System.Windows.Forms.Label();
            this.labelAdapter1_Gateway = new System.Windows.Forms.Label();
            this.labelAdapter1_Mask = new System.Windows.Forms.Label();
            this.textBoxAdapter1_IPAddress = new System.Windows.Forms.TextBox();
            this.labelAdapter1_IPAddress = new System.Windows.Forms.Label();
            this.buttonAdapter1_Apply = new System.Windows.Forms.Button();
            this.groupBoxAdapter2 = new System.Windows.Forms.GroupBox();
            this.checkBoxAdapter2_IPAuto = new System.Windows.Forms.CheckBox();
            this.labelAdapter2_IPAddress = new System.Windows.Forms.Label();
            this.checkBoxAdapter2_DNSAuto = new System.Windows.Forms.CheckBox();
            this.textBoxAdapter2_IPAddress = new System.Windows.Forms.TextBox();
            this.textBoxAdapter2_DNS2 = new System.Windows.Forms.TextBox();
            this.labelAdapter2_Mask = new System.Windows.Forms.Label();
            this.textBoxAdapter2_DNS1 = new System.Windows.Forms.TextBox();
            this.labelAdapter2_Gateway = new System.Windows.Forms.Label();
            this.textBoxAdapter2_Gateway = new System.Windows.Forms.TextBox();
            this.labelAdapter2_DNS1 = new System.Windows.Forms.Label();
            this.textBoxAdapter2_Mask = new System.Windows.Forms.TextBox();
            this.labelAdapter2_DNS2 = new System.Windows.Forms.Label();
            this.buttonConfig1 = new System.Windows.Forms.Button();
            this.buttonConfig2 = new System.Windows.Forms.Button();
            this.buttonConfig3 = new System.Windows.Forms.Button();
            this.buttonConfig4 = new System.Windows.Forms.Button();
            this.buttonDisplayRouteTable = new System.Windows.Forms.Button();
            this.comboBoxConfig = new System.Windows.Forms.ComboBox();
            this.groupBoxAdapter1.SuspendLayout();
            this.groupBoxAdapter2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxAdapter1
            // 
            this.comboBoxAdapter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAdapter1.FormattingEnabled = true;
            this.comboBoxAdapter1.Location = new System.Drawing.Point(6, 19);
            this.comboBoxAdapter1.Name = "comboBoxAdapter1";
            this.comboBoxAdapter1.Size = new System.Drawing.Size(188, 21);
            this.comboBoxAdapter1.TabIndex = 0;
            this.comboBoxAdapter1.SelectedIndexChanged += new System.EventHandler(this.comboBoxAdapter1_SelectedIndexChanged);
            // 
            // comboBoxAdapter2
            // 
            this.comboBoxAdapter2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAdapter2.FormattingEnabled = true;
            this.comboBoxAdapter2.Location = new System.Drawing.Point(6, 24);
            this.comboBoxAdapter2.Name = "comboBoxAdapter2";
            this.comboBoxAdapter2.Size = new System.Drawing.Size(197, 21);
            this.comboBoxAdapter2.TabIndex = 1;
            this.comboBoxAdapter2.SelectedIndexChanged += new System.EventHandler(this.comboBoxAdapter2_SelectedIndexChanged);
            // 
            // groupBoxAdapter1
            // 
            this.groupBoxAdapter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxAdapter1.Controls.Add(this.checkBoxAdapter1_IPAuto);
            this.groupBoxAdapter1.Controls.Add(this.checkBoxAdapter1_DNSAuto);
            this.groupBoxAdapter1.Controls.Add(this.textBoxAdapter1_DNS2);
            this.groupBoxAdapter1.Controls.Add(this.textBoxAdapter1_DNS1);
            this.groupBoxAdapter1.Controls.Add(this.textBoxAdapter1_Gateway);
            this.groupBoxAdapter1.Controls.Add(this.textBoxAdapter1_Mask);
            this.groupBoxAdapter1.Controls.Add(this.labelAdapter1_DNS2);
            this.groupBoxAdapter1.Controls.Add(this.labelAdapter1_DNS1);
            this.groupBoxAdapter1.Controls.Add(this.labelAdapter1_Gateway);
            this.groupBoxAdapter1.Controls.Add(this.labelAdapter1_Mask);
            this.groupBoxAdapter1.Controls.Add(this.textBoxAdapter1_IPAddress);
            this.groupBoxAdapter1.Controls.Add(this.labelAdapter1_IPAddress);
            this.groupBoxAdapter1.Controls.Add(this.comboBoxAdapter1);
            this.groupBoxAdapter1.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAdapter1.Name = "groupBoxAdapter1";
            this.groupBoxAdapter1.Size = new System.Drawing.Size(200, 233);
            this.groupBoxAdapter1.TabIndex = 2;
            this.groupBoxAdapter1.TabStop = false;
            this.groupBoxAdapter1.Text = "Internet";
            // 
            // checkBoxAdapter1_IPAuto
            // 
            this.checkBoxAdapter1_IPAuto.AutoSize = true;
            this.checkBoxAdapter1_IPAuto.Location = new System.Drawing.Point(7, 176);
            this.checkBoxAdapter1_IPAuto.Name = "checkBoxAdapter1_IPAuto";
            this.checkBoxAdapter1_IPAuto.Size = new System.Drawing.Size(61, 17);
            this.checkBoxAdapter1_IPAuto.TabIndex = 2;
            this.checkBoxAdapter1_IPAuto.Text = "Auto IP";
            this.checkBoxAdapter1_IPAuto.UseVisualStyleBackColor = true;
            this.checkBoxAdapter1_IPAuto.CheckedChanged += new System.EventHandler(this.checkBoxAdapter1_IPAuto_CheckedChanged);
            // 
            // checkBoxAdapter1_DNSAuto
            // 
            this.checkBoxAdapter1_DNSAuto.AutoSize = true;
            this.checkBoxAdapter1_DNSAuto.Location = new System.Drawing.Point(7, 199);
            this.checkBoxAdapter1_DNSAuto.Name = "checkBoxAdapter1_DNSAuto";
            this.checkBoxAdapter1_DNSAuto.Size = new System.Drawing.Size(74, 17);
            this.checkBoxAdapter1_DNSAuto.TabIndex = 2;
            this.checkBoxAdapter1_DNSAuto.Text = "Auto DNS";
            this.checkBoxAdapter1_DNSAuto.UseVisualStyleBackColor = true;
            this.checkBoxAdapter1_DNSAuto.CheckedChanged += new System.EventHandler(this.checkBoxAdapter1_DNSAuto_CheckedChanged);
            // 
            // textBoxAdapter1_DNS2
            // 
            this.textBoxAdapter1_DNS2.Location = new System.Drawing.Point(66, 150);
            this.textBoxAdapter1_DNS2.Name = "textBoxAdapter1_DNS2";
            this.textBoxAdapter1_DNS2.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter1_DNS2.TabIndex = 2;
            // 
            // textBoxAdapter1_DNS1
            // 
            this.textBoxAdapter1_DNS1.Location = new System.Drawing.Point(66, 124);
            this.textBoxAdapter1_DNS1.Name = "textBoxAdapter1_DNS1";
            this.textBoxAdapter1_DNS1.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter1_DNS1.TabIndex = 2;
            // 
            // textBoxAdapter1_Gateway
            // 
            this.textBoxAdapter1_Gateway.Location = new System.Drawing.Point(66, 98);
            this.textBoxAdapter1_Gateway.Name = "textBoxAdapter1_Gateway";
            this.textBoxAdapter1_Gateway.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter1_Gateway.TabIndex = 2;
            // 
            // textBoxAdapter1_Mask
            // 
            this.textBoxAdapter1_Mask.Location = new System.Drawing.Point(66, 72);
            this.textBoxAdapter1_Mask.Name = "textBoxAdapter1_Mask";
            this.textBoxAdapter1_Mask.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter1_Mask.TabIndex = 2;
            // 
            // labelAdapter1_DNS2
            // 
            this.labelAdapter1_DNS2.AutoSize = true;
            this.labelAdapter1_DNS2.Location = new System.Drawing.Point(4, 153);
            this.labelAdapter1_DNS2.Name = "labelAdapter1_DNS2";
            this.labelAdapter1_DNS2.Size = new System.Drawing.Size(36, 13);
            this.labelAdapter1_DNS2.TabIndex = 1;
            this.labelAdapter1_DNS2.Text = "DNS2";
            // 
            // labelAdapter1_DNS1
            // 
            this.labelAdapter1_DNS1.AutoSize = true;
            this.labelAdapter1_DNS1.Location = new System.Drawing.Point(4, 127);
            this.labelAdapter1_DNS1.Name = "labelAdapter1_DNS1";
            this.labelAdapter1_DNS1.Size = new System.Drawing.Size(36, 13);
            this.labelAdapter1_DNS1.TabIndex = 1;
            this.labelAdapter1_DNS1.Text = "DNS1";
            // 
            // labelAdapter1_Gateway
            // 
            this.labelAdapter1_Gateway.AutoSize = true;
            this.labelAdapter1_Gateway.Location = new System.Drawing.Point(4, 101);
            this.labelAdapter1_Gateway.Name = "labelAdapter1_Gateway";
            this.labelAdapter1_Gateway.Size = new System.Drawing.Size(49, 13);
            this.labelAdapter1_Gateway.TabIndex = 1;
            this.labelAdapter1_Gateway.Text = "Gateway";
            // 
            // labelAdapter1_Mask
            // 
            this.labelAdapter1_Mask.AutoSize = true;
            this.labelAdapter1_Mask.Location = new System.Drawing.Point(4, 75);
            this.labelAdapter1_Mask.Name = "labelAdapter1_Mask";
            this.labelAdapter1_Mask.Size = new System.Drawing.Size(33, 13);
            this.labelAdapter1_Mask.TabIndex = 1;
            this.labelAdapter1_Mask.Text = "Mask";
            // 
            // textBoxAdapter1_IPAddress
            // 
            this.textBoxAdapter1_IPAddress.Location = new System.Drawing.Point(66, 46);
            this.textBoxAdapter1_IPAddress.Name = "textBoxAdapter1_IPAddress";
            this.textBoxAdapter1_IPAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter1_IPAddress.TabIndex = 2;
            // 
            // labelAdapter1_IPAddress
            // 
            this.labelAdapter1_IPAddress.AutoSize = true;
            this.labelAdapter1_IPAddress.Location = new System.Drawing.Point(4, 49);
            this.labelAdapter1_IPAddress.Name = "labelAdapter1_IPAddress";
            this.labelAdapter1_IPAddress.Size = new System.Drawing.Size(17, 13);
            this.labelAdapter1_IPAddress.TabIndex = 1;
            this.labelAdapter1_IPAddress.Text = "IP";
            // 
            // buttonAdapter1_Apply
            // 
            this.buttonAdapter1_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdapter1_Apply.Location = new System.Drawing.Point(218, 222);
            this.buttonAdapter1_Apply.Name = "buttonAdapter1_Apply";
            this.buttonAdapter1_Apply.Size = new System.Drawing.Size(94, 23);
            this.buttonAdapter1_Apply.TabIndex = 3;
            this.buttonAdapter1_Apply.Text = "Apply";
            this.buttonAdapter1_Apply.UseVisualStyleBackColor = true;
            this.buttonAdapter1_Apply.Click += new System.EventHandler(this.buttonAdapter1_Apply_Click);
            // 
            // groupBoxAdapter2
            // 
            this.groupBoxAdapter2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAdapter2.Controls.Add(this.comboBoxAdapter2);
            this.groupBoxAdapter2.Controls.Add(this.checkBoxAdapter2_IPAuto);
            this.groupBoxAdapter2.Controls.Add(this.labelAdapter2_IPAddress);
            this.groupBoxAdapter2.Controls.Add(this.checkBoxAdapter2_DNSAuto);
            this.groupBoxAdapter2.Controls.Add(this.textBoxAdapter2_IPAddress);
            this.groupBoxAdapter2.Controls.Add(this.textBoxAdapter2_DNS2);
            this.groupBoxAdapter2.Controls.Add(this.labelAdapter2_Mask);
            this.groupBoxAdapter2.Controls.Add(this.textBoxAdapter2_DNS1);
            this.groupBoxAdapter2.Controls.Add(this.labelAdapter2_Gateway);
            this.groupBoxAdapter2.Controls.Add(this.textBoxAdapter2_Gateway);
            this.groupBoxAdapter2.Controls.Add(this.labelAdapter2_DNS1);
            this.groupBoxAdapter2.Controls.Add(this.textBoxAdapter2_Mask);
            this.groupBoxAdapter2.Controls.Add(this.labelAdapter2_DNS2);
            this.groupBoxAdapter2.Location = new System.Drawing.Point(318, 12);
            this.groupBoxAdapter2.Name = "groupBoxAdapter2";
            this.groupBoxAdapter2.Size = new System.Drawing.Size(209, 233);
            this.groupBoxAdapter2.TabIndex = 3;
            this.groupBoxAdapter2.TabStop = false;
            this.groupBoxAdapter2.Text = "School Connection";
            // 
            // checkBoxAdapter2_IPAuto
            // 
            this.checkBoxAdapter2_IPAuto.AutoSize = true;
            this.checkBoxAdapter2_IPAuto.Location = new System.Drawing.Point(9, 180);
            this.checkBoxAdapter2_IPAuto.Name = "checkBoxAdapter2_IPAuto";
            this.checkBoxAdapter2_IPAuto.Size = new System.Drawing.Size(61, 17);
            this.checkBoxAdapter2_IPAuto.TabIndex = 2;
            this.checkBoxAdapter2_IPAuto.Text = "Auto IP";
            this.checkBoxAdapter2_IPAuto.UseVisualStyleBackColor = true;
            this.checkBoxAdapter2_IPAuto.CheckedChanged += new System.EventHandler(this.checkBoxAdapter1_IPAuto_CheckedChanged);
            // 
            // labelAdapter2_IPAddress
            // 
            this.labelAdapter2_IPAddress.AutoSize = true;
            this.labelAdapter2_IPAddress.Location = new System.Drawing.Point(6, 53);
            this.labelAdapter2_IPAddress.Name = "labelAdapter2_IPAddress";
            this.labelAdapter2_IPAddress.Size = new System.Drawing.Size(17, 13);
            this.labelAdapter2_IPAddress.TabIndex = 1;
            this.labelAdapter2_IPAddress.Text = "IP";
            // 
            // checkBoxAdapter2_DNSAuto
            // 
            this.checkBoxAdapter2_DNSAuto.AutoSize = true;
            this.checkBoxAdapter2_DNSAuto.Location = new System.Drawing.Point(9, 203);
            this.checkBoxAdapter2_DNSAuto.Name = "checkBoxAdapter2_DNSAuto";
            this.checkBoxAdapter2_DNSAuto.Size = new System.Drawing.Size(74, 17);
            this.checkBoxAdapter2_DNSAuto.TabIndex = 2;
            this.checkBoxAdapter2_DNSAuto.Text = "Auto DNS";
            this.checkBoxAdapter2_DNSAuto.UseVisualStyleBackColor = true;
            this.checkBoxAdapter2_DNSAuto.CheckedChanged += new System.EventHandler(this.checkBoxAdapter1_DNSAuto_CheckedChanged);
            // 
            // textBoxAdapter2_IPAddress
            // 
            this.textBoxAdapter2_IPAddress.Location = new System.Drawing.Point(68, 50);
            this.textBoxAdapter2_IPAddress.Name = "textBoxAdapter2_IPAddress";
            this.textBoxAdapter2_IPAddress.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter2_IPAddress.TabIndex = 2;
            // 
            // textBoxAdapter2_DNS2
            // 
            this.textBoxAdapter2_DNS2.Location = new System.Drawing.Point(68, 154);
            this.textBoxAdapter2_DNS2.Name = "textBoxAdapter2_DNS2";
            this.textBoxAdapter2_DNS2.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter2_DNS2.TabIndex = 2;
            // 
            // labelAdapter2_Mask
            // 
            this.labelAdapter2_Mask.AutoSize = true;
            this.labelAdapter2_Mask.Location = new System.Drawing.Point(6, 79);
            this.labelAdapter2_Mask.Name = "labelAdapter2_Mask";
            this.labelAdapter2_Mask.Size = new System.Drawing.Size(33, 13);
            this.labelAdapter2_Mask.TabIndex = 1;
            this.labelAdapter2_Mask.Text = "Mask";
            // 
            // textBoxAdapter2_DNS1
            // 
            this.textBoxAdapter2_DNS1.Location = new System.Drawing.Point(68, 128);
            this.textBoxAdapter2_DNS1.Name = "textBoxAdapter2_DNS1";
            this.textBoxAdapter2_DNS1.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter2_DNS1.TabIndex = 2;
            // 
            // labelAdapter2_Gateway
            // 
            this.labelAdapter2_Gateway.AutoSize = true;
            this.labelAdapter2_Gateway.Location = new System.Drawing.Point(6, 105);
            this.labelAdapter2_Gateway.Name = "labelAdapter2_Gateway";
            this.labelAdapter2_Gateway.Size = new System.Drawing.Size(49, 13);
            this.labelAdapter2_Gateway.TabIndex = 1;
            this.labelAdapter2_Gateway.Text = "Gateway";
            // 
            // textBoxAdapter2_Gateway
            // 
            this.textBoxAdapter2_Gateway.Location = new System.Drawing.Point(68, 102);
            this.textBoxAdapter2_Gateway.Name = "textBoxAdapter2_Gateway";
            this.textBoxAdapter2_Gateway.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter2_Gateway.TabIndex = 2;
            // 
            // labelAdapter2_DNS1
            // 
            this.labelAdapter2_DNS1.AutoSize = true;
            this.labelAdapter2_DNS1.Location = new System.Drawing.Point(6, 131);
            this.labelAdapter2_DNS1.Name = "labelAdapter2_DNS1";
            this.labelAdapter2_DNS1.Size = new System.Drawing.Size(36, 13);
            this.labelAdapter2_DNS1.TabIndex = 1;
            this.labelAdapter2_DNS1.Text = "DNS1";
            // 
            // textBoxAdapter2_Mask
            // 
            this.textBoxAdapter2_Mask.Location = new System.Drawing.Point(68, 76);
            this.textBoxAdapter2_Mask.Name = "textBoxAdapter2_Mask";
            this.textBoxAdapter2_Mask.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdapter2_Mask.TabIndex = 2;
            // 
            // labelAdapter2_DNS2
            // 
            this.labelAdapter2_DNS2.AutoSize = true;
            this.labelAdapter2_DNS2.Location = new System.Drawing.Point(6, 157);
            this.labelAdapter2_DNS2.Name = "labelAdapter2_DNS2";
            this.labelAdapter2_DNS2.Size = new System.Drawing.Size(36, 13);
            this.labelAdapter2_DNS2.TabIndex = 1;
            this.labelAdapter2_DNS2.Text = "DNS2";
            // 
            // buttonConfig1
            // 
            this.buttonConfig1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfig1.Location = new System.Drawing.Point(218, 29);
            this.buttonConfig1.Name = "buttonConfig1";
            this.buttonConfig1.Size = new System.Drawing.Size(94, 23);
            this.buttonConfig1.TabIndex = 4;
            this.buttonConfig1.Text = "Auto";
            this.buttonConfig1.UseVisualStyleBackColor = true;
            this.buttonConfig1.Click += new System.EventHandler(this.buttonConfig1_Click);
            // 
            // buttonConfig2
            // 
            this.buttonConfig2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfig2.Location = new System.Drawing.Point(218, 58);
            this.buttonConfig2.Name = "buttonConfig2";
            this.buttonConfig2.Size = new System.Drawing.Size(94, 23);
            this.buttonConfig2.TabIndex = 4;
            this.buttonConfig2.Text = "192.168.1.1";
            this.buttonConfig2.UseVisualStyleBackColor = true;
            this.buttonConfig2.Click += new System.EventHandler(this.buttonConfig2_Click);
            // 
            // buttonConfig3
            // 
            this.buttonConfig3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfig3.Location = new System.Drawing.Point(218, 87);
            this.buttonConfig3.Name = "buttonConfig3";
            this.buttonConfig3.Size = new System.Drawing.Size(94, 23);
            this.buttonConfig3.TabIndex = 4;
            this.buttonConfig3.Text = "192.167.0.1";
            this.buttonConfig3.UseVisualStyleBackColor = true;
            this.buttonConfig3.Click += new System.EventHandler(this.buttonConfig3_Click);
            // 
            // buttonConfig4
            // 
            this.buttonConfig4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConfig4.Location = new System.Drawing.Point(218, 116);
            this.buttonConfig4.Name = "buttonConfig4";
            this.buttonConfig4.Size = new System.Drawing.Size(94, 23);
            this.buttonConfig4.TabIndex = 4;
            this.buttonConfig4.Text = "10.4.15.19";
            this.buttonConfig4.UseVisualStyleBackColor = true;
            this.buttonConfig4.Click += new System.EventHandler(this.buttonConfig4_Click);
            // 
            // buttonDisplayRouteTable
            // 
            this.buttonDisplayRouteTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisplayRouteTable.Location = new System.Drawing.Point(218, 164);
            this.buttonDisplayRouteTable.Name = "buttonDisplayRouteTable";
            this.buttonDisplayRouteTable.Size = new System.Drawing.Size(94, 23);
            this.buttonDisplayRouteTable.TabIndex = 5;
            this.buttonDisplayRouteTable.Text = "Route Table";
            this.buttonDisplayRouteTable.UseVisualStyleBackColor = true;
            this.buttonDisplayRouteTable.Click += new System.EventHandler(this.buttonDisplayRouteTable_Click);
            // 
            // comboBoxConfig
            // 
            this.comboBoxConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxConfig.FormattingEnabled = true;
            this.comboBoxConfig.Location = new System.Drawing.Point(12, 251);
            this.comboBoxConfig.Name = "comboBoxConfig";
            this.comboBoxConfig.Size = new System.Drawing.Size(515, 21);
            this.comboBoxConfig.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(539, 284);
            this.Controls.Add(this.comboBoxConfig);
            this.Controls.Add(this.buttonAdapter1_Apply);
            this.Controls.Add(this.buttonDisplayRouteTable);
            this.Controls.Add(this.buttonConfig4);
            this.Controls.Add(this.buttonConfig3);
            this.Controls.Add(this.buttonConfig2);
            this.Controls.Add(this.buttonConfig1);
            this.Controls.Add(this.groupBoxAdapter2);
            this.Controls.Add(this.groupBoxAdapter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IP Router";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxAdapter1.ResumeLayout(false);
            this.groupBoxAdapter1.PerformLayout();
            this.groupBoxAdapter2.ResumeLayout(false);
            this.groupBoxAdapter2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAdapter1;
        private System.Windows.Forms.ComboBox comboBoxAdapter2;
        private System.Windows.Forms.GroupBox groupBoxAdapter1;
        private System.Windows.Forms.GroupBox groupBoxAdapter2;
        private System.Windows.Forms.TextBox textBoxAdapter1_IPAddress;
        private System.Windows.Forms.Label labelAdapter1_IPAddress;
        private System.Windows.Forms.TextBox textBoxAdapter1_DNS2;
        private System.Windows.Forms.TextBox textBoxAdapter1_DNS1;
        private System.Windows.Forms.TextBox textBoxAdapter1_Gateway;
        private System.Windows.Forms.TextBox textBoxAdapter1_Mask;
        private System.Windows.Forms.Label labelAdapter1_DNS2;
        private System.Windows.Forms.Label labelAdapter1_DNS1;
        private System.Windows.Forms.Label labelAdapter1_Gateway;
        private System.Windows.Forms.Label labelAdapter1_Mask;
        private System.Windows.Forms.CheckBox checkBoxAdapter1_IPAuto;
        private System.Windows.Forms.CheckBox checkBoxAdapter1_DNSAuto;
        private System.Windows.Forms.Button buttonAdapter1_Apply;
        private System.Windows.Forms.Button buttonConfig1;
        private System.Windows.Forms.Button buttonConfig2;
        private System.Windows.Forms.Button buttonConfig3;
        private System.Windows.Forms.Button buttonConfig4;
        private System.Windows.Forms.Button buttonDisplayRouteTable;
        private System.Windows.Forms.CheckBox checkBoxAdapter2_IPAuto;
        private System.Windows.Forms.Label labelAdapter2_IPAddress;
        private System.Windows.Forms.CheckBox checkBoxAdapter2_DNSAuto;
        private System.Windows.Forms.TextBox textBoxAdapter2_IPAddress;
        private System.Windows.Forms.TextBox textBoxAdapter2_DNS2;
        private System.Windows.Forms.Label labelAdapter2_Mask;
        private System.Windows.Forms.TextBox textBoxAdapter2_DNS1;
        private System.Windows.Forms.Label labelAdapter2_Gateway;
        private System.Windows.Forms.TextBox textBoxAdapter2_Gateway;
        private System.Windows.Forms.Label labelAdapter2_DNS1;
        private System.Windows.Forms.TextBox textBoxAdapter2_Mask;
        private System.Windows.Forms.Label labelAdapter2_DNS2;
        private System.Windows.Forms.ComboBox comboBoxConfig;
    }
}

