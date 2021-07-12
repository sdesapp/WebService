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
    public class ApprovalListController : ApiController
    {

        OracleRepository bOra = new OracleRepository();

        // 
        // Url: http://localhost:6363/api/ApprovalList/GetApprovalList?username=username

        [HttpGet]

        public List<Leave> GetApprovalList(string username)
        {

            var dataTable = bOra.GetApprovalList(username);
            List<Leave> Leaves = new List<Leave>();
            foreach (DataRow record in dataTable.Rows)

            {
                Leaves.Add(new Leave()
                {
                    vardoc_no = record["vardoc_no"].ToString(),
                    varemp_code = record["varemp_code"].ToString(),
                    varemp_name = record["emp_name"].ToString(),
                    vardept_code = record["vardept_code"].ToString(),
                    varleave_type = record["varleave_type"].ToString(),
                    varleave_desc = record["varleave_desc"].ToString(),
                    varstart_date = record["varstart_date"].ToString(),
                    varend_date = record["varend_date"].ToString(),
                    varannual_days = record["varannual_days"].ToString(),
                    varother_days = record["varother_days"].ToString(),
                    varunpaid_days = record["varunpaid_days"].ToString(),
                    vartotal_days = record["vartotal_days"].ToString(),
                    varinternal_external = record["varinternal_external"].ToString(),
                    varleave_status = record["varleave_status"].ToString(),
                    elam_file_name = record["elam_file_name"].ToString()
                });
            }

            return Leaves;
        }


        // leave count Url: http://localhost:6363/api/ApprovalList/GetApprovalCount?username=username
        [HttpGet]

        public List<LeavesCount> GetApprovalCount(string username)
        {
            var dataTable = bOra.GetApprovalCount(username);
            List<LeavesCount> LeavesCount = new List<LeavesCount>();
            foreach (DataRow record in dataTable.Rows)
            {
                LeavesCount.Add(new LeavesCount()
                {
                    Count = record["Count"].ToString(),

                });
            }
            return LeavesCount;


        }

        //public List<ProcCount> GetProcCount(string username)
        //{
        //    var dataTable = bOra.GetProcCount(username);
        //    List<ProcCount> ProcCount = new List<ProcCount>();
        //    foreach (DataRow record in dataTable.Rows)
        //    {
        //        ProcCount.Add(new ProcCount()
        //        {
        //            Count1 = record["Count"].ToString(),

        //        });
        //    }
        //    return ProcCount;


        //}


        //url http://localhost:6363/api/ApprovalList/ApproveLeave 
        [HttpPost]

        public void ApproveLeave(Approve approve)
        {
            OracleRepository repo = new OracleRepository();

            string message = "ok";
            repo.ApproveLeave(approve.doc_no, approve.Username, out message);

            Console.Write(message);

        }

        //url http://localhost:6363/api/ApprovalList/RejectLeave 

        [HttpPost]

        public void RejectLeave(Reject reject)
        {
            OracleRepository repo = new OracleRepository();

            string message = "ok";
            repo.RejectLeave(reject.doc_no, reject.Username, reject.Rejreason, out message);

            Console.Write(message);

        }



        // contractlist   http://localhost:6363/api/ApprovalList/GetContractApprovalList?username=username

        [HttpGet]
        public List<Contract> GetContractApprovalList(string username)
        {
            var dataTable = bOra.GetContractApprovalList(username);
            List<Contract> Contracts = new List<Contract>();
            foreach (DataRow record in dataTable.Rows)
            {
                Contracts.Add(new Contract()
                {
                    cont_id = record["cont_id"].ToString(),
                    cust_name = record["cust_name"].ToString(),
                    cust_bl_name = record["cust_bl_name"].ToString(),
                    start_date = record["start_date"].ToString(),
                    end_date = record["end_date"].ToString(),
                });

            }
            return Contracts;

        }

        //   http://localhost:6363/api/ApprovalList/GetContractDetail?custid=custid

        [HttpGet]

   
        public Contracts GetContractDetail(string custid)
        {

            var dataTable = bOra.GetContractDetail(custid);
            Contracts Contracts = new Contracts();
            if (dataTable.Rows.Count > 0)
               {
                var record = dataTable.Rows[0];
                Contracts.cb_cont_id = record["CB_CONT_ID"].ToString();
                Contracts.cb_storerkey = record["CB_STORERKEY"].ToString();
                Contracts.cb_name = record["CB_NAME"].ToString();
                Contracts.cb_bl_name = record["CB_BL_NAME"].ToString();
                Contracts.cb_address = record["CB_ADDRESS"].ToString();
                Contracts.cb_city = record["CB_CITY"].ToString();
                Contracts.cb_vat_reg_number = record["CB_VAT_REG_NUMBER"].ToString();
                Contracts.cb_contact1 = record["CB_CONTACT1"].ToString();
                Contracts.cb_phone1 = record["CB_PHONE1"].ToString();
                Contracts.cb_mobile1 = record["CB_MOBILE1"].ToString();
                Contracts.cb_email1 = record["CB_EMAIL1"].ToString();
                Contracts.cb_signatory = record["CB_SIGNATORY"].ToString();
                Contracts.cb_bl_signatory = record["CB_BL_SIGNATORY"].ToString();
                Contracts.cb_signatory_title = record["CB_SIGNATORY_TITLE"].ToString();
                Contracts.cb_signatory_bl_title = record["CB_SIGNATORY_BL_TITLE"].ToString();
                Contracts.cb_signed_date = record["CB_SIGNED_DATE"].ToString();
                Contracts.cb_cr_number = record["CB_CR_NUMBER"].ToString();
                Contracts.cb_cr_expire_date = record["CB_CR_EXPIRE_DATE"].ToString();
                Contracts.cb_cr_attach_file = record["CB_CR_ATTACH_FILE"].ToString();
                Contracts.cb_vat_attach_file = record["CB_VAT_ATTACH_FILE"].ToString();
                Contracts.cc_cont_id = record["CC_CONT_ID"].ToString();
                Contracts.cc_start_date = record["CC_START_DATE"].ToString();
                Contracts.cc_end_date = record["CC_END_DATE"].ToString();
                Contracts.cc_cust_category = record["CC_CUST_CATEGORY"].ToString();
                Contracts.cc_wh_lease_flag = record["CC_WH_LEASE_FLAG"].ToString();
                Contracts.cc_covs_lease_flag = record["CC_COVS_LEASE_FLAG"].ToString();
                Contracts.cc_openy_lease_flag = record["CC_OPENY_LEASE_FLAG"].ToString();
                Contracts.cc_office_lease_flag = record["CC_OFFICE_LEASE_FLAG"].ToString();
                Contracts.cc_autozone_lease_flag = record["CC_AUTOZONE_LEASE_FLAG"].ToString();
                Contracts.cc_chemzone_lease_flag = record["CC_CHEMZONE_LEASE_FLAG"].ToString();
                Contracts.cc_hi_bill_pay_mode = record["CC_HI_BILL_PAY_MODE"].ToString();
                Contracts.cc_hi_bill_flag = record["CC_HI_BILL_FLAG"].ToString();
                Contracts.cc_cont_dem_bill_flag = record["CC_CONT_DEM_BILL_FLAG"].ToString();
                Contracts.cc_cargo_dem_bill_flag = record["CC_CARGO_DEM_BILL_FLAG"].ToString();
                Contracts.cc_cargo_insp_flag = record["CC_CARGO_INSP_FLAG"].ToString();
                Contracts.cc_cont_type = record["CC_CONT_TYPE"].ToString();
                Contracts.cc_cont_free_days = record["CC_CONT_FREE_DAYS"].ToString();
                Contracts.cc_cargo_free_days = record["CC_CARGO_FREE_DAYS"].ToString();
                Contracts.cc_cont_dem_calc_by = record["CC_CONT_DEM_CALC_BY"].ToString();
                Contracts.cc_cont_dem_charges = record["CC_CONT_DEM_CHARGES"].ToString();
                Contracts.cc_cargo_dem_calc_by = record["CC_CARGO_DEM_CALC_BY"].ToString();
                Contracts.cc_cargo_dem_charges = record["CC_CARGO_DEM_CHARGES"].ToString();
                Contracts.cc_termination_days = record["CC_TERMINATION_DAYS"].ToString();
                Contracts.cc_attach_file = record["CC_ATTACH_FILE"].ToString();
                Contracts.cc_status = record["CC_STATUS"].ToString();
                Contracts.cc_tariff_affected_flag = record["CC_TARIFF_AFFECTED_FLAG"].ToString();
                Contracts.cc_hi_credit_period = record["CC_HI_CREDIT_PERIOD"].ToString();
                Contracts.cc_hi_credit_limit = record["CC_HI_CREDIT_LIMIT"].ToString();

                //dataTable = bOra.GetContractSales(custid);
                //List<GetContractSales> Details = new List<GetContractSales>();
                //foreach (DataRow row in dataTable.Rows)
                //{
                //    Details.Add(new GetContractSales()
                //    {
                //        cst_cont_id = record["CST_CONT_ID"].ToString(),
                //        cst_emp_code = record["CST_EMP_CODE"].ToString(),
                //        cst_remarks = record["CST_REMARKS"].ToString(),
                //        emp_name = record["EMP_NAME"].ToString(),

                //    });
                //}

                //Contracts.GetContractSales = Details;

            }
            return Contracts;

        }

        //    http://localhost:6363/api/ApprovalList/GetContractSales?custid4=custid
        [HttpGet]
        public List<Sale> GetContractSales(string custid4)
        {
            var dataTable = bOra.GetContractSales(custid4);
            List<Sale> Sale = new List<Sale>();
            foreach (DataRow record in dataTable.Rows)
            {
                Sale.Add(new Sale()
                {
                    cst_cont_id = record["CST_CONT_ID"].ToString(),
                    cst_emp_code = record["CST_EMP_CODE"].ToString(),
                    cst_remarks = record["CST_REMARKS"].ToString(),
                    emp_name = record["EMP_NAME"].ToString(),
                });
            }
            return Sale;
        }

        //    http://localhost:6363/api/ApprovalList/GetContractprivilege?custid4=custid
        [HttpGet]
        public List<Privilege> GetContractPrivilege(string custid4)
        {
            var dataTable = bOra.GetContractPrivilege(custid4);
            List<Privilege> Privilege = new List<Privilege>();
            foreach (DataRow record in dataTable.Rows)
            {
                Privilege.Add(new Privilege()
                {
                    csp_cont_id = record["CSP_CONT_ID"].ToString(),
                    csp_dobeforecntarrivalflag = record["CSP_DOBEFORECNTARRIVALFLAG"].ToString(),
                  
                });
            }
            return Privilege;
        }




        //   http://localhost:6363/api/ApprovalList/GetContractTariff?custid1=custid
        [HttpGet]
        public List<Tariff> GetContractTariff(string custid1)
        {
            var dataTable = bOra.GetContractTariff(custid1);
            List<Tariff> Tariff = new List<Tariff>();
            foreach (DataRow record in dataTable.Rows)
            {
                Tariff.Add(new Tariff()
                {
                    descrip = record["DESCRIP"].ToString(),
                    ct_orig_price = record["CT_ORIG_PRICE"].ToString(),
                    ct_change_perc = record["CT_CHANGE_PERC"].ToString(),
                    ct_final_price = record["CT_FINAL_PRICE"].ToString(),
                   
                });

            }
            return Tariff;

        }
        //  http://localhost:6363/api/ApprovalList/GetContractLease?custid2=custid
        [HttpGet]
        public List<Lease> GetContractLease(string custid2)
        {
            var dataTable = bOra.GetContractLease(custid2);
            List<Lease> Lease = new List<Lease>();
            foreach (DataRow record in dataTable.Rows)
            {
                Lease.Add(new Lease()
                {
                    clpd_cont_id = record["CLPD_CONT_ID"].ToString(),
                    clpd_loc = record["CLPD_LOC"].ToString(),
                    clpd_area = record["CLPD_AREA"].ToString(),
                    clpd_bill_frequency = record["CLPD_BILL_FREQUENCY"].ToString(),
                    clpd_start_date = record["CLPD_START_DATE"].ToString(),
                    clpd_end_date = record["CLPD_END_DATE"].ToString(),
                    clpd_electricity = record["CLPD_ELECTRICITY"].ToString(),
                    clpd_cleaning = record["CLPD_CLEANING"].ToString(),
                    clpd_it_services = record["CLPD_IT_SERVICES"].ToString(),
                });
                }
            return Lease;
            }







        // Approval  http://localhost:6363/api/ApprovalList/ApproveContract
        [HttpPost]
        public void ApproveContract(Contractapp contractapp)
        {
            OracleRepository repo = new OracleRepository();
            string P_MSG_ID;
             repo.ApproveContract(contractapp.count_id,contractapp.user_id,contractapp.ip_address, out P_MSG_ID);
          //  repo.ApproveContract("827", "sfaridi", "172.16.1.1", out P_MSG_ID);

        }

        // Reject  http://localhost:6363/api/ApprovalList/RejectContract?contractrej
        [HttpPost]
        public void RejectContract(Contractrej contractrej)
        {
            OracleRepository repo = new OracleRepository();
            string P_MSG_ID="ok";
            repo.RejectContract(contractrej.count_id, contractrej.rej_reason, contractrej.user_id,contractrej.ip_address, out P_MSG_ID);
      //      repo.RejectContract("827", "Reject", "sfaridi", "172.16.1.1", out P_MSG_ID);

        }

    }	

}
    

