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
    /// OrderServer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class OrderServer : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 插入采购订单
        /// </summary>
        /// <param name="ICPOBILLJson">主表Json</param>
        /// ICPOBILLJson={"FTRANSTYPE":"0","FID":"","FBRANDID":"00","FBRANDNAME":"马可波罗","FCLIENTID":"aa4d7dad-4583-4330-b3ef-1e9dead53fb1","FCLIENTACCOUNT":"M4439","FDATE":"2018-09-14","FBILLNO":"PO201809009","FBILLERID":"admin","FTELEPHONE":"","FSTATUSNAME":"草稿","FREMARK":"ggggg"}
        /// <param name="ICPOBILLENTRYListJson">副表的Json</param>
        /// ICPOBILLENTRYListJson=[{"FENTRYID":1,"FMODEL":"165*165","FPLANID":"6bec5469-d30c-4554-8d4f-5ea2c2cb5f5a","FPRODUCTNAME":"亚光砖_FA1305","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010753","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":378,"FSRCQTY":378,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":2,"FMODEL":"165*165","FPLANID":"2894bf6d-e03e-4ee7-900d-706b6b88e258","FPRODUCTNAME":"亚光砖_FA1302","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010784","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":594,"FSRCQTY":594,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":3,"FMODEL":"600*600","FPLANID":"d64f1e3c-2f97-4364-b658-5d715df67dd7","FPRODUCTNAME":"亚光砖_S6052","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00019395","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":5280,"FSRCQTY":5280,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":4,"FMODEL":"600*600","FPLANID":"c32a511a-01bf-4142-9d66-69dcf25e61c3","FPRODUCTNAME":"亚光砖_CI6260","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010145","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":3336,"FSRCQTY":3336,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":5,"FMODEL":"300*450","FPLANID":"ff26b913-3075-4727-95d7-8424a8ecaf8c","FPRODUCTNAME":"亮光瓷片_M45088","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00020572","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":5780,"FSRCQTY":5780,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":6,"FMODEL":"165*165","FPLANID":"6d16973f-5c22-47f4-944f-624173967b61","FPRODUCTNAME":"亚光砖_FA1308","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010771","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":1350,"FSRCQTY":1350,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"},{"FENTRYID":7,"FMODEL":"165*165","FPLANID":"4cd282fe-576f-4b76-9c69-bd273f23011d","FPRODUCTNAME":"亚光砖_FA1301","FPRODUCTTYPE":"瓷砖","FPRODUCTCODE":"00010752","FUNITNAME":"片","FSTATUS":1,"FSTATUSNAME":"草稿","FSTATE":1,"FSTATENAME":"正常","FBATCHNO":null,"FCOLORNO":null,"FREMARK":null,"FPRICE":0,"FADVQTY":0,"FASKQTY":378,"FSRCQTY":378,"FSRCCOST":0,"FNEEDDATE":"2018-08-29 00:00:00"}]
        /// <returns></returns>
        public string SaveICPOBILL(ICPOBILLMODEL ICPOBILL, List<ICPOBILLENTRYMODEL> ICPOBILLENTRYList)
        {



            string result = ICPOBILLBLL.Instance.SaveClient(ICPOBILL, ICPOBILLENTRYList);

            if (result.IsNullOrEmpty())
            {
                return "保存完成！";
            }
            else
            {
                return result;
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

                hn.Common.LogHelper.WriteLog(query.ToString());

                return V_ICPOBILLDAL.Instance.GetWhereStr(query.ToString(), "FBILLDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
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
                    query.AppendFormat(" and to_char(FDATE,'yyyy/MM/dd')  >= '{0}' ", startdate);
                }

                if (!enddate.IsNullOrEmpty() && enddate != "0001/01/01")
                {
                    query.AppendFormat(" and to_char(FDATE,'yyyy/MM/dd') <= '{0}' ", enddate);
                }

                if (loginUser.IsAdmin != 1)
                {
                    //  query.AppendFormat("  AND FPREMISEID IN (SELECT FPREMISEID FROM TB_USERPREMISE WHERE FUSERID = '{0}') ", loginUser.FID);
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat("  AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
                }

                hn.Common.LogHelper.WriteLog(query.ToString());

                return ICPO_BOLentryDAL.Instance.GetWhereStr(query.ToString(), "FDATE DESC").ToList();
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
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
                hn.Common.LogHelper.WriteLog(ex);
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
                List<ICPOBILLMODEL> list = ICPOBILLDAL.Instance.GetWhere(new { FID = icprbillid }).ToList();
                if (list.Count > 0)
                    result = list[0];
            }
            catch (Exception ex)
            {
                hn.Common.LogHelper.WriteLog(ex);
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

                        hn.Common.LogHelper.WriteLog(JsonHelper.ToJson(uptModel));

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
                    //ConfirmSave(action, tempPRList, loginUser);
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
                hn.Common.LogHelper.WriteLog(ex);
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
                hn.Common.LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        public string Save_ICPREntry_List(ICPRBILLENTRYMODEL tModel)
        {
            int iEntryID = 1;
            if (tModel.FPLANID == "0")
            {
                int iCount = ICPRBILLENTRYDAL.Instance.CountWhere(new { FPLANID = "0", FITEMID = tModel.FITEMID });
                iEntryID = iEntryID + iCount;
                tModel.FENTRYID = iEntryID;
            }

            return ICPRBILLENTRYDAL.Instance.Insert(tModel);
        }

        public int Update_FSYNStatus(ICPOBILLMODEL billMode, int iStatus)
        {
            return ICPOBILLDAL.Instance.UpdateWhatWhere(new { FSYNCSTATUS = iStatus }, new { FID = billMode.FID });
        }

        public bool Remote_InsertICPOEntry(ICPOBILLMODEL billModel, ICPOBILLENTRYMODEL entryModel)
        {


            MApiAccess.AccessApi Mapi = new MApiAccess.AccessApi();
            if (string.IsNullOrEmpty(Global.g_token.tokenInfo.endDate) || DateTime.Parse(Global.g_token.tokenInfo.endDate) < DateTime.Now)
            {
                MApiModel.api1.Rootobject tempToken = new MApiModel.api1.Rootobject();
                tempToken.comid = 101;
                tempToken.action = "getToken";
                tempToken.khhm = "300384";
                tempToken.openkey = "10011630";
                Global.g_token = Mapi.AccessApi1(tempToken);
            }

            MApiModel.api3.Rootobject api3 = new MApiModel.api3.Rootobject();
            api3.action = "setMN_cp_24";
            api3.token = Global.g_token.token;

            List<MApiModel.api3.Datum> listSubItems = new List<MApiModel.api3.Datum>();


            MApiModel.api3.Datum subItem = new MApiModel.api3.Datum();
            subItem.sourceno = billModel.FBILLNO;
            subItem.rq = billModel.FDATESTR;
            subItem.comid = "101";
            //subItem.khhm = billModel.Fcompany;
            //subItem.khmc=
            //subItem.pjhm = "";
            subItem.zdr = billModel.FBILLER;
            //产品品种
            //subItem.cppz=billModel
            //产品规格
            //subItem.cpgg = "";
            //产品型号
            subItem.cpxh = "";
            subItem.cpdj = entryModel.Flevel;
            subItem.cpsh = entryModel.FCOLORNO;
            //产品仓号
            subItem.cpcm = entryModel.FstockNO;
            subItem.package = entryModel.FcontractNO;
            subItem.dw = entryModel.Funit;
            //包装片数
            //subItem.ks=entryModel.
            subItem.sl = int.Parse(entryModel.FSRCQTY.ToString());
            subItem.je = int.Parse(entryModel.Famount.ToString());

            listSubItems.Add(subItem);

            api3.data = listSubItems.ToArray();

            MApiModel.recApi3.Rootobject recapi3 = Mapi.AccessApi3(api3);

            if (recapi3.status == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public void Remote_GetICPOEntry(string strFID, string strEntryID, ref ICPOBILLMODEL billModel, ref ICPOBILLENTRYMODEL entryModel)
        {
            FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();
            FactoryService.ICPOBILLENTRYModel_MHLS tempModel = api.GetICPOEntry(strFID, strEntryID);
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


        public List<ICPO_BOLentryModel> Remote_GetICPO_BOEntry(string fbillno, string entryid)
        {
            List<ICPO_BOLentryModel> resultList = new List<ICPO_BOLentryModel>();

            FactoryService.APIServiceClient api = new FactoryService.APIServiceClient();
            List<FactoryService.ICPO_BOLentryModel_MNLS> tempModelList = api.GetICPO_BOEntry(fbillno, entryid).ToList();
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
