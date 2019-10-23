using System;
using System.Data;
using System.Data.OracleClient;

using mService.Models;
using System.Configuration;

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
        // Get Leave and Procurment approval list
        internal  DataTable GetApprovalList(string username)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("select vardoc_no, varemp_code, vardept_code, varleave_type, varleave_desc, varstart_date, varend_date, varannual_days, varother_days, " +
                                            "varunpaid_days, vartotal_days,varinternal_external,varleave_status from TABLE(emp_leave_val(:username)) ", oraConnection);

                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("username", OracleType.VarChar)).Value = username;
                        DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            oraConnection.Close();
            return null;
        }


        //get PROC APPROVAL LIST
        internal DataTable GetProcList(string username)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT REQ_TYPE,REQ_NO,REQ_DATE,REQ_BY,REQ_DEPT_CODE,REQ_DEPT_HEAD,REQ_PURPOSE,SUBMIT,REQ_STATUS,REQ_STATUS_DESC FROM TABLE(proc_app_list(:username))", oraConnection);

                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("username", OracleType.VarChar)).Value = username;
                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            oraConnection.Close();
            return null;
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
  
                    OracleCommand cmd = new OracleCommand("GET_USER_DETAIL", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_USER_ID", OracleType.VarChar).Value = username;
                    cmd.Parameters.Add("P_PASSWORD", OracleType.VarChar).Value = password;
                    
                    OracleParameter param = new OracleParameter("P_USER_NAME", OracleType.Char);
                    param.Size = 400;
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    OracleParameter param1 = new OracleParameter("P_USER_GRP_ID", OracleType.Char);
                    param1.Size = 400;
                    param1.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param1);

                    OracleParameter param2 = new OracleParameter("P_USER_EMP_CODE", OracleType.Char);
                    param2.Size = 400;
                    param2.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param2);

                    OracleParameter param3 = new OracleParameter("P_USER_DESC", OracleType.Char);
                    param3.Size = 400;
                    param3.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param3);

                    OracleParameter param4 = new OracleParameter("P_STATUS", OracleType.Char);
                    param4.Size = 400;
                    param4.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param4);

                    int res=cmd.ExecuteNonQuery();                    
                                        
                     if (res>0)
                      {
                        string strEmpId = cmd.Parameters["P_USER_EMP_CODE"].Value.ToString();
                        string roleName = "Employee";
                        if (string.IsNullOrEmpty(strEmpId)) { roleName = "Customer"; }

                          mAccounts account = new mAccounts()
                          {
                              Name = cmd.Parameters["P_USER_NAME"].Value.ToString(),
                              EmployeeId= strEmpId,
                              RoleName = roleName,
                              ConsigneeId = cmd.Parameters["P_USER_DESC"].Value.ToString(),
                              IsValid = true,
                              Username = username
                          };
                    
                        oraConnection.Close();
                        return account;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        //============================================ Billing
        internal DataTable GetInvoice(string ConsigneeId,string BLNumber,string ContainerNo,string FromDate,string ToDate) {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(  " select distinct im.inv_no as inv_no,to_char(inv_date,'dd/mm/yyyy') as inv_date,decode(term,'CA','Cash','CR','Credit') "+
                                                            " as term, to_char(total, '999,999,999.99') as Amount, im.storerkey as storerkey "+
                                                            " from invoice_master im, invoice_detail id, operationdetails od " +
                                                            " where id.inv_no = im.inv_no " +
                                                            " and od.toid = id.id " +
                                                            " and od.lot = id.lot " +
                                                            " and im.storerkey = :ConsigneeID " +
                                                            " and substr(id.id, 5, length(id.id)) = nvl(:Container, substr(id.id, 5, length(id.id))) " +
                                                            " and od.bolnumber = nvl(:BLNumber, od.bolnumber) " +
                                                            " and im.storerkey like 'SDRS%' and " +
                                                            " to_CHAR(inv_date, 'YYYY/MM/DD') between nvl(:FromDate, to_char(inv_date, 'YYYY/MM/DD')) and nvl(:ToDate, to_char(inv_date, 'YYYY/MM/DD')) " +
                                                            " order by  inv_no ", oraConnection);

                    cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("BLNumber", OracleType.VarChar)).Value = string.IsNullOrEmpty(BLNumber)?string.Empty:BLNumber;
                    cmd.Parameters.Add(new OracleParameter("Container", OracleType.VarChar)).Value = string.IsNullOrEmpty(ContainerNo) ? string.Empty : ContainerNo; 
                    cmd.Parameters.Add(new OracleParameter("FromDate", OracleType.VarChar)).Value = string.IsNullOrEmpty(FromDate) ? string.Empty : FromDate; 
                    cmd.Parameters.Add(new OracleParameter("ToDate", OracleType.VarChar)).Value = string.IsNullOrEmpty(ToDate) ? string.Empty : ToDate;


                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
            oraConnection.Close();
            return null;
        }

        internal DataTable GetInvoiceDetails(string InvoiceID) {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(  "select sl_no,charge_desc,to_char(billedunits,'999') as billedunits,to_char(RATE,'999,999,999.99') as rate,to_char(CHARGE_AMOUNT,'999,999,999.99') as charge_amount, "+
                                                            " marksandnumbers from invoice_detail where inv_no = :invoice_id order by sl_no ", oraConnection);
                    cmd.Parameters.Add(new OracleParameter("invoice_id", OracleType.VarChar)).Value = InvoiceID;

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


        //============================================ Container Status
        internal DataTable GetContainer(string ConsigneeId, string ContainerNo)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(" select od.bolnumber as bolnumber, substr(toid, 5, length(toid)) as Container_no, decode(sku, 'CNT20', '20 Ft.', 'CNT40', '40 Ft', 'CNT45', '45 Ft.') as sku," +
                                                           " carrierreference, company, curr_loc, od.storerkey as storerkey, od.sku as sku1, od.lot as lot" +
                                                           " from storer s, receipt r, operationdetails od " +
                                                           " where r.storerkey = s.storerkey and od.receiptkey = r.receiptkey and " +
                                                           " sku like 'CNT%' and r.storerkey like 'SDRS%' and  lot = (select max(TOLOT) " +
                                                           " from receiptdetail where substr(toid,5,length(toid))=nvl(:ContainerNo,substr(toid,5,length(toid)))) " +
                                                           " and r.storerkey in nvl(:ConsigneeID,r.storerkey) " +
                                                           " and substr(toid, 5, length(toid)) = " +
                                                           " nvl(:ContainerNo, substr(toid, 5, length(toid)))", oraConnection);

                 //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                      cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = string.IsNullOrEmpty(ConsigneeId) ? string.Empty : ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("ContainerNo", OracleType.VarChar)).Value = ContainerNo;
                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            oraConnection.Close();
            return null;
        }

        internal DataTable GetContainerDetails(string ConsigneeId, string ContainerNo)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(" select 'Received in Bonded Zone'as status,to_char(datereceived,'dd/mm/yyyy HH24:MI:SS') as date1 ,datereceived as date2 from receiptdetail where " +
                                                          " toid='CNT-'||:ContainerNo and storerkey =nvl(:ConsigneeID,storerkey) and tolot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) union all " +
                                                          " select 'Moved to Strip Position :'||toloc,to_char(trans_date, 'dd/mm/yyyy HH24:MI:SS'),trans_date from cont_movement_detail where " +
                                                          " container_no='CNT-'||:ContainerNo and lot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) and fromloc='SHIFTOUT' and storerkey=nvl(:ConsigneeID,storerkey) union all " +
                                                          " select description1, to_char(effectivedate, 'dd/mm/yyyy HH24:MI:SS'), effectivedate " +
                                                          " from containerstatus_view where TOSTORERKEY=nvl(:ConsigneeID,TOSTORERKEY) and toid='CNT-'||:ContainerNo and tolot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) union all " +
                                                          " select 'Shipped from Bonded Zone',to_char(effectivedate, 'dd/mm/yyyy HH24:MI:SS'),effectivedate from pickdetail " +
                                                          " where storerkey=nvl(:ConsigneeID,storerkey) and id='CNT-'||:ContainerNo and lot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) order by 3", oraConnection);

                    cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = string.IsNullOrEmpty(ConsigneeId) ? string.Empty : ConsigneeId;
                    //cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("ContainerNo", OracleType.VarChar)).Value = ContainerNo;
                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            oraConnection.Close();
            return null;
        }

        internal DataTable GetContainerStatus(string ContainerNo)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(" Select ENG_DESC from codelkup where listname = 'CNTSTATUS' and code = (select containerstatus from id where" +
                                                          " substr(id, 5, length(id)) = nvl(:ContainerNo, substr(id, 5, length(id)))) ", oraConnection);

                   
                    cmd.Parameters.Add(new OracleParameter("ContainerNo", OracleType.VarChar)).Value = ContainerNo;
                    DataTable dt = new DataTable();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                    oraConnection.Close();
                    return dt;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            oraConnection.Close();
            return null;
        }


        //============================================ News
        internal DataTable GetNews()
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT TITLE,NEWS_DATE,IMAGE_URL,DESCRIPTION FROM NEWS ORDER BY NEWS_DATE DESC", oraConnection);

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
                    cmd.Parameters.Add(new OracleParameter("ID", SqlDbType.UniqueIdentifier)).Value = guid;

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

                    OracleCommand cmd = new OracleCommand("SELECT ID,NO_DATE,DESCRIPTION,ISSEEN,TITLE FROM MOB_NOTIFICATIONS  WHERE username=:Username ORDER BY NO_DATE DESC", oraConnection);
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
