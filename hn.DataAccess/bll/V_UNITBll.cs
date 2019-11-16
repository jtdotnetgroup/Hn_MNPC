using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class V_UNITBll
    {
        public static V_UNITBll Instance
        {
            get { return SingletonProvider<V_UNITBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return V_UNITDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc", string Organizeid = "")
        //{

        //    return V_UNITDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order, Organizeid);
        //}

        public IEnumerable<V_UNITModel> getAllV_UNITBll()
        {

            List<V_UNITModel> list = V_UNITDal.Instance.GetAll().ToList();

            V_UNITModel temp;

            //for (int i = 0; i < list.Count; i++)
            //{
            //    temp = list[i];
            //    if (temp.FDefault.Equals("1"))
            //    {
            //        temp.FDefault = "是";
            //    }
            //    else {
            //        temp.FDefault = "否";
            //    }
            //}

            return list.AsEnumerable();
        }

        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        //{
        //    return V_UNITDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        //}

        //判断编号是否存在
        public bool isSerialNumber(string categoryname, string depid = "")
        {
            var UnitGroupsList = V_UNITDal.Instance.GetAll().ToList();
            return UnitGroupsList.Any(n => n.FNUMBER == categoryname && n.FID != depid);
        }

        //判断编号是否存在
        public bool isFID(string categoryname)
        {

            var UnitGroupsList = V_UNITDal.Instance.GetAll().ToList();

            return UnitGroupsList.Any(n => n.FID == categoryname);
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15, string sort = "FID", string order = "asc")
        {
            return V_UNITDal.Instance.GetEasyUIJson(page, pageSize, sort, order);
        }

    }
}
