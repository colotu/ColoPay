﻿/**
* UploadFileHandlerBase.cs
*
* 功 能： [文件上传基类]
* 类 名： UploadFileHandlerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/19 10:49:40  Rock    初版
*
* 
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web;

namespace ColoPay.Web.Handlers
{
    public abstract class UploadFileHandlerBase : UploadHandlerBase
    {
        #region 子类实现

        /// <summary>
        /// 临时保存原文件
        /// </summary>
        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            //临时保存文件
            file.SaveAs(uploadPath + fileName);
        }

        #endregion 子类实现
    }
}