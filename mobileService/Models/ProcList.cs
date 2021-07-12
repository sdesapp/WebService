using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mService.Models
{
    public class Procurement
    {

   public string REQ_TYPE { get; set; }
    public string REQ_NO { get; set; }
    public string REQ_DATE { get; set; }
    public string REQ_BY { get; set; }
    public string REQ_DEPT_CODE { get; set; }
    public string REQ_DEPT_HEAD { get; set; }
    public string REQ_PURPOSE { get; set; }
    public string SUBMIT { get; set; }
    public string REQ_STATUS { get; set; }
    public string REQ_STATUS_DESC { get; set; }
}
    public class Procurementdet
    {

        public string REQ_TYPE { get; set; }
        public string REQ_NO { get; set; }
        public string REQ_SR_NO { get; set; }
        public string REQ_ITEM_CODE { get; set; }
        public string REQ_ITEM_NAME { get; set; }
        public string MODELCODE { get; set; }
        public string REQ_ITEM_SPECIFICATION { get; set; }
        public string REQ_QTY { get; set; }
        public string REQ_UNIT_OF_MEASURE { get; set; }
        public string REQ_REF_NO { get; set; }
        public string REQ_UNIT_PRICE { get; set; }
        public string REQ_TOTAL_PRICE { get; set; }
        public string REQ_RECVD_QTY { get; set; }
        public string REQ_VENDOR_TYPE { get; set; }


    }

    public class ProcCount
    {
        public string Count { get; set; }
    }

    public class ApproveProc
    {

        public string p_req_type { get; set; }
        public string p_req_no { get; set; }
        public string p_dept_code { get; set; }
        public string p_req_by { get; set; }
        public string p_req_purpose { get; set; }
        public string p_req_date { get; set; }
        public string p_user_id  { get; set; }
        public string p_host_name { get; set; }
        public string p_host_ip  { get; set; }
    }


    public class RejectProc
    {

        public string p_req_type { get; set; }
        public string p_req_no { get; set; }
        public string p_user_id { get; set; }
        public string p_reject_reason { get; set; }
   
    }

}