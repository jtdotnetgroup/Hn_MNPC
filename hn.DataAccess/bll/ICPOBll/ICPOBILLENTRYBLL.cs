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
    public class ICPOBILLENTRYBLL
    {
        public static ICPOBILLENTRYBLL Instance
        {
            get { return SingletonProvider<ICPOBILLENTRYBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int rows = 15, string FOrgID = null, string FBrandID = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            return ICPOBILLENTRYDAL.Instance.GetJson(page, rows, FOrgID, FBrandID, keywords, sort, order);
        }

        /// <summary>
        /// 删除采购订单下明细
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        public int DeleteByICPOBILLID(string FID)
        {
            return ICPOBILLENTRYDAL.Instance.DeleteWhere(new { FICPOBILLID = FID });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Add(ICPOBILLENTRYMODEL model, string ICPOBILLID)
        {
            return ICPOBILLENTRYDAL.Instance.InsertWithFID(model, ICPOBILLID);
        }


        public string Audit(string FID, string FENTRYID, string FITEMID, decimal FAUDQTY, decimal FAUDAMOUNT, bool isPass)
        {
            if (ICPOBILLBLL.Instance.GetStatus(FID) == Constant.BILL_FSTATUS.关闭.ToInt())
            {
                return "采购订单已被关闭！";
            }

            if (isPass)
            {
                if (FAUDQTY <= 0)
                {
                    return "审批数量错误！";
                }

                if (FAUDAMOUNT <= 0)
                {
                    return "审批金额错误！";
                }
            }
            else
            {
                FAUDQTY = 0;
                FAUDAMOUNT = 0;
            }

            return ICPOBILLENTRYDAL.Instance.UpdateWhatWhere(new
            {
                FSTATUS = isPass ? Constant.BILL_FSTATUS.审核通过.ToInt() : Constant.BILL_FSTATUS.审核不通过.ToInt(),
                FAUDQTY = FAUDQTY,
                FAUDAMOUNT = FAUDAMOUNT,
            }, new
            {
                FID = FID,
                FITEMID = FITEMID,
                FENTRYID = FENTRYID,
                FSTATUS = Constant.BILL_FSTATUS.草稿.ToInt(),
            }) > 0 ? null : "保存审核信息失败！";
        }
    }
}