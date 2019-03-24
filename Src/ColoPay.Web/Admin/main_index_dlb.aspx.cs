/**
* main_index.cs
*
* 功 能： 管理员后台
* 类 名： main_index
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/24 15:21:18  Rock    初版
*
* 
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：小鸟科技（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using ColoPay.Model.Pay;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ColoPay.Web.Admin
{
    public partial class main_index_dlb : PageBaseAdmin
    {
        public string CurrentUserName = string.Empty;
        public string GetDateTime = string.Empty;

        private BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
        private Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();
        private ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(CurrentUser.TrueName))
                {
                    CurrentUserName = CurrentUser.TrueName;
                }
                else
                {
                    CurrentUserName = CurrentUser.UserName;
                }
                if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 12)
                {
                    GetDateTime = "早上好";
                }
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                {
                    GetDateTime = "下午好";
                }
                else
                {
                    GetDateTime = "晚上好";
                }

                #region 交易订单信息
                string today = System.DateTime.Now.ToString("yyyy-MM-dd");
                string monthDay = System.DateTime.Now.ToString("yyyy-MM-01");
                string tomorrow = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                //订单量
                this.lblOrderToDay.Text = orderBll.GetOrderCount(today, tomorrow).ToString();
                this.lblOrderYest.Text = orderBll.GetOrderCount(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), today).ToString();
                this.lblOrderWeek.Text = orderBll.GetOrderCount(System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), tomorrow).ToString();
                this.lblOrderMon.Text = orderBll.GetOrderCount(monthDay, tomorrow).ToString();
                //销售额
                this.lblSaleToDay.Text = orderBll.GetOrderAmount(today, tomorrow).ToString("f2");
                this.lblSaleYest.Text = orderBll.GetOrderAmount(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), today).ToString("f2");
                this.lblSaleWeek.Text = orderBll.GetOrderAmount(System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), tomorrow).ToString("f2");
                this.lblSaleMon.Text = orderBll.GetOrderAmount(monthDay, tomorrow).ToString("f2");
                //佣金额
                this.lblFeeToDay.Text = orderBll.GetOrderFee(today, tomorrow).ToString("f2");
                this.lblFeeYest.Text = orderBll.GetOrderFee(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), today).ToString("f2");
                this.lblFeeWeek.Text = orderBll.GetOrderFee(System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), tomorrow).ToString("f2");
                this.lblFeeMon.Text = orderBll.GetOrderFee(monthDay, tomorrow).ToString("f2");

                #endregion

                BindJson(orderBll.OrderStat(System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), tomorrow));
                getTopJson();
            }


        }

        //交易额走势图
        private void BindJson(DataSet ds)
        {
            this.createdDate.Value = "";
            this.amount.Value = "";
            this.amountFee.Value = "";
            List<OrderStat> feeList = new List<OrderStat>();
            //把dataset转换成list
            OrderStat model = null;
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                model = new OrderStat();
                model.Amount = YSWL.Common.Globals.SafeDecimal(dr["Total"], 0);
                model.AmountFee = YSWL.Common.Globals.SafeDecimal(dr["TotalFee"], 0);
                model.CreatedDate = dr["Date"].ToString();
                model.Count = YSWL.Common.Globals.SafeInt(dr["Count"], 0);
                feeList.Add(model);
            }
            for (int i = -6; i < 1; i++)
            {
                string dateStr = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                if (!feeList.Exists(c => c.CreatedDate == dateStr))
                {
                    model = new OrderStat();
                    model.CreatedDate = dateStr;
                    model.Amount = 0;
                    model.AmountFee = 0;
                    feeList.Add(model);
                }
            }
            feeList = feeList.OrderBy(c => c.CreatedDate).ToList();

            this.createdDate.Value = String.Join(",", feeList.Select(c => c.CreatedDate));
            this.amount.Value = String.Join(",", feeList.Select(c => c.Amount));
            this.amountFee.Value = String.Join(",", feeList.Select(c => c.AmountFee));
        }

        private void getTopJson()
        {

            this.enterOrder.Value = "";
            this.enterAmount.Value = "";
            List<OrderTop> feeList = new List<OrderTop>();
            //把dataset转换成list
            OrderTop model = null;
            DataSet ds = orderBll.GetOrderTop(10);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                model = new OrderTop();
                model.Amount = YSWL.Common.Globals.SafeDecimal(dr["TotalAmount"], 0);
                model.AmountFee = YSWL.Common.Globals.SafeDecimal(dr["TotalFee"], 0);
                model.EnterOrder = dr["EnterOrder"].ToString();
                feeList.Add(model);
            }

            this.enterOrder.Value = String.Join(",", feeList.Select(c => c.EnterOrder));
            this.enterAmount.Value = String.Join(",", feeList.Select(c => c.Amount));
            this.enterFee.Value = String.Join(",", feeList.Select(c => c.AmountFee));
        }
    }
}