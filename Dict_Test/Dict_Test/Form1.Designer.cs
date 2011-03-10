namespace Dict_Test
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
            this.groupBoxWordInfo = new System.Windows.Forms.GroupBox();
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.buttonWordShowMeaning = new System.Windows.Forms.Button();
            this.checkBoxIsNew = new System.Windows.Forms.CheckBox();
            this.textBoxWordMeaning = new System.Windows.Forms.TextBox();
            this.labelWordInCap = new System.Windows.Forms.Label();
            this.buttonSaveInfo = new System.Windows.Forms.Button();
            this.listBoxNew = new System.Windows.Forms.ListBox();
            this.listBoxOld = new System.Windows.Forms.ListBox();
            this.groupBoxWordInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxWordInfo
            // 
            this.groupBoxWordInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWordInfo.Controls.Add(this.textBoxWord);
            this.groupBoxWordInfo.Controls.Add(this.buttonWordShowMeaning);
            this.groupBoxWordInfo.Controls.Add(this.checkBoxIsNew);
            this.groupBoxWordInfo.Controls.Add(this.textBoxWordMeaning);
            this.groupBoxWordInfo.Controls.Add(this.labelWordInCap);
            this.groupBoxWordInfo.Controls.Add(this.buttonSaveInfo);
            this.groupBoxWordInfo.Location = new System.Drawing.Point(205, 12);
            this.groupBoxWordInfo.Name = "groupBoxWordInfo";
            this.groupBoxWordInfo.Size = new System.Drawing.Size(217, 444);
            this.groupBoxWordInfo.TabIndex = 0;
            this.groupBoxWordInfo.TabStop = false;
            this.groupBoxWordInfo.Text = "Word Info";
            // 
            // textBoxWord
            // 
            this.textBoxWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWord.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxWord.Location = new System.Drawing.Point(10, 25);
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.Size = new System.Drawing.Size(201, 26);
            this.textBoxWord.TabIndex = 0;
            // 
            // buttonWordShowMeaning
            // 
            this.buttonWordShowMeaning.Location = new System.Drawing.Point(51, 242);
            this.buttonWordShowMeaning.Name = "buttonWordShowMeaning";
            this.buttonWordShowMeaning.Size = new System.Drawing.Size(121, 93);
            this.buttonWordShowMeaning.TabIndex = 2;
            this.buttonWordShowMeaning.Text = "Show!";
            this.buttonWordShowMeaning.UseVisualStyleBackColor = true;
            this.buttonWordShowMeaning.Click += new System.EventHandler(this.buttonWordShowMeaning_Click);
            // 
            // checkBoxIsNew
            // 
            this.checkBoxIsNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxIsNew.AutoSize = true;
            this.checkBoxIsNew.Checked = true;
            this.checkBoxIsNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsNew.Location = new System.Drawing.Point(10, 419);
            this.checkBoxIsNew.Name = "checkBoxIsNew";
            this.checkBoxIsNew.Size = new System.Drawing.Size(78, 17);
            this.checkBoxIsNew.TabIndex = 3;
            this.checkBoxIsNew.Text = "NEW word";
            this.checkBoxIsNew.UseVisualStyleBackColor = true;
            // 
            // textBoxWordMeaning
            // 
            this.textBoxWordMeaning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWordMeaning.Location = new System.Drawing.Point(10, 77);
            this.textBoxWordMeaning.Multiline = true;
            this.textBoxWordMeaning.Name = "textBoxWordMeaning";
            this.textBoxWordMeaning.Size = new System.Drawing.Size(201, 326);
            this.textBoxWordMeaning.TabIndex = 1;
            this.textBoxWordMeaning.Visible = false;
            this.textBoxWordMeaning.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxWordMeaning_KeyDown);
            this.textBoxWordMeaning.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWordMeaning_KeyPress);
            // 
            // labelWordInCap
            // 
            this.labelWordInCap.AutoSize = true;
            this.labelWordInCap.Location = new System.Drawing.Point(6, 54);
            this.labelWordInCap.Name = "labelWordInCap";
            this.labelWordInCap.Size = new System.Drawing.Size(60, 20);
            this.labelWordInCap.TabIndex = 2;
            this.labelWordInCap.Text = "WORD";
            // 
            // buttonSaveInfo
            // 
            this.buttonSaveInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveInfo.Location = new System.Drawing.Point(136, 409);
            this.buttonSaveInfo.Name = "buttonSaveInfo";
            this.buttonSaveInfo.Size = new System.Drawing.Size(75, 29);
            this.buttonSaveInfo.TabIndex = 4;
            this.buttonSaveInfo.Text = "Save Info";
            this.buttonSaveInfo.UseVisualStyleBackColor = true;
            this.buttonSaveInfo.Click += new System.EventHandler(this.buttonSaveInfo_Click);
            // 
            // listBoxNew
            // 
            this.listBoxNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxNew.FormattingEnabled = true;
            this.listBoxNew.ItemHeight = 20;
            this.listBoxNew.Location = new System.Drawing.Point(12, 12);
            this.listBoxNew.Name = "listBoxNew";
            this.listBoxNew.Size = new System.Drawing.Size(187, 424);
            this.listBoxNew.TabIndex = 0;
            this.listBoxNew.SelectedIndexChanged += new System.EventHandler(this.WordList_SelectedIndexChanged);
            // 
            // listBoxOld
            // 
            this.listBoxOld.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxOld.FormattingEnabled = true;
            this.listBoxOld.ItemHeight = 20;
            this.listBoxOld.Location = new System.Drawing.Point(428, 12);
            this.listBoxOld.Name = "listBoxOld";
            this.listBoxOld.Size = new System.Drawing.Size(187, 424);
            this.listBoxOld.TabIndex = 1;
            this.listBoxOld.SelectedIndexChanged += new System.EventHandler(this.WordList_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(627, 468);
            this.Controls.Add(this.listBoxOld);
            this.Controls.Add(this.listBoxNew);
            this.Controls.Add(this.groupBoxWordInfo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "My Own Dicts";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxWordInfo.ResumeLayout(false);
            this.groupBoxWordInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxWordInfo;
        private System.Windows.Forms.Button buttonSaveInfo;
        private System.Windows.Forms.ListBox listBoxNew;
        private System.Windows.Forms.ListBox listBoxOld;
        private System.Windows.Forms.TextBox textBoxWordMeaning;
        private System.Windows.Forms.Label labelWordInCap;
        private System.Windows.Forms.CheckBox checkBoxIsNew;
        private System.Windows.Forms.Button buttonWordShowMeaning;
        private System.Windows.Forms.TextBox textBoxWord;

    }
}

