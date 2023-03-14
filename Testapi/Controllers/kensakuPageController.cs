using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Testapi.Models;
using Testapi.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Testapi.Controllers
{
    public class kensakuPageController : ApiController
    {

        [HttpGet]
        [Route("api/KensakuBtnGet")]
        // GET api/<controller>/5
        public List<PMKensaku> KensakuBtn1(
            string PART_NO,                                         //部品コード
            string PART_NAME_LOC1,                                  //部品名
            int FIND_OPTION                                         //部品名検索条件
            )
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PART_NAME_LOC1 = DbContext.FixedSQLi(PART_NAME_LOC1);
                string sql = SqlTable.getSQLTableBase();
                sql += SqlTable.getSQLBuhimei(PART_NO, PART_NAME_LOC1, FIND_OPTION);
                sql += " order by PM.PART_NO ";
                var result = DbContext.Database.SqlQuery<PMKensaku>(sql).ToList();
                return result;

            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<PMKensaku> KensakuBtn2(
            string PRODUCT_TYPE,                                //製品種別
            string ISSUE_DATE_1,                                //標準図発行日１
            string ISSUE_DATE_2,                                //標準図発行日２
            string DWG_CHG_DATE_1,                              //標準図切替日１
            string DWG_CHG_DATE_2,                              //標準図切替日２
            string CHG_NO,                                      //切替通知書No
            string ISSUE_NO                                     //発行通知書No
            )
        {
            using (var DbContext = new TablesDbContext())
            {
                PRODUCT_TYPE = DbContext.FixedSQLi(PRODUCT_TYPE);
                ISSUE_DATE_1 = DbContext.FixedSQLi(ISSUE_DATE_1);
                ISSUE_DATE_2 = DbContext.FixedSQLi(ISSUE_DATE_2);
                DWG_CHG_DATE_1 = DbContext.FixedSQLi(DWG_CHG_DATE_1);
                DWG_CHG_DATE_2 = DbContext.FixedSQLi(DWG_CHG_DATE_2);
                CHG_NO = DbContext.FixedSQLi(CHG_NO);
                ISSUE_NO = DbContext.FixedSQLi(ISSUE_NO);

                string sql = SqlTable.getSQLTableBase();
                if (PRODUCT_TYPE != "-") { sql += "  and DWG.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' "; }           //製品種別
                sql += SqlTable.getSQLHyoujuuhakko(ISSUE_DATE_1, ISSUE_DATE_2, false);                           //標準図発行日
                sql += SqlTable.getSQLHyoujuuKirikae(DWG_CHG_DATE_1, DWG_CHG_DATE_2, CHG_NO, ISSUE_NO);          //標準図切替日 発行通知書No 切替通知書No
                sql += " order by PM.PART_NO ";

                var result = DbContext.Database.SqlQuery<PMKensaku>(sql).ToList();
                return result;

            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<PMKensaku> ShousaiKensaku(
            string PART_NO,                                     //部品コード
            int PART_NO_OPTION,                                 //部品コード検索方法
            string PART_NAME_LOC1,                              //部品名
            int PART_NAME_LOC1_OPTION,                         //部品名検索方法
            string MAINT_PART_NAME,                             //保守部品名
            int MAINT_PART_NAME_OPTION,                        //保守部品名検索方法
            string DWG_NO,                                      //図番
            int DWG_NO_OPTION,                                  //図番検索方法
            string ISSUE_DATE_1,                                //標準図発行日1
            string ISSUE_DATE_2,                                //標準図発行日２
            string PRODUCT_CODE,                                //製品分類コード
            string PART_TYPE,                                   //部品区分
            string PDM_TYPE,                                    //PDM判定
            string MACHINE_TYPE,                                //機種
            string SELLING_PRICE_TYPE,                          //価格設定
            string MAKER_PART_NO,                               //メーカー型番
            string PLANT_NO,                                    //工場区分
            string MFG_TYPE,                                    //内外作区分
            string STOCK_TYPE,                                  //貯蔵区分
            string ARR_BRANCH_CODE,                             //管理店所
            string ARR_WHO,                                     //在庫担当
            string PO_BRANCH_CODE,                              //発注店所
            string PO_WHO,                                      //発注担当
            string BUCKET,                                      //バケット
            string ORDER_TYPE,                                 //管理基準
            string ABC_TYPE,                                    //ABC区分
            string STOCK_CODE,                                  //在庫管理コード
            string ROUTING_CODE,                                //工程コード
            string VENDOR_CODE,                                 //取引先コード
            string SG_CODE,                                     //作業コード
            bool ckUselWHCode_Checked,                          //倉庫コードチェックボックス
            [FromUri]string[] pWhCode,                                      //倉庫コード   //ERROR
            string LOCATION,                                    //置場/棚番
            string SOKO_TANTO,                                  //倉庫担当
            string PS_FLAG,                                     //P/S展開区分
            string AUTO_PURCHASE_REQ,                           //自動購入指示
            bool ckMoreZero_Checked,                            //在庫数チェックボックス
            string CURRENT_BALANCE_1,                           //在庫数１
            string CURRENT_BALANCE_2,                           //在庫数２
            string eStockAmount_1,                              //在庫金額１
            string eStockAmount_2,                              //在庫金額２
            string YOTEI_TANKA_1,                                //標準単価１
            string YOTEI_TANKA_2,                               //標準単価２
            bool ckNoReceipt_Checked,                           //最終入庫日チェックボックス
            string LAST_RECEIPT_DATE_1,                         //最終入庫日１
            string LAST_RECEIPT_DATE_2,                         //最終入庫日２
            bool ckNoIssue_Checked,                             //最終出庫日チェックボックス
            string LAST_ISSUE_DATE_1,                           //最終出庫日１
            string LAST_ISSUE_DATE_2,                           //最終出庫日２
            string STOCK_START_DATE_1,                          //貯蔵開始日１
            string STOCK_START_DATE_2,                          //貯蔵開始日２
            string STOCK_STOP_FlAG,                             //貯蔵中止予定
            string STOCK_STOP_DATE,                             //貯蔵止め
            string PART_LOCATION,                               //部位
            string MAINT_TYPE,                                  //保守判定
            bool ckPPPMMAINTMS2_notEdit_Checked,                //図面発行後、二次判定が一度も設定されていないものチェックボックス
            bool ckPCEntandSPNoEnt_Checked,                     //製造原価登録済且つ販売価格未登録部品チェックボックス
            bool ckRepairRepEnt_Checked,                        //修理提案書利用チェックボックス
            bool ckPartDescAndRepReason_Checked,                //部品説明or取替理由が未登録チェックボックス
            bool ckNoPhoto_Checked                              //写真未登録チェックボックス
            )
        {
            using (var DbContext = new TablesDbContext())
            {
                //SQL　インジェクション対策 
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PART_NAME_LOC1 = DbContext.FixedSQLi(PART_NAME_LOC1);
                MAINT_PART_NAME = DbContext.FixedSQLi(MAINT_PART_NAME);
                DWG_NO = DbContext.FixedSQLi(DWG_NO);
                ISSUE_DATE_1 = DbContext.FixedSQLi(ISSUE_DATE_1);
                ISSUE_DATE_2 = DbContext.FixedSQLi(ISSUE_DATE_2);
                PRODUCT_CODE = DbContext.FixedSQLi(PRODUCT_CODE);
                PART_TYPE = DbContext.FixedSQLi(PART_TYPE);
                PDM_TYPE = DbContext.FixedSQLi(PDM_TYPE);
                MACHINE_TYPE = DbContext.FixedSQLi(MACHINE_TYPE);
                SELLING_PRICE_TYPE = DbContext.FixedSQLi(SELLING_PRICE_TYPE);
                MAKER_PART_NO = DbContext.FixedSQLi(MAKER_PART_NO);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                MFG_TYPE = DbContext.FixedSQLi(MFG_TYPE);
                STOCK_TYPE = DbContext.FixedSQLi(STOCK_TYPE);
                ARR_BRANCH_CODE = DbContext.FixedSQLi(ARR_BRANCH_CODE);
                ARR_WHO = DbContext.FixedSQLi(ARR_WHO);
                PO_BRANCH_CODE = DbContext.FixedSQLi(PO_BRANCH_CODE);
                PO_WHO = DbContext.FixedSQLi(PO_WHO);
                BUCKET = DbContext.FixedSQLi(BUCKET);
                ORDER_TYPE = DbContext.FixedSQLi(ORDER_TYPE);
                ABC_TYPE = DbContext.FixedSQLi(ABC_TYPE);
                STOCK_CODE = DbContext.FixedSQLi(STOCK_CODE);
                ROUTING_CODE = DbContext.FixedSQLi(ROUTING_CODE);
                VENDOR_CODE = DbContext.FixedSQLi(VENDOR_CODE);
                SG_CODE = DbContext.FixedSQLi(SG_CODE);
                //pWhCode             = DbContext.FixedSQLi(pWhCode            );
                LOCATION = DbContext.FixedSQLi(LOCATION);
                SOKO_TANTO = DbContext.FixedSQLi(SOKO_TANTO);
                PS_FLAG = DbContext.FixedSQLi(PS_FLAG);
                AUTO_PURCHASE_REQ = DbContext.FixedSQLi(AUTO_PURCHASE_REQ);
                CURRENT_BALANCE_1 = DbContext.FixedSQLi(CURRENT_BALANCE_1);
                CURRENT_BALANCE_2 = DbContext.FixedSQLi(CURRENT_BALANCE_2);
                eStockAmount_1 = DbContext.FixedSQLi(eStockAmount_1);
                eStockAmount_2 = DbContext.FixedSQLi(eStockAmount_2);
                YOTEI_TANKA_1 = DbContext.FixedSQLi(YOTEI_TANKA_1);
                YOTEI_TANKA_2 = DbContext.FixedSQLi(YOTEI_TANKA_2);
                LAST_RECEIPT_DATE_1 = DbContext.FixedSQLi(LAST_RECEIPT_DATE_1);
                LAST_RECEIPT_DATE_2 = DbContext.FixedSQLi(LAST_RECEIPT_DATE_2);
                LAST_ISSUE_DATE_1 = DbContext.FixedSQLi(LAST_ISSUE_DATE_1);
                LAST_ISSUE_DATE_2 = DbContext.FixedSQLi(LAST_ISSUE_DATE_2);
                STOCK_START_DATE_1 = DbContext.FixedSQLi(STOCK_START_DATE_1);
                STOCK_START_DATE_2 = DbContext.FixedSQLi(STOCK_START_DATE_2);
                STOCK_STOP_FlAG = DbContext.FixedSQLi(STOCK_STOP_FlAG);
                STOCK_STOP_DATE = DbContext.FixedSQLi(STOCK_STOP_DATE);
                PART_LOCATION = DbContext.FixedSQLi(PART_LOCATION);
                MAINT_TYPE = DbContext.FixedSQLi(MAINT_TYPE);

                //データのテーブル取得
                string sql = SqlTable.getSQLTableBase();

                //検索条件
                //PMマスタ系
                sql += SqlTable.getSQLBuhimei(PART_NO, PART_NAME_LOC1, PART_NO_OPTION);                                 //部品コード 部品名
                sql += SqlTable.getSQLHoshuZumei(MAINT_PART_NAME, MAINT_PART_NAME_OPTION,                               //保守部品名
                                                 DWG_NO, DWG_NO_OPTION);                                                 //図番

                sql += SqlTable.getSQLHyoujuuhakko(ISSUE_DATE_1, ISSUE_DATE_2, true);                                    //標準図発行日

                sql += SqlTable.getSQLPM_Mask(PRODUCT_CODE, PART_TYPE, PDM_TYPE,                                        //製品分類コード 部品区分 PDMタイプ
                                              MACHINE_TYPE, SELLING_PRICE_TYPE, MAKER_PART_NO);                         //機種 価格設定 メーカー型番

                sql += SqlTable.getSQLPCEntandSONotEnt(ckPCEntandSPNoEnt_Checked);                                      //PC登録済 かつ SP未登録

                //手配マスタ系
                sql += SqlTable.getSQLPPPMORDER_Mask(PLANT_NO, MFG_TYPE, STOCK_TYPE,                                    //工場区分 内外作区分 貯蔵区分
                                                    ARR_BRANCH_CODE, ARR_WHO, PO_BRANCH_CODE,                            //管理店所 在庫担当 発注店所
                                                    PO_WHO, BUCKET, ORDER_TYPE, ABC_TYPE,                               //発注担当 バケット 管理基準 ABC区分
                                                    STOCK_TYPE, ROUTING_CODE);                                          //在庫管理コード 工程コード

                //注文マスタ系
                sql += SqlTable.getSQLCHMSA_Mask(VENDOR_CODE, SG_CODE);                                                 //取引先コード 作業コード
                                                                                                                        //在庫マスタ系　【未完】倉庫情報未追加


                sql += SqlTable.getSQLZKMS_Mask(ckUselWHCode_Checked, pWhCode,                                         //倉庫コードチェックボックス 倉庫【追加予定】
                                                LOCATION, SOKO_TANTO, PS_FLAG,                                          //置場/棚番　倉庫担当　P/S展開区分
                                                AUTO_PURCHASE_REQ, ckMoreZero_Checked, CURRENT_BALANCE_1,               //自動購入指示　在庫数チェックボックス　在庫数１
                                                CURRENT_BALANCE_2, eStockAmount_1, eStockAmount_2,                       //在庫数2　在庫金額1　在庫金額2
                                                YOTEI_TANKA_1, YOTEI_TANKA_2, ckNoReceipt_Checked,                      //標準単価1 標準単価2 最終入庫日チェックボックス
                                                LAST_RECEIPT_DATE_1, LAST_RECEIPT_DATE_2, ckNoIssue_Checked,             //最終入庫日１　最終入庫日2　最終出庫日チェックボックス
                                                LAST_ISSUE_DATE_1, LAST_ISSUE_DATE_2, STOCK_START_DATE_1,                //最終出庫日1 最終出庫日2 貯蔵開始日1
                                                STOCK_START_DATE_2, STOCK_STOP_FlAG, STOCK_STOP_DATE);                  // 貯蔵開始日2 貯蔵中止予定 貯蔵止め

                //保守マスタ系
                sql += SqlTable.getSQLPPPMMAINTMS_Mask(PART_LOCATION, MAINT_TYPE);
                //検索条件を追加
                sql += SqlTable.getSQLOption(ckPPPMMAINTMS2_notEdit_Checked, ckRepairRepEnt_Checked,
                                    ckPartDescAndRepReason_Checked, ckNoPhoto_Checked);
                sql += " order by PM.PART_NO ";
                //検索開始
                var result = DbContext.Database.SqlQuery<PMKensaku>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<DialogKoumoku> getDialogs(string CM_KOUNO, string START_DATE, string STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);

                string sql = SqlTable.getSQLDialogKoumoku(CM_KOUNO, START_DATE, STOP_DATE);
                var result = DbContext.Database.SqlQuery<DialogKoumoku>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<CMCODE> getCM_CODE(string CM_KOUNO, string START_DATE, string STOP_DATE, bool CM_CODE_ONLY)
        {
            using (var DbContext = new TablesDbContext())
            {
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);

                if (CM_CODE_ONLY)
                {
                    string sql = SqlTable.getSQLDialogKoumoku(CM_KOUNO, START_DATE, STOP_DATE);
                    var result = DbContext.Database.SqlQuery<CMCODE>(sql).ToList();
                    return result;
                }
            }
            return null;
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/CH_CODE")]
        public List<CMCODE> getCMCODE(string CH_KOUNO, string TODAY)
        {
            using (var DbContext = new TablesDbContext())
            {
                CH_KOUNO = DbContext.FixedSQLi(CH_KOUNO);
                TODAY = DbContext.FixedSQLi(TODAY);

                string sql = "select CH_CODE, CH_CODE_SETUMEI_1 from CHCDMS where CH_CODE = '" + CH_KOUNO + "' and START_DATE < '" + TODAY + "' STOP_DATE > '" + TODAY + "'";
                var result = DbContext.Database.SqlQuery<CMCODE>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/TANTO_CODE")]
        public List<TANTOCODE> getTANTOCODE(string PLANT_NO, string TANTO_KUBUN, string TODAY)
        {
            using (var DbContext = new TablesDbContext())
            {
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                TANTO_KUBUN = DbContext.FixedSQLi(TANTO_KUBUN);
                TODAY = DbContext.FixedSQLi(TODAY);

                string sql = "select TANTO_CODE,JNV.USER_NAME  from CMTANTOMS TM left join JNV_JNSHAIN_01 JNV on TM.USER_ID = JNV.USER_ID where PLANT_NO ='" + PLANT_NO
                           + "'and TANTO_KUBUN ='" + TANTO_KUBUN + "' and START_DATE < '" + TODAY + "' STOP_DATE > '" + TODAY + "'";
                var result = DbContext.Database.SqlQuery<TANTOCODE>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/TANI_CODE")]
        public List<TANICODE> getTANICODE(string LANGUAGE)
        {
            using (var DbContext = new TablesDbContext())
            {
                LANGUAGE = DbContext.FixedSQLi(LANGUAGE);

                string sql = "select TANI from NRTANIMS where LANGUAGE ='" + LANGUAGE + "' ";
                var result = DbContext.Database.SqlQuery<TANICODE>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/KTCODE")]
        public List<KTCODE> getKTCODE(string PART_NO, string PLANT_NO, string TODAY)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                string sql = "SELECT DISTINCT KT_CODE FROM KTSTDTIME WHERE PART_NO = '" + PART_NO + "' AND PLANT_NO IN ('" + PLANT_NO + "')" + "' and START_DATE < '" + TODAY + "' STOP_DATE > '" + TODAY + "'";
                var result = DbContext.Database.SqlQuery<KTCODE>(sql).ToList();
                return result;
            }
        }

        [Route("api/KensakuBtnGet")]
        public List<CM_KOUNOLIST> getSokoType(string CM_KOUNO)
        {
            using (var DbContext = new TablesDbContext())
            {
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);

                string sql = "SELECT CM_CODE,CM_CODE_SETUMEI from cmmsb where cm_kouno = '" + CM_KOUNO + "' order by CM_CODE ";
                var result = DbContext.Database.SqlQuery<CM_KOUNOLIST>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<CM_KOUNOLIST> getSokoInfo(string CM_KOUNO, string data3)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "";
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);
                data3 = DbContext.FixedSQLi(data3);
                if (data3 == "-")
                {
                    sql += "select CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO = '310' order by CM_CODE";
                }
                else
                {
                    sql += "select CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO = '" + CM_KOUNO + "'";
                    sql += " and data3 = '" + data3 + "' order by CM_CODE";
                }
                var result = DbContext.Database.SqlQuery<CM_KOUNOLIST>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<KouteiCode> getKouteiInfo(string KT_START_DATE, string KT_STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "";
                sql += "select * from ktktms where START_DATE <= '" + KT_START_DATE + "' and stop_date >= '" + KT_STOP_DATE + "' order by 1";
                var result = DbContext.Database.SqlQuery<KouteiCode>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<HeaderID> getHeaderInfo(string Table_Id)
        {
            using (var DbContext = new TablesDbContext())
            {
                Table_Id = DbContext.FixedSQLi(Table_Id);
                string sql = "";
                sql += "select PM.PART_NO,PM.PART_REV_NO,PM.PART_NAME_LOC1,PM.UPD_WHO,JNV.USER_NAME UPD_NAME,PM.UPD_WHEN,PM.ENT_WHO,JNV_ENT.USER_NAME ENT_NAME,PM.ENT_WHEN " +
                       ",PM.REV_START_DATE,PM.REV_STOP_DATE,PM.M_START_DATE,PM.M_STOP_DATE,PM.CUR_TYPE,PM.APP_CUR_TYPE " + " From PPPMMS PM ";
                sql += " FULL JOIN JNV_JNSHAIN_01 JNV on PM.UPD_WHO = JNV.USER_ID ";
                sql += " FULL JOIN JNV_JNSHAIN_01 JNV_ENT on PM.ENT_WHO = JNV_ENT.USER_ID ";
                sql += " where PM.PART_NO = '" + Table_Id + "' ";
                var result = DbContext.Database.SqlQuery<HeaderID>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<EditInfo> getEditInfo(string Edit_PART_NO, string Edit_REV_NO, string USER_ID)
        {
            if (Edit_PART_NO != null && Edit_REV_NO != null)
            {
                using (var DbContext = new TablesDbContext())
                {
                    Edit_PART_NO = DbContext.FixedSQLi(Edit_PART_NO);
                    Edit_REV_NO = DbContext.FixedSQLi(Edit_REV_NO);
                    USER_ID = DbContext.FixedSQLi(USER_ID);
                    //個人設定を確認
                    string sql_2 = "";
                    string sql_3 = "";
                    bool CheckIndiviSet = false;

                    string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='PPPMMS' order by USER_ID";
                    var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                    foreach (var user in resultIndiviSet)
                    {
                        //個人設定を確認できたら個人並び順に変更
                        if (USER_ID == user.USER_ID)
                        {
                            CheckIndiviSet = true;
                        }
                    }
                    string MS_TABLE_PPPMMSSQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = 'PPPMMS' and MS_TABLE != '0'";
                    var reusult_ms_table_pppmms = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLE_PPPMMSSQL).ToList();
                    string sql_ms_table_pppmms = "";
                    foreach (var str in reusult_ms_table_pppmms)
                    {
                        if (sql_ms_table_pppmms == "")
                        {
                            sql_ms_table_pppmms += "'" + str.MS_TABLE_Find + "'";
                        }
                        else
                        {
                            sql_ms_table_pppmms += ",'" + str.MS_TABLE_Find + "'";
                        }
                    }

                    string sql = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                    string sql_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                    string In_Con = "";
                    string In_Con_Num = "";
                    string In_Con_Num_where = "";
                    var result = DbContext.Database.SqlQuery<TESTCLASS>(sql).ToList();
                    var result_num = DbContext.Database.SqlQuery<TESTCLASS>(sql_num).ToList();
                    foreach (var str in result)
                    {
                        if (In_Con == "")
                        {
                            In_Con = str.COLUMN_NAME;
                        }
                        else
                        {
                            In_Con += "," + str.COLUMN_NAME;
                        }
                    }
                    foreach (var str in result_num)
                    {
                        if (In_Con_Num == "")
                        {
                            In_Con_Num = str.COLUMN_NAME;
                            In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                        }
                        else
                        {
                            In_Con_Num += "," + str.COLUMN_NAME;
                            In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                        }
                    }

                    sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                    sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                    sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                    sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = 'PPPMMS' group by FIELD_NAME,TABLE_NAME";
                    sql_2 += " ) PMTH ";
                    sql_2 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                    sql_3 = sql_2;

                    sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '" + Edit_PART_NO;
                    sql_2 += "' and PART_REV_NO = " + Edit_REV_NO + ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                    sql_2 += " IN(" + In_Con + " ))) ";
                    sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                    sql_2 += " left join (";

                    string UNION_SQL = "";

                    foreach (var item in reusult_ms_table_pppmms)
                    {
                        if (UNION_SQL == "")
                        {
                            UNION_SQL += SqlTable.GetSQLUnion(item.MS_TABLE_Find, "PPPMMS", "1");
                        }
                        else
                        {
                            string Ckvoid = "";
                            Ckvoid += SqlTable.GetSQLUnion(item.MS_TABLE_Find, "PPPMMS", "1");
                            if (Ckvoid != "")
                            {
                                UNION_SQL += "UNION" + Ckvoid;
                            }
                        }
                    }
                    sql_2 += UNION_SQL + " ) MS_1 on  MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and MS_1.CM_CODE = NEWTABLE.FIELD_VALUE ";

                    sql_2 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                    sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";
                    sql_2 = CheckIndiviSet ?
                        "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = 'PPPMMS' and USER_ID = '" + USER_ID +
                        "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL  order by SYD.SEQ_NO"
                        : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                    sql_3 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '" + Edit_PART_NO;
                    sql_3 += "' and PART_REV_NO = " + Edit_REV_NO + ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                    sql_3 += " IN(" + In_Con_Num + " ))) ";
                    sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                    sql_3 += " left join (" + UNION_SQL + " ) MS_1 on  MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and MS_1.CM_CODE = NEWTABLE.FIELD_VALUE ";

                    sql_3 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                    sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";
                    sql_3 = CheckIndiviSet ?
                        "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = 'PPPMMS' and USER_ID = '" + USER_ID +
                        "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL  order by SYD.SEQ_NO"
                        : sql_3 + " order by PMHD.FIELD_SEQ_NO ";
                    var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                    var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();
                    result_2.AddRange(result_3);
                    result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                    return result_2;
                }
            }
            return null;
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<EditInfo> getEditInfo2(string Edit_PART_NO, string USER_ID, string PLANT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                Edit_PART_NO = DbContext.FixedSQLi(Edit_PART_NO);
                USER_ID = DbContext.FixedSQLi(USER_ID);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);

                //個人設定を確認
                string sql_2 = "";
                string sql_3 = "";
                bool CheckIndiviSet = false;
                string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='PPPMORDER' order by USER_ID";
                var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                foreach (var user in resultIndiviSet)
                {
                    //個人設定を確認できたら個人並び順に変更
                    if (USER_ID == user.USER_ID)
                    {
                        CheckIndiviSet = true;
                    }
                }
                string sql = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMORDER' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                string sql_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMORDER' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<TESTCLASS>(sql).ToList();
                var result_num = DbContext.Database.SqlQuery<TESTCLASS>(sql_num).ToList();
                foreach (var str in result)
                {
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                foreach (var str in result_num)
                {
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                    }

                }

                string MS_TABLE_PPPMORDERSSQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = 'PPPMORDER' and MS_TABLE != '0'";
                var reusult_ms_table_pppmorder = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLE_PPPMORDERSSQL).ToList();

                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = 'PPPMORDER' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";
                sql_2 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                sql_3 = sql_2;

                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMORDER where PART_NO =  '" + Edit_PART_NO;
                sql_2 += "' and PLANT_NO = '" + PLANT_NO + "' ) UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_2 += " left join (";

                string UNION_SQL = "";

                foreach (var item in reusult_ms_table_pppmorder)
                {
                    if (UNION_SQL == "")
                    {
                        UNION_SQL += SqlTable.GetSQLUnion(item.MS_TABLE_Find, "PPPMORDER", "1");
                    }
                    else
                    {
                        string Ckvoid = "";
                        Ckvoid += SqlTable.GetSQLUnion(item.MS_TABLE_Find, "PPPMORDER", "1");
                        if (Ckvoid != "")
                        {
                            UNION_SQL += "UNION" + Ckvoid;
                        }
                    }
                }
                sql_2 += UNION_SQL + " ) MS_1 on  MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and MS_1.CM_CODE = NEWTABLE.FIELD_VALUE ";

                sql_2 += " where PMTH.TABLE_NAME = 'PPPMORDER' ";
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = 'PPPMORDER' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";


                sql_3 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMORDER where PART_NO =  '" + Edit_PART_NO;
                sql_3 += "'  and PLANT_NO = '" + PLANT_NO + "') UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";


                sql_3 += " left join (" + UNION_SQL + " ) MS_1 on  MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and MS_1.CM_CODE = NEWTABLE.FIELD_VALUE ";

                sql_3 += " where PMTH.TABLE_NAME = 'PPPMORDER' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";
                sql_3 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = 'PPPMORDER' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL  order by SYD.SEQ_NO"
                    : sql_3 + " order by PMHD.FIELD_SEQ_NO ";


                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();
                result_2.AddRange(result_3);
                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<EditInfo> GetTestdata(string Test)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                string sql_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<TESTCLASS>(sql).ToList();
                var result_num = DbContext.Database.SqlQuery<TESTCLASS>(sql_num).ToList();
                foreach (var str in result)
                {
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                foreach (var str in result_num)
                {
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                    }
                }

                string sql_2 = "";

                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE from PPPMTABLEAUTHMNG PMTH ";
                sql_2 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME ";
                sql_2 += " and PMHD.FIELD_NAME = PMTH.FIELD_NAME ";

                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '5310GAF001' and PART_REV_NO ='000' )";
                sql_2 += " UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_2 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                sql_2 += " and PMTH.MNG_NO = '1' ";
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";
                sql_2 += " order by PMTH.FIELD_SEQ_NO ";
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();

                string sql_3 = "";

                sql_3 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE from PPPMTABLEAUTHMNG PMTH ";
                sql_3 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME ";
                sql_3 += " and PMHD.FIELD_NAME = PMTH.FIELD_NAME ";

                sql_3 += " LEFT JOIN (select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '5310GAF001' and PART_REV_NO ='000' )";
                sql_3 += " UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_3 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                sql_3 += " and PMTH.MNG_NO = '1' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";
                sql_3 += " order by PMTH.FIELD_SEQ_NO ";
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();
                result_2.AddRange(result_3);

                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));

                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<CHOUMON> GetChoumon(string CH_KOUNO, string START_DATE, string STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                CH_KOUNO = DbContext.FixedSQLi(CH_KOUNO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);
                string sql = "select CH_CODE,CH_CODE_SETUMEI_1,START_DATE,STOP_DATE from CHCDMS where CH_KOUNO ='" + CH_KOUNO + "'AND START_DATE <= '" + START_DATE + "' AND STOP_DATE >= '" + STOP_DATE + "' ORDER BY CH_CODE";
                var result = DbContext.Database.SqlQuery<CHOUMON>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<TANTOU> GetTantou(string TANTO_KUBUN, string PLANT_NO, string START_DATE, string STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                TANTO_KUBUN = DbContext.FixedSQLi(TANTO_KUBUN);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);
                string sql = "select TM.TANTO_CODE,TM.USER_ID,JNV.USER_NAME,TM.START_DATE,TM.STOP_DATE from CMTANTOMS TM ";
                sql += " left join JNV_JNSHAIN_01 JNV on TM.USER_ID = JNV.USER_ID ";
                sql += " where START_DATE <= '" + START_DATE + "' AND STOP_DATE >= '" + STOP_DATE + "' ";
                sql += " AND TANTO_KUBUN = '" + TANTO_KUBUN + "' AND PLANT_NO = '" + PLANT_NO + "' ";
                sql += " ORDER BY TANTO_CODE";
                var result = DbContext.Database.SqlQuery<TANTOU>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<TANTAI> GetTantai(string LANGU)
        {
            using (var DbContext = new TablesDbContext())
            {
                LANGU = DbContext.FixedSQLi(LANGU);
                string sql = "select * from NRTANIMS where  LANGUAGE  = '" + LANGU + "'";
                var result = DbContext.Database.SqlQuery<TANTAI>(sql).ToList();
                return result;
            }
        }


        [HttpGet]
        [Route("api/KensakuBtnGet/UserSetting")]
        public List<UserSetting_DepartmentUsername> GetUser_departName(string PROJECT_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                PROJECT_ID = DbContext.FixedSQLi(PROJECT_ID);
                string sql = "select'[' || DEPT_CODE || ']' || ' ' || DEPT_CODE_J DEPARTMENT_CODE, '[' || USER_ID || ']' || ' ' || USER_NAME USER_CODE from JNV_JNSHAIN_01" +
                    " where USER_ID in ( select UPD_WHO from SYDBGRID where PROJECT_ID = '" + PROJECT_ID + "' group by UPD_WHO) order by DEPT_CODE, USER_ID";
                var result = DbContext.Database.SqlQuery<UserSetting_DepartmentUsername>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<Pic> GetPic(string PIC_PART_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PIC_PART_NO = DbContext.FixedSQLi(PIC_PART_NO);
                string sql = "select * from pppmdocms where PART_NO = '5310A1'";
                var result = DbContext.Database.SqlQuery<Pic>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/Seisaku")]
        public List<KTCODE> getKTCODE(string PART_NO, string PLANT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                string sql = "SELECT DISTINCT KT_CODE FROM KTSTDTIME WHERE PART_NO = '" + PART_NO + "' AND PLANT_NO IN ('" + PLANT_NO + "')";
                var result = DbContext.Database.SqlQuery<KTCODE>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/Seisaku")]
        public List<KouteiJunjou> GetKouteiTable(string PART_NO, string KT_CODE, string PLANT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                KT_CODE = DbContext.FixedSQLi(KT_CODE);
                string sql = "SELECT * FROM KTSTDTIME WHERE PART_NO = '" + PART_NO + "' AND KT_CODE = '" + KT_CODE + "' AND PLANT_NO IN ('" + PLANT_NO + "')";
                var result = DbContext.Database.SqlQuery<KouteiJunjou>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/Seisaku")]
        public List<EditInfo> GetKTSTDTIME_EditTable(string PART_NO, string KT_CODE, string PLANT_NO, string SEQ_NO, string USER_ID, string TABLE_NAME, string CC_CODE, string SG_CODE, string WC_CODE)
        {
            //　確認条件を確認
            if (PART_NO != null && KT_CODE != null && PLANT_NO != null && SEQ_NO != null && TABLE_NAME != null && USER_ID != null)
            {
                using (var DbContext = new TablesDbContext())
                {
                    //　SQL インジェクション対策
                    PART_NO = DbContext.FixedSQLi(PART_NO);
                    KT_CODE = DbContext.FixedSQLi(KT_CODE);
                    PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                    SEQ_NO = DbContext.FixedSQLi(SEQ_NO);
                    TABLE_NAME = DbContext.FixedSQLi(TABLE_NAME);
                    USER_ID = DbContext.FixedSQLi(USER_ID);
                    CC_CODE = DbContext.FixedSQLi(CC_CODE);
                    SG_CODE = DbContext.FixedSQLi(SG_CODE);
                    WC_CODE = DbContext.FixedSQLi(WC_CODE);

                    //　CheckIndivSet 個人並び順の設定を確認するプロパティ
                    // 　False : ユーザーは個人並び順を未設定
                    // 　True　: ユーザーは個人並び順を未設定している
                    bool CheckIndiviSet = false;

                    //　IndivSetSQL 個人並び順設定ユーザーリストを取得のSQLコマンド
                    string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='" + TABLE_NAME + "' order by USER_ID";
                    //　resultIndiviSet 個人並び順設定ユーザーリスト
                    var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                    //　現在使用したユーザーは個人並び順設定確認
                    foreach (var user in resultIndiviSet)
                    {
                        //　設定した場合 CheckIndivSet => True に変更
                        if (USER_ID == user.USER_ID)
                        {
                            CheckIndiviSet = true;
                        }
                    }

                    //　MS_TABLE_SQL 他のデータベースにアクセスリスト取得のSQLコマンド
                    string MS_TABLEL_SQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = '" + TABLE_NAME + "' and MS_TABLE != '0'";
                    //　reusult_ms_table_ktstdtim 他のデータベースにアクセスリスト
                    var reusult_ms_table = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLEL_SQL).ToList();

                    //　テーブル内データが別タイプのため、文字タイプデータと数字タイプデータを別々検査必要があります
                    //　column_name テーブルの列名リスト(文字タイプ)を取得
                    string column_name = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME + "' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                    //　column_name_num テーブルの列名リスト(数字タイプ)を取得
                    string column_name_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME + "' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                    //　In_Con,In_Con_Num,In_Con_Num_where SQLコマンドにデータ居場所を特定するプロパティ
                    string In_Con = "";
                    string In_Con_Num = "";
                    string In_Con_Num_where = "";
                    var result = DbContext.Database.SqlQuery<COLUMNNAME>(column_name).ToList();
                    var result_num = DbContext.Database.SqlQuery<COLUMNNAME>(column_name_num).ToList();
                    //　検索結果のリストを SQLコマンドに変更
                    //　テーブルの列名リスト(文字タイプ)
                    foreach (var str in result)
                    {
                        //　下記で　SQLコマンドで使用
                        //　             　　　　　　 In_Con
                        //                             ↓
                        //　Where ○○　in ('○○','○○',.....,'○○')
                        if (In_Con == "")
                        {
                            In_Con = str.COLUMN_NAME;
                        }
                        else
                        {
                            In_Con += "," + str.COLUMN_NAME;
                        }
                    }
                    //　テーブルの列名リスト(数字タイプ)
                    foreach (var str in result_num)
                    {
                        //　下記で　SQLコマンドで使用
                        //　              　In_Con_Num,In_Con_Num_where
                        //                             ↓
                        //　Where ○○　in ('○○','○○',.....,'○○')
                        if (In_Con_Num == "")
                        {
                            In_Con_Num = str.COLUMN_NAME;
                            In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                        }
                        else
                        {
                            In_Con_Num += "," + str.COLUMN_NAME;
                            In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                        }
                    }
                    string sql_2 = "";
                    string sql_3 = "";

                    //　クライアント側に送るデータフォーマットを設定　
                    sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                    //　テーブル項目の表示状態を取得
                    //　MAX(AUTH_TYPE) 表示状態が複数がある場合高い権限の方に優先する
                    //　AUTH_TYPE   値  状態　　　　 優先順
                    //              2　変更可能     　高
                    //              1  表示　　　　　 
                    //              0  未表示　　　　 低
                    sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                    sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                    sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = '" + TABLE_NAME + "' group by FIELD_NAME,TABLE_NAME";
                    sql_2 += " ) PMTH ";

                    //　テーブルの表示設定を取得
                    //　PPPMTABLEHDRMNG　テーブルから表示の設定取得
                    sql_2 += " full join PPPMTABLEHDRMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                    //　データ値を取得　例の値から行の値に変更必要ので UNPIVOTを使用
                    //  取得したデータは下記の通りになります。
                    // 　　　　　　　　変更前                                                          変更後
                    //  FIELD_NAME_1   FIELD_NAME_2   ....  FIELD_NAME_X　　　　=> 　　　　 FIELD_NAME　　　FIELD_VALUE　
                    //  FIELD_VALUE_1  FIELD_VALUE_2  ....  FIELD_VALUE_X                   FIELD_NAME_1　　FIELD_VALUE_1
                    //                                                                      FIELD_NAME_2    FIELD_VALUE_2
                    //                                                                                ........
                    //                                                                      FIELD_NAME_X    FIELD_VALUE_X
                    //
                    sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM " + TABLE_NAME;
                    sql_2 += " where PART_NO =  '" + PART_NO + "' AND PLANT_NO = '" + PLANT_NO + "' AND KT_CODE = '" + KT_CODE + "' AND SEQ_NO = '" + SEQ_NO + "'";
                    sql_2 += ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";

                    //基本情報を　sql_3に保存
                    sql_3 = sql_2;

                    //  IN_Con 文字タイプ値だけを検索条件を追加
                    sql_2 += " IN(" + In_Con + " ))) ";
                    sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                    //  データ説明例を追加
                    sql_2 += " left join (";
                    // UNION_SQL
                    string UNION_SQL = "";
                    foreach (var item in reusult_ms_table)
                    {
                        if (UNION_SQL == "")
                        {
                            UNION_SQL += SqlTable.GetSQLUnion(item.MS_TABLE_Find, "KTSTDTIME", PLANT_NO);
                        }
                        else
                        {
                            string Ckvoid = "";
                            Ckvoid += SqlTable.GetSQLUnion(item.MS_TABLE_Find, "KTSTDTIME", PLANT_NO);
                            if (Ckvoid != "")
                            {
                                UNION_SQL += " UNION " + Ckvoid;
                            }
                        }
                    }
                    // 特定のデータに説明追加
                    UNION_SQL += SqlTable.getSQLUnion(TABLE_NAME, CC_CODE, WC_CODE, SG_CODE);
                    sql_2 += UNION_SQL;


                    sql_2 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";

                    //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                    //  PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")" 文字タイプだけ検索条件を追加
                    sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";

                    //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                    sql_2 = CheckIndiviSet ?
                        "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                        "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL and SYD.COL_VISIBLE IS NULL order by SYD.SEQ_NO"
                        : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                    //  In_Con_Num 数字タイプ値だけを検索条件を追加
                    sql_3 += " IN(" + In_Con_Num + " ))) ";
                    sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                    //  データ説明例を追加
                    sql_3 += " left join (" + UNION_SQL;

                    //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                    //  PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")" 数字タイプだけ検索条件を追加
                    sql_3 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";
                    sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";

                    //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                    sql_3 = CheckIndiviSet ?
                        "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME ,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                        "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL  order by SYD.SEQ_NO"
                        : sql_3 + " order by PMHD.FIELD_SEQ_NO ";

                    //  検査を開始する
                    var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                    var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();

                    //  検査結果を結合
                    result_2.AddRange(result_3);

                    //  並び順の通りに並べる
                    result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                    return result_2;
                }
            }
            //確認条件が揃ってない場合　NULLで返します
            return null;
        }

        //ユーザー設定画面追加の表示リスト取得
        [HttpGet]
        [Route("api/KensakuBtnGet/UserSettingVis")]
        public List<User_Setting> Get_UserSettting_Visible(string USER_ID, string DBGRID_NAME)
        {
            using (var DbContext = new TablesDbContext())
            {
                USER_ID = DbContext.FixedSQLi(USER_ID);
                DBGRID_NAME = DbContext.FixedSQLi(DBGRID_NAME);

                //　CheckIndivSet 個人並び順の設定を確認するプロパティ
                // 　False : ユーザーは個人並び順を未設定
                // 　True　: ユーザーは個人並び順を未設定している
                bool CheckIndiviSet = false;

                //　IndivSetSQL 個人並び順設定ユーザーリストを取得のSQLコマンド
                string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='" + DBGRID_NAME + "' order by USER_ID";
                //　resultIndiviSet 個人並び順設定ユーザーリスト
                var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                //　現在使用したユーザーは個人並び順設定確認
                foreach (var user in resultIndiviSet)
                {
                    //　設定した場合 CheckIndivSet => True に変更
                    if (USER_ID == user.USER_ID)
                    {
                        CheckIndiviSet = true;
                    }
                }
                //　個人並び順の設定があれば　SYDBGRIDのテーブルを取得、なければPPPMTABLEHDRMNGから取得
                string sql = CheckIndiviSet ? "SELECT FIELD_NAME,FIELD_NAME_J,SEQ_NO,COL_VISIBLE FROM SYDBGRID WHERE USER_ID ='" + USER_ID + "' AND DBGRID_NAME = '" + DBGRID_NAME + "' ORDER BY SEQ_NO" :
                                               "SELECT  FIELD_NAME,FIELD_NAME_LOC1 FIELD_NAME_J,FIELD_SEQ_NO SEQ_NO,'1' as COL_VISIBLE, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' " +
                                               " AND ROLE_ID IN ('1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '99')) and TABLE_NAME = '" + DBGRID_NAME + "' and AUTH_TYPE in (1, 2) group by FIELD_NAME,FIELD_NAME_LOC1,FIELD_SEQ_NO order by FIELD_SEQ_NO ";

                var result = DbContext.Database.SqlQuery<User_Setting>(sql).ToList();
                return result;
            }
        }
        //権限状態を取得
        [HttpGet]
        [Route("api/KensakuBtnGet/AUTH_ST")]
        public List<AUTH_ST> Get_AUTH_ST(string USER_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                USER_ID = DbContext.FixedSQLi(USER_ID);
                string sql = "select ROLE_ID , ROLE_VALUE from CPUMGSSO_USER_ROLE_MST where SYSTEM_ID = 'S0000018' and DISABLED_FLAG = '0'and ROLE_Id in ('A1','A3','A4') and USER_ID = '" + USER_ID + "' ";
                var result = DbContext.Database.SqlQuery<AUTH_ST>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/CHMSA")]
        public List<CHMSA_TABLE> GET_CHMSA_PRIORITY(string PART_NO, string PLANT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);

                string sql = "select CH.SG_CODE ,CH.PRIORITY ,CH.VENDOR_CODE ,VN.VENDOR_NAME_J from CHMSA CH "
                              + " left join(select VENDOR_CODE, VENDOR_NAME_J from CHVENDOR) VN on SUBSTR(CH.VENDOR_CODE, 3) = VN.VENDOR_CODE"
                              + " where PART_NO = '" + PART_NO + "'and PLANT_NO = '" + PLANT_NO + "' order by CH.PRIORITY";

                var result = DbContext.Database.SqlQuery<CHMSA_TABLE>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/CHMSA_TABLE")]
        public List<EditInfo> GET_CHMSA_TABLE(string PART_NO, string PLANT_NO, string USER_ID, string SG_CODE, string VENDOR_CODE, string PRIORITY)
        {
            using (var DbContext = new TablesDbContext())
            {
                string TABLE_NAME = "CHMSA";
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                USER_ID = DbContext.FixedSQLi(USER_ID);
                PRIORITY = DbContext.FixedSQLi(PRIORITY);

                // CheckIndiviSet 個人並び順登録確認
                // True  : 登録
                // False : 未登録
                bool CheckIndiviSet = CheckIndivi(USER_ID, TABLE_NAME);

                //　MS_TABLE_SQL 他のデータベースにアクセスリスト取得のSQLコマンド
                string MS_TABLEL_SQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = '" + TABLE_NAME + "' and MS_TABLE != '0'";
                //　reusult_ms_table_ktstdtim 他のデータベースにアクセスリスト
                var reusult_ms_table = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLEL_SQL).ToList();


                //　テーブル内データが別タイプのため、文字タイプデータと数字タイプデータを別々検査必要があります
                //　column_name テーブルの列名リスト(文字タイプ)を取得
                string column_name = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME + "' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                //　column_name_num テーブルの列名リスト(数字タイプ)を取得
                string column_name_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME + "' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                //　In_Con,In_Con_Num,In_Con_Num_where SQLコマンドにデータ居場所を特定するプロパティ
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<COLUMNNAME>(column_name).ToList();
                var result_num = DbContext.Database.SqlQuery<COLUMNNAME>(column_name_num).ToList();
                //　検索結果のリストを SQLコマンドに変更
                //　テーブルの列名リスト(文字タイプ)
                foreach (var str in result)
                {
                    //　下記で　SQLコマンドで使用
                    //　             　　　　　　 In_Con
                    //                             ↓
                    //　Where ○○　in ('○○','○○',.....,'○○')
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                //　テーブルの列名リスト(数字タイプ)
                foreach (var str in result_num)
                {
                    //　下記で　SQLコマンドで使用
                    //　              　In_Con_Num,In_Con_Num_where
                    //                             ↓
                    //　Where ○○　in ('○○','○○',.....,'○○')
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                    }
                }
                string sql_2 = "";
                string sql_3 = "";

                //　クライアント側に送るデータフォーマットを設定　
                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                //　テーブル項目の表示状態を取得
                //　MAX(AUTH_TYPE) 表示状態が複数がある場合高い権限の方に優先する
                //　AUTH_TYPE   値  状態　　　　 優先順
                //              2　変更可能     　高
                //              1  表示　　　　　 
                //              0  未表示　　　　 低
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = '" + TABLE_NAME + "' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";

                //　テーブルの表示設定を取得
                //　PPPMTABLEHDRMNG　テーブルから表示の設定取得
                sql_2 += " full join PPPMTABLEHDRMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                //　データ値を取得　例の値から行の値に変更必要ので UNPIVOTを使用
                //  取得したデータは下記の通りになります。
                // 　　　　　　　　変更前                                                          変更後
                //  FIELD_NAME_1   FIELD_NAME_2   ....  FIELD_NAME_X　　　　=> 　　　　 FIELD_NAME　　　FIELD_VALUE　
                //  FIELD_VALUE_1  FIELD_VALUE_2  ....  FIELD_VALUE_X                   FIELD_NAME_1　　FIELD_VALUE_1
                //                                                                      FIELD_NAME_2    FIELD_VALUE_2
                //                                                                                ........
                //                                                                      FIELD_NAME_X    FIELD_VALUE_X
                //
                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM " + TABLE_NAME;
                sql_2 += " where PART_NO =  '" + PART_NO + "' AND PLANT_NO = '" + PLANT_NO + "' AND SG_CODE = '" + SG_CODE + "' AND VENDOR_CODE = '" + VENDOR_CODE + "' AND PRIORITY = '" + PRIORITY + "' ";
                sql_2 += ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";

                //基本情報を　sql_3に保存
                sql_3 = sql_2;

                //  IN_Con 文字タイプ値だけを検索条件を追加
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                //  データ説明例を追加
                sql_2 += " left join (";
                // UNION_SQL
                string UNION_SQL = "";
                foreach (var item in reusult_ms_table)
                {
                    if (UNION_SQL == "")
                    {
                        UNION_SQL += SqlTable.GetSQLUnion(item.MS_TABLE_Find, TABLE_NAME, PLANT_NO);
                    }
                    else
                    {
                        string Ckvoid = "";
                        Ckvoid += SqlTable.GetSQLUnion(item.MS_TABLE_Find, TABLE_NAME, PLANT_NO);
                        if (Ckvoid != "")
                        {
                            UNION_SQL += " UNION " + Ckvoid;
                        }
                    }
                }


                // 特定のデータに説明追加
                UNION_SQL += SqlTable.getSQLUnion(TABLE_NAME, "", "", SG_CODE);
                sql_2 += UNION_SQL;

                sql_2 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";

                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")" 文字タイプだけ検索条件を追加
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL and SYD.COL_VISIBLE IS NULL order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                //  In_Con_Num 数字タイプ値だけを検索条件を追加
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";


                //  データ説明例を追加
                sql_3 += " left join (" + UNION_SQL;


                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")" 数字タイプだけ検索条件を追加
                sql_3 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_3 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME ,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL  order by SYD.SEQ_NO"
                    : sql_3 + " order by PMHD.FIELD_SEQ_NO ";

                //  検査を開始する
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();

                //  検査結果を結合
                result_2.AddRange(result_3);

                //  並び順の通りに並べる
                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/SG_CODE_DROPDOWN")]
        public List<SG_CODE_DROPDOWN> GET_SG_DROPDOWN(string SG_KUBUN)
        {
            using (var DbContext = new TablesDbContext())
            {
                SG_KUBUN = DbContext.FixedSQLi(SG_KUBUN);
                string sql = "select SG_CODE || '*' || SETUMEI SG_CODE from KTSGMS where CC_CODE = '--' and WC_CODE ='--' and SG_KUBUN ='" + SG_KUBUN + "' ORDER BY SG_CODE";
                var result = DbContext.Database.SqlQuery<SG_CODE_DROPDOWN>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMPOSPEC")]
        public List<EditInfo> GET_PPPMPOSPEC(string PART_NO, string WORK_CODE, string USER_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                string TABLE_NAME = "PPPMPOSPEC";

                PART_NO = DbContext.FixedSQLi(PART_NO);
                WORK_CODE = DbContext.FixedSQLi(WORK_CODE);
                USER_ID = DbContext.FixedSQLi(USER_ID);

                // CheckIndiviSet 個人並び順登録確認
                // True  : 登録
                // False : 未登録
                bool CheckIndiviSet = CheckIndivi(USER_ID, TABLE_NAME);

                //　MS_TABLE_SQL 他のデータベースにアクセスリスト取得のSQLコマンド
                string MS_TABLEL_SQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = '" + TABLE_NAME + "' and MS_TABLE != '0'";
                //　reusult_ms_table_ktstdtim 他のデータベースにアクセスリスト
                var reusult_ms_table = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLEL_SQL).ToList();


                //　テーブル内データが別タイプのため、文字タイプデータと数字タイプデータを別々検査必要があります
                //　column_name テーブルの列名リスト(文字タイプ)を取得
                string column_name = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME + "' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                //　In_Con,In_Con_Num,In_Con_Num_where SQLコマンドにデータ居場所を特定するプロパティ
                string In_Con = "";
                var result = DbContext.Database.SqlQuery<COLUMNNAME>(column_name).ToList();
                //　検索結果のリストを SQLコマンドに変更
                //　テーブルの列名リスト(文字タイプ)
                foreach (var str in result)
                {
                    //　下記で　SQLコマンドで使用
                    //　             　　　　　　 In_Con
                    //                             ↓
                    //　Where ○○　in ('○○','○○',.....,'○○')
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                string sql_2 = "";

                //　クライアント側に送るデータフォーマットを設定　
                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,null as FIELD_EXPLAIN from( ";
                //　テーブル項目の表示状態を取得
                //　MAX(AUTH_TYPE) 表示状態が複数がある場合高い権限の方に優先する
                //　AUTH_TYPE   値  状態　　　　 優先順
                //              2　変更可能     　高
                //              1  表示　　　　　 
                //              0  未表示　　　　 低
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = '" + TABLE_NAME + "' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";

                //　テーブルの表示設定を取得
                //　PPPMTABLEHDRMNG　テーブルから表示の設定取得
                sql_2 += " full join PPPMTABLEHDRMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                //　データ値を取得　例の値から行の値に変更必要ので UNPIVOTを使用
                //  取得したデータは下記の通りになります。
                // 　　　　　　　　変更前                                                          変更後
                //  FIELD_NAME_1   FIELD_NAME_2   ....  FIELD_NAME_X　　　　=> 　　　　 FIELD_NAME　　　FIELD_VALUE　
                //  FIELD_VALUE_1  FIELD_VALUE_2  ....  FIELD_VALUE_X                   FIELD_NAME_1　　FIELD_VALUE_1
                //                                                                      FIELD_NAME_2    FIELD_VALUE_2
                //                                                                                ........
                //                                                                      FIELD_NAME_X    FIELD_VALUE_X
                //
                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM " + TABLE_NAME;
                sql_2 += " where PART_NO =  '" + PART_NO + "' AND WORK_CODE = '" + WORK_CODE + "'";
                sql_2 += ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";

                //  IN_Con 文字タイプ値だけを検索条件を追加
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_2 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";
                sql_2 += " and AUTH_TYPE <> 0 ";

                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")" 文字タイプだけ検索条件を追加
                //sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL and SYD.COL_VISIBLE IS NULL order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                //  検査を開始する
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();

                //  並び順の通りに並べる
                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/CountENT_PPPMMORDER")]
        public List<Count_ENT> CountENTData(string PART_NO, string PLANT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);

                string sql = "SELECT COUNT(*) COUNT FROM PPPMORDER WHERE PART_NO = '" + PART_NO + "' AND PLANT_NO ='" + PLANT_NO + "'";
                var result = DbContext.Database.SqlQuery<Count_ENT>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/CHMSB")]
        public List<CHMSB> GET_CHMSB(string PART_NO, string PLANT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                string sql = "select * from CHMSB where PLANT_NO ='" + PLANT_NO + "' and PART_NO ='" + PART_NO + "' ";
                var result = DbContext.Database.SqlQuery<CHMSB>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/KOUBA")]
        public List<KOUBA> GET_FT_KOUBA(string departmentId)
        {
            using (var DbContext = new TablesDbContext())
            {
                departmentId = DbContext.FixedSQLi(departmentId);

                string sql = "select data1 from CMMSB where CM_KOUNO = '020' and CM_CODE in ( select DATA1 from CMMSB where CM_KOUNO = '030' and CM_CODE = '" + departmentId + "' )";
                var result = DbContext.Database.SqlQuery<KOUBA>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/ZKMS_KOUBA")]
        public List<CM_KOUNOLIST> GET_FT_ZKMS_KOUBA(string DATA4)
        {
            using (var DbContext = new TablesDbContext())
            {
                DATA4 = DbContext.FixedSQLi(DATA4);

                string sql = "select CM_CODE,CM_CODE_SETUMEI from CMMSB where CM_KOUNO = '010' and CM_CODE in ( SELECT DATA3 from CMMSB WHERE CM_KOUNO = '310' and DATA4 = '" + DATA4 + "') ";
                var result = DbContext.Database.SqlQuery<CM_KOUNOLIST>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/ZKMS")]
        public List<EditInfo> GET_ZKMS(string PART_NO, string SOKO_CODE, string USER_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                string TABLE_NAME = "ZKMS";
                PART_NO = DbContext.FixedSQLi(PART_NO);
                SOKO_CODE = DbContext.FixedSQLi(SOKO_CODE);
                USER_ID = DbContext.FixedSQLi(USER_ID);

                // CheckIndiviSet 個人並び順登録確認
                // True  : 登録
                // False : 未登録
                bool CheckIndiviSet = CheckIndivi(USER_ID, TABLE_NAME);

                //　MS_TABLE_SQL 他のデータベースにアクセスリスト取得のSQLコマンド
                string MS_TABLEL_SQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = '" + TABLE_NAME + "' and MS_TABLE != '0'";
                //　reusult_ms_table_ktstdtim 他のデータベースにアクセスリスト
                var reusult_ms_table = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLEL_SQL).ToList();


                //　テーブル内データが別タイプのため、文字タイプデータと数字タイプデータを別々検査必要があります
                //　column_name テーブルの列名リスト(文字タイプ)を取得
                string column_name = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME + "' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                //　column_name_num テーブルの列名リスト(数字タイプ)を取得
                string column_name_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME + "' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                //　In_Con,In_Con_Num,In_Con_Num_where SQLコマンドにデータ居場所を特定するプロパティ
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<COLUMNNAME>(column_name).ToList();
                var result_num = DbContext.Database.SqlQuery<COLUMNNAME>(column_name_num).ToList();
                //　検索結果のリストを SQLコマンドに変更
                //　テーブルの列名リスト(文字タイプ)
                foreach (var str in result)
                {
                    //　下記で　SQLコマンドで使用
                    //　             　　　　　　 In_Con
                    //                             ↓
                    //　Where ○○　in ('○○','○○',.....,'○○')
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                //　テーブルの列名リスト(数字タイプ)
                foreach (var str in result_num)
                {
                    //　下記で　SQLコマンドで使用
                    //　              　In_Con_Num,In_Con_Num_where
                    //                             ↓
                    //　Where ○○　in ('○○','○○',.....,'○○')
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                    }
                }
                string sql_2 = "";
                string sql_3 = "";

                //　クライアント側に送るデータフォーマットを設定　
                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                //　テーブル項目の表示状態を取得
                //　MAX(AUTH_TYPE) 表示状態が複数がある場合高い権限の方に優先する
                //　AUTH_TYPE   値  状態　　　　 優先順
                //              2　変更可能     　高
                //              1  表示　　　　　 
                //              0  未表示　　　　 低
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = '" + TABLE_NAME + "' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";

                //　テーブルの表示設定を取得
                //　PPPMTABLEHDRMNG　テーブルから表示の設定取得
                sql_2 += " full join PPPMTABLEHDRMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                //　データ値を取得　例の値から行の値に変更必要ので UNPIVOTを使用
                //  取得したデータは下記の通りになります。
                // 　　　　　　　　変更前                                                          変更後
                //  FIELD_NAME_1   FIELD_NAME_2   ....  FIELD_NAME_X　　　　=> 　　　　 FIELD_NAME　　　FIELD_VALUE　
                //  FIELD_VALUE_1  FIELD_VALUE_2  ....  FIELD_VALUE_X                   FIELD_NAME_1　　FIELD_VALUE_1
                //                                                                      FIELD_NAME_2    FIELD_VALUE_2
                //                                                                                ........
                //                                                                      FIELD_NAME_X    FIELD_VALUE_X
                //
                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM " + TABLE_NAME;
                sql_2 += " where PART_NO =  '" + PART_NO + "' AND SOKO_CODE = '" + SOKO_CODE + "' ";
                sql_2 += ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";

                //基本情報を　sql_3に保存
                sql_3 = sql_2;

                //  IN_Con 文字タイプ値だけを検索条件を追加
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                //  データ説明例を追加
                sql_2 += " left join (";
                // UNION_SQL
                string UNION_SQL = "";
                foreach (var item in reusult_ms_table)
                {
                    if (UNION_SQL == "")
                    {
                        UNION_SQL += SqlTable.GetSQLUnion(item.MS_TABLE_Find, TABLE_NAME, "");
                    }
                    else
                    {
                        string Ckvoid = "";
                        Ckvoid += SqlTable.GetSQLUnion(item.MS_TABLE_Find, TABLE_NAME, "");
                        if (Ckvoid != "")
                        {
                            UNION_SQL += " UNION " + Ckvoid;
                        }
                    }
                }

                // 特定のデータに説明追加
                UNION_SQL += SqlTable.getSQLUnion(TABLE_NAME, "", "", "");
                sql_2 += UNION_SQL;

                sql_2 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";

                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")" 文字タイプだけ検索条件を追加
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL and SYD.COL_VISIBLE IS NULL order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                //  In_Con_Num 数字タイプ値だけを検索条件を追加
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";


                //  データ説明例を追加
                sql_3 += " left join (" + UNION_SQL;


                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")" 数字タイプだけ検索条件を追加
                sql_3 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_3 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME ,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL  order by SYD.SEQ_NO"
                    : sql_3 + " order by PMHD.FIELD_SEQ_NO ";

                //  検査を開始する
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();

                //  検査結果を結合
                result_2.AddRange(result_3);

                //  並び順の通りに並べる
                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                return result_2;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/ZKMS_NEW")]
        public List<EditInfo> GET_ZKMS_NEW(string USER_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                string TABLE_NAME = "ZKMS";
                USER_ID = DbContext.FixedSQLi(USER_ID);

                // CheckIndiviSet 個人並び順登録確認
                // True  : 登録
                // False : 未登録
                bool CheckIndiviSet = CheckIndivi(USER_ID, TABLE_NAME);

                //　MS_TABLE_SQL 他のデータベースにアクセスリスト取得のSQLコマンド
                string MS_TABLEL_SQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = '" + TABLE_NAME + "' and MS_TABLE != '0'";
                //　reusult_ms_table_ktstdtim 他のデータベースにアクセスリスト
                var reusult_ms_table = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLEL_SQL).ToList();
                string sql_2 = "";

                //　クライアント側に送るデータフォーマットを設定　
                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,null as FIELD_VALUE from( ";
                //　テーブル項目の表示状態を取得
                //　MAX(AUTH_TYPE) 表示状態が複数がある場合高い権限の方に優先する
                //　AUTH_TYPE   値  状態　　　　 優先順
                //              2　変更可能     　高
                //              1  表示　　　　　 
                //              0  未表示　　　　 低
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = '" + TABLE_NAME + "' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";

                //　テーブルの表示設定を取得
                //　PPPMTABLEHDRMNG　テーブルから表示の設定取得
                sql_2 += " full join PPPMTABLEHDRMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                sql_2 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";

                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")" 文字タイプだけ検索条件を追加
                sql_2 += " and AUTH_TYPE <> 0 ";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL and SYD.COL_VISIBLE IS NULL order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                //  検査を開始する
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();

                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/ZKMS_SOKOCODE")]
        public List<ZKMS_SOKOCODE> Get_ZKMS_SOKOCODE(string PART_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                string sql = "SELECT SOKO_CODE FROM ZKMS WHERE PART_NO = '" + PART_NO + "'";
                var result = DbContext.Database.SqlQuery<ZKMS_SOKOCODE>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMMAINTMS")]
        public List<PPPMMAINTMS> GET_PPPMMAINTMS(string PART_NO, string PART_REV_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PART_REV_NO = DbContext.FixedSQLi(PART_REV_NO);

                string sql = "select * from PPPMMAINTMS where  PART_NO = '" + PART_NO + "' and PART_REV_NO = '" + PART_REV_NO + "' order by  PRIORITY";
                var result = DbContext.Database.SqlQuery<PPPMMAINTMS>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMMAINTMS_MAINTMS")]
        public List<EditInfo> GET_MAINTMS(string PART_NO, string PART_REV_NO, string USER_ID, string COND_PAT_NO, string WHICH_MAINTMS, string PART_LOCATION)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PART_REV_NO = DbContext.FixedSQLi(PART_REV_NO);
                USER_ID = DbContext.FixedSQLi(USER_ID);
                COND_PAT_NO = DbContext.FixedSQLi(COND_PAT_NO);
                WHICH_MAINTMS = DbContext.FixedSQLi(WHICH_MAINTMS);
                PART_LOCATION = DbContext.FixedSQLi(PART_LOCATION);

                //  WHICH_MAINTMS 取得対象を確認
                //　1：１次情報
                //　2：２次情報
                string TABLE_NAME = "";
                string TABLE_NAME_TRAGET = "PPPMMAINTMS";
                if (WHICH_MAINTMS == "1")
                {
                    TABLE_NAME = "MAINTMS1";
                }
                else if (WHICH_MAINTMS == "2")
                {
                    TABLE_NAME = "MAINTMS2";
                }

                // CheckIndiviSet 個人並び順登録確認
                // True  : 登録
                // False : 未登録
                bool CheckIndiviSet = CheckIndivi(USER_ID, TABLE_NAME);

                //　MS_TABLE_SQL 他のデータベースにアクセスリスト取得のSQLコマンド
                string MS_TABLEL_SQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = '" + TABLE_NAME + "' and MS_TABLE != '0'";
                //　reusult_ms_table_ktstdtim 他のデータベースにアクセスリスト
                var reusult_ms_table = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLEL_SQL).ToList();


                //　テーブル内データが別タイプのため、文字タイプデータと数字タイプデータを別々検査必要があります
                //　column_name テーブルの列名リスト(文字タイプ)を取得
                string column_name = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME_TRAGET + "' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                //　column_name_num テーブルの列名リスト(数字タイプ)を取得
                string column_name_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = '" + TABLE_NAME_TRAGET + "' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                //　In_Con,In_Con_Num,In_Con_Num_where SQLコマンドにデータ居場所を特定するプロパティ
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<COLUMNNAME>(column_name).ToList();
                var result_num = DbContext.Database.SqlQuery<COLUMNNAME>(column_name_num).ToList();
                //　検索結果のリストを SQLコマンドに変更
                //　テーブルの列名リスト(文字タイプ)
                foreach (var str in result)
                {
                    //　下記で　SQLコマンドで使用
                    //　             　　　　　　 In_Con
                    //                             ↓
                    //　Where ○○　in ('○○','○○',.....,'○○')
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                //　テーブルの列名リスト(数字タイプ)
                foreach (var str in result_num)
                {
                    //　下記で　SQLコマンドで使用
                    //　              　In_Con_Num,In_Con_Num_where
                    //                             ↓
                    //　Where ○○　in ('○○','○○',.....,'○○')
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                    }
                }
                string sql_2 = "";
                string sql_3 = "";

                //　クライアント側に送るデータフォーマットを設定　
                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                //　テーブル項目の表示状態を取得
                //　MAX(AUTH_TYPE) 表示状態が複数がある場合高い権限の方に優先する
                //　AUTH_TYPE   値  状態　　　　 優先順
                //              2　変更可能     　高
                //              1  表示　　　　　 
                //              0  未表示　　　　 低
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = '" + TABLE_NAME + "' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";

                //　テーブルの表示設定を取得
                //　PPPMTABLEHDRMNG　テーブルから表示の設定取得
                sql_2 += " full join PPPMTABLEHDRMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                //　データ値を取得　例の値から行の値に変更必要ので UNPIVOTを使用
                //  取得したデータは下記の通りになります。
                // 　　　　　　　　変更前                                                          変更後
                //  FIELD_NAME_1   FIELD_NAME_2   ....  FIELD_NAME_X　　　　=> 　　　　 FIELD_NAME　　　FIELD_VALUE　
                //  FIELD_VALUE_1  FIELD_VALUE_2  ....  FIELD_VALUE_X                   FIELD_NAME_1　　FIELD_VALUE_1
                //                                                                      FIELD_NAME_2    FIELD_VALUE_2
                //                                                                                ........
                //                                                                      FIELD_NAME_X    FIELD_VALUE_X
                //
                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM " + TABLE_NAME_TRAGET;
                sql_2 += " where PART_NO =  '" + PART_NO + "' AND PART_REV_NO = '" + PART_REV_NO + "' AND COND_PAT_NO = '" + COND_PAT_NO + "' AND PART_LOCATION = '" + PART_LOCATION + "' ";
                sql_2 += ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";

                //基本情報を　sql_3に保存
                sql_3 = sql_2;

                //  IN_Con 文字タイプ値だけを検索条件を追加
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                //  データ説明例を追加
                sql_2 += " left join (";
                // UNION_SQL
                string UNION_SQL = "";
                foreach (var item in reusult_ms_table)
                {
                    if (UNION_SQL == "")
                    {
                        UNION_SQL += SqlTable.GetSQLUnion(item.MS_TABLE_Find, TABLE_NAME, "");
                    }
                    else
                    {
                        string Ckvoid = "";
                        Ckvoid += SqlTable.GetSQLUnion(item.MS_TABLE_Find, TABLE_NAME, "");
                        if (Ckvoid != "")
                        {
                            UNION_SQL += " UNION " + Ckvoid;
                        }
                    }
                }

                // 特定のデータに説明追加
                UNION_SQL += SqlTable.getSQLUnion(TABLE_NAME, "", "", "");
                sql_2 += UNION_SQL;

                sql_2 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";

                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")" 文字タイプだけ検索条件を追加
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL and SYD.COL_VISIBLE IS NULL order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                //  In_Con_Num 数字タイプ値だけを検索条件を追加
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";


                //  データ説明例を追加
                sql_3 += " left join (" + UNION_SQL;


                //  AUTH_TYPE <> 0　検索条件に未表示状態を取り除く
                //  PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")" 数字タイプだけ検索条件を追加
                sql_3 += " where PMTH.TABLE_NAME = '" + TABLE_NAME + "' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";

                //　もし、個人並び順が設定があれば個人並び順例を追加。なければ、通常の並び順を追加。
                sql_3 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME ,COL_VISIBLE from SYDBGRID where DBGRID_NAME = '" + TABLE_NAME + "' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  and SYD.COL_VISIBLE IS NULL  order by SYD.SEQ_NO"
                    : sql_3 + " order by PMHD.FIELD_SEQ_NO ";

                //  検査を開始する
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();

                //  検査結果を結合
                result_2.AddRange(result_3);

                //  並び順の通りに並べる
                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/SPSCCONIDMS_DROPDOWN")]
        public List<SPSCCONDIIDMS_DROPDOWN> Get_SPSCCONIDMS_DROPDOWN()
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "select CM_CODE,DATA1 ,CM_CODE_SETUMEI from cmmsb where CM_KOUNO = '805' AND DATA2 IS NOT NULL order by CM_CODE";
                var result = DbContext.Database.SqlQuery<SPSCCONDIIDMS_DROPDOWN>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/SPSCCONIDMS")]
        public List<SPSCCONDIDMS> GET_SPSCCONIDMS(string PART_NO, string PART_REV_NO, string PART_LOCATION, string COND_PAT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PART_REV_NO = DbContext.FixedSQLi(PART_REV_NO);
                PART_LOCATION = DbContext.FixedSQLi(PART_LOCATION);
                COND_PAT_NO = DbContext.FixedSQLi(COND_PAT_NO);

                string sql = "select sp.COND_SPEC_ITEM_NO,sp.COND_STAT,sp.COND_CODE,sp.PRODUCT_TYPE,sa.ITEM_NAME_LOC1,pm.PART_NO,pm.PART_REV_NO,pm.PART_LOCATION,pm.COND_PAT_NO,pm.COND_SEQ_NO,pm.CONDITION_ID from spsccondidms sp left join spscitemmsa sa on sp.COND_SPEC_ITEM_NO = sa.SPEC_ITEM_NO left join PPPMMAINTCONDMS pm on pm.CONDITION_ID = sp.CONDITION_ID" +
                             " where sp.CONDITION_ID in (select CONDITION_ID from PPPMMAINTCONDMS where PART_NO = '" + PART_NO + "' and PART_REV_NO = '" + PART_REV_NO +
                             "' AND PART_LOCATION = '" + PART_LOCATION + "' and COND_PAT_NO = '" + COND_PAT_NO + "')" +
                             "AND PART_NO = '" + PART_NO + "' and PART_REV_NO = '" + PART_REV_NO + "' AND PART_LOCATION = '" + PART_LOCATION + "' and COND_PAT_NO = '" + COND_PAT_NO + "' and sa.PRODUCT_TYPE ='1'";

                var result = DbContext.Database.SqlQuery<SPSCCONDIDMS>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/SPSCITEMMSA")]
        public List<SPSCITEMMSA> Get_SPSCITEMMSA(string PRODUCT_TYPE, string TODAY)
        {
            using (var Dbcontext = new TablesDbContext())
            {
                PRODUCT_TYPE = Dbcontext.FixedSQLi(PRODUCT_TYPE);
                TODAY = Dbcontext.FixedSQLi(TODAY);

                string sql = "SELECT * FROM SPSCITEMMSA WHERE DEL_TYPE = '0' AND START_DATE <= '" + TODAY + "' AND STOP_DATE >= '" + TODAY + "' AND PRODUCT_TYPE ='" + PRODUCT_TYPE + "'  and SPEC_ITEM_NO like '10%' order by SPEC_ITEM_NO";
                var result = Dbcontext.Database.SqlQuery<SPSCITEMMSA>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/SPSCITEMMSB")]
        public List<SPSCITEMMSB> Get_SPSCITEMMSB(string SPEC_ITEM_NO, string TODAY)
        {
            using (var Dbcontext = new TablesDbContext())
            {
                SPEC_ITEM_NO = Dbcontext.FixedSQLi(SPEC_ITEM_NO);
                TODAY = Dbcontext.FixedSQLi(TODAY);

                string sql = "SELECT * FROM SPSCITEMMSB WHERE START_DATE <= '" + TODAY + "' AND STOP_DATE >= '" + TODAY + "' AND SPEC_ITEM_NO ='" + SPEC_ITEM_NO + "' order by SPEC_ITEM_NO";
                var result = Dbcontext.Database.SqlQuery<SPSCITEMMSB>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/CURRVAL")]
        public List<CUR_TIME> Get_CURTIME()
        {
            using (var Dbcontext = new TablesDbContext())
            {
                string sql = "SELECT SPSEQ001.NEXTVAL CURRVAL from DUAL";
                var result = Dbcontext.Database.SqlQuery<CUR_TIME>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/CURRVAL_CPIASEQ")]
        public List<CUR_TIME> Get_CURTIME_CPIASEQ()
        {
            using (var Dbcontext = new TablesDbContext())
            {
                string sql = "SELECT CPIASEQ001.NEXTVAL CURRVAL from DUAL";
                var result = Dbcontext.Database.SqlQuery<CUR_TIME>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSUBMS_ARR")]
        public List<PPPMSUBMS_ARR> Get_PPPMSUBMS_ARR(string PART_NO)
        {
            using (var Dbcontext = new TablesDbContext())
            {
                PART_NO = Dbcontext.FixedSQLi(PART_NO);

                string sql = "select distinct arrange_no from pppmsubms where  PART_NO = '" + PART_NO + "'";
                var result = Dbcontext.Database.SqlQuery<PPPMSUBMS_ARR>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSUBMS")]
        public List<PPPMSUBMS> Get_PPPMSUBMS(string PART_NO, string ARRANGE_NO)
        {
            using (var Dbcontext = new TablesDbContext())
            {
                PART_NO = Dbcontext.FixedSQLi(PART_NO);
                ARRANGE_NO = Dbcontext.FixedSQLi(ARRANGE_NO);

                string sql = "select * from pppmsubms where PART_NO = '" + PART_NO + "' AND ARRANGE_NO = '" + ARRANGE_NO + "'";
                var result = Dbcontext.Database.SqlQuery<PPPMSUBMS>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSUBMS_Unit")]
        public List<PPPMSUBMS> Get_PPPMSUBMS_Unit(string PART_NO, string ARRANGE_NO, string SUBMS_REV_NO, string SUBMS_SEQ_NO)
        {
            using (var Dbcontext = new TablesDbContext())
            {
                PART_NO = Dbcontext.FixedSQLi(PART_NO);
                ARRANGE_NO = Dbcontext.FixedSQLi(ARRANGE_NO);
                SUBMS_REV_NO = Dbcontext.FixedSQLi(SUBMS_REV_NO);
                SUBMS_SEQ_NO = Dbcontext.FixedSQLi(SUBMS_SEQ_NO);

                string sql = "select * from pppmsubms where PART_NO = '" + PART_NO + "' AND ARRANGE_NO = '" + ARRANGE_NO + "AND SUBMS_REV_NO = '" + SUBMS_REV_NO + "AND SUBMS_SEQ_NO = '" + SUBMS_SEQ_NO + "'";
                var result = Dbcontext.Database.SqlQuery<PPPMSUBMS>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSUBMS_PartNo_Check")]
        public List<Count_ENT> Get_PPPMSUBMS_PartNo_Check(string PART_NO, string PART_REV_NO)
        {
            using (var Dbcontext = new TablesDbContext())
            {
                PART_NO = Dbcontext.FixedSQLi(PART_NO);
                PART_REV_NO = Dbcontext.FixedSQLi(PART_REV_NO);

                string sql = "select COUNT(*) COUNT from PPPMMS WHERE PART_NO = '" + PART_NO + "'";

                if (PART_REV_NO != "-")
                {
                    sql += " AND PART_REV_NO = '" + PART_REV_NO + "' ";
                }
                var result = Dbcontext.Database.SqlQuery<Count_ENT>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSUBMS_Permission")]
        public List<PPPMSUBMS>Get_PPPMSUBMS_Permission()
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "select * from pppmsubms where cur_type = '1' and app_cur_type = '0' order by PART_NO,BMS_TYPE";
                var result = DbContext.Database.SqlQuery<PPPMSUBMS>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSUBMS_Permission_Count_ENT")]
        public List<Count_ENT> GET_PPPMSUBMS_Permission_Count_ENT( string PART_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                string sql = "select COUNT(*) COUNT from pppmsubms where part_no = '" + PART_NO + "' and cur_type = '1' and app_cur_type = '0'";
                var result = DbContext.Database.SqlQuery<Count_ENT>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMPCCOSTMS")]
        public List<PPPMPCCOSTMS> GET_PPPMPCCOSTMS(string PART_NO,string PLANT_NO,string PART_REV_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                PART_REV_NO = DbContext.FixedSQLi(PART_REV_NO);

                string sql = "select * from PPPMPCCOSTMS where PART_NO ='" + PART_NO + "' and PLANT_NO ='" + PLANT_NO + "' and PART_REV_NO = '" + PART_REV_NO + "'";
                var result = DbContext.Database.SqlQuery<PPPMPCCOSTMS>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSPCOSTMS")]
        public List<PPPMSPCOSTMS_PPPMMS> GET_PPPMSPCOSTMS(string PART_NO,string PART_REV_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PART_REV_NO = DbContext.FixedSQLi(PART_REV_NO);

                string sql = "select * from PPPMSPCOSTMS PMS LEFT JOIN PPPMMS PS on PMS.PART_NO = PS.PART_NO and PMS.PART_REV_NO = PS.PART_REV_NO where PMS.PART_NO = '" + PART_NO + "' and PMS.PART_REV_NO = '" + PART_REV_NO + "'";
                var result = DbContext.Database.SqlQuery<PPPMSPCOSTMS_PPPMMS>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSPCOSTMS_Permission")]
        public List<PPPMSPCOSTMS> GET_PPPMSPCOSTMS_Permission(string PART_NO,string APP_TYPE,string PART_NAME)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                APP_TYPE = DbContext.FixedSQLi(APP_TYPE);
                PART_NAME = DbContext.FixedSQLi(PART_NAME);

                if (APP_TYPE == "00" || APP_TYPE == "11")
                {
                    APP_TYPE = "'00','11'";
                }
                string sql = "select * from PPPMSPCOSTMS PMS left join PPPMMS PS on PMS.PART_NO = PS.PART_NO and PMS.PART_REV_NO = PS.PART_REV_NO where ";
                if(PART_NO != null)
                {
                    sql += "PMS.PART_NO like '" + PART_NO + "%' and ";
                }
                if(PART_NAME != null)
                {
                    sql += "PS.PART_NAME_LOC1 like '" + PART_NAME + "%' and ";
                }
                sql += " APP_TYPE in (" + APP_TYPE + ")";

                var result = DbContext.Database.SqlQuery<PPPMSPCOSTMS>(sql).ToList();

                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMCLASSCOEMS")]
        public List<PPPMCLASSCOEMS> GET_PPPMCLASSCOEMSt()
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "select * from PPPMCLASSCOEMS order by PART_CLASS_ENT_NO desc ";
                var result = DbContext.Database.SqlQuery<PPPMCLASSCOEMS>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSPCOSTMS_Count_ENT")]
        public List<Count_ENT> GET_PPPMCLASSCOEMSt(string PART_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);

                string sql = "select COUNT(*) COUNT from PPPMSPCOSTMS where PART_NO = '" + PART_NO + "' and APP_TYPE <>'20'";
                var result = DbContext.Database.SqlQuery<Count_ENT>(sql).ToList();

                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMMS_REVNO_Count_ENT")]
        public List<Count_ENT> GET_PPPMS_REVNO_COUNT(string PART_NO ,string USER_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                USER_ID = DbContext.FixedSQLi(USER_ID);

                var RESULT = new List<Count_ENT>();
                string SQL_USER_ID = "SELECT ROLE_ID FROM CPUMGSSO_USER_ROLE_MST WHERE USER_ID = '"+USER_ID+ "' and ROLE_ID in ('0','2')";
                var RESULT_USER_ID = DbContext.Database.SqlQuery<CPUMGSSO_USER_ROLE_MST>(SQL_USER_ID).ToList();

                if(RESULT_USER_ID.Count > 0)
                {
                    string SQL = "SELECT COUNT(*) COUNT FROM PPPMMS where PART_NO = '" + PART_NO + "' and MODULE_TYPE='1'";
                    RESULT = DbContext.Database.SqlQuery<Count_ENT>(SQL).ToList();
                }
                else
                {
                    Count_ENT NULL_COUNT = new Count_ENT();
                    NULL_COUNT.COUNT = 0;

                    RESULT.Add(NULL_COUNT);
                }

                return RESULT;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMMS")]
        public List<PPPMMS> GET_PPPMMS(string PART_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);

                string sql = "SELECT * FROM PPPMMS WHERE PART_NO = '" + PART_NO + "'";
                var RESULT = DbContext.Database.SqlQuery<PPPMMS>(sql).ToList();

                return RESULT;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/JNV_JNSHAIN_01")]
        public List<JNV_JNSHAIN_01> GET_JNV_JNSHAIN_01(string USER_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                USER_ID = DbContext.FixedSQLi(USER_ID);

                string sql = "select * from JNV_JNSHAIN_01 where USER_ID = '"+ USER_ID + "'";
                var RESULT = DbContext.Database.SqlQuery<JNV_JNSHAIN_01>(sql).ToList();

                return RESULT;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet/ENT_CPIAPROCWHO")]
        public List<Count_ENT> GET_CPIAPROCWHO(string TABLE_NO,string SEQ_NO,string PROC_TYPE)
        {
            using (var DbContext = new TablesDbContext())
            {
                TABLE_NO = DbContext.FixedSQLi(TABLE_NO);
                SEQ_NO = DbContext.FixedSQLi(SEQ_NO);
                PROC_TYPE = DbContext.FixedSQLi(PROC_TYPE);

                string sql = "select COUNT(*) COUNT from CPIAPROCWHO where TABLE_NO = '"+ TABLE_NO + "' AND SEQ_NO = '"+ SEQ_NO + "' AND PROC_TYPE='"+PROC_TYPE+"'";
                var RESULT = DbContext.Database.SqlQuery<Count_ENT>(sql).ToList();

                return RESULT;
            }
        }
        // TEST
        [HttpGet]
        [Route("api/KensakuBtnGet/Vue_Chat")]
        public Vue_Chat Get_Vue_Chat()
        {
            var Message = new Vue_Chat {
                Re_Message = "<h1>Hello world</h1>"
            };

            return Message;
        }
        
        //GET api end HERE
        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMMS")]
        // POST api/<controller>
        public void Post_PPPMMS(POST_PPPMMS PM)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typePM = PM.GetType().GetProperties();
                foreach (var item in PM.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PM) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PM).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    if (item != "null" && list_name[index] != "PART_NO" && list_name[index] != "PART_REV_NO")
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }

                }

                string sql = "UPDATE PPPMMS " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(PM.PART_NO) + "' and PART_REV_NO = '" + DbContext.FixedSQLi(PM.PART_REV_NO) + "'";
                if (Set_sql != "")
                {
                    DbContext.Database.ExecuteSqlCommand(sql);
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMMS_INSERT")]
        public void PPPMMS_INSERT(PPPMMS PM)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMMS";
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = PM.GetType().GetProperties();
                foreach (var item in PM.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PM) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PM).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null")
                    {
                        if (Ins_sql_name == "")
                        {
                            Ins_sql_name += "(" + item;
                            Ins_sql_value += "('" + list_value[index] + "'";
                        }
                        else
                        {
                            Ins_sql_name += "," + item;
                            Ins_sql_value += ",'" + list_value[index] + "'";
                        }
                    }
                }
                Ins_sql_name += ") ";
                Ins_sql_value += ")";
                // INSERT　新規登録コマンド
                string SQL_INSERT = "INSERT INTO " + Table_Name + " " + Ins_sql_name + "VALUES " + Ins_sql_value;
                //  実行
                DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/NRPMA")]
        // POST api/<controller>
        public void Post_NRPMA(NRPMA NA)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typePM = NA.GetType().GetProperties();
                foreach (var item in NA.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(NA) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(NA).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    if (item != "null" && list_name[index] != "PART_NO" )
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }

                }

                string sql = "UPDATE NRPMA " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(NA.PART_NO) + "'";

                if (Set_sql != "")
                {
                    DbContext.Database.ExecuteSqlCommand(sql);
                }

            }
        }
        [HttpPost]
        [Route("api/KensakuBtnPost/NRPMHIS")]
        public void Post_NRPMHIS(NRPMHIS NS)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typePM = NS.GetType().GetProperties();
                foreach (var item in NS.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(NS) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(NS).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    if (item != "null" && list_name[index] != "PART_NO" && list_name[index] != "REV_PART_NO")
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }

                }

                string sql = "UPDATE NRPMHIS " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(NS.PART_NO) + "' and REV_PART_NO = '" + DbContext.FixedSQLi(NS.REV_PART_NO) + "'";

                if (Set_sql != "")
                {
                    DbContext.Database.ExecuteSqlCommand(sql);
                }
            }
        }
        [HttpPost]
        [Route("api/KensakuBtnPost/NRPMHIS_INSERT")]
        public void NRPMHIS_INSERT(NRPMHIS NS)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "NRPMHIS";
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = NS.GetType().GetProperties();
                foreach (var item in NS.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(NS) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(NS).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null")
                    {
                        if (Ins_sql_name == "")
                        {
                            Ins_sql_name += "(" + item;
                            Ins_sql_value += "('" + list_value[index] + "'";
                        }
                        else
                        {
                            Ins_sql_name += "," + item;
                            Ins_sql_value += ",'" + list_value[index] + "'";
                        }
                    }
                }
                Ins_sql_name += ") ";
                Ins_sql_value += ")";
                // INSERT　新規登録コマンド
                string SQL_INSERT = "INSERT INTO " + Table_Name + " " + Ins_sql_name + "VALUES " + Ins_sql_value;
                //  実行
                DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
            }
        }
        [HttpPost]
        [Route("api/KensakuBtnPost/NRPMB")]
        // POST api/<controller>
        public void Post_NRPMB(NRPMB NB)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                string Ins_value = "";
                string Ins_name = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typePM = NB.GetType().GetProperties();
                foreach (var item in NB.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(NB) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(NB).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    if (item != "null" && list_name[index] != "PART_NO" && list_name[index] != "PLANT_NO")
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                        Ins_name += "," + list_name[index];
                        Ins_value += ",'" + item + "'";
                    }

                }
                foreach (var item in NB.PLANT_NO)
                {
                    var item_1 = "";
                    item_1 = DbContext.FixedSQLi(item);
                    //  データ登録確認
                    string SQL_COUNT = "SELECT COUNT(*) COUNT FROM NRPMB where PART_NO = '" + NB.PART_NO + "' AND PLANT_NO = '" + item_1 + "'";
                    var result_COUNT = DbContext.Database.SqlQuery<Count_ENT>(SQL_COUNT).ToList();
                    bool Count_Check = result_COUNT[0].COUNT != 0;

                    if (Count_Check)
                    {
                        string sql = "UPDATE NRPMB " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(NB.PART_NO) + "' and PLANT_NO = '" + item_1 + "'";
                        if (Set_sql != "")
                        {
                            DbContext.Database.ExecuteSqlCommand(sql);
                        }
                    }
                    else
                    {
                        string sql = "INSERT INTO NRPMB (PART_NO,PLANT_NO" + Ins_name + ") VALUES ('"
                                   + DbContext.FixedSQLi(NB.PART_NO) + "','" + item_1 + "'" + Ins_value + ")";
                        if (Ins_name != "")
                        {
                            DbContext.Database.ExecuteSqlCommand(sql);
                        }
                    }
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMORDER")]
        // POST api/<controller>
        public void Post_PPPMORDER(POST_PPPMORDER PO)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                string Ins_value = "";
                string Ins_name = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typePM = PO.GetType().GetProperties();
                foreach (var item in PO.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PO) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PO).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    if (item != "null" && list_name[index] != "PART_NO" && list_name[index] != "PLANT_NO")
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                        Ins_name += "," + list_name[index];
                        Ins_value += ",'" + item + "'";
                    }
                }
                foreach (var item in PO.PLANT_NO)
                {
                    var item_1 = "";
                    item_1 = DbContext.FixedSQLi(item);
                    //  データ登録確認
                    string SQL_COUNT = "SELECT COUNT(*) COUNT FROM PPPMORDER where PART_NO = '" + PO.PART_NO + "' AND PLANT_NO = '" + item_1 + "'";
                    var result_COUNT = DbContext.Database.SqlQuery<Count_ENT>(SQL_COUNT).ToList();
                    bool Count_Check = result_COUNT[0].COUNT != 0;

                    if (Count_Check)
                    {
                        string sql = "UPDATE PPPMORDER " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(PO.PART_NO) + "' and PLANT_NO = '" + item_1 + "'";
                        if (Set_sql != "")
                        {
                            DbContext.Database.ExecuteSqlCommand(sql);
                        }
                    }
                    else
                    {
                        string sql = "INSERT INTO PPPMORDER (PART_NO,PLANT_NO" + Ins_name + ") VALUES ('"
                                   + DbContext.FixedSQLi(PO.PART_NO) + "','" + item_1 + "'" + Ins_value + ")";
                        if (Ins_name != "")
                        {
                            DbContext.Database.ExecuteSqlCommand(sql);
                        }
                    }
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/KTSTDTIME")]
        public void Post_KTSTDTIME(KTSTDTIME KT)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = KT.GetType().GetProperties();
                foreach (var item in KT.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(KT) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(KT).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    if (item != "null" && list_name[index] != "PART_NO" && list_name[index] != "PLANT_NO" && list_name[index] != "KT_CODE" && list_name[index] != "SEQ_NO")
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }

                }

                string sql = "UPDATE KTSTDTIME " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(KT.PART_NO) + "' and PLANT_NO = '" + DbContext.FixedSQLi(KT.PLANT_NO) + "' " +
                             " and KT_CODE = '" + DbContext.FixedSQLi(KT.KT_CODE) + "'  and SEQ_NO = '" + DbContext.FixedSQLi(KT.SEQ_NO) + "'";

                if (Set_sql != "")
                {
                    DbContext.Database.ExecuteSqlCommand(sql);
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/CHMSA")]
        public void Post_CHMSA(CHMSA CA)
        {
            using (var DbContext = new TablesDbContext())
            {
                //  PK_ID　SYDBGRIDテーブルのプライマリーキー名のリスト
                List<string> PK_ID = new List<string>() { "PART_NO", "SG_CODE", "VENDOR_CODE", "PRIORITY", "TUKA" };
                string Set_sql = "";
                string Where_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = CA.GetType().GetProperties();
                foreach (var item in CA.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(CA) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(CA).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    //  プライマリーキーじゃないものSETコマンドに変換
                    if (item != "null" && !PK_ID.Contains(list_name[index]))
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }
                    else if (item != "null" && PK_ID.Contains(list_name[index]))
                    {
                        if (Where_sql == "")
                        {
                            Where_sql += "WHERE " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Where_sql += " and " + list_name[index] + " = '" + item + "' ";
                        }
                    }

                }

                string sql = "UPDATE CHMSA " + Set_sql + Where_sql;

                if (Set_sql != "")
                {
                    DbContext.Database.ExecuteSqlCommand(sql);
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/KTSTDTIME_Excel")]
        public void Post_KTSTDTIME_Excel(KTSTDTIME KT)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = KT.GetType().GetProperties();
                //  list_nameに行名を保存する
                foreach (var item in KT.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(KT) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(KT).ToString());
                    }
                }
                int index = -1;
                //  list_valueに行名に対応する値を保存する
                foreach (var item in list_value)
                {
                    index += 1;
                    //　検査条件除って、更新する値をSet_sqlに保存する
                    //  Set_sql 更新したいデータをSQLコマンドに変更
                    if (item != "null" && list_name[index] != "PART_NO" && list_name[index] != "PLANT_NO" && list_name[index] != "KT_CODE" && list_name[index] != "SEQ_NO" &&
                        list_name[index] != "WC_CODE" && list_name[index] != "CC_CODE" && list_name[index] != "SG_CODE")
                    {
                        //  最終的は下記の通りになります。
                        //  Set_sql => SET List_Name_1 = "List_Value_1",List_Name_2 = "List_Value_2",......,List_Name_X = "List_Value_X"
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }

                }

                string sql = "UPDATE KTSTDTIME " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(KT.PART_NO) + "' and PLANT_NO = '" + DbContext.FixedSQLi(KT.PLANT_NO) + "' " +
                             " and KT_CODE = '" + DbContext.FixedSQLi(KT.KT_CODE) + "'  and SEQ_NO = '" + DbContext.FixedSQLi(KT.SEQ_NO) + "' " +
                             " and WC_CODE = '" + DbContext.FixedSQLi(KT.WC_CODE) + "' and CC_CODE = '" + DbContext.FixedSQLi(KT.CC_CODE) + "' and SG_CODE = '" + DbContext.FixedSQLi(KT.SG_CODE) + "' ";

                if (Set_sql != "")
                {
                    DbContext.Database.ExecuteSqlCommand(sql);
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/SYDBGRID_CKVALUE")]
        public void POST_SYDBGRID_CKVALUE(SYDBGRID SYD)
        {

            using (var DbContext = new TablesDbContext())
            {
                //　CheckIndivitSet_NoValue 個人並び順の設定を確認するプロパティ
                // 　False : ユーザーは個人並び順を未設定
                // 　True　: ユーザーは個人並び順を未設定している
                bool CheckIndivitSet_NoValue = false;

                //　IndivSetSQL_NoValue 個人並び順設定ユーザーリストを取得のSQLコマンド
                string IndivSetSQL_NoValue = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='" + SYD.DBGRID_NAME + "' AND FORM_NAME = 'ALL' order by USER_ID";
                //　resultIndivSetSQL_NoValue 個人並び順設定ユーザーリスト
                var resultIndivSetSQL_NoValue = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL_NoValue).ToList();
                //　現在使用したユーザーは個人並び順設定確認
                foreach (var user in resultIndivSetSQL_NoValue)
                {
                    //　設定した場合 CheckIndivitSet_NoValue => True に変更
                    if (SYD.USER_ID == user.USER_ID)
                    {
                        CheckIndivitSet_NoValue = true;
                    }
                }

                string Ins_NoValue = "";

                if (!CheckIndivitSet_NoValue)
                {
                    Ins_NoValue += "INSERT INTO SYDBGRID (USER_ID,PROJECT_ID,FORM_NAME,DBGRID_NAME,FIELD_NAME,FIELD_NAME_J,SEQ_NO, COL_VISIBLE, UPD_WHO, UPD_WHEN,ENT_DATE,USER_SEQ_NO) "
                                   + " VALUES ('" + DbContext.FixedSQLi(SYD.USER_ID) + "','PMRA0100','ALL','" + DbContext.FixedSQLi(SYD.DBGRID_NAME) + "','-', NULL ,";
                    //  SEQ_NO
                    switch (SYD.DBGRID_NAME)
                    {
                        case "PPPMMS":
                            Ins_NoValue += "'1',";
                            break;
                        case "PPPMORDER":
                            Ins_NoValue += "'2',";
                            break;
                        case "KTSTDTIME":
                            Ins_NoValue += "'3',";
                            break;
                        case "CHMSA":
                            Ins_NoValue += "'4',";
                            break;
                        case "ZKMS":
                            Ins_NoValue += "'5',";
                            break;
                    }
                    Ins_NoValue += "NULL ,'" + DbContext.FixedSQLi(SYD.USER_ID) + "','" + DbContext.FixedSQLi(SYD.UPD_WHEN) + "','" + DbContext.FixedSQLi(SYD.UPD_WHEN) + "','00')";
                }

                if (!CheckIndivitSet_NoValue)
                {
                    DbContext.Database.ExecuteSqlCommand(Ins_NoValue);
                }
            }

        }
        [HttpPost]
        [Route("api/KensakuBtnPost/SYDBGRID")]
        public void POST_SYDBGRID(SYDBGRID SYD)
        {
            using (var DbContext = new TablesDbContext())
            {
                //　CheckIndivSet 個人並び順の設定を確認するプロパティ
                // 　False : ユーザーは個人並び順を未設定
                // 　True　: ユーザーは個人並び順を未設定している
                bool CheckIndiviSet = false;


                //　IndivSetSQL 個人並び順設定ユーザーリストを取得のSQLコマンド
                string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='" + SYD.DBGRID_NAME + "'　and " + "FIELD_NAME ='" + SYD.FIELD_NAME + "' order by USER_ID";
                //　resultIndiviSet 個人並び順設定ユーザーリスト
                var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                //　現在使用したユーザーは個人並び順設定確認
                foreach (var user in resultIndiviSet)
                {
                    //　設定した場合 CheckIndivSet => True に変更
                    if (SYD.USER_ID == user.USER_ID)
                    {
                        CheckIndiviSet = true;
                    }
                }

                string Set_sql = "";
                string Ins_sql = "";
                //  PK_ID　SYDBGRIDテーブルのプライマリーキー名のリスト
                List<string> PK_ID = new List<string>() { "USER_ID", "DBGRID_NAME", "FIELD_NAME" };
                //  list_name 列名のプロパティリスト
                var list_name = new List<string>() { };
                //  list_value 更新する値のプロパティリスト
                var list_value = new List<string>() { };
                var typePM = SYD.GetType().GetProperties();
                //  SYD.GetType().GetProperties()　データの列名を取得
                foreach (var item in SYD.GetType().GetProperties())
                {
                    //  列名を取得
                    list_name.Add(item.Name);
                    //  もし、送ったデータの値がなければ'null'で保存
                    if (item.GetValue(SYD) == null)
                    {
                        list_value.Add("null");
                    }
                    //  あればそのまま値を保存
                    else
                    {
                        list_value.Add(item.GetValue(SYD).ToString());
                    }
                }
                int index = -1;
                string Ins_Table = "";
                string Ins_Value = "";

                //  更新するデータをSQLコマンドに加工する
                //  SET X1 = 'X1_value' , X2 = 'X2_value' ,....,Xz = 'Xz_value'
                foreach (var item in list_value)
                {
                    index += 1;
                    //  プライマリーキーと更新する値を分ける
                    if (item != "null" & !PK_ID.Contains(list_name[index]))
                    {
                        //  個人並び順登録確認
                        //  個人並び順登未登録な場合
                        if (!CheckIndiviSet)
                        {
                            //  送ってたでーたをSQL新規登録コマンドに変更
                            if (Ins_Table == "" && Ins_Value == "")
                            {
                                //  プライマリーキーは固定の部分があるので初回のループに固定コマンドを追加
                                //  INSERT INTO SYDBGRID (USER_ID　　　　　　　　,PROJECT_ID　　　　,FORM_NAME  ,DBGRID_NAME                ,FIELD_NAME               ,........) 
                                //                VALUE  (USER_ID（送ってデータ）,'PMRA0100'(固定)　,'--'(固定) ,DBGRID_NAME（送ってデータ）,FIELD_NAME（送ってデータ）,.......)
                                if (list_name[index] == "COL_VISIBLE" && item == "")
                                {

                                    Ins_Table += "(USER_ID,PROJECT_ID,FORM_NAME,DBGRID_NAME,FIELD_NAME," + list_name[index];
                                    Ins_Value += "('" + DbContext.FixedSQLi(SYD.USER_ID) + "','PMRA0100','--','" + DbContext.FixedSQLi(SYD.DBGRID_NAME) + "','" + DbContext.FixedSQLi(SYD.FIELD_NAME) + "', "
                                                 + "NULL";

                                }
                                else
                                {
                                    Ins_Table += "(USER_ID,PROJECT_ID,FORM_NAME,DBGRID_NAME,FIELD_NAME," + list_name[index];
                                    Ins_Value += "('" + DbContext.FixedSQLi(SYD.USER_ID) + "','PMRA0100','--','" + DbContext.FixedSQLi(SYD.DBGRID_NAME) + "','" + DbContext.FixedSQLi(SYD.FIELD_NAME) + "', "
                                                   + "'" + item + "'";
                                }
                            }
                            else
                            {
                                if (list_name[index] == "COL_VISIBLE" && item == "")
                                {

                                    Ins_Table += ", " + list_name[index];
                                    Ins_Value += ",NULL";

                                }
                                else
                                {
                                    Ins_Table += ", " + list_name[index];
                                    Ins_Value += ", '" + item + "'";
                                }
                            }
                        }
                        //  個人並び順登登録な場合
                        else
                        {
                            //  送ってたでーたをSQL更新コマンドに変更
                            if (Set_sql == "")
                            {
                                if (list_name[index] == "COL_VISIBLE" && item == "")
                                {

                                    Set_sql += "SET " + list_name[index] + " = NULL ";

                                }
                                else
                                {
                                    Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                                }

                            }
                            else
                            {
                                if (list_name[index] == "COL_VISIBLE" && item == "")
                                {

                                    Set_sql += ", " + list_name[index] + " = NULL ";

                                }
                                else
                                {
                                    Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                                }

                            }
                        }

                    }
                }
                //  新規コマンドを合体して、残りの値を追加
                //  追加した値　
                //  ENT_DATE (登録日)　、USER_SEQ_NO　'00'（固定字）
                Ins_sql = Ins_Table + ",ENT_DATE,USER_SEQ_NO) VALUES " + Ins_Value + "," + DbContext.FixedSQLi(SYD.UPD_WHEN) + ",'00')";

                string sql = CheckIndiviSet ? "UPDATE SYDBGRID " + Set_sql + " where USER_ID = '" + DbContext.FixedSQLi(SYD.USER_ID) + "' and DBGRID_NAME = '" + DbContext.FixedSQLi(SYD.DBGRID_NAME) +
                            "' and FIELD_NAME = '" + DbContext.FixedSQLi(SYD.FIELD_NAME) + "' " : "INSERT INTO SYDBGRID " + Ins_sql;


                if (Set_sql != "" || Ins_sql != "")
                {
                    DbContext.Database.ExecuteSqlCommand(sql);
                }

            }
        }
        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMPOSPEC")]
        public void POST_PPPMPOSPEC(PPPMPOSPEC PO)
        {
            using (var DbContext = new TablesDbContext())
            {
                //　更新したいデータは既に登録しているかを確認
                string SQL_Check_ENT = "SELECT COUNT(*) COUNT FROM PPPMPOSPEC WHERE PART_NO = '" + DbContext.FixedSQLi(PO.PART_NO)
                                     + "' AND WORK_CODE = '" + DbContext.FixedSQLi(PO.WORK_CODE) + "'";
                var result_Check_ENT = DbContext.Database.SqlQuery<Count_ENT>(SQL_Check_ENT).ToList();
                //  更新したいデータはNULLではないを確認
                bool UPD_NOT_NULL = (DbContext.FixedSQLi(PO.PO_SPEC1) != null) || (DbContext.FixedSQLi(PO.PO_SPEC2) != null) || (DbContext.FixedSQLi(PO.PO_SPEC3) != null) ? true : false;
                if (UPD_NOT_NULL)
                {
                    //  登録している場合データを更新する
                    if (result_Check_ENT[0].COUNT != 0)
                    {
                        //  PK_ID　SYDBGRIDテーブルのプライマリーキー名のリスト
                        List<string> PK_ID = new List<string>() { "PART_NO", "WORK_CODE" };
                        string Set_sql = "";
                        string Where_sql = "";
                        var list_name = new List<string>() { };
                        var list_value = new List<string>() { };
                        var typeKT = PO.GetType().GetProperties();
                        foreach (var item in PO.GetType().GetProperties())
                        {
                            list_name.Add(item.Name);
                            if (item.GetValue(PO) == null)
                            {
                                list_value.Add("null");
                            }
                            else
                            {
                                list_value.Add(item.GetValue(PO).ToString());
                            }
                        }
                        int index = -1;
                        foreach (var item in list_value)
                        {
                            index += 1;
                            //  プライマリーキーじゃないものSETコマンドに変換
                            if (item != "null" && !PK_ID.Contains(list_name[index]))
                            {
                                if (Set_sql == "")
                                {
                                    Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                                }
                                else
                                {
                                    Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                                }
                            }
                            else if (item != "null" && PK_ID.Contains(list_name[index]))
                            {
                                if (Where_sql == "")
                                {
                                    Where_sql += "WHERE " + list_name[index] + " = '" + item + "' ";
                                }
                                else
                                {
                                    Where_sql += " and " + list_name[index] + " = '" + item + "' ";
                                }
                            }
                        }

                        string sql = "UPDATE PPPMPOSPEC " + Set_sql + Where_sql;

                        if (Set_sql != "")
                        {
                            DbContext.Database.ExecuteSqlCommand(sql);
                        }
                    }
                    //  登録していない場合、新規登録する
                    else
                    {
                        // INSERT　新規登録コマンド
                        string SQL_INSERT = "INSERT INTO ZKMS (PART_NO,WORK_CODE,PO_SPEC1,PO_SPEC2,PO_SPEC3,UPD_WHO,UPD_WHEN,ENT_WHO,ENT_WHEN)"
                                          + "VALUES ('" + DbContext.FixedSQLi(PO.PART_NO) + "','" + DbContext.FixedSQLi(PO.WORK_CODE) + "','" //PK_CODE
                                          + DbContext.FixedSQLi(PO.PO_SPEC1) + "','" + DbContext.FixedSQLi(PO.PO_SPEC2) + "','" + DbContext.FixedSQLi(PO.PO_SPEC3) + "','"
                                          + DbContext.FixedSQLi(PO.UPD_WHO) + "','" + DbContext.FixedSQLi(PO.UPD_WHEN) + "','"
                                          + DbContext.FixedSQLi(PO.UPD_WHO) + "','" + DbContext.FixedSQLi(PO.UPD_WHEN) + "')";
                        //  実行
                        DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
                    }
                }

            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/ZKMS")]
        public void POST_ZKMS(ZKMS ZK)
        {
            using (var DbContext = new TablesDbContext()) {
                //　更新したいデータは既に登録しているかを確認
                string SQL_Check_ENT = "SELECT COUNT(*) COUNT FROM ZKMS WHERE SOKO_CODE = '" + DbContext.FixedSQLi(ZK.SOKO_CODE)
                                     + "' AND PART_NO = '" + DbContext.FixedSQLi(ZK.PART_NO) + "'";
                var result_Check_ENT = DbContext.Database.SqlQuery<Count_ENT>(SQL_Check_ENT).ToList();

                //  PK_ID　SYDBGRIDテーブルのプライマリーキー名のリスト
                List<string> PK_ID = new List<string>() { "SOKO_CODE", "PART_NO", "EZ_STOCK_FLAG", "MAX_VALUE_SET_TYPE", "MIN_SAFETY_SET_TYPE" };
                string Set_sql = "";
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                string Where_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = ZK.GetType().GetProperties();
                foreach (var item in ZK.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(ZK) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(ZK).ToString());
                    }
                }
                //  登録している場合データを更新する
                if (result_Check_ENT[0].COUNT != 0)
                {
                    int index = -1;
                    foreach (var item in list_value)
                    {
                        index += 1;
                        //  プライマリーキーじゃないものSETコマンドに変換
                        if (item != "null" && !PK_ID.Contains(list_name[index]))
                        {
                            if (Set_sql == "")
                            {
                                Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                            }
                            else
                            {
                                Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                            }
                        }
                        else if (item != "null" && PK_ID.Contains(list_name[index]))
                        {
                            if (Where_sql == "")
                            {
                                Where_sql += "WHERE " + list_name[index] + " = '" + item + "' ";
                            }
                            else
                            {
                                Where_sql += " and " + list_name[index] + " = '" + item + "' ";
                            }
                        }
                    }

                    string sql = "UPDATE ZKMS " + Set_sql + Where_sql;

                    if (Set_sql != "")
                    {
                        DbContext.Database.ExecuteSqlCommand(sql);
                    }
                }
                //  登録していない場合、新規登録する
                else
                {
                    int index = -1;
                    foreach (var item in list_name)
                    {
                        index += 1;
                        if (list_value[index] != "null")
                        {
                            if (Ins_sql_name == "")
                            {
                                Ins_sql_name += "(" + item;
                                Ins_sql_value += "('" + list_value[index] + "'";
                            }
                            else
                            {
                                Ins_sql_name += "," + item;
                                Ins_sql_value += ",'" + list_value[index] + "'";
                            }
                        }
                    }
                    Ins_sql_name += ") ";
                    Ins_sql_value += ")";
                    // INSERT　新規登録コマンド
                    string SQL_INSERT = "INSERT INTO ZKMS " + Ins_sql_name + "VALUES " + Ins_sql_value;
                    //  実行
                    DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
                }

            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/SPSCCONDIDMS_UPDATE")]
        public void POST_SPSCCONDIDM_UPDATE(SPSCCONDIDM_UPDATE SP_UP)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "UPDATE SPSCCONDIDMS SET COND_STAT = '" + SP_UP.COND_STAT + "' , COND_CODE = '" + SP_UP.COND_CODE + "'," +
                            "UPD_WHO = '" + SP_UP.UPD_WHO + "', UPD_WHEN = '" + SP_UP.UPD_WHEN + "' WHERE CONDITION_ID = '" + SP_UP.CONDITION_ID + "'";
                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/SPSCCONDIDMS_INSERT")]
        public void POST_SPSCCONDIDMS_INSERT(SPSCCONDIDMS_INSERT SP_IN)
        {
            using (var DbContext = new TablesDbContext())
            {
                List<string> PPPMM_List = new List<string>() { "PART_NO", "PART_REV_NO", "PART_LOCATION", "COND_PAT_NO", "COND_SEQ_NO", "COND_SORT_NO", "CONDITION_ID", "UPD_WHO", "UPD_WHEN", "ENT_WHO", "ENT_WHEN" };
                List<string> SPS_List = new List<string>() { "CONDITION_ID", "CONDITION_TYPE", "PLAN_LOC_TYPE", "PAT_NO_TYPE", "PRODUCT_TYPE", "CONDITION_ITEM_TYPE", "COND_SPEC_ITEM_NO", "COND_STAT", "COND_CODE", "START_DATE", "STOP_DATE", "UPD_WHO", "UPD_WHEN", "ENT_WHO", "ENT_WHEN" };

                string CONDITION_ID = "";
                //string GET_New_CONDITION_ID_SQL = "SELECT  SPSEQ001.NEXTVAL  FROM DUAL";
                string GET_New_CONDITION_ID_SQL = "select MAX(CONDITION_ID) +1 NEXTVAL from SPSCCONDIDMS where CONDITION_TYPE ='0' ";

                var result_New_CONDITION_ID = DbContext.Database.SqlQuery<NEW_CONDITION_ID>(GET_New_CONDITION_ID_SQL).ToList();
                CONDITION_ID = result_New_CONDITION_ID[0].NEXTVAL;

                string PPPMM_NAME = "";
                string PPPMM_VALUE = "";
                string SPS_NAME = "";
                string SPS_VALUE = "";
                //  送信されたデータを名前と値を分別
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeSP_IN = SP_IN.GetType().GetProperties();
                foreach (var item in SP_IN.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(SP_IN) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(SP_IN).ToString());
                    }
                }

                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (PPPMM_List.Contains(item))
                    {
                        if (PPPMM_NAME == "")
                        {
                            PPPMM_NAME += item;
                            PPPMM_VALUE += item != "CONDITION_ID" ? "'" + list_value[index] + "'" : "'" + CONDITION_ID + "'";
                        }
                        else
                        {
                            PPPMM_NAME += "," + item;
                            PPPMM_VALUE += item != "CONDITION_ID" ? ",'" + list_value[index] + "'" : ",'" + CONDITION_ID + "'";
                        }
                    }
                    if (SPS_List.Contains(item))
                    {
                        if (SPS_NAME == "")
                        {
                            SPS_NAME += item;
                            SPS_VALUE += item != "CONDITION_ID" ? "'" + list_value[index] + "'" : "'" + CONDITION_ID + "'";
                        }
                        else
                        {
                            SPS_NAME += "," + item;
                            SPS_VALUE += item != "CONDITION_ID" ? ",'" + list_value[index] + "'" : ",'" + CONDITION_ID + "'";
                        }
                    }
                }

                //  PPPMMAINTCONDMS に新しいデータを追加
                string sql_IN_PPPMM = "INSERT INTO PPPMMAINTCONDMS (" + PPPMM_NAME + ") VALUES (" + PPPMM_VALUE + ")";
                string sql_IN_SPS = "INSERT INTO SPSCCONDIDMS (" + SPS_NAME + ") VALUES (" + SPS_VALUE + ")";

                DbContext.Database.ExecuteSqlCommand(sql_IN_PPPMM);
                DbContext.Database.ExecuteSqlCommand(sql_IN_SPS);
            }
        }
        // PUT api/<controller>/5
        [HttpPost]
        [Route("api/KensakuBtnPost/POST_PPPMMAINTMS")]
        public void POST_PPPMMAINTMS(PPPMMAINTMS PPM_POST)
        {
            using (var DbContext = new TablesDbContext())
            {
                List<string> PK_list = new List<string>() { "PART_NO", "PART_REV_NO", "PART_LOCATION", "COND_PAT_NO" };
                List<string> PPPMMAINTMS_PARAMETA = new List<string> { "PRIORITY", "CUR_TYPE", "APP_CUR_TYPE", "MAINT_TYPE", "DSG_LIFE_MONTH", "DSG_LIFE_ACT", "DSG_UNIT", "ACT_TYPE_1", "ACT_TYPE_2", "REP_COND", "MAINT_METHOD_1", "MAINT_METHOD_2", "REP_LIFE_MONTH", "REP_LIFE_CHKMONTH", "REP_LIFE_RUN", "REP_LIFE_CHKRUN", "COND_TYPE", "UPD_WHO", "UPD_WHEN", "ENT_WHO", "ENT_WHEN" };

                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var PPPMM_SET = "";
                var PPPMM_WHERE = "";
                var typeSP_IN = PPM_POST.GetType().GetProperties();
                foreach (var item in PPM_POST.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PPM_POST) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PPM_POST).ToString());
                    }
                }

                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null" && PPPMMAINTMS_PARAMETA.Contains(item))
                    {
                        if (PPPMM_SET == "")
                        {
                            PPPMM_SET += item + " = '" + list_value[index] + "'";
                        }
                        else
                        {
                            PPPMM_SET += " , " + item + " = '" + list_value[index] + "'";
                        }
                    }
                    if (PK_list.Contains(item))
                    {
                        if (PPPMM_WHERE == "")
                        {
                            PPPMM_WHERE += item + " = '" + list_value[index] + "' ";
                        }
                        else
                        {
                            PPPMM_WHERE += " AND " + item + " = '" + list_value[index] + "' ";
                        }
                    }
                }

                //  PPPMMAINTMS に新しいデータを追加
                string sql = "UPDATE PPPMMAINTMS  SET  " + PPPMM_SET + " WHERE " + PPPMM_WHERE;

                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }
        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMMAINTCONDMS_DEL")]
        public void PPPMMAINTCONDMS_DEL(PPPMMAINTCONDMS_DEL PPM_DEL)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "DELETE FROM PPPMMAINTCONDMS where PART_NO = '" + PPM_DEL.PART_NO + "' AND PART_REV_NO = '" + PPM_DEL.PART_REV_NO + "' AND PART_LOCATION ='" + PPM_DEL.PART_LOCATION
                           + "' AND COND_PAT_NO = '" + PPM_DEL.COND_PAT_NO + "' AND COND_SEQ_NO >= '" + PPM_DEL.MAX_LENGHT + "'";
                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }
        
        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMMAINTCONDMS_UPDATE_NEW")]
        public void PPPMMAINTCONDMS_DEL(PPPMMAINTCONDMS_UPDATE PPM)
        {
            using (var DbContext = new TablesDbContext())
            {
                List<string> SPS_List = new List<string>() { "CONDITION_ID", "CONDITION_TYPE", "PLAN_LOC_TYPE", "PAT_NO_TYPE", "PRODUCT_TYPE", "CONDITION_ITEM_TYPE", "COND_SPEC_ITEM_NO", "COND_STAT", "COND_CODE", "START_DATE", "STOP_DATE", "UPD_WHO", "UPD_WHEN", "ENT_WHO", "ENT_WHEN" };

                string Find_COND_ID_SQL = "select CONDITION_ID from spsccondidms where PRODUCT_TYPE ='" + PPM.PRODUCT_TYPE + "' AND COND_SPEC_ITEM_NO = '" + PPM.COND_SPEC_ITEM_NO +
                                        "' and COND_STAT ='" + PPM.COND_STAT + "' and COND_CODE = '" + PPM.COND_CODE + "'";
                var COND_ID_LIST = DbContext.Database.SqlQuery<COND_ID>(Find_COND_ID_SQL).ToList();
                string COUNT_PPPMMA = "select COUNT(*) COUNT FROM PPPMMAINTCONDMS WHERE PART_NO = '" + PPM.PART_NO + "' and PART_REV_NO = '" + PPM.PART_REV_NO + "' and PART_LOCATION ='" + PPM.PART_LOCATION + "' and COND_PAT_NO = '" + PPM.COND_PAT_NO + "'";
                var COUNT_ENT = DbContext.Database.SqlQuery<Count_ENT>(COUNT_PPPMMA).ToList();

                //  CONDITION_IDが既に登録している場合ならそのまま　PPPMMAINTCONDM　新しいデータを登録する
                if (COND_ID_LIST.Count > 0)
                {
                    //  UPDATE
                    if (Int32.Parse(PPM.COND_SEQ_NO) + 1 <= COUNT_ENT[0].COUNT)
                    {
                        string UPDATE_PPPMMA = "UPDATE PPPMMAINTCONDMS SET CONDITION_ID = '" + COND_ID_LIST[0].CONDITION_ID + "', UPD_WHO = '" + PPM.UPD_WHO + "',UPD_WHEN = '" + PPM.UPD_WHEN + "' "
                                             + "WHERE PART_NO = '" + PPM.PART_NO + "' and PART_REV_NO = '" + PPM.PART_REV_NO + "' and PART_LOCATION ='" + PPM.PART_LOCATION + "' and COND_PAT_NO = '" + PPM.COND_PAT_NO + "' and COND_SEQ_NO = '" + PPM.COND_SEQ_NO + "'";
                        DbContext.Database.ExecuteSqlCommand(UPDATE_PPPMMA);
                    }
                    //  INSERT
                    else
                    {
                        string INSERT_PPPMMA = "INSERT INTO PPPMMAINTCONDMS (PART_NO,PART_REV_NO,PART_LOCATION,COND_PAT_NO,COND_SEQ_NO,COND_SORT_NO,CONDITION_ID,UPD_WHO,UPD_WHEN,ENT_WHO,ENT_WHEN) "
                             + " VALUES ('" + PPM.PART_NO + "','" + PPM.PART_REV_NO + "','" + PPM.PART_LOCATION + "','" + PPM.COND_PAT_NO + "','" + PPM.COND_SEQ_NO + "','1','" + COND_ID_LIST[0].CONDITION_ID + "','" + PPM.UPD_WHO
                             + "','" + PPM.UPD_WHEN + "','" + PPM.UPD_WHO + "','" + PPM.UPD_WHEN + "')";
                        DbContext.Database.ExecuteSqlCommand(INSERT_PPPMMA);
                    }

                }
                //  もし、CONDITION_ID が登録していなかった場合　spsccondidms　に新しいデータを登録する
                else
                {
                    string CONDITION_ID = "";
                    //TEST用
                    string GET_New_CONDITION_ID_SQL = "SELECT  SPSEQ001.NEXTVAL  FROM DUAL";
                    //string GET_New_CONDITION_ID_SQL = "select MAX(CONDITION_ID) +1 NEXTVAL from SPSCCONDIDMS where CONDITION_TYPE ='0' ";

                    var result_New_CONDITION_ID = DbContext.Database.SqlQuery<NEW_CONDITION_ID>(GET_New_CONDITION_ID_SQL).ToList();
                    CONDITION_ID = (Int32.Parse(result_New_CONDITION_ID[0].NEXTVAL) - PPM.CUR_TIME).ToString();

                    GET_New_CONDITION_ID_SQL = "select MAX(CONDITION_ID) + " + CONDITION_ID + " NEXTVAL from SPSCCONDIDMS where CONDITION_TYPE ='0' ";
                    result_New_CONDITION_ID = DbContext.Database.SqlQuery<NEW_CONDITION_ID>(GET_New_CONDITION_ID_SQL).ToList();
                    CONDITION_ID = result_New_CONDITION_ID[0].NEXTVAL;

                    string SPS_NAME = "";
                    string SPS_VALUE = "";
                    //  送信されたデータを名前と値を分別
                    var list_name = new List<string>() { };
                    var list_value = new List<string>() { };
                    var typeSP_IN = PPM.GetType().GetProperties();
                    foreach (var item in PPM.GetType().GetProperties())
                    {
                        list_name.Add(item.Name);
                        if (item.GetValue(PPM) == null)
                        {
                            list_value.Add("null");
                        }
                        else
                        {
                            list_value.Add(item.GetValue(PPM).ToString());
                        }
                    }

                    int index = -1;
                    foreach (var item in list_name)
                    {
                        index += 1;
                        if (SPS_List.Contains(item))
                        {
                            if (SPS_NAME == "")
                            {
                                SPS_NAME += item;
                                SPS_VALUE += item != "CONDITION_ID" ? "'" + list_value[index] + "'" : "'" + CONDITION_ID + "'";
                            }
                            else
                            {
                                SPS_NAME += "," + item;
                                SPS_VALUE += item != "CONDITION_ID" ? ",'" + list_value[index] + "'" : ",'" + CONDITION_ID + "'";
                            }
                        }
                    }
                    string sql_IN_SPS = "INSERT INTO SPSCCONDIDMS (" + SPS_NAME + ") VALUES (" + SPS_VALUE + ")";

                    DbContext.Database.ExecuteSqlCommand(sql_IN_SPS);

                    //  UPDATE
                    if (Int32.Parse(PPM.COND_SEQ_NO) + 1 <= COUNT_ENT[0].COUNT)
                    {
                        string UPDATE_PPPMMA = "UPDATE PPPMMAINTCONDMS SET CONDITION_ID = '" + CONDITION_ID + "', UPD_WHO = '" + PPM.UPD_WHO + "',UPD_WHEN = '" + PPM.UPD_WHEN + "' "
                                             + "WHERE PART_NO = '" + PPM.PART_NO + "' and PART_REV_NO = '" + PPM.PART_REV_NO + "' and PART_LOCATION ='" + PPM.PART_LOCATION + "' and COND_PAT_NO = '" + PPM.COND_PAT_NO + "' and COND_SEQ_NO = '" + PPM.COND_SEQ_NO + "'";
                        DbContext.Database.ExecuteSqlCommand(UPDATE_PPPMMA);
                    }
                    //  INSERT
                    else
                    {
                        string INSERT_PPPMMA = "INSERT INTO PPPMMAINTCONDMS (PART_NO,PART_REV_NO,PART_LOCATION,COND_PAT_NO,COND_SEQ_NO,COND_SORT_NO,CONDITION_ID,UPD_WHO,UPD_WHEN,ENT_WHO,ENT_WHEN) "
                             + " VALUES ('" + PPM.PART_NO + "','" + PPM.PART_REV_NO + "','" + PPM.PART_LOCATION + "','" + PPM.COND_PAT_NO + "','" + PPM.COND_SEQ_NO + "','1','" + CONDITION_ID + "','" + PPM.UPD_WHO
                             + "','" + PPM.UPD_WHEN + "','" + PPM.UPD_WHO + "','" + PPM.UPD_WHEN + "')";
                        DbContext.Database.ExecuteSqlCommand(INSERT_PPPMMA);
                    }
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMDOCMS_UPLOAD")]
        public void PPPMDOCMS_UPLOAD(PPPMDOCMS PPD)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMDOCMS";
                //  PK_ID　SYDBGRIDテーブルのプライマリーキー名のリスト
                List<string> PK_ID = new List<string>() { "PART_NO", "DOC_SEQ_NO", "DOC_FILE_NAME" };
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = PPD.GetType().GetProperties();
                foreach (var item in PPD.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PPD) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PPD).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null")
                    {
                        if (Ins_sql_name == "")
                        {
                            Ins_sql_name += "(" + item;
                            Ins_sql_value += "('" + list_value[index] + "'";
                        }
                        else
                        {
                            Ins_sql_name += "," + item;
                            Ins_sql_value += ",'" + list_value[index] + "'";
                        }
                    }
                }
                Ins_sql_name += ") ";
                Ins_sql_value += ")";
                // INSERT　新規登録コマンド
                string SQL_INSERT = "INSERT INTO " + Table_Name + " " + Ins_sql_name + "VALUES " + Ins_sql_value;
                //  実行
                DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMSUBMS_UPDATE")]
        public void PPPMSUBMS_UPDATE(PPPMSUBMS PS)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMSUBMS";

                List<string> PK_list = new List<string>() { "PART_NO", "ARRANGE_NO", "SUBMS_REV_NO", "SUBMS_SEQ_NO" };

                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var SET = "";
                var WHERE = "";
                var typeSP_IN = PS.GetType().GetProperties();
                foreach (var item in PS.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PS) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PS).ToString());
                    }
                }

                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null" && !PK_list.Contains(item))
                    {
                        if (SET == "")
                        {
                            SET += item + " = '" + list_value[index] + "'";
                        }
                        else
                        {
                            SET += " , " + item + " = '" + list_value[index] + "'";
                        }
                    }
                    if (PK_list.Contains(item))
                    {
                        if (WHERE == "")
                        {
                            WHERE += item + " = '" + list_value[index] + "' ";
                        }
                        else
                        {
                            WHERE += " AND " + item + " = '" + list_value[index] + "' ";
                        }
                    }
                }

                //  PPPMMAINTMS に新しいデータを追加
                string sql = "UPDATE " + Table_Name + "  SET  " + SET + " WHERE " + WHERE;

                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }



        [HttpGet]
        [Route("api/KensakuBtnGet/PPPMSUBMS_SEQ_NEW")]
        public List<NEW_SEQ_NO> Get_PPPMSUBMS_SEQ_NEW(string PART_NO, string ARRANGE_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                ARRANGE_NO = DbContext.FixedSQLi(ARRANGE_NO);

                string sql = "select MAX(SUBMS_SEQ_NO) + 1 SUBMS_SEQ_NO  from pppmsubms where PART_NO = '" + PART_NO + "' and ARRANGE_NO ='" + ARRANGE_NO + "' and SUBMS_REV_NO ='000'";
                var result = DbContext.Database.SqlQuery<NEW_SEQ_NO>(sql).ToList();

                return result;

            }
        } 

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMSUBMS_INSERT")]
        public void PPPMSUBMS_INSERT(PPPMSUBMS PS)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMSUBMS";
                //  PK_ID　SYDBGRIDテーブルのプライマリーキー名のリスト
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = PS.GetType().GetProperties();
                foreach (var item in PS.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PS) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PS).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null")
                    {
                        if (Ins_sql_name == "")
                        {
                            Ins_sql_name += "(" + item;
                            Ins_sql_value += "('" + list_value[index] + "'";
                        }
                        else
                        {
                            Ins_sql_name += "," + item;
                            Ins_sql_value += ",'" + list_value[index] + "'";
                        }
                    }
                }
                Ins_sql_name += ") ";
                Ins_sql_value += ")";
                // INSERT　新規登録コマンド
                string SQL_INSERT = "INSERT INTO " + Table_Name + " " + Ins_sql_name + "VALUES " + Ins_sql_value;
                //  実行
                DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMSUBMS_PERMISSION")]
        public void PPPMSUBMS_PERMISSION(PPPMSUBMS PS)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "UPDATE pppmsubms SET APP_CUR_TYPE = '1',APP_COMMENT ='"+ PS.APP_COMMENT +"' WHERE PART_NO = '" + PS.PART_NO + "' AND ARRANGE_NO = '" + PS.ARRANGE_NO + "' AND SUBMS_REV_NO = '" + PS.SUBMS_REV_NO + "' AND SUBMS_SEQ_NO = '" + PS.SUBMS_SEQ_NO + "'";
                string sql_old = "UPDATE pppmsubms SET APP_CUR_TYPE = '0',APP_COMMENT ='" + PS.APP_COMMENT + "' WHERE PART_NO = '" + PS.PART_NO + "' AND ARRANGE_NO = '" + PS.ARRANGE_NO + "' AND SUBMS_REV_NO = '" + PS.OLD_SUBMS_REV_NO + "' AND SUBMS_SEQ_NO = '" + PS.SUBMS_SEQ_NO + "'";

                DbContext.Database.ExecuteSqlCommand(sql);
                if(PS.OLD_SUBMS_REV_NO != "NULL")
                {
                    DbContext.Database.ExecuteSqlCommand(sql_old);
                }
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMPCCOSTMS_INSERT")]
        public void PPPMPCCOSTMS_INSERT(PPPMPCCOSTMS PC)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMPCCOSTMS";
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = PC.GetType().GetProperties();
                foreach (var item in PC.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PC) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PC).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null")
                    {
                        if (Ins_sql_name == "")
                        {
                            Ins_sql_name += "(" + item;
                            Ins_sql_value += "('" + list_value[index] + "'";
                        }
                        else
                        {
                            Ins_sql_name += "," + item;
                            Ins_sql_value += ",'" + list_value[index] + "'";
                        }
                    }
                }
                Ins_sql_name += ") ";
                Ins_sql_value += ")";
                // INSERT　新規登録コマンド
                string SQL_INSERT = "INSERT INTO " + Table_Name + " " + Ins_sql_name + "VALUES " + Ins_sql_value;
                //  実行
                DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMPCCOSTMS_UPDATE")]
        public void PPPMPCCOSTMS_UPDATE(PPPMPCCOSTMS PC)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMPCCOSTMS";

                List<string> PK_list = new List<string>() { "PLANT_NO", "PART_NO", "PART_REV_NO", "COST_REV_NO" };

                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var SET = "";
                var WHERE = "";
                foreach (var item in PC.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PC) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PC).ToString());
                    }
                }

                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null" && !PK_list.Contains(item))
                    {
                        if (SET == "")
                        {
                            SET += item + " = '" + list_value[index] + "'";
                        }
                        else
                        {
                            SET += " , " + item + " = '" + list_value[index] + "'";
                        }
                    }
                    if (PK_list.Contains(item))
                    {
                        if (WHERE == "")
                        {
                            WHERE += item + " = '" + list_value[index] + "' ";
                        }
                        else
                        {
                            WHERE += " AND " + item + " = '" + list_value[index] + "' ";
                        }
                    }
                }

                //  PPPMMAINTMS に新しいデータを追加
                string sql = "UPDATE " + Table_Name + "  SET  " + SET + " WHERE " + WHERE;

                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMSPCOSTMS_INSERT")]
        public void PPPMSPCOSTMS_INSERT(PPPMSPCOSTMS SP)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMSPCOSTMS";
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = SP.GetType().GetProperties();
                foreach (var item in SP.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(SP) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(SP).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null")
                    {
                        if (Ins_sql_name == "")
                        {
                            Ins_sql_name += "(" + item;
                            Ins_sql_value += "('" + list_value[index] + "'";
                        }
                        else
                        {
                            Ins_sql_name += "," + item;
                            Ins_sql_value += ",'" + list_value[index] + "'";
                        }
                    }
                }
                Ins_sql_name += ") ";
                Ins_sql_value += ")";
                // INSERT　新規登録コマンド
                string SQL_INSERT = "INSERT INTO " + Table_Name + " " + Ins_sql_name + "VALUES " + Ins_sql_value;
                //  実行
                DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMSPCOSTMS_UPDATE")]
        public void PPPMSPCOSTMS_UPDATE(PPPMSPCOSTMS SP)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "PPPMSPCOSTMS";

                List<string> PK_list = new List<string>() { "PART_NO", "PART_REV_NO", "COST_REV_NO" };

                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var SET = "";
                var WHERE = "";
                foreach (var item in SP.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(SP) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(SP).ToString());
                    }
                }

                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null" && !PK_list.Contains(item))
                    {
                        if (SET == "")
                        {
                            SET += item + " = '" + list_value[index] + "'";
                        }
                        else
                        {
                            SET += " , " + item + " = '" + list_value[index] + "'";
                        }
                    }
                    if (PK_list.Contains(item))
                    {
                        if (WHERE == "")
                        {
                            WHERE += item + " = '" + list_value[index] + "' ";
                        }
                        else
                        {
                            WHERE += " AND " + item + " = '" + list_value[index] + "' ";
                        }
                    }
                }

                //  PPPMMAINTMS に新しいデータを追加
                string sql = "UPDATE " + Table_Name + "  SET  " + SET + " WHERE " + WHERE;

                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }
        [HttpPost]
        [Route("api/KensakuBtnPost/CPIAPROCWHO_INSERT")]
        public void CPIAPROCWHO_INSERT(CPIAPROCWHO CPI)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "CPIAPROCWHO";
                string Ins_sql_name = "";
                string Ins_sql_value = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typeKT = CPI.GetType().GetProperties();
                foreach (var item in CPI.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(CPI) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(CPI).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null")
                    {
                        if (Ins_sql_name == "")
                        {
                            Ins_sql_name += "(" + item;
                            Ins_sql_value += "('" + list_value[index] + "'";
                        }
                        else
                        {
                            Ins_sql_name += "," + item;
                            Ins_sql_value += ",'" + list_value[index] + "'";
                        }
                    }
                }
                Ins_sql_name += ") ";
                Ins_sql_value += ")";
                // INSERT　新規登録コマンド
                string SQL_INSERT = "INSERT INTO " + Table_Name + " " + Ins_sql_name + "VALUES " + Ins_sql_value;
                //  実行
                DbContext.Database.ExecuteSqlCommand(SQL_INSERT);
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/CPIAPROCWHO_UPDATE")]
        public void CPIAPROCWHO_UPDATE(CPIAPROCWHO CPI)
        {
            using (var DbContext = new TablesDbContext())
            {
                var Table_Name = "CPIAPROCWHO";

                List<string> PK_list = new List<string>() { "TABLE_NO", "SEQ_NO", "PROC_TYPE" };

                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var SET = "";
                var WHERE = "";
                foreach (var item in CPI.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(CPI) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(CPI).ToString());
                    }
                }

                int index = -1;
                foreach (var item in list_name)
                {
                    index += 1;
                    if (list_value[index] != "null" && !PK_list.Contains(item))
                    {
                        if (SET == "")
                        {
                            SET += item + " = '" + list_value[index] + "'";
                        }
                        else
                        {
                            SET += " , " + item + " = '" + list_value[index] + "'";
                        }
                    }
                    if (PK_list.Contains(item))
                    {
                        if (WHERE == "")
                        {
                            WHERE += item + " = '" + list_value[index] + "' ";
                        }
                        else
                        {
                            WHERE += " AND " + item + " = '" + list_value[index] + "' ";
                        }
                    }
                }

                //  PPPMMAINTMS に新しいデータを追加
                string sql = "UPDATE " + Table_Name + "  SET  " + SET + " WHERE " + WHERE;

                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        // SQLインジェクション対策
        public static bool CheckIndivi(string USER_ID, string TABLE_NAME)
        {
            using (var DbContext = new TablesDbContext())
            {
                //　CheckIndivSet 個人並び順の設定を確認するプロパティ
                // 　False : ユーザーは個人並び順を未設定
                // 　True　: ユーザーは個人並び順を未設定している
                bool CheckIndiviSet = false;

                //　IndivSetSQL 個人並び順設定ユーザーリストを取得のSQLコマンド
                string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='" + TABLE_NAME + "' order by USER_ID";
                //　resultIndiviSet 個人並び順設定ユーザーリスト
                var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                //　現在使用したユーザーは個人並び順設定確認
                foreach (var user in resultIndiviSet)
                {
                    //　設定した場合 CheckIndivSet => True に変更
                    if (USER_ID == user.USER_ID)
                    {
                        CheckIndiviSet = true;
                    }
                }

                return CheckIndiviSet;
            }
        }
    }
}