using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubdomainScanner
{
    public static class PortScanner
    {
        public static async Task<string> PortScannerAsync(string ip, string port, int timeout = 2)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            try
            {
                TcpClient client = new TcpClient();
                int iport = Convert.ToInt32(port);
                var vonnectTask = client.ConnectAsync(ip, iport);
                var completedTask = await Task.WhenAny(vonnectTask, Task.Delay(Timeout.Infinite, cts.Token));
                // 3. 判断是否超时
                if (completedTask != vonnectTask)
                {
                    client.Close();
                    client.Dispose();
                    return null;
                }

                if (client.Connected)
                {
                    //Console.WriteLine($"✅ 端口 {port} → 开放");
                    client.Close();
                    client.Dispose();
                    return port;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("端口扫描异常：{0}！", ex);
            }
            finally
            {
                cts.Dispose();
            }
            return null;
        }
    }
}
