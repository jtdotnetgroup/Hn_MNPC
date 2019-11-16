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
    public class ICPRBILLBLL
    {
        public static ICPRBILLBLL Instance
        {
            get { return SingletonProvider<ICPRBILLBLL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return ICPRBILLDAL.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15, string startDate = null, string endDate = null, string FOrgID = null, int FStatus = 0, bool approved = false, string sort = "FID", string order = "asc")
        {
            return ICPRBILLDAL.Instance.GetEasyUIJson(page, pageSize, startDate, endDate, FOrgID, FStatus, approved, sort, order);
        }

        /// <summary>
        /// 保存请购计划，同时更新子表明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ICPRBillEntryList"></param>
        /// <param name="ICRPBIDataList"></param>
        /// <param name="ICPRPolicyList"></param>
        /// <param name="deleteICPRBillEntryIDList"></param>
        /// <param name="deleteICRPBIDataIDList"></param>
        /// <param name="deleteICPRPolicyIDList"></param>
        /// <returns></returns>
        public string Save(ICPRBILLMODEL model, List<ICPRBillDataModel> dataList, List<TB_ATTACHMENTModel> attaList)
        {
            foreach (var item in dataList.GroupBy(d => d.FITEMID.Trim() + d.FENTRYID))
            {
                if (item.Count() > 1)
                {
                    return "产生重复数据，请检查！";
                }
            }

            string FID = null;
            string result = null;

            //编辑日期
            model.FBILLDATE = model.FDATE = DateTime.Now;
            //默认状态草稿
            model.FSTATUS = Constant.BILL_FSTATUS.草稿.ToInt();

            ICPRBILLMODEL temp = ICPRBILLDAL.Instance.GetWhere(new { FBILLNO = model.FBILLNO }).FirstOrDefault();

            if (temp != null && temp.FID != model.FID)
            {
                return "请购计划编号重复！";
            }

            //先添加请购计划
            if (model.FID.IsNullOrEmpty())
            {
                #region 新增

                //编辑人
                model.FBILLERID = SysVisitor.Instance.UserId;

                FID = ICPRBILLDAL.Instance.Insert(model);

                if (!FID.IsGuid())
                {
                    result = "保存请购计划失败！";
                }

                #endregion
            }
            else
            {
                FID = model.FID;
                #region 更新

                result = ICPRBILLDAL.Instance.UpdateWhatWhere(new
                {
                    FTRANSID = model.FTRANSID,
                    FTYPEID = model.FTYPEID,
                    FPREMISEID = model.FPREMISEID,
                    FBRANDID = model.FBRANDID,
                    FBILLNO = model.FBILLNO,
                    FTRANSTYPE = model.FTRANSTYPE,
                    FENGINEERINGID = model.FENGINEERINGID,
                    FRECEIVINGADDR = model.FRECEIVINGADDR,
                    FFREIGHTID = model.FFREIGHTID,
                    FTELEPHONE = model.FTELEPHONE,
                    SIGN_MAIN = model.SIGN_MAIN,
                    JDE = model.JDE,
                    CRMNO = model.CRMNO,
                    FREMARK = model.FREMARK,
                    FPROJECTNAME = model.FPROJECTNAME,
                    FPURCHASE_NO = model.FPURCHASE_NO,
                    FSETTLE_ORG = model.FSETTLE_ORG,
                    FDELIVERYADDR = model.FDELIVERYADDR,
                    FWEIGHT = model.FWEIGHT,
                    FPROVINCEID = model.FPROVINCEID,
                    FCITYID = model.FCITYID,
                    FDISTRICTID = model.FDISTRICTID,
                    FCONSIGNEE = model.FCONSIGNEE,
                    FCONSIGNEE_TEL = model.FCONSIGNEE_TEL
                }, new
                {
                    FID = model.FID,
                    FSTATUS = Constant.BILL_FSTATUS.草稿.ToInt()
                }) > 0 ? null : "保存请购计划失败，计划可能已经提交审核了！";

                #endregion
            }

            //是否保存失败！
            if (!result.IsNullOrEmpty())
            {
                return result;
            }


            #region 删除子表明细假设是更新的话

            if (!model.FID.IsNullOrEmpty())
            {
                //删除请购计划明细
                ICPRBILLENTRYBLL.Instance.DeleteByICPRBILLID(FID);

                //删除请购计划附件
                ICPRBILLENTRYBLL.Instance.DeleteByATTACHMENT(FID);

                //删除请购计划参考数据
                //ICPRBIDATABLL.Instance.DeleteByICPRBILLID(FID);

                ////删除请购计划价格政策
                //ICPRPOLICYBLL.Instance.DeleteByICPRBILLID(FID);
            }

            #endregion

            #region 更新子表明细

            foreach (var item in dataList)
            {
                //更新请购计划明细
                ICPRBILLENTRYBLL.Instance.Add(item.ICPRBillEntry, FID);

                //更新请购计划参考数据
                //ICPRBIDATABLL.Instance.Add(item.ICRPBIData, FID, item.FITEMID, item.FENTRYID);

                //更新请购计划价格政策
                //ICPRPOLICYBLL.Instance.Add(item.ICPRPolicy, FID, item.FITEMID, item.FENTRYID);
            }

            #endregion


            foreach (var item in attaList)
            {
                item.FBILLID = FID;
                item.FADD_USER = SysVisitor.Instance.UserId;
                item.FADD_TIME = DateTime.Now;
                item.FTYPE = 1;
                TB_ATTACHMENTDal.Instance.Insert(item);
            }

            return result;
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <returns></returns>
        public string SubmitAudit(string FID)
        {
            return ICPRBILLDAL.Instance.UpdateWhatWhere(new
            {
                FDATE = DateTime.Now,
                FSTATUS = Constant.BILL_FSTATUS.待审核.ToInt(),
            }, new
            {
                FID = FID,
                FSTATUS = Constant.BILL_FSTATUS.草稿.ToInt(),
            }) > 0 ? null : "提交失败，只有草稿装填才能提交！";
        }

        /// <summary>
        /// 审核请购计划
        /// </summary>
        /// <returns></returns>
        public string Audit(string FID, bool isPass)
        {
            return ICPRBILLDAL.Instance.UpdateWhatWhere(new
            {
                FCHECKERID = SysVisitor.Instance.UserId,
                FTELEPHONE = SysVisitor.Instance.MOBILE,
                FSTATUS = isPass ? Constant.BILL_FSTATUS.审核通过.ToInt() : Constant.BILL_FSTATUS.审核不通过.ToInt(),
                FCHECKDATE = DateTime.Now,
            }, new
            {
                FID = FID,
                FSTATUS = Constant.BILL_FSTATUS.待审核.ToInt(),
            }) > 0 ? null : "该请购计划已经审核过了！";
        }

        /// <summary>
        /// 关闭请购计划
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public int Closs(string FID)
        {
            return ICPRBILLDAL.Instance.UpdateWhatWhere(new
            {
                FCHECKERID = SysVisitor.Instance.UserId,
                FTELEPHONE = SysVisitor.Instance.MOBILE,
                FSTATUS = Constant.BILL_FSTATUS.关闭.ToInt(),
                FCHECKDATE = DateTime.Now,
            }, new { FID = FID });
        }

        public int GetStatus(string FID)
        {
            return ICPRBILLDAL.Instance.GetStatus(FID);
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="FID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int SetStatus(string FID, Constant.BILL_FSTATUS status)
        {
            return ICPRBILLDAL.Instance.UpdateWhatWhere(new
            {
                FSTATUS = status.ToInt()
            }, new
            {
                FID = FID
            });
        }
    }
}