using hn.Common;
using hn.DataAccess;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class EasyUIComboBoxController : Controller
    {
        /// <summary>
        /// EasyUIComboBox 获取经营场所数据
        /// </summary>
        [HttpPost]
        public string GetMarketLocations()
        {
            var datalist = TB_PREMISEDal.Instance.GetAll();

            List<object> list = new List<object>();
            foreach (var item in datalist)
            {
                list.Add(new
                {
                    id = item.FID,
                    text = item.FNAME
                });
            }

            return JSONhelper.ToJson(list);
        }

        /// <summary>
        /// EasyUIComboBox 获取品牌数据
        /// </summary>
        [HttpPost]
        public string GetBrands()
        {
            var datalist = TB_BrandDal.Instance.GetAll();

            List<object> list = new List<object>();
            foreach (var item in datalist)
            {
                list.Add(new
                {
                    id = item.FID,
                    text = item.FNAME
                });
            }

            return JSONhelper.ToJson(list);
        }

        /// <summary>
        /// EasyUIComboBox 获取审核组数据
        /// </summary>
        [HttpPost]
        public string GetApprovalGroups()
        {
            var datalist = TB_REVIEWTEAMDal.Instance.GetAll();

            List<object> list = new List<object>();
            foreach (var item in datalist)
            {
                list.Add(new
                {
                    id = item.FID,
                    text = item.FNAME
                });
            }

            return JSONhelper.ToJson(list);
        }

        /// <summary>
        /// 启用禁用
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public string IsEnable(int value = 1)
        {
            List<object> list = new List<object>();

            list.Add(new { id = 1, text = "启用" });
            list.Add(new { id = 0, text = "禁用" });

            return JSONhelper.ToJson(list);
        }

        /// <summary>
        /// 组织架构类别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string TB_ORGANIZATION_FTYPE()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.TB_ORGANIZATION_FTYPE)));
        }

        /// <summary>
        /// 采购订单审核状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string ICPOBILL_FSTATUS()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.BILL_FSTATUS)));
        }

        [HttpPost]
        public string BILL_TYPE()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.BILL_TYPE)));
        }

        /// <summary>
        /// 单据状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string BILL_FSTATUS()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.BILL_FSTATUS), PublicMethod.GetBool(Request["isall"])));
        }

        [HttpPost]
        public string BILL_DELIVERY_FSTATUS()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.BILL_DELIVERY_FSTATUS), PublicMethod.GetBool(Request["isall"])));
        }

        /// <summary>
        /// 请购计划单据状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string ICPRBILL_FSTATUS()
        {
            return JSONhelper.ToJson(GetEmumList(typeof(Constant.ICPRBILL_FSTATUS), PublicMethod.GetBool(Request["isall"])));
        }


        /// <summary>
        /// 是或否
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public string YesOrNo(int value = 0)
        {
            List<object> list = new List<object>();

            list.Add(new { id = 1, text = "是" });
            list.Add(new { id = 0, text = "否" });

            return JSONhelper.ToJson(list);
        }

        #region 获取字典

        /// <summary>
        /// 获取销区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string SubDicsFORG()
        {
            return JSONhelper.ToJson(getSubDics(Constant.SysDics.销区));
        }

        /// <summary>
        /// 获取一级销区数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSaleLocation1(Constant.SysDics code)
        {
            var list = SYS_SUBDICSBLL.Instance.GetByCodeWithEnable(code);
            var list2 = from c in list
                        where c.FPARENTID == null
                        select c;

            return JSONhelper.ToJson(list2.Select(s => new
            {
                id = s.FID,
                text = s.FNAME,
            }));
        }

        /// <summary>
        /// 获取二级销区数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSaleLocation2(string parentID)
        {
            var list = SYS_SUBDICSBLL.Instance.GetByParentID(parentID);

            return JSONhelper.ToJson(list.Select(s => new
            {
                id = s.FID,
                text = s.FNAME,
            }));
        }

        /// <summary>
        /// 获取运输方式
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string SubDicsTRANS()
        {
            return JSONhelper.ToJson(getSubDics(Constant.SysDics.运输方式));
        }

        private object getSubDics(Constant.SysDics code)
        {
            return SYS_SUBDICSBLL.Instance.GetByCodeWithEnable(code).Select(s => new
            {
                id = s.FID,
                code = s.FNUMBER,
                text = s.FNAME,
            });
        }

        /// <summary>
        /// 获取指定类别的字典数据
        /// </summary>
        /// <param name="categoryCode">字典类别编码</param>
        /// <returns></returns>
        [HttpPost]
        public string GetSubDicByCategoryCode()
        {
            string categoryCode = Request["categoryCode"];
            int category = 0;
            if (!int.TryParse(categoryCode, out category))
            {
                return "";
            }
            switch (category)
            {
                case 103:
                    return JSONhelper.ToJson(getSubDics(Constant.SysDics.价格政策类型));

                default:
                    break;
            }
            return "";
        }



        #endregion

        /// <summary>
        /// 遍历枚举列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private List<object> GetEmumList(Type type, bool isAll = false)
        {
            List<object> list = new List<object>();

            int count = 0;

            if (isAll)
            {
                list.Add(new
                {
                    id = "",
                    text = "全部"
                });
            }

            foreach (var value in Enum.GetValues(type))
            {
                list.Add(new
                {
                    id = Convert.ToInt32(value),
                    text = value.ToString()//,
                    //selected = count > 0 ? null : "selected",
                });

                count++;
            }

            return list;
        }

        /// <summary>
        /// 获取组织架构-销区数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string TB_ORGANIZATION_Sale(string parentID, string saleLV = "")
        {
            int lv = 0;//销区等级
            if (saleLV != "" && saleLV == "1")
            {
                lv = 1;
            }
            else
            {
                lv = 2;
            }
            string parentid = string.IsNullOrEmpty(parentID) ? "0" : parentID;
            var datalist = V_ORGANIZATIONDal.Instance.GetWhere(new { FPARENTALID = parentid });
            if (datalist.Count() <= 0)
            {
                return "";
            }
            List<object> list = new List<object>();
            foreach (var item in datalist)
            {
                if (item.FSTATUS != 1)
                {
                    continue;
                }
                if (item.FTYPE != (lv + 1))//销区等级过滤
                {
                    TB_ORGANIZATION_Sale_Recursion(item.FID, list, lv);
                    continue;
                }
                list.Add(new
                {
                    id = item.FID,
                    text = item.FORGNAME
                });
                TB_ORGANIZATION_Sale_Recursion(item.FID, list, lv);
            }

            return JSONhelper.ToJson(list);
        }
        /// <summary>
        /// 获取组织架构-销区数据 递归
        /// </summary>
        /// <param name="parentID">父级ID</param>
        /// <param name="list">返回数据集合</param>
        /// <param name="lv">销区等级</param>
        public void TB_ORGANIZATION_Sale_Recursion(string parentID, List<object> list, int lv)
        {
            string parentid = string.IsNullOrEmpty(parentID) ? "0" : parentID;
            var datalist = V_ORGANIZATIONDal.Instance.GetWhere(new { FPARENTALID = parentid });
            if (datalist.Count() <= 0)
            {
                return;
            }
            foreach (var item in datalist)
            {
                if (item.FSTATUS != 1)
                {
                    continue;
                }
                if (item.FTYPE != (lv + 1))
                {
                    TB_ORGANIZATION_Sale_Recursion(item.FID, list, lv);
                    continue;
                }
                list.Add(new
                {
                    id = item.FID,
                    text = item.FORGNAME
                });

                TB_ORGANIZATION_Sale_Recursion(item.FID, list, lv);
            }
        }

        public string GetProvince()
        {
            var list = TB_EBPLDal.Instance.GetWhere(new { EBPL_PARENT_PM_CODE = "100000", EBPL_IS_ABLE = "ENABLE" }).ToList();

            return JSONhelper.ToJson(list);
        }

        public string GetCity(string provinceid)
        {
            var list = TB_EBPLDal.Instance.GetWhere(new { EBPL_PARENT_PM_CODE = provinceid, EBPL_IS_ABLE = "ENABLE" }).ToList();

            return JSONhelper.ToJson(list);
        }

        public string GetDistrict(string cityid)
        {
            var list = TB_EBPLDal.Instance.GetWhere(new { EBPL_PARENT_PM_CODE = cityid, EBPL_IS_ABLE = "ENABLE" }).ToList();

            return JSONhelper.ToJson(list);
        }
    }
}