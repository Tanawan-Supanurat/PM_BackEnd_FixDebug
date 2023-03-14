using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class CPIAPROCWHO
    {
        public string TABLE_NO { get; set; }
        public string SEQ_NO { get; set; }
        public string PROC_TYPE { get; set; }
        public string PROC_WHO { get; set; }
        public string PROC_NAME_ENG { get; set; }
        public string PROC_NAME_LOC { get; set; }
        public string PROC_WHEN { get; set; }
        public string UPD_WHO { get; set; }
        public string UPD_WHEN { get; set; }
        public string ENT_WHO { get; set; }
        public string ENT_WHEN { get; set; }

    }
}