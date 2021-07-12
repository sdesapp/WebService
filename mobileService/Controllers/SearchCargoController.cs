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
    public class SearchCargoController : ApiController
    {

		OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/SearchCargo/GetCargoTracking?CargoblNo=


        [HttpGet]
		       public Cargos GetCargoTracking(string CargoblNo)
        {

            var dataTable = bOra.GetCargoTracking(CargoblNo);

            // it will be always one container with multiple details
            Cargos Cargos = new Cargos();

            if (dataTable.Rows.Count > 0)
            {

                var record = dataTable.Rows[0];
                Cargos.main_consignee = record["main_consignee"].ToString();
                Cargos.main_consignee_name = record["main_consignee_name"].ToString();
                Cargos.storerkey = record["storerkey"].ToString();
                Cargos.company = record["company"].ToString();
                Cargos.bolnumber = record["bolnumber"].ToString();
                Cargos.containerno = record["containerno"].ToString();
                Cargos.truck_number = record["truck_number"].ToString();
                Cargos.recvd_date = record["recvd_date"].ToString();
                Cargos.recvd_loc = record["recvd_loc"].ToString();
                Cargos.lotcurr_loc = record["curr_loc"].ToString();
                Cargos.recvd_qty = record["recvd_qty"].ToString();
                Cargos.recvd_weight = record["recvd_weight"].ToString();
                Cargos.shipped_date = record["shipped_date"].ToString();
                Cargos.tolot = record["tolot"].ToString();

                 dataTable = bOra.GetCargoHistory(CargoblNo);
                List<GetCargoHistory> Details = new List<GetCargoHistory>();
                foreach (DataRow row in dataTable.Rows)
                {
                    Details.Add(new GetCargoHistory()
                    {
                        ENG_DESC = row["ENG_DESC"].ToString(),
                        Date_done = row["Date_done"].ToString(),
                       
                    });
                }

                Cargos.GetCargoHistory = Details;

            }

            return Cargos;
        }
        public List<GetCargoHistory> GetTrackingDetails(string ContainerNo)
        {
            var dataTable = bOra.GetTrackingDetails(ContainerNo);
            List<GetCargoHistory> Details = new List<GetCargoHistory>();
            foreach (DataRow record in dataTable.Rows)
            {
                Details.Add(new GetCargoHistory()
                {
                    ENG_DESC = record["ENG_DESC"].ToString(),
                    Date_done = record["Date_done"].ToString(),
                                 });
            }
            return Details;

        }
    }

    
}
