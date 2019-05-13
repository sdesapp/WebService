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
    public class BillingController : ApiController
    {

		OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/Appointments/GetAppointments?username=Test
        

        [HttpGet]
		public List<Invoice> GetInvoice(string ConsigneeID,string BL,string Container,string FromDate,string ToDate)
		{

			var dataTable=bOra.GetInvoice(ConsigneeID,BL,Container,FromDate,ToDate);
			List<Invoice> Invoices = new List<Invoice>();
			foreach(DataRow record in dataTable.Rows)
			{
                Invoices.Add(new Invoice() {
					InvNumber = record["inv_no"].ToString(),
                    InvDate = record["inv_date"].ToString(),
                    Term = record["term"].ToString(),
                    Amount = record["Amount"].ToString(),
                    StorerKey = record["storerkey"].ToString(),
                });
			}

			return Invoices;
		}

        [HttpGet]
        public List<InvoiceDetails> GetInvoiceDetails(string InvoiceID)
        {
            var dataTable = bOra.GetInvoiceDetails(InvoiceID);
            List<InvoiceDetails> Details = new List<InvoiceDetails>();
            foreach (DataRow record in dataTable.Rows)
            {
                Details.Add(new InvoiceDetails()
                {
                    sl_no = record["sl_no"].ToString(),
                    billedunits = record["billedunits"].ToString(),
                    charge_amount = record["charge_amount"].ToString(),
                    charge_desc = record["charge_desc"].ToString(),
                    marksandnumbers = record["marksandnumbers"].ToString(),
                    rate = record["rate"].ToString(),
                });
            }

            return Details;
           
        }

		
    }
}
