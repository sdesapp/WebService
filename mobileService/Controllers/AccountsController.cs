using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using mService.Models;
using System.Web;
using Repositories;

namespace mService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountsController : ApiController
    {
        OracleRepository bOra = new OracleRepository();

        [HttpPost,HttpGet]
        public mAccounts VerifyUser() //(mAccounts credentials)
        {

            try
            {
				mAccounts credentials = new mAccounts() {
					Username= "sfaridi",
					Password= "1234567",
					DeviceId="no-id",
					DeviceType="web"
				};

				//Active Directory User Login

				var  verifiedEmp = bOra.Verify(credentials.Username, credentials.Password);


                if (verifiedEmp != null)
                {
                    string token = (Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray())).Replace("=", "");
                    bOra.InsertAccountDetails(token, verifiedEmp.RoleName, credentials.Username,  credentials.DeviceType, credentials.DeviceId);

					verifiedEmp.Token = token;
					return verifiedEmp;
				}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // invalid credentials
            }
            

            return null;

        }

        [HttpGet]
        [RESTAuthorize]
        public bool SignOut()
        {
            return bOra.SignOutUser(RESTAuthorize.strToken, User.Identity.Name);            
        }

       
    }


}
