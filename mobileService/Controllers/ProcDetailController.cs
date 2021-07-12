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
  //  [RESTAuthorize]
    public class ProcDetailController : ApiController
    {

        OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/ProcDetail/GetProcDetail?reqno=reqno

        [HttpGet]

        public List<Procurementdet> GetProcDetail(string reqno)
        {

            // var dataTable = bOra.GetProcList("pramya");
            var dataTable = bOra.GetProcList(reqno);
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


    }
			

}
    

