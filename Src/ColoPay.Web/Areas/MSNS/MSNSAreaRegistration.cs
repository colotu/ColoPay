using System.Web.Mvc;
using YSWL.Web;

namespace YSWL.MALL.Web.Areas.MSNS
{
    public class MSNSAreaRegistration : AreaRegistrationBase
    {
        public MSNSAreaRegistration() : base(AreaRoute.MSNS) { }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MSNS_default",
                "MSNS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


            #region 注册Mobile区域子路由 覆盖父类注册方法

            //如当前为主路由 - 区域注册不执行
            if (MvcApplication.MainAreaRoute != CurrentArea || IsRegisterArea)
            {
                context.MapRoute(
                    name: CurrentRouteName + "Base",
                    url: CurrentRoutePath + "{controller}/{action}/{id}",
                    defaults:
                        new
                        {
                            controller = "Home",
                            action = "Index",
                            id = UrlParameter.Optional
                        }
                    ,
                    constraints: new
                    {
                        id = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    }
                    , namespaces: new string[] { string.Format("YSWL.MALL.Web.Areas.{0}.Controllers", AreaName) }
                    );

                context.MapRoute(
                    name: CurrentRouteName + CustomAreaName,
                    url: CurrentRoutePath + "{controller}/{action}/{viewname}/{id}",
                    defaults:
                        new
                        {
                            controller = "Home",
                            action = "Index",
                            viewname = UrlParameter.Optional,
                            id = UrlParameter.Optional
                        }
                    ,
                    constraints: new
                    {
                        viewname = @"[\w]{0,50}", //大小写字母/数字/下划线
                        id = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                    }
                    , namespaces: new string[] { string.Format("YSWL.MALL.Web.Areas.{0}.Controllers", AreaName) }
                    );
            }
            #endregion
        }
    }
}
