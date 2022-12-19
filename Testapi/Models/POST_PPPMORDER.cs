using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class POST_PPPMORDER
    {
        public string PART_NO { get; set; }
        public List<string> PLANT_NO { get; set; }
        public string ARR_PLANT_NO { get; set; }
        public string ARR_BRANCH_CODE { get; set; }
        public string ARR_WHO { get; set; }
        public string STOCK_CODE { get; set; }
        public string MFG_TYPE { get; set; }
        public string ROUTING_CODE { get; set; }
        public string STOCK_TYPE { get; set; }
        public string AUTO_WAREHOUSE_CODE { get; set; }
        public string ORDER_TYPE { get; set; }
        public string TRANS_STOCK_TYPE { get; set; }
        public string ABC_TYPE { get; set; }
        public string SCRAP_PCNT { get; set; }
        public string BUCKET { get; set; }
        public string MATL_BRANCH_CODE1 { get; set; }
        public string MATL_BRANCH_CODE2 { get; set; }
        public string MATL_BRANCH_CODE3 { get; set; }
        public string MATL_BRANCH_CODE4 { get; set; }
        public string MATL_BRANCH_CODE5 { get; set; }
        public string MATL_BRANCH_CODE6 { get; set; }
        public string MATL_BRANCH_CODE7 { get; set; }
        public string MATL_BRANCH_CODE8 { get; set; }
        public string MATL_BRANCH_CODE9 { get; set; }
        public string MATL_BRANCH_CODE10 { get; set; }
        public string MATL_BRANCH_CODE11 { get; set; }
        public string MATL_BRANCH_CODE12 { get; set; }
        public string MATL_AUTO_ORD_TYPE { get; set; }
        public string TRACE_TYPE { get; set; }
        public string PO_TYPE { get; set; }
        public string YEAR_CH_QTY { get; set; }
        public string WCCC_FOLLOW_TYPE { get; set; }
        public string SET_COLOR_TYPE { get; set; }
        public string AUTO_ARR_COMP_DATE { get; set; }
        public string AUTO_CH_REQ_TYPE { get; set; }
        public string LOT_PRINT_TYPE { get; set; }
        public string MFG_LOT_SIZE { get; set; }
        public string MFG_LEADTIME { get; set; }
        public string MFG_MIN_LOT_SIZE { get; set; }
        public string VENDOR_SUPPLY_TYPE { get; set; }
        public string STOP_DATE { get; set; }
        public string PO_PLANT_NO { get; set; }
        public string PO_BRANCH_CODE { get; set; }
        public string PO_WHO { get; set; }
        public string QC_PLANT_NO { get; set; }
        public string QC_BRANCH_CODE { get; set; }
        public string FILING_REC_CODE1 { get; set; }
        public string FILING_REC_CODE2 { get; set; }
        public string DELIV_LOCATION { get; set; }
        public string QC_FLAG { get; set; }
        public string ISSUE_DAYS { get; set; }
        public string TRANSFER_REQ_PLANT { get; set; }
        public string FC_FLAG { get; set; }
        public string STOCK_PLAN_COUNT { get; set; }
        public string STOCK_PLAN_TYPE { get; set; }
        public string ISSUE_FC_METHOD { get; set; }
        public string FM_ISSUE_FC_RATE { get; set; }
        public string POG_ISSUE_FC_RATE { get; set; }
        public string SUB_CHG_TYPE { get; set; }
        public string SUB_START_DATE { get; set; }
        public string UPD_WHO { get; set; }
        public string UPD_WHEN { get; set; }
        public string ENT_WHO { get; set; }
        public string ENT_WHEN { get; set; }
        public string REPAIR_PSC_TYPE { get; set; }
    }
}