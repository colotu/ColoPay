using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoPay.WebApi.Models
{
    public class PayInfo
    {
        public string appid { get; set; }
        public string secrit { get; set; }
        public string order_no { get; set; }
        public decimal amount { get; set; }
        public string remark { get; set; }
        public string paytype { get; set; }
        public int get_code { get; set; }
        public string return_url { get; set; }
        public bool istest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bankcode { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string bankcard { get; set; }

       /// <summary>
       /// 持卡人手机号
       /// </summary>
        public string moblie { get; set; }
        /// <summary>
        /// 持卡人身份证号
        /// </summary>
        public string idcard { get; set; }
        /// <summary>
        /// 持卡人姓名
        /// </summary>
        public string realname { get; set; }
        /// <summary>
        /// 订单标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string product { get; set; }

    }
}