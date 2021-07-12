using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mService.Models
{
    

     public class Containers
    {
        public string bolnumber { get; set; }
        public string Container_no { get; set; }
        public string sku { get; set; }
        public string carrierreference { get; set; }
        public string company { get; set; }
        public string curr_loc { get; set; }
        public string storerkey { get; set; }
        public string sku1 { get; set; }
        public string lot { get; set; }

        public string ENG_DESC { get; set; }

        public List<GetTrackingDetails> GetTrackingDetails { get; set; }

    }
    public class GetTrackingDetails
    {
        public string status { get; set; }
        public string date1 { get; set; }
        public string date2 { get; set; }

    }


}