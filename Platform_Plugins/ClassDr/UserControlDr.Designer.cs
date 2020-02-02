namespace ClassDr
{
    partial class UserControlDr
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
            this.textBoxUsrName = new System.Windows.Forms.TextBox();
            this.maskedTextBoxUsrPas = new System.Windows.Forms.MaskedTextBox();
            this.groupBox登陆 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonIPv4 = new System.Windows.Forms.Button();
            this.buttonIPv6 = new System.Windows.Forms.Button();
            this.groupBox信息 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox登陆.SuspendLayout();
            this.groupBox信息.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUsrName
            // 
            this.textBoxUsrName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUsrName.Location = new System.Drawing.Point(109, 32);
            this.textBoxUsrName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxUsrName.Name = "textBoxUsrName";
            this.textBoxUsrName.Size = new System.Drawing.Size(261, 22);
            this.textBoxUsrName.TabIndex = 0;
            // 
            // maskedTextBoxUsrPas
            // 
            this.maskedTextBoxUsrPas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBoxUsrPas.Location = new System.Drawing.Point(109, 73);
            this.maskedTextBoxUsrPas.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.maskedTextBoxUsrPas.Name = "maskedTextBoxUsrPas";
            this.maskedTextBoxUsrPas.PasswordChar = '●';
            this.maskedTextBoxUsrPas.Size = new System.Drawing.Size(261, 22);
            this.maskedTextBoxUsrPas.TabIndex = 1;
            // 
            // groupBox登陆
            // 
            this.groupBox登陆.BackColor = System.Drawing.Color.Transparent;
            this.groupBox登陆.Controls.Add(this.buttonIPv6);
            this.groupBox登陆.Controls.Add(this.buttonIPv4);
            this.groupBox登陆.Controls.Add(this.label2);
            this.groupBox登陆.Controls.Add(this.label1);
            this.groupBox登陆.Controls.Add(this.textBoxUsrName);
            this.groupBox登陆.Controls.Add(this.maskedTextBoxUsrPas);
            this.groupBox登陆.Font = new System.Drawing.Font("SimSun-ExtB", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox登陆.Location = new System.Drawing.Point(130, 110);
            this.groupBox登陆.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox登陆.Name = "groupBox登陆";
            this.groupBox登陆.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox登陆.Size = new System.Drawing.Size(380, 179);
            this.groupBox登陆.TabIndex = 2;
            this.groupBox登陆.TabStop = false;
            this.groupBox登陆.Text = "登陆";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码：";
            // 
            // buttonIPv4
            // 
            this.buttonIPv4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonIPv4.Location = new System.Drawing.Point(13, 123);
            this.buttonIPv4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonIPv4.Name = "buttonIPv4";
            this.buttonIPv4.Size = new System.Drawing.Size(147, 48);
            this.buttonIPv4.TabIndex = 3;
            this.buttonIPv4.Text = "IPv4登陆";
            this.buttonIPv4.UseVisualStyleBackColor = true;
            // 
            // buttonIPv6
            // 
            this.buttonIPv6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIPv6.Location = new System.Drawing.Point(225, 123);
            this.buttonIPv6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonIPv6.Name = "buttonIPv6";
            this.buttonIPv6.Size = new System.Drawing.Size(147, 48);
            this.buttonIPv6.TabIndex = 3;
            this.buttonIPv6.Text = "IPv6登陆";
            this.buttonIPv6.UseVisualStyleBackColor = true;
            // 
            // groupBox信息
            // 
            this.groupBox信息.BackColor = System.Drawing.Color.Transparent;
            this.groupBox信息.Controls.Add(this.label3);
            this.groupBox信息.Font = new System.Drawing.Font("SimSun-ExtB", 11.25F);
            this.groupBox信息.Location = new System.Drawing.Point(88, 295);
            this.groupBox信息.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox信息.Name = "groupBox信息";
            this.groupBox信息.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox信息.Size = new System.Drawing.Size(464, 85);
            this.groupBox信息.TabIndex = 3;
            this.groupBox信息.TabStop = false;
            this.groupBox信息.Text = "信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "label1";
            // 
            // UserControlDr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ClassDrcom.Properties.Resources.BackGround;
            this.Controls.Add(this.groupBox信息);
            this.Controls.Add(this.groupBox登陆);
            this.Font = new System.Drawing.Font("SimSun-ExtB", 11.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "UserControlDr";
            this.Size = new System.Drawing.Size(640, 418);
            this.groupBox登陆.ResumeLayout(false);
            this.groupBox登陆.PerformLayout();
            this.groupBox信息.ResumeLayout(false);
            this.groupBox信息.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUsrName;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxUsrPas;
        private System.Windows.Forms.GroupBox groupBox登陆;
        private System.Windows.Forms.Button buttonIPv6;
        private System.Windows.Forms.Button buttonIPv4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox信息;
        private System.Windows.Forms.Label label3;
    }
}
