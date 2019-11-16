using hn.Client.Service.Common;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using hn.DataAccess;
using hn.DataAccess.bll;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using hn;
using hn.Common.Data;

namespace hn.Client.Service
{
    public partial class APIService
    {

        /// <summary>
        /// 获取经营场所列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<V_PREMISEModel> GetPremiseList(string keyword)
        {
            try
            {
                StringBuilder query = new StringBuilder();

                if (!keyword.IsNullOrEmpty())
                {
                    query.Append(" and ( ");

                    query.AppendFormat("  FCODE like '%{0}%' ", keyword);
                    query.AppendFormat(" OR FNAME like '%{0}%' ", keyword);
                    query.AppendFormat(" OR FCLASSAREA2NAME like '%{0}%' ", keyword);

                    query.Append(" ) ");
                }

                return V_PREMISEDal.Instance.GetWhereStr(query.ToString()).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 获取请购计划明细对象
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public V_ICPRBILLENTRYMODEL GetV_ICPRBILLENTRYMODEL(string FID) {
            return V_ICPRBILLENTRYDAL.Instance.GetWhereStr("  and FID ='" + FID + "'").FirstOrDefault();
        }
        /// <summary>
        /// 获取请购计划明细列表
        /// </summary>
        /// <param name="fpremisename"></param>
        /// <returns></returns>
        public List<V_ICPRBILLENTRYMODEL> V_ICPRBILLENTRYMODEL_List(string fpremisename)
        {
            return V_ICPRBILLENTRYDAL.Instance.GetWhereStr("  and fpremisename ='" + fpremisename + "'").ToList();
        }
        /// <summary>
        /// 获取请购计划明细列表
        /// </summary>
        /// <param name="icprbillid">请购计划单据ID</param>
        /// <returns></returns>
        public List<V_ICPRBILLENTRYMODEL> GetPurchasePlanEntryList(string icprbillid, string status, bool bICPO)
        {
            try
            {
                if (bICPO == false)
                {
                    if (!string.IsNullOrEmpty(status))
                    {
                        return V_ICPRBILLENTRYDAL.Instance.GetWhere(new { FPLANID = icprbillid, FStatus = status }).OrderBy(m => m.FENTRYID).ToList();
                    }
                    else
                    {
                        return V_ICPRBILLENTRYDAL.Instance.GetWhere(new { FPLANID = icprbillid }).OrderBy(m => m.FENTRYID).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(status))
                    {

                        return V_ICPRBILLENTRYDAL.Instance.GetWhereStr("  and fplanid='" + icprbillid + "' and fstatus='" + status + "' and (ICPOBILLENTRYID is null or ICPOBILLENTRYID='')").OrderBy(m => m.FSRCCODE).ToList();
                    }
                    else
                    {
                        return V_ICPRBILLENTRYDAL.Instance.GetWhereStr("  and fplanid='" + icprbillid + "'  and (ICPOBILLENTRYID is null or ICPOBILLENTRYID='')").OrderBy(m => m.FSRCCODE).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取请购计划明细列表（组柜用）
        /// </summary>
        /// <param name="productinfo">商品信息</param>
        /// <param name="classarea2name">销区</param>
        /// <param name="premiseid">经营场所</param>
        /// <param name="brand">品牌部</param>
        /// <param name="deliveryaddr">发货地点</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public List<V_ICPRBILLENTRYMODEL> GetPurchasePlanEntryByDeliveryList(
             hn.Core.Model.User loginUser,
             string yjfsid,
             string classarea2name,
             string premiseid,
             string brand,
             string deliveryaddr,
             string typename,
             string billno,
             int status, bool bHaveSL)
        {
            try
            {
                StringBuilder query = new StringBuilder();

                if (!yjfsid.IsNullOrEmpty())
                {
                    query.AppendFormat(" and fplanid in(select fid from icprbill where ftransname='{0}') ", yjfsid);
                }

                if (!premiseid.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FPREMISEID = '{0}' ", premiseid);
                }

                if (!classarea2name.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FCLASSAREA2NAME LIKE '%{0}%' ", classarea2name);
                }

                if (!brand.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FPREMISEBRANDNAME LIKE '%{0}%' ", brand);
                }

                //FPREMISEBRANDID
                //query.AppendFormat(" and FPREMISEBRANDID='01' ", "");

                if (!deliveryaddr.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FDELIVERYADDR LIKE '%{0}%' ", deliveryaddr);
                }

                if (!typename.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FTYPENAME LIKE '%{0}%' ", typename);
                }

                if (!billno.IsNullOrEmpty())
                {
                    query.AppendFormat(" and ICPRBILLNO LIKE '%{0}%' ", billno);
                }

                if (status > 0)
                {
                    query.AppendFormat(" and FSTATUS = {0} ", status);
                }

                if (bHaveSL)
                    query.AppendFormat(" and fid in (select icprbillentryid from v_icpr_icpo_icseout_thd where (leftsl>0 or leftsl is null) and autoid is not null and fdesbillno is not null) ", "");

                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }
                LogHelper.WriteLog(query.ToStr());




                List<V_ICPRBILLENTRYMODEL> list = V_ICPRBILLENTRYDAL.Instance.GetWhereStr(query.ToString()).ToList();
                return list;

                /*
                List<V_ICPRBILLENTRYMODEL> list1 = new List<V_ICPRBILLENTRYMODEL>();
                foreach (var sub in list)
                {
                    if (Get_CountTHD(sub.FID) > 0)
                    {
                        list1.Add(sub);
                    }
                }


                return list1; 
                */
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }

        }

        public int Get_CountTHD(string jhdh)
        {
            List<v_thdModel> list = new List<v_thdModel>();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("  FROM v_thd where 1=1 ");
                if (!string.IsNullOrEmpty(jhdh))
                {
                    // sql.AppendLine(" and autoid in (select autoid from v_icpr_icpo_icseout_thd where (leftsl>0 or leftsl is null)  and autoid is not null and fdesbillno is not null and icprbillentryid='" + jhdh + "')");
                    sql.AppendLine(" and cpxh not like '%各型号%' and cpxh not like '%托板%' and cpxh not like '%捆绑器%'  and autoid is not null and fdesbillno is not null and icprbillentryid='" + jhdh + "')");

                    //!x.cpxh.Contains("各型号")&&!x.cpxh.Contains("托板") && !x.cpxh.Contains("捆绑器")
                    //sql.AppendLine(" and cpxh not like '%各型号%' and cpxh not like '%托板%' and cpxh not like '%捆绑器%'");

                    //sql.AppendLine("  and pjhm in (select FDesBillNo from icpobill where fid in (select fplanid from  icpobillentry where fplanid in(select fid from icprbillentry where fid='"+jhdh+"')))");
                }
                else
                {
                    // sql.AppendLine(" and autoid in (select autoid from v_icpr_icpo_icseout_thd where (leftsl>0 or leftsl is null)  and autoid is not null and fdesbillno is not null)");

                    sql.AppendLine(" and cpxh not like '%各型号%' and cpxh not like '%托板%' and cpxh not like '%捆绑器%'  and autoid is not null and fdesbillno is not null)");



                }


                LogHelper.WriteLog(sql.ToString());

                List<v_thdModel> list33 = v_thdDal.Instance.Query(sql.ToString()).ToList();
                return list33.Count;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }


        }


        /// <summary>
        /// 获取请购计划列表
        /// </summary>
        /// <param name="classarea2name">销区</param>
        /// <param name="brand">品牌</param>
        /// <param name="status">状态</param>
        /// <param name="premiseid">经营场所</param>
        /// <returns></returns>
        public List<V_ICPRBILLMODEL> GetPurchasePlanList(
            hn.Core.Model.User loginUser,
            string classarea2name,
            string brand,
            int status,
            string premiseid,
            string billno,
            string startdate,
            string enddate,
            bool searchclose = false
            )
        {
            try
            {
                StringBuilder query = new StringBuilder();

                if (!premiseid.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FPREMISEID = '{0}' ", premiseid);
                }

                if (status > 0)
                {
                    query.AppendFormat(" and FStatus = {0} ", status);
                }

                if (!searchclose)
                {
                    query.AppendLine(" and FStatus <> 5 ");
                }

                if (!classarea2name.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FCLASSAREA2NAME like '%{0}%' ", classarea2name);
                }

                if (!billno.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FBILLNO like '%{0}%' ", billno);
                }

                if (!brand.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FBRANDID = '{0}' ", brand);
                }

                if (!startdate.IsNullOrEmpty() && startdate != "0001/01/01")
                {
                    query.AppendFormat(" and   FBILLDATE >= to_date('{0}','yyyy-MM-dd HH24:mi:ss') ", startdate);
                }

                if (!enddate.IsNullOrEmpty() && enddate != "0001/01/01")
                {
                    query.AppendFormat(" and   FBILLDATE <= to_date('{0}','yyyy-MM-dd HH24:mi:ss') ", enddate);
                }

                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                LogHelper.WriteLog(query.ToString());

                return V_ICPRBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public List<V_ICPRBILLMODEL> GetPurchasePlanImport(
         hn.Core.Model.User loginUser,
         string strQuery
         )
        {
            try
            {
                StringBuilder query = new StringBuilder();



                // query.AppendLine(" and FStatus = 3 ");


                if (!strQuery.IsNullOrEmpty())
                {
                    query.AppendFormat(" and (FCLASSAREA2NAME like '%{0}%' or FBILLNO like '%{1}%')", strQuery, strQuery);
                }


                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                LogHelper.WriteLog(query.ToString());

                return V_ICPRBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public List<V_ICPRBILLMODEL> GetPurchasePlanImport2(
   hn.Core.Model.User loginUser,
   string strQuery, string brandid
   )
        {
            try
            {
                StringBuilder query = new StringBuilder();

                // query.AppendLine(" and FStatus = 3 ");


                if (!strQuery.IsNullOrEmpty())
                {
                    query.AppendFormat(" and (FCLASSAREA2NAME like '%{0}%' or FBILLNO like '%{1}%')", strQuery, strQuery);
                }


                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                query.AppendFormat(" and FBRANDID ='{0}'", brandid);

                LogHelper.WriteLog(query.ToString());

                return V_ICPRBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public List<V_ICPRBILLMODEL> GetPurchasePlanImport3(
hn.Core.Model.User loginUser,
hn.Common. Cls_query.P_QueryOrder queryCls
)
        {
            try
            {
                StringBuilder query = new StringBuilder();


                if (loginUser.IsAdmin != 1)
                {
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                query.AppendFormat(" and FBRANDID ='{0}'", queryCls.brand);

                if (!string.IsNullOrEmpty(queryCls.address))
                {
                    query.AppendFormat(" and FRECEIVINGADDR like '%{0}%'", queryCls.address);
                }
                if (!string.IsNullOrEmpty(queryCls.P_BillNo))
                {
                    query.AppendFormat(" and FBILLNO like '%{0}%'", queryCls.P_BillNo);
                }
                if (queryCls.startTime != DateTime.MinValue)
                {
                    //query.AppendFormat(" and FBILLDATE >= to_date('{0}','yyyyMMdd')", queryCls.startTime);
                }

                if (queryCls.endTime != DateTime.MinValue)
                {
                    //query.AppendFormat(" and FBILLDATE <= to_date('{0}','yyyyMMdd')", queryCls.endTime);
                }

                // query.AppendFormat(" and FSTATUS='7' and FID not in (select FPLANID from icprbillentry where FID in (select FPLANID from ICPOBILLENTRY))", "");

                query.AppendFormat(" and FSTATUS='7' and FID in (select FPLANID from icprbillentry where ICPOBILLENTRYID is null or ICPOBILLENTRYID='')", "");

                LogHelper.WriteLog(query.ToString());

                return V_ICPRBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns></returns>
        public List<TB_BrandModel> GetBrandList(hn.Core.Model.User loginUser)
        {
            try
            {

                var brandlist = TB_BrandDal.Instance.GetAll().ToList();
                if (loginUser.IsAdmin != 1)
                {
                    List<TB_BrandModel> list = new List<TB_BrandModel>();

                    var userbrand = TB_USERBRANDDal.Instance.GetWhere(new { FUSERID = loginUser.FID });
                    foreach (var model in brandlist)
                    {
                        var count = userbrand.Where(m => m.FBRANDID == model.FID).Count();
                        if (count > 0)
                        {
                            list.Add(model);
                        }
                    }

                    return list;
                }
                else
                {
                    return brandlist;
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 采购确认
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ConfirmSave(string action, List<ICPRBILLENTRYMODEL> data, User loginUser)
        {
            var conn = DbUtils.GetConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                foreach (ICPRBILLENTRYMODEL model in data)
                {
                    ICPRBILLENTRYMODEL uptModel = ICPRBILLENTRYDAL.Instance.Get(model.FID);


                    if (uptModel != null)
                    {

                        //uptModel.FACCOUNT = model.FACCOUNT;
                        //uptModel.FSTOREHOUSE = model.FSTOREHOUSE;
                        //uptModel.FPOLICY = model.FPOLICY;
                        uptModel.FCOMMITQTY = model.FCOMMITQTY;
                        //uptModel.FTRANSNAME = model.FTRANSNAME;
                        uptModel.FORDERREMARK1 = model.FORDERREMARK1;
                        uptModel.FORDERREMARK2 = model.FORDERREMARK2;

                        if (action == "confirm")
                        {
                            uptModel.FCONFIRM_USER = loginUser.FID;
                            uptModel.FCONFIRM_TIME = DateTime.Now;
                            uptModel.FSTATUS = (int)Constant.ICPRBILL_FSTATUS.采购确认;
                        }
                        else if (action == "unconfirm")
                        {
                            var count = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICPRID = uptModel.FID }).ToList();
                            if (count.Count > 0)
                            {
                                continue;
                            }
                            uptModel.FSTATUS = (int)Constant.ICPRBILL_FSTATUS.审核通过;
                        }

                        LogHelper.WriteLog(JsonHelper.ToJson(uptModel));

                        DbUtils.Update(uptModel,conn,tran);
                    }

                }
                tran.Commit();
                conn.Close();

                var entry = ICPRBILLENTRYDAL.Instance.Get(data.First().FID);

                if (action == "unconfirm")
                {
                    DbUtils.UpdateWhatWhere< ICPRBILLMODEL>(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.审核通过 }, new { FID = entry.FPLANID });
                }
                else
                {
                    if (ICPRBILLENTRYDAL.Instance.GetConfirmStatus(entry.FPLANID) == 0)
                    {
                       
                    }
                    UpdateConfirmTime(entry.FID);
                    //LogHelper.WriteLog("Hello");
                   
                }

                

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("ConfirmError");
                LogHelper.WriteLog(ex);
                tran.Rollback();
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 关闭请购计划明细记录
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CloseSave(List<ICPRBILLENTRYMODEL> data, User loginUser, string content)
        {
            try
            {
                foreach (ICPRBILLENTRYMODEL model in data)
                {
                    ICPRBILLENTRYMODEL uptModel = ICPRBILLENTRYDAL.Instance.Get(model.FID);
                    if (uptModel != null)
                    {
                        uptModel.FSTATUS = (int)Constant.ICPRBILL_FSTATUS.关闭;
                        uptModel.FCLOSE_USER = loginUser.FID;
                        uptModel.FCLOSE_TIME = DateTime.Now;
                        uptModel.FCLOSE_RESON = content;
                        uptModel.FCLOSE = 1;

                        ICPRBILLENTRYDAL.Instance.Update(uptModel);

                        if (ICPRBILLENTRYDAL.Instance.GetCloseStatus(uptModel.FPLANID) == 0)
                        {
                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.关闭 }, new { FID = uptModel.FPLANID });
                        }
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 库存查询接口
        /// </summary>
        /// <param name="productname"></param>
        /// <param name="stockname"></param>
        /// <param name="productnumber"></param>
        /// <param name="wdr"></param>
        /// <param name="batchno"></param>
        /// <param name="brand"></param>
        /// <param name="mode"></param>
        /// <param name="colorno"></param>
        /// <returns></returns>
        public DataTable GetStockList(
           string productname = null,
           string stockname = null,
           string productnumber = null,
           string wdr = null,
           string batchno = null,
           string brand = null,
           string mode = null,
           string colorno = null,
           bool isfactory = true,
           bool istrust = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(brand) &&
                    (!string.IsNullOrEmpty(productname) || !string.IsNullOrEmpty(mode) || !string.IsNullOrEmpty(stockname) || !string.IsNullOrEmpty(colorno)))
                {
                    int pagecount = 0;
                    FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();
                    DataTable table = api.WmStock(
                        out pagecount,
                        productnumber,
                        productname,
                        stockname,
                        brand,
                        batchno,
                        wdr,
                        mode,
                        colorno,
                        20,
                        1, isfactory, istrust);

                    LogHelper.WriteLog(JsonHelper.ToJson(table));
                    return table;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取请购计划附件列表
        /// </summary>
        /// <param name="icprbillid"></param>
        /// <returns></returns>
        public List<TB_ATTACHMENTModel> GetAttachmentList(string billid)
        {
            try
            {
                StringBuilder query = new StringBuilder();

                if (!string.IsNullOrEmpty(billid))
                {
                    query.AppendFormat(" and FBILLID = '{0}' ", PublicMethod.GetString(billid));
                }

                return TB_ATTACHMENTDal.Instance.GetWhereStr(query.ToString(), "FADD_TIME DESC").ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 更新请购计划数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateICPRBILL(ICPRBILLMODEL model)
        {
            try
            {
                ICPRBILLMODEL icprModel = ICPRBILLDAL.Instance.Get(model.FID);
                icprModel.FIDENTIFICATION = model.FIDENTIFICATION;

                return ICPRBILLDAL.Instance.Update(icprModel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return -1;
            }
        }

        /// <summary>
        /// 获取请购计划数据
        /// </summary>
        /// <param name="icprbillid"></param>
        /// <returns></returns>
        public V_ICPRBILLMODEL GetPurchasePlanModel(string icprbillid)
        {
            try
            {
                V_ICPRBILLMODEL icprModel = V_ICPRBILLDAL.Instance.Get(icprbillid);

                return icprModel;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 刷新价格政策编号处理
        /// </summary>
        /// <param name="icprbillid"></param>
        /// <param name="entryids"></param>
        /// <returns></returns>
        public List<V_ICPRBILLENTRYMODEL> RefrshPriceNumberList(string icprbillid, List<string> entryids)
        {
            try
            {
                var list = V_ICPRBILLENTRYDAL.Instance.GetByIDList(entryids).ToList();
                foreach (var model in list)
                {
                    TB_PRICEPOLICYModel priceModel = TB_PRICEPOLICYDal.Instance.GetWhere(new { FPRODUCTNUMBER = model.FITEMID }).OrderBy(m => m.FAUDITPRICE).OrderByDescending(m => m.FID).FirstOrDefault();
                    if (priceModel != null)
                    {
                        ICPRBILLENTRYMODEL entryModel = ICPRBILLENTRYDAL.Instance.Get(model.FID);
                        entryModel.FPRICENUMBER = priceModel.FPRICENUMBER;
                        ICPRBILLENTRYDAL.Instance.Update(entryModel);
                    }
                }

                return GetPurchasePlanEntryList(icprbillid, "3", false);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取价格政策列表
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public List<TB_PRICEPOLICYModel> GetPriceNumberListByItemID(string itemid)
        {
            try
            {
                var list = TB_PRICEPOLICYDal.Instance.GetWhere(new { FPRODUCTNUMBER = itemid }).OrderBy(m => m.FAUDITPRICE).OrderByDescending(m => m.FID).ToList();

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取价格政策列表
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public List<TB_PRICEPOLICYModel> GetPriceNumberListByBrandID(string BrandID)
        {
            try
            {
                var list = TB_PRICEPOLICYDal.Instance.GetWhere(new { FBRAND = BrandID }).OrderBy(m => m.FAUDITPRICE).OrderByDescending(m => m.FID).ToList();

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 写入采购确认时间
        /// </summary>
        /// <param name="FPlanId"></param>

        public void UpdateConfirmTime(string entryFid)
        {

            var entry = ICPRBILLENTRYDAL.Instance.Get(entryFid);

            string sql = string.Format("SELECT COUNT(*) FROM ICPRBILLENTRY WHERE FPLANID='{0}'", entry.FPLANID);

            var allEntryCount = DbUtils.CountBySQL(sql);

            LogHelper.WriteLog(sql);

            sql += string.Format(" AND FSTATUS={0}", (int)Constant.ICPRBILL_FSTATUS.采购确认);

            LogHelper.WriteLog(sql);

            var confirmEntryCount = DbUtils.CountBySQL(sql);

            LogHelper.WriteLog(string.Format("COUNT1={0};COUNT2={1}", allEntryCount, confirmEntryCount));

            // 当所有计划明细都确认时，往请购计划单写入采购当前确认时间
            if (allEntryCount == confirmEntryCount)
            {
                DbUtils.UpdateWhatWhere<ICPRBILLMODEL>(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.采购确认, FConfirmTime = DateTime.Now }, new { FID = entry.FPLANID });
            }
        }
    }
}