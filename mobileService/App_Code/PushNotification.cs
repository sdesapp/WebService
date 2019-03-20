using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using PushSharp;
using PushSharp.Google;
using PushSharp.Apple;

namespace mService
{
    public class PushNotification
    {

        public static void SendNotificationToIOS(object objParameters)
        {
            List<object> objs = objParameters as List<object>;
            string strMessage = (string)objs[0];
            string strTokenIds = (string)objs[1];



            // Configuration (NOTE: .pfx can also be used here)
            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production,
        System.Web.Hosting.HostingEnvironment.MapPath("~/Resources/ios/[AppName].PushCert.Production.p12"),
        ConfigurationManager.AppSettings["iosP12Password"].ToString());

            // Create a new broker
            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        //Console.WriteLine ($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}",apnsNotification.Identifier,statusCode);

                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException           
                        //Console.WriteLine ($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) => {
                Console.WriteLine("Apple Notification Sent!");
            };

            // Start the broker
            apnsBroker.Start();

            // Queue a notification to send
            apnsBroker.QueueNotification(new ApnsNotification
            {
                DeviceToken = strTokenIds,
                Payload = JObject.Parse("{\"aps\":{\"badge\":1,\"alert\":" + "\"" + strMessage + "\",\"sound\":\"cat.caf\"}}")
            });

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            apnsBroker.Stop();

        }

        public static void SendNotificationToAndroid(object objParameters)
        {
            List<object> objs = objParameters as List<object>;
            string strMessage = (string)objs[0];
            string DeviceId = (string)objs[1];
            string strTitle = (string) objs[2];

            // Configuration
            var config = new GcmConfiguration("------------",
                "AAAAciE9MkQ:------------------------------",
                null);
            config.GcmUrl = "https://fcm.googleapis.com/fcm/send";

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            Console.WriteLine($"GCM Notification Succeeded: ID={succeededNotification.MessageId}");
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e = failedKvp.Value;

                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc={e.Message}");
                        }

                    }
                    else 
                    {
                        Console.WriteLine("GCM Notification Failed for some unknown reason: "+ex.Message );
                    }
                    

                    // Mark it as handled
                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) => {
                Console.WriteLine("GCM Notification Sent!");
            };

            // Start the broker
            gcmBroker.Start();

            
            //collapse_key, notId ??


            string postData = "{\"collapse_key\": " + "\"" + strTitle + "\",\"time_to_live\":108,\"delay_while_idle\":true,\"data\": { \"notId\":\" "+strTitle+" \",  \"title\":\""+ strTitle + "\" ,\"message\" : " + "\"" + strMessage + "\",\"time\": " + "\"" + System.DateTime.Now.ToString() + "\"},\"registration_ids\":[\"" + DeviceId + "\"]}";
            gcmBroker.QueueNotification(new GcmNotification
            {
                RegistrationIds = new List<string> {DeviceId},
                Data = JObject.Parse(postData) 
            });


            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();
        }

       
    }
}