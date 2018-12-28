using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YSWL.Common;
using YSWL.Components.Setting;
using ColoPay.Model.SysManage;
using Webdiyer.WebControls.Mvc;
using YSWL.Accounts.Bus;
using YSWL.Json;
using YSWL.Web;


namespace ColoPay.Web.Areas.Supplier.Controllers
{
    public class HomeController : SupplierControllerBase
    {
        //
        // GET: /Shop/Home/
        BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
        Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();

        private ColoPay.BLL.SysManage.SysTree sm = new ColoPay.BLL.SysManage.SysTree();

        public ActionResult Index()
        {
            return View();
        }
        #region 修改资料
        public ActionResult UserModify()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
                Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
                if (null != model)
                {
                    return View(model);
                }
            }
            return RedirectToAction("Login", "Account");//去登录
        }
        [HttpPost]
        public ActionResult UserModify(string txtName, string txtTrueName, string txtEmail)
        {
            AccountsPrincipal user = new AccountsPrincipal(txtName.Trim());
            YSWL.Accounts.Bus.User currentUser = new YSWL.Accounts.Bus.User(user);
            currentUser.UserName = txtName;
            currentUser.TrueName = txtTrueName.Trim();
            currentUser.Email = txtEmail.Trim();
            JsonObject json = new JsonObject();
            if (currentUser.Update())
            {
                json.Put("Result", "OK");
                return Json(json);
            }
            else
            {
                json.Put("Result", "NO");
                return Json(json);
            }
        }
        #endregion

        #region 修改密码
        public ActionResult UserPass()
        {
            YSWL.Accounts.Bus.User currentUser = this.CurrentUser;
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult UserPass(string oldPassword, string newPassword, string confirmPassword)
        {
            if (!HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");//去登录
            SiteIdentity SID = new SiteIdentity(User.Identity.Name);
            JsonObject json = new JsonObject();
            if (SID.TestPassword(oldPassword) == 0)
            {
                json.Put("Result", "Error");
                return Json(json);
            }
            else
            {
                if (newPassword.Trim() != confirmPassword.Trim())
                {
                    json.Put("Result", "ConfirmError");
                    return Json(json);
                }
                else
                {
                    YSWL.Accounts.Bus.User currentUser = CurrentUser;
                    if (!currentUser.SetPassword(CurrentUser.UserName, newPassword, MvcApplication.IsAutoConn))
                    {
                        json.Put("Result", "NO");
                        return Json(json);
                    }
                    else
                    {
                        json.Put("Result", "OK");
                        return Json(json);
                    }

                }
            }

        }
        #endregion

        #region 用户信息
        public ActionResult UserInfo()
        {
            //YSWL.Accounts.Bus.User currentUser = this.CurrentUser;
            BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
            Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);

            ViewBag.userIP = Request.UserHostAddress;
            return View(model);
        }
        #endregion



















        #region 获取商家分类名称
        /// <summary>
        /// 获取商家分类名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetEnteClassName(object target)
        {
            //合资、独资、国有、私营、全民所有制、集体所有制、股份制、有限责任制
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "合资";
                        break;
                    case "2":
                        str = "独资";
                        break;
                    case "3":
                        str = "国有";
                        break;
                    case "4":
                        str = "私营";
                        break;
                    case "5":
                        str = "全民所有制";
                        break;
                    case "6":
                        str = "集体所有制";
                        break;
                    case "7":
                        str = "股份制";
                        break;
                    case "8":
                        str = "有限责任制";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家性质
        /// <summary>
        /// 获取商家性质
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetCompanyType(object target)
        {
            //0:个体工商; 1:私营独资商家; 2:国营商家。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "个体工商";
                        break;
                    case "2":
                        str = "私营独资商家";
                        break;
                    case "3":
                        str = "国营商家";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家审核状态
        /// <summary>
        /// 获取商家审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            //0:未审核; 1:正常;2:冻结;3:删除
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "未审核";
                        break;
                    case "1":
                        str = "正常";
                        break;
                    case "2":
                        str = "冻结";
                        break;
                    case "3":
                        str = "删除";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家等级
        /// <summary>
        /// 获取商家等级
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetSuppRank(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "一星级";
                        break;
                    case "2":
                        str = "二星级";
                        break;
                    case "3":
                        str = "三星级";
                        break;
                    case "4":
                        str = "四星级";
                        break;
                    case "5":
                        str = "五星级";
                        break;
                    default:
                        str = "无";
                        break;
                }
            }
            return str;
        }
        #endregion


        #region 首页
        public ActionResult Main(string viewName = "Main")
        {
            ViewBag.userName = string.IsNullOrWhiteSpace(CurrentUser.TrueName) ? CurrentUser.UserName : CurrentUser.TrueName;
            uModel = uBll.GetUsersExpModel(CurrentUser.UserID);
            if (uModel != null)
            {
                ViewBag.lastLoginDate = uModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.lastLoginDate = CurrentUser.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            //剩余


            return View(viewName);
        }
        #endregion

        #region 头部
        public PartialViewResult Header(string viewName = "_Header")
        {
            return PartialView(viewName);
        }
        #endregion

        #region 导航
        public PartialViewResult Top(string viewName = "_Top")
        {
            ViewBag.UserName = string.IsNullOrWhiteSpace(CurrentUser.TrueName) ? CurrentUser.UserName : CurrentUser.TrueName;
            //0:admin后台 1:企业后台  2:代理商后台 3:用户后台 4商家后台
            List<ColoPay.Model.SysManage.SysNode> AllNodeList = sm.GetTreeListByTypeCache(4, true, MvcApplication.IsAutoConn);
            List<ColoPay.Model.SysManage.SysNode> FirstList = AllNodeList.Where(c => c.ParentID == 0).ToList();
            List<ColoPay.Model.SysManage.SysNode> NodeList = new List<SysNode>();
            foreach (var item in FirstList)
            {
                //判断权限
                if ((item.PermissionID == -1) || (UserPrincipal.HasPermissionID(item.PermissionID)))
                {
                    NodeList.Add(item);
                }
            }
            return PartialView(viewName, NodeList);
        }
        #endregion

        #region 左侧导航

        public PartialViewResult LeftMenu(int id, string viewName = "_LeftMenu")
        {
            List<ColoPay.Model.SysManage.SysNode> AllNodeList = sm.GetTreeListByTypeCache(4, true, MvcApplication.IsAutoConn);
            ColoPay.Model.SysManage.SysNode nodeModel = AllNodeList.FirstOrDefault(c => c.NodeID == id);

            ViewBag.NodeName = nodeModel == null ? "" : nodeModel.TreeText;
            List<ColoPay.Model.SysManage.SysNode> NodeList = AllNodeList.Where(c => c.ParentID == id).ToList();
            return PartialView(viewName, NodeList);
        }

        #endregion

        #region
        public PartialViewResult Swich(string viewName = "_Swich")
        {
            return PartialView(viewName);
        }
        #endregion



        #region 供应商菜单管理
        #region 菜单首页
        public ViewResult SupplierMenu()
        {
            return View();
        }
        #endregion

        #region 获取菜单列表
        public PartialViewResult LoadMenu(int pageIndex = 1, string viewName = "_MenuList")
        {
            return PartialView(viewName);
        }
        #endregion

        #region 添加菜单Get
        public ViewResult AddMenu()
        {
            return View();
        }
        #endregion










        #endregion

        #region 供应商广告管理

        



        #region 添加广告
        public ViewResult AddAds(int positionId = 0)
        {
            ViewBag.PositionId = positionId;
            return View();
        }

        #endregion

        #endregion

    }
}

