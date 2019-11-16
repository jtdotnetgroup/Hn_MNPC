using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.DataAccess.Model;
using hn.DataAccess.Bll;
using hn.Common;
using hn.DataAccess.dal;

namespace hn.Mvc.Controllers
{
    public class TreeViewController : Controller
    {

        [HttpPost]
        public string DicCategory()
        {
            List<object> list = new List<object>();

            foreach (var item in SYS_DICSBLL.Instance.GetAll())
            {
                list.Add(new
                {
                    id = item.FID,
                    //text = string.Format("{0}[{1}]", item.FCLASSNAME, item.FCLASSCODE),
                    text = string.Format("{0}", item.FCLASSNAME),
                    iconCls = "icon-bullet_blue",
                });
            }

            return JSONhelper.ToJson(list);

        }

        [HttpPost]
        public string SubDicCategory(string parentID)
        {
            List<object> list = new List<object>();

            foreach (var item in SYS_SUBDICSBLL.Instance.GetByClassID(parentID))
            {
                list.Add(new
                {
                    id = item.FID,
                    text = item.FNAME,
                    iconCls = "icon-bullet_blue",
                });
            }

            return JSONhelper.ToJson(list);

        }


        [HttpPost]
        public string PriorityList()
        {
            List<object> list = new List<object>();

            for (int i = 1; i <= 5; i++)
            {
                list.Add(new
                {
                    id = i,
                    text = i + "级",
                    iconCls = "icon-bullet_blue"
                });
            }

            return JSONhelper.ToJson(list);

        }


        [HttpPost]
        public string SubDicCategoryByCode(string code, bool appendAll = false)
        {
            List<object> list = new List<object>();

            if (appendAll)
            {
                list.Add(new
                {
                    id = "",
                    text = "全部",
                    iconCls = "icon-bullet_blue",
                });
            }

            string parentID = SYS_DICSBLL.Instance.GetIDByCode(code);

            if (!parentID.IsNullOrEmpty())
            {
                foreach (var item in SYS_SUBDICSBLL.Instance.GetByClassID(parentID))
                {
                    list.Add(new
                    {
                        id = item.FID,
                        text = item.FNAME,
                        iconCls = "icon-bullet_blue",
                    });
                }
            }
            return JSONhelper.ToJson(list);

        }

        /// <summary>
        /// 获取商品分类树
        /// </summary>
        /// <param name="appendAll"></param>
        /// <returns></returns>
        [HttpPost]
        public string CategoryList(bool appendAll = false)
        {
            //List<object> list = new List<object>();

            //if (appendAll)

            //{
            //    list.Add(new
            //    {
            //        id = "",
            //        text = "全部",
            //        iconCls = "icon-bullet_blue",
            //    });
            //}

            IEnumerable<object> list = CategoryBll.Instance.GetCategoryTreegridList();

            return JSONhelper.ToJson(list);
        }

        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <param name="appendAll"></param>
        /// <returns></returns>
        [HttpPost]
        public string BrandList(bool appendAll = false)
        {

            List<object> list = new List<object>();

            

            foreach (var item in TB_BrandBll.Instance.GetAll())
            {
                list.Add(new
                {
                    id = item.FID,
                    text = item.FNAME,
                    iconCls = "icon-bullet_blue",
                    children = getBrandAccount(item.FID),
                    attributes=0,
                    state = "closed"
            });
            }

            if (appendAll)
            {
                List<object> alllist = new List<object>();
                alllist.Add(new
                {
                    id = "",
                    text = "全部",
                    iconCls = "icon-bullet_blue",
                    children = list
                });
                return JSONhelper.ToJson(alllist);
            }

            return JSONhelper.ToJson(list);
        }

        private List<object> getBrandAccount(string fid)
        {
            var accounts = TB_CLIENTACCOUNTDal.Instance.GetWhere(new { FBRANDID  = fid}).ToList();
            List<object> list = new List<object>();
            foreach(TB_CLIENTACCOUNTModel model in accounts)
            {
                list.Add(new
                {
                    id = model.FID,
                    text = model.FACCOUNT,
                    attributes = 1
                });
            }

            return list;
        }

        #region 组织架构

        /// <summary>
        /// 获取组织架构
        /// </summary>
        /// <param name="showDisable"></param>
        /// <param name="NotDisplayID"></param>
        /// <returns></returns>
        [HttpPost]
        public string TB_ORGANIZATION(bool showDisable = true, string NotDisplayID = null)
        {
            List<TB_OrganizationModel> dataList = TB_OrganizationBll.Instance.GetAll().ToList();

            List<object> list = new List<object>();

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
                    iconCls = "icon-bullet_blue",
                    children = TB_ORGANIZATIONRecursion(dataList, item.FID, showDisable, NotDisplayID, null, false),
                });
            }

            return JSONhelper.ToJson(list);
        }

        /// <summary>
        /// 部门加用户
        /// </summary>
        /// <param name="showDisable"></param>
        /// <param name="NotDisplayID"></param>
        /// <returns></returns>
        [HttpPost]
        public string TB_ORGANIZATIONWithUser(bool showDisable = true, string NotDisplayID = null)
        {
            List<TB_OrganizationModel> dataList;

            dataList = TB_OrganizationBll.Instance.GetAll().ToList();

            List<hn.Core.Model.User> userList = hn.Core.Bll.UserBll.Instance.GetAll().ToList();

            List<object> list = new List<object>();

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
                    iconCls = "icon-bullet_blue",
                    IsUser = false,
                    children = TB_ORGANIZATIONRecursion(dataList, item.FID, showDisable, NotDisplayID, userList, true),
                });
            }

            return JSONhelper.ToJson(list);
        }

        private object TB_ORGANIZATIONRecursion(IEnumerable<TB_OrganizationModel> all, string FPARENTALID, bool showDisable, string NotDisplayID, IEnumerable<hn.Core.Model.User> userAll, bool appendUser)
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
                    iconCls = "icon-bullet_blue",
                    IsUser = false,
                    children = TB_ORGANIZATIONRecursion(all, item.FID, showDisable, NotDisplayID, userAll, appendUser),
                });
            }

            if (appendUser)
            {
                list.AddRange(userAll.Where(U => U.FDepartment == FPARENTALID).Select(s => new
                {
                    id = s.FID,
                    text = s.TrueName,
                    IsUser = true,
                    iconCls = "icon-bullet_green",
                }));
            }

            return list;
        }

        #endregion
    }
}
