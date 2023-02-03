using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class PPPMMAINTCONDMS_UPDATE
    {
        public int CUR_TIME { get; set; }
        public string PART_NO { get; set; }
        public string PART_REV_NO { get; set; }
        public string PART_LOCATION { get; set; }
        public string COND_PAT_NO { get; set; }
        public string CONDITION_ID { get; set; }
        public string CONDITION_TYPE { get; set; }
        public string PLAN_LOC_TYPE { get; set; }
        public string PAT_NO_TYPE { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string CONDITION_ITEM_TYPE { get; set; }
        public string COND_SPEC_ITEM_NO { get; set; }
        public string COND_STAT { get; set; }
        public string COND_CODE { get; set; }
        public string START_DATE { get; set; }
        public string STOP_DATE { get; set; }
        public string COND_SEQ_NO { get; set; }
        public string UPD_WHO { get; set; }
        public string UPD_WHEN { get; set; }
        public string ENT_WHO { get; set; }
        public string ENT_WHEN { get; set; }
    }
}