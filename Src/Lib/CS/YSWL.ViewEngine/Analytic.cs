/**
* Authorize.cs
*
* 功 能： 授权
* 类 名： Authorize
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 21:56:59  Ben    初版
*
* Copyright (c) 2013 YSWL Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：云商未来（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.Mvc;
using YSWL.Components;
using YSWL.Web;

namespace YSWL.ViewEngine
{
    internal static class Analytic
    {
        private static readonly string DATA = YSWL.Common.DEncrypt.Hex16.Decode(
                "0050006F007700650072006500640020006200790020003C006100200068007200650066003D00220068007400740070003A002F002F007700770077002E0079007300350036002E0063006F006D002F00220020007400610072006700650074003D0022005F0062006C0061006E006B00220020007300740079006C0065003D00220063006F006C006F0072003A00200023003300330033003B0022003E007B0034007D003C002F0061003E0020007B0030007D002000A900200032003000310031002D007B0033007D00200059005300350036002C00200049006E0063002E");

        private static readonly string DATA2 =
            YSWL.Common.DEncrypt.Hex16.Decode(
                "003C006100200068007200650066003D00220068007400740070003A002F002F007700770077002E0079007300350036002E0063006F006D00220020007400610072006700650074003D0022005F0062006C0061006E006B00220020007300740079006C0065003D00220063006F006C006F0072003A00230038003500380035003800350022003E4E915546672A676563D04F9B6280672F652F6301003C002F0061003E");


        internal static void CreateBegin(ControllerContext controllerContext, AreaRoute areaRoute)
        {
            string action = controllerContext.RouteData.Values["action"] as string;
            switch (action)
            {
                case "Footer":
                    switch (areaRoute)
                    {
                        case AreaRoute.SNS:
                        case AreaRoute.CMS:
                            controllerContext.HttpContext.Response.Output.WriteLine("<div id='footer' class='footer'>");
                            break;
                        case AreaRoute.Shop:
                            controllerContext.HttpContext.Response.Output.WriteLine("<div id='ft' >");
                            break;
                        case AreaRoute.MShop:
                            controllerContext.HttpContext.Response.Output.WriteLine("<div id='footer' class='footer'>");
                            break;
                    }
                    break;
            }
        }

        private static string _productInfo;
        internal static void CreateEnd(ControllerContext controllerContext, AreaRoute areaRoute)
        {
            string action = controllerContext.RouteData.Values["action"] as string;
            switch (action)
            {
                case "Footer":
                    if (string.IsNullOrEmpty(_productInfo))
                    {
                        _productInfo = MvcApplication.ProductInfo;
                    }
                    switch (areaRoute)
                    {
                        case AreaRoute.CMS:
                            controllerContext.HttpContext.Response.Output.WriteLine(
                                     "<div class=\"bot_menu\">{0}<br/>{1}</div>",
                                     (MvcApplication.IsAuthorize)
                                         ? string.Format("{0} {1}", MvcApplication.WebPowerBy, MvcApplication.WebRecord)
                                         : string.Format(
                                             "<div class=\"bot_menu\" >" + DATA + "</div><div class=\"bot_menu\"> {1} {2}</div>",
                                             MvcApplication.Version, MvcApplication.WebPowerBy, MvcApplication.WebRecord, DateTime.Now.Year,
                                             _productInfo), MvcApplication.PageFootJs);
                            break;
                        case AreaRoute.SNS:
                            controllerContext.HttpContext.Response.Output.WriteLine(
                                    "<div class='clear'></div><div class='footer_bot' style='margin-top: -23px;margin-bottom: 23px'>{0}<br/>{1}<div class='clear'></div></div></div>",
                                    (MvcApplication.IsAuthorize)
                                        ? string.Format("{0}<br/>{1}", MvcApplication.WebPowerBy, MvcApplication.WebRecord)
                                        : string.Format(
                                            "<div style=\"float: left;margin-left: 33px;text-align: left;\">" + DATA + "</div><div style=\"float: right;margin-right: 33px;text-align: right;\"> {1} <br /> {2}</div>",
                                            MvcApplication.Version + "<br/>", MvcApplication.WebPowerBy, MvcApplication.WebRecord, DateTime.Now.Year,
                                            _productInfo), MvcApplication.PageFootJs);
                            break;
                        case AreaRoute.Shop:
                            controllerContext.HttpContext.Response.Output.WriteLine(
                                    "<div class='copyright'>{0}<br/>{1}</div>",
                                    (MvcApplication.IsAuthorize)
                                        ? string.Format("<p> <span class='mr15'>{0}</span> <span>{1}</span></p>", MvcApplication.WebPowerBy, MvcApplication.WebRecord)
                                        : string.Format(
                                            "<p> <span class='mr15'>{1}</span> <span>{2}</span></p><p><span class='mr15'>" + DATA + "</span></p>",
                                            MvcApplication.Version, MvcApplication.WebPowerBy, MvcApplication.WebRecord, DateTime.Now.Year,
                                           _productInfo), MvcApplication.PageFootJs);
                            break;
                        case AreaRoute.MShop:
                            controllerContext.HttpContext.Response.Output.WriteLine(
                                "<div class='copyright'>{0}{1}</div>",
                                (MvcApplication.IsAuthorize)
                                    ? (string.IsNullOrWhiteSpace(MvcApplication.WebPowerBy)
                                        ? string.Empty
                                        : string.Format("<p> <span class='mr15'>{0}</span> </p>",
                                            MvcApplication.WebPowerBy))
                                    : (string.Format(
                                        "<p> <span class='mr15'>{0}</span> </p><p><span class='mr15'>" + DATA2 +
                                        "</span></p>",
                                        MvcApplication.WebPowerBy)), MvcApplication.PageFootJs);
                            break;
                    }
                    break;
            }
        }
    }
}
