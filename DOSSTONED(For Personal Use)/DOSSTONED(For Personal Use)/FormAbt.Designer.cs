namespace DOSSTONED_For_Personal_Use_
{
    partial class FormAbt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbt));
            this.BtnFrmAbtOK = new System.Windows.Forms.Button();
            this.LabelFrmAbt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnFrmAbtOK
            // 
            this.BtnFrmAbtOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFrmAbtOK.Location = new System.Drawing.Point(103, 189);
            this.BtnFrmAbtOK.Name = "BtnFrmAbtOK";
            this.BtnFrmAbtOK.Size = new System.Drawing.Size(136, 23);
            this.BtnFrmAbtOK.TabIndex = 0;
            this.BtnFrmAbtOK.Text = "OK";
            this.BtnFrmAbtOK.UseVisualStyleBackColor = true;
            this.BtnFrmAbtOK.Click += new System.EventHandler(this.BtnFrmAbtOK_Click);
            // 
            // LabelFrmAbt
            // 
            this.LabelFrmAbt.AutoSize = true;
            this.LabelFrmAbt.BackColor = System.Drawing.Color.Transparent;
            this.LabelFrmAbt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelFrmAbt.Location = new System.Drawing.Point(7, 7);
            this.LabelFrmAbt.Name = "LabelFrmAbt";
            this.LabelFrmAbt.Size = new System.Drawing.Size(319, 176);
            this.LabelFrmAbt.TabIndex = 1;
            this.LabelFrmAbt.Text = "CamVerify / Password\r\nProgram Certificated / Program Starter\r\nSetup Menu\r\nWeb Bro" +
                "wser (FileDownloader / Favorite Link / RSS)\r\n每日课表\r\n\r\n\r\n\r\n\r\nNotify(Finished)\r\nFla" +
                "shdisk(Finished) / Sys Protect";
            this.LabelFrmAbt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormAbt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(334, 224);
            this.ControlBox = false;
            this.Controls.Add(this.LabelFrmAbt);
            this.Controls.Add(this.BtnFrmAbtOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About DOSSTONED";
            this.Load += new System.EventHandler(this.FormAbt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnFrmAbtOK;
        private System.Windows.Forms.Label LabelFrmAbt;
    }
}