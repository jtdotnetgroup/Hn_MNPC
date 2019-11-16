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
    public class ICPRBILLENTRYBLL
    {
        public static ICPRBILLENTRYBLL Instance
        {
            get { return SingletonProvider<ICPRBILLENTRYBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15, string ICPRBILLID = null, string sort = "FID", string order = "asc")
        {
            return ICPRBILLENTRYDAL.Instance.GetJson(page, pageSize, ICPRBILLID, sort, order);
        }


        /// <summary>
        /// 保存请购计划明细
        /// </summary>
        /// <returns></returns>
        public string Save(ICPRBILLENTRYMODEL model, string ICPRBILLID)
        {
            string result = null;

            if (model.FID.IsNullOrEmpty())
            {
                result = ICPRBILLENTRYDAL.Instance.InsertWithFID(model, ICPRBILLID) > 0 ? null : "保存请购计划明细失败！";
            }
            else
            {
                result = ICPRBILLENTRYDAL.Instance.Update(model) > 0 ? null : "保存请购计划明细失败！";
            }

            return result;
        }

        /// <summary>
        /// 添加请购计划明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ICPRBILLID"></param>
        /// <param name="FITEMID"></param>
        /// <param name="FENTRYID"></param>
        /// <returns></returns>
        public int Add(ICPRBILLENTRYMODEL model, string ICPRBILLID)
        {
            model.FPLANID = ICPRBILLID;
            model.FSTATUS = 1;

            return ICPRBILLENTRYDAL.Instance.Insert(model).IsGuid() ? 1 : 0;
        }

        public int BatchDelete(string IDList)
        {
            return ICPRBILLENTRYDAL.Instance.BatchDelete(IDList);
        }

        /// <summary>
        /// 删除请购计划下明细
        /// </summary>
        /// <param name="ICPRBILLID"></param>
        /// <returns></returns>
        public int DeleteByICPRBILLID(string ICPRBILLID)
        {
            return ICPRBILLENTRYDAL.Instance.DeleteWhere(new { FPLANID = ICPRBILLID });
        }


        /// <summary>
        /// 删除请购计划下附件
        /// </summary>
        /// <param name="ICPRBILLID"></param>
        /// <returns></returns>
        public int DeleteByATTACHMENT(string ICPRBILLID)
        {
            return TB_ATTACHMENTDal.Instance.DeleteWhere(new { FBILLID = ICPRBILLID });
        }

        /// <summary>
        /// 审核请购计划明细，主表状态修改
        /// </summary>
        /// <param name="FID"></param>
        /// <param name="FITEMID"></param>
        /// <param name="FENTRYID"></param>
        /// <returns></returns>
        public string Audit(string FID, string FITEMID, string FENTRYID, bool isPass, decimal FAUDQTY, decimal FAUDAMOUNT)
        {
            #region 审核条件检测

            int status = ICPRBILLBLL.Instance.GetStatus(FID);

            if (status == Constant.BILL_FSTATUS.草稿.ToInt())
            {
                return "该请购计划未提交审核，不能审核！";
            }

            if (status == Constant.BILL_FSTATUS.关闭.ToInt())
            {
                return "该请购计划已关闭，不能继续审核！";
            }

            if (isPass)
            {
                if (FAUDQTY <= 0)
                {
                    return "审核数量错误！";
                }

                if (FAUDAMOUNT <= 0)
                {
                    return "审核金额错误！";
                }
            }
            else
            {
                FAUDQTY = 0;
                FAUDAMOUNT = 0;
            }

            #endregion

            ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new
            {
                FSTATUS = isPass ? Constant.BILL_FSTATUS.审核通过.ToInt() : Constant.BILL_FSTATUS.审核不通过.ToInt(),
                FAUDQTY = FAUDQTY,
                FAUDAMOUNT = FAUDAMOUNT,
            }, new
            {
                FID = FID,
                FITEMID = FITEMID,
                FENTRYID = FENTRYID,
            });

            IEnumerable<int> statusList = ICPRBILLENTRYDAL.Instance.GetStatus().Select(i => i.ToInt());

            if (statusList.Where(s => s == Constant.BILL_FSTATUS.审核通过.ToInt()).Count() == statusList.Count())
            {
                //全审核通过
                ICPRBILLBLL.Instance.SetStatus(FID, Constant.BILL_FSTATUS.审核通过);
            }
            else if (statusList.Where(s => s == Constant.BILL_FSTATUS.审核不通过.ToInt()).Count() == statusList.Count())
            {
                //全审核不通过
                ICPRBILLBLL.Instance.SetStatus(FID, Constant.BILL_FSTATUS.审核不通过);
            }

            return null;
        }
    }
}