namespace Notes
{
    partial class FormNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNotes));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.comboBoxEvent = new System.Windows.Forms.ComboBox();
            this.CheckBoxProtect = new System.Windows.Forms.CheckBox();
            this.CheckBoxRepair = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(381, 26);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DoubleClick += new System.EventHandler(this.tabControl1_DoubleClick);
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // textBoxNote
            // 
            this.textBoxNote.AcceptsReturn = true;
            this.textBoxNote.AcceptsTab = true;
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBoxNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNote.Location = new System.Drawing.Point(2, 23);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNote.Size = new System.Drawing.Size(381, 387);
            this.textBoxNote.TabIndex = 0;
            // 
            // comboBoxEvent
            // 
            this.comboBoxEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEvent.FormattingEnabled = true;
            this.comboBoxEvent.Location = new System.Drawing.Point(161, 416);
            this.comboBoxEvent.Name = "comboBoxEvent";
            this.comboBoxEvent.Size = new System.Drawing.Size(222, 24);
            this.comboBoxEvent.TabIndex = 1;
            this.comboBoxEvent.TabStop = false;
            // 
            // CheckBoxProtect
            // 
            this.CheckBoxProtect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBoxProtect.AutoSize = true;
            this.CheckBoxProtect.Location = new System.Drawing.Point(2, 418);
            this.CheckBoxProtect.Name = "CheckBoxProtect";
            this.CheckBoxProtect.Size = new System.Drawing.Size(75, 21);
            this.CheckBoxProtect.TabIndex = 2;
            this.CheckBoxProtect.Text = "Protect";
            this.CheckBoxProtect.UseVisualStyleBackColor = true;
            this.CheckBoxProtect.CheckedChanged += new System.EventHandler(this.CheckBoxProtect_CheckedChanged);
            // 
            // CheckBoxRepair
            // 
            this.CheckBoxRepair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckBoxRepair.AutoSize = true;
            this.CheckBoxRepair.Location = new System.Drawing.Point(83, 418);
            this.CheckBoxRepair.Name = "CheckBoxRepair";
            this.CheckBoxRepair.Size = new System.Drawing.Size(72, 21);
            this.CheckBoxRepair.TabIndex = 2;
            this.CheckBoxRepair.Text = "Repair";
            this.CheckBoxRepair.UseVisualStyleBackColor = true;
            this.CheckBoxRepair.CheckedChanged += new System.EventHandler(this.CheckBoxRepair_CheckedChanged);
            // 
            // FormNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(385, 443);
            this.Controls.Add(this.CheckBoxRepair);
            this.Controls.Add(this.CheckBoxProtect);
            this.Controls.Add(this.comboBoxEvent);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNotes";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.FormNotes_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNotes_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.ComboBox comboBoxEvent;
        private System.Windows.Forms.CheckBox CheckBoxProtect;
        private System.Windows.Forms.CheckBox CheckBoxRepair;
    }
}

