using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class NRPMHIS
    {
        public string PART_NO { get; set; }
        public string REV_PART_NO { get; set; }
        public string REV_GOKAN { get; set; }
        public string REV_START_DATE { get; set; }
        public string UPD_WHO { get; set; }
        public string UPD_WHEN { get; set; }
        public string REV_STOP_DATE { get; set; }
        public string REV_REASON { get; set; }
    }
}