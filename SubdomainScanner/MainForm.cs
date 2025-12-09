using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;

namespace SubdomainScanner
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            txtPort.Watermark = "默认";
            EnableDoubleBuffering(listSubdomain);
            InitForm();
        }
        private void EnableDoubleBuffering(ListView listView)
        {
            // 反射设置DoubleBuffered属性
            typeof(ListView).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, listView, new object[] { true });

            // 可选：设置父容器双缓冲（如Panel）
            var parent = listView.Parent;
            if (parent != null)
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, parent, new object[] { true });
            }
        }
        #region 窗体相关
        private void InitForm()
        {
            try
            {
                this.listSubdomain.ColumnWidthChanged -= new System.Windows.Forms.ColumnWidthChangedEventHandler(this.list_Info_ColumnWidthChanged);
                this.listSubdomain.ColumnWidthChanging -= new System.Windows.Forms.ColumnWidthChangingEventHandler(this.list_Info_ColumnWidthChanging);
                this.SizeChanged -= new System.EventHandler(this.MainForm_SizeChanged);
                this.Width = GlobalConfig.FormWidth;
                this.Height = GlobalConfig.FormHeight;
                listSubdomain.Columns.Clear();
                foreach (var item in GlobalConfig.ColumnWidth)
                {
                    ColumnHeader columnHeader = new ColumnHeader();
                    columnHeader.Text = item.Key;
                    columnHeader.Width = item.Value;
                    listSubdomain.Columns.Add(columnHeader);
                }

                this.listSubdomain.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.list_Info_ColumnWidthChanged);
                this.listSubdomain.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.list_Info_ColumnWidthChanging);
                this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("初始化窗体异常：{0}", ex));
            }
        }
        private void list_Info_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            ListView list = sender as ListView;
            var name = list.Columns[e.ColumnIndex].Text;
            if (GlobalConfig.ColumnWidth.ContainsKey(name))
            {
                GlobalConfig.ColumnWidth[name] = e.NewWidth;
                Console.WriteLine($"列{name}新宽度：{e.NewWidth}");
            }
            else
            {
                Console.WriteLine($"未知列{name}");
            }
        }
        private void list_Info_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            Console.WriteLine($"保存列宽{e.ColumnIndex}");
            GlobalConfig.SaveConfig();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"窗体宽{this.Width},高{this.Height}");
            GlobalConfig.FormWidth = this.Width;
            GlobalConfig.FormHeight = this.Height;
            GlobalConfig.SaveConfig();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void tsmi_Setting_Click(object sender, EventArgs e)
        {
            Setting form = new Setting();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Task.Factory.StartNew(() =>
                {
                    ReadDictionaryFile();
                });
            }
        }

        private void tsmi_Size_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalConfig.DefaultForm();
                GlobalConfig.SaveConfig();
                InitForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新列表异常：" + ex);
            }
        }
        #endregion
        #region 变量定义

        /// <summary>
        /// dns列表
        /// </summary>
        private Dictionary<string, string> dicDns = new Dictionary<string, string> {
            { "114DNS-114.114.114.114", "114.114.114.114" },
            { "阿里DNS-223.5.5.5", "223.5.5.5" },
            { "DNSPOD-119.29.29.29", "119.29.29.29" },
            { "直接输入DNS服务器地址", "" }
        };
        /// <summary>
        /// 线程列表
        /// </summary>
        private List<string> listThreadCount = new List<string> { "全速", "50", "100", "150", "200", "300", "500" };

        /// <summary>
        /// 原始字典数组
        /// </summary>
        private string[] dicData = new string[0];
        /// <summary>
        /// 字典队列
        /// </summary>
        private ConcurrentQueue<string> queueDic = new ConcurrentQueue<string>();
        /// <summary>
        /// 子域名队列
        /// </summary>
        private ConcurrentQueue<DomainInfo> queueDomain = new ConcurrentQueue<DomainInfo>();
        /// <summary>
        /// 域名
        /// </summary>
        private string Domain = string.Empty;
        /// <summary>
        /// 是否限制线程数
        /// </summary>
        private bool limitThreadCount { get { return ThreadCount != -1; } }
        /// <summary>
        /// 线程总数
        /// </summary>
        int ThreadCount = -1;
        /// <summary>
        /// 扫描端口
        /// </summary>
        bool ScanPort = true;
        /// <summary>
        /// 扫描默认端口
        /// </summary>
        bool ScanDefaultPort = false;
        /// <summary>
        /// 端口列表
        /// </summary>
        string[] listPort = new string[0];
        /// <summary>
        /// 扫描服务
        /// </summary>
        bool ScanServer = true;
        /// <summary>
        /// 是否正在运行
        /// </summary>
        private bool Running = false;
        /// <summary>
        /// 是否正在停止
        /// </summary>
        private bool Stoping = false;
        /// <summary>
        /// Dns的IP地址
        /// </summary>
        private string Dns = string.Empty;
        /// <summary>
        /// 取消信号
        /// </summary>
        CancellationTokenSource cancellation = new CancellationTokenSource();
        /// <summary>
        /// 过滤字符串
        /// </summary>
        private string filter = string.Empty;
        /// <summary>
        /// 是否正在加载字典
        /// </summary>
        private bool loadingDictionary = false;
        /// <summary>
        /// 爆破线程
        /// </summary>
        private Thread threadMain = null;
        /// <summary>
        /// 列表更新线程
        /// </summary>
        private Thread threadUpdate = null;
        /// <summary>
        /// 字典个数
        /// </summary>
        private int DictionaryCount = 0;
        /// <summary>
        /// 子域名个数
        /// </summary>
        private int ChildDomainCount = 0;
        /// <summary>
        /// 显示子域名数
        /// </summary>
        private int DisplayChildDomainCount = 0;
        /// <summary>
        /// 已经枚举的个数
        /// </summary>
        private int EnumerationCount = 0;
        /// <summary>
        /// 枚举计时
        /// </summary>
        Stopwatch stopwatch = new Stopwatch();
        /// <summary>
        /// 状态缓存
        /// </summary>
        private ConcurrentDictionary<ToolStripLabel, string> labelUpdates = new ConcurrentDictionary<ToolStripLabel, string>();
        /// <summary>
        /// 状态更新timer
        /// </summary>
        System.Threading.Timer timer = null;
        /// <summary>
        /// 选中的行
        /// </summary>
        private ListViewItem SelectedItem = null;
        #endregion

        #region 界面事件


        private void MainForm_Load(object sender, EventArgs e)
        {
            listSubdomain.View = View.Details;
            foreach (var item in dicDns)
            {
                comboDns.Items.Add(item.Key);
            }
            comboDns.SelectedIndex = 2;
            foreach (var item in listThreadCount)
            {
                comboThreadCount.Items.Add(item);
            }
            comboThreadCount.SelectedIndex = comboThreadCount.Items.Count - 1;
            Task.Factory.StartNew(() => { ReadDictionaryFile(); });

            timer = new System.Threading.Timer(new TimerCallback(UpdateUI), null, 0, 200);
        }
        /// <summary>
        /// 更新状态栏
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateUI(object obj)
        {
            try
            {
                foreach (var item in labelUpdates)
                {
                    SetStatus(item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新UI异常：{0}", ex);
            }
        }
        /// <summary>
        /// 启动按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Running)
            {
                Stoping = true;
                Running = false;
                cancellation.CancelAfter(15 * 1000);
                SetButtonText(btnStart, "停止中");
            }
            else if (!Stoping)
            {
                var task = StartEnumerationAsync();
                Task.WhenAll(task);
            }
        }
        /// <summary>
        /// 右键菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {

                switch (e.ClickedItem.Text)
                {
                    case "打开网站":
                        OpenUrl(SelectedItem);
                        break;
                    case "复制域名":
                        CopyDomain(SelectedItem);
                        break;
                    case "复制IP":
                        CopyIP(SelectedItem);
                        break;
                    case "复制所选项":
                        CopySelectItem(SelectedItem);
                        break;
                    case "导出域名":
                        ExportDomain();
                        break;
                    case "导出全部":
                        ExportAllDomain();
                        break;
                    default:
                        MessageBox.Show("不支持的功能：" + e.ClickedItem.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("右键功能异常：{0}", ex);
            }
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listSubdomain_MouseClick(object sender, MouseEventArgs e)
        {
            ListView listView = (ListView)sender;
            ListViewItem item = listView.GetItemAt(e.X, e.Y);
            if (item != null && e.Button == MouseButtons.Right)
            {
                SelectedItem = item;
                contextMain.Show(listView, e.X, e.Y);
            }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化爆破信息
        /// </summary>
        private void InitBruteForce()
        {
            try
            {
                cancellation = new CancellationTokenSource();
                Running = true;
                ChildDomainCount = 0;
                DisplayChildDomainCount = 0;
                EnumerationCount = 0;
                DictionaryCount = dicData.Length;
                queueDic = new ConcurrentQueue<string>(dicData);
                queueDomain = new ConcurrentQueue<DomainInfo>();
                ClearListView(listSubdomain);
            }
            catch (Exception ex)
            {
                Console.WriteLine("初始化爆破异常：" + ex);
            }
        }
        /// <summary>
        /// 初始化界面更新线程
        /// </summary>
        private void InitThreadUpdate()
        {
            try
            {
                if (threadUpdate == null)
                {
                    threadUpdate = new Thread(new ThreadStart(UpdateListViewByQueue));
                    threadUpdate.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("列表更新线程异常：" + ex);
            }
        }
        #endregion
        #region 写入操作
        /// <summary>
        /// 写子域名信息进队列
        /// </summary>
        /// <param name="info"></param>
        private void WriteQueueDomain(DomainInfo info)
        {
            try
            {
                ChildDomainCount++;
                queueDomain.Enqueue(info);
                InitThreadUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("写子域名信息进队列异常：" + ex);
            }
        }
        #endregion

        #region 功能
        /// <summary>
        /// 检查Dns的ip
        /// </summary>
        /// <param name="dns"></param>
        /// <returns></returns>
        private bool CheckDnsIpAddress(string dns)
        {
            if (!string.IsNullOrEmpty(Dns))
            {
                return Regex.IsMatch(dns, @"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$");
            }
            return false;
        }
        /// <summary>
        /// 加载字典中
        /// </summary>
        private void ReadDictionaryFile()
        {
            try
            {
                loadingDictionary = true;
                using (FileStream fs = new FileStream(GlobalConfig.DictionaryPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string line = null;
                        while ((line = reader.ReadLine()) != null)
                        {
                            queueDic.Enqueue(line);
                        }
                        Console.WriteLine("读取字典完成");
                        dicData = queueDic.ToArray();
                        loadingDictionary = false;
                        queueDic = new ConcurrentQueue<string>();
                        //SetStatus(labelStatus, string.Format("字典加载完成，个数：{0}，等待启动", dicData.Length));

                        AddLabelStatusUpdate(labelStatus, string.Format("字典加载完成，个数：{0}，等待启动", dicData.Length));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("读取字典异常：" + ex);
            }
        }
        /// <summary>
        /// 启动枚举
        /// </summary>
        private async Task StartEnumerationAsync()
        {
            try
            {
                if (loadingDictionary)
                {
                    MessageBox.Show("正在加载字典中，请稍候！");
                    return;
                }
                if (dicData.Length == 0)
                {
                    MessageBox.Show("请加载正确的字典！");
                    return;
                }
                Domain = txtDomain.Text;
                if (string.IsNullOrEmpty(Domain))
                {
                    MessageBox.Show("请输入域名！");
                    return;
                }

                if (checkScanPort.Checked)
                {
                    ScanPort = true;
                    string port = txtPort.Text;
                    if (string.IsNullOrEmpty(port))
                    {
                        ScanDefaultPort = true;
                    }
                    else
                    {
                        ScanDefaultPort = false;
                        listPort = port.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                        if (listPort.Length > 0)
                        {
                            ScanPort = checkScanPort.Checked;
                        }
                    }
                    ScanServer = checkScanServer.Checked;
                }
                else
                {
                    ScanPort = false;
                    ScanDefaultPort = false;
                    ScanServer = false;
                }
                string selectDns = string.Empty;
                if (string.IsNullOrEmpty(comboDns.Text))
                {
                    selectDns = comboDns.SelectedItem.ToString();
                }
                else
                {
                    selectDns = comboDns.Text;
                }
                if (dicDns.ContainsKey(selectDns))
                {
                    Dns = dicDns[selectDns];
                }
                else
                {
                    Dns = selectDns;
                }
                if (!CheckDnsIpAddress(Dns))
                {
                    MessageBox.Show("请输入正确的DNS服务器地址！");
                    return;
                }
                ThreadCount = comboThreadCount.SelectedItem.ToString() == "全速" ? -1 : Convert.ToInt32(comboThreadCount.SelectedItem.ToString());

                DnsCustomResolver.SetDnsServer(Dns, ThreadCount);
                filter = await GetFilterAsync();
                if (string.IsNullOrEmpty(filter) || MessageBox.Show(string.Format("此域名泛解析到：{0}。程序将自动跳过解析到此 IP 的域名。\r\n是否继续爆破 ", filter), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (threadMain == null)
                    {
                        threadMain = new Thread(new ThreadStart(BruteForceEnumeration));
                        threadMain.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动爆破异常：" + ex);
            }
        }
        /// <summary>
        /// 获取泛解析过滤信息
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetFilterAsync()
        {
            string result = string.Empty;
            try
            {
                List<Task<IPAddress[]>> list = new List<Task<IPAddress[]>>();
                for (int i = 0; i < 3; i++)
                {
                    string str = string.Format("{0}seaydomaincheck.{1}", i, Domain);
                    list.Add(DnsCustomResolver.ResolveWithDnsAsync(str, GlobalConfig.DnsTimeout));

                }
                try
                {
                    await Task.WhenAll(list.ToArray());
                    List<IPAddress> res = new List<IPAddress>();
                    foreach (var task in list)
                    {
                        if (task.Result != null)
                        {
                            res.AddRange(task.Result);
                        }
                    }
                    return GetIp(res.ToArray());
                }
                catch (AggregateException ex)
                {
                    // 处理多个异常
                    foreach (var innerEx in ex.InnerExceptions)
                    {
                        Console.WriteLine($"Error: {innerEx.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取Filter异常：" + ex);
            }
            //0seaydomaincheck.jumei.com
            return result;
        }
        /// <summary>
        /// 暴力枚举
        /// </summary>
        private void BruteForceEnumeration()
        {

            try
            {
                SetButtonText(btnStart, "启动中");
                InitBruteForce();
                SetButtonText(btnStart, "停止");
                Console.WriteLine($"开始查找子域名，domain：{Domain}，dns:{Dns}");
                stopwatch = new Stopwatch();
                stopwatch.Start();
                StartTasks(limitThreadCount ? ThreadCount : GlobalConfig.FullspeedThreads);
                AddLabelStatusUpdate(labelThread, "");

            }
            catch (Exception ex)
            {
                Console.WriteLine("暴力枚举主线程异常：" + ex);
            }
            finally
            {
                stopwatch.Stop();
                DnsCustomResolver.Release();
                AddLabelStatusUpdate(labelTime, string.Format("总耗时：{0}秒", stopwatch.Elapsed.TotalSeconds.ToString("F2")));
                AddLabelStatusUpdate(labelStatus, string.Format("完成，共发现{0}个子域名", ChildDomainCount));
                Stoping = false;
                SetButtonText(btnStart, "启动");
                threadMain = null;
            }
        }

        /// <summary>
        /// 全速
        /// </summary>
        /// <param name="threadCount"></param>
        public void StartTasks(int threadCount)
        {

            ThreadPool.SetMinThreads(threadCount, threadCount); // 推荐设为CPU核心数的2-4倍
            ThreadPool.SetMaxThreads(threadCount * 5, threadCount * 5); // 根据系统资源调整，避免过度消耗
            // 启动多个异步worker并行处理
            var tasks = Enumerable.Range(0, threadCount)
                                   .Select(_ => StartFindSubdomainAsync())
                                   .ToArray();
            // 等待所有消费者完成;
            Console.WriteLine("等待所有线程执行完");
            Task.WaitAll(tasks.ToArray(), cancellation.Token);
            Console.WriteLine("所有线程执行完毕");
        }
        /// <summary>
        /// 查找子域名线程
        /// </summary>
        /// <returns></returns>
        private async Task StartFindSubdomainAsync()
        {
            try
            {
                while (Running && !cancellation.IsCancellationRequested)
                {
                    if (!cancellation.IsCancellationRequested && queueDic.IsEmpty) break;
                    //await _concurrencyLimiter.WaitAsync(); // 控制并发度
                    if (queueDic.TryDequeue(out var data))
                    {
                        try
                        {
                            EnumerationCount++;
                            DomainInfo info = new DomainInfo(data, EnumerationCount);
                            await FindSubdomainAsync(info).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"查子域名线程异常:{ex}");
                        }
                        finally
                        {
                            //_concurrencyLimiter.Release();
                        }
                    }
                    else
                    {
                        //_concurrencyLimiter.Release();
                        await Task.Delay(10); // 短暂让步，避免忙等待
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"查子域名线程异常:{ex}");
            }
        }

        /// <summary>
        /// 查找子域名
        /// </summary>
        /// <param name="child"></param>
        private async Task FindSubdomainAsync(DomainInfo info)
        {
            Stopwatch stopwatchFind = new Stopwatch();
            stopwatchFind.Start();
            try
            {
                if (info != null)
                {
                    info.Domain = string.Format("{0}.{1}", info.Child, Domain);
                    AddLabelStatusUpdate(labelStatus, string.Format("枚举模式，已经发现{0}个域名，进度{1}/{2}({3}%)  当前{4}", ChildDomainCount, EnumerationCount, DictionaryCount, Math.Round(EnumerationCount * 100.0 / DictionaryCount, 1), info.Domain));
                    AddLabelStatusUpdate(labelTime, string.Format("耗时：{0}秒({1}条/秒)", stopwatch.Elapsed.TotalSeconds.ToString("F2"), Math.Round(EnumerationCount / stopwatch.Elapsed.TotalSeconds, 1)));
                    ThreadPool.GetAvailableThreads(out var workerThreads, out var ioThreads);
                    AddLabelStatusUpdate(labelThread, string.Format("线程数：{0},可用工作线程: {1}, I/O线程: {2}", Process.GetCurrentProcess().Threads.Count, workerThreads, ioThreads));
                    var res = await checkDnsAsync(info);

                    if (res && !string.IsNullOrEmpty(info.Ip))
                    {
                        //Console.WriteLine("DNS解析成功：{0}", info.Ip);
                        await checkPortAsync(info);
                        WriteQueueDomain(info);
                    }
                }
                //else
                //{
                //    Console.WriteLine("字典值为空");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ info?.Index}暴力枚举{ info?.Domain},异常:{ex}");
            }
            finally
            {
                stopwatchFind.Stop();
                Console.WriteLine($"{info?.Domain},查找子域名耗时:{stopwatchFind.ElapsedMilliseconds}");
            }
        }
        /// <summary>
        /// 检查dns
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task<bool> checkDnsAsync(DomainInfo info)
        {
            try
            {
                if (info == null)
                {
                    Console.WriteLine("DNS解析obj为null");
                    return false;
                }
                //Console.WriteLine("DNS解析Domain:{0}", info.Domain);

                var result = await DnsCustomResolver.ResolveWithDnsAsync(info.Domain);
                if (result != null && result.Length > 0)
                {
                    info.Ip = GetIp(result);
                    if (info.Ip == filter)
                    {
                        return false;
                    }
                    return true;
                }
                //else
                //{
                //    Console.WriteLine("获取IP地址{0}失败", info.Domain);
                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine("获取IP地址异常:{0}", ex);
            }
            return false;
        }
        /// <summary>
        /// 获取ips
        /// </summary>
        /// <param name="ips"></param>
        /// <returns></returns>
        private string GetIp(IPAddress[] ips)
        {
            try
            {
                if (ips != null && ips.Length > 0)
                {
                    Array.Sort(ips, new IpAddressComparer());
                    List<string> list = new List<string>();
                    foreach (var item in ips)
                    {
                        list.Add(item.ToString());
                    }
                    return string.Join(",", list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetIp异常：{ex}");
            }
            return string.Empty;
        }
        /// <summary>
        /// 扫描端口
        /// </summary>
        /// <param name="info"></param>
        private async Task checkPortAsync(DomainInfo info)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            try
            {
                if (ScanPort && ScanServer)
                {
                    List<Task<ServerInfo>> list = new List<Task<ServerInfo>>();
                    if (ScanDefaultPort)
                    {
                        list.Add(HttpHelper.GetServerInfo(info.Domain, "80", NetProtocol.http, GlobalConfig.ServerTimeout));
                        list.Add(HttpHelper.GetServerInfo(info.Domain, "443", NetProtocol.https, GlobalConfig.ServerTimeout));
                    }
                    else
                    {
                        foreach (var port in listPort)
                        {
                            list.Add(HttpHelper.GetServerInfo(info.Domain, port, NetProtocol.http, GlobalConfig.ServerTimeout));
                            list.Add(HttpHelper.GetServerInfo(info.Domain, port, NetProtocol.https, GlobalConfig.ServerTimeout));
                        }
                    }
                    try
                    {
                        //Console.WriteLine($"扫描{info.Domain}线程启动完成");
                        await Task.WhenAll(list.ToArray());
                        //Console.WriteLine($"扫描{info.Domain}线程完成");
                        info.ServerInfos = new List<ServerInfo>(list.Count);
                        foreach (var task in list)
                        {
                            if (task.Result != null)
                            {
                                info.ServerInfos.Add(task.Result);
                            }
                        }
                        info.SetPortByServerInfos();
                    }
                    catch (AggregateException ex)
                    {
                        // 处理多个异常
                        foreach (var innerEx in ex.InnerExceptions)
                        {
                            Console.WriteLine($"Error: {innerEx.Message}");
                        }
                    }

                }
                else if (ScanPort)
                {
                    List<Task<string>> list = new List<Task<string>>();
                    foreach (var port in listPort)
                    {
                        list.Add(PortScanner.PortScannerAsync(info.Domain, port, GlobalConfig.PortTimeout));
                    }
                    try
                    {
                        //Console.WriteLine($"扫描{info.Domain}线程启动完成");
                        await Task.WhenAll(list.ToArray());
                        //Console.WriteLine($"扫描{info.Domain}线程完成");
                        List<string> result = new List<string>();
                        foreach (var task in list)
                        {
                            if (task.Result != null)
                            {
                                result.Add(task.Result);
                            }
                        }
                        info.Port = string.Join(",", result);
                    }
                    catch (AggregateException ex)
                    {
                        // 处理多个异常
                        foreach (var innerEx in ex.InnerExceptions)
                        {
                            Console.WriteLine($"Error: {innerEx.Message}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"扫描{info.Domain}端口,异常：{ex}");
            }
            finally
            {
                //stopwatch.Stop();
                //Console.WriteLine($"{info.Domain},端口或服务扫描耗时:{stopwatch.ElapsedMilliseconds}");
            }
        }

        /// <summary>
        /// 打开网址
        /// </summary>
        /// <param name="item"></param>
        private void OpenUrl(ListViewItem item)
        {
            var info = item.Tag as DomainInfo;
            if (string.IsNullOrEmpty(info.Url))
            {
                string url = string.Format("http://{0}", info.Domain);
                Process.Start(url);
            }
            else
            {
                Process.Start(info.Url);
            }
        }
        /// <summary>
        /// 复制域名
        /// </summary>
        /// <param name="item"></param>
        private void CopyDomain(ListViewItem item)
        {
            var info = item.Tag as DomainInfo;
            Clipboard.SetText(info.Domain);
        }
        /// <summary>
        /// 复制IP
        /// </summary>
        /// <param name="item"></param>
        private void CopyIP(ListViewItem item)
        {
            var info = item.Tag as DomainInfo;
            Clipboard.SetText(info.Ip);
        }
        /// <summary>
        /// 复制所选项
        /// </summary>
        /// <param name="item"></param>
        private void CopySelectItem(ListViewItem item)
        {
            var info = item.Tag as DomainInfo;
            string str = GetCsvText(new List<DomainInfo> { info });
            Clipboard.SetText(str);
        }
        /// <summary>
        /// 导出域名
        /// </summary>
        private void ExportDomain()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "导出子域名";
            dialog.FileName = string.Format("{0}子域名列表_Layer.txt", Domain);
            dialog.OverwritePrompt = true;
            dialog.Filter = "Text files (.txt)|.txt|All files (.)|.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                List<DomainInfo> list = GetAllDomainInfo();
                string contents = list.Select(s => s.Domain).Join(Environment.NewLine);
                File.WriteAllText(file, contents);
            }
        }
        /// <summary>
        /// 导出所有域名
        /// </summary>
        private void ExportAllDomain()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "导出子域名";
            dialog.FileName = string.Format("{0}子域名列表_Layer.txt", Domain);
            dialog.OverwritePrompt = true;
            dialog.Filter = "Text files (.txt)|.txt|All files (.)|.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                List<DomainInfo> list = GetAllDomainInfo();
                string contents = GetCsvText(list);
                File.WriteAllText(file, contents);
            }
        }
        /// <summary>
        /// 生成csv文件内容
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetCsvText(List<DomainInfo> list)
        {
            StringBuilder sb = new StringBuilder();
            string separator = "\t\t";
            List<string> header = new List<string> { "域名", "解析IP", "开放端口", "WEB服务器", "网站状态" };
            foreach (var item in header)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(item);
            }
            sb.AppendLine();
            foreach (var info in list)
            {
                sb.AppendFormat("{1}{0}", separator, info.Domain);
                sb.AppendFormat("{1}{0}", separator, info.Ip);
                sb.AppendFormat("{1}{0}", separator, info.Port);
                sb.AppendFormat("{1}{0}", separator, info.WebServer);
                sb.AppendFormat("{1}{0}", separator, info.WebStatus);
                sb.AppendFormat("{0}", info.Source);
                sb.AppendLine();
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取所有子域名数据
        /// </summary>
        /// <returns></returns>
        private List<DomainInfo> GetAllDomainInfo()
        {
            try
            {
                List<DomainInfo> list = new List<DomainInfo>();
                foreach (ListViewItem item in listSubdomain.Items)
                {
                    list.Add((DomainInfo)item.Tag);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("从列表获取子域名信息异常：" + ex);
                return null;
            }
        }
        #endregion
        #region 界面更新
        /// <summary>
        /// 更新列表
        /// </summary>
        private void UpdateListViewByQueue()
        {
            try
            {
                while (queueDomain.TryDequeue(out DomainInfo info))
                {
                    UpdateListView(listSubdomain, info);
                }
                threadUpdate = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("队列数据更新列表异常：" + ex);
            }
        }
        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="listView"></param>
        private void ClearListView(ListView listView)
        {
            try
            {
                if (listView.InvokeRequired)
                {
                    listView.Invoke(new Action<ListView>(ClearListView), listView);
                }
                else
                {
                    listView.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("清空列表异常：" + ex);
            }
        }
        /// <summary>
        /// 设置按钮文本
        /// </summary>
        /// <param name="button"></param>
        /// <param name="text"></param>
        private void SetButtonText(Button button, string text)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    button.Text = text;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("设置按钮文本异常：" + ex);
            }
        }
        /// <summary>
        /// 设置状态内容
        /// </summary>
        /// <param name="text"></param>
        private void SetStatus(ToolStripLabel label, string text)
        {
            try
            {
                //用队列异步执行操作
                this.Invoke(new Action(() =>
                {
                    label.Text = text;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("设置状态内容异常：" + ex);
            }
        }
        /// <summary>
        /// 增加状态更新缓存
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        private void AddLabelStatusUpdate(ToolStripLabel label, string text)
        {
            try
            {
                if (labelUpdates.ContainsKey(label))
                {
                    labelUpdates[label] = text;
                }
                else
                {
                    labelUpdates.TryAdd(label, text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("设置状态内容异常：" + ex);
            }
        }
        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="info"></param>
        private void UpdateListView(ListView listView, DomainInfo info)
        {
            try
            {
                if (listView.InvokeRequired)
                {
                    listView.Invoke(new Action<ListView, DomainInfo>(UpdateListView), listView, info);
                }
                else
                {
                    try
                    {
                        DisplayChildDomainCount++;
                        ListViewItem item = new ListViewItem(DisplayChildDomainCount.ToString());
                        item.Tag = info;
                        item.SubItems.Add(info.Domain);
                        item.SubItems.Add(info.Ip);
                        item.SubItems.Add(info.Port);
                        item.SubItems.Add(info.WebServer);
                        item.SubItems.Add(info.WebStatus);
                        item.SubItems.Add(info.Source);
                        //listView.BeginUpdate();
                        //listView.Items.Add(item);
                        //listView.EndUpdate();
                        listView.SuspendLayout();
                        try
                        {
                            // 批量操作...
                            listView.Items.Add(item);
                        }
                        finally
                        {
                            listView.ResumeLayout();
                        }

                        if (!Running)
                        {
                            AddLabelStatusUpdate(labelStatus, string.Format("完成，共发现{0}个子域名", DisplayChildDomainCount));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("更新列表异常2：" + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新列表异常：" + ex);
            }
        }
        #endregion


    }

}
