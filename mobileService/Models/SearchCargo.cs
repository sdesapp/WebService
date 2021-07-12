using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mService.Models
{
    

     public class Cargos
    {
        public string main_consignee { get; set; }
        public string main_consignee_name { get; set; }
        public string storerkey { get; set; }
        public string company { get; set; }
        public string bolnumber { get; set; }
        public string containerno { get; set; }
        public string truck_number { get; set; }
        public string recvd_date { get; set; }
        public string recvd_loc { get; set; }
        public string lotcurr_loc { get; set; }
        public string recvd_qty { get; set; }
        public string recvd_weight { get; set; }
        public string shipped_date { get; set; }
        public string tolot { get; set; }
     

     public List<GetCargoHistory> GetCargoHistory { get; internal set; }
    }
    public class GetCargoHistory
    {
        public string ENG_DESC { get; set; }
        public string Date_done { get; set; }
      
    }


}