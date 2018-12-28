using System;
using System.Web.Mvc;
using YSWL.MALL.Model.SysManage;

namespace YSWL.MALL.Web.Areas.MSNS.Controllers
{
    /// <summary>
    /// MSNS网站前台基类
    /// </summary>

    public class MSNSControllerBase : YSWL.MALL.Web.Controllers.ControllerBase
    {

        #region UserName
        public string UserOpen
        {
            get
            {
                if (Session["WeChat_UserName"] != null)
                {
                    return Session["WeChat_UserName"].ToString();
                }
                return String.Empty;
            }
        }
        #endregion

        #region  OpenId
        public string OpenId
        {
            get
            {
                if (Session["WeChat_OpenId"] != null)
                {
                    return Session["WeChat_OpenId"].ToString();
                }
                return String.Empty;
            }
        }
        #endregion

        //TODO: 性能损耗警告,每次访问页面都加载了以下数据 BEN ADD 2013-03-12
        public int FallDataSize = Common.Globals.SafeInt(YSWL.MALL.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallDataSize", ApplicationKeyType.SNS), 20);
        public int PostDataSize = Common.Globals.SafeInt(YSWL.MALL.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS), 15);
        public int CommentDataSize = Common.Globals.SafeInt(YSWL.MALL.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS), 5);
        public int FallInitDataSize = Common.Globals.SafeInt(YSWL.MALL.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS), 5);
        public int GroupId
        {
            get
            {
                return BLL.SysManage.ConfigSystem.GetIntValueByCache("V_SNS_GroupId");
            }
        }
        private readonly YSWL.MALL.BLL.SNS.TaoBaoConfig _taoBaoConfig = new YSWL.MALL.BLL.SNS.TaoBaoConfig(ApplicationKeyType.OpenAPI);

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (MvcApplication.IsInstall && !filterContext.IsChildAction)
            {
                ViewBag.TaoBaoAppkey = _taoBaoConfig.TaoBaoAppkey;
            }
            base.OnResultExecuting(filterContext);
        }
    }
}
