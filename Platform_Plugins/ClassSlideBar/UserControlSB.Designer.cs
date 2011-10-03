namespace ClassSlideBar
{
    partial class UserControlSB
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelSB = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSBMode = new System.Windows.Forms.GroupBox();
            this.checkBoxP0 = new System.Windows.Forms.CheckBox();
            this.checkBoxP1 = new System.Windows.Forms.CheckBox();
            this.checkBoxP2 = new System.Windows.Forms.CheckBox();
            this.checkBoxP3 = new System.Windows.Forms.CheckBox();
            this.checkBoxP4 = new System.Windows.Forms.CheckBox();
            this.checkBoxP5 = new System.Windows.Forms.CheckBox();
            this.checkBoxP6 = new System.Windows.Forms.CheckBox();
            this.checkBoxP7 = new System.Windows.Forms.CheckBox();
            this.checkBoxBreathe = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBoxSBMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSB
            // 
            this.labelSB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSB.AutoSize = true;
            this.labelSB.BackColor = System.Drawing.Color.Transparent;
            this.labelSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSB.ForeColor = System.Drawing.Color.Aqua;
            this.labelSB.Location = new System.Drawing.Point(3, 176);
            this.labelSB.Name = "labelSB";
            this.labelSB.Size = new System.Drawing.Size(45, 16);
            this.labelSB.TabIndex = 0;
            this.labelSB.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "SlideBar display panel\r\n";
            // 
            // groupBoxSBMode
            // 
            this.groupBoxSBMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSBMode.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSBMode.Controls.Add(this.checkBoxP0);
            this.groupBoxSBMode.Controls.Add(this.checkBoxP1);
            this.groupBoxSBMode.Controls.Add(this.checkBoxP2);
            this.groupBoxSBMode.Controls.Add(this.checkBoxP3);
            this.groupBoxSBMode.Controls.Add(this.checkBoxP4);
            this.groupBoxSBMode.Controls.Add(this.checkBoxP5);
            this.groupBoxSBMode.Controls.Add(this.checkBoxP6);
            this.groupBoxSBMode.Controls.Add(this.checkBoxP7);
            this.groupBoxSBMode.Controls.Add(this.checkBoxBreathe);
            this.groupBoxSBMode.Controls.Add(this.comboBox1);
            this.groupBoxSBMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSBMode.ForeColor = System.Drawing.Color.Aqua;
            this.groupBoxSBMode.Location = new System.Drawing.Point(8, 28);
            this.groupBoxSBMode.Name = "groupBoxSBMode";
            this.groupBoxSBMode.Size = new System.Drawing.Size(436, 145);
            this.groupBoxSBMode.TabIndex = 2;
            this.groupBoxSBMode.TabStop = false;
            this.groupBoxSBMode.Text = "SlideBar Mode";
            // 
            // checkBoxP0
            // 
            this.checkBoxP0.AutoSize = true;
            this.checkBoxP0.Location = new System.Drawing.Point(220, 104);
            this.checkBoxP0.Name = "checkBoxP0";
            this.checkBoxP0.Size = new System.Drawing.Size(43, 29);
            this.checkBoxP0.TabIndex = 5;
            this.checkBoxP0.Text = "0";
            this.checkBoxP0.UseVisualStyleBackColor = true;
            this.checkBoxP0.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxP1
            // 
            this.checkBoxP1.AutoSize = true;
            this.checkBoxP1.Location = new System.Drawing.Point(171, 104);
            this.checkBoxP1.Name = "checkBoxP1";
            this.checkBoxP1.Size = new System.Drawing.Size(43, 29);
            this.checkBoxP1.TabIndex = 5;
            this.checkBoxP1.Text = "1";
            this.checkBoxP1.UseVisualStyleBackColor = true;
            this.checkBoxP1.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxP2
            // 
            this.checkBoxP2.AutoSize = true;
            this.checkBoxP2.Location = new System.Drawing.Point(55, 104);
            this.checkBoxP2.Name = "checkBoxP2";
            this.checkBoxP2.Size = new System.Drawing.Size(110, 29);
            this.checkBoxP2.TabIndex = 5;
            this.checkBoxP2.Text = "2 (Light)";
            this.checkBoxP2.UseVisualStyleBackColor = true;
            this.checkBoxP2.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxP3
            // 
            this.checkBoxP3.AutoSize = true;
            this.checkBoxP3.Location = new System.Drawing.Point(6, 104);
            this.checkBoxP3.Name = "checkBoxP3";
            this.checkBoxP3.Size = new System.Drawing.Size(43, 29);
            this.checkBoxP3.TabIndex = 5;
            this.checkBoxP3.Text = "3";
            this.checkBoxP3.UseVisualStyleBackColor = true;
            this.checkBoxP3.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxP4
            // 
            this.checkBoxP4.AutoSize = true;
            this.checkBoxP4.Location = new System.Drawing.Point(153, 69);
            this.checkBoxP4.Name = "checkBoxP4";
            this.checkBoxP4.Size = new System.Drawing.Size(43, 29);
            this.checkBoxP4.TabIndex = 5;
            this.checkBoxP4.Text = "4";
            this.checkBoxP4.UseVisualStyleBackColor = true;
            this.checkBoxP4.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxP5
            // 
            this.checkBoxP5.AutoSize = true;
            this.checkBoxP5.Location = new System.Drawing.Point(104, 69);
            this.checkBoxP5.Name = "checkBoxP5";
            this.checkBoxP5.Size = new System.Drawing.Size(43, 29);
            this.checkBoxP5.TabIndex = 5;
            this.checkBoxP5.Text = "5";
            this.checkBoxP5.UseVisualStyleBackColor = true;
            this.checkBoxP5.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxP6
            // 
            this.checkBoxP6.AutoSize = true;
            this.checkBoxP6.Location = new System.Drawing.Point(55, 69);
            this.checkBoxP6.Name = "checkBoxP6";
            this.checkBoxP6.Size = new System.Drawing.Size(43, 29);
            this.checkBoxP6.TabIndex = 5;
            this.checkBoxP6.Text = "6";
            this.checkBoxP6.UseVisualStyleBackColor = true;
            this.checkBoxP6.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxP7
            // 
            this.checkBoxP7.AutoSize = true;
            this.checkBoxP7.Location = new System.Drawing.Point(6, 69);
            this.checkBoxP7.Name = "checkBoxP7";
            this.checkBoxP7.Size = new System.Drawing.Size(43, 29);
            this.checkBoxP7.TabIndex = 5;
            this.checkBoxP7.Text = "7";
            this.checkBoxP7.UseVisualStyleBackColor = true;
            this.checkBoxP7.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // checkBoxBreathe
            // 
            this.checkBoxBreathe.AutoSize = true;
            this.checkBoxBreathe.Checked = true;
            this.checkBoxBreathe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBreathe.Location = new System.Drawing.Point(202, 69);
            this.checkBoxBreathe.Name = "checkBoxBreathe";
            this.checkBoxBreathe.Size = new System.Drawing.Size(106, 29);
            this.checkBoxBreathe.TabIndex = 4;
            this.checkBoxBreathe.Text = "Breathe";
            this.checkBoxBreathe.UseVisualStyleBackColor = true;
            this.checkBoxBreathe.CheckedChanged += new System.EventHandler(this.SetStatusBit);
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "0";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(215, 33);
            this.comboBox1.TabIndex = 2;
            // 
            // UserControlSB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ClassSlideBar.Properties.Resources._018_4d3e765edc687dfc86364caf6c0f583a;
            this.Controls.Add(this.groupBoxSBMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelSB);
            this.Name = "UserControlSB";
            this.Size = new System.Drawing.Size(447, 201);
            this.Load += new System.EventHandler(this.UserControlSB_Load);
            this.groupBoxSBMode.ResumeLayout(false);
            this.groupBoxSBMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxSBMode;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBoxBreathe;
        private System.Windows.Forms.CheckBox checkBoxP0;
        private System.Windows.Forms.CheckBox checkBoxP1;
        private System.Windows.Forms.CheckBox checkBoxP2;
        private System.Windows.Forms.CheckBox checkBoxP3;
        private System.Windows.Forms.CheckBox checkBoxP4;
        private System.Windows.Forms.CheckBox checkBoxP5;
        private System.Windows.Forms.CheckBox checkBoxP6;
        private System.Windows.Forms.CheckBox checkBoxP7;
    }
}
