using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

using mService.Models;
using System.Configuration;
using mService.Models;



namespace Repositories
{

    public class OracleRepository 
    {
        OracleConnection oraConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["oraConnectionString"].ConnectionString);

        //============================================ General
        /// <summary>
        /// Execute the query and check if the record exist
        /// </summary>
        /// <param name="_query"></param>
        /// <returns></returns>
        public bool isRecordExist(string _query) {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(_query, oraConnection);
                    OracleDataReader dr=cmd.ExecuteReader();
                    if (dr.Read())
                    {
						oraConnection.Close();
                        return true;
                    }

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return false;
        }

        /// <summary>
        /// Execute the query and returns the numbers of rows affected
        /// </summary>
        /// <param name="_query"></param>
        /// <returns></returns>
        public int ExecuteQuery(string _query)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(_query, oraConnection);
                    return cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return 0;
        }

        //============================================ Accounts

        internal void InsertAccountDetails(string token,string roleName, string Username, string deviceType, string deviceId)
        {

			try {

				oraConnection.Open();
				if (oraConnection.State == ConnectionState.Open)
				{

					OracleCommand cmd = new OracleCommand("INSERT INTO MOB_AUTHENTICATION (TOKEN,ROLENAME,USER_ID,DEVICE_TYPE,DEVICE_ID) VALUES (:TOKEN,:ROLENAME,:USER_ID,:DEVICE_TYPE,:DEVICE_ID) ", oraConnection);
					cmd.Parameters.Add(new OracleParameter("TOKEN",OracleType.VarChar)).Value=token;
					cmd.Parameters.Add(new OracleParameter("ROLENAME", OracleType.VarChar)).Value = roleName;
					cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar)).Value = Username;
					cmd.Parameters.Add(new OracleParameter("DEVICE_TYPE", OracleType.VarChar)).Value = deviceType;
					cmd.Parameters.Add(new OracleParameter("DEVICE_ID", OracleType.VarChar)).Value = deviceId;

					cmd.ExecuteReader();

				}

			} catch (Exception ex) {

			}
			oraConnection.Close();

            
        }

        internal string GetEmpName(string verifiedEmpId)
        {
            throw new NotImplementedException();
        }

        internal mAccounts Verify(string username, string password)
        {
			try
			{

				oraConnection.Open();
				if (oraConnection.State == ConnectionState.Open)
				{
					//str = "select user_id,user_password,user_desc,user_type,user_name,substr(user_desc,1,10),user_screen from login_user where user_id='" & userid.Text & "'"
					OracleCommand cmd = new OracleCommand("DFZDB_CRYPTO.ENCRYPT ", oraConnection);

					OracleParameter p = new OracleParameter("p_key_string", OracleType.VarChar,30);
					p.Value= "GALAXYERP";
					p.Direction = ParameterDirection.Input;
					cmd.Parameters.Add(p);

					OracleParameter p1 = new OracleParameter("P_STRING", OracleType.VarChar,250);
					p1.Value = password;
					p1.Direction = ParameterDirection.InputOutput;
					cmd.Parameters.Add(p1);

					OracleParameter p2 = new OracleParameter("P_ERROR_NO", OracleType.Int32);
					p2.Direction = ParameterDirection.Output;
					cmd.Parameters.Add(p2);


					
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.ExecuteNonQuery();
					var passEncrypt = cmd.Parameters["P_STRING"].Value;


					cmd = new OracleCommand(" select user_id,user_password,user_desc,USER_GRP_ID,user_type,user_name,substr(user_desc,1,10),USER_EMP_CODE,user_screen from login_user where user_id=:Username AND user_password=:Password", oraConnection);
					cmd.Parameters.Add("Username", OracleType.VarChar).Value = username;
					cmd.Parameters.Add("Password", OracleType.VarChar).Value = passEncrypt;
					OracleDataReader dr= cmd.ExecuteReader();
					if (dr.Read())
					{
						mAccounts account = new mAccounts()
						{
							Name = dr["user_name"].ToString(),
							RoleName = dr["USER_GRP_ID"].ToString(),
							EmployeeId = dr["USER_EMP_CODE"].ToString(),
                            ConsigneeId= dr["user_desc"].ToString(),
                            IsValid =true,
							Username=username



						};
						
						oraConnection.Close();
						return account;
					}

				}

			}
			catch (Exception ex)
			{

			}
			oraConnection.Close();
			return null;
		}

        public mAccounts GetUserDetails(string Username) {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(" select user_id,user_password,user_desc,USER_GRP_ID,user_type,user_name,substr(user_desc,1,10),USER_EMP_CODE,user_screen from login_user where user_id=:Username", oraConnection);
                    cmd.Parameters.Add("Username", OracleType.VarChar).Value = Username;
                    
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        mAccounts account = new mAccounts()
                        {
                            Name = dr["user_name"].ToString(),
                            RoleName = dr["USER_GRP_ID"].ToString(),
                            EmployeeId = dr["USER_EMP_CODE"].ToString(),
                            ConsigneeId = dr["user_desc"].ToString(),
                            IsValid = true,
                            Username = Username
                        };

                        oraConnection.Close();
                        return account;
                    }

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return null;
        }
        internal bool SignOutUser(string strToken, string USER_ID)
        {
			try
			{

				

				oraConnection.Open();
				if (oraConnection.State == ConnectionState.Open)
				{

					OracleCommand cmd = new OracleCommand("DELETE FROM MOB_AUTHENTICATION WHERE USER_ID=0:USER_ID AND TOKEN=:TOKEN", oraConnection);
					cmd.Parameters.Add(new OracleParameter("USERNAME", OracleType.VarChar)).Value = USER_ID;

					cmd.ExecuteReader();
					oraConnection.Close();
					return true;

				}

			}
			catch (Exception ex)
			{

			}
			oraConnection.Close();
			return false;
		}

        //============================================ News
        internal DataTable GetNews()
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT TITLE,NEWS_DATE,IMAGE_URL,DESCRIPTION FROM NEWS ORDER BY NEWS_DATE DESC ", oraConnection);

                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return null;
        }

        //============================================ Notifications
        internal bool NotificationDelete(string name, Guid guid)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("DELETE FROM MOB_NOTIFICATIONS WHERE UPPER(Username)=@Username AND ID=@ID ", oraConnection);
                    cmd.Parameters.Add(new OracleParameter("Username", SqlDbType.VarChar)).Value =name.ToUpper();
                    cmd.Parameters.Add(new OracleParameter("ID", SqlDbType.UniqueIdentifier)).Value = ID;

                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        oraConnection.Close();
                        return true;
                    }
                   
                    
                    

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return false;
        }

        internal DataTable Notifications(string name)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT ID,[DATE],Description,IsSeen,Title FROM MOB_Notifications WHERE UPPER(Username)=@Username ORDER BY [Date] DESC", oraConnection);
                    cmd.Parameters.Add(new OracleParameter("Username", SqlDbType.VarChar)).Value = name.ToUpper();

                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return null;
        }

        internal int NotificationsCount(string name)
        {
            int notyCount = 0;

            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT Count(ID) C FROM MOB_Notifications WHERE UPPER(Username)=@Username AND IsSeen=0 ", oraConnection);
                    cmd.Parameters.Add(new OracleParameter("Username", SqlDbType.VarChar)).Value = name.ToUpper();
                    

                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        notyCount = Convert.ToInt32(dr["C"].ToString());

                        oraConnection.Close();
                        return notyCount;
                    }

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return notyCount;
        }

        internal bool NotificationsSeen(string name)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("UPDATE MOB_Notifications SET IsSeen=1 WHERE UPPER(Username)=@Username", oraConnection);
                    cmd.Parameters.Add(new OracleParameter("Username", SqlDbType.VarChar)).Value = name.ToUpper();
                    

                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return true;

                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return false;
        }


        //============================================= Appointments

        internal DataTable GetLoadingPorts()
		{
			try
			{

				oraConnection.Open();
				if (oraConnection.State == ConnectionState.Open)
				{

					OracleCommand cmd = new OracleCommand("SELECT BL_DESC FROM CODELKUP WHERE LISTNAME='LOADINPORT' ORDER BY CODE ", oraConnection);

					DataTable dt = new DataTable();
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dt);
					oraConnection.Close();
					return dt;

				}

			}
			catch (Exception ex)
			{

			}
			oraConnection.Close();
			return null;
		}

		internal DataTable GetSubConsignees(string USER_ID)
		{
			try
			{
				oraConnection.Open();
				if (oraConnection.State == ConnectionState.Open)
				{

					OracleCommand cmd = new OracleCommand(" select * from MOB_SUB_CONSIGNEES where USER_ID=:USER_ID ", oraConnection);
					cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar)).Value = USER_ID;
					
					DataTable dt = new DataTable();
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dt);
					oraConnection.Close();
					return dt;

				}
			}
			catch (Exception ex)
			{

			}
			oraConnection.Close();
			return null;
		}

        public string GetNewRecordNumber()
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(" select nvl(max(TM_KEY),0)+1 from DOCUMENT_SERIES ", oraConnection);
                    
                    OracleDataReader dr = cmd.ExecuteReader();
                    string recNum = null;
                    if (dr.Read())
                    {
                        recNum = dr[0].ToString();
                        dr.Close();
                        OracleCommand cmd1 = new OracleCommand("update DOCUMENT_SERIES set TM_KEY=:RecNum", oraConnection);
                        cmd1.Parameters.Add(new OracleParameter("RecNum", OracleType.VarChar)).Value = recNum; // UP: ColumnType
                        cmd1.ExecuteReader();

                    }

                    return recNum;

                }
            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return null;
        }

		internal DataTable GetAppointments (string USER_ID)
		{
			try
			{



				oraConnection.Open();
				if (oraConnection.State == ConnectionState.Open)
				{

					OracleCommand cmd = new OracleCommand(" select * from MOB_SUB_CONSIGNEES where USER_ID=:USER_ID ", oraConnection);
					cmd.Parameters.Add(new OracleParameter("USER_ID", OracleType.VarChar)).Value = USER_ID;

					DataTable dt = new DataTable();
					OracleDataAdapter da = new OracleDataAdapter(cmd);
					da.Fill(dt);
					oraConnection.Close();
					return dt;

				}

			}
			catch (Exception ex)
			{

			}
			oraConnection.Close();
			return null;
		}

        public bool Save(Appointments app,string Username)
        {
            mAccounts user = GetUserDetails(Username);
            bool rowAffected = false;
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("MOB_TRUCK_MANIFEST", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new OracleParameter("P_STORERKEY", OracleType.VarChar)).Value = user.ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("P_PORTOFLOADING", OracleType.VarChar)).Value = app.PortOfLoading;
                    cmd.Parameters.Add(new OracleParameter("P_MBOLNUMBER", OracleType.VarChar)).Value = app.MasterBolNumber;
                    cmd.Parameters.Add(new OracleParameter("P_BOLNUMBER", OracleType.VarChar)).Value = app.BolNumber;
                    cmd.Parameters.Add(new OracleParameter("P_TRUCKNUMBER", OracleType.VarChar)).Value = app.TruckNumber;
                    cmd.Parameters.Add(new OracleParameter("P_COMMODITY", OracleType.VarChar)).Value = app.Commodity;
                    cmd.Parameters.Add(new OracleParameter("P_PACKAGE", OracleType.VarChar)).Value = app.Package;
                    cmd.Parameters.Add(new OracleParameter("P_QTY", OracleType.VarChar)).Value = app.Qty;
                    cmd.Parameters.Add(new OracleParameter("P_WEIGHT", OracleType.VarChar)).Value = app.Weight;
                    cmd.Parameters.Add(new OracleParameter("P_USER_ID", OracleType.VarChar)).Value = user.Username;
                    

                    if (cmd.ExecuteNonQuery() > 0) { rowAffected = true; }
                    oraConnection.Close();
                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return rowAffected;
        }
        public bool Submit(Appointments app,string Username)
        {
            mAccounts user = GetUserDetails(Username);
            bool rowAffected = false;
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SUBMIT_REQUEST", oraConnection);
                    cmd.CommandType =  CommandType.StoredProcedure;

                    cmd.Parameters.Add(new OracleParameter("P_MM_KEY", OracleType.VarChar)).Value = app.RecNumber;
                    cmd.Parameters.Add(new OracleParameter("P_MAIN_CONSIGNEE", OracleType.VarChar)).Value = user.ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("P_USER_ID", OracleType.VarChar)).Value = user.Username;
                    cmd.Parameters.Add(new OracleParameter("P_TYPE", OracleType.VarChar)).Value = "T";

                    if (cmd.ExecuteNonQuery()>0) { rowAffected = true; }
                    oraConnection.Close();
                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            return rowAffected;
            
        }

        public bool Delete(string recNumber)
        {
            if (isRecordExist("select NVL(TM_STATUS,9) FROM TRUCK_MANIFEST WHERE TM_KEY=nvl('" + recNumber + "','0') and TM_STATUS=0"))
            {
                if(ExecuteQuery("Delete from truck_manifest where tm_key='" + recNumber + "'")>0) {
                    return true;
                }
            }
            return false;
        }


	}
    
   

   
    




}
