using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.Filter;
using hn.Common.Provider;
using hn.Core.Dal;
using hn.Core.Model;
using System.Data.SqlClient;
using hn.Common.Data.SqlServer;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Bll
{
    public class ICSEOUTBILLBLL
    {
        public static ICSEOUTBILLBLL Instance
        {
            get { return SingletonProvider<ICSEOUTBILLBLL>.Instance; }
        }


        public string GetEasyUIJson(int page = 1, int pageSize = 15, string startDate = null, string endDate = null, string FOrgID = null, int status = 0, string sort = "FID", string order = "asc")
        {
            return ICSEOUTBILLDAL.Instance.GetJson(page, pageSize, startDate, endDate, FOrgID, status, sort, order);
        }


        public string Save(ICSEOUTBILLMODEL ICSEOUTBILL, IEnumerable<ICSEOUTBILLENTRYMODEL> ICSEOUTBILLENTRYList)
        {
            #region 检查
            ICSEOUTBILLMODEL temp = ICSEOUTBILLDAL.Instance.GetWhere(new { FBILLNO = ICSEOUTBILL.FBILLNO }).FirstOrDefault();

            if (temp != null && temp.FID != ICSEOUTBILL.FID)
            {
                return "单据单号重复！";
            }


            //foreach (var item in ICSEOUTBILLENTRYList.GroupBy(i => i.FITEMID + i.FENTRYID))
            //{
            //    if (item.Count() > 1)
            //    {
            //        return "商品资料重复！";

            //    }
            //}

            #endregion

            string FID = ICSEOUTBILL.FID;

            ICSEOUTBILL.FSTATUS = Constant.BILL_FSTATUS.草稿.ToInt();

            if (FID.IsNullOrEmpty())
            {
                if (ICSEOUTBILL.FBILLERID == null)
                {
                    ICSEOUTBILL.FBILLERID = SysVisitor.Instance.UserId;
                }
             
                ICSEOUTBILL.FBILLDATE = DateTime.Now;
                ICSEOUTBILL.FCAR_STATUS = 1; // 1:待发布，2:已发布，3:发布失败，4:已确认，5:关闭，6:关闭（改）

                FID = ICSEOUTBILLDAL.Instance.Insert(ICSEOUTBILL);

                if (!FID.IsGuid())
                {
                    return "发货通知保存失败！";
                }
            }
            else
            {
                int status = ICSEOUTBILLDAL.Instance.GetStatus(ICSEOUTBILL.FID);

                if (status == Constant.BILL_FSTATUS.审核通过.ToInt())
                {
                    return "该发货通知已经审核，不允许编辑！";
                }

                if (status == Constant.BILL_FSTATUS.完成.ToInt())
                {
                    return "该发货通知已经完成，不允许编辑！";
                }

                if (status == Constant.BILL_FSTATUS.关闭.ToInt())
                {
                    return "该发货通知已经关闭，不允许编辑！";
                }

                //int result = ICSEOUTBILLDAL.Instance.Update(ICSEOUTBILL);

                int result = ICSEOUTBILLDAL.Instance.UpdateWhatWhere(new
                {
                    FPREMISEID = ICSEOUTBILL.FPREMISEID,
                    FBRANDID = ICSEOUTBILL.FBRANDID,
                    FCLIENTID = ICSEOUTBILL.FCLIENTID,
                    FBILLNO = ICSEOUTBILL.FBILLNO,
                    FSTATUS = Constant.BILL_FSTATUS.草稿.ToInt(),
                    FCARNUMBER = ICSEOUTBILL.FCARNUMBER,
                    FLOADCAPACITY = ICSEOUTBILL.FLOADCAPACITY,
                    FDELIVERER = ICSEOUTBILL.FDELIVERER,
                    FDELIVERERTEL = ICSEOUTBILL.FDELIVERERTEL,
                    FDELIVERERIDNO = ICSEOUTBILL.FDELIVERERIDNO,
                    FDELIVERERADDR = ICSEOUTBILL.FDELIVERERADDR,
                    FRECEIVER = ICSEOUTBILL.FRECEIVER,
                    FRECEIVERTEL = ICSEOUTBILL.FRECEIVERTEL,
                    FRECEIVERADDR = ICSEOUTBILL.FRECEIVERADDR,
                    FALLWEIGHT = ICSEOUTBILL.FALLWEIGHT,
                    FALLVOLUME = ICSEOUTBILL.FALLVOLUME,
                    FREMARK = ICSEOUTBILL.FREMARK,
                    FTRANSTYPE = ICSEOUTBILL.FTRANSTYPE,
                    FTRANSID = ICSEOUTBILL.FTRANSID,
                    FDELIVERDATE = ICSEOUTBILL.FDELIVERDATE,
                    FEXPRESSCOMPANYID = ICSEOUTBILL.FEXPRESSCOMPANYID,
                    FPROJECTNAME = ICSEOUTBILL.FPROJECTNAME,
                    FCENTER_WAREHOUSE = ICSEOUTBILL.FCENTER_WAREHOUSE,
                    FDELIVERY_METHOD = ICSEOUTBILL.FDELIVERY_METHOD ,
                    FPURCHASE_NO = ICSEOUTBILL.FPURCHASE_NO,
                    FPLANDESC = ICSEOUTBILL.FPLANDESC,
                    FIS_CONSIGN = ICSEOUTBILL.FIS_CONSIGN,
                    FBILLING_TYPE = ICSEOUTBILL.FBILLING_TYPE,
                    FSETTLE_ORG = ICSEOUTBILL.FSETTLE_ORG,
                    FDELIVERY_REQUIRE = ICSEOUTBILL.FDELIVERY_REQUIRE,
                    FBRAND_DEPART = ICSEOUTBILL.FBRAND_DEPART,
                }, new
                {
                    FID = ICSEOUTBILL.FID
                });

                if (result <= 0)
                {
                    return "发货通知保存失败！";
                }
            }


            var entrys = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICSEOUTID  = FID}).ToList();
            //删除明细
            ICSEOUTBILLENTRYBLL.Instance.DeleteByICSEOUTBILLID(FID);

            foreach (var model in entrys)
            {
                var count = ICSEOUTBILLENTRYList.Where(m => m.FICPRID == model.FICPRID).Count();
                if (count == 0)
                {
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
            }

           

            foreach (var item in ICSEOUTBILLENTRYList)
            {
                item.FICSEOUTID = FID;
                string id = ICSEOUTBILLENTRYDAL.Instance.Insert(item);
                LogHelper.WriteLog("herherehrer:"+item.thdbm);

                //var list = ICSEOUTBILLENTRYDAL.Instance.GetWhere(new { FICPRID = item.FICPRID });

                string sql = string.Format("SELECT SUM(FCOMMITQTY*FRATE) FROM V_ICSEOUTBILLENTRY WHERE FICPRID='{0}' and (FGROUP_STATUS is null or FGROUP_STATUS = 0)", item.FICPRID);
                DataTable table = DbUtils.Query(sql);
                decimal total =  PublicMethod.GetDecimal(table.Rows[0][0]);
                ICPRBILLENTRYMODEL icprModel = ICPRBILLENTRYDAL.Instance.Get(item.FICPRID);
                if (icprModel != null)
                {
                    icprModel.FLEFTAMOUNT = icprModel.FASKQTY - total;

                    if (icprModel.FLEFTAMOUNT <=0)
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



                //ICPRBILLENTRYMODEL icprModel = ICPRBILLENTRYDAL.Instance.Get(item.FICPRID);
                //if (icprModel != null)
                //{
                //    if (icprModel.FORDERUNITQTY >= total)
                //    {
                //        icprModel.FLEFTAMOUNT = icprModel.FLEFTAMOUNT - total;
                //    }
                //    else
                //    {
                //        icprModel.FLEFTAMOUNT = 0;
                //    }

                //    ICPRBILLENTRYDAL.Instance.Update(icprModel);
                //}
            }

            return null;
        }
    }
}