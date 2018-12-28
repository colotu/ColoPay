using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YSWL.Accounts.Bus;
using YSWL.MALL.BLL.SNS;
using YSWL.Common;
using YSWL.Components.Filters;
using YSWL.Components.Handlers.API;
using YSWL.Components.Setting;
using YSWL.Json;
using YSWL.MALL.Model.SysManage;
using YSWL.MALL.ViewModel.SNS;
using YSWL.MALL.Web.Areas.MSNS.Controllers;
using YSWL.MALL.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;

namespace YSWL.MALL.Web.Areas.MSNS.Controllers
{
    public class HomeController : MSNSControllerBase
    {
        private YSWL.MALL.BLL.SNS.Groups bllGroups = new YSWL.MALL.BLL.SNS.Groups();
        private YSWL.MALL.BLL.SNS.GroupTopics bllTopic = new GroupTopics();
        private YSWL.MALL.BLL.SNS.GroupUsers bllGroupUser = new GroupUsers();
        private YSWL.MALL.BLL.SNS.GroupTopicReply bllReply = new GroupTopicReply();

        #region 小组首页
        public ActionResult Index(int? page, string type, string q, int groupId =0)
        {
            YSWL.MALL.ViewModel.SNS.GroupInfo groupInfo = new MALL.ViewModel.SNS.GroupInfo();
            groupId=groupId > 0 ? groupId : GroupId;
            groupInfo.Group = bllGroups.GetModel(groupId);
            ViewBag.GroupId = groupId;
            ViewBag.GetType = type;
            if (null != groupInfo.Group)
            {
                ViewBag.IsCreator = (CurrentUser != null && groupInfo.Group.CreatedUserId == CurrentUser.UserID);
                //小组帖子数
                ViewBag.toalcount = groupInfo.Group.TopicCount;
            }
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 1;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = 0;
            switch (type)
            {
                case "Search":
                    //小组帖子搜索
                    //推荐帖子
                    q = Common.InjectionFilter.Filter(q);
                    toalcount = bllTopic.GetCountByKeyWord(q, groupId);
                    ViewBag.q = q;
                    ViewBag.toalcount = toalcount;
                    groupInfo.TopicList = new PagedList<YSWL.MALL.Model.SNS.GroupTopics>(
                        bllTopic.SearchTopicByKeyWord(startIndex, endIndex, q, groupId, "")
                        , page ?? 1, pagesize, toalcount);
                    if (Request.IsAjaxRequest())
                        return PartialView(CurrentThemeViewPath + "/Group/TopicList.cshtml", groupInfo.TopicList);
                    break;
                case "User":
                    //小组管理员
                    groupInfo.AdminUserList = bllGroupUser.GetAdminUserList(groupId);
                    if (groupInfo.AdminUserList.Count > 0 && groupInfo.AdminUserList[0].Role == 2)
                    {
                        BLL.Members.UsersExp bllUser = new BLL.Members.UsersExp();
                        groupInfo.AdminUserList[0].User = bllUser.GetUsersExpModelByCache(groupInfo.AdminUserList[0].UserID);
                    }
                    //小组成员总数 非管理员用户
                    toalcount = bllGroupUser.GetRecordCount("GroupId=" + groupId + " AND Role = 0");
                    //小组成员分页加载  非管理员用户
                    groupInfo.UserList = new PagedList<Model.SNS.GroupUsers>(
                        bllGroupUser.GetUserList(groupId, startIndex, endIndex)
                        , page ?? 1, pagesize, toalcount);
                    if (Request.IsAjaxRequest())
                        return PartialView(CurrentThemeViewPath + "/Group/UserList.cshtml", groupInfo);
                    break;
                case "Recommand":
                    //推荐帖子
                    toalcount = bllTopic.GetRecommandCount(groupId);
                    groupInfo.TopicList = new PagedList<YSWL.MALL.Model.SNS.GroupTopics>(
                        bllTopic.GetTopicListPageByGroup(groupId, startIndex, endIndex, true)
                        , page ?? 1, pagesize, toalcount);
                    if (Request.IsAjaxRequest())
                        return PartialView(CurrentThemeViewPath + "/Group/TopicList.cshtml", groupInfo.TopicList);
                    break;
                default:
                    //帖子
                    toalcount = bllTopic.GetRecordCount("  Status=1 and GroupID=" + groupId);
                    groupInfo.TopicList = new PagedList<YSWL.MALL.Model.SNS.GroupTopics>(
                        bllTopic.GetTopicListPageByGroup(groupId, startIndex, endIndex, false)
                        , page ?? 1, pagesize, toalcount);
                    ViewBag.page = (toalcount + pagesize - 1) / pagesize;
                    if (Request.IsAjaxRequest())
                        return PartialView("_ReplyList",groupInfo);
                    break;
            }
            groupInfo.NewTopicList = bllTopic.GetNewTopListByGroup(groupId, 10);
            groupInfo.NewUserList = bllGroupUser.GetNewUserListByGroup(groupId, 9);
            ViewBag.joined = (currentUser != null && bllGroupUser.Exists(groupId, currentUser.UserID)) ? "ok" : "error";

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("GroupList", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, null != groupInfo.Group?groupInfo.Group.GroupName:""},        //小组名称
                new[] { PageSetting.RKEY_CTAG, null != groupInfo.Group?groupInfo.Group.Tags:"" },              //小组标签
                new[] { PageSetting.RKEY_CDES, null != groupInfo.Group?groupInfo.Group.GroupDescription:"" }); //小组描述
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(groupInfo);
        }
        #endregion

