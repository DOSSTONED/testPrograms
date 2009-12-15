namespace NK_刷课
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonShua = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.xkxh1 = new System.Windows.Forms.TextBox();
            this.buttonTui = new System.Windows.Forms.Button();
            this.buttonNavigate = new System.Windows.Forms.Button();
            this.groupBoxxkxh = new System.Windows.Forms.GroupBox();
            this.xkxh4 = new System.Windows.Forms.TextBox();
            this.xkxh3 = new System.Windows.Forms.TextBox();
            this.xkxh2 = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.labelUserpassword = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUserpassword = new System.Windows.Forms.TextBox();
            this.buttonTimer1 = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonxksc = new System.Windows.Forms.Button();
            this.groupBoxxkxh.SuspendLayout();
            this.groupBoxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(12, 48);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(738, 314);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://192.168.4.15", System.UriKind.Absolute);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(12, 13);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(601, 22);
            this.textBoxURL.TabIndex = 1;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogin.Location = new System.Drawing.Point(186, 29);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(65, 33);
            this.buttonLogin.TabIndex = 6;
            this.buttonLogin.Text = "登录";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 16;
            this.listBoxLog.Location = new System.Drawing.Point(407, 368);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(343, 148);
            this.listBoxLog.TabIndex = 14;
            // 
            // buttonShua
            // 
            this.buttonShua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShua.Location = new System.Drawing.Point(186, 18);
            this.buttonShua.Name = "buttonShua";
            this.buttonShua.Size = new System.Drawing.Size(80, 28);
            this.buttonShua.TabIndex = 11;
            this.buttonShua.Text = "选课";
            this.buttonShua.UseVisualStyleBackColor = true;
            this.buttonShua.Click += new System.EventHandler(this.buttonShua_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 25000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // xkxh1
            // 
            this.xkxh1.Location = new System.Drawing.Point(6, 21);
            this.xkxh1.MaxLength = 4;
            this.xkxh1.Name = "xkxh1";
            this.xkxh1.Size = new System.Drawing.Size(38, 22);
            this.xkxh1.TabIndex = 7;
            this.xkxh1.WordWrap = false;
            // 
            // buttonTui
            // 
            this.buttonTui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTui.Location = new System.Drawing.Point(272, 18);
            this.buttonTui.Name = "buttonTui";
            this.buttonTui.Size = new System.Drawing.Size(80, 28);
            this.buttonTui.TabIndex = 12;
            this.buttonTui.Text = "退课";
            this.buttonTui.UseVisualStyleBackColor = true;
            this.buttonTui.Click += new System.EventHandler(this.buttonTui_Click);
            // 
            // buttonNavigate
            // 
            this.buttonNavigate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNavigate.Location = new System.Drawing.Point(619, 7);
            this.buttonNavigate.Name = "buttonNavigate";
            this.buttonNavigate.Size = new System.Drawing.Size(74, 35);
            this.buttonNavigate.TabIndex = 2;
            this.buttonNavigate.Text = "Go! ->";
            this.buttonNavigate.UseVisualStyleBackColor = true;
            this.buttonNavigate.Click += new System.EventHandler(this.buttonNavigate_Click);
            // 
            // groupBoxxkxh
            // 
            this.groupBoxxkxh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxxkxh.Controls.Add(this.xkxh4);
            this.groupBoxxkxh.Controls.Add(this.xkxh3);
            this.groupBoxxkxh.Controls.Add(this.xkxh2);
            this.groupBoxxkxh.Controls.Add(this.xkxh1);
            this.groupBoxxkxh.Controls.Add(this.buttonTui);
            this.groupBoxxkxh.Controls.Add(this.buttonShua);
            this.groupBoxxkxh.Location = new System.Drawing.Point(12, 462);
            this.groupBoxxkxh.Name = "groupBoxxkxh";
            this.groupBoxxkxh.Size = new System.Drawing.Size(362, 54);
            this.groupBoxxkxh.TabIndex = 7;
            this.groupBoxxkxh.TabStop = false;
            this.groupBoxxkxh.Text = "选课序号";
            // 
            // xkxh4
            // 
            this.xkxh4.Location = new System.Drawing.Point(138, 21);
            this.xkxh4.MaxLength = 4;
            this.xkxh4.Name = "xkxh4";
            this.xkxh4.Size = new System.Drawing.Size(38, 22);
            this.xkxh4.TabIndex = 10;
            this.xkxh4.WordWrap = false;
            // 
            // xkxh3
            // 
            this.xkxh3.Location = new System.Drawing.Point(94, 21);
            this.xkxh3.MaxLength = 4;
            this.xkxh3.Name = "xkxh3";
            this.xkxh3.Size = new System.Drawing.Size(38, 22);
            this.xkxh3.TabIndex = 9;
            this.xkxh3.WordWrap = false;
            // 
            // xkxh2
            // 
            this.xkxh2.Location = new System.Drawing.Point(50, 21);
            this.xkxh2.MaxLength = 4;
            this.xkxh2.Name = "xkxh2";
            this.xkxh2.Size = new System.Drawing.Size(38, 22);
            this.xkxh2.TabIndex = 8;
            this.xkxh2.WordWrap = false;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(62, 21);
            this.textBoxUsername.MaxLength = 10;
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(100, 22);
            this.textBoxUsername.TabIndex = 4;
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxLogin.Controls.Add(this.labelUserpassword);
            this.groupBoxLogin.Controls.Add(this.labelUsername);
            this.groupBoxLogin.Controls.Add(this.textBoxUserpassword);
            this.groupBoxLogin.Controls.Add(this.textBoxUsername);
            this.groupBoxLogin.Controls.Add(this.buttonLogin);
            this.groupBoxLogin.Location = new System.Drawing.Point(12, 375);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(272, 81);
            this.groupBoxLogin.TabIndex = 9;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "登录";
            // 
            // labelUserpassword
            // 
            this.labelUserpassword.AutoSize = true;
            this.labelUserpassword.Location = new System.Drawing.Point(8, 52);
            this.labelUserpassword.Name = "labelUserpassword";
            this.labelUserpassword.Size = new System.Drawing.Size(36, 17);
            this.labelUserpassword.TabIndex = 9;
            this.labelUserpassword.Text = "密码";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(6, 24);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(50, 17);
            this.labelUsername.TabIndex = 9;
            this.labelUsername.Text = "用户名";
            // 
            // textBoxUserpassword
            // 
            this.textBoxUserpassword.Location = new System.Drawing.Point(62, 49);
            this.textBoxUserpassword.MaxLength = 40;
            this.textBoxUserpassword.Name = "textBoxUserpassword";
            this.textBoxUserpassword.PasswordChar = '*';
            this.textBoxUserpassword.Size = new System.Drawing.Size(100, 22);
            this.textBoxUserpassword.TabIndex = 5;
            // 
            // buttonTimer1
            // 
            this.buttonTimer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTimer1.Location = new System.Drawing.Point(299, 377);
            this.buttonTimer1.Name = "buttonTimer1";
            this.buttonTimer1.Size = new System.Drawing.Size(86, 79);
            this.buttonTimer1.TabIndex = 13;
            this.buttonTimer1.Text = "刷！";
            this.buttonTimer1.UseVisualStyleBackColor = true;
            this.buttonTimer1.Click += new System.EventHandler(this.buttonTimer1_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(699, 7);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(51, 35);
            this.buttonGo.TabIndex = 3;
            this.buttonGo.Text = "Go!";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // buttonxksc
            // 
            this.buttonxksc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonxksc.Location = new System.Drawing.Point(619, 462);
            this.buttonxksc.Name = "buttonxksc";
            this.buttonxksc.Size = new System.Drawing.Size(131, 54);
            this.buttonxksc.TabIndex = 15;
            this.buttonxksc.Text = "选课手册";
            this.buttonxksc.UseVisualStyleBackColor = true;
            this.buttonxksc.Click += new System.EventHandler(this.buttonxksc_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 528);
            this.Controls.Add(this.buttonxksc);
            this.Controls.Add(this.buttonTimer1);
            this.Controls.Add(this.groupBoxLogin);
            this.Controls.Add(this.groupBoxxkxh);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.buttonNavigate);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Form1";
            this.Text = "刷课 V0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxxkxh.ResumeLayout(false);
            this.groupBoxxkxh.PerformLayout();
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonShua;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox xkxh1;
        private System.Windows.Forms.Button buttonTui;
        private System.Windows.Forms.Button buttonNavigate;
        private System.Windows.Forms.GroupBox groupBoxxkxh;
        private System.Windows.Forms.TextBox xkxh4;
        private System.Windows.Forms.TextBox xkxh3;
        private System.Windows.Forms.TextBox xkxh2;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.GroupBox groupBoxLogin;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelUserpassword;
        private System.Windows.Forms.TextBox textBoxUserpassword;
        private System.Windows.Forms.Button buttonTimer1;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonxksc;
    }
}

