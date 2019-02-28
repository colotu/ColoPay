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
        public string bankcode { get; set; }

    }
}