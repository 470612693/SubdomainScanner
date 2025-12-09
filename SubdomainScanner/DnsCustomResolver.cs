using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubdomainScanner
{
    public class DnsCustomResolver
    {
        /// <summary>
        /// 空闲客户端列表
        /// </summary>
        private static ConcurrentBag<DnsQuery> listFree = new ConcurrentBag<DnsQuery>();

        private static List<int> listTransactionId = new List<int>();
        private static string DnsServer = string.Empty;
        /// <summary>
        /// 初始值
        /// </summary>
        private static int BaseTransactionId = 25535;
        /// <summary>
        /// 
        /// </summary>
        private static int InitCount { set; get; } = 100;
        private static object lockobj = new object();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dnsServer"></param>
        public static void SetDnsServer(string dnsServer,int initCount)
        {
            DnsServer = dnsServer;
            Console.WriteLine($"初始DNS服务器: {dnsServer}");
            listFree = new ConcurrentBag<DnsQuery>();
            listTransactionId = new List<int>();
            BaseTransactionId = 25535;
            InitCount = initCount;
            Init(InitCount);
        }
        /// <summary>
        /// 初始化指定数量的dns对象
        /// </summary>
        /// <param name="count"></param>
        private static void Init(int count)
        {
            for (int i = 0; i < count; i++)
            {
                DnsQuery query = InitDnsQuery();
                if (query != null)
                {
                    listTransactionId.Add(query.TransactionId);
                    listFree.Add(query);
                }
                //else
                //{
                //    Console.WriteLine($"初始化DNS客户端异常");
                //}
            }
        }
        private static DnsQuery InitDnsQuery()
        {
            try
            {
                DnsQuery query = new DnsQuery();
                int transactionId = BaseTransactionId++;
                if (listTransactionId.Contains(transactionId))
                {
                    Console.WriteLine($"TransactionId：{transactionId}已经存在");
                    return null;
                }
                query.TransactionId = transactionId;
                query.Client = new UdpClient(DnsServer, 53);
                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取客户端异常：" + ex);
                return null;
            }
        }
        private static DnsQuery GetUdpClient()
        {
            try
            {
                lock (lockobj)
                {
                    DnsQuery query;
                    if (listFree.TryTake(out query))
                    {
                        return query;
                    }
                    else
                    {

                        Init(InitCount);
                        if (listFree.TryTake(out query))
                        {
                            return query;
                        }
                        else
                        {
                            Console.WriteLine("获取客户端异常!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取客户端异常：" + ex);
            }
            return null;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Release()
        {
            try
            {
                if (listFree.Count != listTransactionId.Count)
                {
                    Console.WriteLine($"等待dns资源使用完成,listTransactionId:{listTransactionId.Count},listFree:{listFree.Count}");
                    Thread.Sleep(500);
                }
                //释放
                Console.WriteLine($"释放dns资源对象:{listFree.Count}个");
                DnsQuery query;
                while (listFree.TryTake(out query))
                {
                    try
                    {
                        query.Client.Close();
                        query.Client.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"客户端释放失败: {ex.Message}");
                    }
                }
                listTransactionId.Clear();
                listTransactionId = null;
                listFree = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"释放资源失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 异步获取DNS解析结果
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static async Task<IPAddress[]> ResolveWithDnsAsync(string domain, int timeout = 2)
        {
            DnsQuery query = GetUdpClient();
            // 1. 创建超时控制源（异步专用）
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            try
            {
                if (query != null)
                {
                    byte[] queryData = GetDnsQuery(domain, query.TransactionId);
                    await query.Client.SendAsync(queryData, queryData.Length);

                    var receiveTask = query.Client.ReceiveAsync();
                    var completedTask = await Task.WhenAny(receiveTask, Task.Delay(Timeout.Infinite, cts.Token));

                    // 3. 判断是否超时
                    if (completedTask != receiveTask)
                    {
                        return null;
                    }
                    UdpReceiveResult result = await receiveTask;
                    return ParseDnsResponse(result.Buffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"解析失败: {ex.Message}");
            }
            finally
            {
                cts.Dispose();
                listFree.Add(query);
            }
            return null;
        }
        private static byte[] GetDnsQuery(string domain, int transactionId = 0)
        {
            byte[] query = new byte[512]; // DNS报文最大512字节
            int offset = 0;

            // 事务ID（随机生成）
            Random rand = new Random();
            if (transactionId == 0)
            {
                transactionId = rand.Next(0, 65535);
            }
            //Console.WriteLine($"transactionId：{transactionId}");
            query[0] = (byte)(transactionId >> 8);
            query[1] = (byte)transactionId;
            //byte QR = 0;//1 bit操作类型：0：查询报文            1：响应报文
            //byte OPCODE = 0;//4 bit查询类型：0：标准查询1：反向查询2：服务器状态查询3～15：保留未用
            //byte AA = 0;//1 bit//若置位，则表示该域名解析服务器是授权回答该域的。
            //byte TC = 0;//1 bit//若置位，则表示报文被截断。使用UDP传输时，应答的总长度超过512字节时，只返回报文的前512个字节内容。
            //byte RD = 0;//1 bit//客户端希望域名解析服务器采取的解析方式：            0：表示希望域名解析服务器采取迭代解析1：表示希望域名解析服务器采取递归解析
            //byte RA = 0;//1 bit//域名解析服务器采取的解析方式：0：表示域名解析服务器采取迭代解析1：表示域名解析服务器采取递归解析
            //byte Z = 0;//3 bit//全部置0，保留未用。
            //byte RCODE = 0;//4 bit//响应类型：0：无差错1：查询格式错2：服务器失效3：域名不存在4：查询没有被执行5：查询被拒绝6-15: 保留未用
            // 标志位：标准查询（0x0100）
            query[2] = 0x01; // QR=0（查询），Opcode=0（标准查询）
            query[3] = 0x00;//query[3] = 0x80; // AA=0，TC=0，RD=1（递归查询）

            // 问题数=1，回答数=0，授权数=0，附加数=0
            query[4] = 0x00; query[5] = 0x01; // 问题数
            query[6] = 0x00; query[7] = 0x00; // 回答数
            query[8] = 0x00; query[9] = 0x00; // 授权数
            query[10] = 0x00; query[11] = 0x01; // 附加数

            // 域名解析（如"www.example.com" -> [3]www[7]example[3]com[0]）
            string[] parts = domain.Split('.');
            offset = 12;
            foreach (string part in parts)
            {
                if (part.Length > 63) throw new ArgumentException("域名部分过长");
                query[offset++] = (byte)part.Length;
                foreach (byte b in Encoding.ASCII.GetBytes(part))
                    query[offset++] = b;
            }
            query[offset++] = 0x00; // 域名结束
            int queryType = 1;
            // 查询类型（A记录=1，MX=15）
            query[offset++] = 0x00; query[offset++] = (byte)queryType;
            // 查询类（IN=1）
            query[offset++] = 0x00; query[offset++] = 0x01;
            //附加数据
            query[offset++] = 0x00;//Name
            query[offset++] = 0x00; query[offset++] = 0x29; // Type
            query[offset++] = 0x10; query[offset++] = 0x00; //UDP payload size: 4096
            query[offset++] = 0x00;//Higher bits in extended RCODE: 0x00
            query[offset++] = 0x00;//EDNS0 version: 0
            query[offset++] = 0x00; query[offset++] = 0x00;//Z: 0x0000
            query[offset++] = 0x00; query[offset++] = 0x00;//Data length: 0
            // 截取有效数据
            Array.Resize(ref query, offset);
            ////Console.WriteLine($"发送数据：{BytesToHex(query)}");
            return query;
        }
        public static string BytesToHex(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return string.Concat(bytes.Select(b => b.ToString("X2")));
        }

        public static byte[] HexStringToByteArray(string hexString)
        {
            // 校验输入有效性
            if (string.IsNullOrEmpty(hexString))
                throw new ArgumentException("十六进制字符串不能为空");
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("十六进制字符串长度必须为偶数");
            if (!System.Text.RegularExpressions.Regex.IsMatch(hexString, @"^[0-9A-Fa-f]+$"))
                throw new ArgumentException("包含非十六进制字符");

            // 核心转换逻辑
            return Enumerable.Range(0, hexString.Length / 2)
                .Select(i => Convert.ToByte(hexString.Substring(i * 2, 2), 16))
                .ToArray();
        }
        public static IPAddress[] ParseDnsResponse(byte[] response)
        {
            if (response.Length < 12) throw new ArgumentException("响应报文过短");

            // 解析头部
            ushort flags = ToUInt16(response, 2);
            if ((flags & 0x8000) == 0) throw new Exception("非响应报文");
            ushort rcode = (ushort)(flags & 0x0F);
            if (rcode != 0) throw new Exception($"DNS错误，代码：{rcode}");

            // 跳过问题部分
            int offset = 12;
            ushort qdCount = ToUInt16(response, 4);
            for (int i = 0; i < qdCount; i++)
            {
                offset = ParseDomain(response, offset, out string name);
                offset += 4; // 跳过类型和类
            }

            // 解析回答部分
            ushort anCount = ToUInt16(response, 6);
            List<IPAddress> result = new List<IPAddress>();
            for (int i = 0; i < anCount; i++)
            {
                ParseDomain(response, offset, out string name);
                //ushort cname = ToUInt16(response, offset);
                offset += 2;
                ushort type = ToUInt16(response, offset); offset += 2;
                ushort rclass = ToUInt16(response, offset); offset += 2;
                uint ttl = ToUInt32(response, offset); offset += 4;
                ushort rdlength = ToUInt16(response, offset); offset += 2;
                var res = ParseRData(name, type, response, offset, rdlength);
                if (res != null)
                {
                    result.Add(res);
                }
                offset += rdlength;
            }
            return result.ToArray();
        }
        private static UInt16 ToUInt16(byte[] bytes, int index)
        {
            byte[] data = new byte[2];
            Buffer.BlockCopy(bytes, index, data, 0, data.Length);
            return BitConverter.ToUInt16(data.Reverse().ToArray(), 0);
        }
        private static UInt32 ToUInt32(byte[] bytes, int index)
        {
            byte[] data = new byte[4];
            Buffer.BlockCopy(bytes, index, data, 0, data.Length);
            return BitConverter.ToUInt16(data.Reverse().ToArray(), 0);
        }
        private static IPAddress ParseRData(string name, ushort type, byte[] response, int offset, ushort rdlength)
        {
            string domain;
            switch (type)
            {
                case 1: // A
                    if (rdlength == 4)
                    {
                        byte[] rdata = new byte[rdlength];
                        Array.Copy(response, offset, rdata, 0, rdlength);
                        var ip = new IPAddress(rdata);
                        //Console.WriteLine($"{name} → {ip}");
                        return ip;
                    }
                    break;
                case 5: // CNAME
                    var cname = ParseDomain(response, offset, out domain);
                    //Console.WriteLine($"{name} → CNAME {domain}");
                    break;
                case 15: // MX
                    ushort priority = BitConverter.ToUInt16(response, 0);
                    var exchange = ParseDomain(response, 2, out domain);
                    //Console.WriteLine($"{name} → MX 优先级{priority}, 域名{exchange}");
                    break;
            }
            return null;
        }

        private static int ParseDomain(byte[] data, int offset, out string domain)
        {
            domain = "";
            int start = offset;
            while (true)
            {
                if (offset >= data.Length) throw new Exception("域名解析越界");
                byte length = data[offset++];
                if ((length & 0xC0) == 0)
                {
                    if (length == 0)
                    {
                        break;
                    }
                    if (!string.IsNullOrEmpty(domain))
                    {
                        domain += ".";
                    }
                    domain += Encoding.ASCII.GetString(data, offset, length);
                    offset += length;
                }
                else
                {
                    int pointer = data[offset++];
                    int savedOffset = offset;
                    string name2;
                    ParseDomain(data, pointer, out name2);
                    if (!string.IsNullOrEmpty(domain))
                    {
                        domain += ".";
                    }
                    domain += name2;
                    break;
                }
            }
            return offset;
        }
    }

    public class DnsQuery
    {
        public int TransactionId { set; get; }
        public UdpClient Client { set; get; }
    }
}