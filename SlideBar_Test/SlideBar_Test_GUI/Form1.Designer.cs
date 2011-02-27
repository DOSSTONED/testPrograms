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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBoxControl.SuspendLayout();
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
            this.labelbAction.Location = new System.Drawing.Point(223, 0);
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
            this.labelbPosition.Location = new System.Drawing.Point(223, 24);
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
            this.labelbCurSpeed.Location = new System.Drawing.Point(223, 48);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxControl);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxControl.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button button1;

    }
}

