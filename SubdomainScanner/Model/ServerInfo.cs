using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SubdomainScanner
{
    public class ServerInfo
    {
        public string Url { set; get; }
        public int Port { set; get; }

        public string FullUrl
        {
            get
            {
                return string.Format("{0}://{1}:{2}", Protocol, Url, Port);
            }
        }

        public string Server { set; get; }

        public NetProtocol Protocol { set; get; }

        public HttpStatusCode? StatusCode { set; get; } = null;

        public bool IsOK
        {
            get { return StatusCode == HttpStatusCode.OK; }
        }

        public string Description
        {
            get
            {
                if (StatusCode == null)
                {
                    return string.Format("{0}:-", Port);
                }
                if (StatusCode == HttpStatusCode.OK)
                {
                    return string.Format("{0}:正常访问", Port);
                }
                if (dicStatusCode.ContainsKey(StatusCode.Value))
                {
                    return string.Format("{0}:({2}){1}", Port, dicStatusCode[StatusCode.Value], (int)StatusCode);
                }
                return string.Format("{0}:({2})status code{1}", Port, StatusCode.ToString(), (int)StatusCode);
            }
        }
        public ServerInfo()
        {

        }
        public ServerInfo(string url, string port, NetProtocol protocol)
        {
            Url = url;
            Port = Convert.ToInt32(port);
            Protocol = protocol;
        }
        public ServerInfo(string url, int port, NetProtocol protocol)
        {
            Url = url;
            Port = port;
            Protocol = protocol;
        }

        private readonly Dictionary<HttpStatusCode, string> dicStatusCode = new Dictionary<HttpStatusCode, string>
    {
        { HttpStatusCode.Continue, "继续" },
        { HttpStatusCode.SwitchingProtocols, "切换协议" },
        { HttpStatusCode.OK, "请求成功" },
        { HttpStatusCode.Created, "已创建" },
        { HttpStatusCode.Accepted, "已接受" },
        { HttpStatusCode.NonAuthoritativeInformation, "非权威信息" },
        { HttpStatusCode.NoContent, "无内容" },
        { HttpStatusCode.ResetContent, "重置内容" },
        { HttpStatusCode.PartialContent, "部分内容" },
        { HttpStatusCode.MultipleChoices, "多种选择" },
        { HttpStatusCode.MovedPermanently, "永久移动" },
        { HttpStatusCode.Found, "临时移动" },
        { HttpStatusCode.SeeOther, "查看其他位置" },
        { HttpStatusCode.NotModified, "未修改" },
        { HttpStatusCode.UseProxy, "使用代理" },
        { HttpStatusCode.TemporaryRedirect, "临时重定向" },
        { HttpStatusCode.BadRequest, "错误请求" },
        { HttpStatusCode.Unauthorized, "未授权" },
        { HttpStatusCode.Forbidden, "禁止访问" },
        { HttpStatusCode.NotFound, "未找到" },
        { HttpStatusCode.MethodNotAllowed, "方法不允许" },
        { HttpStatusCode.Conflict, "冲突" },
        { HttpStatusCode.Gone, "已删除" },
        { HttpStatusCode.InternalServerError, "服务器内部错误" },
        { HttpStatusCode.NotImplemented, "未实现" },
        { HttpStatusCode.BadGateway, "错误的网关" },
        { HttpStatusCode.ServiceUnavailable, "服务不可用" },
        { HttpStatusCode.GatewayTimeout, "网关超时" },
        // 包含所有标准状态码...
    };
    }
    public enum NetProtocol
    {
        http,
        https
    }
}