        public JsonResult GetPageData(int page = 1)
        {
            JsonObject json = new JsonObject();
            YSWL.MALL.ViewModel.SNS.GroupInfo groupInfo = new MALL.ViewModel.SNS.GroupInfo();
            YSWL.MALL.BLL.SNS.GroupTopics groupTopicsBll = new GroupTopics();
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page > 1 ? (page - 1) * pagesize + 1 : 1;
            //计算分页结束索引
            int endIndex = page * pagesize;
            //总记录数
            int toalcount = 0;
            toalcount = bllTopic.GetRecordCount("  Status=1 and GroupID=" + GroupId);
            groupInfo.TopicList = new PagedList<YSWL.MALL.Model.SNS.GroupTopics>(
                bllTopic.GetTopicListPageByGroup(GroupId, startIndex, endIndex, false)
                , page, pagesize, toalcount);
            if (groupInfo.TopicList != null && groupInfo.TopicList.Count > 0)//有话题
            {
                YSWL.MALL.Model.SNS.GroupTopics groupTopics;
                JsonObject jsonObject;
                JsonObject reply;
                HttpCookie cookie;
                List<JsonObject> replyList=new List<JsonObject>();
                List<JsonObject> resultList=new List<JsonObject>();
                foreach (YSWL.MALL.Model.SNS.GroupTopics item in groupInfo.TopicList)//遍历话题列表
                {
                    groupTopics = groupTopicsBll.GetModelByCache(item.TopicID);
                    jsonObject=new JsonObject();
                    if (null != groupTopics)
                    {
                        if (null != CurrentUser)
                        {
                            cookie = Request.Cookies["topicFav_" + groupTopics.TopicID + CurrentUser.UserID];
                            if (null != cookie) //取消赞
                            {
                                jsonObject.Put("support", "support");
                            }
                            else
                            {
                                jsonObject.Put("support", "nosupport");
                            }
                        }
                        jsonObject.Put("uid", groupTopics.CreatedUserID);
                        jsonObject.Put("topicId", groupTopics.TopicID);
                        jsonObject.Put("nickName",groupTopics.CreatedNickName);
                        jsonObject.Put("createdDate", YSWL.MALL.Web.Components.DateTimeHelper.ConvertDateToTime(groupTopics.CreatedDate));
                        jsonObject.Put("description",Server.HtmlDecode(YSWL.MALL.ViewModel.ViewModelBase.ReplaceFace(groupTopics.Description)));
                        jsonObject.Put("imageUrl", YSWL.MALL.Web.Components.FileHelper.GeThumbImage(groupTopics.ImageUrl, "T180X120_"));
                        jsonObject.Put("ref", groupTopics.ImageUrl);
                        jsonObject.Put("favCount",groupTopics.FavCount);
                        List<YSWL.MALL.Model.SNS.GroupTopicReply> list = bllReply.GetTopicReplyByTopic(groupTopics.TopicID, startIndex, endIndex);
                        if (null != list && list.Count > 0)
                        {
                            foreach (YSWL.MALL.Model.SNS.GroupTopicReply topicReply in list)
                            {
                                reply = new JsonObject();
                                reply.Put("replynickName", topicReply.ReplyNickName);
                                reply.Put("replyContent", Server.HtmlDecode(YSWL.MALL.ViewModel.ViewModelBase.ReplaceFace(topicReply.Description)));
                                reply.Put("replyCount", list.Count);
                                replyList.Add(reply);
                            }
                            jsonObject.Put("replyList", replyList); 
                        }
                    }
                   resultList.Add(jsonObject);
                }
               // return new Result(ResultStatus.Success,resultList);
                json.Put("status", "success");
                json.Put("result", resultList);
                return Json(json);
            }
            return null;
        }

