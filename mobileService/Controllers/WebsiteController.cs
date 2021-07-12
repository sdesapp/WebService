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
    public class websiteController : ApiController
    {

		OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/website/GetJobTitles


       [HttpGet]
		public List<JobTitles> GetJobTitles()
		{

			var dataTable=bOra.GetJobTitles();
			List<JobTitles> allJobTitles = new List<JobTitles>();
			foreach(DataRow record in dataTable.Rows)
			{
                allJobTitles.Add(new JobTitles() {
                     title = record["title"].ToString(),
                EXPERIENCE = record["EXPERIENCE"].ToString(),
                 POSTED_ON = record["POSTED_ON"].ToString(),
            JOBDESCRIPTION = record["JOBDESCRIPTION"].ToString()
                    

                });
			}

			return allJobTitles;
		}


        // Url: http://localhost:6363/api/website/GetAppointments?username=Test
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
                    if (bOra.Submit(app, "link")) //(bOra.Submit(app, User.Identity.Name))
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
