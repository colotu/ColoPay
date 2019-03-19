using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoPay.WebApi.Models
{
    public class QrNotify
    {
        public int status { get; set; }

        public int customerid { get; set; }

        //平台订单号
        public string sdpayno { get; set; }

        //商户订单号
        public string sdorderno { get; set; }

        public decimal total_fee { get; set; }

        public string paytype { get; set; }

        public string remark { get; set; }

        public string sign { get; set; }
    
    }
}