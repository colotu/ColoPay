/**  版本信息模板在安装目录下，可自行修改。
* Order.cs
*
* 功 能： N/A
* 类 名： Order
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/12/29 23:41:08   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using YSWL.Common;
using ColoPay.Model.Pay;
namespace ColoPay.BLL.Pay
{
	/// <summary>
	/// Order
	/// </summary>
	public partial class Order
	{
		private readonly ColoPay.DAL.Pay.Order dal=new ColoPay.DAL.Pay.Order();
		public Order()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int OrderId)
        {
            return dal.Exists(OrderId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ColoPay.Model.Pay.Order model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ColoPay.Model.Pay.Order model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int OrderId)
        {

            return dal.Delete(OrderId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string OrderIdlist)
        {
            return dal.DeleteList(YSWL.Common.Globals.SafeLongFilter(OrderIdlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ColoPay.Model.Pay.Order GetModel(int OrderId)
        {

            return dal.GetModel(OrderId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ColoPay.Model.Pay.Order GetModelByCache(int OrderId)
        {

            string CacheKey = "OrderModel-" + OrderId;
            object objModel = YSWL.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(OrderId);
                    if (objModel != null)
                    {
                        int ModelCache = YSWL.Common.ConfigHelper.GetConfigInt("ModelCache");
                        YSWL.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ColoPay.Model.Pay.Order)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ColoPay.Model.Pay.Order> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ColoPay.Model.Pay.Order> DataTableToList(DataTable dt)
        {
            List<ColoPay.Model.Pay.Order> modelList = new List<ColoPay.Model.Pay.Order>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ColoPay.Model.Pay.Order model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        public ColoPay.Model.Pay.Order GetModel(string code)
        {
            return dal.GetModel(code);
        }
        public bool CompleteOrder(ColoPay.Model.Pay.Order orderInfo)
        {
            bool isSuccess = dal.CompleteOrder(orderInfo);
            if (orderInfo.Agentd > 0)//处理代理商佣金
            {
                ColoPay.BLL.Pay.BalanceDetail detailBll = new BalanceDetail();
                detailBll.AgentBalance(orderInfo);
            }
            return isSuccess;
        }

        public ColoPay.Model.Pay.Order GetModelEx(string enterOrder,int enterpriseID)
        {
            return dal.GetModelEx(enterOrder, enterpriseID);
        }

        public bool HasNotify(int orderId)
        {
            return dal.HasNotify(orderId);
        }
        #endregion  ExtensionMethod
    }
}