        #region 话题回复列表
        public PartialViewResult TopicReply(int Id, int? page, string viewName = "TopicReply", string ajaxvName = "TopicReplyList")
        {
            //if (!bllTopic.Exists(Id) || !bllTopic.UpdatePVCount(Id))
            //{
            //    return RedirectToAction("Index", "Group");
            //
            ViewBag.support = "nosupport";
            if (null != CurrentUser)
            {
                HttpCookie cookie = Request.Cookies["topicFav_" + Id + CurrentUser.UserID];
                if (null != cookie) //取消赞
                {
                    ViewBag.support = "support";
                }
            }
            YSWL.MALL.ViewModel.SNS.TopicReply Model = new TopicReply();
            Model.Topic = bllTopic.GetModel(Id);
            ViewBag.Activity = "invalid";
            if (null != Model.Topic && null != CurrentUser)
            {
                if (Model.Topic.CreatedUserID == CurrentUser.UserID) //自己的帖子
                {
                    ViewBag.Activity = "active";
                }
            }
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            ViewBag.page = page+1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 1;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = bllReply.GetRecordCount(" Status=1 and TopicID =" + Id);
            List<YSWL.MALL.Model.SNS.GroupTopicReply> list = bllReply.GetTopicReplyByTopic(Id, startIndex, endIndex);
            if (list != null && list.Count > 0)
            {
                Model.TopicsReply = new PagedList<YSWL.MALL.Model.SNS.GroupTopicReply>(list, page ?? 1, pagesize, toalcount);
            }
            if (Request.IsAjaxRequest())
                return PartialView(ajaxvName, Model);
            Model.UserJoinGroups = bllGroups.GetUserJoinGroup(Model.Topic != null ? Model.Topic.CreatedUserID : 0, 9);
            Model.HotTopic = bllTopic.GetHotListByGroup(Model.Topic.GroupID, 9);
            Model.Group = bllGroups.GetModel(Model.Topic.GroupID);
            Model.UserPostTopics = bllTopic.GetTopicByUserId(Model.Topic.CreatedUserID, 9);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("GroupDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, Model.Group.GroupName },    //小组名称
                new[] { PageSetting.RKEY_CTNAME, Model.Topic.Title },       //帖子标题
                new[] { PageSetting.RKEY_CTAG, Model.Topic.Tags },          //帖子标签
                new[] { PageSetting.RKEY_CDES, Model.Topic.Description });  //帖子内容
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return PartialView(viewName, Model);
        } 
        #endregion

        #region 创建新话题
        [TokenAuthorize]
        public ActionResult NewTopic(int GroupId)
        {
            ViewBag.Title = "发表主题";
            int subStringLength = BLL.SysManage.ConfigSystem.GetIntValueByCache("MSNS_GroupTopic_SubLength");
            ViewBag.ContentLength = subStringLength<1?10:subStringLength;
            YSWL.MALL.Model.SNS.Groups model = new Model.SNS.Groups();
            model = bllGroups.GetModel(GroupId);
            return View(model);
        } 
        #endregion

