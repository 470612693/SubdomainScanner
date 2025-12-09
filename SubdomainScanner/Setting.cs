using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubdomainScanner
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            txtDictionaryPath.Text = GlobalConfig.DictionaryPath;
            txtDnsTimeout.Text = GlobalConfig.DnsTimeout.ToString();
            txtPortTimeout.Text = GlobalConfig.PortTimeout.ToString();
            txtServerTimeout.Text = GlobalConfig.ServerTimeout.ToString();
            txtFullspeedThreads.Text = GlobalConfig.FullspeedThreads.ToString();
            labelFullspeedThreads.Text = string.Format("建议:CPU核心数*5;建议值：{0}", Environment.ProcessorCount * 5);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtDictionaryPath.Text = dialog.FileName;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                string DictionaryPath = txtDictionaryPath.Text;
                string DnsTimeout = txtDnsTimeout.Text;
                string PortTimeout = txtPortTimeout.Text;
                string ServerTimeout = txtServerTimeout.Text;
                string FullspeedThreads = txtFullspeedThreads.Text;
                int iDnsTimeout = 0;
                int iPortTimeout = 0;
                int iServerTimeout = 0;
                int iFullspeedThreads = 0;
                if (!File.Exists(DictionaryPath))
                {
                    MessageBox.Show("字典不存在，无法保存配置");
                    return;
                }
                if (!(int.TryParse(DnsTimeout, out iDnsTimeout) && iDnsTimeout > 0))
                {
                    MessageBox.Show("Dns请求超时时间必须大于0，且为整数，无法保存配置");
                    return;
                }
                if (!(int.TryParse(PortTimeout, out iPortTimeout) && iPortTimeout > 0))
                {
                    MessageBox.Show("端口扫描超时时间必须大于0，且为整数，无法保存配置");
                    return;
                }
                if (!(int.TryParse(ServerTimeout, out iServerTimeout) && iServerTimeout > 0))
                {
                    MessageBox.Show("服务扫描超时时间必须大于0，且为整数，无法保存配置");
                    return;
                }
                if (!(int.TryParse(FullspeedThreads, out iFullspeedThreads) && iFullspeedThreads > 0))
                {
                    MessageBox.Show("全速线程数必须大于0，且为整数，无法保存配置");
                    return;
                }
                GlobalConfig.DictionaryPath = DictionaryPath;
                GlobalConfig.DnsTimeout = iDnsTimeout;
                GlobalConfig.PortTimeout = iPortTimeout;
                GlobalConfig.ServerTimeout = iServerTimeout;
                GlobalConfig.FullspeedThreads = iFullspeedThreads;
                GlobalConfig.SaveConfig();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("修改配置文件异常：" + ex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtDictionaryPath.Text = GlobalConfig.DefaultDictionaryPath;
        }
    }
}
