using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace hn.DataAccess.bll
{
    public class TB_UnitBll
    {
        public static TB_UnitBll Instance
        {
            get { return SingletonProvider<TB_UnitBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_UnitDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        //public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc", string Organizeid = "")
        //{

        //    return TB_UnitDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order, Organizeid);
        //}

        public IEnumerable<TB_UnitModel> getAllTB_UnitBll()
        {

            List<TB_UnitModel> list = TB_UnitDal.Instance.GetAll().ToList();

            TB_UnitModel temp;

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
        //    return TB_UnitDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        //}

        //判断编号是否存在
        public bool isSerialNumber(string categoryname, string depid = "")
        {
            var UnitGroupsList = TB_UnitDal.Instance.GetAll().ToList();
            return UnitGroupsList.Any(n => n.FNUMBER == categoryname && n.FID != depid);
        }

        //判断编号是否存在
        public bool isFID(string categoryname)
        {

            var UnitGroupsList = TB_UnitDal.Instance.GetAll().ToList();

            return UnitGroupsList.Any(n => n.FID == categoryname);
        }

        //删除
        public JsonMessage DelectBT_UnitModel(String FID)
        {
            int k = 0;
            string msg = "删除失败";

            if (isFID(FID))
            {
                k = TB_UnitDal.Instance.Delete(FID);
                if (k > 0)
                {
                    msg = "删除成功";
                }
            }
            else
            {
                msg = "删除失败";
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != 0 };
        }

        //修改
        public JsonMessage EditBT_UnitGroupModel(TB_UnitModel model)
        {
            string msg = "修改失败！";
            int k = 0;
            if (isFID(model.FID))

                k = TB_UnitDal.Instance.Update(model);
            if (k > 0)
            {
                msg = "修改成功";
            }
            else
            {
                msg = "修改失败！";
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != 0 };
        }

        //添加
        public JsonMessage AddNewBT_UnitModel(TB_UnitModel model)
        {
            string k = "";
            string msg = "添加失败！";
            if (isSerialNumber(model.FNUMBER))
                msg = "编号已存在！";
            else
            {
                k = TB_UnitDal.Instance.Insert(model);
                if (k != "")
                {
                    msg = "添加成功。";
                    model.FID = k;
                }

            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != "" };
        }

        public string GetEasyUIJson(int page = 1, int pageSize = 15, string sort = "FID", string order = "asc")
        {
            return TB_UnitDal.Instance.GetEasyUIJson(page, pageSize, sort, order);
        }

        public string Save(TB_UnitModel model)
        {

            #region 检查

            TB_UnitModel temp = TB_UnitDal.Instance.GetWhere(new { FNUMBER = model.FNUMBER }).FirstOrDefault();

            if (temp != null && temp.FID != model.FID)
            {
                return "编号重复！";
            }

            #endregion

            if (model.FID.IsNullOrEmpty())
            {
                model.FUPDATETIME = DateTime.Now;
                string FID = TB_UnitDal.Instance.Insert(model);

                return FID.IsGuid() ? null : "保存失败！";
            }
            else
            {
                return TB_UnitDal.Instance.UpdateWhatWhere(new {
                    FGROUPID= model.FGROUPID,
                    FNUMBER = model.FNUMBER,
                    FNAME = model.FNAME,
                    FDEFAULT = model.FDEFAULT,
                    FRATE = model.FRATE,
                    FREMARK = model.FREMARK,
                    FUPDATETIME = DateTime.Now,
                }, new
                {
                    FID = model.FID
                }) > 0 ? null : "保存失败！";
            }
        }
    }
}
