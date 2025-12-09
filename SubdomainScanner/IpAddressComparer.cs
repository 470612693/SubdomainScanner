using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SubdomainScanner
{
    public class IpAddressComparer : IComparer<IPAddress>
    {
        public int Compare(IPAddress x, IPAddress y)
        {
            if (x == null || y == null) return 0; // 处理空值

            // 按地址族分组：IPv4优先于IPv6
            if (x.AddressFamily != y.AddressFamily)
            {
                return x.AddressFamily.CompareTo(y.AddressFamily);
            }

            // 获取字节数组进行比较
            byte[] xBytes = x.GetAddressBytes();
            byte[] yBytes = y.GetAddressBytes();

            // 逐字节比较
            for (int i = 0; i < Math.Min(xBytes.Length, yBytes.Length); i++)
            {
                int result = xBytes[i].CompareTo(yBytes[i]);
                if (result != 0) return result;
            }

            // 长度不同时处理（IPv4 vs IPv6）
            return xBytes.Length.CompareTo(yBytes.Length);
        }
    }
}
