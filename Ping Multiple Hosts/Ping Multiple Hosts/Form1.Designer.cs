namespace Ping_Multiple_Hosts
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
            this.tabControlHosts = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonPing = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.groupBoxOneHost = new System.Windows.Forms.GroupBox();
            this.textBoxOneHostDst = new System.Windows.Forms.TextBox();
            this.groupBoxOneHostRes = new System.Windows.Forms.GroupBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelTTL = new System.Windows.Forms.Label();
            this.labelBytes = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.textBoxTTL = new System.Windows.Forms.TextBox();
            this.textBoxBytes = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxHosts = new System.Windows.Forms.GroupBox();
            this.textBoxHosts = new System.Windows.Forms.TextBox();
            this.listBoxHostsReplies = new System.Windows.Forms.ListBox();
            this.toolTipHostsRange = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlHosts.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxControls.SuspendLayout();
            this.groupBoxOneHost.SuspendLayout();
            this.groupBoxOneHostRes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxHosts.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlHosts
            // 
            this.tabControlHosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlHosts.Controls.Add(this.tabPage1);
            this.tabControlHosts.Controls.Add(this.tabPage2);
            this.tabControlHosts.Location = new System.Drawing.Point(12, 12);
            this.tabControlHosts.Name = "tabControlHosts";
            this.tabControlHosts.SelectedIndex = 0;
            this.tabControlHosts.Size = new System.Drawing.Size(229, 250);
            this.tabControlHosts.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxOneHostRes);
            this.tabPage1.Controls.Add(this.groupBoxOneHost);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(221, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ping a host";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBoxHosts);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(221, 224);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ping hosts";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonPing
            // 
            this.buttonPing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPing.Location = new System.Drawing.Point(6, 20);
            this.buttonPing.Name = "buttonPing";
            this.buttonPing.Size = new System.Drawing.Size(75, 23);
            this.buttonPing.TabIndex = 1;
            this.buttonPing.Text = "Ping !";
            this.buttonPing.UseVisualStyleBackColor = true;
            this.buttonPing.Click += new System.EventHandler(this.buttonPing_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(148, 20);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxControls.Controls.Add(this.buttonPing);
            this.groupBoxControls.Controls.Add(this.buttonExit);
            this.groupBoxControls.Location = new System.Drawing.Point(12, 268);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(229, 49);
            this.groupBoxControls.TabIndex = 3;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Action Center";
            // 
            // groupBoxOneHost
            // 
            this.groupBoxOneHost.Controls.Add(this.textBoxOneHostDst);
            this.groupBoxOneHost.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOneHost.Location = new System.Drawing.Point(3, 3);
            this.groupBoxOneHost.Name = "groupBoxOneHost";
            this.groupBoxOneHost.Size = new System.Drawing.Size(215, 53);
            this.groupBoxOneHost.TabIndex = 0;
            this.groupBoxOneHost.TabStop = false;
            this.groupBoxOneHost.Text = "Host destination";
            // 
            // textBoxOneHostDst
            // 
            this.textBoxOneHostDst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOneHostDst.Location = new System.Drawing.Point(6, 19);
            this.textBoxOneHostDst.Name = "textBoxOneHostDst";
            this.textBoxOneHostDst.Size = new System.Drawing.Size(203, 20);
            this.textBoxOneHostDst.TabIndex = 0;
            // 
            // groupBoxOneHostRes
            // 
            this.groupBoxOneHostRes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBoxOneHostRes.Controls.Add(this.textBoxBytes);
            this.groupBoxOneHostRes.Controls.Add(this.textBoxTTL);
            this.groupBoxOneHostRes.Controls.Add(this.textBoxTime);
            this.groupBoxOneHostRes.Controls.Add(this.labelBytes);
            this.groupBoxOneHostRes.Controls.Add(this.labelTTL);
            this.groupBoxOneHostRes.Controls.Add(this.labelTime);
            this.groupBoxOneHostRes.Location = new System.Drawing.Point(6, 65);
            this.groupBoxOneHostRes.Name = "groupBoxOneHostRes";
            this.groupBoxOneHostRes.Size = new System.Drawing.Size(209, 153);
            this.groupBoxOneHostRes.TabIndex = 1;
            this.groupBoxOneHostRes.TabStop = false;
            this.groupBoxOneHostRes.Text = "Result";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(31, 35);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(36, 13);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "Time :";
            // 
            // labelTTL
            // 
            this.labelTTL.AutoSize = true;
            this.labelTTL.Location = new System.Drawing.Point(34, 61);
            this.labelTTL.Name = "labelTTL";
            this.labelTTL.Size = new System.Drawing.Size(33, 13);
            this.labelTTL.TabIndex = 0;
            this.labelTTL.Text = "TTL :";
            // 
            // labelBytes
            // 
            this.labelBytes.AutoSize = true;
            this.labelBytes.Location = new System.Drawing.Point(28, 87);
            this.labelBytes.Name = "labelBytes";
            this.labelBytes.Size = new System.Drawing.Size(39, 13);
            this.labelBytes.TabIndex = 0;
            this.labelBytes.Text = "Bytes :";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(73, 32);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(100, 20);
            this.textBoxTime.TabIndex = 1;
            // 
            // textBoxTTL
            // 
            this.textBoxTTL.Location = new System.Drawing.Point(73, 58);
            this.textBoxTTL.Name = "textBoxTTL";
            this.textBoxTTL.Size = new System.Drawing.Size(100, 20);
            this.textBoxTTL.TabIndex = 1;
            // 
            // textBoxBytes
            // 
            this.textBoxBytes.Location = new System.Drawing.Point(73, 84);
            this.textBoxBytes.Name = "textBoxBytes";
            this.textBoxBytes.Size = new System.Drawing.Size(100, 20);
            this.textBoxBytes.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxHostsReplies);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 165);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // groupBoxHosts
            // 
            this.groupBoxHosts.Controls.Add(this.textBoxHosts);
            this.groupBoxHosts.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxHosts.Location = new System.Drawing.Point(3, 3);
            this.groupBoxHosts.Name = "groupBoxHosts";
            this.groupBoxHosts.Size = new System.Drawing.Size(215, 53);
            this.groupBoxHosts.TabIndex = 2;
            this.groupBoxHosts.TabStop = false;
            this.groupBoxHosts.Text = "Hosts Range";
            // 
            // textBoxHosts
            // 
            this.textBoxHosts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHosts.Location = new System.Drawing.Point(6, 19);
            this.textBoxHosts.Name = "textBoxHosts";
            this.textBoxHosts.Size = new System.Drawing.Size(203, 20);
            this.textBoxHosts.TabIndex = 0;
            this.toolTipHostsRange.SetToolTip(this.textBoxHosts, "This is an example of how to input a range of IPs:\r\n192.168.1.100-192.168.1.200\r\n" +
                    "192.168.1.0\r\nCurrently only IPv4 available, and maximum is 255 hosts.");
            // 
            // listBoxHostsReplies
            // 
            this.listBoxHostsReplies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxHostsReplies.FormattingEnabled = true;
            this.listBoxHostsReplies.Location = new System.Drawing.Point(6, 19);
            this.listBoxHostsReplies.Name = "listBoxHostsReplies";
            this.listBoxHostsReplies.Size = new System.Drawing.Size(203, 134);
            this.listBoxHostsReplies.TabIndex = 0;
            // 
            // toolTipHostsRange
            // 
            this.toolTipHostsRange.ToolTipTitle = "Tips:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 320);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(253, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLabelStatus.Text = "Normal";
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonPing;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(253, 342);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxControls);
            this.Controls.Add(this.tabControlHosts);
            this.MinimumSize = new System.Drawing.Size(269, 380);
            this.Name = "Form1";
            this.Text = "Ping hosts !";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlHosts.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxOneHost.ResumeLayout(false);
            this.groupBoxOneHost.PerformLayout();
            this.groupBoxOneHostRes.ResumeLayout(false);
            this.groupBoxOneHostRes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBoxHosts.ResumeLayout(false);
            this.groupBoxHosts.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlHosts;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxOneHostRes;
        private System.Windows.Forms.TextBox textBoxBytes;
        private System.Windows.Forms.TextBox textBoxTTL;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Label labelBytes;
        private System.Windows.Forms.Label labelTTL;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.GroupBox groupBoxOneHost;
        private System.Windows.Forms.TextBox textBoxOneHostDst;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxHostsReplies;
        private System.Windows.Forms.GroupBox groupBoxHosts;
        private System.Windows.Forms.TextBox textBoxHosts;
        private System.Windows.Forms.ToolTip toolTipHostsRange;
        private System.Windows.Forms.Button buttonPing;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
    }
}

