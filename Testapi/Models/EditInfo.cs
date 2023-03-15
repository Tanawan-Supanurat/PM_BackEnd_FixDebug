using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class EditInfo
    {
        public string FIELD_NAME_LOC1 { get; set; }
        public string FIELD_NAME { get; set; }
        public string AUTH_TYPE { get; set; }
        public int FIELD_SEQ_NO { get; set; }
        public string ALIGNMENT{ get; set; }
        public string EDIT_ENABLE{ get; set; }
        public string CELL_LENGTH{ get; set; }
        public string CELL_TYPE { get; set; }
        public string INI_VALUE { get; set; }
        public string MS_TABLE { get; set; }
        public string MS_ITEM_NO{ get; set; }
        public string MS_TYPE { get; set; }
        public string UPD_WHO{ get; set; }
        public string UPD_WHEN{ get; set; }
        public string ENT_WHO{ get; set; }
        public string ENT_WHEN { get; set; }
        public string FIELD_VALUE { get; set; }
        public string FIELD_EXPLAIN { get; set; }
    }
}