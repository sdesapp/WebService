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
   // [RESTAuthorize]
    public class SearchcntController : ApiController
    {

		OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/Searchcnt/GetContainer1?ContainerNo=


        [HttpGet]
		       public Containers GetTracking(string ContainerNo)
        {

            var dataTable = bOra.GetTracking( ContainerNo);

            // it will be always one container with multiple details
            Containers Containers = new Containers();

            if (dataTable.Rows.Count > 0)
            {
                var record = dataTable.Rows[0];
                Containers.bolnumber = record["bolnumber"].ToString();
                Containers.Container_no = record["Container_no"].ToString();
                Containers.sku = record["sku"].ToString();
                Containers.carrierreference = record["carrierreference"].ToString();
                Containers.company = record["company"].ToString();
                Containers.curr_loc = record["curr_loc"].ToString();
                Containers.storerkey = record["storerkey"].ToString();
                Containers.sku1 = record["sku1"].ToString();
                Containers.lot = record["lot"].ToString();

                dataTable = bOra.GetContainerStatus(ContainerNo);
                if (dataTable.Rows.Count > 0)
                {
                    record = dataTable.Rows[0];
                    Containers.ENG_DESC = record["ENG_DESC"].ToString();
                }

                dataTable = bOra.GetTrackingDetails( ContainerNo);
                List<GetTrackingDetails> Details = new List<GetTrackingDetails>();
                foreach (DataRow row in dataTable.Rows)
                {
                    Details.Add(new GetTrackingDetails()
                    {
                        status = row["status"].ToString(),
                        date1 = row["date1"].ToString(),
                        date2 = row["date2"].ToString()
                    });
                }

                Containers.GetTrackingDetails = Details;

            }

            return Containers;
        }
        public List<GetTrackingDetails> GetTrackingDetails(string ContainerNo)
        {
            var dataTable = bOra.GetTrackingDetails(ContainerNo);
            List<GetTrackingDetails> Details = new List<GetTrackingDetails>();
            foreach (DataRow record in dataTable.Rows)
            {
                Details.Add(new GetTrackingDetails()
                {
                    status = record["status"].ToString(),
                    date1 = record["date1"].ToString(),
                    date2 = record["date2"].ToString()
                });
            }
            return Details;

        }
    }
   

}