        #region 检测用户是加入小组
        public ActionResult AJaxCheckUserIsJoinGroup(FormCollection fm)
        {
            YSWL.MALL.Model.SNS.GroupUsers model = new Model.SNS.GroupUsers();
            YSWL.MALL.BLL.SNS.GroupUsers bll = new YSWL.MALL.BLL.SNS.GroupUsers();
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
            if (bll.Exists(GroupId, currentUser.UserID))
            {
                return Content("joined");
            }
            return Content("No");

        }
        #endregion

        #region  加入小组
        public ActionResult AjaxJoinGroup(FormCollection fm)
        {
            YSWL.MALL.Model.SNS.GroupUsers model = new Model.SNS.GroupUsers();
            YSWL.MALL.BLL.SNS.GroupUsers bll = new YSWL.MALL.BLL.SNS.GroupUsers();
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
           if (null == CurrentUser)
           {
               return Content("notLogin");
           }
            if (bll.Exists(GroupId, currentUser.UserID))
            {
                return Content("joined");
            }
            model.GroupID = GroupId;
            model.JoinTime = DateTime.Now;
            model.NickName = currentUser.NickName;
            model.UserID = currentUser.UserID;
            model.Status = 1;
            if (bll.AddEx(model))
            {
                return Content("Yes");
            }
            return Content("No");
        }
        #endregion

        #region ajax创建回复
        public ActionResult AJaxCreateTopicReply(FormCollection fm)
        {
            string Des = Common.InjectionFilter.Filter(fm["Des"] != null ? fm["Des"].ToString() : "");
            long Pid = Common.Globals.SafeLong(fm["Pid"], 0);
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
            int TopicId = Common.Globals.SafeInt(fm["TopicId"], 0);
            string ImageUrl = "";
            //商品和图片只能发一个
            if (Pid == 0)
            {
                ImageUrl = fm["ImageUrl"] != null ? fm["ImageUrl"].ToString() : "";
            }
            if (string.IsNullOrEmpty(Des))
            {
                return Content("No");
            }
            YSWL.MALL.BLL.SNS.GroupTopicReply bllTopicReply = new YSWL.MALL.BLL.SNS.GroupTopicReply();
            YSWL.MALL.Model.SNS.GroupTopicReply modelTopicReply = new Model.SNS.GroupTopicReply();
            modelTopicReply.Description = Des;
            //移动图片
            string savePath = "/Upload/SNS/Images/Group/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            string saveThumbsPath = "/Upload/SNS/Images/GroupThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            modelTopicReply.PhotoUrl = YSWL.MALL.BLL.SNS.Photos.MoveImage(ImageUrl, savePath, saveThumbsPath);
            modelTopicReply.GroupID = GroupId;
            modelTopicReply.CreatedDate = DateTime.Now;
            modelTopicReply.ReplyUserID = currentUser.UserID;
            modelTopicReply.ReplyNickName = currentUser.NickName;
            modelTopicReply.Status = 1;
            modelTopicReply.TopicID = TopicId;
            if ((modelTopicReply.ReplyID = bllTopicReply.AddEx(modelTopicReply, Pid)) > 0)
            {
                YSWL.MALL.BLL.SNS.GroupTopicReply bllReply = new YSWL.MALL.BLL.SNS.GroupTopicReply();
                List<YSWL.MALL.Model.SNS.GroupTopicReply> list = new List<Model.SNS.GroupTopicReply>();
                modelTopicReply = bllReply.GetModel(modelTopicReply.ReplyID);
                list.Add(modelTopicReply);
                return PartialView(CurrentThemeViewPath + "/Group/TopicReplyResult.cshtml", list);
            }
            return Content("No");
        } 
        #endregion

