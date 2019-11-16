using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using System.Configuration;
using hn.Client.Service;
using hn.DataAccess.model;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System.Text;
using hn.DataAccess.Dal;

namespace WebApiServer
{
    /// <summary>
    /// BaseInfoServer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BaseInfoServer : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 获取指定数据字典的列表数据
        /// </summary>
        /// <param name="categorycode"></param>
        /// <returns></returns>
        /// <returns></returns>
        public List<SYS_SUBDICSMODEL> GetDics(string categorycode, string keyword, bool all = false)
        {
            try
            {
                List<SYS_SUBDICSMODEL> datas = new List<SYS_SUBDICSMODEL>();
                SYS_DICSMODEL parent = SYS_DICSDAL.Instance.GetWhere(new { FCLASSCODE = categorycode }).FirstOrDefault();
                if (parent != null)
                {

                    var list = SYS_SUBDICSDAL.Instance.GetWhere(new { FCLASSID = parent.FID }).OrderBy(m => m.FNAME).ToList();
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        foreach (var item in list)
                        {
                            if (string.IsNullOrEmpty(keyword) || item.FNAME.Contains(keyword))
                            {
                                datas.Add(item);
                            }
                        }

                        list = datas;
                    }

                    if (all)
                    {
                        list.Insert(0, new SYS_SUBDICSMODEL() { FNAME = "全部", FID = "" });
                    }

                    return list;
                }

                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取厂家账户列表
        /// </summary>
        /// <param name="brandid">品牌ID</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<V_CLIENTACCOUNTModel> GetClientAccountList(string brandid, string keyword)
        {
            try
            {
                string where = " and FBRANDID='" + brandid + "'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    where += string.Format(" AND (FACCOUNT like '%{0}%' OR FNAME LIKE '%{0}%')", keyword);
                }

                return V_CLIENTACCOUNTDal.Instance.GetWhereStr(where).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取承运公司列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<TB_EXPRESSCOMPANYModel> GetExpressCompanyList(string keyword)
        {
            try
            {
                return TB_EXPRESSCOMPANYDal.Instance.GetAll().ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<ProductViewModel> GetProductList(string keywords)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                if (!keywords.IsNullOrEmpty())
                {
                    query.Append(" and ( ");

                    query.AppendFormat("  FPRODUCTNAME like '%{0}%' ", keywords);
                    query.AppendFormat(" OR FPRODUCTTYPE like '%{0}%' ", keywords);
                    query.AppendFormat(" OR FPRODUCTCODE like '%{0}%' ", keywords);
                    query.AppendFormat(" OR FMODEL like '%{0}%' ", keywords);

                    query.Append(" ) ");
                }

                return ProductViewDal.Instance.GetWhereStr(query.ToStr()).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取厂家代码列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SRCModel> GetSrcList(string productid, string keyword)
        {
            try
            {
                string where = " and FPRODUCTID='" + productid + "'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    where += string.Format(" AND (FSRCNAME like '%{0}%' OR FSRCCODE LIKE '%{0}%')", keyword);
                }

                return SRCDal.Instance.GetWhereStr(where).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }

        public List<TB_EBPLModel> GetProvince()
        {
            var list = TB_EBPLDal.Instance.GetWhere(new { EBPL_PARENT_PM_CODE = "100000", EBPL_IS_ABLE = "ENABLE" }).ToList();

            return list;
        }

        public List<TB_EBPLModel> GetCity(string provinceid)
        {
            var list = TB_EBPLDal.Instance.GetWhere(new { EBPL_PARENT_PM_CODE = provinceid, EBPL_IS_ABLE = "ENABLE" }).ToList();

            return list;
        }

        public List<TB_EBPLModel> GetDistrict(string cityid)
        {
            var list = TB_EBPLDal.Instance.GetWhere(new { EBPL_PARENT_PM_CODE = cityid, EBPL_IS_ABLE = "ENABLE" }).ToList();

            return list;
        }

        /// <summary>
        /// 获取厂家发货基地列表
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public List<TB_DELIVER_BASEModel> GetDeliverBaseList(string brand, string keyword)
        {
            try
            {
                string where = " and FBRAND='" + brand + "'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    where += string.Format(" AND (FBASEA_NAME like '%{0}%' OR FADDRESS LIKE '%{0}%')", keyword);
                }

                return TB_DELIVER_BASEDal.Instance.GetWhereStr(where).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
    }
}
