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
    public class MasterBll
    {
        public static MasterBll Instance
        {
            get { return SingletonProvider<MasterBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            MasterDal.Instance.Where = "";
            return MasterDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetJson2(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            MasterDal.Instance.Where = " and STATUS<>3";
            return MasterDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        /// <summary>
        /// 判断技师名是否存在
        /// </summary>
        /// <param name="Mastername">技师名</param>
        /// <param name="MasterId">技师Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasMasterNo(string phone, string id = "0")
        {
            var Masters = from n in MasterDal.Instance.GetAll()
                            where n.PHONE == phone && n.FID != id
                           select n;
            return Masters.Any();
        }

        public string AddMaster(MasterModel u)
        {
            string uid = "0";
            string msg = "技师添加失败！";
            if (HasMasterNo(u.PHONE, u.FID))
            {
                uid = "-2";
                msg = "手机号码已存在。";
                return new JsonMessage { Data = uid.ToString(), Message = msg, Success = false}.ToString();
            }
            else
            {
                u.MASTER_NO = MasterDal.Instance.GetNewNo();
                uid = MasterDal.Instance.Insert(u);
                if (uid != "")
                {
                    msg = "添加APP技师成功！";
                    LogBll<MasterModel> log = new LogBll<MasterModel>();
                    u.FID = uid;
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = (uid != "") }.ToString();
        }

        public string EditMaster(MasterModel u)
        {
            int k;
            string msg = "技师编辑失败。";
            if (HasMasterNo(u.PHONE, u.FID))
            {
                k = -2;
                msg = "手机号码已存在。";
                return new JsonMessage { Data = "", Message = msg, Success = false }.ToString();
            }
            else
            {
                var oldMaster = MasterDal.Instance.Get(u.FID);
                k = MasterDal.Instance.Update(u);
                if (k > 0)
                {
                    msg = "编辑APP技师成功。";
                    LogBll<MasterModel> log = new LogBll<MasterModel>();
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }
    }

}