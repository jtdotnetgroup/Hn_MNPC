using hn.Common;
using hn.Core;
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
using System.Web;

namespace hn.Client.Service
{
    public partial class APIService
    {
        /// <summary>
        /// 插入采购订单
        /// </summary>
        /// <param name="ICPOBILLJson">主表Json</param>
        /// ICPOBILLJson={"FTRANSTYPE":"0","FID":"","FBRANDID":"00","FBRANDNAME":"马可波罗","FCLIENTID":"aa4d7dad-4583-4330-b3ef-1e9dead53fb1","FCLIENTACCOUNT":"M4439","FDATE":"2018-09-14","FBILLNO":"PO201809009","FBILLERID":"admin","FTELEPHONE":"","FSTATUSNAME":"草稿","FREMARK":"ggggg"}
        /// <param name="ICPOBILLENTRYListJson">副表的Json</param>
        /// ICPOBILLENTRYListJson=[{"FENTRYID":1,"FMODEL":"165*165","FPLANID":"6bec5469-d30c-4554-8d4f-5ea2c2cb5f5a","FPRODUCTNAME":"亚光砖_FA1305","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010753","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":378,"FSRCQTY":378,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":2,"FMODEL":"165*165","FPLANID":"2894bf6d-e03e-4ee7-900d-706b6b88e258","FPRODUCTNAME":"亚光砖_FA1302","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010784","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":594,"FSRCQTY":594,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":3,"FMODEL":"600*600","FPLANID":"d64f1e3c-2f97-4364-b658-5d715df67dd7","FPRODUCTNAME":"亚光砖_S6052","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00019395","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":5280,"FSRCQTY":5280,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":4,"FMODEL":"600*600","FPLANID":"c32a511a-01bf-4142-9d66-69dcf25e61c3","FPRODUCTNAME":"亚光砖_CI6260","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010145","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":3336,"FSRCQTY":3336,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":5,"FMODEL":"300*450","FPLANID":"ff26b913-3075-4727-95d7-8424a8ecaf8c","FPRODUCTNAME":"亮光瓷片_M45088","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00020572","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":5780,"FSRCQTY":5780,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":6,"FMODEL":"165*165","FPLANID":"6d16973f-5c22-47f4-944f-624173967b61","FPRODUCTNAME":"亚光砖_FA1308","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010771","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":1350,"FSRCQTY":1350,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":7,"FMODEL":"165*165","FPLANID":"4cd282fe-576f-4b76-9c69-bd273f23011d","FPRODUCTNAME":"亚光砖_FA1301","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010752","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":378,"FSRCQTY":378,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"}]
        /// <returns></returns>
        public string SaveICPOBILL(ICPOBILLMODEL ICPOBILL, List<ICPOBILLENTRYMODEL> ICPOBILLENTRYList) {
            string result = ICPOBILLBLL.Instance.SaveClient(ICPOBILL, ICPOBILLENTRYList);
            if (result.IsNullOrEmpty())
            {
                return "保存完成！"+ result;
            }
            else
            {
                return result;
            }
        }


        public string ZFICPOBILL(string fEntryID)
        {
            int result = ICPOBILLENTRYDAL.Instance.Delete(fEntryID);
            //ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.审核通过 }, new { FID = uptModel.FICPOBILLID });
            ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new { ICPOBILLENTRYID = "" }, new { ICPOBILLENTRYID = fEntryID });        
            if (result>0)
            {
                return "作废成功！";
            }
            else
            {
                return "作废失败！";
            }
        }

        public string ZFICPRBILLEntry(string fEntryID)
        {
            int result = 1;

            //ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.审核通过 }, new { FID = uptModel.FICPOBILLID });
            result=ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new { ICPOBILLENTRYID = "" }, new { icpobillentryid = fEntryID });

