using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace hn.Client.Service
{
    public partial class APIService
    {

        public ProductViewModel getProductView(string fid)
        {
            return ProductViewDal.Instance.Get(fid);

        }
      
        /// <summary>
        /// 获取指定数据字典的列表数据
        /// </summary>
        /// <param name="categorycode"></param>
        /// <returns></returns>
        /// <returns></returns>
        public List<SYS_SUBDICSMODEL> GetDics(string categorycode,string keyword, bool all = false)
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


        public List<TB_PREMISEModel> GetJYDW(string keyword)
        {
            try
            {
                List<TB_PREMISEModel> datas = new List<TB_PREMISEModel>();

                var list = TB_PREMISEDal.Instance.GetAll().ToList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    return list.Where(x => x.FCODE.Contains(keyword) || x.FNAME.Contains(keyword)).ToList();
                }
                else
                {
                    return list;
                }


               



            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        public SRCModel getSrc(string pz,string gg,string xh)
        {
            List<SRCModel> list = SRCDal.Instance.GetWhere(new { FSRCCODE = pz + "||" + xh + "||" + gg }).ToList();
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }

        public SYS_SUBDICSMODEL GetArea2(string fid)
        {
            try
            {

                List<SYS_SUBDICSMODEL> sys = SYS_SUBDICSDAL.Instance.GetWhere(new {FID=fid }).ToList();
                if (sys.Count > 0)
                {
                    return sys[0];
                }
                else {
                    return null;
                }

          
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }
        public List<SYS_SUBDICSMODEL> GetDics_Area(string categorycode, string keyword, bool all = false)
        {
            try
            {
                List<SYS_SUBDICSMODEL> datas = new List<SYS_SUBDICSMODEL>();
                SYS_DICSMODEL parent = SYS_DICSDAL.Instance.GetWhere(new { FCLASSCODE = categorycode }).FirstOrDefault();
                if (parent != null)
                {

                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * from SYS_SUBDICS where fclassid='"+parent.FID+"'");
                    sql.AppendLine(" and fid in(select fclassarea2 from v_icpr_icpo_icseout_thd where (leftsl>0 or leftsl is null)  and autoid is not null and fdesbillno is not null)");
                    LogHelper.WriteLog(sql.ToStr());
                  
                    var list = SYS_SUBDICSDAL.Instance.Query(sql.ToString()).OrderBy(m => m.FNAME).ToList();
                    LogHelper.WriteLog(sql.ToStr());

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


        public SYS_SUBDICSMODEL GetSingleDics(string fid)
        {
            try
            {
                SYS_SUBDICSMODEL single = SYS_SUBDICSDAL.Instance.Get(fid);


                return single;
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
                /*
                string where = " and FBRANDID='" + brandid + "'";
                if (!string.IsNullOrEmpty(keyword))
                {
                    where += string.Format(" AND (FACCOUNT like '%{0}%' OR FNAME LIKE '%{0}%')", keyword);
                }

                return V_CLIENTACCOUNTDal.Instance.GetWhereStr(where).ToList();
                */

                string where = " and FBRANDID='" + brandid + "'";               
                 where += string.Format(" AND (FACCOUNT LIKE '%MN%')", keyword);               
                var l1= V_CLIENTACCOUNTDal.Instance.GetWhereStr(where).OrderBy(x=>x.FACCOUNT).ToList();

                where = " and FBRANDID='" + brandid + "'";               
                    where += string.Format(" AND (FACCOUNT LIKE '%FDK%')", keyword);              
                var l2 = V_CLIENTACCOUNTDal.Instance.GetWhereStr(where).OrderBy(x => x.FACCOUNT).ToList();

                where = " and FBRANDID='" + brandid + "'";               
                    where += string.Format(" AND (FACCOUNT LIKE '%GW%')", keyword);
               
                var l3 = V_CLIENTACCOUNTDal.Instance.GetWhereStr(where).OrderBy(x => x.FACCOUNT).ToList();

                List<V_CLIENTACCOUNTModel> vList = new List<V_CLIENTACCOUNTModel>();
                foreach (var sub in l1)
                {
                    vList.Add(sub);
                }

                foreach (var sub in l2)
                {
                    vList.Add(sub);
                }
                foreach (var sub in l3)
                {
                    vList.Add(sub);
                }

                return vList;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        public V_CLIENTACCOUNTModel GetClientAccountSingle(string clientid)
        {
            try
            {
              
                return V_CLIENTACCOUNTDal.Instance.Get(clientid);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }
        }


        public V_CLIENTACCOUNTModel GetClientByAccount(string faccount)
        {
            try
            {

                var list= V_CLIENTACCOUNTDal.Instance.GetWhere(" FAccount='"+faccount+"'",null).ToList();
                if (list.Count() > 0)
                {
                    return list[0];
                }
                else
                {
                    return new V_CLIENTACCOUNTModel();
                }
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
                return TB_EXPRESSCOMPANYDal.Instance.GetWhereStr($" AND  FNAME LIKE '%{keyword}%'").ToList();
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
        public List<ProductViewModel> GetProductList(hn.Core.Model.User loginUser, string keywords)
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
                if (loginUser.IsAdmin != 1)
                {
                    //品牌/厂家进行数据权限控制
                    query.AppendFormat(" AND FBRANDID IN (SELECT FBRANDID FROM TB_USERBRAND WHERE FUSERID = '{0}') ", loginUser.FID);
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
        public List<SRCModel> GetSrcList(string productid,string keyword)
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