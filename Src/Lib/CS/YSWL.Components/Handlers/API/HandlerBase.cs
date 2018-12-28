﻿/**
* HandlerBase.cs
*
* 功 能： Json.RPC API 基类
* 类 名： HandlerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/17 17:04:23  Ben    初版
*
* Copyright (c) 2012 YSWL Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：云商未来（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using YSWL.Components;
using YSWL.Json.RPC;
using YSWL.Json.RPC.Web;

namespace YSWL.Components.Handlers.API
{

    public class HandlerBase : JsonRpcHandler, IRequiresSessionState
    {
        #region 常量成员
        protected const string REQUEST_HEADER_METHOD = "X-JSON-RPC";

        protected const string ERROR_CODE_UNAUTHORIZED = "41";
        protected const string ERROR_MSG_UNAUTHORIZED = "您的帐号未授权，无法使用接口系统！";

        protected const string ERROR_CODE_ARGUMENT = "102";
        protected const string ERROR_MSG_ARGUMENT = "参数错误!";

        protected const string ERROR_CODE_NODATA = "104";
        protected const string ERROR_MSG_NODATA = "数据不存在！";

        protected const string ERROR_MSG_LOG = "API: [{0}] 方法发生错误! {1}";

        /// <summary>
        /// 上传目录 格式化字符串
        /// <remarks>
        /// {0} 模块文件夹 如 简报 或 考勤
        /// {1} 用户ID文件夹
        /// </remarks>
        /// </summary>
      protected static readonly string PATH_FORMAT_FOLDER =
            string.Format("/{0}/API/{1}/", MvcApplication.UploadFolder, "{0}/{1}");

        /// <summary>
        /// 上传文件名 格式化字符串
        /// <remarks> 
        /// {0} 模块主键 如 简报ID 或 考勤ID
        /// {1} 文件名称 接口端上传
        /// </remarks>
        /// </summary>
        protected const string PATH_FORMAT_FILENAME = "{0}_{1}";

        /// <summary>
        /// 日期时间格式
        /// </summary>
        protected const string DATE_FORMAT_DATETIME = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 日期格式
        /// </summary>
        protected const string DATE_FORMAT_DATE = "yyyy-MM-dd";
        #endregion

        #region 构造

        private bool requestSecurity = false;
        public HandlerBase(bool _requestSecurity)
        {
            requestSecurity = _requestSecurity;
        }

        #endregion

        public override void ProcessRequest()
        {
            try
            {
                base.ProcessRequest();
            }
            catch (JsonRpcException ex)
            {
                if (HttpContext.Current.IsDebuggingEnabled)
                    throw;

                //LogHelp.AddInvadeLog(ex.Message, System.Web.HttpContext.Current.Request);
                Response.Clear();
                Response.StatusCode = 404;
                Response.Status = "404 Not Found";
                Response.End();
            }
        }

        protected override IDictionary GetFeatures()
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                return base.GetFeatures();
            }

            string key = typeof(JsonRpcService).FullName;
            IDictionary config = (IDictionary)HttpRuntime.Cache.Get(key);

            if (config == null)
            {
                config = new Hashtable(6);

                config.Add("rpc", typeof(JsonRpcExecutive).AssemblyQualifiedName);
                config.Add("getrpc", typeof(JsonRpcGetProtocol).AssemblyQualifiedName);

                HttpRuntime.Cache.Add(key, config, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            return config;
        }

        protected override bool RequestSecurity(HttpContext context)
        {

            #region  判断是否为自动链接数据库
            if (MvcApplication.IsAutoConn)
            {
                string tag = context.Request.Headers["enterpriseStr"];
                if (!String.IsNullOrWhiteSpace(tag))
                {
                    long enterpriseId = YSWL.Common.DEncrypt.DEncrypt.ConvertToNumber(tag);
                    YSWL.Common.CallContextHelper.SetAutoTag(enterpriseId);
                }
            }
            #endregion 

            if (!requestSecurity) return true;

            bool securityParam =Common.ConfigHelper.GetConfigBool("API_Security");
            int intervalSeconds = Common.ConfigHelper.GetConfigInt("API_TimeInterval");
            string apiKey = Common.ConfigHelper.GetConfigString("API_Key");
            if ((HttpContext.Current != null && HttpContext.Current.IsDebuggingEnabled) || !securityParam)//没开启安全验证直接请求
            {
                return true;
            }
            string requestId = context.Request.Headers["requestId"];
            string timespan = context.Request.Headers["timespan"];
            if (string.IsNullOrWhiteSpace(timespan)) return false;
            DateTime receiveTime = DateTime.ParseExact(timespan, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            string md5Value = context.Request.Headers["md5hex"];
            if (string.IsNullOrWhiteSpace(md5Value)) return false;
         
            if (receiveTime.AddSeconds(intervalSeconds) > DateTime.Now)//在20s时间内
            {
                
                MD5 md5 = MD5.Create();
                string firstMd5;
                string secondMd5;
                byte[] bs = Encoding.UTF8.GetBytes(requestId + timespan + apiKey);
                byte[] hs = md5.ComputeHash(bs);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hs)
                {
                    // 以十六进制格式格式化
                    sb.Append(b.ToString("x2"));
                }
                firstMd5 = sb.ToString();
                sb.Clear();
                byte[] bss = Encoding.UTF8.GetBytes(requestId + timespan + firstMd5);
                byte[] hss = md5.ComputeHash(bss);
                // StringBuilder sb = new StringBuilder();
                foreach (byte b in hss)
                {
                    // 以十六进制格式格式化
                    sb.Append(b.ToString("x2"));
                }
                secondMd5 = sb.ToString();
                if (!string.Equals(secondMd5, md5Value))
                {
                    return false;
                }
                return base.RequestSecurity(context);
            }
            return false;
        }
    }
}