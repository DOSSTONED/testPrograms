namespace Elliptic_Curves
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
            this.textBoxOriCrd = new System.Windows.Forms.TextBox();
            this.panelOriCrd = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxOriX = new System.Windows.Forms.TextBox();
            this.numericUpDownFormula2 = new System.Windows.Forms.NumericUpDown();
            this.labelFormula1 = new System.Windows.Forms.Label();
            this.labelFormula3 = new System.Windows.Forms.Label();
            this.numericUpDownFormula4 = new System.Windows.Forms.NumericUpDown();
            this.panelOutputY = new System.Windows.Forms.Panel();
            this.textBoxOutputY = new System.Windows.Forms.TextBox();
            this.numericUpDownScale = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFormula2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFormula4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxOriCrd
            // 
            this.textBoxOriCrd.Location = new System.Drawing.Point(12, 12);
            this.textBoxOriCrd.Name = "textBoxOriCrd";
            this.textBoxOriCrd.Size = new System.Drawing.Size(183, 21);
            this.textBoxOriCrd.TabIndex = 0;
            // 
            // panelOriCrd
            // 
            this.panelOriCrd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelOriCrd.BackColor = System.Drawing.Color.Teal;
            this.panelOriCrd.Location = new System.Drawing.Point(12, 39);
            this.panelOriCrd.Name = "panelOriCrd";
            this.panelOriCrd.Size = new System.Drawing.Size(441, 492);
            this.panelOriCrd.TabIndex = 1;
            this.panelOriCrd.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelOriCrd_MouseClick);
            this.panelOriCrd.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelOriCrd_MouseMove);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(378, 10);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxOriX
            // 
            this.textBoxOriX.Location = new System.Drawing.Point(201, 12);
            this.textBoxOriX.Name = "textBoxOriX";
            this.textBoxOriX.Size = new System.Drawing.Size(171, 21);
            this.textBoxOriX.TabIndex = 3;
            // 
            // numericUpDownFormula2
            // 
            this.numericUpDownFormula2.DecimalPlaces = 1;
            this.numericUpDownFormula2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownFormula2.Location = new System.Drawing.Point(546, 13);
            this.numericUpDownFormula2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownFormula2.Name = "numericUpDownFormula2";
            this.numericUpDownFormula2.Size = new System.Drawing.Size(58, 21);
            this.numericUpDownFormula2.TabIndex = 4;
            this.numericUpDownFormula2.Value = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            // 
            // labelFormula1
            // 
            this.labelFormula1.AutoSize = true;
            this.labelFormula1.Location = new System.Drawing.Point(469, 15);
            this.labelFormula1.Name = "labelFormula1";
            this.labelFormula1.Size = new System.Drawing.Size(71, 12);
            this.labelFormula1.TabIndex = 5;
            this.labelFormula1.Text = "y^2 = x^3 +";
            // 
            // labelFormula3
            // 
            this.labelFormula3.AutoSize = true;
            this.labelFormula3.Location = new System.Drawing.Point(610, 15);
            this.labelFormula3.Name = "labelFormula3";
            this.labelFormula3.Size = new System.Drawing.Size(23, 12);
            this.labelFormula3.TabIndex = 6;
            this.labelFormula3.Text = "x +";
            // 
            // numericUpDownFormula4
            // 
            this.numericUpDownFormula4.DecimalPlaces = 1;
            this.numericUpDownFormula4.Location = new System.Drawing.Point(639, 13);
            this.numericUpDownFormula4.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownFormula4.Name = "numericUpDownFormula4";
            this.numericUpDownFormula4.Size = new System.Drawing.Size(62, 21);
            this.numericUpDownFormula4.TabIndex = 7;
            // 
            // panelOutputY
            // 
            this.panelOutputY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelOutputY.BackColor = System.Drawing.Color.Teal;
            this.panelOutputY.Location = new System.Drawing.Point(471, 39);
            this.panelOutputY.Name = "panelOutputY";
            this.panelOutputY.Size = new System.Drawing.Size(444, 492);
            this.panelOutputY.TabIndex = 8;
            this.panelOutputY.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelOutputY_MouseClick);
            // 
            // textBoxOutputY
            // 
            this.textBoxOutputY.Location = new System.Drawing.Point(707, 12);
            this.textBoxOutputY.Name = "textBoxOutputY";
            this.textBoxOutputY.Size = new System.Drawing.Size(131, 21);
            this.textBoxOutputY.TabIndex = 9;
            // 
            // numericUpDownScale
            // 
            this.numericUpDownScale.DecimalPlaces = 1;
            this.numericUpDownScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownScale.Location = new System.Drawing.Point(845, 10);
            this.numericUpDownScale.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownScale.Name = "numericUpDownScale";
            this.numericUpDownScale.Size = new System.Drawing.Size(70, 21);
            this.numericUpDownScale.TabIndex = 10;
            this.numericUpDownScale.Value = new decimal(new int[] {
            30,
            0,
            0,
            65536});
            this.numericUpDownScale.ValueChanged += new System.EventHandler(this.numericUpDownScale_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 543);
            this.Controls.Add(this.numericUpDownScale);
            this.Controls.Add(this.textBoxOutputY);
            this.Controls.Add(this.panelOutputY);
            this.Controls.Add(this.numericUpDownFormula4);
            this.Controls.Add(this.labelFormula3);
            this.Controls.Add(this.labelFormula1);
            this.Controls.Add(this.numericUpDownFormula2);
            this.Controls.Add(this.textBoxOriX);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.panelOriCrd);
            this.Controls.Add(this.textBoxOriCrd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFormula2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFormula4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOriCrd;
        private System.Windows.Forms.Panel panelOriCrd;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox textBoxOriX;
        private System.Windows.Forms.NumericUpDown numericUpDownFormula2;
        private System.Windows.Forms.Label labelFormula1;
        private System.Windows.Forms.Label labelFormula3;
        private System.Windows.Forms.NumericUpDown numericUpDownFormula4;
        private System.Windows.Forms.Panel panelOutputY;
        private System.Windows.Forms.TextBox textBoxOutputY;
        private System.Windows.Forms.NumericUpDown numericUpDownScale;
    }
}

