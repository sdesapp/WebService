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
    //[RESTAuthorize]
    public class LeaveApproveController : ApiController
    {

        OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/LeaveApprove/GetApprovalLeave

        [HttpPost]

        public void ApprovelLeave()
        {
            OracleRepository repo = new OracleRepository();

            string message = "";
            repo.ApproveLeave("habbas", "3517", out message);

            Console.Write(message);

        }


    }
			

}
    

