using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mService.Models;
using System.Net.Mail;

namespace mService.Controllers
{
    public class WebEmailController : ApiController
    {
        public IHttpActionResult sendmail(WebEmail ec)
        {
            string subject = ec.subject;
            string body = ec.body;
            string to = ec.to;
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("shahnawazsf@gmail.com");
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("shahnawazsf@gmail.coim", "Jan@2019");
            smtp.Send(mm);
            return Ok();


        }
    }
}
