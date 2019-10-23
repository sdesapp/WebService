using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mService.Models
{
    

    public class Leave
    {
        public string vardoc_no { get; set; }
        public string varemp_code { get; set; }
        public string vardept_code { get; set; }
        public string varleave_type { get; set; }
        public string varleave_desc { get; set; }
        public string varstart_date { get; set; }
        public string varend_date { get; set; }
        public string varannual_days { get; set; }
        public string varother_days { get; set; }
        public string varunpaid_days { get; set; }
        public string vartotal_days { get; set; }
        public string varinternal_external { get; set; }
        public string varleave_status { get; set; }

            }
}