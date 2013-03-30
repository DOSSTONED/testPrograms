namespace 统计网页
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
            this.textBoxURLs = new System.Windows.Forms.TextBox();
            this.buttonNext = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSum = new System.Windows.Forms.Button();
            this.listBoxTitle = new System.Windows.Forms.ListBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.listBoxURL = new System.Windows.Forms.ListBox();
            this.buttonyYAO = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxURLs
            // 
            this.textBoxURLs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURLs.Location = new System.Drawing.Point(637, 535);
            this.textBoxURLs.Multiline = true;
            this.textBoxURLs.Name = "textBoxURLs";
            this.textBoxURLs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxURLs.Size = new System.Drawing.Size(594, 280);
            this.textBoxURLs.TabIndex = 3;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(203, 54);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(71, 23);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(298, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(933, 517);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(66, 23);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Engine:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Page:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(66, 49);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSum);
            this.groupBox1.Controls.Add(this.listBoxTitle);
            this.groupBox1.Controls.Add(this.textBoxTitle);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonBack);
            this.groupBox1.Controls.Add(this.buttonNext);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 440);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // buttonSum
            // 
            this.buttonSum.Location = new System.Drawing.Point(136, 15);
            this.buttonSum.Name = "buttonSum";
            this.buttonSum.Size = new System.Drawing.Size(138, 33);
            this.buttonSum.TabIndex = 8;
            this.buttonSum.Text = "Sum!";
            this.buttonSum.UseVisualStyleBackColor = true;
            this.buttonSum.Click += new System.EventHandler(this.buttonSum_Click);
            // 
            // listBoxTitle
            // 
            this.listBoxTitle.FormattingEnabled = true;
            this.listBoxTitle.Items.AddRange(new object[] {
            "Surface Pro即将降临 上手体验先睹为快",
            "巧用Windows 8的文件历史功能自动备份文件"});
            this.listBoxTitle.Location = new System.Drawing.Point(6, 131);
            this.listBoxTitle.Name = "listBoxTitle";
            this.listBoxTitle.Size = new System.Drawing.Size(268, 303);
            this.listBoxTitle.TabIndex = 7;
            this.listBoxTitle.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Location = new System.Drawing.Point(6, 93);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(268, 20);
            this.textBoxTitle.TabIndex = 7;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(136, 54);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(61, 23);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxURL
            // 
            this.listBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxURL.FormattingEnabled = true;
            this.listBoxURL.Location = new System.Drawing.Point(12, 538);
            this.listBoxURL.Name = "listBoxURL";
            this.listBoxURL.Size = new System.Drawing.Size(619, 277);
            this.listBoxURL.TabIndex = 7;
            this.listBoxURL.SelectedIndexChanged += new System.EventHandler(this.listBoxURL_SelectedIndexChanged);
            // 
            // buttonyYAO
            // 
            this.buttonyYAO.Location = new System.Drawing.Point(12, 458);
            this.buttonyYAO.Name = "buttonyYAO";
            this.buttonyYAO.Size = new System.Drawing.Size(91, 71);
            this.buttonyYAO.TabIndex = 8;
            this.buttonyYAO.Text = "要这个页面";
            this.buttonyYAO.UseVisualStyleBackColor = true;
            this.buttonyYAO.Click += new System.EventHandler(this.buttonyYAO_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(118, 458);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(91, 71);
            this.buttonDel.TabIndex = 8;
            this.buttonDel.Text = "这个没用";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(217, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 55);
            this.button1.TabIndex = 9;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 827);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonyYAO);
            this.Controls.Add(this.listBoxURL);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxURLs);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxURLs;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.ListBox listBoxTitle;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSum;
        private System.Windows.Forms.ListBox listBoxURL;
        private System.Windows.Forms.Button buttonyYAO;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button button1;
    }
}

