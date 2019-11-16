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
    public class TB_OrganizationBll
    {
        public static TB_OrganizationBll Instance
        {
            get { return SingletonProvider<TB_OrganizationBll>.Instance; }
        }

        public IEnumerable<object> GetOrganizationTreeNodes(string parentid = "")
        {
            var nodes = TB_OrganizationDal.Instance.GetChildren(parentid);
            var treeNodes = from n in nodes
                            select
                                new { id = n.FID, text = n.FORGNAME, children = GetOrganizationTreeNodes(n.FID) };
            return treeNodes;
        }

        public IEnumerable<TB_OrganizationModel> GetAll()
        {
            return TB_OrganizationDal.Instance.GetAll();
        }

        public IEnumerable<TB_OrganizationModel> GetAllByEnable()
        {
            return TB_OrganizationDal.Instance.GetWhere(new { FSTATUS = 1 });
        }

        /// <summary>
        /// 获取组织数据
        /// </summary>
        /// <returns></returns>
        public string GetOrganizationTreeJson()
        {
            var nodes = GetOrganizationTreeNodes();
            return JSONhelper.ToJson(nodes);
        }

        public string GetOrganizationTreegridData()
        {
            return JSONhelper.ToJson(TB_OrganizationDal.Instance.GetChildren());
        }
        

        public bool HasOrganizationByName(string organizationname, string fid = "")
        {
            var TB_Organizations = TB_OrganizationDal.Instance.GetAll().ToList();
            return TB_Organizations.Any(n => n.FORGNAME == organizationname && n.FID != fid);
        }

        public bool HasOrganizationByNumber(string number, string fid = "")
        {
            var TB_Organizations = TB_OrganizationDal.Instance.GetAll().ToList();
            return TB_Organizations.Any(n => n.FORGCODE == number && n.FID != fid);
        }

        public string AddNewOrganization(TB_OrganizationModel dep)
        {
            string k = "";
            string msg = "添加失败！";
            if (HasOrganizationByNumber(dep.FORGCODE))
                msg = "组织编号已存在！";
            else if (HasOrganizationByName(dep.FORGNAME))
                msg = "组织名称已存在！";
            else
            {
                k = TB_OrganizationDal.Instance.Insert(dep);
                if (k != "")
                {
                    msg = "添加成功。";
                    dep.FID = k;
                }
            }


            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != "" }.ToString();
        }

        public string EditOrganization(TB_OrganizationModel org)
        {
            string msg = "修改失败。";
            int k = 0;
            var oldDep = TB_OrganizationDal.Instance.Get(org.FID);
            if (HasOrganizationByNumber(org.FORGCODE, org.FID))
                msg = "组织编号已存在。";
            else if (HasOrganizationByName(org.FORGNAME, org.FID))
                msg = "组织名称已存在！";
            else
            {
                k = TB_OrganizationDal.Instance.Update(org);
                if (k > 0)
                {
                    msg = "修改成功。";
                }
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public string DeleteOrganization(string depid)
        {
            if (TB_OrganizationDal.Instance.CountWhere(new { FPARENTALID = depid }) > 0)
            {
                return "有下级组织数据，不能删除！";
            }

            return TB_OrganizationDal.Instance.Delete(depid) > 0 ? null : "删除失败，该数据可能已经删除了！";
        }


        //public User GetUserBy(string userName)
        //{
        //    return GetAll().ToList().Find(n => n.UserName == userName);
        //}


        public TB_OrganizationModel getOrganizeModeName(string FID)
        {
            return TB_OrganizationDal.Instance.Get(FID);
        }

        public string Add(TB_OrganizationModel model)
        {
            if (TB_OrganizationDal.Instance.CountWhere(new { FORGCODE = model.FORGCODE }) > 0)
            {
                return "组织编号已存在！";
            }

            return TB_OrganizationDal.Instance.Insert(model);
        }

        public string Edit(TB_OrganizationModel model)
        {
            TB_OrganizationModel temp = TB_OrganizationDal.Instance.GetWhere(new { FORGCODE = model.FORGCODE }).FirstOrDefault();

            if (temp != null && temp.FID != model.FID)
            {
                return "组织编号已存在！";
            }

            return TB_OrganizationDal.Instance.UpdateWhatWhere(new
            {
                FPARENTALID =model.FPARENTALID,
                FSTATUS = model.FSTATUS,
                FORGCODE = model.FORGCODE,
                FORGNAME = model.FORGNAME,
                FHEADER = model.FHEADER,
                FTYPE = model.FTYPE,
                FATTRIBUTE1 = model.FATTRIBUTE1,
                FATTRIBUTE2 = model.FATTRIBUTE2,
                FATTRIBUTE3 = model.FATTRIBUTE3,
                FATTRIBUTE4 = model.FATTRIBUTE4,
                FATTRIBUTE5 = model.FATTRIBUTE5,
                FATTRIBUTE6 = model.FATTRIBUTE6,
                FATTRIBUTE7 = model.FATTRIBUTE7,
                FATTRIBUTE8 = model.FATTRIBUTE8,
                FATTRIBUTE9 = model.FATTRIBUTE9,
                FATTRIBUTE10 = model.FATTRIBUTE10,
                //FDEFAULTADDR = model.FDEFAULTADDR,
                //FSTOCKCODE = model.FSTOCKCODE,
                //FSTOCKNAME = model.FSTOCKNAME,
            }, new
            {
                FID = model.FID
            }) > 0 ? null : "保存失败！";
        }

        public string Delete(string FID)
        {
            if(TB_OrganizationDal.Instance.CountWhere(new { FPARENTALID = FID }) > 0)
            {
                return "当前组织架构存在下级，不允许删除！";
            }

            TB_OrganizationDal.Instance.Delete(FID);

            return null;
        }
    }
}