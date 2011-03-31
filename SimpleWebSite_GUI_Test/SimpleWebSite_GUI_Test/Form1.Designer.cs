namespace SimpleWebSite_GUI_Test
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonKnow = new System.Windows.Forms.Button();
            this.buttonDontKnow = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.checkBoxUseIPv6Proxy = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTotalWords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCurReviewed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCurKnows = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxShowWB = new System.Windows.Forms.CheckBox();
            this.labelExplanation = new System.Windows.Forms.Label();
            this.checkBoxSound = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(374, 411);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save!";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(17, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 407);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // buttonKnow
            // 
            this.buttonKnow.Location = new System.Drawing.Point(17, 41);
            this.buttonKnow.Name = "buttonKnow";
            this.buttonKnow.Size = new System.Drawing.Size(160, 23);
            this.buttonKnow.TabIndex = 4;
            this.buttonKnow.Text = "I know it!";
            this.buttonKnow.UseVisualStyleBackColor = true;
            this.buttonKnow.Click += new System.EventHandler(this.buttonKnow_Click);
            // 
            // buttonDontKnow
            // 
            this.buttonDontKnow.Location = new System.Drawing.Point(17, 70);
            this.buttonDontKnow.Name = "buttonDontKnow";
            this.buttonDontKnow.Size = new System.Drawing.Size(160, 23);
            this.buttonDontKnow.TabIndex = 4;
            this.buttonDontKnow.Text = "I don\'t know it!";
            this.buttonDontKnow.UseVisualStyleBackColor = true;
            this.buttonDontKnow.Click += new System.EventHandler(this.buttonDontKnow_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.webBrowser1.Location = new System.Drawing.Point(17, 182);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(211, 223);
            this.webBrowser1.TabIndex = 6;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // checkBoxUseIPv6Proxy
            // 
            this.checkBoxUseIPv6Proxy.AutoSize = true;
            this.checkBoxUseIPv6Proxy.Location = new System.Drawing.Point(17, 99);
            this.checkBoxUseIPv6Proxy.Name = "checkBoxUseIPv6Proxy";
            this.checkBoxUseIPv6Proxy.Size = new System.Drawing.Size(114, 17);
            this.checkBoxUseIPv6Proxy.TabIndex = 7;
            this.checkBoxUseIPv6Proxy.Text = "use sixxs.org proxy";
            this.checkBoxUseIPv6Proxy.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTotalWords,
            this.toolStripStatusLabelCurReviewed,
            this.toolStripStatusLabelCurKnows});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(461, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelTotalWords
            // 
            this.toolStripStatusLabelTotalWords.Name = "toolStripStatusLabelTotalWords";
            this.toolStripStatusLabelTotalWords.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusLabelTotalWords.Text = "Total Words:";
            // 
            // toolStripStatusLabelCurReviewed
            // 
            this.toolStripStatusLabelCurReviewed.Name = "toolStripStatusLabelCurReviewed";
            this.toolStripStatusLabelCurReviewed.Size = new System.Drawing.Size(123, 17);
            this.toolStripStatusLabelCurReviewed.Text = "Reviewed this session:";
            // 
            // toolStripStatusLabelCurKnows
            // 
            this.toolStripStatusLabelCurKnows.Name = "toolStripStatusLabelCurKnows";
            this.toolStripStatusLabelCurKnows.Size = new System.Drawing.Size(82, 17);
            this.toolStripStatusLabelCurKnows.Text = "Known words:";
            // 
            // checkBoxShowWB
            // 
            this.checkBoxShowWB.AutoSize = true;
            this.checkBoxShowWB.Checked = true;
            this.checkBoxShowWB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowWB.Location = new System.Drawing.Point(17, 122);
            this.checkBoxShowWB.Name = "checkBoxShowWB";
            this.checkBoxShowWB.Size = new System.Drawing.Size(94, 17);
            this.checkBoxShowWB.TabIndex = 9;
            this.checkBoxShowWB.Text = "show meaning";
            this.checkBoxShowWB.UseVisualStyleBackColor = true;
            this.checkBoxShowWB.CheckedChanged += new System.EventHandler(this.checkBoxShowWB_CheckedChanged);
            this.checkBoxShowWB.MouseEnter += new System.EventHandler(this.checkBoxShowWB_MouseEnter);
            // 
            // labelExplanation
            // 
            this.labelExplanation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExplanation.Font = new System.Drawing.Font("SimHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelExplanation.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelExplanation.Location = new System.Drawing.Point(234, 10);
            this.labelExplanation.Name = "labelExplanation";
            this.labelExplanation.Size = new System.Drawing.Size(215, 395);
            this.labelExplanation.TabIndex = 10;
            this.labelExplanation.Text = "解释";
            // 
            // checkBoxSound
            // 
            this.checkBoxSound.AutoSize = true;
            this.checkBoxSound.Location = new System.Drawing.Point(17, 145);
            this.checkBoxSound.Name = "checkBoxSound";
            this.checkBoxSound.Size = new System.Drawing.Size(89, 17);
            this.checkBoxSound.TabIndex = 11;
            this.checkBoxSound.Text = "Pronounce it!";
            this.checkBoxSound.UseVisualStyleBackColor = true;
            this.checkBoxSound.CheckedChanged += new System.EventHandler(this.checkBoxSound_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 459);
            this.Controls.Add(this.checkBoxSound);
            this.Controls.Add(this.labelExplanation);
            this.Controls.Add(this.checkBoxShowWB);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.checkBoxUseIPv6Proxy);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.buttonDontKnow);
            this.Controls.Add(this.buttonKnow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonKnow;
        private System.Windows.Forms.Button buttonDontKnow;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.CheckBox checkBoxUseIPv6Proxy;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTotalWords;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurReviewed;
        private System.Windows.Forms.CheckBox checkBoxShowWB;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurKnows;
        private System.Windows.Forms.Label labelExplanation;
        private System.Windows.Forms.CheckBox checkBoxSound;

    }
}

