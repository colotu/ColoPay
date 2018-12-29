
using System;
using System.Data;
using System.Collections.Generic;
using YSWL.Common;
using ColoPay.Model.Pay;
namespace ColoPay.BLL.Pay
{
	/// <summary>
	/// Enterprise
	/// </summary>
	public partial class Enterprise
	{
		private readonly ColoPay.DAL.Pay.Enterprise dal=new ColoPay.DAL.Pay.Enterprise();
		public Enterprise()
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
		public bool Exists(int EnterpriseID)
		{
			return dal.Exists(EnterpriseID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ColoPay.Model.Pay.Enterprise model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ColoPay.Model.Pay.Enterprise model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int EnterpriseID)
		{
			
			return dal.Delete(EnterpriseID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string EnterpriseIDlist )
		{
			return dal.DeleteList(YSWL.Common.Globals.SafeLongFilter(EnterpriseIDlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ColoPay.Model.Pay.Enterprise GetModel(int EnterpriseID)
		{
			
			return dal.GetModel(EnterpriseID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ColoPay.Model.Pay.Enterprise GetModelByCache(int EnterpriseID)
		{
			
			string CacheKey = "EnterpriseModel-" + EnterpriseID;
			object objModel = YSWL.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(EnterpriseID);
					if (objModel != null)
					{
						int ModelCache = YSWL.Common.ConfigHelper.GetConfigInt("ModelCache");
						YSWL.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ColoPay.Model.Pay.Enterprise)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ColoPay.Model.Pay.Enterprise> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ColoPay.Model.Pay.Enterprise> DataTableToList(DataTable dt)
		{
			List<ColoPay.Model.Pay.Enterprise> modelList = new List<ColoPay.Model.Pay.Enterprise>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ColoPay.Model.Pay.Enterprise model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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

		#endregion  ExtensionMethod
	}
}

