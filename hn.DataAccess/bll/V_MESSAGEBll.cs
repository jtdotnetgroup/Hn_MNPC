using hn.Common;
using hn.Common.Provider;
using hn.Core;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.Core.Model;
using hn.DataAccess.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{

    public class V_MESSAGEBll
    {
 
        public static V_MESSAGEBll Instance
        {
            get { return SingletonProvider<V_MESSAGEBll>.Instance; }
        }

        //判断编号是否存在
        public bool isSerialNumber(string categoryname, string depid = "")
        {
            var UnitGroupsList = V_MESSAGEDal.Instance.GetAll().ToList();
            return UnitGroupsList.Any(n => n.FID == categoryname && n.FID != depid);
        }

        //判断编号是否存在
        public bool isFID(string categoryname)
        {

            var UnitGroupsList = V_MESSAGEDal.Instance.GetAll().ToList();

            return UnitGroupsList.Any(n => n.FID == categoryname);
        }

        public string GetEasyUIJson(int page=1,int pageSize=15, string startDate = null, string endDate = null, string FRECEIVERID = null)
        {
            return V_MESSAGEDal.Instance.GetEasyUIJson(page,pageSize, startDate, endDate, FRECEIVERID);
        }


    }
        
}
