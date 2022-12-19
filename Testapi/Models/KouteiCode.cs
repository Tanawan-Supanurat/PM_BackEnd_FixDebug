using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class KouteiCode
    {
        public string KT_CODE       { get; set; }
        public string SEQ_NO        { get; set; }
        public string WC_CODE       { get; set; }
        public string CC_CODE       { get; set; }
        public string SG_CODE       { get; set; }
        public string SETUP_STDTIME { get; set; }
        public string WORK_STDTIME  { get; set; }
        public string SEI_STDPCNT   { get; set; }
        public string REMARKS       { get; set; }
        public string START_DATE    { get; set; }
        public string STOP_DATE     { get; set; }
        public string UPD_WHO       { get; set; }
        public string UPP_WHEN      { get; set; }
        public string ENT_DATE      { get; set; }
    }
}