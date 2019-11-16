using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using System.Configuration;
using hn.Client.Service;
using hn.DataAccess.model;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System.Text;
using hn.DataAccess.Dal;
using System.Data;
using hn.DataAccess;

namespace WebApiServer
{
    /// <summary>
    /// PleaseOrderServer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PleaseOrderServer : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

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
        /// 获取请购计划明细列表
        /// </summary>
        /// <param name="icprbillid">请购计划单据ID</param>
        /// <returns></returns>
        public List<V_ICPRBILLENTRYMODEL> GetPurchasePlanEntryList(string icprbillid, string status)
        {
            try
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
             string productinfo,
             string classarea2name,
             string premiseid,
             string brand,
             string deliveryaddr,
             string typename,
             string billno,
             int status)
        {
            try
            {
                StringBuilder query = new StringBuilder();

                if (!productinfo.IsNullOrEmpty())
                {
                    query.Append(" and ( ");

                    query.AppendFormat("  FPRODUCTCODE like '%{0}%' ", productinfo);
                    query.AppendFormat(" OR FPRODUCTNAME like '%{0}%' ", productinfo);
                    query.AppendFormat(" OR FMODEL like '%{0}%' ", productinfo);

                    query.Append(" ) ");
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

                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                return V_ICPRBILLENTRYDAL.Instance.GetWhereStr(query.ToString()).ToList();
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
                    query.AppendFormat(" and to_char(FBILLDATE,'yyyy/MM/dd')  >= '{0}' ", startdate);
                }

                if (!enddate.IsNullOrEmpty() && enddate != "0001/01/01")
                {
                    query.AppendFormat(" and to_char(FBILLDATE,'yyyy/MM/dd') <= '{0}' ", enddate);
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

                        ICPRBILLENTRYDAL.Instance.Update(uptModel);

                        if (action == "unconfirm")
                        {
                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.审核通过 }, new { FID = uptModel.FPLANID });
                        }
                        else
                        {
                            if (ICPRBILLENTRYDAL.Instance.GetConfirmStatus(uptModel.FPLANID) == 0)
                            {
                                ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.采购确认 }, new { FID = uptModel.FPLANID });
                            }
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
                   
                    return new DataTable();
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

                return GetPurchasePlanEntryList(icprbillid, "3");
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
    }
}
