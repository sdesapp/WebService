using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mService.Models
{
    

    public class Invoice
    {
        public string InvNumber { get; set; }
        public string InvDate { get; set; }
       public string Term { get; set; }
        public string Amount { get; set; }
        public string StorerKey { get; set; }

    }

    public class InvoiceDetails
    {
        public string sl_no { get; set; }
        public string charge_desc { get; set; }
        public string billedunits { get; set; }
        public string rate { get; set; }
        public string charge_amount { get; set; }
        public string marksandnumbers { get; set; }



    }



}