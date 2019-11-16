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
    public class ICPO_BOLentryBLL
    {
        public static ICPO_BOLentryBLL Instance
        {
            get { return SingletonProvider<ICPO_BOLentryBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int rows = 15, string FOrgID = null, string FBrandID = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            return ICPOBILLDAL.Instance.GetJson(page, rows, FOrgID, FBrandID, keywords, sort, order);
        }

        public string Save(ICPO_BOLentryModel ICPOBILL)
        {
            string FID = null;

            #region 执行前检测
            if (!ICPOBILL.FID.IsGuid())
            {
                ICPO_BOLentryModel temp = ICPO_BOLentryDAL.Instance.GetWhere(new { Ficbolno = ICPOBILL.Ficbolno }).FirstOrDefault();

                if (temp != null && temp.FID != ICPOBILL.FID)
                {
                    return "提货单号重复！";
                }
            }

            //foreach (var item in ICPOBILLENTRYList.GroupBy(i => i.FITEMID + i.FENTRYID))
            //{
            //    if (item.Count() > 1)
            //    {
            //        return "采购订单明细商品信息重复！";
            //    }
            //}

            //foreach (var item in ICPOPOLICYList.GroupBy(i => i.FITEMID + i.FSRCENTRYID))
            //{
            //    if (item.Count() > 1)
            //    {
            //        return "采购订单价格政策商品信息重复！";
            //    }
            //}

            #endregion

            #region 保存主表

            if (!ICPOBILL.FID.IsGuid())
            {
              
                FID = ICPO_BOLentryDAL.Instance.Insert(ICPOBILL);

                if (!FID.IsGuid())
                {
                    return "保存提货单失败！";
                }
            }
            else
            {
                FID = ICPOBILL.FID;

                var model =  ICPOBILLDAL.Instance.Get(FID);              
                ICPOBILLDAL.Instance.Update(model);
              
            }

            #endregion

        
            return null;
        }


  

        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <returns></returns>
        public int GetStatus(string FID)
        {
            return ICPO_BOLentryDAL.Instance.GetStatus(FID);
        }
    }
}