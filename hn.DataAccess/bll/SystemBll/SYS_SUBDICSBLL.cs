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
    public class SYS_SUBDICSBLL
    {
        public static SYS_SUBDICSBLL Instance
        {
            get { return SingletonProvider<SYS_SUBDICSBLL>.Instance; }
        }

        public string GetEasyUIJson(int pageindex, int pagesize, string parentID = null, string parentCode = null, string sort = "FID", string order = "asc")
        {
            return SYS_SUBDICSDAL.Instance.GetJson(pageindex, pagesize, parentID, parentCode, sort, order);
        }

        public IEnumerable<SYS_SUBDICSMODEL> GetAll()
        {
            return SYS_SUBDICSDAL.Instance.GetAll();
        }

        /// <summary>
        /// 获取数据字典下数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IEnumerable<SYS_SUBDICSMODEL> GetByClassID(string ClassID)
        {
            return SYS_SUBDICSDAL.Instance.GetWhere(new { FClassID = ClassID });
        }

        /// <summary>
        /// 获取数据字典下数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IEnumerable<SYS_SUBDICSMODEL> GetByParentID(string parentID)
        {
            return SYS_SUBDICSDAL.Instance.GetWhere(new { FPARENTID = parentID });
        }

        public IEnumerable<SYS_SUBDICSMODEL> GetByCode(Constant.SysDics code)
        {
            SYS_DICSMODEL parent = SYS_DICSBLL.Instance.GetByCode(code);

            if (parent != null)
            {
                return SYS_SUBDICSDAL.Instance.GetWhere(new { FCLASSID = parent.FID });
            }

            return null;
        }

        public IEnumerable<SYS_SUBDICSMODEL> GetByCode(string code)
        {
            SYS_DICSMODEL parent = SYS_DICSDAL.Instance.GetWhere(new { FCLASSCODE = code }).FirstOrDefault();
            if (parent != null)
            {
                return SYS_SUBDICSDAL.Instance.GetWhere(new { FCLASSID = parent.FID }).OrderBy(m=>m.FNAME);
            }

            return null;
        }

        public IEnumerable<SYS_SUBDICSMODEL> GetByCodeWithEnable(Constant.SysDics code)
        {
            SYS_DICSMODEL parent = SYS_DICSBLL.Instance.GetByCode(code);

            if (parent != null)
            {
                return SYS_SUBDICSDAL.Instance.GetWhere(new
                {
                    FCLASSID = parent.FID,
                    FSTATUS = 1,
                });
            }

            return null;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public string Add(string sysParentID, string number, string name, int status, string creatorID, string remark, string parentID)
        {
            if (SYS_SUBDICSDAL.Instance.CountWhere(new { FCLASSID = sysParentID, FNUMBER = number }) > 0)
            {
                return "编码重复，请检查！";
            }

            SYS_SUBDICSMODEL model = new SYS_SUBDICSMODEL()
            {
                FCLASSID = sysParentID,
                FNUMBER = number,
                FNAME = name,
                FSTATUS = status,
                FCREATOR = creatorID,
                FREMARK = remark,
                FCREATETIME = DateTime.Now,
                FPARENTID = parentID,
            };

            return SYS_SUBDICSDAL.Instance.Insert(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public string Update(string ID, string number, string name, int status, string updaterID, string remark,string parentID,string classid)
        {

            SYS_SUBDICSMODEL model = SYS_SUBDICSDAL.Instance.GetWhere(new { FNumber = number, FCLASSID = classid }).FirstOrDefault();

            if (model != null && model.FID != ID)
            {
                return "编码重复，请检查！";
            }

            return SYS_SUBDICSDAL.Instance.UpdateWhatWhere(new
            {
                FNumber = number,
                FName = name,
                FStatus = status,
                FUpdater = updaterID,
                FRemark = remark,
                FUpdateTime = DateTime.Now,
                FPARENTID = parentID,
            }, new { FID = ID }) > 0 ? null : "修改失败！";
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Delete(string ID)
        {
            return SYS_SUBDICSDAL.Instance.Delete(ID);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int BatchDelete(string ID)
        {
            return SYS_SUBDICSDAL.Instance.Delete(ID);
        }

        public int DeleteByParentID(string ID)
        {
            return SYS_SUBDICSDAL.Instance.DeleteWhere(new { FClassID = ID });
        }
    }

}