
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using hn.DataAccess.Model;
using hn.APIService.DataAccess.Model;
using hn.APIService.DataAccess.Dal;
using hn.Common;

namespace hn.APIService
{
    public class APIService : IAPIService
    {

        #region 蒙厂添加的方法

      

        public bool InsertICPOEntry(string oneModelJson)
        {
            int iCount = ICPOBILLENTRYDal.Instance.InsertEx(JSONhelper.ConvertToObject<ICPOBILLENTRYModel_MHLS>(oneModelJson));
            if (iCount > 0) return true;
            else return false;
        }


        /// <summary>
        /// 接口3:客户端获取后判断是否改变，客户端再自己更新自己的状态
        /// </summary>
        /// <param name="strFID"></param>
        /// <returns></returns>
        public ICPOBILLENTRYModel_MHLS GetICPOEntry(string strFID, string strEntryID)
        {
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" and FID='{0}' and FENTRYID='{1}'", strFID, strEntryID);
            List<ICPOBILLENTRYModel_MHLS> tList = ICPOBILLENTRYDal.Instance.GetWhere(query).ToList();
            if (tList.Count > 0)
            {
                return tList[0];
            }
            else
            {
                return null;
            }

        }

    /// <summary>
    /// 接口4:获取提货单
    /// </summary>
    /// <param name="fbillno"></param>
    /// <param name="entryid"></param>
    /// <returns></returns>
    public List<ICPO_BOLentryModel_MNLS> GetICPO_BOEntry(string fbillno, string entryid)
        {
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" and FPObillno='{0}' and Fpobillentry='{1}'", fbillno, entryid);
            List<ICPO_BOLentryModel_MNLS> result = ICPO_BOLentryModel_MNLSDal.Instance.GetWhere(query).ToList();
          
