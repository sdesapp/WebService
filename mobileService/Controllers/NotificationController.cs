using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Repositories;

namespace mService.Controllers
{
    public class NotificationController : ApiController
    {
        OracleRepository bOra = new OracleRepository();

        [HttpGet]
        public bool Notify(string Title,string Msg,string Username)
        {

            try
            {


                DataTable dt = bOra.Notifications(User.Identity.Name);

                if (dt != null)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        List<object> obj = new List<object>(){
                        Msg,
                        row["DEVICE_ID"].ToString(),Title};

                        if (row["DEVICE_TYPE"].ToString().ToLower() == "android")
                        {

                            new Thread(() =>
                            {
                                ThreadPool.QueueUserWorkItem(new WaitCallback(PushNotification.SendNotificationToAndroid), obj);
                            }).Start();

                        }

                        if (row["DEVICE_TYPE"].ToString().ToLower() == "ios")
                        {
                            new Thread(() =>
                            {
                                ThreadPool.QueueUserWorkItem(new WaitCallback(PushNotification.SendNotificationToIOS), obj);
                            }).Start();

                        }
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

            
            //return false;
        }

        
    }
}
