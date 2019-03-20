
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.SessionState;
using mService.Models;



namespace mService
{
    public class RESTAuthorize : AuthorizeAttribute
    {
        public static string strToken = "";
        private string _roleName { get; set; }

        //  default constructor
        public RESTAuthorize()
        {
            
        }

        // parameteric constructor
        public RESTAuthorize(string RoleName)
        {
            this._roleName = RoleName;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (actionContext == null)
                throw new ArgumentException("filterContext");
            bool isAuthenticated = VerifyUserCredentials(actionContext);
            if (!isAuthenticated)
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"LogInRequired\":\"True\", \"result\":\"Session Expired\"}")
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = responseMessage;

                return;
            }
            return;
        }
        private bool VerifyUserCredentials(HttpActionContext filterContext)
        {
            
            try
            {
                if (filterContext.Request != null)
                {
                    var authKey = filterContext.Request.Headers.GetValues("Auth-Key").ToList();
                    if (authKey != null)
                    {

                        strToken = authKey[0];
                        

                        mMobileAuthentications mMobileAuthentication = ValidateToken(authKey[0]);
                        if (mMobileAuthentication != null)
                        {

                            SetPrincipal(mMobileAuthentication.USER_ID, mMobileAuthentication.RoleName);

                            if (!string.IsNullOrEmpty(this._roleName))
                            {
                                if (this._roleName == mMobileAuthentication.RoleName)
                                {
                                    // return true if authenticated && matches the rolename
                                    return true;
                                }
                            }
                            else
                            {
                                // return true if only authenticated
                                return true;
                            }

                            
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                HttpActionContext actionContext = new HttpActionContext();
                HttpResponseMessage responseMessage = new HttpResponseMessage()
                {
                    Content = new StringContent("{\"IsAuthenticated\":\"True\", \"result\":\"Invalid Credentials\"}")
                };
                responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = responseMessage;
            }
            return false;
        }

        public static mMobileAuthentications ValidateToken(string strToken)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["oraConnectionString"].ToString();

            SqlConnection con =
            new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd =
                    new SqlCommand(" SELECT TOKEN,ROLENAME,USER_ID FROM MOB_AUTHENTICATION WHERE TOKEN=@token", con);

                cmd.Parameters.Add(new SqlParameter("token", SqlDbType.VarChar)).Value = strToken;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new mMobileAuthentications()
                    {
                        Token = dr.GetValue(0).ToString(),
                        RoleName = dr.GetValue(1).ToString(),
						USER_ID = dr.GetValue(2).ToString()
                    };
                }

                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                throw new Exception(ex.Message);
            }

            return null;
        }

        public void SetPrincipal(string Username,string RoleName)
        {
            
            var customIdentity=new GenericIdentity(Username);

            IPrincipal principal = new GenericPrincipal(customIdentity, new[] { RoleName });
            Thread.CurrentPrincipal = principal;
            // Verification.   
            if (HttpContext.Current != null)
            {
                // Setting.   
                HttpContext.Current.User = principal;
            }
        }

    }
    
}
