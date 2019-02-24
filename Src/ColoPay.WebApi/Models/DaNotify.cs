using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoPay.WebApi.Models
{
    public class DaNotify
    {
        public string order { get; set; }
        public string code { get; set; }
        public decimal mount { get; set; }
        public string msg { get; set; }
        public string time { get; set; }
        public string sign { get; set; }

    }
}