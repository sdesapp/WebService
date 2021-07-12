using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mService;
using System.Net.Http;
using mService.Models;

namespace mService.Controllers
{
    public class GmailsendController : Controller
    {
        // GET: Gmailsend
        public ActionResult Index()
        {
            return View();
        }

      //  [HttpPost]
        //public ActionResult Index(WebEmail ec)

        //{
        //    HttpClient hc = new HttpClient();
        //    hc.BaseAddress = new Uri("http://localhost:6363/api/WebEmail/sendmail");
        //    System.Threading.Tasks.Task<HttpResponseMessage> consumewebapi = hc.PostAsJsonAsync<WebEmailController>("WebEmail", ec);
        //    consumewebapi.Wait();
        //    HttpResponseMessage sendmail = consumewebapi.Result;
        //    if(sendmail.IsSuccessStatusCode)
        //    {
        //        ViewBag.message = "the mail has been sent to " + ec.to.ToString();
        //    }


        //    return View();
        //}
        //
    }
}