            return result;
        }
        /// <summary>
        /// 接口4:更新提货单
        /// </summary>
        /// <param name="fbillno"></param>
        /// <param name="entryid"></param>
        /// <returns></returns>
        public int SetICPO_BOEntryStatus(string fbillno, string entryid)
        {
            int iResult = 0;          
            iResult = ICPO_BOLentryModel_MNLSDal.Instance.UpdateWhatWhere(new { FSYNCSTATUS = 1 }, new { FPObillno=fbillno, Fpobillentry=entryid });

            return iResult;
        }


        #endregion

        /// <summary>
        /// 发货计划-输出接口
        /// </summary>
        /// <param name="icseout">发货计划主记录</param>
        /// <param name="icseoutentry">发货计划明细记录</param>
        /// <returns></returns>
        public string ICSEOUTBILLSync(List<V_ICSEOUTBILLMODEL> icseout, List<V_ICSEOUTBILLENTRYMODEL> icseoutentry)
        {
            try
            {
                if (icseout.Count > 0)
                {
                    int result = 0;
                    foreach (V_ICSEOUTBILLENTRYMODEL entrymodel in icseoutentry)
                    {
                        //普通开单
                        if (icseout[0].FBILLING_TYPE == 1)
                        {

                            //PLANBill_HuaNaiDal.Instance.DeleteWhere(new { FBILLNO = icseout[0].FBILLNO, FSYNCSTATUS = -3 });

                            PLANBill_HuaNaiDal.Instance.DeleteWhere(new { FBILLNO = icseout[0].FBILLNO, FENTRYID = entrymodel.FENTRYID});

                            PLANBill_HuaNaiModel newmodel = new PLANBill_HuaNaiModel();
                            newmodel.FBILLNO = icseout[0].FBILLNO;
                            newmodel.FSTATUS = 1;
                            newmodel.FSYNCSTATUS = 0;
                            newmodel.FACCOUNT = icseout[0].FACCOUNT;
                            //newmodel.FPLATENUMBER =string.Format(@"{0}\{1}\{2}\{3}", icseout[0].FCARNUMBER, icseout[0].FEXPRESSCOMPANYNAME, icseout[0].FDELIVERERADDR, icseout[0].FPROJECTNAME) ;
                            newmodel.FPLATENUMBER = icseout[0].FCARNUMBER;
                            newmodel.FDRIVER = icseout[0].FDELIVERER;
                            newmodel.FDRPHONE = icseout[0].FDELIVERERTEL;
                            newmodel.FCENTER_WAREHOUSE = icseout[0].FCENTER_WAREHOUSE;
                            newmodel.FIS_CONSIGN = PublicMethod.GetInt(icseout[0].FIS_CONSIGN);
                            newmodel.FDELIVERY_METHOD = icseout[0].FDELIVERY_METHODNUMBER;
                            newmodel.FENTRYID = PublicMethod.GetInt(entrymodel.FENTRYID);
                            newmodel.FITEMID = entrymodel.FITEMID;
                            newmodel.FSRCCODE = entrymodel.FSRCCODE;
                            newmodel.FSRCNAME = entrymodel.FSRCNAME;
                            newmodel.FSRCMODEL = entrymodel.FSRCMODEL;
                            newmodel.FBATCHNO = entrymodel.FBATCHNO;
                            newmodel.FCOLORNO = entrymodel.FCOLORNO;
                            newmodel.FPRICE = entrymodel.FACCOUNTPRICE;
                            newmodel.FAMOUNT = entrymodel.FAMOUNT;
                            newmodel.FERR_MESSAGE = entrymodel.FERR_MESSAGE;
                            newmodel.FAUDQTY = entrymodel.FCOMMITQTY;
                            //newmodel.FCOMMITQTY = entrymodel.FCOMMITQTY;
                            newmodel.FREMARK = icseout[0].FREMARK;
                            newmodel.FNEEDDATE = icseout[0].FDELIVERDATE;
                            newmodel.FGRADE = entrymodel.FLEVEL;
                            newmodel.FLINE_NOTE = entrymodel.FDESCRIPTION;
                            newmodel.FPLANDESC = icseout[0].FPLANDESC;
                            newmodel.FPURCHASE_NO = icseout[0].FPURCHASE_NO;
                            newmodel.FIS_CONSIGN = icseout[0].FIS_CONSIGN;

                            result = PLANBill_HuaNaiDal.Instance.Insert(newmodel);
                        }

                        //托管库开单
                        if (icseout[0].FBILLING_TYPE == 2)
                        {
                            Customer_TG_Order_HuaNaiDal.Instance.DeleteWhere(new { FBILLNO = icseout[0].FBILLNO, FSYNCSTATUS = -3 });

                            Customer_TG_Order_HuaNaiModel newmodel = new Customer_TG_Order_HuaNaiModel();
                            newmodel.FBILLNO = icseout[0].FBILLNO;
                            newmodel.FNEEDDATE = icseout[0].FDELIVERDATE;
                            newmodel.CUSTOMER_NO = icseout[0].FACCOUNT;
                            newmodel.CUST_REF = icseout[0].FACCOUNT;
                            newmodel.CAR_NO = icseout[0].FCARNUMBER;
                            newmodel.NOTE = icseout[0].FPLANDESC;
                            newmodel.PART_NO = entrymodel.FSRCCODE;
                            newmodel.LOCATION_NO = entrymodel.FSTOCKNUMBER;
                            newmodel.WareHouse = entrymodel.FSTOCK;
                            newmodel.LOT_BATCH_NO = entrymodel.FBATCHNO;
                            newmodel.LOCATION_NO = entrymodel.FLOCATION_NO;
                            newmodel.QUANTITY = entrymodel.FCOMMITQTY;
                            newmodel.lineNote = entrymodel.FDESCRIPTION;
                            newmodel.WAIV_DEV_REJ_NO = entrymodel.FWDR;
                            newmodel.FSYNCSTATUS = 0;
                            newmodel.FENTRYID = PublicMethod.GetInt(entrymodel.FENTRYID);

                            result = Customer_TG_Order_HuaNaiDal.Instance.Insert(newmodel);
                        }
                    }

                    if (result > 0)
                    {

                        trans_finishModel model = new trans_finishModel();
                        model.fbillno = icseout[0].FBILLNO;
                        model.if_TGbill = (icseout[0].FBILLING_TYPE == 2);
                        trans_finishDal.Instance.Insert(model);

                        return JsonHelper.ToJson(new { errCode = 0, errMsg = "上传成功！" });
                    }
                }

                return JsonHelper.ToJson(new { errCode = -1, errMsg = "上传失败！" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public string InventorySync(List<SALES_ORDER_DATAMODEL> salesOrderDatas, List<SALESOUT_DATAMODEL> salesoutDatas, List<TP_INVENTORYMODEL> inventorys)
        {
            try
            {
                foreach (var model in salesOrderDatas)
                {
                    sales_order_dataModel newmodel = new sales_order_dataModel();
                    newmodel.fid = PublicMethod.GetInt(model.FID);
                    newmodel.Part_name = PublicMethod.GetString(model.PART_NAME);
                    newmodel.PRIME_COMMODITY_NAME = PublicMethod.GetString(model.PRIME_COMMODITY_NAME);
                    newmodel.fqty = PublicMethod.GetDecimal(model.FQTY);
                    newmodel.ftime = PublicMethod.GetDateTime(model.FTIME);
                    newmodel.funit =PublicMethod.GetString(model.FUNIT);
                    //LogHelper.WriteLog(JsonHelper.ToJson(newmodel));
                    sales_order_dataDal.Instance.InsertEx(newmodel);
                }

                foreach (var model in salesoutDatas)
                {
                    salesout_dataModel newmodel = new salesout_dataModel();
                    newmodel.fid = PublicMethod.GetInt(model.FID);
                    newmodel.fdate = PublicMethod.GetDateTime(model.FDATE);
                    newmodel.Part_name = model.PART_NAME;
                    newmodel.PRIME_COMMODITY_NAME = model.PRIME_COMMODITY_NAME;
                    newmodel.fqty = PublicMethod.GetDecimal(model.FQTY);
                    newmodel.Shipping_date = PublicMethod.GetDateTime(model.SHIPPING_DATE);
                    newmodel.funit = model.FUNIT;
                    newmodel.COLOR_NO = model.COLOR_NO;
                   // LogHelper.WriteLog(JsonHelper.ToJson(newmodel));
                    salesout_dataDal.Instance.InsertEx(newmodel);
                }

                foreach (var model in inventorys)
                {
                    TP_inventoryModel newmodel = new TP_inventoryModel();
                    newmodel.fid = PublicMethod.GetInt(model.FID);
                    newmodel.Part_name = model.PART_NAME;
                    newmodel.PRIME_COMMODITY_NAME = model.PRIME_COMMODITY_NAME;
                    newmodel.fqty = PublicMethod.GetDecimal(model.FQTY);
                    newmodel.ftime = PublicMethod.GetDateTime(model.FTIME);
                    newmodel.funit = model.FUNIT;
                    //LogHelper.WriteLog(JsonHelper.ToJson(newmodel));
                    TP_inventoryDal.Instance.InsertEx(newmodel);
                }

                LogHelper.WriteLog("123123");

                return JsonHelper.ToJson(new { errCode = 0, errMsg = "上传成功！" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 厂家库存接口
        /// </summary>
        /// <returns></returns>
        public DataTable WmStock(
            string productnumber,
            string productname,
            string stockname,
            string brandname,
            string batchno,
            string wdr,
            string mode,
            string colorno,
            out int pagecount,
            int pagesize = 20,
            int page = 1,
            bool isfactory = true,
            bool istrust = false)
        {
            try
            {
                //ProcCustomPage pagesp = new ProcCustomPage();
                //pagesp.PageIndex = page;
                //pagesp.PageSize = pagesize;
                //pagesp.TableName = "WM_stock";

                //pagesp.WhereString = " 1=1 ";
                //if (!string.IsNullOrEmpty(productnumber))
                //{
                //    pagesp.WhereString += string.Format("   And FNumber in ('{0}')", productnumber.Replace(",", "','"));
                //}
                //if (!string.IsNullOrEmpty(productname))
                //{
                //    pagesp.WhereString += string.Format("   And FName LIKE '%{0}%'", productname);
                //}
                //if (!string.IsNullOrEmpty(stockname))
                //{
                //    pagesp.WhereString += string.Format("   And FStockName LIKE '%{0}%'", stockname);
                //}
                //if (!string.IsNullOrEmpty(batchno))
                //{
                //    pagesp.WhereString += string.Format("   And FBatchNo LIKE '%{0}%'", batchno);
                //}
                //if (!string.IsNullOrEmpty(wdr))
                //{
                //    pagesp.WhereString += string.Format("   And FWDRID LIKE '%{0}%'", wdr);
                //}

                //pagesp.KeyFields = "";
                //pagesp.OrderFields = " FStockName ";

                //pagecount = 0;

                //return DbUtils.Query("select * from WM_stock where " + pagesp.WhereString);
                pagecount = 0;

                StringBuilder sql = new StringBuilder();
                DataTable table1 = new DataTable();
                table1.Columns.Add(new DataColumn("FIDentityID"));
                table1.Columns.Add(new DataColumn("FNumber"));
                table1.Columns.Add(new DataColumn("FName"));
                table1.Columns.Add(new DataColumn("FModel"));
                table1.Columns.Add(new DataColumn("FBatchNo"));
                table1.Columns.Add(new DataColumn("FColorNo"));
                table1.Columns.Add(new DataColumn("FUnit"));
                table1.Columns.Add(new DataColumn("FQty"));
                table1.Columns.Add(new DataColumn("FBasicUnit"));
                table1.Columns.Add(new DataColumn("FBasicQty"));
                table1.Columns.Add(new DataColumn("FStockNumber"));
                table1.Columns.Add(new DataColumn("FStockName"));
                table1.Columns.Add(new DataColumn("FSPNumber"));
                table1.Columns.Add(new DataColumn("FSPName"));
                table1.Columns.Add(new DataColumn("FGrade"));
                table1.Columns.Add(new DataColumn("FWDRID"));
                table1.Columns.Add(new DataColumn("FTrusteeshipFlag"));
                table1.TableName = "Table1";
                if (isfactory)
                {
                    sql.AppendLine("declare @config_msg varchar(100)");
                    sql.AppendFormat("exec _WM_Stock_Get @config_msg output ,'{0}','{1}','{2}','{3}'", brandname, productname, mode, colorno);
                    sql.AppendLine("print @config_msg");

                    LogHelper.WriteLog(sql.ToString());

                    table1 = DbUtils.Query(sql.ToString());
                    table1.Columns.Add(new DataColumn("FTrusteeshipFlag"));
                }

                if (istrust)
                {
                    sql = new StringBuilder();
                    sql.AppendLine("declare @config_msg varchar(100)");
                    sql.AppendFormat("exec _WM_Stock_tg_Get @config_msg output ,'{0}','{1}'", brandname, productname);
                    sql.AppendLine("print @config_msg");

                    LogHelper.WriteLog(sql.ToString());

                    DataTable table2 = DbUtils.Query(sql.ToString());
                    foreach (DataRow row in table2.Rows)
                    {
                        DataRow newRow = table1.NewRow();
                        newRow["FNumber"] = row["Part_No"];
                        newRow["FName"] = row["part_name"];
                        newRow["FUnit"] = row["unit"];
                        newRow["FBatchNo"] = row["lot_batch_no"];
                        newRow["FStockName"] = row["warehouse"];
                        newRow["FWDRID"] = row["W_D_R_ID"];
                        newRow["FQty"] = row["qty_box"];
                        newRow["FBasicQty"] = row["Qty_pic"];
                        newRow["FTrusteeshipFlag"] = "Y";
                        newRow["FSPName"] = row["location_no"];

                        table1.Rows.Add(newRow);
                    }
                    table1.AcceptChanges();
                    //LogHelper.WriteLog(JsonHelper.ToJson(table1));
                    //  table1.Merge(table2);

                }

                LogHelper.WriteLog(JsonHelper.ToJson(table1));
                return table1;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        /// <summary>
        /// 获取厂家发货计划最新数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetICSEOUTUpdateData()
        {
            try
            {
                string sql = "SELECT * FROM _PLANBill_HuaNai WHERE FSYNCSTATUS IN (-1,1,2)";
                return DbUtils.Query(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取Finfo_RE_id字段等于0的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetFinfo_RE_id0()
        {
            try
            {
                string sql = "SELECT top 50 * FROM _PLANBill_HuaNai WHERE Finfo_RE_id = 1";
                return DbUtils.Query(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 更新发货计划同步状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string UpdateFinfo_RE_id(int id)
        {
            try
            {
                PLANBill_HuaNaiDal.Instance.UpdateWhatWhere(new { Finfo_RE_id = 0 }, new { FID = id });

                return JsonHelper.ToJson(new { errCode = 0, errMsg = "处理成功！" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        /// <summary>
        /// 更新发货计划同步状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string UpdateCSEOUTSyncStatus(int id, int status)
        {
            try
            {
                PLANBill_HuaNaiDal.Instance.UpdateWhatWhere(new { FSYNCSTATUS = status }, new { FID = id });

                return JsonHelper.ToJson(new { errCode = 0, errMsg = "处理成功！" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取厂家发货计划最新数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetICSEOUTUpdateData2()
        {
            try
            {
                //string sql = "SELECT * FROM _Customer_TG_Order_HuaNai_test WHERE FSYNCSTATUS IN (-1,1,2)";
                string sql = "SELECT * FROM _Customer_TG_Order_HuaNai WHERE FSYNCSTATUS IN (-1,1,2)";
                return DbUtils.Query(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 更新发货计划同步状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string UpdateCSEOUTSyncStatus2(int id, int status)
        {
            try
            {
                Customer_TG_Order_HuaNaiDal.Instance.UpdateWhatWhere(new { FSYNCSTATUS = status }, new { FID = id });

                return JsonHelper.ToJson(new { errCode = 0, errMsg = "处理成功！" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取厂家销区发货最新数据
        /// </summary>
        /// <returns></returns>        
        public DataTable GetSTOCKUpdateData()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public DataTable GetTMP_STOCKBill()
        {
            try
            {
                string sql = "SELECT * FROM TMP_STOCKBill WHERE FSYNCSTATUS = 0 ORDER BY FBILLNO";
                return DbUtils.Query(sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public string UpdateTMP_STOCKBillStatus(int id, int status)
        {
            try
            {
                TMP_STOCKBillDal.Instance.UpdateWhatWhere(new { FSYNCSTATUS = status }, new { FID = id });

                return JsonHelper.ToJson(new { errCode = 0, errMsg = "处理成功！" });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


    }
}
