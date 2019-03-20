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

namespace mService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RESTAuthorize]
    public class AllUsersController : ApiController
    {
        [HttpGet]
        public object getNews()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cStringServices"].ToString();

            SqlConnection con =
            new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd =
                    new SqlCommand("SELECT TITLE,NEWS_DATE,IMAGE_URL,DESCRIPTION FROM NEWS ORDER BY NEWS_DATE DESC", con);
                List<mNews> NewsList = new List<mNews>();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mNews news=new mNews();

                    news.Title = dr["TITLE"].ToString();
                    news.Date = DateTime.ParseExact(dr["NEWS_DATE"].ToString(),"dd/MM/yyyy hh:mm:ss",CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    news.ImageUrl = dr["IMAGE_URL"].ToString();
                    news.Description = dr["DESCRIPTION"].ToString();


                    NewsList.Add(news);
                }
                con.Close();
                return NewsList;

            }
            catch (Exception ex)
            {
                con.Close();
                return (ex.Message);
            }

            return null;

        }

        [HttpGet]
        public int getNotificationCount()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cStringServices"].ToString();

            SqlConnection con =
            new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd =
                    new SqlCommand("SELECT Count(ID) C FROM MOB_Notifications WHERE UPPER(Username)=@Username AND IsSeen=0 ", con);
                cmd.Parameters.Add(new SqlParameter("Username", SqlDbType.VarChar)).Value = User.Identity.Name.ToUpper();

                

                SqlDataReader dr = cmd.ExecuteReader();
                int notyCount = 0;
                while (dr.Read())
                {
                    notyCount = Convert.ToInt32(dr["C"].ToString());
                }
                con.Close();
                return notyCount;

            }
            catch (Exception ex)
            {
                con.Close();
                //throw new Exception(ex.Message);
            }

            return 0;
        }

        [HttpGet]
        public IList<mNotifications> getNotifications()
        {
            
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cStringServices"].ToString();

            SqlConnection con =
            new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd =
                    new SqlCommand("SELECT ID,[DATE],Description,IsSeen,Title FROM MOB_Notifications WHERE UPPER(Username)=@Username ORDER BY [Date] DESC", con);
                cmd.Parameters.Add(new SqlParameter("Username", SqlDbType.VarChar)).Value = User.Identity.Name.ToUpper();

                List<mNotifications> notyList = new List<mNotifications>();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mNotifications noty = new mNotifications()
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Date = DateTime.ParseExact(dr["Date"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                        Description = dr["Description"].ToString(),
                        isSeen = Convert.ToInt32(dr["IsSeen"]),
                        Title = dr["Title"].ToString()
                    };
                    


                    notyList.Add(noty);
                }
                con.Close();
                return notyList;

            }
            catch (Exception ex)
            {
                con.Close();
                //throw new Exception(ex.Message);
            }

            return null;
        }

        [HttpGet]
        public bool NotificationSeen()
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cStringServices"].ToString();

            SqlConnection con =
            new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd =
                    new SqlCommand("UPDATE MOB_Notifications SET IsSeen=1 WHERE UPPER(Username)=@Username ", con);
                cmd.Parameters.Add(new SqlParameter("Username", SqlDbType.VarChar)).Value =User.Identity.Name.ToUpper();
                cmd.ExecuteReader();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                con.Close();
                //throw new Exception(ex.Message);
            }

            return false;

        }

        [HttpPost]
        public bool NotificationDelete(string ID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cStringServices"].ToString();

            SqlConnection con =
            new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd =
                    new SqlCommand("DELETE FROM MOB_NOTIFICATIONS WHERE UPPER(Username)=@Username AND ID=@ID", con);
                cmd.Parameters.Add(new SqlParameter("Username", SqlDbType.VarChar)).Value =User.Identity.Name.ToUpper();
                cmd.Parameters.Add(new SqlParameter("ID", SqlDbType.UniqueIdentifier)).Value = new Guid(ID);
                cmd.ExecuteReader();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                con.Close();
                
            }

            return false;
        }

    }
}
