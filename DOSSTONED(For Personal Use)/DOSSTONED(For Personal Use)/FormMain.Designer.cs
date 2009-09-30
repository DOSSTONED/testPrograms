namespace DOSSTONED_For_Personal_Use_
{
    partial class FormPop
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPop));
            this.CheckBoxRepair = new System.Windows.Forms.CheckBox();
            this.CheckBoxProtect = new System.Windows.Forms.CheckBox();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContxtMenuIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripRepair = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripProtect = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusProtect = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusRepair = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnHide = new System.Windows.Forms.Button();
            this.BtnAbt = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ContxtMenuIcon.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckBoxRepair
            // 
            this.CheckBoxRepair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxRepair.AutoSize = true;
            this.CheckBoxRepair.Location = new System.Drawing.Point(137, 174);
            this.CheckBoxRepair.Name = "CheckBoxRepair";
            this.CheckBoxRepair.Size = new System.Drawing.Size(57, 17);
            this.CheckBoxRepair.TabIndex = 0;
            this.CheckBoxRepair.Text = "Repair";
            this.CheckBoxRepair.UseVisualStyleBackColor = true;
            this.CheckBoxRepair.CheckedChanged += new System.EventHandler(this.CheckBoxRepair_CheckedChanged);
            // 
            // CheckBoxProtect
            // 
            this.CheckBoxProtect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxProtect.AutoSize = true;
            this.CheckBoxProtect.Location = new System.Drawing.Point(71, 174);
            this.CheckBoxProtect.Name = "CheckBoxProtect";
            this.CheckBoxProtect.Size = new System.Drawing.Size(60, 17);
            this.CheckBoxProtect.TabIndex = 0;
            this.CheckBoxProtect.Text = "Protect";
            this.CheckBoxProtect.UseVisualStyleBackColor = true;
            this.CheckBoxProtect.CheckedChanged += new System.EventHandler(this.CheckBoxProtect_CheckedChanged);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.ContxtMenuIcon;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "DOSSTONED";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_Click);
            // 
            // ContxtMenuIcon
            // 
            this.ContxtMenuIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripRepair,
            this.ToolStripProtect,
            this.exitToolStripMenuItem});
            this.ContxtMenuIcon.Name = "ContxtMenuIcon";
            this.ContxtMenuIcon.Size = new System.Drawing.Size(113, 70);
            // 
            // ToolStripRepair
            // 
            this.ToolStripRepair.Name = "ToolStripRepair";
            this.ToolStripRepair.Size = new System.Drawing.Size(112, 22);
            this.ToolStripRepair.Text = "Repair";
            this.ToolStripRepair.Click += new System.EventHandler(this.CheckBoxRepair_CheckedChanged);
            // 
            // ToolStripProtect
            // 
            this.ToolStripProtect.Name = "ToolStripProtect";
            this.ToolStripProtect.Size = new System.Drawing.Size(112, 22);
            this.ToolStripProtect.Text = "Protect";
            this.ToolStripProtect.Click += new System.EventHandler(this.CheckBoxProtect_CheckedChanged);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusProtect,
            this.StatusRepair});
            this.StatusStrip.Location = new System.Drawing.Point(0, 223);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(206, 22);
            this.StatusStrip.SizingGrip = false;
            this.StatusStrip.TabIndex = 1;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // StatusProtect
            // 
            this.StatusProtect.Name = "StatusProtect";
            this.StatusProtect.Size = new System.Drawing.Size(113, 17);
            this.StatusProtect.Text = "Protection: Disabled";
            this.StatusProtect.Click += new System.EventHandler(this.CheckBoxProtect_CheckedChanged);
            // 
            // StatusRepair
            // 
            this.StatusRepair.Name = "StatusRepair";
            this.StatusRepair.Size = new System.Drawing.Size(91, 17);
            this.StatusRepair.Text = "Repair: Disabled";
            this.StatusRepair.Click += new System.EventHandler(this.CheckBoxRepair_CheckedChanged);
            // 
            // BtnHide
            // 
            this.BtnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnHide.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnHide.Location = new System.Drawing.Point(119, 197);
            this.BtnHide.Name = "BtnHide";
            this.BtnHide.Size = new System.Drawing.Size(75, 23);
            this.BtnHide.TabIndex = 2;
            this.BtnHide.Text = "Hide";
            this.BtnHide.UseVisualStyleBackColor = true;
            this.BtnHide.Click += new System.EventHandler(this.FormPop_Deactivate);
            // 
            // BtnAbt
            // 
            this.BtnAbt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAbt.Location = new System.Drawing.Point(38, 197);
            this.BtnAbt.Name = "BtnAbt";
            this.BtnAbt.Size = new System.Drawing.Size(75, 23);
            this.BtnAbt.TabIndex = 3;
            this.BtnAbt.Text = "About";
            this.BtnAbt.UseVisualStyleBackColor = true;
            this.BtnAbt.Click += new System.EventHandler(this.BtnAbt_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "3rqwer",
            "234r",
            "wet",
            "w34",
            "t",
            "",
            "34tw"});
            this.listBox1.Location = new System.Drawing.Point(12, 11);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(182, 157);
            this.listBox1.TabIndex = 4;
            // 
            // FormPop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.BtnHide;
            this.ClientSize = new System.Drawing.Size(206, 245);
            this.ControlBox = false;
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.BtnHide);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.CheckBoxRepair);
            this.Controls.Add(this.CheckBoxProtect);
            this.Controls.Add(this.BtnAbt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DOSSTONED";
            this.Deactivate += new System.EventHandler(this.FormPop_Deactivate);
            this.Load += new System.EventHandler(this.FormPop_Load);
            this.ContxtMenuIcon.ResumeLayout(false);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBoxRepair;
        private System.Windows.Forms.CheckBox CheckBoxProtect;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ToolStripStatusLabel StatusRepair;
        private System.Windows.Forms.ToolStripStatusLabel StatusProtect;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.Button BtnHide;
        private System.Windows.Forms.Button BtnAbt;
        private System.Windows.Forms.ContextMenuStrip ContxtMenuIcon;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripRepair;
        private System.Windows.Forms.ToolStripMenuItem ToolStripProtect;
        private System.Windows.Forms.ListBox listBox1;
    }
}

