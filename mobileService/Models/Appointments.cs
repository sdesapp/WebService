using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mService.Models
{
    

    public class Appointments
    {
        public string RecNumber { get; set; }
        public string RcptDate { get; set; }
        public string ConsigneeKey { get; set; }
        public string PortOfLoading { get; set; }
        public string MasterBolNumber { get; set; }
        public string BolNumber { get; set; }
        public string TruckNumber { get; set; }
        public string Commodity { get; set; }
        public string Package { get; set; }
        public string Qty { get; set; }
        public string Weight { get; set; }

        
    }

	public class LoadingPorts
	{
		public string BL_DESC { get; set; }
	}

	public class Consignee
	{
	
		public String ENG_DESC { get; set; }
		public string CODE { get; set; }
	}

    
   
}