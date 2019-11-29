using hn.Common;
using hn.Common.Data;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using hn.DataAccess;
using hn.DataAccess.bll;
using hn.DataAccess.Bll;
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
using System.Threading.Tasks;
using System.Web;

namespace hn.Client.Service
{
    public partial class APIService
    {



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
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取提货单
        /// </summary>
        /// <param name="thdbm"></param>
        /// <returns></returns>
        public v_thdModel getTHD(string thdbm)
        {
            try
            {
                List<v_thdModel> list = v_thdDal.Instance.GetWhere(new { AUTOID = thdbm }).ToList();

                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return new v_thdModel();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取提货单
        /// </summary>
        /// <param name="thdbmList"></param>
        /// <returns></returns>

        public List<v_thdModel> getTHDList(string[] thdbmList)
        {
            try
            {
                string where = " AND AUTOID IN (";
                thdbmList.ToList().ForEach(p =>
                {
                    where += "'" + p + "'";
                    where = thdbmList.Last() == p ? where += ")" : where += ",";
                });

                var list = v_thdDal.Instance.GetWhereStr(where);

                if (list.Count() > 0)
                {
                    return list.ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw;
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
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        public decimal getLeftNum_THD(string thdbm)
        {
            try
            {
                List<thdModel> list = thdDal.Instance.GetWhere(new { AUTOID = int.Parse(thdbm) }).ToList();

                List<ICSEOUTBILLENTRYMODEL> listOut = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { thdbm = thdbm }).ToList();

                decimal iOut = 0;


                foreach (var sub in listOut)
                {
                    iOut += sub.FCOMMITQTY;
                }

                if (list.Count > 0)
                {
                    thdModel thd = list[0];
                    decimal decimalSL = decimal.Parse(thd.sl);
                    decimalSL = decimalSL - iOut;
                    return decimalSL;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
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
                                //用户修改收地址后如果执行此句，会还原为原值导致修改失效----20191121  改
                                //model.FRECEIVERADDR = icprModel.FRECEIVINGADDR;
                                model.FPLANDESC = icprModel.JDE;
                                model.FPURCHASE_NO = icprModel.FPURCHASE_NO;
                                model.FREMARK = icprModel.FREMARK;
                                model.FSETTLE_ORG = icprModel.FSETTLE_ORG;

                            }
                        }

                    }
                }
                LogHelper.WriteLog(entrys[0].thdbm);
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
                LogHelper.WriteLog(ex);
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

                query.AppendFormat(" and fid in(select ficseoutid from ICSEOUTBILLentry where thdbm is not null) ", "");

                LogHelper.WriteLog(query.ToString());

                return V_ICSEOUTBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(JsonHelper.ToJson(list));

                return list;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
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
                        decimal total = PublicMethod.GetDecimal(table.Rows[0][0]);
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
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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


                    var icseoutlist = V_ICSEOUTBILLDAL.Instance.GetWhere(new { FID = id });
                    var icseoutentrylist = V_ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICSEOUTID = id });


                    FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();
                    resultjson = api.ICSEOUTBILLSync(icseoutlist.ToArray(), icseoutentrylist.ToArray());


                    if (!string.IsNullOrEmpty(resultjson))
                    {
                        DataResult result = JsonHelper.ConvertToObject<DataResult>(resultjson);
                        if (result != null)
                        {
                            ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FFACTORYSTATUS = 1, FSYNCSTATUS = 1 }, new { FID = id });
                            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(new { FERR_MESSAGE = "" }, new { FICSEOUTID = id });
                        }

