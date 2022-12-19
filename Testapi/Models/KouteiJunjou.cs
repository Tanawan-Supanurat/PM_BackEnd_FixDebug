using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class KouteiJunjou
    {
        public string KT_CODE { set; get; }
        public string SEQ_NO { set; get; }
        public string WC_CODE { set; get; }
        public string CC_CODE { set; get; }
        public string SG_CODE { set; get; }
        public string SHAIN_KUBUN { set; get; }
        public string VENDOR_CODE { set; get; }
        public string SG_KUBUN { set; get; }
        public string MACHINE_TYPE { set; get; }
        public string SETUP_STDTIME { set; get; }
        public string WORK_STDTIME { set; get; }
        public string SEI_STDPCNT { set; get; }
        public string SG_LEADTIME { set; get; }
        public string START_DATE { set; get; }
        public string STOP_DATE { set; get; }
        public string UPD_WHO { set; get; }
        public string UPD_WHEN { set; get; }
        public string ENT_DATE { set; get; }
        public string KIJI { set; get; }
        public string RANK { set; get; }
        public string KANRIBAN_LT { set; get; }
        public string SETUP_DUE_TIME { set; get; }
        public string WORK_DUE_TIME { set; get; }
    }
}