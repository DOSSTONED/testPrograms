namespace 遗传算法解决TSP
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
            this.panelDrawing = new System.Windows.Forms.Panel();
            this.buttonGivePoints = new System.Windows.Forms.Button();
            this.buttonCal = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labelRepeatTimes = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelDrawing
            // 
            this.panelDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDrawing.Location = new System.Drawing.Point(12, 24);
            this.panelDrawing.Name = "panelDrawing";
            this.panelDrawing.Size = new System.Drawing.Size(720, 720);
            this.panelDrawing.TabIndex = 0;
            // 
            // buttonGivePoints
            // 
            this.buttonGivePoints.Location = new System.Drawing.Point(738, 24);
            this.buttonGivePoints.Name = "buttonGivePoints";
            this.buttonGivePoints.Size = new System.Drawing.Size(75, 23);
            this.buttonGivePoints.TabIndex = 1;
            this.buttonGivePoints.Text = "Give Points";
            this.buttonGivePoints.UseVisualStyleBackColor = true;
            this.buttonGivePoints.Click += new System.EventHandler(this.buttonGivePoints_Click);
            // 
            // buttonCal
            // 
            this.buttonCal.Location = new System.Drawing.Point(738, 53);
            this.buttonCal.Name = "buttonCal";
            this.buttonCal.Size = new System.Drawing.Size(75, 23);
            this.buttonCal.TabIndex = 2;
            this.buttonCal.Text = "Calculate";
            this.buttonCal.UseVisualStyleBackColor = true;
            this.buttonCal.Click += new System.EventHandler(this.buttonCal_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(819, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(387, 654);
            this.listBox1.TabIndex = 3;
            // 
            // labelRepeatTimes
            // 
            this.labelRepeatTimes.AutoSize = true;
            this.labelRepeatTimes.Location = new System.Drawing.Point(738, 722);
            this.labelRepeatTimes.Name = "labelRepeatTimes";
            this.labelRepeatTimes.Size = new System.Drawing.Size(54, 13);
            this.labelRepeatTimes.TabIndex = 4;
            this.labelRepeatTimes.Text = "Repeated";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(869, 722);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(30, 13);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "Time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 768);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelRepeatTimes);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonCal);
            this.Controls.Add(this.buttonGivePoints);
            this.Controls.Add(this.panelDrawing);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelDrawing;
        private System.Windows.Forms.Button buttonGivePoints;
        private System.Windows.Forms.Button buttonCal;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label labelRepeatTimes;
        private System.Windows.Forms.Label labelTime;
    }
}

