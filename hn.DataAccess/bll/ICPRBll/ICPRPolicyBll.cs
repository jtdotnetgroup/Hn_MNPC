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
    public class ICPRPOLICYBLL
    {
        public static ICPRPOLICYBLL Instance
        {
            get { return SingletonProvider<ICPRPOLICYBLL>.Instance; }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="IDList"></param>
        /// <returns></returns>
        public int BatchDelete(string IDList)
        {
            return ICPRPOLICYDAL.Instance.BatchDelete(IDList);
        }

        /// <summary>
        /// 保存请购计划价格政策
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Save(ICPRPOLICYMODEL model, string ICPRBILLID)
        {
            string result = null;

            if (model.FID.IsNullOrEmpty())
            {
                result = ICPRPOLICYDAL.Instance.InsertWithFID(model, ICPRBILLID) > 0 ? null : "保存请购计划价格政策失败！";
            }
            else
            {
                result = ICPRPOLICYDAL.Instance.Update(model) > 0 ? null : "保存请购计划价格政策失败！";
            }

            return result;
        }

        /// <summary>
        /// 删除请购计划下参考数据
        /// </summary>
        /// <param name="ICPRBILLID"></param>
        /// <returns></returns>
        public int DeleteByICPRBILLID(string ICPRBILLID)
        {
            return ICPRPOLICYDAL.Instance.DeleteWhere(new { FID = ICPRBILLID });
        }

        /// <summary>
        /// 添加请购计划价格政策
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ICPRBILLID"></param>
        /// <param name="FITEMID"></param>
        /// <param name="FENTRYID"></param>
        /// <returns></returns>
        public int Add(ICPRPOLICYMODEL model, string ICPRBILLID, string FITEMID, int FENTRYID)
        {
            model.FITEMID = FITEMID;
            model.FENTRYID = FENTRYID;

            return ICPRPOLICYDAL.Instance.InsertWithFID(model, ICPRBILLID);
        }

        public IEnumerable<string> GetFPOLICYIDListByUse(string FPOLICYIDList)
        {
            return ICPRPOLICYDAL.Instance.GetFPOLICYIDListByUse(FPOLICYIDList);
        }
    }

}