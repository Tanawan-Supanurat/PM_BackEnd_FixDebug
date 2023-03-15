using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class OracleToPostgres
    {
        public static string ChangeToPostgresSQL(string Oracle_SQL ,bool IsLower)
        {
            List<string> Orcle_SQL_ReplaceList = new List<string>() { "all_tab_cols", "'NUMBER' " , "INTERNAL_COLUMN_ID", "select ROLE_ID" };
            List<string> Postgres_SQL_ReplaceList = new List<string>() { "information_schema.columns", "'numeric'", "ordinal_position", "select cast(role_id as numeric)" };
            string sql = Oracle_SQL;
            int index = -1;
            foreach (var item in Orcle_SQL_ReplaceList )
            {
                index++;

                sql = sql.Replace(item, Postgres_SQL_ReplaceList[index]);
                
            }
            if(IsLower)
            {
                sql = sql.ToLower();
            }

            return sql;
        }
    }
}