        #region 发表主题
        public ActionResult AJaxCreateTopic(FormCollection fm)
        {
            int subStringLength = BLL.SysManage.ConfigSystem.GetIntValueByCache("MSNS_GroupTopic_SubLength");
            subStringLength = subStringLength < 1 ? 15 : subStringLength;
            string Title = Common.StringPlus.SubString(fm["Des"],subStringLength);
            string Des = fm["Des"] ?? "";
            long Pid = Common.Globals.SafeLong(fm["Pid"], 0);
            string ImageUrl = "";
            //商品和图片只能发一个
            if (Pid == 0)
            {
                ImageUrl = fm["ImageUrl"] != null ? fm["ImageUrl"].ToString() : "";
            }
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Des))
            {
                return Content("No");
            }
            YSWL.MALL.BLL.SNS.GroupTopics bllTopic = new YSWL.MALL.BLL.SNS.GroupTopics();
            YSWL.MALL.Model.SNS.GroupTopics modelTopic = new Model.SNS.GroupTopics();
            modelTopic.Title = Title;
            modelTopic.Description = Des;
            //移动图片
            string savePath = "/Upload/SNS/Images/Group/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            string saveThumbsPath = "/Upload/SNS/Images/GroupThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            modelTopic.ImageUrl = YSWL.MALL.BLL.SNS.Photos.MoveImage(ImageUrl, savePath, saveThumbsPath);
            modelTopic.GroupID = GroupId;
            modelTopic.GroupName = "";
            modelTopic.CreatedDate = DateTime.Now;
            modelTopic.CreatedUserID = currentUser.UserID;
            modelTopic.CreatedNickName = currentUser.NickName;
            if ((modelTopic.TopicID = bllTopic.AddEx(modelTopic, Pid)) > 0)
            {
                return Content(modelTopic.TopicID.ToString());
            }
            //SNS连续发帖冻结帐号
            else if (modelTopic.TopicID == -2)
            {
                //已冻结帐号, 强制用户退出
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Remove(Globals.SESSIONKEY_USER);
                Session.Clear();
                Session.Abandon();
            }
            return Content("No");
        }
        #endregion

        #region 判断是否含有禁用词
        [ValidateInput(false)]
        public ActionResult ContainsDisWords()
        {
            string desc = Request.Params["Desc"];
            return YSWL.MALL.BLL.Settings.FilterWords.ContainsDisWords(desc) ? Content("True") : Content("False");
        }
        #endregion

        #region 检测当前用户是否登录
        public ActionResult CheckUserState()
        {
            if (currentUser != null)
            {
                return Content("Yes");
            }
            return Content("No");

        }

        public ActionResult CheckUserState4UserType()
        {
            if (currentUser != null)
            {
                return Content(currentUser.UserType == "AA" ? "Yes4AA" : "Yes");
            }
            return Content("No");

        }

        public ActionResult GetCurrentUser()
        {
            JsonObject jsonObject=new JsonObject();
            if (currentUser != null)
            {
                return Content(currentUser.UserID.ToString());
            }

            return Content("-1");
        }
        #endregion

        #region Ajax登录
        [HttpPost]
        public ActionResult AjaxLogin(string UserName, string UserPwd)
        {
            bool IsCloseLogin = YSWL.MALL.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            if (IsCloseLogin)
            {
                return Content("-1");
            }
            if (ModelState.IsValid)
            {
                AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(UserName, UserPwd);
                if (userPrincipal != null)
                {
                    User currentUser = new YSWL.Accounts.Bus.User(userPrincipal);
                    if (!currentUser.Activity)
                    {
                        ModelState.AddModelError("Message", "对不起，该帐号已被冻结，请联系管理员！");
                    }
                    //if (currentUser.UserType == "AA")
                    //{
                    //    ModelState.AddModelError("Message", "您是管理员用户，您没有权限登录后台系统！") ;                        
                    //}
                    HttpContext.User = userPrincipal;
                    FormsAuthentication.SetAuthCookie(UserName, true);
                    Session[YSWL.Common.Globals.SESSIONKEY_USER] = currentUser;
                    //登录成功加积分
                    YSWL.MALL.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                    int pointers = pointBll.AddPoints(1, currentUser.UserID, "登录操作");
                    int rankScore = BLL.Members.RankDetail.AddScore(1, currentUser.UserID, "登录操作");
                    return Content(string.Format("1|{0}|{1}", pointers, rankScore));
                }
                else
                {
                    return Content("0");
                }
            }
            return Content("0");
        } 
        #endregion

        public ActionResult TopicDetail(int id)
        {
            ViewBag.support = "nosupport";
            if (null != CurrentUser)
            {
                HttpCookie cookie = Request.Cookies["topicFav_" + id + CurrentUser.UserID];
                if (null != cookie) //取消赞
                {
                    ViewBag.support = "support";
                }
            }
            YSWL.MALL.Model.SNS.GroupTopics topicsModel = bllTopic.GetModelByCache(id);
                return View(topicsModel);
        }


        public ActionResult AddTopicFav(int topicId)
        {
            JsonObject jsonObject = new JsonObject();
            if (null == currentUser)
            {
                jsonObject.Accumulate("result", "3");//3表示没有登录
                return base.Json(jsonObject);
            }
            YSWL.MALL.Model.SNS.GroupTopics topicsModel = bllTopic.GetModelByCache(topicId);
            HttpCookie cookie =Request.Cookies["topicFav_" + topicId + CurrentUser.UserID];
            if (null != cookie)//取消赞
            {
                cookie = new HttpCookie("topicFav_" + topicId + CurrentUser.UserID);
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
                if (!OperateTopic(topicsModel, 0))
                {
                    jsonObject.Accumulate("result", "0");//失败了
                    return base.Json(jsonObject);
                }
            }
            else//赞
            {
                cookie = new HttpCookie("topicFav_" + topicId + CurrentUser.UserID);
                Response.Cookies.Add(cookie);
                if (!OperateTopic(topicsModel, 1))
                {
                    jsonObject.Accumulate("result", "0");//失败了
                    return base.Json(jsonObject);
                }
                cookie.Value = "HasSupport";
            }
            int count = topicsModel.FavCount;
            jsonObject.Accumulate("totalCount", count);
            jsonObject.Accumulate("result", "1");//1代表成功
            return base.Json(jsonObject);
        }
        #region 赞和取消赞
        /// <summary>
        /// 操作GroupTopics 赞或者取消赞
        /// </summary>
        /// <param name="topic">话题</param>
        /// <param name="type">操作1代表赞0代表取消赞</param>
        /// <returns></returns>
        public bool OperateTopic(YSWL.MALL.Model.SNS.GroupTopics topic, int type)
        {
            if (topic == null) return false;
            if (type == 1)
            {
                topic.FavCount += 1;
                return bllTopic.Update(topic);
            }
            else if (type == 0)
            {
                topic.FavCount -= 1;
                return bllTopic.Update(topic);
            }
            else
            {
                return false;
            }
        } 
        #endregion
        public ViewResult UserList(int? page, string type = "User", int groupId = 0,string viewName="UserLists",string ajaxViewName="_UserList")
        {
            YSWL.MALL.ViewModel.SNS.GroupInfo groupInfo = new MALL.ViewModel.SNS.GroupInfo();
            groupId = groupId > 0 ? groupId : GroupId;
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 1;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = 0;
            //小组成员总数 非管理员用户
            toalcount = bllGroupUser.GetRecordCount("GroupId=" + groupId + " AND Role = 0");
            //小组成员分页加载  非管理员用户
            groupInfo.UserList = new PagedList<Model.SNS.GroupUsers>(
                bllGroupUser.GetUserList(groupId, startIndex, endIndex)
                , page ?? 1, pagesize, toalcount);
            if (Request.IsAjaxRequest())
            {
                return View(ajaxViewName, groupInfo.UserList);
            }
            return View(viewName,groupInfo.UserList);
        }

        //删除帖子
        public ActionResult DeleteTopic(int id)
        {
            YSWL.MALL.BLL.SNS.GroupTopics groupTopicsBll=new GroupTopics();
            if (groupTopicsBll.DeleteTopic(id))//删除成功
            {
                return Content("ok");
            }
            return Content("no");
        }
        public ActionResult DeleteReply(int id)
        {
            YSWL.MALL.BLL.SNS.GroupTopicReply replyBll=new GroupTopicReply();
            if (replyBll.Delete(id))
            {
                return Content("ok");
            }
            return Content("no");
        }
    }
}
