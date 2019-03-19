using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoPay.WebApi.Models
{
    public class FengHeNotify
    {
        public string returncode { get; set; }
        public string  memberid { get; set; }
        //平台订单号
        public string orderid { get; set; }
         
        public string amount { get; set; }

        public string datetime { get; set; }

        public string transaction_id { get; set; }

        public string attach { get; set; }
        public string sign { get; set; }

    }
}