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
    public class ICPRICEPOLICYBLL
    {
        public static ICPRICEPOLICYBLL Instance
        {
            get { return SingletonProvider<ICPRICEPOLICYBLL>.Instance; }
        }

        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        //{
        //    return ICPricePolicyDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        //}

        public string GetEasyUIJson(int page = 1, int rows = 15, string FOrgID = null, string FBrandID = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            return ICPRICEPOLICYDAL.Instance.GetJson(page, rows, FOrgID, FBrandID, keywords, sort, order);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        /// <param name="deleteEntryList"></param>
        /// <returns></returns>
        public object Save(ICPRICEPOLICYMODEL model, IEnumerable<ICPRICEPOLICYENTRYMODEL> list, string deleteEntryList)
        {
            string FID = null;

            #region 有效性检测

            //检测价格政策编号是否重复！
            ICPRICEPOLICYMODEL temp = ICPRICEPOLICYDAL.Instance.GetWhere(new
            {
                FBILLNO = model.FBILLNO
            }).FirstOrDefault();

            if (temp != null && temp.FID != model.FID)
            {
                return new { Status = -1, Message = "价格政策的编号重复！" };
            }

            //同一个品牌、厂家账户下不允许存在相同的优先级
            var listTemp = ICPRICEPOLICYDAL.Instance.GetWhere(new
            {
                FBRANDID = model.FBRANDID,
                FCLIENTID = model.FCLIENTID,
                FPRIORITY = model.FPRIORITY
            }).ToList();
            foreach (var item in listTemp)
            {
                if (item != null && item.FID != model.FID)
                {
                    return new { Status = -1, Message = "同一个品牌、厂家账户下不允许存在相同的优先级！" };
                }
            }

            //检测价格政策明细商品是否重复：同一个品牌 + 厂家账号 + 优先级的价格政策下，不允许存在重复商品
            foreach (var item in list.GroupBy(l => new { id = l.FITEMID }))
            {
                if (item.Count() > 1)
                {
                    return new { Status = -1, Message = "价格政策明细的商品重复！" };
                }
            }

            //日期
            if (model.FBEGDATE>model.FENDDATE)
            {
                return new { Status = -1, Message = "起始日期不能大于截止日期！" };
            }


            //检测要删除价格政策明细是否在使用
            //请购计划价格明细
            var ICPRPOLICYLIST = ICPRPOLICYBLL.Instance.GetFPOLICYIDListByUse(deleteEntryList).ToList();

            //采购订单价格明细
            //var ICPOBILLENTRYLIST = ICPOBILLENTRYBLL.Instance.GetFPOLICYIDListByUse(deleteEntryList).ToList();

            if (ICPRPOLICYLIST.Count > 0)
            {
                return new { Status = -2, Message = string.Join(",", ICPRPOLICYLIST.Select(l => l)) };
            }

            //采购订单价格明细
            //var ICPOBILLENTRYLIST = ICPOBILLENTRYBLL.Instance.GetFPOLICYIDListByUse(deleteEntryList).ToList();

            //if (ICPRPOLICYLIST.Count > 0)
            //{
            //    return "";
            //}


            #endregion

            //首先保存价格政策
            if (model.FID.IsNullOrEmpty())
            {
                model.FBILLDATE = DateTime.Now;
                model.FCHECKSTATUS = Constant.ICPricePolicyStatus.待审核.ToInt();

                var dicModel = SYS_SUBDICSDAL.Instance.Get(model.FPOLICYTYPE);
                if (dicModel != null)
                {
                    model.FPRIORITY = string.Format("{0}{1}", dicModel.FNUMBER, DateTime.Now.ToString("yyyyMMdd")).ToDecimal();
                }

                FID = ICPRICEPOLICYDAL.Instance.Insert(model);

                if (!FID.IsGuid())
                {
                    return new { Status = -1, Message = "保存价格政策失败！" };
                }
            }
            else
            {
                int result = ICPRICEPOLICYDAL.Instance.UpdateWhatWhere(new
                {
                    FBRANDID = model.FBRANDID,
                    FCLIENTID = model.FCLIENTID,
                    FCLIENTACCOUNT = model.FCLIENTACCOUNT,
                    FBILLNO = model.FBILLNO,
                    FNAME = model.FNAME,
                    FPOLICYTYPE = model.FPOLICYTYPE,
                    //FPRIORITY = model.FPRIORITY,
                    FPROJECTID = model.FPROJECTID,
                    FBEGDATE = model.FBEGDATE,
                    FENDDATE = model.FENDDATE,
                    //FSTATUS = Constant.ICPricePolicyStatus.草稿.ToInt(),
                    FREMARK = model.FREMARK
                }, new
                {
                    FID = model.FID,
                });

                if (result > 0)
                {
                    FID = model.FID;
                }
                else
                {
                    return new { Status = -1, Message = "保存价格政策失败！" };
                }
            }

            //其次保存价格政策明细
            foreach (var item in list)
            {
                item.FQTYREST = item.FQTYLIMIT;
                if (!ICPRICEPOLICYENTRYBLL.Instance.Save(item, FID).IsGuid())
                {
                    return new { Status = -1, Message = "保存价格政策明细失败！" };
                }
            }

            //最后删除需要删除的价格政策明细
            ICPRICEPOLICYENTRYBLL.Instance.BatchDelete(deleteEntryList);

            return new { Status = 0, Message = "保存成功！" }; ;
        }

        public string Delete(string ID)
        {
            //删除价格政策明细
            if (ICPRICEPOLICYENTRYBLL.Instance.DeleteByPOLICYID(ID) < 0)
            {
                return "价格政策已经被使用，不能删除！";
            }

            //删除价格政策
            int result = ICPRICEPOLICYDAL.Instance.Delete(ID);
            return null;
        }
    }

}