using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSWL.Payment.Model;

namespace ColoPay.Model.Pay
{
    public partial class Order : IOrderInfo
    {


        #region 克隆构造
        /// <summary>
        /// 克隆构造
        /// </summary>
        /// <remarks>不会克隆 订单项目 和 子订单集合</remarks>
        public Order(Order orderInfo)
        {
            _orderid = orderInfo.OrderId;
            _ordercode = orderInfo.OrderCode;
            _enterorder= orderInfo.EnterOrder;
            _enterpriseid = orderInfo.EnterpriseID;
            _agentd = orderInfo.Agentd;
            _feerate = orderInfo.FeeRate;
            _orderamount= orderInfo.OrderAmount;
            _paymodeid = orderInfo.PayModeId;
            _orderinfo = orderInfo.OrderInfo;
            _appid = orderInfo.AppId;
            _appsecrit = orderInfo.AppSecrit;
            _appurl = orderInfo.AppUrl;
            _appreturnurl = orderInfo.AppReturnUrl;
            _createdtime = orderInfo.CreatedTime;
            _paymenttypename = orderInfo.PaymentTypeName;
            _paymentgateway = orderInfo.PaymentGateway;
            _paymentstatus = orderInfo.PaymentStatus;
            _paymentfee = orderInfo.PaymentFee;
            _amount = orderInfo.Amount;
            _remark = orderInfo.Remark;
        }
        #endregion

        #region IOrderInfo 成员

        public System.DateTime OrderDate
        {
            get { return CreatedTime; }
            set { CreatedTime = value; }
        }

        OrderStatus IOrderInfo.OrderStatus
        {
            get
            {
                //订单状态 -4 系统锁定 | -3 后台锁定 | -2 用户锁定 | -1 死单(取消) | 0 未处理 | 1 活动 | 2 已完成

                return YSWL.Payment.Model.OrderStatus.InProgress;


            }
        }

        PaymentStatus IOrderInfo.PaymentStatus
        {
            get
            {
                //支付状态 0 未支付 | 1 等待确认 | 2 已支付 | 3 处理中(预留) | 4 支付异常(预留)
                switch (PaymentStatus)
                {
                    case 0:
                        return YSWL.Payment.Model.PaymentStatus.NotYet;
                    case 2:
                        return YSWL.Payment.Model.PaymentStatus.Prepaid;
                    default:
                        return YSWL.Payment.Model.PaymentStatus.All;
                }
            }
        }

        RefundStatus IOrderInfo.RefundStatus
        {
            get
            {
                //退款状态 0 未退款 | 1 请求退款 | 2 处理中 | 3 已退款 | 4 拒绝退款

                return YSWL.Payment.Model.RefundStatus.None;

            }
        }

        ShippingStatus IOrderInfo.ShippingStatus
        {
            get
            {
                //配送状态 0 未发货 | 1 打包中 | 2 已发货 | 3 已确认收货 | 4 拒收退货中 | 5 拒收已退货

                return YSWL.Payment.Model.ShippingStatus.NotYet; 
            }
        }

        public string GatewayOrderId
        {
            get
            {
                return "";
            }
            set {  }
        }

        public int PaymentTypeId
        {
            get { return PayModeId; }
            set { PayModeId = value; }
        }
        #endregion

        #region 收货人
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string ShipName { get; set; }
        /// <summary>
        /// 收货人Email
        /// </summary>
        public string ShipEmail { get; set; }
        /// <summary>
        /// 收货人收货地区
        /// </summary>
        public string ShipRegion { get; set; }
        /// <summary>
        /// 收货人收货地址
        /// </summary>
        public string ShipAddress { get; set; }
        /// <summary>
        /// 收货人座机号码
        /// </summary>
        public string ShipTelPhone { get; set; }
        /// <summary>
        /// 收货人手机号码
        /// </summary>
        public string ShipCellPhone { get; set; }

        public string BuyerEmail { get; set; }
        #endregion
    }
}
