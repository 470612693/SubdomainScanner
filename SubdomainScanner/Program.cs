using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SubdomainScanner
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 强制捕获UI线程异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // 注册UI线程异常事件
            Application.ThreadException += Application_ThreadException;

            // 注册非UI线程异常事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        // UI线程异常处理（可操作UI）
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, "UI线程异常");
        }

        // 非UI线程异常处理（需线程安全操作UI）
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception ?? new Exception("未知异常");
            HandleException(ex, "非UI线程异常");
            // 强制退出程序（非UI线程无法直接操作UI）
            Thread.CurrentThread.Abort();
        }

        // 统一异常处理逻辑
        private static void HandleException(Exception ex, string source)
        {
            // 1. 记录日志
            LogException(ex, source);

            // 2. 线程安全更新UI（仅UI线程可直接操作）
            if (Application.MessageLoop)
            {
                MessageBox.Show($"程序发生错误：{ex.Message}", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // 非UI线程通过BeginInvoke安全更新UI
                Application.OpenForms[0]?.BeginInvoke(new Action(() =>
                    MessageBox.Show($"程序发生错误：{ex.Message}", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
        }
        private static void LogException(Exception ex, string source)
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLogs.txt");
            var logEntry = $"[{DateTime.Now}] 来源：{source}\n" +
                           $"类型：{ex.GetType().Name}\n" +
                           $"消息：{ex.Message}\n" +
                           $"堆栈：{ex.StackTrace}\n\n";

            // 线程安全写入日志
            File.AppendAllText(logPath, logEntry);
        }
    }
}