                        LogHelper.WriteLog(resultjson);
                    }
                }

                return resultjson;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        public string SyncDeliveryByIDsMN(string jsonStr, string fid)
        {
            try
            {
                LogHelper.WriteLog(jsonStr);
                string resultjson = "";

                MApiModel.api12.Rootobject getapi6Temp = JSONhelper.ConvertToObject<MApiModel.api12.Rootobject>(jsonStr);
                MApiModel.api12.Datum[] LItem = getapi6Temp.data;


                foreach (var item in LItem)
                {
                    item.bz = item.bz == null ? "" : item.bz;
                    item.pjhm3 = item.pjhm;
                }


                MApiModel.api12.Rootobject getapi6 = new MApiModel.api12.Rootobject();
                MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();

                MApiModel.recToken.Rootobject g_token = new MApiModel.recToken.Rootobject();


                MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                tempToken.comid = getapi6Temp.comid;
                tempToken.action = "getToken";
                tempToken.khhm = "300384";
                tempToken.openkey = hn.Common.StringHelper.MD5string("10011630");
                g_token = Mapi.AccessApi1(tempToken);


                LogHelper.WriteLog(JSONhelper.ToJson(g_token));

                getapi6.comid = tempToken.comid;
                getapi6.action = "getMN_cp_1p_1";
                getapi6.token = g_token.token;

                getapi6.data = LItem;


                LogHelper.WriteLog(JSONhelper.ToJson(getapi6));

                MApiModel.recApi12.Rootobject recAPI6 = Mapi.AccessApi12(getapi6);

                LogHelper.WriteLog(JSONhelper.ToJson(recAPI6));


                if (recAPI6.status == 0)
                {

                    ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FFACTORYSTATUS = 1, FSYNCSTATUS = 1 }, new { FID = fid });
                    ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(new { FERR_MESSAGE = "" }, new { FICSEOUTID = fid });
                    LogHelper.WriteLog(resultjson);
                    resultjson = "1";
                }
                else
                {
                    resultjson = recAPI6.msg;
                }


                return resultjson;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public string SynDeliveryNot(string fid)
        {
            ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new { FFACTORYSTATUS = 0, FSYNCSTATUS = 0 }, new { FID = fid });
            ICSEOUTBILLENTRYDAL.Instance.UpdateWhatWhere(new { FERR_MESSAGE = "" }, new { FICSEOUTID = fid });

            return "1";
        }
        public string GetStartDate_syn()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * from thd where sl>xzsl and  rownum<=2000 order by rq ");
            List<thdModel> listTHD = thdDal.Instance.Query(sql.ToString()).ToList();

            string strStartDate = "2016-01-01";

            if (listTHD.Count == 0)
            {
                strStartDate = DateTime.Now.AddDays(-30).ToStr();
            }
            else
            {

                TimeSpan iSpan = DateTime.Now - listTHD[0].rq;
                if (iSpan.TotalDays > 30)
                {
                    strStartDate = DateTime.Now.AddDays(-30).ToStr();
                }
                else
                {
                    strStartDate = listTHD[0].rq.AddDays(-1).ToStr();
                }
            }
            return strStartDate;
        }


        public string GetStartDate_syn_cc()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * from thd where sl>xzsl and  rownum<=2000 order by rq ");
            List<thdModel> listTHD = thdDal.Instance.Query(sql.ToString()).ToList();

            string strStartDate = "2016-01-01";

            if (listTHD.Count == 0)
            {
                strStartDate = DateTime.Now.AddDays(-30).ToStr();
            }
            else
            {
                TimeSpan iSpan = DateTime.Now - listTHD[0].rq;
                if (iSpan.TotalDays > 30)
                {
                    strStartDate = DateTime.Now.AddDays(-30).ToStr();
                }
                else
                {
                    strStartDate = listTHD[0].rq.AddDays(-1).ToStr();
                }
            }
            return strStartDate;
        }


        public int GetLocalSum(string autoid, string icseoutbillentryid)
        {
            int iResult = 0;

            List<ICSEOUTBILLENTRYMODEL> list = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { thdbm = autoid }).ToList();
            if (list.Count == 0)
            {
                iResult = 0;
            }
            else
            {
                //FCOMMITQTY
                if (icseoutbillentryid != "")
                {
                    iResult = (int)list.Where(x => x.FID != icseoutbillentryid).Sum(x => x.FCOMMITQTY);
                }
                else
                {
                    iResult = (int)list.Sum(x => x.FCOMMITQTY);
                }
            }
            return iResult;

        }

        public string GeICPO_BOLListMN_syn(DateTime theTime)
        {

            string result = "";
            string strTemplate = "select  *　from　thd where AUTOID='theid'";


            int iCount = 0;

            try
            {


                // string rq1 = theTime.AddDays(-1).Year + "/" + (theTime.AddDays(-1).Month < 10 ? "0" + theTime.AddDays(-1).Month.ToStr() : theTime.AddDays(-1).Month.ToStr()) + "/" + (theTime.AddDays(-1).Day < 10 ? "0" + theTime.AddDays(-1).Day.ToStr() : theTime.AddDays(-1).Day.ToStr());
                //string rq2 = theTime.AddDays(1).Year + "/" + (theTime.AddDays(1).Month < 10 ? "0" + theTime.AddDays(1).Month.ToStr() : theTime.AddDays(1).Month.ToStr()) + "/" + (theTime.AddDays(1).Day < 10 ? "0" + theTime.AddDays(1).Day.ToStr() : theTime.AddDays(1).Day.ToStr());
                string rq1 = theTime.Year + "/" + (theTime.Month < 10 ? "0" + theTime.Month.ToStr() : theTime.Month.ToStr()) + "/" + (theTime.Day < 10 ? "0" + theTime.Day.ToStr() : theTime.Day.ToStr());
                string rq2 = rq1;


                MApiModel.recApi8.Rootobject recapi8 = new MApiModel.recApi8.Rootobject();
                MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();
                if (Global.g_token == null || Global.g_token.tokenInfo == null || string.IsNullOrEmpty(Global.g_token.tokenInfo.endDate) || DateTime.Parse(Global.g_token.tokenInfo.endDate) < DateTime.Now)
                {
                    MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                    tempToken.comid = 100;
                    tempToken.action = "getToken";
                    tempToken.khhm = "300384";
                    tempToken.openkey = hn.Common.StringHelper.MD5string("10011630");
                    Global.g_token = Mapi.AccessApi1(tempToken);
                }

                LogHelper.WriteLog(JsonHelper.ToJson(Global.g_token));

                MApiModel.api8.Rootobject getapi8 = new MApiModel.api8.Rootobject();
                getapi8.action = "getMN_cp_13";
                getapi8.token = Global.g_token.token;
                getapi8.khhm = "300384";
                getapi8.comid = 100;
                getapi8.pzhm = "";
                getapi8.pageIndex = 1;
                getapi8.pageSize = 1000;
                getapi8.rq1 = rq1;
                getapi8.rq2 = rq2;

                LogHelper.WriteLog(JsonHelper.ToJson(getapi8));
                LogHelper.WriteLog(JsonHelper.ToJson(getapi8));
                string strParam = MApiAccess.Helper.getProperties<MApiModel.api8.Rootobject>(getapi8);
                recapi8 = Mapi.AccessApi8(getapi8);
                LogHelper.WriteLog(JsonHelper.ToJson(recapi8));


                try
                {
                    if (recapi8.resultInfo.Length > 0)
                    {
                        foreach (var sub in recapi8.resultInfo)
                        {

                            List<thdModel> listTemp = thdDal.Instance.Query(strTemplate.Replace("theid", sub.AUTOID.ToStr())).ToList();
                            if (listTemp.Count == 0)
                            {
                                thdModel one = new thdModel();
                                one.AUTOID = sub.AUTOID;
                                one.cpdj = sub.cpdj;
                                one.cpgg = sub.cpgg;
                                one.cppz = sub.cppz;
                                one.cpxh = sub.cpxh;
                                one.DB = sub.DB.ToStr();
                                one.dj = sub.dj.ToStr();
                                one.dw = sub.dw;
                                one.je = sub.je.ToStr();
                                one.khhm = sub.khhm;
                                one.khhm1 = sub.khhm1;
                                one.khmc = sub.khmc;
                                one.khmc1 = sub.khmc1;
                                one.ks = sub.ks.ToStr();
                                one.pzhm = sub.pzhm.ToStr();
                                one.rq = Convert.ToDateTime(sub.rq);
                                one.sl = sub.sl.ToStr();
                                one.LEFTNUM = sub.sl;
                                one.CPSH = sub.cpsh;
                                one.CPCM = sub.cpcm;
                                one.tpackage = sub.package;
                                one.PJHM = sub.pjhm;
                                one.GG = sub.gg.ToStr();
                                one.GGS = sub.ggs.ToStr();
                                one.dhno = sub.dhno;
                                one.bz = sub.bz;
                                try
                                {
                                    one.XZSL = int.Parse(sub.xzsl);
                                }
                                catch
                                {
                                    one.XZSL = 0;
                                }
                                string s = thdDal.Instance.Insert(one);

                                if (s.IsGuid())
                                    iCount++;
                                else
                                {
                                    result += s;


                                }

                            }
                        }
                    }

                    result += theTime.ToString("yyyy-MM-dd") + "-同步" + iCount + "条记录";

                }
                catch (Exception ee)
                {
                    result = ee.ToStr();
                }



                return result;
            }
            catch (Exception ee)
            {
                result = ee.ToStr();
            }

            return result;

        }


        public string GeICPO_BOLListMN_syn_cc(DateTime theTime)
        {
            string result = "";

            int iCount = 0;
            try
            {
                string rq1 = theTime.Year + "/" + (theTime.Month < 10 ? "0" + theTime.Month.ToStr() : theTime.Month.ToStr()) + "/" + (theTime.Day < 10 ? "0" + theTime.Day.ToStr() : theTime.Day.ToStr());
                string rq2 = rq1;
                MApiModel.recApi8.Rootobject recapi8 = new MApiModel.recApi8.Rootobject();
                MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();
                if (Global.g_token == null || Global.g_token.tokenInfo == null || string.IsNullOrEmpty(Global.g_token.tokenInfo.endDate) || DateTime.Parse(Global.g_token.tokenInfo.endDate) < DateTime.Now)
                {
                    MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                    tempToken.comid = 100;
                    tempToken.action = "getToken";
                    tempToken.khhm = "300384";
                    tempToken.openkey = hn.Common.StringHelper.MD5string("10011630");
                    Global.g_token = Mapi.AccessApi1(tempToken);
                }

                LogHelper.WriteLog(JsonHelper.ToJson(Global.g_token));

                MApiModel.api8.Rootobject getapi8 = new MApiModel.api8.Rootobject();
                getapi8.action = "getMN_cp_13";
                getapi8.token = Global.g_token.token;
                getapi8.khhm = "300384";
                getapi8.comid = 100;
                getapi8.pzhm = "";
                getapi8.pageIndex = 1;
                getapi8.pageSize = 1000;
                getapi8.rq1 = rq1;
                getapi8.rq2 = rq2;

                LogHelper.WriteLog(JsonHelper.ToJson(getapi8));
                LogHelper.WriteLog(JsonHelper.ToJson(getapi8));
                string strParam = MApiAccess.Helper.getProperties<MApiModel.api8.Rootobject>(getapi8);
                recapi8 = Mapi.AccessApi8(getapi8);
                LogHelper.WriteLog(JsonHelper.ToJson(recapi8));


                try
                {
                    if (recapi8.resultInfo.Length > 0)
                    {
                        int iCount11 = thdDal.Instance.DeleteWhere(new { rq = Convert.ToDateTime(rq1) });
                        LogHelper.WriteLog("iCount=" + iCount11);
                        foreach (var sub in recapi8.resultInfo)
                        {

                            int iDelete = thdDal.Instance.DeleteWhere(new { AUTOID = sub.AUTOID });

                            thdModel one = new thdModel();
                            one.AUTOID = sub.AUTOID;
                            one.cpdj = sub.cpdj;
                            one.cpgg = sub.cpgg;
                            one.cppz = sub.cppz;
                            one.cpxh = sub.cpxh;
                            one.DB = sub.DB.ToStr();
                            one.dj = sub.dj.ToStr();
                            one.dw = sub.dw;
                            one.je = sub.je.ToStr();
                            one.khhm = sub.khhm;
                            one.khhm1 = sub.khhm1;
                            one.khmc = sub.khmc;
                            one.khmc1 = sub.khmc1;
                            one.ks = sub.ks.ToStr();
                            one.pzhm = sub.pzhm.ToStr();
                            one.rq = Convert.ToDateTime(sub.rq);
                            one.sl = sub.sl.ToStr();
                            one.LEFTNUM = sub.sl;
                            one.CPSH = sub.cpsh;
                            one.CPCM = sub.cpcm;
                            one.tpackage = sub.package;
                            one.PJHM = sub.pjhm;
                            one.GG = sub.gg.ToStr();
                            one.GGS = sub.ggs.ToStr();
                            one.dhno = sub.dhno;
                            one.bz = sub.bz;
                            try
                            {
                                one.XZSL = int.Parse(sub.xzsl);
                            }
                            catch
                            {
                                one.XZSL = 0;
                            }
                            string s = thdDal.Instance.Insert(one);

                            if (s.IsGuid())
                                iCount++;
                            else
                            {
                                result += s;


                            }


                        }
                    }

                    result += theTime.ToString("yyyy-MM-dd") + "-同步" + iCount + "条记录";

                }
                catch (Exception ee)
                {
                    result = ee.ToStr();
                }



                return result;
            }
            catch (Exception ee)
            {
                result = ee.ToStr();
            }

            return result;

        }

        public string GetCCD(DateTime theTime)
        {
            string result = "";

            int iCount = 0;
            try
            {
                string rq1 = theTime.Year + "/" + (theTime.Month < 10 ? "0" + theTime.Month.ToStr() : theTime.Month.ToStr()) + "/" + (theTime.Day < 10 ? "0" + theTime.Day.ToStr() : theTime.Day.ToStr());
                string rq2 = rq1;
                MApiModel.recApi9.Rootobject recapi8 = new MApiModel.recApi9.Rootobject();
                MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();
                if (Global.g_token == null || Global.g_token.tokenInfo == null || string.IsNullOrEmpty(Global.g_token.tokenInfo.endDate) || DateTime.Parse(Global.g_token.tokenInfo.endDate) < DateTime.Now)
                {
                    MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                    tempToken.comid = 100;
                    tempToken.action = "getToken";
                    tempToken.khhm = "300384";
                    tempToken.openkey = hn.Common.StringHelper.MD5string("10011630");
                    Global.g_token = Mapi.AccessApi1(tempToken);
                }

                LogHelper.WriteLog(JsonHelper.ToJson(Global.g_token));

                MApiModel.api9.Rootobject getapi8 = new MApiModel.api9.Rootobject();
                getapi8.action = "getMN_cp_14";
                getapi8.token = Global.g_token.token;
                getapi8.khhm = "300384";
                getapi8.comid = 100;
                getapi8.pzhm = "";
                getapi8.pageIndex = 1;
                getapi8.pageSize = 1000;
                getapi8.rq1 = rq1;
                getapi8.rq2 = rq2;

                LogHelper.WriteLog(JsonHelper.ToJson(getapi8));
                LogHelper.WriteLog(JsonHelper.ToJson(getapi8));
                string strParam = MApiAccess.Helper.getProperties<MApiModel.api9.Rootobject>(getapi8);
                recapi8 = Mapi.AccessApi9(getapi8);
                LogHelper.WriteLog(JsonHelper.ToJson(recapi8));


                try
                {
                    if (recapi8.resultInfo.Length > 0)
                    {
                        int iCount11 = ccdDal.Instance.DeleteWhere(new { rq = Convert.ToDateTime(rq1) });
                        LogHelper.WriteLog("iCount=" + iCount11);
                        foreach (var sub in recapi8.resultInfo)
                        {

                            int iDelete = ccdDal.Instance.DeleteWhere(new { AUTOID = sub.autoid });

                            ccdModel one = new ccdModel();
                            one.autoid = sub.autoid.ToStr();
                            one.cpdj = sub.cpdj;
                            one.cpgg = sub.cpgg;
                            one.cppz = sub.cppz;
                            one.cpxh = sub.cpxh;
                            one.db = sub.DB.ToStr();
                            one.dj = sub.dj.ToStr();
                            one.dw = sub.dw;
                            one.je = sub.je.ToStr();
                            one.khhm = sub.khhm;

                            one.khmc = sub.khmc;

                            one.ks = sub.ks.ToStr();
                            one.pzhm = sub.pzhm.ToStr();
                            one.rq = sub.rq;
                            one.sl = sub.sl.ToStr();


                            string s = ccdDal.Instance.Insert(one);

                            if (s.IsGuid())
                                iCount++;
                            else
                            {
                                result += s;


                            }


                        }
                    }

                    result += theTime.ToString("yyyy-MM-dd") + "-同步" + iCount + "条记录";

                }
                catch (Exception ee)
                {
                    result = ee.ToStr();
                }



                return result;
            }
            catch (Exception ee)
            {
                result = ee.ToStr();
            }

            return result;

        }
        /// <summary>
        /// 销区
        /// </summary>
        /// <returns></returns>
        public List<v_thd> v_thdModel_List()
        {
            return Newv_thdDal.Instance.GetAll().ToList();
        }
        /// <summary>
        /// 销区
        /// </summary>
        /// <returns></returns>
        public string[] v_thdModelName_List()
        {
            return Newv_thdDal.Instance.GetAll().Select(s => s.fpremisename).Distinct().ToArray();
        }
        public List<v_thdModel> GeICPO_BOLListMN_db(
     MApiModel.api8.Rootobject getapi8, string cpxh, string cpgg, string jhdh, string thd, string area2, int bNotArea)
        {
            List<v_thdModel> list = new List<v_thdModel>();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("  FROM v_thd where 1=1 ");
                sql.AppendLine(" and rq>= to_date('" + getapi8.rq1 + "','yyyy-mm-dd') and rq<= to_date('" + getapi8.rq2 + "','yyyy-mm-dd')");
                if (!string.IsNullOrEmpty(getapi8.pzhm))
                {
                    sql.AppendLine(" and pzhm like '%" + getapi8.pzhm + "%'");
                }

                if (!string.IsNullOrEmpty(cpxh))
                {
                    sql.AppendLine(" and cpxh like '%" + cpxh + "%'");
                }

                if (!string.IsNullOrEmpty(cpgg))
                {
                    sql.AppendLine(" and cpgg like '%" + cpgg + "%'");
                }

                if (bNotArea == 1)////1=没有对应的请购计划
                {
                    sql.AppendLine(" and icprbillentryid is  null");
                }

                //if (!string.IsNullOrEmpty(area2))
                //{
                //    string condition =area2== "无匹配关系" ? " and (nvl(fclassarea2,'zzz'） = 'zzz'" + " or fclassarea2name like ' %" + area2 + "%') " :
                //        " and （fclassarea2= '" + cpgg + "' or fclassarea2name like '%" + area2 + "%') ";
                //    //sql.AppendLine(" and （fclassarea2= '" + cpgg + "' or fclassarea2name like '%" + area2 + "%') ");
                //    sql.AppendLine(condition);
                //}
                if (!string.IsNullOrEmpty(area2))
                {
                    sql.AppendLine(" and fpremisename = '" + area2 + "'");
                }
                if (!string.IsNullOrEmpty(cpgg))
                {
                    sql.AppendLine(" and cpgg like '%" + cpgg + "%'");
                }
                if (!string.IsNullOrEmpty(thd))
                {
                    sql.AppendLine(" and pzhm like '%" + thd + "%'");
                }

                sql.AppendLine(" and regexp_like('301979,301980,301981,301982,301983,301984,301985,301986,301987,301988,30198801',KHHM)");
                //sql.AppendLine(" and cpcm!=null ");
                if (!string.IsNullOrEmpty(jhdh))
                {
                    //sql.AppendLine(" and autoid in (select autoid from v_icpr_icpo_icseout_thd where (leftsl>0 or leftsl is null) and sl>0  and autoid is not null and fdesbillno is not null and icprbillentryid='" + jhdh + "')");
                    sql.AppendLine(" and cpxh not like '%各型号%' and cpxh not like '%托板%' and cpxh not like '%捆绑器%' and sl>0  and autoid is not null and fdesbillno is not null and icprbillentryid='" + jhdh + "')");

                    //sql.AppendLine("  and pjhm in (select FDesBillNo from icpobill where fid in (select fplanid from  icpobillentry where fplanid in(select fid from icprbillentry where fid='"+jhdh+"')))");
                }
                else
                {

                    //sql.AppendLine(" and to_number(nvl(Sl,'0'))>0 and autoid in (select autoid from v_icpr_icpo_icseout_thd ) ");
                    sql.AppendLine(" and cpxh not like '%各型号%' and cpxh not like '%托板%' and cpxh not like '%捆绑器%' and to_number(nvl(Sl,'0'))>0  ");

                    //sql.AppendLine(" and autoid in (select autoid from v_icpr_icpo_icseout_thd where leftnum>0)");

                }

                sql.AppendLine(" order by rq desc");

                LogHelper.WriteLog(sql.ToString());

                List<v_thdModel> list33 = v_thdDal.Instance.Query(sql.ToString()).ToList();
                foreach (var sub in list33)
                {

                    if (string.IsNullOrEmpty(sub.gg))
                    {
                        sub.gg = "10.16";
                    }
                }
                LogHelper.WriteLog(JsonHelper.ToJson(list33));

                list33.RemoveAll(x => x.LEFTNUM1 <= 0);

                return list33;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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
                LogHelper.WriteLog(ex);
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
                    DataTable table = DbUtils.Query(sql);
                    decimal total = PublicMethod.GetDecimal(table.Rows[0][0]);
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
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
        #region 旧代码 20191116----18：30
        /// <summary>
        /// 约车处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        //public string ContractCar(List<string> ids)
        //{
        //    try
        //    {
        //        var list = V_ICSEOUTBILLDAL.Instance.GetByIDList(ids);
        //        foreach (var icseout in list)
        //        {
        //            var entrys = V_ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FPLANID = icseout.FID }).ToList();
        //            var items = new List<object>();
        //            foreach (var entry in entrys)
        //            {
        //                items.Add(new
        //                {
        //                    itemNo = entry.FENTRYID,
        //                    itemQuantity = entry.FASKQTY,
        //                    productCode = entry.FPRODUCTCODE,
        //                    productName = entry.FPRODUCTNAME,
        //                    categoriesCode = entry.FCATEGORYCODE,
        //                    categoriesName = entry.FCATEGORYNAME,
        //                    volume = entry.FVOLUME,
        //                    weight = entry.FWEIGHT,
        //                    value = entry.FPRICE,
        //                    color = entry.FCOLORNO,
        //                    format = entry.FMODEL,
        //                    brand = entry.FBRAND,
        //                    //lampNo = 
        //                    batch = entry.FBATCHNO,
        //                    storeCode = entry.FSTOCKNUMBER,
        //                    storeName = entry.FSTOCK,
        //                    //attributes = 
        //                    remark = entry.FREMARK,
        //                });
        //            }

        //            object sender = new { };
        //            V_DELIVER_BASEModel baseModel = V_DELIVER_BASEDal.Instance.Get(icseout.FDELIVER_BASE_ID);
        //            if (baseModel != null)
        //            {
        //                sender = new
        //                {
        //                    senderName = icseout.FDELIVERERADDR,
        //                    senderMobile = icseout.FCLIENTELE_PHONE,
        //                    senderPhone = icseout.FCLIENTELE_PHONE,
        //                    senderProvinceNO = baseModel.FPROVINCEID,
        //                    senderProvince = baseModel.FPROVINCENAME,
        //                    senderCityNO = baseModel.FCITYID,
        //                    senderCity = baseModel.FCITYNAME,
        //                    senderAreaNO = baseModel.FDISTRICTID,
        //                    senderArea = baseModel.FDISTRICTNAME,
        //                    senderAddress = baseModel.FADDRESS,
        //                };
        //            }


        //            var order = new
        //            {
        //                source = "3",
        //                sourceName = "采购平台",
        //                ownerUserId = "",
        //                ownerUserName = "",
        //                storeCode = icseout.FDELIVER_BASE_ID,
        //                storeName = icseout.FBASEA_NAME,
        //                orderNo = icseout.FGROUP_NO,
        //                userId = "3",
        //                userName = "华耐立家",
        //                sourceNo = "",
        //                sourceCode = "",
        //                reOrderNo = "",
        //                parOrderNo = "",
        //                orderType = "3",
        //                orderTypeName = "干线运输",
        //                businessType = icseout.FDELIVERY_TYPE,
        //                projectName = icseout.FPROJECTNAME,
        //                serviceType = "5",
        //                isFreight = "",
        //                transportType = icseout.FTRANSNAME,
        //                orderTime = icseout.FBILLDATE,
        //                totalValue = "",
        //                brandCode = icseout.FBRANDID,
        //                brandName = icseout.FBRANDNAME,
        //                totalWeight = icseout.FALLWEIGHT,
        //                //totalCapacity = icseout.f
        //                isinv = icseout.FISTICKET,
        //                remark1 = icseout.FREMARK,
        //                items = items,
        //                sender = sender,
        //                receiver = new
        //                {
        //                    receiverName = icseout.FCONSIGNEE,
        //                    receiverMobile = icseout.FCONSIGNEE_TEL,
        //                    receiverPhone = icseout.FCONSIGNEE_TEL,
        //                    receiverProvinceNO = icseout.FPROVINCEID,
        //                    receiverProvince = icseout.FRECEIVER_PROVINCE_NAME,
        //                    receiverCityNO = icseout.FCITYID,
        //                    receiverCity = icseout.FRECEIVER_CITY_NAME,
        //                    receiverAreaNO = icseout.FDISTRICTID,
        //                    receiverArea = icseout.FRECEIVER_DISTRICT_NAME,
        //                    receiverAddress = icseout.FRECEIVERADDR,
        //                },
        //                timeService = new
        //                {

        //                }
        //            };


        //            yaj_order(icseout.FID, order);
        //        }

        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(ex);
        //        throw ex;
        //    }
        //}

        #endregion

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
                    var entrys = V_ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICSEOUTID = icseout.FID }).ToList();
                    var items = new List<object>();
                    foreach (var entry in entrys)
                    {
                        items.Add(new
                        {
                            itemNo = entry.FENTRYID,
                            itemQuantity = entry.FCOMMITQTY,
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
                            unit = entry.FORDERUNIT,
                            //lampNo = 
                            batch = entry.FBATCHNO,
                            storeCode = entry.FSTOCKNUMBER,
                            storeName = entry.FSTOCK,
                            //attributes = 
                            remark = entry.FREMARK,
                        });
                    }

                    bool result = false;

                    if (icseout.FPUBLISH_COUNT == 0)
                    {
                        object sender = new { };
                        V_DELIVER_BASEModel baseModel = V_DELIVER_BASEDal.Instance.Get(icseout.FDELIVER_BASE_ID);
                        if (baseModel != null)
                        {
                            sender = JSONhelper.ToJson(new
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
                                senderTownNO = baseModel.FCOUNTYID,
                                senderTown = baseModel.FCOUNTYNAME,
                            });
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
                            totalValue = icseout.FTOTALVALUE.ToStr(),
                            brandCode = icseout.FBRANDID,
                            brandName = icseout.FBRANDNAME,
                            totalWeight = icseout.FACTUAL_WEIGHT,
                            totalCapacity = icseout.FACTUAL_VOLUME,
                            isinv = icseout.FISTICKET == 1 ? "1" : "0",
                            remark1 = icseout.FPLAN_INFO,
                            rem1 = icseout.FPRICE_BATCHNO,
                            rem2 = icseout.FPRICE_BY_OFFLINE,
                            items = JSONhelper.ToJson(items),
                            sender = sender,
                            receiver = JSONhelper.ToJson(new
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
                                receiverTownNO = icseout.FCOUNTYID,
                                receiverTown = icseout.FRECEIVER_COUNTY_NAME,
                            }),
                            timeService = JSONhelper.ToJson(new
                            {
                                scheduleDay = icseout.FREQUEST_DELIVERY_DATE.ToString("yyyy-MM-dd"),
                                deliveryDay = icseout.FESTIMATED_DELIVERY_DATE.ToString("yyyy-MM-dd")
                            }),
                            attributes = JSONhelper.ToJson(new
                            {
                                //是否含税
                                incTax = icseout.FISTICKET == 1 ? "1" : "0",
                                //是否报价
                                isQuote = icseout.FIS_PRICE,
                                //计费方式
                                BillMethod = icseout.FTRANSPORT_PRICE_TYPE_NAME,
                                //结算方式
                                SetMethod = icseout.FFREIGHT_NAME,
                                //承运商
                                carrierCode = icseout.FEXPRESSCOMPANYCODE,
                                //车牌号
                                numberPlate = icseout.FCARNUMBER,
                                //司机
                                driverName = icseout.FDELIVERER,
                                //联系方式
                                driverCode = icseout.FDELIVERERTEL,
                                //运费
                                freight = icseout.FSTANDARD_FREIGHT,
                                //单价
                                unitPrice = icseout.FFREIGHT_PRICE,

                            }),
                            invoice = JSONhelper.ToJson(new
                            {
                                billTitle = icseout.FCOMPANY,
                                billNo = icseout.FCOMPANY_NO
                            })
                        };

                        result = yaj_order("yaj_order", icseout.FID, icseout.FGROUP_NO, order);
                    }
                    else
                    {
                        var order = new
                        {
                            orderNo = icseout.FGROUP_NO,
                            source = "3",
                            sourceName = "采购平台",
                            factoryNo = icseout.FSRCBILLNO,
                            billingDate = icseout.FBILLDATE,
                            totalWeight = icseout.FACTUAL_WEIGHT,
                            totalCapacity = icseout.FALLVOLUME,
                            items = JSONhelper.ToJson(items),
                            status = "1", //1代表货物更新、2货物不更新、3厂家发货、4签收确认
                        };

                        result = yaj_order("yaj_update", icseout.FID, icseout.FGROUP_NO, order);
                    }

                    var m = ICSEOUTBILLDAL.Instance.Get(icseout.FID);
                    if (result)
                    {
                        m.FCAR_STATUS = 2;  //已发布
                        m.FPUBLISH_COUNT = m.FPUBLISH_COUNT + 1; //干线物流发布次数
                    }
                    else
                    {
                        m.FCAR_STATUS = 3;  //发布失败
                    }

                    //更新约车状态
                    ICSEOUTBILLDAL.Instance.Update(m);
                }

                return "";
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
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


        private bool yaj_order(string butype, string orderid, string orderno, object content)
        {
            string url = ConfigurationManager.AppSettings["PostOrderUrl"];
            if (!string.IsNullOrEmpty(url))
            {
                HttpItem item = new HttpItem();
                item.URL = url;
                item.Encoding = Encoding.UTF8;
                item.Method = "POST";
                item.ContentType = "application/x-www-form-urlencoded";
                item.PostEncoding = Encoding.UTF8;

                string textContent = JSONhelper.ToJson(new
                {
                    outcode = orderid,
                    orderNo = orderno,
                    notifytime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    butype = butype,
                    source = "3",
                    type = "json",
                    sign = StringHelper.ToBase64(StringHelper.MD5string(JSONhelper.ToJson(content) + "yaj,123")),
                    content = JSONhelper.ToJson(content)
                });

                item.Postdata = string.Format("businessKey={0}&merchantType=HNGX&token=HNGX&textContent={1}&orderType=HNGX", orderno, textContent);
                LogHelper.WriteLog(item.Postdata);
                HttpResult result = HttpHelper.Instance.GetHtml(item);
                if (!string.IsNullOrEmpty(result.Html))
                {
                    LogHelper.WriteLog(butype + "_result=" + result.Html);
                    OrderResult jsonRs = JSONhelper.ConvertToObject<OrderResult>(result.Html);

                    if (jsonRs.returnCode == 200)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public class OrderResult
        {
            public int returnCode;
            public string info;
            public string data;
        }

    }
}