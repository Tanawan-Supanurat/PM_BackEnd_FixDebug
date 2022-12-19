using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class NRPMB
    {
        public string PART_NO { get; set; }
        public List<string> PLANT_NO { get; set; }
        public string MFG_TYPE { get; set; }
        public string STOCK { get; set; }
        public string AUTO_SOKO { get; set; }
        public string ORDER_TYPE { get; set; }
        public string ORDER_AUTO { get; set; }
        public string KT_CODE { get; set; }
        public string TE_TENSHO { get; set; }
        public string TE_TANTO { get; set; }
        public string CH_TENSHO { get; set; }
        public string CH_TANTO { get; set; }
        public string NONYU_LOCATION { get; set; }
        public string CH_HANTEI { get; set; }
        public string TEISHUTU_CD_1 { get; set; }
        public string TEISHUTU_CD_2 { get; set; }
        public string QC_FLAG { get; set; }
        public string QC_TENSHO { get; set; }
        public string ABC_TYPE { get; set; }
        public string SCRAP_PCNT { get; set; }
        public string BUCKET { get; set; }
        public string MFG_LOT_SIZE { get; set; }
        public string MFG_LEADTIME { get; set; }
        public string ISSUE_DAYS { get; set; }
        public string STOP_DATE { get; set; }
        public string UPD_WHO { get; set; }
        public string UPD_WHEN { get; set; }
        public string ENT_DATE { get; set; }
        public string STOCK_KANRI { get; set; }
        public string HOZAI_KANRI1 { get; set; }
        public string HOZAI_KANRI2 { get; set; }
        public string HOZAI_KANRI3 { get; set; }
        public string HOZAI_KANRI4 { get; set; }
        public string HOZAI_KANRI5 { get; set; }
        public string HOZAI_KANRI6 { get; set; }
        public string HOZAI_KANRI7 { get; set; }
        public string HOZAI_KANRI8 { get; set; }
        public string HOZAI_KANRI9 { get; set; }
        public string HOZAI_KANRI10 { get; set; }
        public string HOZAI_KANRI11 { get; set; }
        public string HOZAI_KANRI12 { get; set; }
        public string MFG_MIN_LOT_SIZE { get; set; }
        public string SET_COLOR_FLAG { get; set; }
        public string WCCC_FOLLOW_FLAG { get; set; }
        public string AUTO_TE_COMP_DATE { get; set; }
        public string TRACE_FLAG { get; set; }
        public string VENDOR_SIKYU_FLAG { get; set; }
        public string AUTO_CH_REQ_FLAG { get; set; }
        public string YEAR_CH_QTY { get; set; }
        public string LOT_PRINT_TYPE { get; set; }
        public string HOZAI_AUTO_ORD_FLAG { get; set; }
        public string TRANSFER_REQ_PLANT { get; set; }
        public string FC_FLAG { get; set; }
        public string STOCK_PLAN_COUNT { get; set; }
        public string STOCK_PLAN_TYPE { get; set; }
        public string ISSUE_FC_METHOD { get; set; }
        public string FM_ISSUE_FC_RATE { get; set; }
        public string POG_ISSUE_FC_RATE { get; set; }
    }
}