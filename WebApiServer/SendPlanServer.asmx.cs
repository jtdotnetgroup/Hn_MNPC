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
using hn.Common;
using hn.Common.Data;
using hn.DataAccess.Bll;

namespace WebApiServer
{
    /// <summary>
    /// SendPlanServer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SendPlanServer : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        /// <summary>
        /// 生成单据编号
        /// </summary>
        /// <param name="billtype">单据类型</param>
        /// <returns></returns>
        public string GetNewBillNo(string billtype)
        {
            try
            {
                return DbUtils.GetBillNo(billtype, "年月");
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 组柜计划单据保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entrys"></param>
        /// <returns></returns>
        public bool DeleteGroupBill(string groupno)
        {
            try
            {
                List<ICSEOUTBILLMODEL> list = ICSEOUTBILLDAL.Instance.GetWhere(new { FGROUP_NO = groupno }).ToList();
                foreach (var m in list)
                {
                    ICSEOUTBILLENTRYDAL.Instance.DeleteWhere(new { FICSEOUTID = m.FID });
                }

                ICSEOUTBILLDAL.Instance.DeleteWhere(new { FGROUP_NO = groupno });

                return true;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 发货计划单据保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entrys"></param>
        /// <returns></returns>
        public bool DeliveryBillSave(ICSEOUTBILLMODEL model, List<ICSEOUTBILLENTRYMODEL> entrys, bool delivery = true)
        {
            try
            {
                if (!delivery)
                {
                    if (entrys.Count > 0)
                    {

                        List<ICPRBILLENTRYMODEL> icprModels = ICPRBILLENTRYDAL.Instance.GetWhere(new { FID = entrys[0].FICPRID }).ToList();
                        if (icprModels.Count > 0)
                        {
                            ICPRBILLMODEL icprModel = ICPRBILLDAL.Instance.Get(icprModels[0].FPLANID);
                            if (icprModel != null)
                            {
                                model.FPROJECTNAME = icprModel.FPROJECTNAME;
                                model.FRECEIVERADDR = icprModel.FRECEIVINGADDR;
                                model.FPLANDESC = icprModel.JDE;
                                model.FPURCHASE_NO = icprModel.FPURCHASE_NO;
                                model.FREMARK = icprModel.FREMARK;
                                model.FSETTLE_ORG = icprModel.FSETTLE_ORG;
                            }
                        }

                    }
                }
                var result = ICSEOUTBILLBLL.Instance.Save(model, entrys);
                if (result == null && !string.IsNullOrEmpty(model.FGROUP_NO))
                {
                    //插入装车发货明细
                    var details = TB_DELIVERY_DETAILDal.Instance.GetWhere(new { FGROUP_NO = model.FGROUP_NO }).ToList();
                    if (details.Count > 0)
                    {
                        TB_DELIVERY_DETAILModel m = details[0]; m.FGROUP_NO = model.FGROUP_NO;
                        m.FDELIVERDATE = model.FDELIVERDATE;
                        m.FBRANDID = model.FBRANDID;
                        m.FPREMISEID = model.FPREMISEID;
                        m.FPLAN_INFO = model.FPLAN_INFO;
                        m.FTRANSID = model.FTRANSID;
                        m.FDELIVERERADDR = model.FDELIVERERADDR;
                        m.FRECEIVERADDR = model.FRECEIVERADDR;
                        m.FALLWEIGHT = model.FALLWEIGHT;
                        m.FCARNUMBER = model.FCARNUMBER;
                        m.FEXPRESSCOMPANYID = model.FEXPRESSCOMPANYID;
                        TB_DELIVERY_DETAILDal.Instance.Update(m);
                    }
                    else
                    {
                        TB_DELIVERY_DETAILModel m = new TB_DELIVERY_DETAILModel();
                        m.FGROUP_NO = model.FGROUP_NO;
                        m.FDATE = DateTime.Now;
                        m.FDELIVERDATE = model.FDELIVERDATE;
                        m.FBRANDID = model.FBRANDID;
                        m.FPREMISEID = model.FPREMISEID;
                        m.FPLAN_INFO = model.FPLAN_INFO;
                        m.FTRANSID = model.FTRANSID;
                        m.FDELIVERERADDR = model.FDELIVERERADDR;
                        m.FRECEIVERADDR = model.FRECEIVERADDR;
                        m.FALLWEIGHT = model.FALLWEIGHT;
                        m.FCARNUMBER = model.FCARNUMBER;
                        m.FEXPRESSCOMPANYID = model.FEXPRESSCOMPANYID;
                        m.FSTATUS = 0;//草稿状态
                        TB_DELIVERY_DETAILDal.Instance.Insert(m);
                    }


                }


                return true;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取发货计划列表
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <param name="classarea2name">销区</param>
        /// <param name="premiseid">经营场所</param>
        /// <param name="status">状态</param>
        /// <param name="car">车辆</param>
        /// <param name="stock">仓库</param>
        /// <param name="account">厂家账户</param>
        /// <param name="expresscompany">承运公司</param>
        /// <param name="factotryno">厂家单号</param>
        /// <param name="billno">发货计划单号</param>
        /// <param name="groupno">组柜单号</param>
        /// <param name="projectname">工程名称</param>
        /// <param name="searchclose">是否查询已关闭的数据</param>
        /// <returns></returns>
        public List<V_ICSEOUTBILLMODEL> GetDeliveryList(
            hn.Core.Model.User loginUser,
            string brand,
            string classarea2name,
            string premiseid,
            int status,
            string car,
            string stock,
            string account,
            string expresscompany,
            string factotryno,
            string billno,
            string groupno,
            string projectname,
            string startdate,
            string enddate,
            bool searchclose = false)
        {
            try
            {
                StringBuilder query = new StringBuilder();

                if (!premiseid.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FPREMISEID = '{0}' ", premiseid);
                }

                if (!brand.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FBRANDID = '{0}' ", brand);
                }

                if (!account.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FCLIENTID = '{0}' ", account);
                }

                if (!expresscompany.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FEXPRESSCOMPANYID = '{0}' ", expresscompany);
                }

                if (!classarea2name.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FCLASSAREA2NAME LIKE '%{0}%' ", classarea2name);
                }

                if (!car.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FCARNUMBER LIKE '%{0}%' ", car);
                }

                if (!factotryno.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FSRCBILLNO LIKE '%{0}%' ", factotryno);
                }

                if (!billno.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FBILLNO LIKE '%{0}%' ", billno);
                }

                if (!groupno.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FGROUP_NO LIKE '%{0}%' ", groupno);
                }

                if (!projectname.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FPROJECTNAME LIKE '%{0}%' ", projectname);
                }

                if (status > 0)
                {
                    query.AppendFormat(" and FSTATUS = {0} ", status);
                }

                if (!searchclose)
                {
                    query.AppendLine(" and FStatus <> 5 ");
                }

                if (!startdate.IsNullOrEmpty())
                {
                    query.AppendFormat(" and to_char(FBILLDATE,'yyyy/MM/dd')  >= '{0}' ", startdate);
                }

                if (!enddate.IsNullOrEmpty())
                {
                    query.AppendFormat(" and to_char(FBILLDATE,'yyyy/MM/dd') <= '{0}' ", enddate);
                }


                if (loginUser.IsAdmin != 1)
                {
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat(" AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                hn.Common.LogHelper.WriteLog(query.ToString());

                return V_ICSEOUTBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取发货计划明细列表
        /// </summary>
        /// <param name="billid">发货计划单据ID</param>
        /// <returns></returns>
        public List<V_ICSEOUTBILLENTRYMODEL> GetDeliveryEntryList(string billid)
        {
            try
            {
                StringBuilder query = new StringBuilder();

                query.AppendFormat(" and FICSEOUTID = '{0}'  ", billid);

                var list = V_ICSEOUTBILLENTRYDAL.Instance.GetWhereStr(query.ToString()).ToList();
                hn.Common.LogHelper.WriteLog(JsonHelper.ToJson(list));

                return list;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 批量删除发货计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDeliveryByIDs(string ids)
        {
            try
            {
                string[] array = ids.Split(',');

                foreach (string id in array)
                {
                    ICSEOUTBILLDAL.Instance.Delete(id);

                    var list = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICSEOUTID = id }).ToList();

                    ICSEOUTBILLENTRYDAL.Instance.DeleteWhere(new { FICSEOUTID = id });
                    foreach (var model in list)
                    {
                        string sql = string.Format("SELECT SUM(FCOMMITQTY*FRATE) FROM V_ICSEOUTBILLENTRY WHERE FICPRID='{0}'", model.FICPRID);
                        DataTable table = DbUtils.Query(sql);
                        decimal total = hn.Common.PublicMethod.GetDecimal(table.Rows[0][0]);
                        ICPRBILLENTRYMODEL icprModel = ICPRBILLENTRYDAL.Instance.Get(model.FICPRID);
                        if (icprModel != null)
                        {
                            icprModel.FLEFTAMOUNT = icprModel.FASKQTY - total;

                            if (icprModel.FLEFTAMOUNT > 0)
                            {
                                icprModel.FSTATUS = 7;

                                ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.采购确认 }, new { FID = icprModel.FPLANID });
                            }

                            ICPRBILLENTRYDAL.Instance.Update(icprModel);


                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 批量审核发货计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int AuditDeliveryByIDs(string ids, int status, User loginUser)
        {
            try
            {
                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    ICSEOUTBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = status,
                            FCHECKERID = loginUser.FID,
                            FCHECKDATE = DateTime.Now
                        },
                        new { FID = id });
                }

                return 1;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 批量反审核发货计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int AuditAntiDeliveryByIDs(string ids)
        {
            try
            {
                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    ICSEOUTBILLDAL.Instance.UpdateWhatWhere(
                        new
                        {
                            FSTATUS = 1,
                            FCHECKERID = ""
                        },
                        new { FID = id });
                }

                return 1;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 批量上传数据到厂家库
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string SyncDeliveryByIDs(string ids)
        {
            try
            {
                string resultjson = "";
                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();

                    var icseoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FID = id });
                    var icseoutentrylist = V_ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICSEOUTID = id });

                    resultjson = api.ICSEOUTBILLSync(icseoutlist.ToArray(), icseoutentrylist.ToArray());
                    if (!string.IsNullOrEmpty(resultjson))
                    {
                        DataResult result = JsonHelper.ConvertToObject<DataResult>(resultjson);
                        if (result != null)
                        {
                            ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FFACTORYSTATUS = 1, FSYNCSTATUS = 1 }, new { FID = id });
                            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(new { FERR_MESSAGE = "" }, new { FICSEOUTID = id });
                        }

                        hn.Common.LogHelper.WriteLog(resultjson);
                    }
                }

                return resultjson;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        ///// <summary>
        ///// 获取发货计划数据
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public V_ICSEOUTBILLMODEL GetDeliveryData(string id)
        //{
        //    try
        //    {
        //        return V_ICSEOUTBILLDAL.Instance.Get(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(ex);
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 根据组柜编号获取发货计划明细列表
        /// </summary>
        /// <param name="groupno">组柜编号</param>
        /// <returns></returns>
        public List<V_ICSEOUTBILLENTRYMODEL> GetDeliveryEntryByGroupNo(string groupno)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT T1.*");
                sql.AppendLine("  FROM V_ICSEOUTBILLENTRY T1");
                sql.AppendLine(" INNER JOIN V_ICSEOUTBILL T2");
                sql.AppendLine("    ON T1.FICSEOUTID = T2.FID");
                sql.AppendFormat(" WHERE T2.FGROUP_NO = '{0}'", groupno);
                return V_ICSEOUTBILLENTRYDAL.Instance.Query(sql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 根据组柜编号获取发货计划列表
        /// </summary>
        /// <param name="groupno">组柜编号</param>
        /// <returns></returns>
        public List<V_ICSEOUTBILLMODEL> GetDeliveryByGroupNo(string groupno)
        {
            try
            {
                return V_ICSEOUTBILLDAL.Instance.GetWhere(new { FGROUP_NO = groupno }).ToList();
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 通过商品ID获取组柜厂家代码信息
        /// </summary>
        /// <param name="itemid">商品ID</param>
        /// <returns></returns>
        public SRCModel GetSrcModelByItemID(string itemid)
        {
            try
            {
                ProductModel product = ProductDal.Instance.Get(itemid);
                if (product != null)
                {
                    var list = SRCDal.Instance.GetWhere(new { FSRCCODE = product.FGROUP_NO }).ToList();
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 更新标识内容
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="centent"></param>
        /// <returns></returns>
        public bool UpdateIdentification(string ids, string content)
        {
            try
            {
                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FIDENTIFICATION = content }, new { FID = id });
                }

                return true;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 更新发货计划明细发货数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commitqty"></param>
        /// <returns></returns>
        public bool UpdateGetDeliveryEntryData(string id, decimal commitqty)
        {
            try
            {
                ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(new { FCOMMITQTY = commitqty }, new { FID = id });

                return true;
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取新的拆单单号
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public string GetDismantlingNo(string no)
        {
            try
            {
                string sql = string.Format("SELECT COUNT(1) FROM ICSEOUTBILL WHERE FBILLNO LIKE '{0}%'", no);
                DataTable table = DbUtils.Query(sql);
                if (table.Rows.Count > 0)
                {
                    string count = table.Rows[0][0].ToStr();

                    return no + "-" + count;
                }
                return "";
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        /// <summary>
        /// 更新组柜明细状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string UpdateDeliveryGroupStatus(string ids, int status)
        {
            try
            {
                string[] array = ids.Split(',');
                foreach (string id in array)
                {
                    ICSEOUTBILLENTRYMODEL model = ICSEOUTBILLENTRYDAL.Instance.Get(id);
                    if (model != null)
                    {
                        model.FGROUP_STATUS = status;
                    }
                    ICSEOUTBILLENTRYDAL.Instance.Update(model);

                    //更新请购计划数量
                    string sql = string.Format("SELECT SUM(FCOMMITQTY*FRATE) FROM V_ICSEOUTBILLENTRY WHERE FICPRID='{0}' and (FGROUP_STATUS is null or FGROUP_STATUS = 0)", model.FICPRID);
                    DataTable table = hn.Common.Data.DbUtils.Query(sql);
                    decimal total = hn.Common.PublicMethod.GetDecimal(table.Rows[0][0]);
                    ICPRBILLENTRYMODEL icprModel = ICPRBILLENTRYDAL.Instance.Get(model.FICPRID);
                    if (icprModel != null)
                    {
                        icprModel.FLEFTAMOUNT = icprModel.FASKQTY - total;

                        if (icprModel.FLEFTAMOUNT <= 0)
                        {
                            icprModel.FSTATUS = 5;
                        }
                        else
                        {
                            icprModel.FSTATUS = 7;
                        }

                        ICPRBILLENTRYDAL.Instance.Update(icprModel);

                        if (ICPRBILLENTRYDAL.Instance.GetCloseStatus(icprModel.FPLANID) == 0)
                        {
                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.关闭 }, new { FID = icprModel.FPLANID });
                        }
                        else
                        {
                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPRBILL_FSTATUS.采购确认 }, new { FID = icprModel.FPLANID });
                        }
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 约车处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string ContractCar(List<string> ids)
        {
            try
            {
                var list = V_ICSEOUTBILLDAL.Instance.GetByIDList(ids);
                foreach (var icseout in list)
                {
                    var entrys = V_ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FPLANID = icseout.FID }).ToList();
                    var items = new List<object>();
                    foreach (var entry in entrys)
                    {
                        items.Add(new
                        {
                            itemNo = entry.FENTRYID,
                            itemQuantity = entry.FASKQTY,
                            productCode = entry.FPRODUCTCODE,
                            productName = entry.FPRODUCTNAME,
                            categoriesCode = entry.FCATEGORYCODE,
                            categoriesName = entry.FCATEGORYNAME,
                            volume = entry.FVOLUME,
                            weight = entry.FWEIGHT,
                            value = entry.FPRICE,
                            color = entry.FCOLORNO,
                            format = entry.FMODEL,
                            brand = entry.FBRAND,
                            //lampNo = 
                            batch = entry.FBATCHNO,
                            storeCode = entry.FSTOCKNUMBER,
                            storeName = entry.FSTOCK,
                            //attributes = 
                            remark = entry.FREMARK,
                        });
                    }

                    object sender = new { };
                    V_DELIVER_BASEModel baseModel = V_DELIVER_BASEDal.Instance.Get(icseout.FDELIVER_BASE_ID);
                    if (baseModel != null)
                    {
                        sender = new
                        {
                            senderName = icseout.FDELIVERERADDR,
                            senderMobile = icseout.FCLIENTELE_PHONE,
                            senderPhone = icseout.FCLIENTELE_PHONE,
                            senderProvinceNO = baseModel.FPROVINCEID,
                            senderProvince = baseModel.FPROVINCENAME,
                            senderCityNO = baseModel.FCITYID,
                            senderCity = baseModel.FCITYNAME,
                            senderAreaNO = baseModel.FDISTRICTID,
                            senderArea = baseModel.FDISTRICTNAME,
                            senderAddress = baseModel.FADDRESS,
                        };
                    }


                    var order = new
                    {
                        source = "3",
                        sourceName = "采购平台",
                        ownerUserId = "",
                        ownerUserName = "",
                        storeCode = icseout.FDELIVER_BASE_ID,
                        storeName = icseout.FBASEA_NAME,
                        orderNo = icseout.FGROUP_NO,
                        userId = "3",
                        userName = "华耐立家",
                        sourceNo = "",
                        sourceCode = "",
                        reOrderNo = "",
                        parOrderNo = "",
                        orderType = "3",
                        orderTypeName = "干线运输",
                        businessType = icseout.FDELIVERY_TYPE,
                        projectName = icseout.FPROJECTNAME,
                        serviceType = "5",
                        isFreight = "",
                        transportType = icseout.FTRANSNAME,
                        orderTime = icseout.FBILLDATE,
                        totalValue = "",
                        brandCode = icseout.FBRANDID,
                        brandName = icseout.FBRANDNAME,
                        totalWeight = icseout.FALLWEIGHT,
                        //totalCapacity = icseout.f
                        isinv = icseout.FISTICKET,
                        remark1 = icseout.FREMARK,
                        items = items,
                        sender = sender,
                        receiver = new
                        {
                            receiverName = icseout.FCONSIGNEE,
                            receiverMobile = icseout.FCONSIGNEE_TEL,
                            receiverPhone = icseout.FCONSIGNEE_TEL,
                            receiverProvinceNO = icseout.FPROVINCEID,
                            receiverProvince = icseout.FRECEIVER_PROVINCE_NAME,
                            receiverCityNO = icseout.FCITYID,
                            receiverCity = icseout.FRECEIVER_CITY_NAME,
                            receiverAreaNO = icseout.FDISTRICTID,
                            receiverArea = icseout.FRECEIVER_DISTRICT_NAME,
                            receiverAddress = icseout.FRECEIVERADDR,
                        },
                        timeService = new
                        {

                        }
                    };


                    yaj_order(icseout.FID, order);
                }

                return "";
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        private string yaj_order(string orderid, object content)
        {
            string url = ConfigurationManager.AppSettings["PostOrderUrl"];
            if (!string.IsNullOrEmpty(url))
            {
                HttpItem item = new HttpItem();
                item.URL = ConfigurationManager.AppSettings["PostOrderUrl"];
                item.Encoding = Encoding.UTF8;
                item.Method = "POST";
                item.Accept = "application/json";
                item.ContentType = "application/json;charset=utf-8";
                item.PostEncoding = Encoding.UTF8;

                var postdata = new
                {
                    outcode = orderid,
                    notifytime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    butype = "yaj_order",
                    source = "B",
                    type = "json",
                    sign = StringHelper.ToBase64(StringHelper.MD5string(JSONhelper.ToJson(content) + "yaj,123")),
                    content = content
                };

                item.Postdata = JSONhelper.ToJson(postdata);
                HttpResult result = HttpHelper.Instance.GetHtml(item);
                if (!string.IsNullOrEmpty(result.Html))
                {
                    return result.Html;
                }
            }

            return "";
        }

    }
}
