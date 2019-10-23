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


    }
			

}
    

