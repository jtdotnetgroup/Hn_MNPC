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
    public class SYS_DICSBLL
    {
        public static SYS_DICSBLL Instance
        {
            get { return SingletonProvider<SYS_DICSBLL>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return SYS_DICSDAL.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public IEnumerable<SYS_DICSMODEL> GetAll()
        {
            return SYS_DICSDAL.Instance.GetAll();
        }

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public string Add(SYS_DICSMODEL entity)
        //{
        //    if (Sys_DicsDal.Instance.CountWhere(new { FCLASSCODE = entity.FCLASSCODE }) > 0)
        //    {
        //        return "编码重复，请检查！";
        //    }

        //    return Sys_DicsDal.Instance.Insert(entity).IsGuid() ? "保存失败！" : null;
        //}

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public string Add(string classCode, string name, int sysDefault, string creatorID, string remark)
        {
            if (SYS_DICSDAL.Instance.CountWhere(new { FCLASSCODE = classCode }) > 0)
            {
                return "编码重复，请检查！";
            }

            SYS_DICSMODEL entity = new SYS_DICSMODEL()
            {
                FCLASSCODE = classCode,
                FCLASSNAME = name,
                FSYSDEFAULT = sysDefault,
                FCREATORID = creatorID,
                FREMARK = remark,
                FCREATETIME = DateTime.Now,
            };

            return SYS_DICSDAL.Instance.Insert(entity);
        }

        public string Update(SYS_DICSMODEL entity)
        {
            SYS_DICSMODEL model = SYS_DICSDAL.Instance.GetWhere(new { FCLASSCODE = entity.FCLASSCODE }).FirstOrDefault();

            if (model != null && model.FID != entity.FID)
            {
                return "编码重复，请检查！";
            }

            return SYS_DICSDAL.Instance.UpdateWhatWhere(new
            {
                FCLASSNAME = entity.FCLASSNAME,
                FCLASSCODE = entity.FCLASSCODE,
                FUPDATETIME = DateTime.Now,
                FUPDATERID = entity.FUPDATERID,
                FREMARK = entity.FREMARK
            }, new
            {
                FID = entity.FID
            }) == 0 ? "保存失败！" : null;
        }

        /// <summary>
        /// 获取指定字典
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SYS_DICSMODEL GetInfoByID(string fid)
        {
            return SYS_DICSDAL.Instance.GetWhere(new { FID = fid }).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定字典
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SYS_DICSMODEL GetByCode(Constant.SysDics code)
        {
            return SYS_DICSDAL.Instance.GetWhere(new { FClassCode = code.ToInt().ToString() }).FirstOrDefault();
        }

        /// <summary>
        /// 获取指定Code字典的ID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetIDByCode(string code)
        {
            SYS_DICSMODEL model = SYS_DICSDAL.Instance.GetWhere(new { FClassCode = code }).FirstOrDefault();

            return model == null ? null : model.FID;
        }

        /// <summary>
        /// 删除数据字典主表，子表下数据会一同删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Delete(string ID)
        {
            //Sys_DicsDal.Instance.DeleteWithSub(ID);
            SYS_DICSDAL.Instance.Delete(ID);

            SYS_SUBDICSBLL.Instance.DeleteByParentID(ID);

            return 0;
        }
    }

}