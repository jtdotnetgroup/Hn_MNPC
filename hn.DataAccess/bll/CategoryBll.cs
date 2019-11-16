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
    public class CategoryBll
    {
        public static CategoryBll Instance
        {
            get { return SingletonProvider<CategoryBll>.Instance; }
        }

        private IEnumerable<object> GetCategoryTreeNodes(string parentid = "")
        {
            var nodes = CategoryDal.Instance.GetChildren(parentid);
            var treeNodes = from n in nodes
                            select
                                new { id = n.FID, text = n.CATEGORY_NAME, children = GetCategoryTreeNodes(n.FID) };
            return treeNodes;
        }

        /// <summary>
        /// 获取分类数据
        /// </summary>
        /// <returns></returns>
        public string GetCategoryTreeJson()
        {
            var nodes = GetCategoryTreeNodes();
            return JSONhelper.ToJson(nodes);
        }

        public string GetCategoryTreegridData()
        {
            return JSONhelper.ToJson(CategoryDal.Instance.GetChildren());
        }

        public IEnumerable<object> GetCategoryTreegridList()
        {
            List<CategoryModel> modelList = CategoryDal.Instance.GetAll().ToList();
            List<object> list = new List<object>();

            foreach (var item in modelList.Where(m => m.PARENT_ID.IsNullOrEmpty()))
            {
                list.Add(getChildrenList(modelList, item));
            }

            return list;
        }

        private object getChildrenList(IEnumerable<CategoryModel> all, CategoryModel model)
        {
            List<object> list = new List<object>();
            foreach (var item in all.Where(m => m.PARENT_ID == model.FID))
            {
                list.Add(getChildrenList(all, item));
            }

            return new
            {
                id = model.FID,
                text = model.CATEGORY_NAME,
                children = list,
            };
        }

        public bool HasCategoryByName(string categoryname, string depid = "")
        {
            var Categorys = CategoryDal.Instance.GetAll().ToList();
            return Categorys.Any(n => n.CATEGORY_NAME == categoryname && n.FID != depid);
        }

        public bool HasCategoryByNumber(string number, string depid = "")
        {
            var Categorys = CategoryDal.Instance.GetAll().ToList();
            return Categorys.Any(n => n.CATEGORY_NUMBER == number && n.FID != depid);
        }

        public string AddNewCategory(CategoryModel dep)
        {
            string k = "";
            string msg = "添加失败！";
            if (HasCategoryByNumber(dep.CATEGORY_NUMBER))
                msg = "分类编号已存在！";
            else if (HasCategoryByName(dep.CATEGORY_NAME))
                msg = "分类名称已存在！";
            else
            {
                k = CategoryDal.Instance.Insert(dep);
                if (k != "")
                {
                    msg = "添加成功。";
                    dep.FID = k;
                }
            }


            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != "" }.ToString();
        }

        public string EditCategory(CategoryModel dep)
        {
            string msg = "修改失败。";
            int k = 0;
            var oldDep = CategoryDal.Instance.Get(dep.FID);
            if (HasCategoryByNumber(dep.CATEGORY_NUMBER, dep.FID))
                msg = "分类编号已存在。";
            else if (HasCategoryByName(dep.CATEGORY_NAME, dep.FID))
                msg = "分类名称已存在！";
            else
            {
                if (dep.PARENT_ID == "null")
                {
                    dep.PARENT_ID = null;
                }
                k = CategoryDal.Instance.Update(dep);
                if (k > 0)
                {
                    msg = "修改成功。";
                }
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public string DeleteCategory(string depid)
        {
            string msg = "删除失败";
            var dep = CategoryDal.Instance.Get(depid);
            if (dep.children.Any())
                msg = "有下级分类数据，不能删除。";

            int k = CategoryDal.Instance.Delete(depid);
            if (k > 0)
            {
                msg = "删除成功。";
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }



    }

}