            LogHelper.WriteLog(fEntryID);
            //return "1";
            if (result > 0)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }



        public V_ICPOBILLMODEL getICPOBillCLientID(string thdbm)
        {
            List<V_ICPOBILLMODEL> list = V_ICPOBILLDAL.Instance.GetWhereStr(" and FDESBILLNO='" + thdbm + "'", "FID desc").ToList();
            if (list.Count == 0)
            {
                return new V_ICPOBILLMODEL();
            }
            else
            {
               return list[0];
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
        public List<V_ICPOBILLMODEL> GetOrderList(
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
                    query.AppendFormat("  and FBILLDATE >= to_date('{0}','yyyy-MM-dd HH24:mi:ss') ", startdate);
                }

                if (!enddate.IsNullOrEmpty() && enddate != "0001/01/01")
                {
                    query.AppendFormat(" and FBILLDATE <= to_date('{0}','yyyy-MM-dd HH24:mi:ss') ", enddate);
                }

                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                LogHelper.WriteLog(query.ToString());
                
                return V_ICPOBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

       public List<ICPO_BOLentryModel> GeICPO_BOLList(
           hn.Core.Model.User loginUser,
          string FPObillno,
          string Ficbolno,
          int FSYNCSTATUS,
          string FACCOUNT,
          string FITEMID,
          string startdate,
          string enddate, bool searchclose = false)
        {
            try
            {
                StringBuilder query = new StringBuilder();

                if (!FPObillno.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FPObillno = '{0}' ", FPObillno);
                }

                if (!Ficbolno.IsNullOrEmpty())
                {
                    query.AppendFormat(" and Ficbolno = '{0}' ", Ficbolno);
                }

                if (FSYNCSTATUS > 0)
                {
                    query.AppendFormat(" and FSYNCSTATUS = {0} ", FSYNCSTATUS);
                }

                if (!FACCOUNT.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FACCOUNT = '{0}' ", FACCOUNT);
                }

                if (!FITEMID.IsNullOrEmpty())
                {
                    query.AppendFormat(" and FITEMID = '{0}' ", FITEMID);
                }
                

                if (!startdate.IsNullOrEmpty() && startdate != "0001/01/01")
                {
                    query.AppendFormat(" and  FDATE >= to_date('{0}','yyyy-MM-dd HH24:mi:ss') ", startdate);
                }

                if (!enddate.IsNullOrEmpty() && enddate != "0001/01/01")
                {
                    query.AppendFormat(" and  FDATE <= to_date('{0}','yyyy-MM-dd HH24:mi:ss') ", enddate);
                }

                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                LogHelper.WriteLog(query.ToString());

                return ICPO_BOLentryDAL.Instance.GetWhereStr(query.ToString(), "FDATE DESC").ToList();
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
        public List<V_ICPOBILLENTRYMODEL> GetOrderEntryList(string icprbillid, string status)
        {
            try
            {
                if (!string.IsNullOrEmpty(status))
                {
                    return V_ICPOBILLENTRYDAL.Instance.GetWhere(new { FICPOBILLID = icprbillid, FStatus = status }).OrderBy(m => m.FENTRYID).ToList();
                }
                else
                {
                    return V_ICPOBILLENTRYDAL.Instance.GetWhere(new { FICPOBILLID = icprbillid }).OrderBy(m => m.FENTRYID).ToList();
                }
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
        public ICPOBILLMODEL GetSingleOrder(string icprbillid)
        {
            ICPOBILLMODEL result = new ICPOBILLMODEL();
            try
            {                
               List<ICPOBILLMODEL> list= ICPOBILLDAL.Instance.GetWhere(new { FID = icprbillid }).ToList();
                if (list.Count > 0)
                    result = list[0];
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// 采购确认,要同时更新请购单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ConfirmSave_ICPO(string action, List<ICPOBILLENTRYMODEL> data, User loginUser)
        {
            try
            {
                List<ICPRBILLENTRYMODEL> tempPRList = new List<ICPRBILLENTRYMODEL>();
                foreach (ICPOBILLENTRYMODEL model in data)
                {
                    ICPRBILLENTRYMODEL tempPR = ICPRBILLENTRYDAL.Instance.Get(model.FPLANID);
                    if (tempPR != null)
                    {
                        tempPRList.Add(tempPR);
                    }

                    ICPOBILLENTRYMODEL uptModel = ICPOBILLENTRYDAL.Instance.Get(model.FID);
                    if (uptModel != null)
                    {

                     
                        if (action == "confirm")
                        {
                           
                            uptModel.FSTATUS = (int)Constant.ICPOBILL_FSTATUS.采购确认;
                        }
                        else if (action == "unconfirm")
                        {
                                                     
                            uptModel.FSTATUS = (int)Constant.ICPOBILL_FSTATUS.审核通过;
                        }

                        LogHelper.WriteLog(JsonHelper.ToJson(uptModel));

                        ICPOBILLENTRYDAL.Instance.Update(uptModel);

                        if (action == "unconfirm")
                        {
                            ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.审核通过 }, new { FID = uptModel.FICPOBILLID });
                        }
                        else
                        {
                            if (ICPOBILLENTRYDAL.Instance.GetConfirmStatus(uptModel.FID) == 0)
                            {
                                ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.采购确认 }, new { FID = uptModel.FICPOBILLID });
                            }
                        }
                    
                       
                        

                    }

                }

                if (tempPRList.Count > 0)
                {
                    ConfirmSave(action, tempPRList, loginUser);
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
        public bool CloseSave_ICPO(List<ICPOBILLENTRYMODEL> data, User loginUser, string content)
        {
            try
            {
                foreach (ICPOBILLENTRYMODEL model in data)
                {
                    ICPOBILLENTRYMODEL uptModel = ICPOBILLENTRYDAL.Instance.Get(model.FID);
                    if (uptModel != null)
                    {
                        uptModel.FSTATUS = (int)Constant.ICPOBILL_FSTATUS.关闭;

                        ICPOBILLENTRYDAL.Instance.Update(uptModel);

                        if (ICPOBILLENTRYDAL.Instance.GetCloseStatus(uptModel.FID) == 0)
                        {
                            ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.关闭 }, new { FID = uptModel.FICPOBILLID });
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


        public bool Delete_ICPO(string fid)
        {
            try
            {
                List<ICPOBILLENTRYMODEL> list = ICPOBILLENTRYDAL.Instance.GetWhereStr(" and FICPOBILLID='" + fid+"'").ToList();

                foreach (var sub in list)
                {
                    ZFICPOBILL(sub.FID);
                }


                ICPOBILLDAL.Instance.DeleteWhere(new { FID=fid});
                ICPOBILLENTRYDAL.Instance.DeleteWhere(new { FICPOBILLID = fid });

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        public bool Delete_ICPOEntry(string fid)
        {
            try
            {
                List<ICPOBILLENTRYMODEL> list = ICPOBILLENTRYDAL.Instance.GetWhereStr(" and FICPOBILLID='" + fid + "'").ToList();

                foreach (var sub in list)
                {
                    ZFICPOBILL(sub.FID);
                }


                ICPOBILLDAL.Instance.DeleteWhere(new { FID = fid });
                ICPOBILLENTRYDAL.Instance.DeleteWhere(new { FICPOBILLID = fid });

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
        public bool AuditSave_ICPO(List<ICPOBILLENTRYMODEL> data, User loginUser, string content)
        {
            try
            {
                foreach (ICPOBILLENTRYMODEL model in data)
                {
                    ICPOBILLENTRYMODEL uptModel = ICPOBILLENTRYDAL.Instance.Get(model.FID);
                    if (uptModel != null)
                    {
                        uptModel.FSTATUS = (int)Constant.ICPOBILL_FSTATUS.审核通过;
                       
                        ICPOBILLENTRYDAL.Instance.Update(uptModel);

                     
                            ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.审核通过 }, new { FID = uptModel.FICPOBILLID });
                        
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

        public bool UnAuditSave_ICPO(List<ICPOBILLENTRYMODEL> data, User loginUser, string content)
        {
            try
            {
                foreach (ICPOBILLENTRYMODEL model in data)
                {
                    ICPOBILLENTRYMODEL uptModel = ICPOBILLENTRYDAL.Instance.Get(model.FID);
                    if (uptModel != null)
                    {
                        uptModel.FSTATUS = (int)Constant.ICPOBILL_FSTATUS.草稿;

                        ICPOBILLENTRYDAL.Instance.Update(uptModel);


                        ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = (int)Constant.ICPOBILL_FSTATUS.草稿 }, new { FID = uptModel.FICPOBILLID });

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



        public string Save_ICPREntry_List(ICPRBILLENTRYMODEL tModel)
        {
            int iEntryID = 1;
            if (tModel.FPLANID == "0")
            {
               int iCount= ICPRBILLENTRYDAL.Instance.CountWhere(new { FPLANID="0",FITEMID=tModel.FITEMID});
                iEntryID = iEntryID + iCount;
                tModel.FENTRYID = iEntryID;
            }

            return ICPRBILLENTRYDAL.Instance.Insert(tModel);
        }

        public int Update_FSYNStatus(ICPOBILLMODEL billMode,int iStatus)
        {
            return ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSYNCSTATUS = iStatus }, new { FID = billMode.FID });
        }

        public int Update_FSYN_Remote_Status(string billMode, int iStatus,string cjbh, Dictionary<int, string> dic_entry_thdbmdetail)
        {
            if (dic_entry_thdbmdetail != null && dic_entry_thdbmdetail.Count > 0)
            {
                foreach(var sub in dic_entry_thdbmdetail)
                {
                    ICPOBILLENTRYDAL.Instance.UpdateWhatWhere(new { THDBMDETAIL = sub.Value }, new { FICPOBILLID = billMode, FENTRYID=sub.Key });
                }
            }

            return ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSYNCSTATUS = iStatus, FDesBillNo=cjbh }, new { FID = billMode });
        }

        public string Remote_InsertICPOEntry(MApiModel.api3.Rootobject getapi3)
        {
            try
            {

                MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();


                MApiModel.recToken.Rootobject g_token = new MApiModel.recToken.Rootobject();


                MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                tempToken.comid = int.Parse(getapi3.comid);
                tempToken.action = "getToken";
                tempToken.khhm = "300384";
                tempToken.openkey = hn.Common.StringHelper.MD5string("10011630");
                g_token = Mapi.AccessApi1(tempToken);


                LogHelper.WriteLog(JSONhelper.ToJson(g_token));

                getapi3.token = g_token.token;

                MApiModel.recApi3.Rootobject recapi3 = Mapi.AccessApi3(getapi3);
                LogHelper.WriteLog(JSONhelper.ToJson(recapi3));
                if (recapi3.status == 0)
                {
                    try
                    {
                        return recapi3.resultInfo[0].pzhm;
                    }
                    catch
                    {
                        return "error:未返回编号";
                    }
                }
                else
                {
                    return "error:" + recapi3.msg;
                }
            }
            catch (Exception ee)
            {

                return "error:" + ee.ToString();
            }

        }


        public MApiModel.recApi24.Rootobject Remote_Get24(
MApiModel.api24.Rootobject getapi24)
        {
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

           // getapi24.comid = "100";
            getapi24.action = "getMN_cp_24";
            getapi24.token = Global.g_token.token;

            MApiModel.recApi24.Rootobject rec = Mapi.AccessApi24(getapi24);

            return rec;
        }
        public  MApiModel.recApi2.Rootobject GetStockListMN(
  MApiModel.api2.Rootobject getapi2)
        {
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

            getapi2.comid = "100";
            getapi2.action = "getMN_cp_11";
            getapi2.token = Global.g_token.token;

            MApiModel.recApi2.Rootobject rec = Mapi.AccessApi2(getapi2);

            return rec;
        }

        public MApiModel.recApi2.Rootobject GetStockListMN_2(
MApiModel.api2.Rootobject getapi2,int comid)
        {
            MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();

           
                MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                tempToken.comid = comid;
                tempToken.action = "getToken";
                tempToken.khhm = "300384";
                tempToken.openkey = hn.Common.StringHelper.MD5string("10011630");


            MApiModel.recToken.Rootobject token = Mapi.AccessApi1(tempToken);
           

            getapi2.comid = "100";
            getapi2.action = "getMN_cp_11";
            getapi2.token = token.token;

            MApiModel.recApi2.Rootobject rec = Mapi.AccessApi2(getapi2);

            return rec;
        }


        public void Remote_GetICPOEntry(string strFID,string strEntryID,ref ICPOBILLMODEL billModel,ref ICPOBILLENTRYMODEL entryModel)
        {
            FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();
            FactoryService.ICPOBILLENTRYModel_MHLS tempModel= api.GetICPOEntry(strFID,strEntryID);
            billModel.FID = tempModel.FID;
            billModel.FBILLNO = tempModel.FBILLNO;
            billModel.FDesBillNo = tempModel.FDesBillNo;
            billModel.Fcompany = tempModel.Fcompany;
            billModel.FprojectNO = tempModel.FprojectNO;
            billModel.FSYNCSTATUS = tempModel.FSYNCSTATUS;
            billModel.FPOtype = tempModel.FPOtype;
            billModel.Fpricepolicy = tempModel.Fpricepolicy;

            entryModel.Fdesbillentry = tempModel.Fdesbillentry;
            entryModel.FERR_MESSAGE = tempModel.FERR_MESSAGE;

        }


       public  List<ICPO_BOLentryModel> Remote_GetICPO_BOEntry(string fbillno, string entryid)
        {
            List<ICPO_BOLentryModel> resultList = new List<ICPO_BOLentryModel>();

            FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();
            List<FactoryService.ICPO_BOLentryModel_MNLS> tempModelList= api.GetICPO_BOEntry(fbillno, entryid).ToList();
            foreach (var sub in tempModelList)
            {
                ICPO_BOLentryModel model = new ICPO_BOLentryModel();
                model.FACCOUNT = sub.FACCOUNT;
                model.Famount = sub.Famount;
                model.FAUDQTY = sub.FAUDQTY;
                model.FCOLORNO = sub.FCOLORNO;
                model.FcommitQTY = sub.FcommitQTY;
                model.FcontractNO = sub.FcontractNO;
                model.FDATE = sub.FDATE;
                model.Ficbolentry = sub.Ficbolentry;
                model.Ficbolno = sub.Ficbolno;
                model.FID = sub.FID;
                model.FITEMID = sub.FITEMID;
                model.Flevel = sub.Flevel;
                model.Fpobillentry = sub.Fpobillentry;
                model.FPObillno = sub.FPObillno;
                model.fprice = sub.fprice;
                model.FprojectNO = sub.FprojectNO;
                model.FREMARK = sub.FREMARK;
                model.FSRCCODE = sub.FSRCCODE;
                model.FSRCMODEL = sub.FSRCMODEL;
                model.FSRCNAME = sub.FSRCNAME;
                model.FstockNO = sub.FstockNO;
                model.FSYNCSTATUS = sub.FSYNCSTATUS;
                model.FTIMESTAMP = sub.FTIMESTAMP;
                model.Funit = sub.Funit;
                resultList.Add(model);
            }

            return resultList;
        }

        public int Remote_SetICPO_BOEntryStatus(string fbillno, string entryid)
        {
            FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();
            return api.SetICPO_BOEntryStatus(fbillno, entryid);
        }

    }
}