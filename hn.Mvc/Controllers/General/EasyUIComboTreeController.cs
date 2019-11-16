using hn.Common;
using hn.DataAccess;
using hn.DataAccess.Bll;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{


    public class EasyUIComboTreeController : Controller
    {
        /// <summary>
        /// 组织架构
        /// </summary>
        /// <param name="showDisable">是否显示禁用</param>
        /// <param name="NotDisplayID">单个不显示ID</param>
        /// <returns></returns>
        [HttpPost]
        public string TB_ORGANIZATION(bool showDisable = true, string NotDisplayID = null)
        {
            List<TB_OrganizationModel> dataList;

            dataList = TB_OrganizationBll.Instance.GetAll().ToList();

            List<object> list = new List<object>();

            list.Insert(0, new
            {
                id = 0,
                text = "请选择上级组织"
            });
            foreach (var item in dataList.Where(d => d.FPARENTALID.IsNullOrEmpty()))
            {
                if (!showDisable && item.FSTATUS != 1 || NotDisplayID == item.FID)
                {
                    continue;
                }

                list.Add(new
                {
                    id = item.FID,
                    text = item.FORGNAME,
                    children = TB_ORGANIZATIONRecursion(dataList, item.FID, showDisable, NotDisplayID),
                });
            }

            return JSONhelper.ToJson(list);
        }

        private object TB_ORGANIZATIONRecursion(List<TB_OrganizationModel> all, string FPARENTALID, bool showDisable, string NotDisplayID)
        {
            List<object> list = new List<object>();

            foreach (var item in all.Where(a => a.FPARENTALID == FPARENTALID))
            {
                if (!showDisable && item.FSTATUS != 1 || NotDisplayID == item.FID)
                {
                    continue;
                }

                list.Add(new
                {
                    id = item.FID,
                    text = item.FORGNAME,
                    children = TB_ORGANIZATIONRecursion(all, item.FID, showDisable, NotDisplayID),
                });
            }

            return list;
        }

        [HttpPost]
        public string SYS_SUBDICS(string parentID = null, bool showDisable = true, string NotDisplayID = null)
        {
            IEnumerable<SYS_SUBDICSMODEL> dataList = SYS_SUBDICSBLL.Instance.GetByClassID(parentID).ToList();

            List<object> list = new List<object>();
  
            foreach (var item in dataList.Where(d => d.FPARENTID.IsNullOrEmpty()))
            {
                if (!showDisable && item.FSTATUS != 1 || NotDisplayID == item.FID)
                {
                    continue;
                }

                list.Add(new
                {
                    id = item.FID,
                    text = item.FNAME,
                    children = dataList.Where(s => s.FPARENTID == item.FID).Select(s => new
                    {
                        id = item.FID,
                        text = item.FNAME,
                    })
                });
            }

            return JSONhelper.ToJson(list);
        }

        private object SYS_SUBDICSRecursion(List<SYS_SUBDICSMODEL> all, string FPARENTID, bool showDisable, string NotDisplayID)
        {
            List<object> list = new List<object>();

            foreach (var item in all.Where(a => a.FPARENTID == FPARENTID))
            {
                if (!showDisable && item.FSTATUS != 1 || NotDisplayID == item.FID)
                {
                    continue;
                }

                list.Add(new
                {
                    id = item.FID,
                    text = item.FNAME,
                    children = SYS_SUBDICSRecursion(all, item.FID, showDisable, NotDisplayID),
                });
            }

            return list;
        }


        /// <summary>
        /// 获取销区数据 下拉树
        /// </summary>
        /// <returns></returns>
        public string GetSaleLocation(Constant.SysDics code)
        {
            var list = SYS_SUBDICSBLL.Instance.GetByCodeWithEnable(code);
            var list2 = from c in list
                        where c.FPARENTID == null
                        select c;

            return JSONhelper.ToJson(list2.Select(s => new
            {
                id = s.FID,
                text = s.FNAME,
                children = list.Where(o => o.FPARENTID == s.FID).Select(o => new
                {
                    id = o.FID,
                    text = o.FNAME,
                })
            }));
        }

        //public string GetSaleLocationRecursion(string parentID)
        //{
        //    var list = SYS_SUBDICSBLL.Instance.GetByParentID(parentID);

        //    return JSONhelper.ToJson(list.Select(s => new
        //    {
        //        id = s.FID,
        //        text = s.FNAME,
        //    }));
        //}

    }
}