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
    public class AppointmentsController : ApiController
    {

		OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/Appointments/GetAppointments?username=Test
        

        [HttpGet]
		public List<LoadingPorts> GetLoadingPorts()
		{

			var dataTable=bOra.GetLoadingPorts();
			List<LoadingPorts> allLoadingPorts = new List<LoadingPorts>();
			foreach(DataRow record in dataTable.Rows)
			{
				allLoadingPorts.Add(new LoadingPorts() {
					BL_DESC = record["BL_DESC"].ToString()
				});
			}

			return allLoadingPorts;
		}

		[HttpGet]
		public List<Consignee> GetConsignee(string USER_ID)
		{
			if (!string.IsNullOrEmpty(USER_ID))
			{
				var dataTable = bOra.GetSubConsignees(USER_ID);
				List<Consignee> consignees = new List<Consignee>();
				foreach (DataRow record in dataTable.Rows)
				{
					consignees.Add(new Consignee()
					{
						ENG_DESC = record["ENG_DESC"].ToString(),
						CODE = record["CODE"].ToString(),
					});
				}

				return consignees;
			}
			return null;

		}

        [HttpGet]
        public string GetNewRecordNumber()
        {
            return bOra.GetNewRecordNumber();

        }

        [HttpPost]
        public bool SaveRecord(Appointments appt)
        {
            // required field validation
            if(string.IsNullOrEmpty(appt.BolNumber) || 
                string.IsNullOrEmpty(appt.TruckNumber) || 
                string.IsNullOrEmpty(appt.Commodity) || 
                string.IsNullOrEmpty(appt.Package) ||
                string.IsNullOrEmpty(appt.Qty) ||
                string.IsNullOrEmpty(appt.Weight)
                )
            {
                return false;
            }


            //return bOra.Save(appt, User.Identity.Name);
            return bOra.Save(appt, "link");

        }

        [HttpPost]
        public bool Submit(List<Appointments> AppList)
        {
            bool isSubmitted = false;
            foreach (Appointments app in AppList)
            {
                if(bOra.isRecordExist("select NVL(TM_STATUS,9) FROM TRUCK_MANIFEST WHERE TM_KEY=nvl('" + app.RecNumber + "','0') and TM_STATUS=0"))
                {
                    if (bOra.Submit(app, User.Identity.Name))
                    {
                        isSubmitted = true;
                    }
                    else
                    {
                        isSubmitted = false;
                    }
                    
                }
                else
                {
                    isSubmitted = false;
                }
            }

            return isSubmitted;
            
        }

        [HttpPost]
        public bool Delete(string recNumber)
        {
            return bOra.Delete(recNumber);
        }
    }
}
