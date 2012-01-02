namespace CNC
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelIndex = new System.Windows.Forms.Label();
            this.textBoxHead = new System.Windows.Forms.TextBox();
            this.textBoxEncoded = new System.Windows.Forms.TextBox();
            this.labelOri = new System.Windows.Forms.Label();
            this.textBoxOri = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Index: ";
            // 
            // labelIndex
            // 
            this.labelIndex.AutoSize = true;
            this.labelIndex.Location = new System.Drawing.Point(114, 17);
            this.labelIndex.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelIndex.Name = "labelIndex";
            this.labelIndex.Size = new System.Drawing.Size(24, 25);
            this.labelIndex.TabIndex = 1;
            this.labelIndex.Text = "0";
            // 
            // textBoxHead
            // 
            this.textBoxHead.Location = new System.Drawing.Point(29, 48);
            this.textBoxHead.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBoxHead.Name = "textBoxHead";
            this.textBoxHead.Size = new System.Drawing.Size(68, 31);
            this.textBoxHead.TabIndex = 2;
            this.textBoxHead.Text = "2:";
            // 
            // textBoxEncoded
            // 
            this.textBoxEncoded.Location = new System.Drawing.Point(29, 98);
            this.textBoxEncoded.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBoxEncoded.Name = "textBoxEncoded";
            this.textBoxEncoded.Size = new System.Drawing.Size(402, 31);
            this.textBoxEncoded.TabIndex = 2;
            this.textBoxEncoded.TextChanged += new System.EventHandler(this.textBoxOri_TextChanged);
            // 
            // labelOri
            // 
            this.labelOri.AutoSize = true;
            this.labelOri.Location = new System.Drawing.Point(24, 159);
            this.labelOri.Name = "labelOri";
            this.labelOri.Size = new System.Drawing.Size(70, 25);
            this.labelOri.TabIndex = 3;
            this.labelOri.Text = "label2";
            // 
            // textBoxOri
            // 
            this.textBoxOri.Location = new System.Drawing.Point(29, 187);
            this.textBoxOri.Name = "textBoxOri";
            this.textBoxOri.Size = new System.Drawing.Size(402, 31);
            this.textBoxOri.TabIndex = 4;
            this.textBoxOri.TextChanged += new System.EventHandler(this.textBoxOri_TextChanged_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 324);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(402, 31);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 367);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBoxOri);
            this.Controls.Add(this.labelOri);
            this.Controls.Add(this.textBoxEncoded);
            this.Controls.Add(this.textBoxHead);
            this.Controls.Add(this.labelIndex);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelIndex;
        private System.Windows.Forms.TextBox textBoxHead;
        private System.Windows.Forms.TextBox textBoxEncoded;
        private System.Windows.Forms.Label labelOri;
        private System.Windows.Forms.TextBox textBoxOri;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}

