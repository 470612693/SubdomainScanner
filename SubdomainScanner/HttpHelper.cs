using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubdomainScanner
{
    public static class HttpHelper
    {

        public static async Task<ServerInfo> GetServerInfo(string url, string port, NetProtocol protocol, int timeout = 2)
        {
            ServicePointManager.DefaultConnectionLimit = 1000; // 调整连接池大小
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            // 1. 创建超时控制源（异步专用）
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));

            ServerInfo info = new ServerInfo(url, port, protocol);
            try
            {
                //Console.WriteLine($"请求{info.Url}");
                WebRequest request = WebRequest.Create(info.FullUrl);
                request.Method = "HEAD"; 
                if (timeout > 0)
                {
                    request.Timeout = timeout * 1000; // 超时时间（毫秒）
                }
             
                // 2. 启动异步请求 + 超时等待
                var responseTask = request.GetResponseAsync();
                var completedTask = await Task.WhenAny(responseTask, Task.Delay(Timeout.Infinite, cts.Token));

                // 3. 判断是否超时
                if (completedTask != responseTask)
                {
                    request.Abort(); // 主动终止请求
                    //stopwatch.Stop();
                    //Console.WriteLine($"请求{url}超时！耗时：{stopwatch.ElapsedMilliseconds}ms");
                    return null;
                }

                // 4. 处理正常响应
                using (WebResponse response = await responseTask)
                {
                    var httpResponse = (HttpWebResponse)response;
                    info.StatusCode = httpResponse.StatusCode;
                    info.Server = httpResponse.Headers["Server"];
                    //stopwatch.Stop();
                    //Console.WriteLine($"请求{url}成功！状态码:{(int)info.StatusCode},Server:{info.Server}，耗时：{stopwatch.ElapsedMilliseconds}ms");
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var httpResponse = (HttpWebResponse)ex.Response;
                    info.StatusCode = httpResponse.StatusCode;
                    info.Server = httpResponse.Headers["Server"];
                    //stopwatch.Stop();
                    //Console.WriteLine($"请求{url}成功！状态码:{(int)info.StatusCode},Server:{info.Server}，耗时：{stopwatch.ElapsedMilliseconds}ms");
                }
                //else
                //{
                //    stopwatch.Stop();
                //    Console.WriteLine($"请求{url}异常！耗时：{stopwatch.ElapsedMilliseconds}ms");
                //}
            }
            catch (Exception ex)
            {
                //stopwatch.Stop();
                //Console.WriteLine($"请求{url}失败！耗时：{stopwatch.ElapsedMilliseconds}ms: {ex.Message}");
                return null;
            }
            finally
            {
                cts.Dispose();
            }
            return info;
        }
    }
}
