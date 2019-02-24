using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoPay.WebApi.Models
{
    public class QueryInfo
    {
        public string appid { get; set; }
        public string secrit { get; set; }
        public string order_no { get; set; }
    }
}