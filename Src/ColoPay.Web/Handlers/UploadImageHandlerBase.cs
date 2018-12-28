/**
* UploadImageHandlerBase.cs
*
* 功 能： 上传图片Handler基类
* 类 名： UploadImageHandlerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/4 17:06:00    Ben     初版
* V0.02  2012/10/16 18:45:00  Ben     上传图片基类再次抽离出上传文件基类
*
* Copyright (c) 2012 YS56 Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：云商未来（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Drawing;
using System.Web;
using YSWL.Common;
using System.Collections.Generic;
using ColoPay.Model.SysManage;
using ColoPay.Web.Components;

namespace ColoPay.Web.Handlers
{
    public abstract class UploadImageHandlerBase : UploadHandlerBase
    {
        protected readonly MakeThumbnailMode ThumbnailMode;

        protected override string[] AllowFileExt
        {
            get
            {
                return ".jpg|.jpeg|.gif|.png|.bmp".Split('|');
            }
        }

        public UploadImageHandlerBase(MakeThumbnailMode mode = MakeThumbnailMode.None, bool isLocalSave = true,ApplicationKeyType applicationKeyType = ApplicationKeyType.None)
            : base(isLocalSave,applicationKeyType)
        {
            ThumbnailMode = mode == MakeThumbnailMode.None ? MakeThumbnailMode.W : mode;
        }

        #region 子类实现
     

        #endregion

        
    }
}