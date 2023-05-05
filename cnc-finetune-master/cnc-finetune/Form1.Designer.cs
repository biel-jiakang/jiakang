namespace cnc_finetune {
    partial class Form1 {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.实时记录 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Port = new System.Windows.Forms.TextBox();
            this.Port1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CNCName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PLC_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TCP_IP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.实时记录.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(-1, 219);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 26;
            this.button5.Text = "测试";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(-1, 171);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 25;
            this.button4.Text = "坐标";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(-1, 120);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "截图";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // 实时记录
            // 
            this.实时记录.Controls.Add(this.tabPage1);
            this.实时记录.Controls.Add(this.tabPage2);
            this.实时记录.Location = new System.Drawing.Point(106, 31);
            this.实时记录.Name = "实时记录";
            this.实时记录.SelectedIndex = 0;
            this.实时记录.Size = new System.Drawing.Size(468, 332);
            this.实时记录.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "消息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(22, 23);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(432, 266);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Port);
            this.tabPage2.Controls.Add(this.Port1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.CNCName);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.PLC_IP);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.TCP_IP);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(460, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "参数";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(66, 78);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(100, 21);
            this.Port.TabIndex = 61;
            // 
            // Port1
            // 
            this.Port1.AutoSize = true;
            this.Port1.Location = new System.Drawing.Point(7, 83);
            this.Port1.Name = "Port1";
            this.Port1.Size = new System.Drawing.Size(29, 12);
            this.Port1.TabIndex = 60;
            this.Port1.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 59;
            this.label3.Text = "机台名字";
            // 
            // CNCName
            // 
            this.CNCName.Location = new System.Drawing.Point(66, 18);
            this.CNCName.Name = "CNCName";
            this.CNCName.Size = new System.Drawing.Size(100, 21);
            this.CNCName.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 57;
            this.label2.Text = "PLC_IP";
            // 
            // PLC_IP
            // 
            this.PLC_IP.Location = new System.Drawing.Point(66, 108);
            this.PLC_IP.Name = "PLC_IP";
            this.PLC_IP.Size = new System.Drawing.Size(100, 21);
            this.PLC_IP.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 55;
            this.label1.Text = "TCP_IP";
            // 
            // TCP_IP
            // 
            this.TCP_IP.Location = new System.Drawing.Point(66, 48);
            this.TCP_IP.Name = "TCP_IP";
            this.TCP_IP.Size = new System.Drawing.Size(100, 21);
            this.TCP_IP.TabIndex = 54;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 44;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 396);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.实时记录);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.实时记录.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl 实时记录;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.Label Port1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox CNCName;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox PLC_IP;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox TCP_IP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
    }
}

