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
    public class ApprovalListController : ApiController
    {

        OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/ApprovalList/GetApprovalList?username=username

        [HttpGet]

        public List<Leave> GetApprovalList(string username)
        {

            var dataTable = bOra.GetApprovalList(username);
            List<Leave> Leaves = new List<Leave>();
            foreach (DataRow record in dataTable.Rows)

            {
                Leaves.Add(new Leave()
                {
                    vardoc_no = record["vardoc_no"].ToString(),
                    varemp_code = record["varemp_code"].ToString(),
                    vardept_code = record["vardept_code"].ToString(),
                    varleave_type = record["varleave_type"].ToString(),
                    varleave_desc = record["varleave_desc"].ToString(),
                    varstart_date = record["varstart_date"].ToString(),
                    varend_date = record["varend_date"].ToString(),
                    varannual_days = record["varannual_days"].ToString(),
                    varother_days = record["varother_days"].ToString(),
                    varunpaid_days = record["varunpaid_days"].ToString(),
                    vartotal_days = record["vartotal_days"].ToString(),
                    varinternal_external = record["varinternal_external"].ToString(),
                    varleave_status = record["varleave_status"].ToString(),
                    

                });
            }

            return Leaves;
        }


    }
			

}
    

