using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class CHMSB
    {
        public string PLANT_NO { get; set; }
        public string PART_NO { get; set; }
        public string SG_CODE { get; set; }
        public string VENDOR_CODE { get; set; }
        public string LOT_SIZE { get; set; }
        public string VAR_CH_TANKA { get; set; }
        public string UPD_WHO { get; set; }
        public string UPD_WHEN { get; set; }
        public string ENT_DATE { get; set; }
        public string CH_MATL_COST { get; set; }
        public string CH_SUPPLY_COST { get; set; }
        public string CH_KAKOU_COST { get; set; }
        public string CH_NIJI_KAKOU_COST { get; set; }
        public string CH_TRNSPT_COST { get; set; }
        public string CH_MNG_COST { get; set; }
        public string CH_GAIN_COST { get; set; }
        public string CH_ETC_COST { get; set; }
    }
}