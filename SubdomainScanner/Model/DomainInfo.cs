using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubdomainScanner
{
    public class DomainInfo
    {
        public string Child { set; get; }
        public int Index { set; get; }
        public string Domain { set; get; }
        public string Ip { set; get; }
        public string Port { set; get; } = "-";
        public string WebServer
        {
            get
            {
                if (ServerInfos != null)
                {
                    if (ServerInfos.Count == 0)
                    {
                        return "端口未开放";
                    }
                    ServerInfo temp = ServerInfos.OrderBy(o => o.Port).FirstOrDefault(f => f.IsOK);
                    if (temp != null)
                    {
                        return temp.Server ?? "";
                    }
                    temp = ServerInfos.OrderBy(o => o.Port).First();
                    if (temp.StatusCode == null)
                    {
                        return "获取失败";
                    }
                    return temp.Server ?? "-";
                }
                return "-";
            }
        }
        public string WebStatus
        {
            get
            {
                if (ServerInfos != null)
                {
                    if (ServerInfos.Count == 0)
                    {
                        return "端口未开放";
                    }
                    ServerInfo temp = ServerInfos.OrderBy(o => o.Port).FirstOrDefault(f => f.IsOK);
                    if (temp != null)
                    {
                        return temp.Description;
                    }
                    return ServerInfos.OrderBy(o => o.Port).First().Description;
                }
                return "-";
            }
        }
        public string Source { set; get; } = "暴力枚举";
        public string Url
        {
            get
            {
                if (ServerInfos?.Count > 0)
                {
                    return ServerInfos.FirstOrDefault().FullUrl;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 服务详情
        /// </summary>
        public List<ServerInfo> ServerInfos { set; get; } = null;
        public DomainInfo()
        {

        }
        public DomainInfo(string child, int Index)
        {
            this.Child = child;
            this.Index = Index;
        }
        /// <summary>
        /// 根据服务设置端口
        /// </summary>
        public void SetPortByServerInfos()
        {
            if (ServerInfos != null && ServerInfos.Count > 0)
            {
                Port = string.Join(",", ServerInfos.Select(s => s.Port).Distinct().ToList());
            }
            else
            {
                Port = "-";
            }
        }
    }
}
