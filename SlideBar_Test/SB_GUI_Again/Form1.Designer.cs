namespace SB_GUI_Again
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.buttonLED_OFF = new System.Windows.Forms.Button();
            this.buttonLED_ON = new System.Windows.Forms.Button();
            this.buttonDISCONNECT = new System.Windows.Forms.Button();
            this.buttonCONNECT = new System.Windows.Forms.Button();
            this.buttonBREATH_ON = new System.Windows.Forms.Button();
            this.buttonBREATH_OFF = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxControl.Controls.Add(this.buttonLED_OFF);
            this.groupBoxControl.Controls.Add(this.buttonLED_ON);
            this.groupBoxControl.Controls.Add(this.buttonDISCONNECT);
            this.groupBoxControl.Controls.Add(this.buttonCONNECT);
            this.groupBoxControl.Controls.Add(this.buttonBREATH_ON);
            this.groupBoxControl.Controls.Add(this.buttonBREATH_OFF);
            this.groupBoxControl.Location = new System.Drawing.Point(12, 12);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Size = new System.Drawing.Size(260, 113);
            this.groupBoxControl.TabIndex = 4;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "Control Box";
            // 
            // buttonLED_OFF
            // 
            this.buttonLED_OFF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLED_OFF.Location = new System.Drawing.Point(142, 77);
            this.buttonLED_OFF.Name = "buttonLED_OFF";
            this.buttonLED_OFF.Size = new System.Drawing.Size(112, 23);
            this.buttonLED_OFF.TabIndex = 5;
            this.buttonLED_OFF.Text = "LED_ON";
            this.buttonLED_OFF.UseVisualStyleBackColor = true;
            this.buttonLED_OFF.Click += new System.EventHandler(this.buttonLED_OFF_Click);
            // 
            // buttonLED_ON
            // 
            this.buttonLED_ON.Location = new System.Drawing.Point(7, 77);
            this.buttonLED_ON.Name = "buttonLED_ON";
            this.buttonLED_ON.Size = new System.Drawing.Size(112, 23);
            this.buttonLED_ON.TabIndex = 4;
            this.buttonLED_ON.Text = "LED_OFF";
            this.buttonLED_ON.UseVisualStyleBackColor = true;
            this.buttonLED_ON.Click += new System.EventHandler(this.buttonLED_ON_Click);
            // 
            // buttonDISCONNECT
            // 
            this.buttonDISCONNECT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDISCONNECT.Location = new System.Drawing.Point(142, 48);
            this.buttonDISCONNECT.Name = "buttonDISCONNECT";
            this.buttonDISCONNECT.Size = new System.Drawing.Size(112, 23);
            this.buttonDISCONNECT.TabIndex = 3;
            this.buttonDISCONNECT.Text = "CONNECT";
            this.buttonDISCONNECT.UseVisualStyleBackColor = true;
            this.buttonDISCONNECT.Click += new System.EventHandler(this.buttonDISCONNECT_Click);
            // 
            // buttonCONNECT
            // 
            this.buttonCONNECT.Location = new System.Drawing.Point(7, 48);
            this.buttonCONNECT.Name = "buttonCONNECT";
            this.buttonCONNECT.Size = new System.Drawing.Size(112, 23);
            this.buttonCONNECT.TabIndex = 2;
            this.buttonCONNECT.Text = "DISCONNECT";
            this.buttonCONNECT.UseVisualStyleBackColor = true;
            this.buttonCONNECT.Click += new System.EventHandler(this.buttonCONNECT_Click);
            // 
            // buttonBREATH_ON
            // 
            this.buttonBREATH_ON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBREATH_ON.Location = new System.Drawing.Point(142, 19);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(91, 190);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(175, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "SetSlideBarStatusREWRITE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBoxControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.Button buttonLED_OFF;
        private System.Windows.Forms.Button buttonLED_ON;
        private System.Windows.Forms.Button buttonDISCONNECT;
        private System.Windows.Forms.Button buttonCONNECT;
        private System.Windows.Forms.Button buttonBREATH_ON;
        private System.Windows.Forms.Button buttonBREATH_OFF;
        private System.Windows.Forms.Button button2;
    }
}

