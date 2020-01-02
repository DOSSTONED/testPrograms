namespace PortProxy_46
{
    partial class FormMain
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
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.labelPort = new System.Windows.Forms.Label();
            this.buttonStartProxy = new System.Windows.Forms.Button();
            this.groupBoxProxies = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.groupBoxProxies.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPort.Location = new System.Drawing.Point(133, 19);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.numericUpDownPort.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownPort.TabIndex = 0;
            this.numericUpDownPort.Value = new decimal(new int[] {
            12345,
            0,
            0,
            0});
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(6, 21);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(101, 13);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "HTTP Port Number:";
            // 
            // buttonStartProxy
            // 
            this.buttonStartProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartProxy.Location = new System.Drawing.Point(142, 132);
            this.buttonStartProxy.Name = "buttonStartProxy";
            this.buttonStartProxy.Size = new System.Drawing.Size(75, 23);
            this.buttonStartProxy.TabIndex = 2;
            this.buttonStartProxy.Text = "Start Proxy";
            this.buttonStartProxy.UseVisualStyleBackColor = true;
            this.buttonStartProxy.Click += new System.EventHandler(this.buttonStartProxy_Click);
            // 
            // groupBoxProxies
            // 
            this.groupBoxProxies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProxies.Controls.Add(this.labelPort);
            this.groupBoxProxies.Controls.Add(this.numericUpDownPort);
            this.groupBoxProxies.Location = new System.Drawing.Point(12, 12);
            this.groupBoxProxies.Name = "groupBoxProxies";
            this.groupBoxProxies.Size = new System.Drawing.Size(205, 110);
            this.groupBoxProxies.TabIndex = 3;
            this.groupBoxProxies.TabStop = false;
            this.groupBoxProxies.Text = "Current available proxies:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 167);
            this.Controls.Add(this.groupBoxProxies);
            this.Controls.Add(this.buttonStartProxy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.Text = "PortProxy";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.groupBoxProxies.ResumeLayout(false);
            this.groupBoxProxies.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Button buttonStartProxy;
        private System.Windows.Forms.GroupBox groupBoxProxies;
    }
}

