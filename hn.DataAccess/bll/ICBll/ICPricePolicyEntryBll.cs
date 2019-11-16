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
    public class ICPRICEPOLICYENTRYBLL
    {
        public static ICPRICEPOLICYENTRYBLL Instance
        {
            get { return SingletonProvider<ICPRICEPOLICYENTRYBLL>.Instance; }
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string policyID = null, string startDate = null, string endDate = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            return ICPRICEPOLICYENTRYDAL.Instance.GetJson(pageindex, pagesize, policyID, startDate, endDate, sort, order);
        }

        public string Save(ICPRICEPOLICYENTRYMODEL model, string FPolicyID = null)
        {
            string FID = null;

            if (!model.FID.IsNullOrEmpty())
            {
                //更新
                return ICPRICEPOLICYENTRYDAL.Instance.UpdateWhatWhere(new
                {
                    //FUNITID = model.FUNITID,
                    FBATCHNO = model.FBATCHNO,
                    FCOLORNO = model.FCOLORNO,
                    //FPRICETYPE = model.FPRICETYPE,
                    FQTYLIMIT = model.FQTYLIMIT,
                    //FENDQTY = model.FENDQTY,
                    //FWHOLESALEPRICE = model.FWHOLESALEPRICE,
                    //FAJUST = model.FAJUST,
                    //FADD = model.FADD,
                    //FAREAPRICE = model.FAREAPRICE,
                    //FBEGDATE = model.FBEGDATE,
                    //FENDDATE = model.FENDDATE,
                    //FPROJECTID = model.FPROJECTID,
                    FREMARK = model.FREMARK,
                }, new
                {
                    FID = model.FID
                }) > 0 ? model.FID : "保存价格政策失败！";
            }
            else
            {
                model.FPOLICYID = FPolicyID;
                //插入
                FID = ICPRICEPOLICYENTRYDAL.Instance.Insert(model);
                return FID.IsGuid() ? FID : "保存价格政策失败！";
            }
        }

        public IEnumerable<ICPRICEPOLICYENTRYMODEL> GetByIDList(IEnumerable<string> IDList)
        {
            return ICPRICEPOLICYENTRYDAL.Instance.GetByIDList(IDList);
        }

        public int BatchDelete(string IDList)
        {
            return ICPRICEPOLICYENTRYDAL.Instance.BatchDelete(IDList);
        }

        /// <summary>
        /// 结果负数表示价格政策明细被占用了
        /// </summary>
        /// <param name="POLICYID"></param>
        /// <returns></returns>
        public int DeleteByPOLICYID(string POLICYID)
        {
            if(CheckUseByPOLICYID(POLICYID))
            {
                return -1;
            }

            return ICPRICEPOLICYENTRYDAL.Instance.DeleteWhere(new
            {
                FPOLICYID = POLICYID
            });
        }

        /// <summary>
        /// 检测价格正常明细被使用数量
        /// </summary>
        /// <returns></returns>
        public bool CheckUseByPOLICYID(string POLICYID)
        {
            return ICPRICEPOLICYENTRYDAL.Instance.CountIsUseByPOLICYID(POLICYID) > 0;
        }

        /// <summary>
        /// 获取指定商品价格政策明细
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICPRICEPOLICYENTRYMODEL> GetByProduct(string FORGID, string PRODUCTIDLIST)
        {
            return ICPRICEPOLICYENTRYDAL.Instance.GetByProductWithFORGID(FORGID, PRODUCTIDLIST);
        }
    }

}