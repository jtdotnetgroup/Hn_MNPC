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
    public class OrganizeBll
    {
        public static OrganizeBll Instance
        {
            get { return SingletonProvider<OrganizeBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc", string Organizeid = "")
        {
            return OrganizeDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order, Organizeid);
        }

        public string TreeJson()
        {
            try
            {
                IEnumerable<OrganizeModel> list1 = OrganizeDal.Instance.GetWhere(new { TYPE = 0 });
                IEnumerable<OrganizeModel> list2 = OrganizeDal.Instance.GetWhere(new { TYPE = 1 });
                List<TreeItem> data1 = new List<TreeItem>();
                List<TreeItem> data2 = new List<TreeItem>();

                data1.Add(new TreeItem() { id = "0", text = "总部" });
                foreach (OrganizeModel model in list1)
                {
                    data1.Add(new TreeItem() { id = model.FID, text = model.NAME });
                }
                foreach (OrganizeModel model in list2)
                {
                    data2.Add(new TreeItem() { id = model.FID, text = model.NAME });
                }


                List<TreeItem> tree = new List<TreeItem>();
                tree.Add(new TreeItem() { id = "", text = "华耐", children = data1 });
                tree.Add(new TreeItem() { id = "1", text = "加盟商", children = data2 });

                return JSONhelper.ToJson(tree);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string TreeJsonEx()
        {
            try
            {
                IEnumerable<OrganizeModel> list1 = OrganizeDal.Instance.GetWhere(new { TYPE = 0 });
                IEnumerable<OrganizeModel> list2 = OrganizeDal.Instance.GetWhere(new { TYPE = 1 });
                List<TreeItem> data1 = new List<TreeItem>();
                List<TreeItem> data2 = new List<TreeItem>();

                data1.Add(new TreeItem() { id = "0", text = "总部" });
                foreach (OrganizeModel model in list1)
                {
                   
                    List<MasterModel> list = MasterDal.Instance.GetWhere(new { ORGANIZE_ID  = model.FID}).ToList();
                    List<TreeItem> treeMaster = new List<TreeItem>();
                    foreach (MasterModel master in list)
                    {
                        treeMaster.Add(new TreeItem() { id=master.FID,text = master.NAME });
                    }

                    data1.Add(new TreeItem() { id = model.FID, text = model.NAME, children=treeMaster });
                }
                foreach (OrganizeModel model in list2)
                {
                    List<MasterModel> list = MasterDal.Instance.GetWhere(new { ORGANIZE_ID = model.FID }).ToList();
                    List<TreeItem> treeMaster = new List<TreeItem>();
                    foreach (MasterModel master in list)
                    {
                        treeMaster.Add(new TreeItem() { id = master.FID, text = master.NAME });
                    }
                    data2.Add(new TreeItem() { id = model.FID, text = model.NAME, children = treeMaster });
                }


                List<TreeItem> tree = new List<TreeItem>();
                tree.Add(new TreeItem() { id = "", text = "华耐", children = data1 });
                tree.Add(new TreeItem() { id = "1", text = "加盟商", children = data2 });

                return JSONhelper.ToJson(tree);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
      
       
       
        private class TreeItem
        {
            public string id { get; set; }
            public string text { get; set; }
            public List<TreeItem> children { get; set; }

        }

    }
}
