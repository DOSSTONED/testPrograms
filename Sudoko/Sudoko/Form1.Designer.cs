namespace Sudoko
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelCurrentStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(244, 550);
            this.treeView1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(262, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(177, 267);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "0 0 0 0 2 6 0 0 0\r\n0 0 2 5 0 0 0 6 0\r\n6 0 1 0 8 7 5 2 0\r\n0 7 5 9 6 0 0 3 2\r\n0 3 4" +
    " 0 0 0 6 0 0\r\n1 0 0 0 0 0 0 7 9\r\n0 0 0 6 0 9 0 0 0\r\n5 0 3 7 0 2 9 8 0\r\n0 0 9 0 5" +
    " 0 0 1 0\r\n\r\n";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(445, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 76);
            this.button1.TabIndex = 2;
            this.button1.Text = "Calc!";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // labelCurrentStatus
            // 
            this.labelCurrentStatus.AutoSize = true;
            this.labelCurrentStatus.Location = new System.Drawing.Point(262, 282);
            this.labelCurrentStatus.Name = "labelCurrentStatus";
            this.labelCurrentStatus.Size = new System.Drawing.Size(162, 28);
            this.labelCurrentStatus.TabIndex = 3;
            this.labelCurrentStatus.Text = "Current Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 252);
            this.label1.TabIndex = 4;
            this.label1.Text = "0 0 0 0 2 6 0 0 0\r\n0 0 2 5 0 0 0 6 0\r\n6 0 1 0 8 7 5 2 0\r\n0 7 5 9 6 0 0 3 2\r\n0 3 4" +
    " 0 0 0 6 0 0\r\n1 0 0 0 0 0 0 7 9\r\n0 0 0 6 0 9 0 0 0\r\n5 0 3 7 0 2 9 8 0\r\n0 0 9 0 5" +
    " 0 0 1 0\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 574);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCurrentStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.treeView1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelCurrentStatus;
        private System.Windows.Forms.Label label1;


    }
}

