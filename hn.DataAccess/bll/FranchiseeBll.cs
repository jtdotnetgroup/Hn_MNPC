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
    public class FranchiseeBll
    {
        public static FranchiseeBll Instance
        {
            get { return SingletonProvider<FranchiseeBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return FranchiseeDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }


        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="Franchiseename">用户名</param>
        /// <param name="FranchiseeId">用户Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasFranchiseeNo(string name, string id = "0")
        {
            var Franchisees = from n in FranchiseeDal.Instance.GetAll()
                            where n.COMPANY_NAME == name && n.FID != id
                           select n;
            return Franchisees.Any();
        }

        public string AddFranchisee(FranchiseeModel u)
        {
            string uid = "0";
            string msg = "加盟商添加失败！";
            if (HasFranchiseeNo(u.COMPANY_NAME, u.FID))
            {
                uid = "-2";
                msg = "加盟商已存在。";
            }
            else
            {
                uid = FranchiseeDal.Instance.Insert(u);
                if (uid != "")
                {

                    OrganizeModel model = new OrganizeModel();
                    model.CODE = uid;
                    model.NAME = u.COMPANY_NAME;
                    model.SHORTNAME = u.COMPANY_NAME;
                    model.LINKMAN = u.LINK_MAN;
                    model.PHONE = u.TEL;
                    model.FAX = "";
                    model.EMAIL = "";
                    model.ADDRESS = u.ADDRESS;
                    model.POSTCODE = u.ZIP_CODE;
                    model.DISABLED = 1;
                    model.DESCRIPTION = u.REMARK;
                    model.TYPE = 1;
                    OrganizeDal.Instance.Insert(model);

                    msg = "添加加盟商成功！";
                    LogBll<FranchiseeModel> log = new LogBll<FranchiseeModel>();
                    u.FID = uid;
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        }

        public string EditFranchisee(FranchiseeModel u)
        {
           
            int k;
            string msg = "加盟商编辑失败。";
            if (HasFranchiseeNo(u.COMPANY_NAME, u.FID))
            {
                k = -2;
                msg = "加盟商已存在。";
            }
            else
            {
                var oldFranchisee = FranchiseeDal.Instance.Get(u.FID);
                k = FranchiseeDal.Instance.Update(u);
                if (k > 0)
                {
                    msg = "编辑加盟商成功。";
                    LogBll<FranchiseeModel> log = new LogBll<FranchiseeModel>();
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }
    }

}