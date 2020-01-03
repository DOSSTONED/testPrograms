namespace TCP查看.Net4
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnLocalEndPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRemoteEndPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnLocalEndPoint,
            this.ColumnRemoteEndPoint,
            this.ColumnState});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(655, 238);
            this.dataGridView1.TabIndex = 1;
            // 
            // ColumnLocalEndPoint
            // 
            this.ColumnLocalEndPoint.Frozen = true;
            this.ColumnLocalEndPoint.HeaderText = "LocalEndPoint";
            this.ColumnLocalEndPoint.Name = "ColumnLocalEndPoint";
            this.ColumnLocalEndPoint.ReadOnly = true;
            this.ColumnLocalEndPoint.Width = 256;
            // 
            // ColumnRemoteEndPoint
            // 
            this.ColumnRemoteEndPoint.Frozen = true;
            this.ColumnRemoteEndPoint.HeaderText = "RemoteEndPoint";
            this.ColumnRemoteEndPoint.Name = "ColumnRemoteEndPoint";
            this.ColumnRemoteEndPoint.ReadOnly = true;
            this.ColumnRemoteEndPoint.Width = 256;
            // 
            // ColumnState
            // 
            this.ColumnState.Frozen = true;
            this.ColumnState.HeaderText = "State";
            this.ColumnState.Name = "ColumnState";
            this.ColumnState.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 262);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLocalEndPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRemoteEndPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnState;
    }
}

