using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace mService.Models
{


    public class Leave
    {
        public string vardoc_no { get; set; }
        public string varemp_code { get; set; }
        public string varemp_name { get; set; }
        public string vardept_code { get; set; }
        public string varleave_type { get; set; }
        public string varleave_desc { get; set; }
        public string varstart_date { get; set; }
        public string varend_date { get; set; }
        public string varannual_days { get; set; }
        public string varother_days { get; set; }
        public string varunpaid_days { get; set; }
        public string vartotal_days { get; set; }
        public string varinternal_external { get; set; }
        public string varleave_status { get; set; }
        public string elam_file_name { get; set; }


    }



    public class LeavesCount
    {
        public string Count { get; set; }
//        public List<ProcCount> ProcCount { get; set; }
            }

    //public class ProcCount
    //{
    //    public string Count1{ get; set; }
    //}
    public class Approve
    {
        public string doc_no { get; set; }
        public string Username { get; set; }
        public string message { get; set; }
        //  public string PortOfLoading { get; set; }
    }
    public class Reject
    {
        public string doc_no { get; set; }
        public string Username { get; set; }
        public string Rejreason { get; set; }
        public string message { get; set; }

        //  public string PortOfLoading { get; set; }
    }


    public class Contract
    {
        public string cont_id { get; set; }
        public string cust_name { get; set; }
        public string cust_bl_name { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
           }


    public class Contracts
    {
        public string cb_cont_id { get; set; }
        public string cb_storerkey { get; set; }
        public string cb_name { get; set; }
        public string cb_bl_name { get; set; }
        public string cb_address { get; set; }
        public string cb_city { get; set; }
        public string cb_vat_reg_number { get; set; }
        public string cb_contact1 { get; set; }
        public string cb_phone1 { get; set; }
        public string cb_mobile1 { get; set; }
        public string cb_email1 { get; set; }
        public string cb_signatory { get; set; }
        public string cb_bl_signatory { get; set; }
        public string cb_signatory_title { get; set; }
        public string cb_signatory_bl_title { get; set; }
        public string cb_signed_date { get; set; }
        public string cb_cr_number { get; set; }
        public string cb_cr_expire_date { get; set; }
        public string cb_cr_attach_file { get; set; }
        public string cb_vat_attach_file { get; set; }
        public string cc_cont_id { get; set; }
        public string cc_start_date { get; set; }
        public string cc_end_date { get; set; }
        public string cc_cust_category { get; set; }
        public string cc_wh_lease_flag { get; set; }
        public string cc_covs_lease_flag { get; set; }
        public string cc_openy_lease_flag { get; set; }
        public string cc_office_lease_flag { get; set; }
        public string cc_autozone_lease_flag { get; set; }
        public string cc_chemzone_lease_flag { get; set; }
        public string cc_hi_bill_pay_mode { get; set; }
        public string cc_hi_bill_flag { get; set; }
        public string cc_cont_dem_bill_flag { get; set; }
        public string cc_cargo_dem_bill_flag { get; set; }
        public string cc_cargo_insp_flag { get; set; }
        public string cc_cont_type { get; set; }
        public string cc_cont_free_days { get; set; }
        public string cc_cargo_free_days { get; set; }
        public string cc_cont_dem_calc_by { get; set; }
        public string cc_cont_dem_charges { get; set; }
        public string cc_cargo_dem_calc_by { get; set; }
        public string cc_cargo_dem_charges { get; set; }
        public string cc_termination_days { get; set; }
        public string cc_attach_file { get; set; }
        public string cc_status { get; set; }
        public string cc_tariff_affected_flag { get; set; }
        public string cc_hi_credit_period { get; set; }
        public string cc_hi_credit_limit { get; set; }
        public List<GetContractSales> GetContractSales { get; internal set; }
    }
    public class GetContractSales
    {

        public string cst_cont_id { get; set; }
        public string cst_emp_code { get; set; }
        public string cst_remarks { get; set; }
        public string emp_name { get; set; }

    }

    public class Sale
    {

        public string cst_cont_id { get; set; }
        public string cst_emp_code { get; set; }
        public string cst_remarks { get; set; }
        public string emp_name { get; set; }

    }


    public class Privilege
    {

        public string csp_cont_id { get; set; }
        public string csp_dobeforecntarrivalflag { get; set; }
      

    }

    public class Tariff
    {
        public string descrip { get; set; }
        public string ct_orig_price { get; set; }
        public string ct_change_perc { get; set; }
        public string ct_final_price { get; set; }
      
    }

    //Lease

    public class Lease
    {
        public string clpd_cont_id { get; set; }
        public string clpd_loc { get; set; }
        public string clpd_area { get; set; }
        public string clpd_bill_frequency { get; set; }
        public string clpd_start_date { get; set; }
        public string clpd_end_date { get; set; }
        public string clpd_electricity { get; set; }
        public string clpd_cleaning { get; set; }
        public string clpd_it_services { get; set; }


    }
   
    public class Contractapp
    {
        public string count_id { get; set; }
        public string user_id { get; set; }
        public string ip_address { get; set; }
        public string P_MSG_ID { get; set; }
        
    }

    public class Contractrej
    {
        public string count_id { get; set; }
        public string rej_reason { get; set; }
        public string user_id { get; set; }
        public string ip_address { get; set; }
        public string P_MSG_ID { get; set; }


    }

}