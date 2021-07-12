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

        public OracleParameter Param { get; private set; }

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

        // Get Leave Count 
        internal DataTable GetApprovalCount(string username)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("select count(1) as Count from TABLE(emp_leave_val(:username)), EMP_MASTER where varemp_code = emp_code", oraConnection);

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


        // Procurment count 
        internal DataTable GetProcCount(string username)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT Count(1) as Count FROM TABLE(PROC_APPROVAL_LIST(:username))", oraConnection);

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

        // Get Leave approval list
        internal  DataTable GetApprovalList(string username)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)

                {

                    OracleCommand cmd = new OracleCommand(" select vardoc_no, varemp_code,emp_name, vardept_code, varleave_type, varleave_desc, varstart_date, varend_date, varannual_days, varother_days," +
                                                          " varunpaid_days, vartotal_days, varinternal_external, varleave_status, elam_file_name from TABLE(emp_leave_val(:username)) , EMP_MASTER, emp_leave_application_master " +
                                                          " where varemp_code = emp_code and vardoc_no = elam_doc_no ", oraConnection);

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

       

        // Get pending Contracts
        internal DataTable GetContractApprovalList(string username)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("select cont_id,cust_name,cust_bl_name,start_date,end_date from TABLE(WEB_CUST_CONTRACT_APPR_LIST(:username))", oraConnection);

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



        internal DataTable GetContractDetail(string custid)
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("select CB_CONT_ID,CB_STORERKEY,CB_NAME,CB_BL_NAME,CB_ADDRESS,CB_CITY,CB_VAT_REG_NUMBER,CB_CONTACT1,CB_PHONE1,CB_MOBILE1,CB_EMAIL1,CB_SIGNATORY,CB_BL_SIGNATORY,CB_SIGNATORY_TITLE,CB_SIGNATORY_BL_TITLE,CB_SIGNED_DATE,CB_CR_NUMBER,CB_CR_EXPIRE_DATE,CB_CR_ATTACH_FILE,CB_VAT_ATTACH_FILE,CC_CONT_ID,CC_START_DATE," +
                                                         "CC_END_DATE,DECODE(CC_CUST_CATEGORY,001, 'PROPERTY LEASED',002,'HANDLING SERVICES') CC_CUST_CATEGORY,CC_WH_LEASE_FLAG,CC_COVS_LEASE_FLAG,CC_OPENY_LEASE_FLAG,CC_OFFICE_LEASE_FLAG,CC_AUTOZONE_LEASE_FLAG,CC_CHEMZONE_LEASE_FLAG,DECODE(CC_HI_BILL_PAY_MODE,'CA', 'CASH','CR', 'CREDIT') CC_HI_BILL_PAY_MODE,CC_HI_BILL_FLAG,CC_CONT_DEM_BILL_FLAG,CC_CARGO_DEM_BILL_FLAG,CC_CARGO_INSP_FLAG,CC_CONT_TYPE,CC_CONT_FREE_DAYS,CC_CARGO_FREE_DAYS,DECODE(CC_CONT_DEM_CALC_BY,001, 'By Container',002,'By Weight',003,'By Area Size') CC_CONT_DEM_CALC_BY,CC_CONT_DEM_CHARGES," +
                                                        "DECODE(CC_CARGO_DEM_CALC_BY,001, 'By Weight',002, 'By Area Size') CC_CARGO_DEM_CALC_BY,CC_CARGO_DEM_CHARGES,CC_TERMINATION_DAYS,CC_ATTACH_FILE,CC_STATUS,CC_TARIFF_AFFECTED_FLAG,CC_HI_CREDIT_PERIOD,CC_HI_CREDIT_LIMIT from CUSTOMER_BASIC a, CUSTOMER_CONTRACT b where CB_CONT_ID = :custid and CB_CONT_ID = CC_CONT_ID", oraConnection);

                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("custid", OracleType.VarChar)).Value = custid;
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



        internal DataTable GetContractTariff(string custid1)
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("select DESCRIP,CT_ORIG_PRICE,CT_CHANGE_PERC,CT_FINAL_PRICE from CUSTOMER_TARIFF A," +
                                                          "GLDISTRIBUTION B where ct_cont_id= :custid1 AND a.ct_charge_code=B.GLDISTRIBUTIONKEY ORDER BY a.ct_sl_no", oraConnection);

                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("custid1", OracleType.VarChar)).Value = custid1;
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


        internal DataTable GetContractLease(string custid2) //LEASE
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("SELECT CLPD_CONT_ID,CLPD_LOC,CLPD_AREA,DECODE(CLPD_BILL_FREQUENCY,01,'Per Annum',02, 'Six Months', 03,'Quarterly',04, 'Monthly') CLPD_BILL_FREQUENCY, CLPD_START_DATE, CLPD_END_DATE,CLPD_ELECTRICITY, CLPD_CLEANING, CLPD_IT_SERVICES FROM CUSTOMER_LEASE_PROPERTY_DETAIL where CLPD_CONT_ID =:custid2", oraConnection);                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("custid2", OracleType.VarChar)).Value = custid2;
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



        internal DataTable GetContractSales(string custid4) //Sales
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("select a.CST_CONT_ID,a.CST_EMP_CODE,a.CST_REMARKS,b.EMP_NAME FROM CUSTOMER_SALES_TEAM a, EMP_MASTER b  " +
                        "where a.cst_emp_code = b.emp_code and  a.cst_cont_id =:custid4", oraConnection);                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("custid4", OracleType.VarChar)).Value = custid4;
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



        internal DataTable GetContractPrivilege(string custid4) //SPECIAL_PREVILEGE
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("SELECT CSP_CONT_ID, CSP_DOBEFORECNTARRIVALFLAG FROM CUSTOMER_SPECIAL_PREVILEGE where csp_cont_id=:custid4", oraConnection);                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("custid4", OracleType.VarChar)).Value = custid4;
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


        public bool ApproveContract(string count_id, string user_id, string ip_address, out string P_MSG_ID)
        {
            bool rowAffected = false;
            P_MSG_ID = null;
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("CUSTOMER_CONTRACT_APPROVAL_PROCESS.APPROVE", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("P_USER_ID", OracleType.VarChar, Username, ParameterDirection.Input);

                    OracleParameter Param1 = new OracleParameter();
                    Param1.ParameterName = "P_CONT_ID";
                    Param1.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param1.Direction = ParameterDirection.Input;
                    //       Param.Size = 88;
                    Param1.Value = count_id;
                    cmd.Parameters.Add(Param1);

                    OracleParameter Param2 = new OracleParameter();
                    Param2.ParameterName = "P_USER_ID";
                    Param2.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param2.Direction = ParameterDirection.Input;
                    //   Param.Size = 88;
                    Param2.Value = user_id;
                    cmd.Parameters.Add(Param2);

                    OracleParameter Param3 = new OracleParameter();
                    Param3.ParameterName = "P_IP_ADDRESS";
                    Param3.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param3.Direction = ParameterDirection.Input;
                    Param3.Value = ip_address;
                    cmd.Parameters.Add(Param3);

                    OracleParameter Param4 = new OracleParameter();
                    Param4.ParameterName = "P_MSG_ID";
                    Param4.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param4.Direction = ParameterDirection.Output;
                    Param4.Size = 250;
                    cmd.Parameters.Add(Param4);


                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        rowAffected = true;
                        oraConnection.Close();
                        return rowAffected;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            //   message = "";
            return rowAffected;

        }






        public bool RejectContract(string count_id, string rej_reason, string user_id, string ip_address, out string message)
        {
            bool rowAffected = false;
            message = null;
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("CUSTOMER_CONTRACT_APPROVAL_PROCESS.REJECT", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("P_USER_ID", OracleType.VarChar, Username, ParameterDirection.Input);

                    OracleParameter Param2 = new OracleParameter();
                    Param2.ParameterName = "P_CONT_ID";
                    Param2.DbType = DbType.Int32; // don't use OracleDbType, will return decimal in some cases, not int
                    Param2.Direction = ParameterDirection.Input;
                    Param2.Value = count_id;
                    cmd.Parameters.Add(Param2);

                    OracleParameter Param4 = new OracleParameter();
                    Param4.ParameterName = "P_REJ_REASON";
                    Param4.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param4.Direction = ParameterDirection.Input;
                    Param4.Value = rej_reason;
                    cmd.Parameters.Add(Param4);

                    OracleParameter Param1 = new OracleParameter();
                    Param1.ParameterName = "P_USER_ID";
                    Param1.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param1.Direction = ParameterDirection.Input;
                    Param1.Value = user_id;
                    cmd.Parameters.Add(Param1);


                    OracleParameter Param3 = new OracleParameter();
                    Param3.ParameterName = "P_IP_ADDRESS";
                    Param3.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param3.Direction = ParameterDirection.Input;
                    Param3.Value = ip_address;
                    cmd.Parameters.Add(Param3);


                    Param = new OracleParameter();
                    Param.ParameterName = "P_MSG_ID";
                    Param.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param.Direction = ParameterDirection.Output;
                    Param.Size = 250;
                    cmd.Parameters.Add(Param);


                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        rowAffected = true;
                        message = cmd.Parameters["P_MSG_ID"].Value.ToString();
                        oraConnection.Close();
                        return rowAffected;

                        //  rowAffected = true;
                        //string message = cmd.Parameters["P_MSG_ID"].Value.ToString();
                        //  oraConnection.Close();
                        //  return rowAffected;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            message = "";
            return rowAffected;

        }



        public bool ApproveLeave(String doc_no, string Username,out string message)
        {
            bool rowAffected = false;
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("APPROVE_LEAVE", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("P_USER_ID", OracleType.VarChar, Username, ParameterDirection.Input);

                    Param = new OracleParameter();
                    Param.ParameterName = "P_DOC_NO";
                    Param.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param.Direction = ParameterDirection.Input;
                    Param.Size = 88;
                    Param.Value = doc_no;
                    cmd.Parameters.Add(Param);

                    OracleParameter Param1 = new OracleParameter();
                    Param1.ParameterName = "P_USER_ID";
                    Param1.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param1.Direction = ParameterDirection.Input;
                    Param1.Value = Username;
                    cmd.Parameters.Add(Param1);
                    
                    OracleParameter Param2 = new OracleParameter();
                    Param2 = new OracleParameter();
                    Param2.ParameterName = "P_MSG_TEXT";
                    Param2.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param2.Direction = ParameterDirection.Output;
                    Param2.Size = 250;
                    cmd.Parameters.Add(Param2);
              //      ora_cmd.Parameters.Add("P_MSG_TEXT", OracleDbType.Varchar2, message, ParameterDirection.Output);

                    OracleParameter Param3 = new OracleParameter();
                    Param3.ParameterName = "P_LANG";
                    Param3.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param3.Direction = ParameterDirection.Input;
                    Param3.Value = "E";
                    cmd.Parameters.Add(Param3);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        rowAffected = true;
                       message = cmd.Parameters["P_MSG_TEXT"].Value.ToString();
                        oraConnection.Close();
                        return rowAffected;
                    }                    
                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
            message = "";
            return rowAffected;

        }

        //Reject Leave


        public bool RejectLeave(string doc_no,string Username, string Rejreason, out string message)
        {
            bool rowAffected = false;
        
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("REJECT_LEAVE", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("P_USER_ID", OracleType.VarChar, Username, ParameterDirection.Input);

                    OracleParameter Param2 = new OracleParameter();
                    Param2.ParameterName = "P_DOC_NO";
                    Param2.DbType = DbType.Int32; // don't use OracleDbType, will return decimal in some cases, not int
                    Param2.Direction = ParameterDirection.Input;
                    Param2.Value = doc_no;
                    cmd.Parameters.Add(Param2);

                    OracleParameter Param1 = new OracleParameter();
                    Param1.ParameterName = "P_USER_ID";
                    Param1.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param1.Direction = ParameterDirection.Input;
                    Param1.Value = Username;
                    cmd.Parameters.Add(Param1);

                    OracleParameter Param4 = new OracleParameter();
                    Param4.ParameterName = "P_REASON";
                    Param4.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param4.Direction = ParameterDirection.Input;
                    Param4.Value = Rejreason;
                    cmd.Parameters.Add(Param4);

                    Param = new OracleParameter();
                    Param.ParameterName = "P_MSG_TEXT";
                    Param.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param.Direction = ParameterDirection.Output;
                    Param.Size = 250;
                    cmd.Parameters.Add(Param);


                    OracleParameter Param3 = new OracleParameter();
                    Param3.ParameterName = "P_LANG";
                    Param3.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param3.Direction = ParameterDirection.Input;
                    Param3.Value = "E";
                    cmd.Parameters.Add(Param3);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        rowAffected = true;
                        message = cmd.Parameters["P_MSG_TEXT"].Value.ToString();
                        oraConnection.Close();
                        return rowAffected;
                    }
                }

            }
            catch (Exception ex)
            {
                
            }
            oraConnection.Close();
            message = "";
            return rowAffected;

        }



      

        //get Procurment approval APPROVAL LIST
        internal DataTable GetProcList(string username)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT REQ_TYPE,REQ_NO,REQ_DATE,REQ_BY,REQ_DEPT_CODE,REQ_DEPT_HEAD,REQ_PURPOSE,SUBMIT,REQ_STATUS,REQ_STATUS_DESC FROM TABLE(PROC_APPROVAL_LIST(:username))", oraConnection);

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

        //GET Procurmenet details 
        internal DataTable GetProcDetail(string reqno)
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("select REQ_TYPE,REQ_NO,REQ_SR_NO,REQ_ITEM_CODE,REQ_ITEM_NAME,MODELCODE,REQ_ITEM_SPECIFICATION,REQ_QTY,REQ_UNIT_OF_MEASURE,REQ_REF_NO,REQ_UNIT_PRICE,REQ_TOTAL_PRICE,REQ_RECVD_QTY,REQ_VENDOR_TYPE from PROCUREMENT_REQUEST_DETAIL  where REQ_NO =:reqno order by REQ_SR_NO", oraConnection);

                    //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
                    cmd.Parameters.Add(new OracleParameter("reqno" , OracleType.VarChar)).Value = reqno;
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

        //===================================================Pro Approval

        public bool ApproveProc(string p_req_type, string p_req_no, string p_dept_code, string p_req_by, string p_req_purpose, string p_req_date, string p_user_id, string p_host_name, string p_host_ip)
        {
            bool rowAffected = false;
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("MOB_PROC_APPROVAL", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("P_USER_ID", OracleType.VarChar, Username, ParameterDirection.Input);

                    OracleParameter Param1 = new OracleParameter();
                    Param1.ParameterName = "P_REQ_TYPE";
                    Param1.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param1.Direction = ParameterDirection.Input;
             //       Param.Size = 88;
                    Param1.Value = p_req_type;
                    cmd.Parameters.Add(Param1);

                    OracleParameter Param2 = new OracleParameter();
                    Param2.ParameterName = "P_REQ_NO";
                    Param2.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param2.Direction = ParameterDirection.Input;
                 //   Param.Size = 88;
                    Param2.Value = p_req_no;
                    cmd.Parameters.Add(Param2);

                    OracleParameter Param3 = new OracleParameter();
                    Param3.ParameterName = "P_DEPT_CODE";
                    Param3.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param3.Direction = ParameterDirection.Input;
                    Param3.Value = p_dept_code;
                    cmd.Parameters.Add(Param3);


                    OracleParameter Param4 = new OracleParameter();
                    Param4.ParameterName = "P_REQ_BY";
                    Param4.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param4.Direction = ParameterDirection.Input;
                    Param4.Value = p_req_by;
                    cmd.Parameters.Add(Param4);

                    OracleParameter Param5 = new OracleParameter();
                    Param5.ParameterName = "P_REQ_PURPOSE";
                    Param5.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param5.Direction = ParameterDirection.Input;
                    Param5.Value = p_req_purpose;
                    cmd.Parameters.Add(Param5);

                    OracleParameter Param6 = new OracleParameter();
                    Param6.ParameterName = "P_REQ_DATE";
                    Param6.DbType = DbType.Date; // don't use OracleDbType, will return decimal in some cases, not int
                    Param6.Direction = ParameterDirection.Input;
                    Param6.Value = p_req_date;
                    cmd.Parameters.Add(Param6);
                 
                    OracleParameter Param7 = new OracleParameter();
                    Param7.ParameterName = "P_USER_ID";
                    Param7.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param7.Direction = ParameterDirection.Input;
                    Param7.Value = p_user_id;
                    cmd.Parameters.Add(Param7);

                    OracleParameter Param8 = new OracleParameter();
                    Param8.ParameterName = "P_HOST_NAME";
                    Param8.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param8.Direction = ParameterDirection.Input;
                    Param8.Value = p_host_name;
                    cmd.Parameters.Add(Param8);

                    OracleParameter Param9 = new OracleParameter();
                    Param9.ParameterName = "P_HOST_IP";
                    Param9.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param9.Direction = ParameterDirection.Input;
                    Param9.Value = p_host_ip;
                    cmd.Parameters.Add(Param9);

                    
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        rowAffected = true;
                        string message = "OK";
                        oraConnection.Close();
                        return rowAffected;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
         //   message = "";
            return rowAffected;

        }

        //===================================================Pro Reject
        public bool RejectProc(string p_req_type, string p_req_no, string p_user_id, string p_reject_reason)
        {
            bool rowAffected = false;
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand("MOB_PROC_REJECT ", oraConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("P_USER_ID", OracleType.VarChar, Username, ParameterDirection.Input);


                    Param = new OracleParameter();
                    Param.ParameterName = "P_REQ_TYPE";
                    Param.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param.Direction = ParameterDirection.Input;
                    Param.Size = 88;
                    Param.Value = p_req_type;
                    cmd.Parameters.Add(Param);
                   
                    OracleParameter Param1 = new OracleParameter();
                    Param1.ParameterName = "P_REQ_NO";
                    Param1.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param1.Direction = ParameterDirection.Input;
                    Param1.Value = p_req_no;
                    cmd.Parameters.Add(Param1);
              
                    OracleParameter Param3 = new OracleParameter();
                    Param3.ParameterName = "P_USER_ID";
                    Param3.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param3.Direction = ParameterDirection.Input;
                    Param3.Value = p_user_id;
                    cmd.Parameters.Add(Param3);

                    OracleParameter Param4 = new OracleParameter();
                    Param4.ParameterName = "P_REJECT_REASON";
                    Param4.DbType = DbType.String; // don't use OracleDbType, will return decimal in some cases, not int
                    Param4.Direction = ParameterDirection.Input;
                    Param4.Value = p_reject_reason;
                    cmd.Parameters.Add(Param4);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        rowAffected = true;
                        string message = "OK";
                        oraConnection.Close();
                        return rowAffected;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            oraConnection.Close();
     //       message = "";
            return rowAffected;

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

        //******************************************** Container Tracking for all *****************

       internal DataTable GetTracking(string ContainerNo)
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand(" select od.bolnumber as bolnumber, substr(toid, 5, length(toid)) as Container_no, decode(sku, 'CNT20', '20 Ft.', 'CNT40', '40 Ft', 'CNT45', '45 Ft.') as sku, " +
                                                          " carrierreference, company, curr_loc, od.storerkey as storerkey, od.sku as sku1, od.lot as lot from storer s, receipt r, operationdetails od " +
                                                          " where r.storerkey = s.storerkey  and od.receiptkey = r.receiptkey and sku like 'CNT%' and r.storerkey like 'SDRS%' and " +
                                                          " lot = (select max(TOLOT) from receiptdetail where substr(toid,5,length(toid)) = nvl(:ContainerNo,substr(toid,5,length(toid)))) and " +
                                                          " substr(toid, 5, length(toid)) = nvl(:ContainerNo, substr(toid, 5, length(toid)))", oraConnection);
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

       internal DataTable GetTrackingDetails(string ContainerNo)
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(" select 'Received in Bonded Zone'as status,to_char(datereceived,'dd/mm/yyyy HH24:MI:SS') as date1 ,datereceived as date2 from receiptdetail where " +
                                                          " toid='CNT-'||:ContainerNo and tolot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) union all " +
                                                          " select 'Moved to Strip Position :'||toloc,to_char(trans_date, 'dd/mm/yyyy HH24:MI:SS'),trans_date from cont_movement_detail where " +
                                                          " container_no='CNT-'||:ContainerNo and lot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) and fromloc='SHIFTOUT' union all " +
                                                          " select description1, to_char(effectivedate, 'dd/mm/yyyy HH24:MI:SS'), effectivedate " +
                                                          " from containerstatus_view where  toid='CNT-'||:ContainerNo and tolot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) union all " +
                                                          " select 'Shipped from Bonded Zone',to_char(effectivedate, 'dd/mm/yyyy HH24:MI:SS'),effectivedate from pickdetail " +
                                                          " where id='CNT-'||:ContainerNo and lot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) order by 3", oraConnection);

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

        // =================================================Cargo Tracking For All ==========================


        internal DataTable GetCargoTracking(string CargoblNo)
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {
                    OracleCommand cmd = new OracleCommand(" select main_consignee,main_consignee_name,T.storerkey as storerkey,company,bolnumber,containerno, " +
                                                          " truck_number,to_char(recvd_date, 'dd/mm/yyyy') as recvd_date,recvd_loc,curr_loc,recvd_qty,recvd_weight, " +
                                                          " to_char(shipped_date, 'dd/mm/yyyy hh24:mi:ss') as shipped_date,tolot from storer s, WEB_CARGO_DETAIL T where " +
                                                          " T.storerkey = s.storerkey and bolnumber = :CargoblNo order by t.curr_loc", oraConnection);
                    cmd.Parameters.Add(new OracleParameter("CargoblNo", OracleType.VarChar)).Value = CargoblNo;
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
 // ====================================================Cargo History
        internal DataTable GetCargoHistory(string CargoblNo)
        {
            try
            {
                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand(" Select ENG_DESC,TO_CHAR(CTH_ACTION_DATETIME,'DD/MM/YYYY HH24:MI:SS') as date_done From " +
                                                          " CODELKUP, CARGO_TRANSFER_REQUEST, CARGO_TRANSFER_HISTORY Where " +
                                                          " CTH_NUMBER = CTR_NUMBER and CT_ACTION = CODE and LISTNAME = 'CARGTRANSACTION' " +
                                                          " and CTR_BOLNUMBER = :CargoblNo order by CTH_ACTION_DATETIME DESC", oraConnection);
                    cmd.Parameters.Add(new OracleParameter("CargoblNo", OracleType.VarChar)).Value = CargoblNo;
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




        //**************************************************** careers ****************************
        internal DataTable GetJobTitles()
        {
            try
            {

                oraConnection.Open();
                if (oraConnection.State == ConnectionState.Open)
                {

                    OracleCommand cmd = new OracleCommand("SELECT TITLE,EXPERIENCE,POSTED_ON,JOBDESCRIPTION FROM careers WHERE STATUS = '1' ORDER BY ID ", oraConnection);

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


        //**************************************************************Container Status all ************************** 
    
        
        //internal DataTable GetContainer1(string ContainerNo)
        //{
        //    try
        //    {

        //        oraConnection.Open();
        //        if (oraConnection.State == ConnectionState.Open)
        //        {

        //            OracleCommand cmd = new OracleCommand("select od.bolnumber as bolnumber, substr(toid, 5, length(toid)) as Container_no, decode(sku, 'CNT20', '20 Ft.', 'CNT40', '40 Ft', 'CNT45', '45 Ft.') as sku," +
        //                                                   " carrierreference, company, curr_loc, od.storerkey as storerkey, od.sku as sku1, od.lot as lot" +
        //                                                   " from storer s, receipt r, operationdetails od " +
        //                                                   " where r.storerkey = s.storerkey and od.receiptkey = r.receiptkey and " +
        //                                                   " sku like 'CNT%' and r.storerkey like 'SDRS%' and  lot = (select max(TOLOT) " +
        //                                                   " from receiptdetail where substr(toid,5,length(toid))=nvl(:ContainerNo,substr(toid,5,length(toid)))) " +
        //                                                   " and r.storerkey in nvl(:ConsigneeID,r.storerkey) " +
        //                                                   " and substr(toid, 5, length(toid)) = " +
        //                                                   " nvl(:ContainerNo, substr(toid, 5, length(toid)))", oraConnection);

        //            //   cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
        //            //  cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = string.IsNullOrEmpty(ConsigneeId) ? string.Empty : ConsigneeId;
        //            cmd.Parameters.Add(new OracleParameter("ContainerNo", OracleType.VarChar)).Value = ContainerNo;
        //            DataTable dt = new DataTable();
        //            OracleDataAdapter da = new OracleDataAdapter(cmd);
        //            da.Fill(dt);
        //            oraConnection.Close();
        //            return dt;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);

        //    }
        //    oraConnection.Close();
        //    return null;
        //}

        //internal DataTable GetContainerDetails1(string ContainerNo)
        //{
        //    try
        //    {

        //        oraConnection.Open();
        //        if (oraConnection.State == ConnectionState.Open)
        //        {

        //            OracleCommand cmd = new OracleCommand(" select 'Received in Bonded Zone'as status,to_char(datereceived,'dd/mm/yyyy HH24:MI:SS') as date1 ,datereceived as date2 from receiptdetail where " +
        //                                                  " toid='CNT-'||:ContainerNo and storerkey =nvl(:ConsigneeID,storerkey) and tolot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) union all " +
        //                                                  " select 'Moved to Strip Position :'||toloc,to_char(trans_date, 'dd/mm/yyyy HH24:MI:SS'),trans_date from cont_movement_detail where " +
        //                                                  " container_no='CNT-'||:ContainerNo and lot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) and fromloc='SHIFTOUT' and storerkey=nvl(:ConsigneeID,storerkey) union all " +
        //                                                  " select description1, to_char(effectivedate, 'dd/mm/yyyy HH24:MI:SS'), effectivedate " +
        //                                                  " from containerstatus_view where TOSTORERKEY=nvl(:ConsigneeID,TOSTORERKEY) and toid='CNT-'||:ContainerNo and tolot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) union all " +
        //                                                  " select 'Shipped from Bonded Zone',to_char(effectivedate, 'dd/mm/yyyy HH24:MI:SS'),effectivedate from pickdetail " +
        //                                                  " where id='CNT-'||:ContainerNo and lot=(select max(TOLOT) from receiptdetail where toid='CNT-'||:ContainerNo) order by 3", oraConnection);

        //            //    cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = string.IsNullOrEmpty(ConsigneeId) ? string.Empty : ConsigneeId;
        //            //cmd.Parameters.Add(new OracleParameter("ConsigneeID", OracleType.VarChar)).Value = ConsigneeId;
        //            cmd.Parameters.Add(new OracleParameter("ContainerNo", OracleType.VarChar)).Value = ContainerNo;
        //            DataTable dt = new DataTable();
        //            OracleDataAdapter da = new OracleDataAdapter(cmd);
        //            da.Fill(dt);
        //            oraConnection.Close();
        //            return dt;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);

        //    }
        //    oraConnection.Close();
        //    return null;
        //}



    }


    





}
