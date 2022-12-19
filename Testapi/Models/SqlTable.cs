using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testapi.Models
{
    public class SqlTable
    {
        //基本のテーブルを取得
        public static string getSQLTableBase()
        {
            string sql = "";
            sql += "SELECT ";
            sql += "  DWG.ISSUE_NO ";
            sql += "  , DWG.ISSUE_DATE ";
            sql += "  , CHG.CHG_NO ";
            sql += "  , PM.DWG_NO ";
            sql += "  , PM.DWG_REV_NO ";
            sql += "  , PM.PART_NO ";
            sql += "  , PM.PART_REV_NO ";
            sql += "  , PM.PART_NAME_LOC1 ";
            sql += "  , ISS.SEQ_NO as ISS_SEQ_NO ";
            sql += " FROM PPPMMS PM ";
            sql += " left join ( PPSDDWGMS DWG left join PPSDISSUEMS ISS on DWG.ISSUE_NO = ISS.ISSUE_NO and ISS.APP_CUR_TYPE = '1'";
            sql += " left join PPSDCHGMS CHG on DWG.ISSUE_NO = CHG.ISSUE_NO and CHG.APP_CUR_TYPE = '1') on PM.DWG_NO = DWG.DWG_NO  and PM.DWG_REV_NO = DWG.DWG_REV_NO";
            sql += " where  PM.APP_CUR_TYPE = '1' and (  (PM.PM_TYPE <> 'W' ) OR ((PM_TYPE = 'W') and (PRODUCT_CODE IS NOT NULL))) ";

            return sql;
        }
        //部品検索条件
        public static string getSQLBuhimei(string PART_NO, string PART_NAME_LOC1, int FIND_OPTION)
        {
            string sql = "";
            switch (FIND_OPTION)
            {
                //前方一致
                case 1:
                    //部品コード
                    if (PART_NO != null)
                    {
                        sql += "and PM.PART_NO LIKE '" + PART_NO + "%' ";
                    }
                    //部品名
                    if (PART_NAME_LOC1 != null)
                    {
                        sql += "and PM.PART_NAME_LOC1 LIKE '" + PART_NAME_LOC1 + "%' ";
                    }
                    break;
                //完全一致
                case 2:
                    //部品コード
                    if (PART_NO != null)
                    {
                        sql += "and PM.PART_NO = '" + PART_NO + "' ";
                    }
                    //部品名
                    if (PART_NAME_LOC1 != null)
                    {
                        sql += "and PM.PART_NAME_LOC1 = '" + PART_NAME_LOC1 + "' ";
                    }
                    break;
                //部分一致
                case 3:
                    //部品コード
                    if (PART_NO != null)
                    {
                        sql += "and PM.PART_NO LIKE '%" + PART_NO + "%' ";
                    }
                    //部品名
                    if (PART_NAME_LOC1 != null)
                    {
                        sql += "and PM.PART_NAME_LOC1 LIKE '%" + PART_NAME_LOC1 + "%' ";
                    }
                    break;
            }
            return sql;
        }
        //保守部品名 図番名
        public static string getSQLHoshuZumei(string MAINT_PART_NAME, int MAINT_PART_NAME_OPTION, string DWG_NO, int DWG_NO_OPTION)
        {
            string sql = "";
            //保守部品名
            switch (MAINT_PART_NAME_OPTION)
            {
                //前方一致
                case 1:
                    if (MAINT_PART_NAME != null)
                    {
                        sql += "and PM.MAINT_PART_NAME LIKE '" + MAINT_PART_NAME + "%' ";
                    }
                    break;
                //完全一致
                case 2:
                    if (MAINT_PART_NAME != null)
                    {
                        sql += "and PM.MAINT_PART_NAME = '" + MAINT_PART_NAME + "' ";
                    }
                    break;
                //部分一致
                case 3:
                    if (MAINT_PART_NAME != null)
                    {
                        sql += "and PM.MAINT_PART_NAME LIKE '%" + MAINT_PART_NAME + "%' ";
                    }
                    break;
            }
            //図番名
            switch (DWG_NO_OPTION)
            {
                //前方一致
                case 1:
                    if (DWG_NO != null)
                    {
                        sql += "and PM.DWG_NO LIKE '" + DWG_NO + "%' ";
                    }
                    break;
                //完全一致
                case 2:
                    if (DWG_NO != null)
                    {
                        sql += "and PM.DWG_NO = '" + DWG_NO + "' ";
                    }
                    break;
                //部分一致
                case 3:
                    if (DWG_NO != null)
                    {
                        sql += "and PM.DWG_NO LIKE '%" + DWG_NO + "%' ";
                    }
                    break;
            }
            return sql;
        }
        //製品分類コード 部品区分 PDMタイプ 機種 価格設定 メーカー型番 SP未登録
        public static string getSQLPM_Mask(string PRODUCT_CODE, string PART_TYPE, string PDM_TYPE, string MACHINE_TYPE, string SELLING_PRICE_TYPE, string MAKER_PART_NO)
        {
            string sql = "";
            //製品分類コード
            if (PRODUCT_CODE != null)
            { sql += " and PM.PRODUCT_CODE = '" + PRODUCT_CODE + "'"; }
            //部品区分
            if (PART_TYPE != null)
            { sql += " and PM.PART_TYPE = '" + PART_TYPE + "'"; }
            //PDMタイプ
            if (PDM_TYPE != null)
            { sql += " and PM.PDM_TYPE = '" + PDM_TYPE + "'"; }
            //機種
            if (MACHINE_TYPE != null)
            { sql += " and PM.MACHINE_TYPE = '" + MACHINE_TYPE + "'"; }
            //価格設定(販売価格判定)
            if (SELLING_PRICE_TYPE != null && SELLING_PRICE_TYPE != "-")
            { sql += " and PM.SELLING_PRICE_TYPE = '" + SELLING_PRICE_TYPE + "'"; }
            //メーカー型番
            if (MAKER_PART_NO != null)
            { sql += " and PM.MAKER_PART_NO = '" + MAKER_PART_NO + "'"; }
            return sql;
        }
        //標準図発行日
        public static string getSQLHyoujuuhakko(string ISSUE_DATE_1, string ISSUE_DATE_2, bool Kensaku_Option)
        {
            string sql = "";
            //詳細検索
            if (Kensaku_Option)
            {
                if (ISSUE_DATE_1 != null)
                {
                    sql += " and (PM.DWG_NO, PM.DWG_REV_NO) in ( select  DWG_NO , DWG_REV_NO  from  PPSDDWGMS  where ";

                }
                if (ISSUE_DATE_1 != null && ISSUE_DATE_2 == null)
                {
                    sql += "  DWG.ISSUE_DATE = '" + ISSUE_DATE_1 + "' )";
                }
                if (ISSUE_DATE_1 != null && ISSUE_DATE_2 != null)
                {
                    sql += "  DWG.ISSUE_DATE >= '" + ISSUE_DATE_1 + "' ";
                    sql += "  and DWG.ISSUE_DATE <= '" + ISSUE_DATE_2 + "' )";
                }
            }
            //基本検索
            else
            {
                if (ISSUE_DATE_1 != null && ISSUE_DATE_2 == null)
                {
                    sql += "  and DWG.ISSUE_DATE = '" + ISSUE_DATE_1 + "' ";
                }
                else if (ISSUE_DATE_1 == null && ISSUE_DATE_2 != null)
                {
                    sql += "  and DWG.ISSUE_DATE = '" + ISSUE_DATE_2 + "' ";
                }
                if (ISSUE_DATE_1 != null && ISSUE_DATE_2 != null)
                {
                    sql += "  and DWG.ISSUE_DATE >= '" + ISSUE_DATE_1 + "' ";
                    sql += "  and DWG.ISSUE_DATE <= '" + ISSUE_DATE_2 + "' ";
                }
            }
            return sql;
        }
        //標準図切替指示書No
        public static string getSQLHyoujuuKirikae(string DWG_CHG_DATE_1, string DWG_CHG_DATE_2, string CHG_NO, string ISSUE_NO)
        {
            string sql = "";
            //標準図切替日
            if (DWG_CHG_DATE_1 != null || DWG_CHG_DATE_2 != null)
            {
                sql += " and (CHG.CHG_NO, CHG.CHG_REV_NO) in ( ";
                sql += " select ";
                sql += " CHG_NO ";
                sql += ", CHG_REV_NO ";
                sql += " from ";
                sql += " PPSDCHGHIS CHGHS ";
                sql += " where ";
                sql += " CHGHS.CHG_NO IS NOT NULL ";
                if (DWG_CHG_DATE_1 != null && DWG_CHG_DATE_2 == null)
                {
                    sql += "  and CHGHS.DWG_CHG_DATE = '" + DWG_CHG_DATE_1 + "' ";
                }
                else if (DWG_CHG_DATE_1 == null && DWG_CHG_DATE_2 != null)
                {
                    sql += "  and CHGHS.DWG_CHG_DATE = '" + DWG_CHG_DATE_2 + "' ";
                }
                else if (DWG_CHG_DATE_1 != null && DWG_CHG_DATE_2 != null)
                {
                    sql += "  and CHGHS.DWG_CHG_DATE >= '" + DWG_CHG_DATE_1 + "' ";
                    sql += "  and CHGHS.DWG_CHG_DATE <= '" + DWG_CHG_DATE_2 + "' ";
                }
                sql += ") ";
            }
            if (CHG_NO != null)
            {
                sql += "  and CHG.CHG_NO = '" + CHG_NO + "' ";
            }
            if (ISSUE_NO != null)
            {
                sql += "  and DWG.ISSUE_NO = '" + ISSUE_NO + "' ";
            }
            return sql;
        }
        //PC登録済 かつ SP未登録
        public static string getSQLPCEntandSONotEnt(bool ckPCEntandSPNoEnt_Checked)
        {
            string sql = "";
            if (ckPCEntandSPNoEnt_Checked)
            {
                sql += " and exists (  select  1  from  PPPMPCCOSTMS  where  PPPMPCCOSTMS.PART_NO = PM.PART_NO  and PPPMPCCOSTMS.PLANT_NO = '5'  and PPPMPCCOSTMS.APP_CUR_TYPE = '1'  ) ";
                sql += " and not exists (  select  1  from  PPPMSPCOSTMS  where  PPPMSPCOSTMS.PART_NO = PM.PART_NO  and (  PPPMSPCOSTMS.CUR_TYPE = '1'  or PPPMSPCOSTMS.APP_CUR_TYPE = '1' )  )  ";
            }
            return sql;
        }
        //手配マスタテーブルを取得
        public static string getSQLPPPMORDER_Base()
        {
            return " and PM.PART_NO in (  select  PART_NO  from  PPPMORDER  where  PPPMORDER.PART_NO is NOT NULL ";
        }
        //手配マスタ系
        public static string getSQLPPPMORDER_Mask(string PLANT_NO, string MFG_TYPE, string STOCK_TYPE, string ARR_BRANCH_CODE,
                                                  string ARR_WHO, string PO_BRANCH_CODE, string PO_WHO, string BUCKET,
                                                  string ORDER_TYPE, string ABC_TYPE, string STOCK_CODE, string ROUTING_CODE)
        {
            string sql = "";
            if (PLANT_NO != null)
            {
                sql += " and PPPMORDER.PLANT_NO = '" + PLANT_NO + "'";
            }
            if (MFG_TYPE != null)
            {
                sql += " and PPPMORDER.MFG_TYPE = '" + MFG_TYPE + "'";
            }
            if (STOCK_TYPE != null)
            {
                sql += " and PPPMORDER.STOCK_TYPE = '" + STOCK_TYPE + "'";
            }
            if (ARR_BRANCH_CODE != null)
            {
                sql += " and PPPMORDER.ARR_BRANCH_CODE = '" + ARR_BRANCH_CODE + "'";
            }
            if (ARR_WHO != null)
            {
                sql += " and PPPMORDER.ARR_WHO = '" + ARR_WHO + "'";
            }
            if (PO_BRANCH_CODE != null)
            {
                sql += " and PPPMORDER.PO_BRANCH_CODE = '" + PO_BRANCH_CODE + "'";
            }
            if (PO_WHO != null)
            {
                sql += " and PPPMORDER.PO_WHO = '" + PO_WHO + "'";
            }
            if (BUCKET != null)
            {
                sql += " and PPPMORDER.BUCKET = '" + BUCKET + "'";
            }
            if (ORDER_TYPE != null)
            {
                sql += " and PPPMORDER.ORDER_TYPE = '" + ORDER_TYPE + "'";
            }
            if (ABC_TYPE != null)
            {
                sql += " and PPPMORDER.ABC_TYPE = '" + ABC_TYPE + "'";
            }
            if (STOCK_CODE != null)
            {
                sql += " and PPPMORDER.STOCK_CODE = '" + STOCK_CODE + "'";
            }
            if (ROUTING_CODE != null)
            {
                sql += " and PPPMORDER.ROUTING_CODE = '" + ROUTING_CODE + "'";
            }
            if (sql != "")
            {
                return getSQLPPPMORDER_Base() + sql + ")";
            }
            return sql;
        }
        //注文マスタ系テーブルを取得
        public static string getSQLCHMSA_Base()
        {
            return " and PM.PART_NO in (  select  PART_NO  from  CHMSA  where  CHMSA.PART_NO IS NOT NULL ";
        }
        //注文マスタ系
        public static string getSQLCHMSA_Mask(string VENDOR_CODE, string SG_CODE)
        {
            string sql = "";
            if (VENDOR_CODE != null)
            { sql += " and CHMSA.VENDOR_CODE = '" + VENDOR_CODE + "'"; }
            if (SG_CODE != null)
            { sql += " and CHMSA.SG_CODE = '" + SG_CODE + "'"; }
            if (sql != "")
            { return getSQLCHMSA_Base() + sql + ")"; }
            return sql;
        }
        //在庫マスタ系テーブルを取得
        public static string getSQLZKMS_Base()
        {
            return " and PM.PART_NO in (  select PART_NO  from  ZKMS  where   ZKMS.PART_NO IS NOT NULL";
        }
        //在庫マスタ系
        public static string getSQLZKMS_Mask(bool ckUselWHCode_Checked, string[] pWhCode,
                                            string LOCATION, string SOKO_TANTO, string PS_FLAG,
                                            string AUTO_PURCHASE_REQ, bool ckMoreZero_Checked, string CURRENT_BALANCE_1,
                                            string CURRENT_BALANCE_2, string eStockAmount_1, string eStockAmount_2,
                                            string YOTEI_TANKA_1, string YOTEI_TANKA_2, bool ckNoReceipt_Checked,
                                            string LAST_RECEIPT_DATE_1, string LAST_RECEIPT_DATE_2, bool ckNoIssue_Checked,
                                            string LAST_ISSUE_DATE_1, string LAST_ISSUE_DATE_2, string STOCK_START_DATE_1,
                                            string STOCK_START_DATE_2, string STOCK_STOP_FlAG, string STOCK_STOP_DATE)
        {
            string sql = "";
            //倉庫コード 【未完】
            if (ckUselWHCode_Checked)
            {

                int count = 0;
                foreach (string str in pWhCode)
                {
                    if (count == 0)
                    {
                        sql += " and ( ZKMS.WH_CODE = '" + str;
                    }
                    else
                    {
                        sql += "' or ZKMS.WH_CODE = '" + str;
                    }
                    count++;
                }
                if (count != 0)
                    sql += "' )";
            }
            //置場/棚番
            if (LOCATION != null)
            { sql += " and ZKMS.LOCATION =  '" + LOCATION + "'"; }
            //倉庫担当
            if (SOKO_TANTO != null)
            { sql += " and ZKMS.SOKO_TANTO =  '" + SOKO_TANTO + "'"; }
            //P/S展開区分
            if (PS_FLAG != null)
            { sql += " and ZKMS.PS_FLAG =  '" + PS_FLAG + "'"; }
            //自動購入指示
            if (AUTO_PURCHASE_REQ != null)
            { sql += " and ZKMS.AUTO_PURCHASE_REQ =  '" + AUTO_PURCHASE_REQ + "'"; }
            //在庫数 0じゃないチェックOn
            if (ckMoreZero_Checked)
            { sql += " and ZKMS.CURRENT_BALANCE <> 0'"; }
            else
            {
                if (CURRENT_BALANCE_1 != null)
                { sql += " and (ZKMS.CURRENT_BALANCE >= '" + CURRENT_BALANCE_1 + "'"; }
                if (CURRENT_BALANCE_2 != null)
                { sql += " and (ZKMS.CURRENT_BALANCE = '" + CURRENT_BALANCE_2 + "'"; }

            }
            //在庫金額
            if (eStockAmount_1 != null)
            { sql += "  and (ZKMS.CURRENT_BALANCE * ZKYTAN.YOTEI_TANKA) >= '" + eStockAmount_1 + "'"; }
            if (eStockAmount_2 != null)
            { sql += "  and (ZKMS.CURRENT_BALANCE * ZKYTAN.YOTEI_TANKA) <= '" + eStockAmount_2 + "'"; }
            //標準単価
            if (YOTEI_TANKA_1 != null)
            { sql += "  and (ZKMS.CURRENT_BALANCE * ZKYTAN.YOTEI_TANKA) >= '" + YOTEI_TANKA_1 + "'"; }
            if (YOTEI_TANKA_2 != null)
            { sql += "  and (ZKMS.CURRENT_BALANCE * ZKYTAN.YOTEI_TANKA) <= '" + YOTEI_TANKA_2 + "'"; }
            //最終入庫日
            if (ckNoReceipt_Checked)
            { sql += " and ZKMS.LAST_RECEIPT_DATE IS NULL "; }
            else
            {
                if (LAST_RECEIPT_DATE_1 != null)
                { sql += " and (ZKMS.CURRENT_BALANCE >= '" + LAST_RECEIPT_DATE_1 + "'"; }
                if (LAST_RECEIPT_DATE_2 != null)
                { sql += " and (ZKMS.CURRENT_BALANCE <= '" + LAST_RECEIPT_DATE_2 + "'"; }

            }
            //最終出庫日
            if (ckNoIssue_Checked)
            { sql += " and ZKMS.LAST_ISSUE_DATE IS NULL "; }
            else
            {
                if (LAST_RECEIPT_DATE_1 != null)
                { sql += " and (ZKMS.LAST_ISSUE_DATE >= '" + LAST_ISSUE_DATE_1 + "'"; }
                if (LAST_RECEIPT_DATE_2 != null)
                { sql += " and (ZKMS.LAST_ISSUE_DATE <= '" + LAST_ISSUE_DATE_2 + "'"; }

            }
            //貯蔵開始日
            if (LAST_RECEIPT_DATE_1 != null)
            { sql += " and (ZKMS.STOCK_START_DATE >= '" + STOCK_START_DATE_1 + "'"; }
            if (LAST_RECEIPT_DATE_2 != null)
            { sql += " and (ZKMS.STOCK_START_DATE <= '" + STOCK_START_DATE_2 + "'"; }
            //貯蔵中止予定
            if (STOCK_STOP_FlAG != null)
            {
                switch (STOCK_STOP_FlAG)
                {
                    case "0":
                        {
                            sql += " and ZKMS.STOCK_STOP_FlAG = '0'";
                            break;
                        }
                    case "1":
                        {
                            sql += " and ZKMS.STOCK_STOP_FlAG = '1'";
                            break;
                        }
                }
            }
            //貯蔵止のみ
            if (STOCK_STOP_DATE != null)
            {
                switch (STOCK_STOP_DATE)
                {
                    case "0":
                        {
                            sql += " and ZKMS.STOCK_STOP_DATE IS NULL ";
                            break;
                        }
                    case "1":
                        {
                            sql += "  and ZKMS.STOCK_STOP_DATE IS NOT NULL ";
                            break;
                        }
                }
            }
            if (sql != "")
            {
                return getSQLZKMS_Base() + sql + ")";
            }
            return sql;
        }
        //保守マスタ系テーブルを取得
        public static string getSQLPPPMMAINTMS_Base()
        {
            return " and PM.PART_NO in (  select  PART_NO  from  PPPMMAINTMS  where  PPPMMAINTMS.PART_NO IS NOT NULL ";
        }
        //保守マスタ系
        public static string getSQLPPPMMAINTMS_Mask(string PART_LOCATION, string MAINT_TYPE)
        {
            string sql = "";
            //部位
            if (PART_LOCATION != null)
            {
                sql += " and PPPMMAINTMS.PART_LOCATION = '" + PART_LOCATION + "'";
            }
            //保守判定
            if (MAINT_TYPE != null && MAINT_TYPE != "-")
            {
                sql += " and PPPMMAINTMS.MAINT_TYPE = '" + MAINT_TYPE + "'";
            }
            return sql != "" ? getSQLPPPMMAINTMS_Base() + sql + ")" : sql;
        }
        //図面発行後、二次判定が一度も設定されていないもの。
        public static string getSQLckPPPMMAINTMS2_notEdit()
        {
            return " and PM.PART_NO in (select  PART_NO from  PPPMMAINTMS pmm where  MAINT_TYPE = '1'  and not Exists (select 1 from PPPMMAINTMSUPD pmu " +
                   "  where pmu.part_no = pmm.part_no)  and not Exists (select 1 from PPPMMAINTMS pmm_sb  where pmm_sb.part_no = pmm.part_no  and pmm_sb.upd_who = '@FIS')) ";
        }
        //修理提案書利用
        public static string getSQLckRepairRepEnt()
        {
            return " and PM.PART_NO in ( 'select  PART_CODE 'from  SPECREPAIR_COST_LEDGER REP_LED  inner join SPECPROCESS_LOG REP_LOG  on REP_LED.ESTIMATE_NO = REP_LOG.KEY1 " +
                   "  and REP_LED.ESTIMATE_REV_NO = REP_LOG.KEY2 where REP_LED.PART_CODE is not null )  ";
        }
        //写真未登録
        public static string getSQLckNoPhoto()
        {
            return "  and not exists (  select  1  from  PPPMDOCMS DOC  where  DOC.PART_NO = PM.PART_NO  ) and PM.PHOTO_TYPE IS NULL ";
        }
        //部品説明or取替理由が未登録
        public static string getSQLckPartDescAndRepReason()
        {
            return "  and (  PM.PART_DESCRIPTION is null  or PM.REPLACE_REASON is null  )  ";
        }
        //条件確認
        public static string getSQLOption(bool ckPPPMMAINTMS2_notEdit_Checked, bool ckRepairRepEnt_Checked, bool ckNoPhoto_Checked, bool ckPartDescAndRepReason_Checked)
        {
            string sql = "";
            if (ckPPPMMAINTMS2_notEdit_Checked)
            {
                sql += getSQLckPPPMMAINTMS2_notEdit();
            }
            if (ckRepairRepEnt_Checked)
            {
                sql += getSQLckRepairRepEnt();
            }

            if (ckNoPhoto_Checked)
            {
                sql += getSQLckNoPhoto();
            }
            if (ckPartDescAndRepReason_Checked)
            {
                sql += getSQLckPartDescAndRepReason();
            }
            return sql;
        }
        //参照画面（共用マスタ）データを取得
        public static string getSQLDialogKoumoku(string CM_KOUNO, string START_DATE, string STOP_DATE)
        {
            string sql = "";
            sql += "SELECT CM_CODE,CM_CODE_SETUMEI,START_DATE,STOP_DATE FROM cmmsb WHERE ";
            sql += "CM_KOUNO = '" + CM_KOUNO + "'";
            sql += " and START_DATE <= '" + START_DATE + "'";
            sql += " and STOP_DATE >= '" + STOP_DATE + "'";
            sql += " ORDER BY sort_index,CM_CODE ";
            return sql;
        }

        public static string GetSQLUnion(string MS_TABLE, string TABLE_NAME, string PLANT_NO)
        {
            string Union_SQL = "";
            //共用マスター
            Union_SQL += MS_TABLE == "1" ? " select CM_KOUNO,CM_CODE,CM_CODE_SETUMEI,null as FIELD_NAME from cmmsb where CM_KOUNO in (select MS_ITEM_NO from PPPMTABLEHDRMNG" +
                                           " where table_name = '" + TABLE_NAME + "' and MS_TABLE in ('1'))  " : "";
            //注文コードマスター
            Union_SQL += MS_TABLE == "2" ? " select CH_KOUNO CM_KOUNO,CH_CODE CM_CODE ,CH_CODE_SETUMEI_1 CM_CODE_SETUMEI,null as FIELD_NAME from CHCDMS  where CH_KOUNO in " +
                                           " (select MS_ITEM_NO from PPPMTABLEHDRMNG where table_name = '" + TABLE_NAME + "' and MS_TABLE in ('2')) " : "";
            //単位読替マスタ
            Union_SQL += MS_TABLE == "3" && TABLE_NAME != "ZKMS" ? " select TANTO_KUBUN CM_KOUNO , TANTO_CODE CM_CODE,USER_ID CM_CODE_SETUMEI,null as FIELD_NAME from CMTANTOMS where TANTO_KUBUN in " +
                                                                 " (select MS_ITEM_NO from PPPMTABLEHDRMNG where PLANT_NO = '" + PLANT_NO + "' and table_name = '" + TABLE_NAME + "' and MS_TABLE in ('3')) " : "";

            //工程コードマスタ
            //Union_SQL += MS_TABLE == '4' ?
            //Union_SQL += MS_TABLE == '5' ?
            return Union_SQL;
        }
        public static string getSQLUnion(string TABLE_NAME, string CC_CODE, string WC_CODE, string SG_CODE)
        {
            string Extra_Union = "";

            Extra_Union += TABLE_NAME == "KTSTDTIME" ? " union select  null as CM_KOUNO,null as CM_CODE, CM_CODE_SETUMEI,FIELD_NAME from (select  TB1.SETUMEI SG_CODE, TB2.SETUMEI CC_CODE, TB3.CM_CODE_SETUMEI WC_CODE from   KTSGMS TB1   , KTCCMS TB2   , CMMSB TB3   , CMMSB TB4 " +
                                                        " where TB1.WC_CODE = TB2.WC_CODE and TB1.CC_CODE = TB2.CC_CODE and TB3.CM_KOUNO = '020' and TB1.WC_CODE = TB3.CM_CODE and TB4.CM_KOUNO = '010' and TB3.DATA1 = TB4.CM_CODE and TB1.SG_KUBUN <> 'K' and " +
                                                        " TB1.WC_CODE = '" + WC_CODE + "' and TB1.CC_CODE = '" + CC_CODE + "' and TB1.SG_CODE = '" + SG_CODE + "') UNPIVOT(CM_CODE_SETUMEI for FIELD_NAME in(SG_CODE, CC_CODE, WC_CODE) ) " +
                                                        " ) MS_1 on ( MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and MS_1.CM_CODE = NEWTABLE.FIELD_VALUE) or  MS_1.FIELD_NAME = PMHD.FIELD_NAME  "
                                                        : " ) MS_1 on  MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and MS_1.CM_CODE = NEWTABLE.FIELD_VALUE ";
            return Extra_Union;
        }
    }
}