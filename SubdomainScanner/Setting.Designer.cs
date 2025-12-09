namespace SubdomainScanner
{
    partial class Setting
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
            this.txtDictionaryPath = new System.Windows.Forms.TextBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDnsTimeout = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPortTimeout = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerTimeout = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.txtFullspeedThreads = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelFullspeedThreads = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字典路径：";
            // 
            // txtDictionaryPath
            // 
            this.txtDictionaryPath.Location = new System.Drawing.Point(76, 31);
            this.txtDictionaryPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtDictionaryPath.Name = "txtDictionaryPath";
            this.txtDictionaryPath.Size = new System.Drawing.Size(381, 21);
            this.txtDictionaryPath.TabIndex = 10;
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(182, 193);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 4;
            this.btnSetting.Text = "保存";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(301, 193);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(462, 30);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Dns请求超时时间：";
            // 
            // txtDnsTimeout
            // 
            this.txtDnsTimeout.Location = new System.Drawing.Point(125, 59);
            this.txtDnsTimeout.Name = "txtDnsTimeout";
            this.txtDnsTimeout.Size = new System.Drawing.Size(50, 21);
            this.txtDnsTimeout.TabIndex = 8;
            this.txtDnsTimeout.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "秒";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "秒";
            // 
            // txtPortTimeout
            // 
            this.txtPortTimeout.Location = new System.Drawing.Point(125, 86);
            this.txtPortTimeout.Name = "txtPortTimeout";
            this.txtPortTimeout.Size = new System.Drawing.Size(50, 21);
            this.txtPortTimeout.TabIndex = 11;
            this.txtPortTimeout.Text = "2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "端口扫描超时时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(180, 117);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "秒";
            // 
            // txtServerTimeout
            // 
            this.txtServerTimeout.Location = new System.Drawing.Point(125, 113);
            this.txtServerTimeout.Name = "txtServerTimeout";
            this.txtServerTimeout.Size = new System.Drawing.Size(50, 21);
            this.txtServerTimeout.TabIndex = 14;
            this.txtServerTimeout.Text = "2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 117);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "服务扫描超时时间：";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(543, 30);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 16;
            this.btnDefault.Text = "默认";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // txtFullspeedThreads
            // 
            this.txtFullspeedThreads.Location = new System.Drawing.Point(125, 140);
            this.txtFullspeedThreads.Name = "txtFullspeedThreads";
            this.txtFullspeedThreads.Size = new System.Drawing.Size(50, 21);
            this.txtFullspeedThreads.TabIndex = 18;
            this.txtFullspeedThreads.Text = "2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 144);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "全速线程数：";
            // 
            // labelFullspeedThreads
            // 
            this.labelFullspeedThreads.AutoSize = true;
            this.labelFullspeedThreads.Location = new System.Drawing.Point(180, 143);
            this.labelFullspeedThreads.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFullspeedThreads.Name = "labelFullspeedThreads";
            this.labelFullspeedThreads.Size = new System.Drawing.Size(17, 12);
            this.labelFullspeedThreads.TabIndex = 19;
            this.labelFullspeedThreads.Text = "个";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 233);
            this.ControlBox = false;
            this.Controls.Add(this.labelFullspeedThreads);
            this.Controls.Add(this.txtFullspeedThreads);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtServerTimeout);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPortTimeout);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDnsTimeout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.txtDictionaryPath);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDictionaryPath;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDnsTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPortTimeout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServerTimeout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.TextBox txtFullspeedThreads;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelFullspeedThreads;
    }
}