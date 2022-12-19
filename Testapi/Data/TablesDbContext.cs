using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;

namespace Testapi.Data
{
    public class TablesDbContext :DbContext
    {
        public TablesDbContext()
        : base("name=oraFj01x")
        {
            Database.SetInitializer<TablesDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //スキーマを指定（デフォルトではdbo）
            modelBuilder.HasDefaultSchema("SSDBA");

        }
        //破棄
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // SQLインジェクション対策
        public string FixedSQLi(string str)
        {
            if (str == null)
            {
                return str;
            }
            str = str.Replace("'", " ");
            str = str.Replace(";", " ");
            str = str.Replace("--", " ");
            str = str.Replace("/*", " ");
            str = str.Replace("*/", " ");
            return str.Trim();
        }
    }
}