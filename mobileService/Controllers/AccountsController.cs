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

        //[HttpGet]
        //public mAccounts Test(string u,string p)
        //{
        //   return VerifyUser(new mAccounts() {
        //        Username =u,
        //        Password =p,
        //        DeviceId = "no-id",
        //        DeviceType = "web"
        //    });
        //}

        [HttpPost,HttpGet]
        public mAccounts VerifyUser(mAccounts credentials)
        {

            try
            {
				var  verifiedEmp = bOra.Verify(credentials.Username, credentials.Password);

                // For Customer EmployeeId=""
                // For Employee EmployeeId="XXXX"
                // For Invalid  EmployeeId="N"

                if (verifiedEmp != null && verifiedEmp.EmployeeId.TrimEnd()!="N")
                {
                    string token = (Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray())).Replace("=", "");
                    bOra.InsertAccountDetails(token, verifiedEmp.RoleName, credentials.Username,  credentials.DeviceType, credentials.DeviceId);

					verifiedEmp.Token = token;
					return verifiedEmp;
				}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                // invalid credentials
            }
            

            return null;

        }


        [HttpPost, HttpGet]
        public mAccounts VerifyUser1()
        {

            try
            {
                mAccounts credentials = new mAccounts()
                {
                    Username = "ars",
                    Password = "1234567",
                    DeviceId = "no-id",
                    DeviceType = "web"
                };

                //Active Directory User Login

                var verifiedEmp = bOra.Verify(credentials.Username, credentials.Password);
               // return verifiedEmp;

                if (verifiedEmp != null)
                {
                    string token = (Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray())).Replace("=", "");
                    bOra.InsertAccountDetails(token, verifiedEmp.RoleName, credentials.Username, credentials.DeviceType, credentials.DeviceId);

                    verifiedEmp.Token = token;
                    return verifiedEmp;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
