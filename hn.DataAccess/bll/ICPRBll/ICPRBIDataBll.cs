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
    public class ICPRBIDATABLL
    {
        public static ICPRBIDATABLL Instance
        {
            get { return SingletonProvider<ICPRBIDATABLL>.Instance; }
        }

        /// <summary>
        /// 保存价格政策参考数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Save(ICPRBIDATAMODEL model,string ICPRBILLID)
        {
            string result = null;

            if(model.FID.IsNullOrEmpty())
            {
                result= ICPRBIDATADAL.Instance.InsertWithFID(model,model.FID) > 0 ? null : "保存价格政策参考数据失败！";
            }
            else
            {
                result = ICPRBIDATADAL.Instance.Update(model) > 0 ? null : "保存价格政策参考数据失败！";
            }

            return result;
        }

        /// <summary>
        /// 添加请购计划参考数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ICPRBILLID"></param>
        /// <param name="FITEMID"></param>
        /// <param name="FENTRYID"></param>
        /// <returns></returns>
        public int Add(ICPRBIDATAMODEL model, string ICPRBILLID, string FITEMID, int FENTRYID)
        {
            model.FITEMID = FITEMID;
            model.FENTRYID = FENTRYID;

            return ICPRBIDATADAL.Instance.InsertWithFID(model, ICPRBILLID);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="IDList"></param>
        /// <returns></returns>
        public int BatchDelete(string IDList)
        {
            return ICPRBIDATADAL.Instance.BatchDelete(IDList);
        }

        /// <summary>
        /// 删除请购计划下参考数据
        /// </summary>
        /// <param name="ICPRBILLID"></param>
        /// <returns></returns>
        public int DeleteByICPRBILLID(string ICPRBILLID)
        {
            return ICPRBIDATADAL.Instance.DeleteWhere(new { FID = ICPRBILLID });
        }
    }

}