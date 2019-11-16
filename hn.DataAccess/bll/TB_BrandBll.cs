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
    public class TB_BrandBll
    {

        public static TB_BrandBll Instance
        {
            get { return SingletonProvider<TB_BrandBll>.Instance; }
        }

        public int Update(TB_BrandModel model)
        {
            return TB_BrandDal.Instance.Update(model);
        }

        public int Delete(string FID)
        {
            return TB_BrandDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string keywords, string sort = "FID", string order = "asc")
        {
            return TB_BrandDal.Instance.GetJson(pageindex, pagesize, keywords, sort, order);
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string sort = "FID", string order = "asc")
        {
            return TB_BrandDal.Instance.GetEasyUIJson(pageindex, pagesize, sort, order);
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="Brandname">用户名</param>
        /// <param name="BrandId">用户Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasBrandNo(string name, string id = "0")
        {
            var Brands = from n in TB_BrandDal.Instance.GetAll()
                              where n.FNAME == name && n.FID != id
                              select n;
            return Brands.Any();
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="number">代码</param>
        /// <param name="BrandId">用户Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasBrandNumber(string number, string id = "0")
        {
            var Brands = from n in TB_BrandDal.Instance.GetAll()
                         where n.FNUMBER == number && n.FID != id
                         select n;
            return Brands.Any();
        }

        public string AddBrand(TB_BrandModel u)
        {
            string uid = "0";
            string msg = "厂家品牌添加失败！";
            if (HasBrandNo(u.FNAME, u.FID))
            {
                uid = "";
                msg = "厂家品牌已存在。";
            }
            else
            {
                if (HasBrandNumber(u.FNUMBER, u.FID))
                {
                    uid = "";
                    msg = "厂家品牌代码已存在。";
                }
                else
                {
                    uid = TB_BrandDal.Instance.Insert(u);
                    if (uid != "")
                    {
                        msg = "添加厂家品牌成功！";
                        LogBll<TB_BrandModel> log = new LogBll<TB_BrandModel>();
                        u.FID = uid;
                        log.AddLog(u);
                    }
                }
            }
            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        }

        public string EditBrand(TB_BrandModel u)
        {

            int k;
            string msg = "厂家品牌编辑失败。";
            if (HasBrandNo(u.FNAME, u.FID))
            {
                k = -2;
                msg = "厂家品牌已存在。";
            }
            else
            {
                if (HasBrandNumber(u.FNUMBER, u.FID))
                {
                    k = -2;
                    msg = "厂家品牌代码已存在。";
                }
                else
                {
                    var oldBrand = TB_BrandDal.Instance.Get(u.FID);
                    k = TB_BrandDal.Instance.Update(u);
                    if (k > 0)
                    {
                        msg = "编辑厂家品牌成功。";
                        LogBll<TB_BrandModel> log = new LogBll<TB_BrandModel>();
                        log.AddLog(u);
                    }
                }

            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public IEnumerable<TB_BrandModel>GetAll()
        {
            return TB_BrandDal.Instance.GetAll();
        }

    }
}
