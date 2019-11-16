using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Core.Dal;
using hn.Common.Provider;
using hn.Common;
using hn.Core.Model;
namespace hn.Core.Bll
{
    public class DepartmentBll
    {
        public  static  DepartmentBll Instance
        {
            get { return SingletonProvider<DepartmentBll>.Instance; }
        }

        private IEnumerable<object> GetDepartmentTreeNodes(string parentid = "0")
        {
            var nodes = DepartmentDal.Instance.GetChildren(parentid);
            var treeNodes = from n in nodes
                            orderby n.Sortnum ascending
                            select
                                new {id = n.FID, text = n.DepartmentName, children = GetDepartmentTreeNodes(n.FID)};
            return treeNodes;
        }

        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <returns></returns>
        public string GetDepartmentTreeJson()
        {
            var nodes = GetDepartmentTreeNodes();
            return JSONhelper.ToJson(nodes);
        }

        public string GetDepartmentTreegridData()
        {
            return JSONhelper.ToJson(DepartmentDal.Instance.GetChildren());
        }

        public bool HasDepartmentBy(string departmentName, string depid = "0")
        {
            var departments = DepartmentDal.Instance.GetAll().ToList();
            return departments.Any(n => n.DepartmentName == departmentName && n.FID!=depid);
        }

        public string AddNewDepartment(Department dep)
        {
            string k = "";
            string msg = "添加失败！";
            if (HasDepartmentBy(dep.DepartmentName))
                msg = "部门名称已存在！";
            else
            {
                k = DepartmentDal.Instance.Insert(dep);
                if(k!="")
                {
                    msg = "添加成功。";
                    LogBll<Department> log = new LogBll<Department>();
                    dep.FID = k;
                    log.AddLog(dep);
                }
            }

            return new JsonMessage {Data = k.ToString(), Message = msg, Success = k != ""}.ToString();
        }

        public string EditDepartment(Department dep)
        {
            string msg = "修改失败。";
            int k = 0;
            var oldDep = DepartmentDal.Instance.Get(dep.FID);
            if(HasDepartmentBy(dep.DepartmentName,dep.FID))
                msg = "部门名称已存在。";
            else
            {
                k = DepartmentDal.Instance.Update(dep);
                if(k>0)
                {
                    msg = "修改成功。";
                    LogBll<Department> log = new LogBll<Department>();
                    log.UpdateLog(oldDep,dep);
                }
            }

            return new JsonMessage {Data = k.ToString(), Message = msg, Success = k > 0}.ToString();
        }

        public string DeleteDepartment(string depid)
        {
            string msg = "删除失败";
            var dep = DepartmentDal.Instance.Get(depid);
            if (dep.children.Any())
                msg = "有下级部门数据，不能删除。";

            int k = DepartmentDal.Instance.Delete(depid);
            if (k > 0)
            {
                msg = "删除成功。";
                LogBll<Department> log = new LogBll<Department>();
                log.DeleteLog(dep);
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }


       
    }
}
