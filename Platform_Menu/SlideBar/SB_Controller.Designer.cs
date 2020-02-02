namespace SlideBar_Controller
{
    partial class SB_Controller
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
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.buttonResetColors = new System.Windows.Forms.Button();
            this.labelbnLightEffect = new System.Windows.Forms.Label();
            this.labelbLightEffect = new System.Windows.Forms.Label();
            this.labelbnBreathEffect = new System.Windows.Forms.Label();
            this.labelbBreathEffect = new System.Windows.Forms.Label();
            this.labelbnAction = new System.Windows.Forms.Label();
            this.labelbEnabled = new System.Windows.Forms.Label();
            this.labelbnPosition = new System.Windows.Forms.Label();
            this.labelbEvent = new System.Windows.Forms.Label();
            this.labelbnEnabled = new System.Windows.Forms.Label();
            this.labelbSpeed = new System.Windows.Forms.Label();
            this.labelbnSpeed = new System.Windows.Forms.Label();
            this.labelbPosition = new System.Windows.Forms.Label();
            this.labelbnEvent = new System.Windows.Forms.Label();
            this.labelbAction = new System.Windows.Forms.Label();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelControls = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelIOBytes = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxIOBytes = new System.Windows.Forms.GroupBox();
            this.groupBoxEvtBytes = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelEventBytes = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRefreshIOBytes = new System.Windows.Forms.Button();
            this.groupBoxInputBytes = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelInputBytes = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxControls.SuspendLayout();
            this.groupBoxIOBytes.SuspendLayout();
            this.groupBoxEvtBytes.SuspendLayout();
            this.groupBoxInputBytes.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.buttonRefreshIOBytes);
            this.groupBoxInfo.Controls.Add(this.buttonResetColors);
            this.groupBoxInfo.Controls.Add(this.labelbnLightEffect);
            this.groupBoxInfo.Controls.Add(this.labelbLightEffect);
            this.groupBoxInfo.Controls.Add(this.labelbnBreathEffect);
            this.groupBoxInfo.Controls.Add(this.labelbBreathEffect);
            this.groupBoxInfo.Controls.Add(this.labelbnAction);
            this.groupBoxInfo.Controls.Add(this.labelbEnabled);
            this.groupBoxInfo.Controls.Add(this.labelbnPosition);
            this.groupBoxInfo.Controls.Add(this.labelbEvent);
            this.groupBoxInfo.Controls.Add(this.labelbnEnabled);
            this.groupBoxInfo.Controls.Add(this.labelbSpeed);
            this.groupBoxInfo.Controls.Add(this.labelbnSpeed);
            this.groupBoxInfo.Controls.Add(this.labelbPosition);
            this.groupBoxInfo.Controls.Add(this.labelbnEvent);
            this.groupBoxInfo.Controls.Add(this.labelbAction);
            this.groupBoxInfo.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxInfo.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(284, 273);
            this.groupBoxInfo.TabIndex = 1;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Slidebar info";
            // 
            // buttonResetColors
            // 
            this.buttonResetColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetColors.Location = new System.Drawing.Point(6, 230);
            this.buttonResetColors.Name = "buttonResetColors";
            this.buttonResetColors.Size = new System.Drawing.Size(164, 37);
            this.buttonResetColors.TabIndex = 1;
            this.buttonResetColors.Text = "Reset to Balck";
            this.buttonResetColors.UseVisualStyleBackColor = true;
            this.buttonResetColors.Click += new System.EventHandler(this.buttonResetColors_Click);
            // 
            // labelbnLightEffect
            // 
            this.labelbnLightEffect.AutoSize = true;
            this.labelbnLightEffect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(0)))), ((int)(((byte)(224)))));
            this.labelbnLightEffect.Location = new System.Drawing.Point(152, 199);
            this.labelbnLightEffect.Name = "labelbnLightEffect";
            this.labelbnLightEffect.Size = new System.Drawing.Size(24, 28);
            this.labelbnLightEffect.TabIndex = 0;
            this.labelbnLightEffect.Text = "0";
            // 
            // labelbLightEffect
            // 
            this.labelbLightEffect.AutoSize = true;
            this.labelbLightEffect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(0)))), ((int)(((byte)(224)))));
            this.labelbLightEffect.Location = new System.Drawing.Point(6, 199);
            this.labelbLightEffect.Name = "labelbLightEffect";
            this.labelbLightEffect.Size = new System.Drawing.Size(125, 28);
            this.labelbLightEffect.TabIndex = 0;
            this.labelbLightEffect.Text = "LightEffect:";
            // 
            // labelbnBreathEffect
            // 
            this.labelbnBreathEffect.AutoSize = true;
            this.labelbnBreathEffect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(224)))));
            this.labelbnBreathEffect.Location = new System.Drawing.Point(152, 171);
            this.labelbnBreathEffect.Name = "labelbnBreathEffect";
            this.labelbnBreathEffect.Size = new System.Drawing.Size(24, 28);
            this.labelbnBreathEffect.TabIndex = 0;
            this.labelbnBreathEffect.Text = "0";
            // 
            // labelbBreathEffect
            // 
            this.labelbBreathEffect.AutoSize = true;
            this.labelbBreathEffect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(224)))));
            this.labelbBreathEffect.Location = new System.Drawing.Point(6, 171);
            this.labelbBreathEffect.Name = "labelbBreathEffect";
            this.labelbBreathEffect.Size = new System.Drawing.Size(140, 28);
            this.labelbBreathEffect.TabIndex = 0;
            this.labelbBreathEffect.Text = "BreathEffect:";
            // 
            // labelbnAction
            // 
            this.labelbnAction.AutoSize = true;
            this.labelbnAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelbnAction.Location = new System.Drawing.Point(152, 31);
            this.labelbnAction.Name = "labelbnAction";
            this.labelbnAction.Size = new System.Drawing.Size(50, 28);
            this.labelbnAction.TabIndex = 0;
            this.labelbnAction.Text = "null";
            // 
            // labelbEnabled
            // 
            this.labelbEnabled.AutoSize = true;
            this.labelbEnabled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelbEnabled.Location = new System.Drawing.Point(6, 143);
            this.labelbEnabled.Name = "labelbEnabled";
            this.labelbEnabled.Size = new System.Drawing.Size(98, 28);
            this.labelbEnabled.TabIndex = 0;
            this.labelbEnabled.Text = "Enabled:";
            // 
            // labelbnPosition
            // 
            this.labelbnPosition.AutoSize = true;
            this.labelbnPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelbnPosition.Location = new System.Drawing.Point(152, 59);
            this.labelbnPosition.Name = "labelbnPosition";
            this.labelbnPosition.Size = new System.Drawing.Size(24, 28);
            this.labelbnPosition.TabIndex = 0;
            this.labelbnPosition.Text = "0";
            // 
            // labelbEvent
            // 
            this.labelbEvent.AutoSize = true;
            this.labelbEvent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(224)))), ((int)(((byte)(0)))));
            this.labelbEvent.Location = new System.Drawing.Point(6, 115);
            this.labelbEvent.Name = "labelbEvent";
            this.labelbEvent.Size = new System.Drawing.Size(73, 28);
            this.labelbEvent.TabIndex = 0;
            this.labelbEvent.Text = "Event:";
            // 
            // labelbnEnabled
            // 
            this.labelbnEnabled.AutoSize = true;
            this.labelbnEnabled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelbnEnabled.Location = new System.Drawing.Point(152, 143);
            this.labelbnEnabled.Name = "labelbnEnabled";
            this.labelbnEnabled.Size = new System.Drawing.Size(24, 28);
            this.labelbnEnabled.TabIndex = 0;
            this.labelbnEnabled.Text = "0";
            // 
            // labelbSpeed
            // 
            this.labelbSpeed.AutoSize = true;
            this.labelbSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(0)))));
            this.labelbSpeed.Location = new System.Drawing.Point(6, 87);
            this.labelbSpeed.Name = "labelbSpeed";
            this.labelbSpeed.Size = new System.Drawing.Size(79, 28);
            this.labelbSpeed.TabIndex = 0;
            this.labelbSpeed.Text = "Speed:";
            // 
            // labelbnSpeed
            // 
            this.labelbnSpeed.AutoSize = true;
            this.labelbnSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(0)))));
            this.labelbnSpeed.Location = new System.Drawing.Point(152, 87);
            this.labelbnSpeed.Name = "labelbnSpeed";
            this.labelbnSpeed.Size = new System.Drawing.Size(24, 28);
            this.labelbnSpeed.TabIndex = 0;
            this.labelbnSpeed.Text = "0";
            // 
            // labelbPosition
            // 
            this.labelbPosition.AutoSize = true;
            this.labelbPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labelbPosition.Location = new System.Drawing.Point(6, 59);
            this.labelbPosition.Name = "labelbPosition";
            this.labelbPosition.Size = new System.Drawing.Size(106, 28);
            this.labelbPosition.TabIndex = 0;
            this.labelbPosition.Text = "Posotion:";
            // 
            // labelbnEvent
            // 
            this.labelbnEvent.AutoSize = true;
            this.labelbnEvent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(224)))), ((int)(((byte)(0)))));
            this.labelbnEvent.Location = new System.Drawing.Point(152, 115);
            this.labelbnEvent.Name = "labelbnEvent";
            this.labelbnEvent.Size = new System.Drawing.Size(50, 28);
            this.labelbnEvent.TabIndex = 0;
            this.labelbnEvent.Text = "null";
            // 
            // labelbAction
            // 
            this.labelbAction.AutoSize = true;
            this.labelbAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelbAction.Location = new System.Drawing.Point(6, 31);
            this.labelbAction.Name = "labelbAction";
            this.labelbAction.Size = new System.Drawing.Size(83, 28);
            this.labelbAction.TabIndex = 0;
            this.labelbAction.Text = "Action:";
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxControls.Controls.Add(this.flowLayoutPanelControls);
            this.groupBoxControls.Location = new System.Drawing.Point(12, 291);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(284, 322);
            this.groupBoxControls.TabIndex = 2;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Slidebar Event";
            // 
            // flowLayoutPanelControls
            // 
            this.flowLayoutPanelControls.AutoScroll = true;
            this.flowLayoutPanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelControls.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelControls.Name = "flowLayoutPanelControls";
            this.flowLayoutPanelControls.Size = new System.Drawing.Size(278, 303);
            this.flowLayoutPanelControls.TabIndex = 0;
            // 
            // flowLayoutPanelIOBytes
            // 
            this.flowLayoutPanelIOBytes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelIOBytes.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelIOBytes.Name = "flowLayoutPanelIOBytes";
            this.flowLayoutPanelIOBytes.Size = new System.Drawing.Size(216, 362);
            this.flowLayoutPanelIOBytes.TabIndex = 3;
            // 
            // groupBoxIOBytes
            // 
            this.groupBoxIOBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxIOBytes.Controls.Add(this.flowLayoutPanelIOBytes);
            this.groupBoxIOBytes.Location = new System.Drawing.Point(530, 12);
            this.groupBoxIOBytes.Name = "groupBoxIOBytes";
            this.groupBoxIOBytes.Size = new System.Drawing.Size(222, 381);
            this.groupBoxIOBytes.TabIndex = 4;
            this.groupBoxIOBytes.TabStop = false;
            this.groupBoxIOBytes.Text = "IOBytes[128]";
            // 
            // groupBoxEvtBytes
            // 
            this.groupBoxEvtBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEvtBytes.Controls.Add(this.flowLayoutPanelEventBytes);
            this.groupBoxEvtBytes.Location = new System.Drawing.Point(530, 399);
            this.groupBoxEvtBytes.Name = "groupBoxEvtBytes";
            this.groupBoxEvtBytes.Size = new System.Drawing.Size(222, 211);
            this.groupBoxEvtBytes.TabIndex = 6;
            this.groupBoxEvtBytes.TabStop = false;
            this.groupBoxEvtBytes.Text = "Event Bytes [64]";
            // 
            // flowLayoutPanelEventBytes
            // 
            this.flowLayoutPanelEventBytes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelEventBytes.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelEventBytes.Name = "flowLayoutPanelEventBytes";
            this.flowLayoutPanelEventBytes.Size = new System.Drawing.Size(216, 192);
            this.flowLayoutPanelEventBytes.TabIndex = 0;
            // 
            // buttonRefreshIOBytes
            // 
            this.buttonRefreshIOBytes.Location = new System.Drawing.Point(176, 230);
            this.buttonRefreshIOBytes.Name = "buttonRefreshIOBytes";
            this.buttonRefreshIOBytes.Size = new System.Drawing.Size(102, 37);
            this.buttonRefreshIOBytes.TabIndex = 2;
            this.buttonRefreshIOBytes.Text = "IOBytes";
            this.buttonRefreshIOBytes.UseVisualStyleBackColor = true;
            this.buttonRefreshIOBytes.Click += new System.EventHandler(this.buttonRefreshIOBytes_Click);
            // 
            // groupBoxInputBytes
            // 
            this.groupBoxInputBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInputBytes.Controls.Add(this.flowLayoutPanelInputBytes);
            this.groupBoxInputBytes.Location = new System.Drawing.Point(302, 12);
            this.groupBoxInputBytes.Name = "groupBoxInputBytes";
            this.groupBoxInputBytes.Size = new System.Drawing.Size(222, 598);
            this.groupBoxInputBytes.TabIndex = 5;
            this.groupBoxInputBytes.TabStop = false;
            this.groupBoxInputBytes.Text = "InputBytes[128]";
            // 
            // flowLayoutPanelInputBytes
            // 
            this.flowLayoutPanelInputBytes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelInputBytes.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelInputBytes.Name = "flowLayoutPanelInputBytes";
            this.flowLayoutPanelInputBytes.Size = new System.Drawing.Size(216, 579);
            this.flowLayoutPanelInputBytes.TabIndex = 3;
            // 
            // SB_Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 625);
            this.Controls.Add(this.groupBoxInputBytes);
            this.Controls.Add(this.groupBoxEvtBytes);
            this.Controls.Add(this.groupBoxIOBytes);
            this.Controls.Add(this.groupBoxControls);
            this.Controls.Add(this.groupBoxInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SB_Controller";
            this.Text = "SlideBar config by DOSSTONED";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SB_Controller_FormClosing);
            this.Load += new System.EventHandler(this.SB_Controller_Load);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxIOBytes.ResumeLayout(false);
            this.groupBoxEvtBytes.ResumeLayout(false);
            this.groupBoxInputBytes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label labelbAction;
        private System.Windows.Forms.Label labelbPosition;
        private System.Windows.Forms.Label labelbEvent;
        private System.Windows.Forms.Label labelbSpeed;
        private System.Windows.Forms.Label labelbEnabled;
        private System.Windows.Forms.Label labelbLightEffect;
        private System.Windows.Forms.Label labelbBreathEffect;
        private System.Windows.Forms.Label labelbnLightEffect;
        private System.Windows.Forms.Label labelbnBreathEffect;
        private System.Windows.Forms.Label labelbnEnabled;
        private System.Windows.Forms.Label labelbnEvent;
        private System.Windows.Forms.Label labelbnSpeed;
        private System.Windows.Forms.Label labelbnPosition;
        private System.Windows.Forms.Label labelbnAction;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelControls;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIOBytes;
        private System.Windows.Forms.GroupBox groupBoxIOBytes;
        private System.Windows.Forms.GroupBox groupBoxEvtBytes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelEventBytes;
        private System.Windows.Forms.Button buttonResetColors;
        private System.Windows.Forms.Button buttonRefreshIOBytes;
        private System.Windows.Forms.GroupBox groupBoxInputBytes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelInputBytes;
    }
}

