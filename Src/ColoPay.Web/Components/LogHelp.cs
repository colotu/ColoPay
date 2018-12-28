using System.Web;

namespace ColoPay.Web
{
    /// <summary>
    /// 系统日志类
    /// </summary>
    public static class LogHelp
    {
        /// <summary>
        /// Add User oprate log
        /// </summary>      
        public static void AddUserLog(string Username, string UserType, string OPInfo, HttpRequest request)
        {
            ColoPay.Model.SysManage.UserLog model = new ColoPay.Model.SysManage.UserLog();
            model.OPInfo = OPInfo;
            model.Url = request.Url.AbsoluteUri;
            //获取的是局域网分配的IP
            //string strHostName = System.Net.Dns.GetHostName();
            //string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(3).ToString();
            //model.UserIP = clientIPAddress;
            //本地测试获取的IP为127.0.0.1
            model.UserIP = request.UserHostAddress;
            model.UserName = Username;
            model.UserType = UserType;
            ColoPay.BLL.SysManage.UserLog.LogUserAdd(model);
        }
        public static void AddUserLog(string Username, string UserType, string OPInfo)
        {
            ColoPay.Model.SysManage.UserLog model = new ColoPay.Model.SysManage.UserLog();
            model.OPInfo = OPInfo;
            model.Url = "";
            //获取的是局域网分配的IP
            //string strHostName = System.Net.Dns.GetHostName();
            //string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(3).ToString();
            //model.UserIP = clientIPAddress;
            //本地测试获取的IP为127.0.0.1
            model.UserIP = "";
            model.UserName = Username;
            model.UserType = UserType;
            ColoPay.BLL.SysManage.UserLog.LogUserAdd(model);
        }
        /// <summary>
        /// Add User oprate log
        /// </summary>      
        public static void AddUserLog(string Username, string UserType, string OPInfo, System.Web.UI.Page page)
        {
            ColoPay.Model.SysManage.UserLog model = new ColoPay.Model.SysManage.UserLog();
            model.OPInfo = OPInfo;
            model.Url = page.Request.Url.AbsoluteUri;
            //获取的是局域网分配的IP
            //string strHostName = System.Net.Dns.GetHostName();
            //string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(3).ToString();
            //model.UserIP = clientIPAddress;
            //本地测试获取的IP为127.0.0.1
            model.UserIP = page.Request.UserHostAddress;
            model.UserName = Username;
            model.UserType = UserType;
            ColoPay.BLL.SysManage.UserLog.LogUserAdd(model);
        }

        /// <summary>
        /// Add system error log
        /// </summary>      
        public static void AddErrorLog(string Loginfo, string StackTrace)
        {
            ColoPay.Model.SysManage.ErrorLog model = new ColoPay.Model.SysManage.ErrorLog();
            model.Loginfo = Loginfo;
            model.StackTrace = StackTrace;
            if (HttpContext.Current != null)
            {
                model.Url = HttpContext.Current.Request.Url.AbsoluteUri;
            }
            else
            {
                model.Url = string.Empty;
            }
            ColoPay.BLL.SysManage.ErrorLog.Add(model);
        }
        /// <summary>
        /// Add system error log
        /// </summary>      
        public static void AddErrorLog(string Loginfo, string StackTrace, HttpRequest request)
        {
            ColoPay.Model.SysManage.ErrorLog model = new ColoPay.Model.SysManage.ErrorLog();
            model.Loginfo = Loginfo;
            model.StackTrace = StackTrace;
            model.Url = request.Url.AbsoluteUri;
            ColoPay.BLL.SysManage.ErrorLog.Add(model);
        }
        /// <summary>
        /// Add system error log
        /// </summary>      
        public static void AddErrorLog(string Loginfo, string StackTrace, System.Web.UI.Page page)
        {
            ColoPay.Model.SysManage.ErrorLog model = new ColoPay.Model.SysManage.ErrorLog();
            model.Loginfo = Loginfo;
            model.StackTrace = StackTrace;
            model.Url = page.Request.Url.AbsoluteUri;
            ColoPay.BLL.SysManage.ErrorLog.Add(model);
        }
        public static void AddErrorLog(string Loginfo, string StackTrace, string ClassName)
        {
            ColoPay.Model.SysManage.ErrorLog model = new ColoPay.Model.SysManage.ErrorLog();
            model.Loginfo = Loginfo;
            model.StackTrace = "";
            model.Url = ClassName;
            ColoPay.BLL.SysManage.ErrorLog.Add(model);
        }

        /// <summary>
        /// 新增入侵日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="request"></param>
        public static void AddInvadeLog(string msg, HttpRequest request)
        {
            ColoPay.Model.SysManage.ErrorLog model = new ColoPay.Model.SysManage.ErrorLog();
            model.Loginfo = string.Format("入侵拦截:[{0}] IP:[{1}]", msg, request.UserHostAddress);
            model.StackTrace = string.Empty;
            model.Url = request.Url.AbsoluteUri;
            ColoPay.BLL.SysManage.ErrorLog.Add(model);
        }
    }
}
