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
    public class AllUsersController : ApiController
    {
        OracleRepository bOra = new OracleRepository();

        [HttpGet]
        public List<mNews> getNews()
        {
            List<mNews> newsList = new List<mNews>();

            foreach(DataRow record in bOra.GetNews().Rows)
            {
                mNews n = new mNews()
                {
                    Title=record["TITLE"].ToString(),
                    Date= record["Date"].ToString(),
                    Description= record["Description"].ToString(),
                    ImageUrl= record["ImageUrl"].ToString()
                };
                newsList.Add(n);
            }

            return newsList;
        }

        [HttpGet]
        public int getNotificationCount()
        {
            return bOra.NotificationsCount(User.Identity.Name);

        }

        [HttpGet]
        public IList<mNotifications> getNotifications()
        {
            List<mNotifications> notyList = new List<mNotifications>();

            var dt = bOra.Notifications(User.Identity.Name);

            foreach (DataRow record in dt.Rows)
            {
                mNotifications noty = new mNotifications()
                {
                    ID = Convert.ToInt32(record["ID"]),
                    Date = DateTime.ParseExact(record["NO_DATE"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                    Description = record["Description"].ToString(),
                    isSeen = Convert.ToInt32(record["IsSeen"]),
                    Title = record["Title"].ToString()
                };

                notyList.Add(noty);
            }

            return notyList;
            
        }

        [HttpGet]
        public bool NotificationSeen()
        {
            return bOra.NotificationsSeen(User.Identity.Name);

        }

        [HttpPost]
        public bool NotificationDelete(string ID)
        {
            return bOra.NotificationDelete(User.Identity.Name, new Guid(ID));

        }

    }
}
