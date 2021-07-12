using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using mService.Models;
using System.Data.SqlClient;
using System.Globalization;
using Repositories;

namespace mService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RESTAuthorize]
    public class ProcListController : ApiController
    {

        OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/ProcList/GetProcList?username=username

        [HttpGet]


        public List<Procurement> GetProcList(string username)
        {

            // var dataTable = bOra.GetProcList("pramya");
            var dataTable = bOra.GetProcList(username);
            List<Procurement> Procurements = new List<Procurement>();
            foreach (DataRow record in dataTable.Rows)


            {
                Procurements.Add(new Procurement()
                {
                    REQ_TYPE = record["REQ_TYPE"].ToString(),
                    REQ_NO = record["REQ_NO"].ToString(),
                    REQ_DATE = record["REQ_DATE"].ToString(),
                    REQ_BY = record["REQ_BY"].ToString(),
                    REQ_DEPT_CODE = record["REQ_DEPT_CODE"].ToString(),
                    REQ_DEPT_HEAD = record["REQ_DEPT_HEAD"].ToString(),
                    REQ_PURPOSE = record["REQ_PURPOSE"].ToString(),
                    SUBMIT = record["SUBMIT"].ToString(),
                    REQ_STATUS = record["REQ_STATUS"].ToString(),
                    REQ_STATUS_DESC = record["REQ_STATUS_DESC"].ToString(),
                  });
            }

            return Procurements;
        }



        //      Url: http://localhost:6363/api/procList/GetProcDetail?reqno=reqno

        [HttpGet]

        public List<Procurementdet> GetProcDetail(string reqno)
        {

              var dataTable = bOra.GetProcDetail(reqno);
            List<Procurementdet> Procurements1 = new List<Procurementdet>();
            foreach (DataRow record in dataTable.Rows)


            {
                Procurements1.Add(new Procurementdet()
                {
                    REQ_TYPE = record["REQ_TYPE"].ToString(),
                    REQ_NO = record["REQ_NO"].ToString(),
                    REQ_SR_NO = record["REQ_SR_NO"].ToString(),
                    REQ_ITEM_CODE = record["REQ_ITEM_CODE"].ToString(),
                    REQ_ITEM_NAME = record["REQ_ITEM_NAME"].ToString(),
                    MODELCODE = record["MODELCODE"].ToString(),
                    REQ_ITEM_SPECIFICATION = record["REQ_ITEM_SPECIFICATION"].ToString(),
                    REQ_QTY = record["REQ_QTY"].ToString(),
                    REQ_UNIT_OF_MEASURE = record["REQ_UNIT_OF_MEASURE"].ToString(),
                    REQ_UNIT_PRICE = record["REQ_UNIT_PRICE"].ToString(),
                    REQ_TOTAL_PRICE = record["REQ_TOTAL_PRICE"].ToString(),
                    REQ_RECVD_QTY = record["REQ_RECVD_QTY"].ToString(),
                    REQ_VENDOR_TYPE = record["REQ_VENDOR_TYPE"].ToString(),

                });
            }

            return Procurements1;
        }



        public List<ProcCount> GetProcCount(string username)
        {
            var dataTable = bOra.GetProcCount(username);
            List<ProcCount> ProcCount = new List<ProcCount>();
            foreach (DataRow record in dataTable.Rows)
            {
                ProcCount.Add(new ProcCount()
                {
                    Count = record["Count"].ToString(),

                });
            }
            return ProcCount;


        }

        //  http://localhost:6363/api/procList/ApproveProc
        [HttpPost]
        public void ApproveProc(ApproveProc approve)
        {
            OracleRepository repo = new OracleRepository();

            string message = "ok";
         //    repo.ApproveProc("ITEM REQUEST", "IRQ0003996", "IT01", "SHAHNAWAZ FARIDI", "app testing purpose", "18/11/2020", "habbas", "android", "0.0.0.0");
            repo.ApproveProc(approve.p_req_type, approve.p_req_no, approve.p_dept_code, approve.p_req_by, approve.p_req_purpose, approve.p_req_date, approve.p_user_id, approve.p_host_name, approve.p_host_ip);

            Console.Write(message);

        }
        //  http://localhost:6363/api/procList/RejectProc

        [HttpPost]
        public void RejectProc(RejectProc reject)
        {
            OracleRepository repo = new OracleRepository();

            string message = "ok";
           repo.RejectProc(reject.p_req_type, reject.p_req_no, reject.p_user_id, reject.p_reject_reason);

            Console.Write(message);

        }



    }


}
    

