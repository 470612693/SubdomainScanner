using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubdomainScanner
{
    public static class GlobalConfig
    {
        public static string DefaultDictionaryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dic.txt");
        /// <summary>
        /// 字典路径
        /// </summary>
        public static string DictionaryPath { set; get; } = DefaultDictionaryPath;
        /// <summary>
        /// Dns请求超时时间
        /// </summary>
        public static int DnsTimeout { set; get; } = 5;
        /// <summary>
        /// 端口扫描超时时间
        /// </summary>
        public static int PortTimeout { set; get; } = 5;
        /// <summary>
        /// 服务扫描超时时间
        /// </summary>
        public static int ServerTimeout { set; get; } = 5;
        /// <summary>
        /// 列宽
        /// </summary>
        public static Dictionary<string, int> ColumnWidth { set; get; } = new Dictionary<string, int> { };
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public static int FormWidth { set; get; } = 1100;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public static int FormHeight { set; get; } = 700;
        /// <summary>
        /// 全速线程数
        /// </summary>
        public static int FullspeedThreads { set; get; } = 700;
        /// <summary>
        /// 更新锁
        /// </summary>
        private static object lockobj = new object();
        static GlobalConfig()
        {
            IninConfig();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private static void IninConfig()
        {
            try
            {

                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string configPath = Path.Combine(appDataPath, Application.ProductName, "config");
                if (!Directory.Exists(Path.GetDirectoryName(configPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(configPath));
                }
                if (File.Exists(configPath))
                {
                    lock (lockobj)
                    {
                        //加载配置
                        string content = File.ReadAllText(configPath);
                        LoadConfig(content);
                    }
                }
                else
                {
                    DefaultColumnWidth();
                    lock (lockobj)
                    {
                        string content = GetConfig();
                        File.WriteAllText(configPath, content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("初始配置异常：{0}", ex);
            }

        }
        /// <summary>
        /// 默认列
        /// </summary>
        private static void DefaultColumnWidth()
        {
            ColumnWidth.Clear();
            ColumnWidth.Add("序号", 50);
            ColumnWidth.Add("域名", 150);
            ColumnWidth.Add("解析IP", 350);
            ColumnWidth.Add("开放端口", 100);
            ColumnWidth.Add("Web服务器", 100);
            ColumnWidth.Add("网站状态", 150);
            ColumnWidth.Add("来源", 80);
        }
        /// <summary>
        /// 默认窗口大小
        /// </summary>
        public static void DefaultForm()
        {
            FormWidth = 1100;
            FormHeight = 700;
            DefaultColumnWidth();
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        public static void SaveConfig()
        {
            try
            {
                lock (lockobj)
                {
                    string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string configPath = Path.Combine(appDataPath, Application.ProductName, "config");
                    string content = GetConfig();
                    File.WriteAllText(configPath, content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("初始配置异常：{0}", ex);
            }
        }
        #region 生成配置内容
        /// <summary>
        /// 生成配置内容
        /// </summary>
        /// <returns></returns>
        private static string GetConfig()
        {
            Type type = typeof(GlobalConfig);
            var properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                try
                {
                    var t = property.PropertyType;
                    if (t == typeof(int))
                    {
                        object value = property.GetValue(null);
                        sb.AppendFormat("{0}={1}", property.Name, value);
                        sb.AppendLine();
                    }
                    else if (t == typeof(string))
                    {
                        object value = property.GetValue(null);
                        sb.AppendFormat("{0}={1}", property.Name, value);
                        sb.AppendLine();
                    }
                    else if (t == typeof(Dictionary<string, int>))
                    {
                        Dictionary<string, int> dic = (Dictionary<string, int>)property.GetValue(null);
                        var value = GetConfigForDictionary(dic);
                        sb.AppendFormat("{0}=[{1}]", property.Name, value);
                        sb.AppendLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("生成配置异常：{0}", ex);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成列宽配置
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string GetConfigForDictionary(Dictionary<string, int> dic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dic)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }
                sb.AppendFormat("{0}:{1}", item.Key, item.Value);
            }
            return sb.ToString();
        }
        #endregion
        #region 加载配置
        /// <summary>
        /// 解析配置
        /// </summary>
        /// <param name="content"></param>
        private static void LoadConfig(string content)
        {
            var matches = Regex.Matches(content, "(?<key>[^=]+)=(?<value>[^\r\n]+)[\r\n]*", RegexOptions.Multiline);
            Type type = typeof(GlobalConfig);
            foreach (Match match in matches)
            {
                string key = match.Groups["key"].Value;
                string value = match.Groups["value"].Value;

                var property = type.GetProperty(key, BindingFlags.Static | BindingFlags.Public);
                if (property != null)
                {

                    var t = property.PropertyType;
                    if (t == typeof(Dictionary<string, int>))
                    {
                        Dictionary<string, int> dic = GetDictionaryByConfig(value);
                        property.SetValue(null, dic);
                    }
                    else if (t == typeof(int))
                    {
                        int ivalue = Convert.ToInt32(value);
                        property.SetValue(null, ivalue);
                    }
                    else
                    {
                        property.SetValue(null, value);
                    }
                }
                else
                {
                    Console.WriteLine("未知配置");
                }
            }
        }
        /// <summary>
        /// 解析列宽配置
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static Dictionary<string, int> GetDictionaryByConfig(string content)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            try
            {
                content = content.Replace("[", "").Replace("]", "");
                var matches = Regex.Matches(content, "(?<key>[^:]+):(?<value>[0-9]+),?", RegexOptions.Multiline);
                foreach (Match match in matches)
                {
                    try
                    {
                        string key = match.Groups["key"].Value;
                        int value = Convert.ToInt32(match.Groups["value"].Value);

                        result.Add(key, value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("解析字典配置项异常：{0}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("解析字典配置异常：{0}", ex);
            }
            return result;
        }
        #endregion
    }
}
