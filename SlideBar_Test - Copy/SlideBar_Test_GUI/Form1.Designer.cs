namespace SlideBar_Test_GUI
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
            this.labelbAction = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelbPosition = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelbCurSpeed = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.buttonLED_OFF = new System.Windows.Forms.Button();
            this.buttonLED_ON = new System.Windows.Forms.Button();
            this.buttonDISCONNECT = new System.Windows.Forms.Button();
            this.buttonCONNECT = new System.Windows.Forms.Button();
            this.buttonBREATH_ON = new System.Windows.Forms.Button();
            this.buttonBREATH_OFF = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.checkBoxMethod = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.buttonSet8And68 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "bAction: ";
            // 
            // labelbAction
            // 
            this.labelbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelbAction.AutoSize = true;
            this.labelbAction.Location = new System.Drawing.Point(205, 0);
            this.labelbAction.Name = "labelbAction";
            this.labelbAction.Size = new System.Drawing.Size(21, 24);
            this.labelbAction.TabIndex = 1;
            this.labelbAction.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "bPosition";
            // 
            // labelbPosition
            // 
            this.labelbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelbPosition.AutoSize = true;
            this.labelbPosition.Location = new System.Drawing.Point(205, 24);
            this.labelbPosition.Name = "labelbPosition";
            this.labelbPosition.Size = new System.Drawing.Size(21, 24);
            this.labelbPosition.TabIndex = 3;
            this.labelbPosition.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "bCurSpeed";
            // 
            // labelbCurSpeed
            // 
            this.labelbCurSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelbCurSpeed.AutoSize = true;
            this.labelbCurSpeed.Location = new System.Drawing.Point(205, 48);
            this.labelbCurSpeed.Name = "labelbCurSpeed";
            this.labelbCurSpeed.Size = new System.Drawing.Size(21, 24);
            this.labelbCurSpeed.TabIndex = 1;
            this.labelbCurSpeed.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelbAction);
            this.panel1.Controls.Add(this.labelbCurSpeed);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labelbPosition);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 78);
            this.panel1.TabIndex = 1;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Controls.Add(this.buttonLED_OFF);
            this.groupBoxControl.Controls.Add(this.buttonLED_ON);
            this.groupBoxControl.Controls.Add(this.buttonDISCONNECT);
            this.groupBoxControl.Controls.Add(this.buttonCONNECT);
            this.groupBoxControl.Controls.Add(this.buttonBREATH_ON);
            this.groupBoxControl.Controls.Add(this.buttonBREATH_OFF);
            this.groupBoxControl.Location = new System.Drawing.Point(12, 96);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(247, 113);
            this.groupBoxControl.TabIndex = 2;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "Control Box";
            // 
            // buttonLED_OFF
            // 
            this.buttonLED_OFF.Location = new System.Drawing.Point(125, 77);
            this.buttonLED_OFF.Name = "buttonLED_OFF";
            this.buttonLED_OFF.Size = new System.Drawing.Size(112, 23);
            this.buttonLED_OFF.TabIndex = 5;
            this.buttonLED_OFF.Text = "LED_ON";
            this.buttonLED_OFF.UseVisualStyleBackColor = true;
            this.buttonLED_OFF.Click += new System.EventHandler(this.buttonLED_On_Click);
            // 
            // buttonLED_ON
            // 
            this.buttonLED_ON.Location = new System.Drawing.Point(7, 77);
            this.buttonLED_ON.Name = "buttonLED_ON";
            this.buttonLED_ON.Size = new System.Drawing.Size(112, 23);
            this.buttonLED_ON.TabIndex = 4;
            this.buttonLED_ON.Text = "LED_OFF";
            this.buttonLED_ON.UseVisualStyleBackColor = true;
            this.buttonLED_ON.Click += new System.EventHandler(this.buttonLED_Off_Click);
            // 
            // buttonDISCONNECT
            // 
            this.buttonDISCONNECT.Location = new System.Drawing.Point(125, 48);
            this.buttonDISCONNECT.Name = "buttonDISCONNECT";
            this.buttonDISCONNECT.Size = new System.Drawing.Size(112, 23);
            this.buttonDISCONNECT.TabIndex = 3;
            this.buttonDISCONNECT.Text = "DISCONNECT";
            this.buttonDISCONNECT.UseVisualStyleBackColor = true;
            this.buttonDISCONNECT.Click += new System.EventHandler(this.buttonDISCONNECT_Click);
            // 
            // buttonCONNECT
            // 
            this.buttonCONNECT.Location = new System.Drawing.Point(7, 48);
            this.buttonCONNECT.Name = "buttonCONNECT";
            this.buttonCONNECT.Size = new System.Drawing.Size(112, 23);
            this.buttonCONNECT.TabIndex = 2;
            this.buttonCONNECT.Text = "CONNECT";
            this.buttonCONNECT.UseVisualStyleBackColor = true;
            this.buttonCONNECT.Click += new System.EventHandler(this.buttonCONNECT_Click);
            // 
            // buttonBREATH_ON
            // 
            this.buttonBREATH_ON.Location = new System.Drawing.Point(125, 19);
            this.buttonBREATH_ON.Name = "buttonBREATH_ON";
            this.buttonBREATH_ON.Size = new System.Drawing.Size(112, 23);
            this.buttonBREATH_ON.TabIndex = 1;
            this.buttonBREATH_ON.Text = "BREATH_ON";
            this.buttonBREATH_ON.UseVisualStyleBackColor = true;
            this.buttonBREATH_ON.Click += new System.EventHandler(this.buttonBREATH_ON_Click);
            // 
            // buttonBREATH_OFF
            // 
            this.buttonBREATH_OFF.Location = new System.Drawing.Point(7, 19);
            this.buttonBREATH_OFF.Name = "buttonBREATH_OFF";
            this.buttonBREATH_OFF.Size = new System.Drawing.Size(112, 23);
            this.buttonBREATH_OFF.TabIndex = 0;
            this.buttonBREATH_OFF.Text = "BREATH_OFF";
            this.buttonBREATH_OFF.UseVisualStyleBackColor = true;
            this.buttonBREATH_OFF.Click += new System.EventHandler(this.buttonBREATH_OFF_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInfo.Location = new System.Drawing.Point(188, 244);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonInfo.TabIndex = 4;
            this.buttonInfo.Text = "About";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // checkBoxMethod
            // 
            this.checkBoxMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxMethod.AutoSize = true;
            this.checkBoxMethod.Location = new System.Drawing.Point(12, 248);
            this.checkBoxMethod.Name = "checkBoxMethod";
            this.checkBoxMethod.Size = new System.Drawing.Size(138, 17);
            this.checkBoxMethod.TabIndex = 5;
            this.checkBoxMethod.Text = "Use SBarHook function";
            this.checkBoxMethod.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Location = new System.Drawing.Point(73, 222);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(13, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "array[8] = ";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "array[68] = ";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown2.Location = new System.Drawing.Point(215, 222);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown2.TabIndex = 6;
            this.numericUpDown2.Value = new decimal(new int[] {
            84,
            0,
            0,
            0});
            // 
            // buttonSet8And68
            // 
            this.buttonSet8And68.Location = new System.Drawing.Point(124, 219);
            this.buttonSet8And68.Name = "buttonSet8And68";
            this.buttonSet8And68.Size = new System.Drawing.Size(19, 23);
            this.buttonSet8And68.TabIndex = 9;
            this.buttonSet8And68.Text = "!";
            this.buttonSet8And68.UseVisualStyleBackColor = true;
            this.buttonSet8And68.Click += new System.EventHandler(this.buttonSet8And68_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 279);
            this.Controls.Add(this.buttonSet8And68);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBoxMethod);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.groupBoxControl);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelbAction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelbPosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelbCurSpeed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.Button buttonBREATH_OFF;
        private System.Windows.Forms.Button buttonBREATH_ON;
        private System.Windows.Forms.Button buttonCONNECT;
        private System.Windows.Forms.Button buttonDISCONNECT;
        private System.Windows.Forms.Button buttonLED_ON;
        private System.Windows.Forms.Button buttonLED_OFF;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.CheckBox checkBoxMethod;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button buttonSet8And68;

    }
}

