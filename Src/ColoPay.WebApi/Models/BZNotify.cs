using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoPay.WebApi.Models
{
    public class BZNotify
    {
        public string mch_id { get; set; }
        public string order_no { get; set; }
        public int money { get; set; }
        public string sdpayno { get; set; }
        public int status { get; set; }
        public string paytype { get; set; }
        public string sign { get; set; }
    }
}