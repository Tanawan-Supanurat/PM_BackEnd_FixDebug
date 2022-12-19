using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class HeaderID
    {
        public string PART_NO { get; set; }
        public string PART_REV_NO { get; set; }
        public string PART_NAME_LOC1 {get; set;}
        public string UPD_WHO { get; set;}
        public string UPD_NAME {get; set;}
        public string UPD_WHEN {get; set;}
        public string ENT_WHO {get; set;}
        public string ENT_NAME {get; set;}
        public string ENT_WHEN { get; set; }
        public string REV_START_DATE { get; set; }
        public string REV_STOP_DATE { get; set; }
        public string M_START_DATE {get;set;}
        public string M_STOP_DATE {get;set;}
        public string CUR_TYPE {get;set;}
        public string APP_CUR_TYPE {get;set;}
    }
}