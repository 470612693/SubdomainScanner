namespace SubdomainScanner
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolLabelStatus = new System.Windows.Forms.ToolStripLabel();
            this.labelStatus = new System.Windows.Forms.ToolStripLabel();
            this.labelTime = new System.Windows.Forms.ToolStripLabel();
            this.labelThread = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listSubdomain = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.comboThreadCount = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboDns = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkScanServer = new System.Windows.Forms.CheckBox();
            this.checkScanPort = new System.Windows.Forms.CheckBox();
            this.txtPort = new SubdomainScanner.WatermarkTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Size = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开网站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制域名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制所选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出域名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1080, 629);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1080, 679);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLabelStatus,
            this.labelStatus,
            this.labelTime,
            this.labelThread});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(200, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolLabelStatus
            // 
            this.toolLabelStatus.Name = "toolLabelStatus";
            this.toolLabelStatus.Size = new System.Drawing.Size(44, 22);
            this.toolLabelStatus.Text = "状态：";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(56, 22);
            this.labelStatus.Text = "等待启动";
            // 
            // labelTime
            // 
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(44, 22);
            this.labelTime.Text = "总耗时";
            // 
            // labelThread
            // 
            this.labelThread.Name = "labelThread";
            this.labelThread.Size = new System.Drawing.Size(44, 22);
            this.labelThread.Text = "线程数";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 629);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listSubdomain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1076, 625);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listSubdomain
            // 
            this.listSubdomain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listSubdomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSubdomain.FullRowSelect = true;
            this.listSubdomain.HideSelection = false;
            this.listSubdomain.Location = new System.Drawing.Point(3, 43);
            this.listSubdomain.Name = "listSubdomain";
            this.listSubdomain.Size = new System.Drawing.Size(1070, 579);
            this.listSubdomain.TabIndex = 0;
            this.listSubdomain.UseCompatibleStateImageBehavior = false;
            this.listSubdomain.View = System.Windows.Forms.View.Details;
            this.listSubdomain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listSubdomain_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "域名";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "解析IP";
            this.columnHeader3.Width = 350;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "开放端口";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Web服务器";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "网站状态";
            this.columnHeader6.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "来源";
            this.columnHeader7.Width = 80;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.comboThreadCount);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboDns);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.checkScanServer);
            this.panel2.Controls.Add(this.checkScanPort);
            this.panel2.Controls.Add(this.txtPort);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtDomain);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1070, 34);
            this.panel2.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(796, 7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // comboThreadCount
            // 
            this.comboThreadCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboThreadCount.FormattingEnabled = true;
            this.comboThreadCount.Location = new System.Drawing.Point(732, 9);
            this.comboThreadCount.Name = "comboThreadCount";
            this.comboThreadCount.Size = new System.Drawing.Size(47, 20);
            this.comboThreadCount.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(697, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "线程：";
            // 
            // comboDns
            // 
            this.comboDns.FormattingEnabled = true;
            this.comboDns.Location = new System.Drawing.Point(517, 9);
            this.comboDns.Name = "comboDns";
            this.comboDns.Size = new System.Drawing.Size(150, 20);
            this.comboDns.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "DNS：";
            // 
            // checkScanServer
            // 
            this.checkScanServer.AutoSize = true;
            this.checkScanServer.Checked = true;
            this.checkScanServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkScanServer.Location = new System.Drawing.Point(381, 11);
            this.checkScanServer.Name = "checkScanServer";
            this.checkScanServer.Size = new System.Drawing.Size(84, 16);
            this.checkScanServer.TabIndex = 5;
            this.checkScanServer.Text = "服务器信息";
            this.checkScanServer.UseVisualStyleBackColor = true;
            // 
            // checkScanPort
            // 
            this.checkScanPort.AutoSize = true;
            this.checkScanPort.Checked = true;
            this.checkScanPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkScanPort.Location = new System.Drawing.Point(303, 11);
            this.checkScanPort.Name = "checkScanPort";
            this.checkScanPort.Size = new System.Drawing.Size(72, 16);
            this.checkScanPort.TabIndex = 4;
            this.checkScanPort.Text = "扫描端口";
            this.checkScanPort.UseVisualStyleBackColor = true;
            // 
            // txtPort
            // 
            this.txtPort.ForeColor = System.Drawing.Color.Gray;
            this.txtPort.Location = new System.Drawing.Point(226, 9);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(69, 21);
            this.txtPort.TabIndex = 3;
            this.txtPort.Watermark = "默认";
            this.txtPort.WatermarkColor = System.Drawing.Color.Gray;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口：";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(44, 9);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(141, 21);
            this.txtDomain.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "域名：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Setting,
            this.tsmi_Size});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1080, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_Setting
            // 
            this.tsmi_Setting.Name = "tsmi_Setting";
            this.tsmi_Setting.Size = new System.Drawing.Size(44, 21);
            this.tsmi_Setting.Text = "设置";
            this.tsmi_Setting.Click += new System.EventHandler(this.tsmi_Setting_Click);
            // 
            // tsmi_Size
            // 
            this.tsmi_Size.Name = "tsmi_Size";
            this.tsmi_Size.Size = new System.Drawing.Size(80, 21);
            this.tsmi_Size.Text = "自适应大小";
            this.tsmi_Size.Click += new System.EventHandler(this.tsmi_Size_Click);
            // 
            // contextMain
            // 
            this.contextMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开网站ToolStripMenuItem,
            this.复制域名ToolStripMenuItem,
            this.复制IPToolStripMenuItem,
            this.复制所选项ToolStripMenuItem,
            this.导出域名ToolStripMenuItem,
            this.导出全部ToolStripMenuItem});
            this.contextMain.Name = "contextMain";
            this.contextMain.Size = new System.Drawing.Size(137, 136);
            this.contextMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMain_ItemClicked);
            // 
            // 打开网站ToolStripMenuItem
            // 
            this.打开网站ToolStripMenuItem.Name = "打开网站ToolStripMenuItem";
            this.打开网站ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.打开网站ToolStripMenuItem.Text = "打开网站";
            // 
            // 复制域名ToolStripMenuItem
            // 
            this.复制域名ToolStripMenuItem.Name = "复制域名ToolStripMenuItem";
            this.复制域名ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.复制域名ToolStripMenuItem.Text = "复制域名";
            // 
            // 复制IPToolStripMenuItem
            // 
            this.复制IPToolStripMenuItem.Name = "复制IPToolStripMenuItem";
            this.复制IPToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.复制IPToolStripMenuItem.Text = "复制IP";
            // 
            // 复制所选项ToolStripMenuItem
            // 
            this.复制所选项ToolStripMenuItem.Name = "复制所选项ToolStripMenuItem";
            this.复制所选项ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.复制所选项ToolStripMenuItem.Text = "复制所选项";
            // 
            // 导出域名ToolStripMenuItem
            // 
            this.导出域名ToolStripMenuItem.Name = "导出域名ToolStripMenuItem";
            this.导出域名ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.导出域名ToolStripMenuItem.Text = "导出域名";
            // 
            // 导出全部ToolStripMenuItem
            // 
            this.导出全部ToolStripMenuItem.Name = "导出全部ToolStripMenuItem";
            this.导出全部ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.导出全部ToolStripMenuItem.Text = "导出全部";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 679);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "子域名扫描器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolLabelStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Setting;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listSubdomain;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboDns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkScanServer;
        private System.Windows.Forms.CheckBox checkScanPort;
        private WatermarkTextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox comboThreadCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripLabel labelStatus;
        private System.Windows.Forms.ContextMenuStrip contextMain;
        private System.Windows.Forms.ToolStripMenuItem 打开网站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制域名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制IPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制所选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出域名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出全部ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel labelTime;
        private System.Windows.Forms.ToolStripLabel labelThread;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Size;
    }
}

