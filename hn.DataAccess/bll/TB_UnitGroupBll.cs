using hn.Common;
using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class TB_UnitGroupBll
    {


        public static TB_UnitGroupBll Instance
        {
            get { return SingletonProvider<TB_UnitGroupBll>.Instance; }
        }


        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_UnitGroupDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }



        //判断编号是否存在
        public bool isSerialNumber(string categoryname, string depid = "")
        {
            var UnitGroupsList = TB_UnitGroupDal.Instance.GetAll().ToList();
            return UnitGroupsList.Any(n => n.FUNITID == categoryname && n.FID != depid);
        }

        //判断编号是否存在
        public bool isFID(string categoryname)
        {

            var UnitGroupsList = TB_UnitGroupDal.Instance.GetAll().ToList();

            return UnitGroupsList.Any(n => n.FID == categoryname);
        }

        public string GetAllBT_UnitGroupModel()
        {

            TB_UnitGroupDal Dal = new TB_UnitGroupDal();

            return JSONhelper.ToJson(Dal.GetChildren());

        }

        //删除
        public JsonMessage DelectBT_UnitGroupModel(String FID)
        {
            int k = 0;
            string msg = "删除失败";

            if (isFID(FID))
            {
                k = TB_UnitGroupDal.Instance.Delete(FID);
                if (k > 0)
                {
                    msg = "删除成功";
                }
            }
            else {
                msg = "删除失败";
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != 0 };
        }

        //修改
        public JsonMessage EditBT_UnitGroupModel(TB_UnitGroupModel model)
        {
            string msg = "修改失败！";
            int k = 0;
            if (isFID(model.FID))

                k = TB_UnitGroupDal.Instance.Update(model);
            if (k > 0)
            {
                msg = "修改成功";
            }
            else {
                msg = "修改失败！";
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != 0 };
        }

        //添加
        public JsonMessage AddNewBT_UnitGroupModel(TB_UnitGroupModel model)
        {
            string k = "";
            string msg = "添加失败！";
            if (isSerialNumber(model.FUNITID))
                msg = "编号已存在！";

            else
            {
                k = TB_UnitGroupDal.Instance.Insert(model);
                if (k != "")
                {
                    msg = "添加成功。";
                    model.FID = k;
                }

            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k != "" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Save(TB_UnitGroupModel model)
        {

            #region 检测

            TB_UnitGroupModel temp = TB_UnitGroupDal.Instance.GetWhere(new { FNUMBER = model.FUNITID }).FirstOrDefault();

            if (temp != null && temp.FID != model.FID)
            {
                return "编号已存在！";
            }

            #endregion

            if (model.FID.IsNullOrEmpty())
            {
                model.FUPDATETIME = DateTime.Now;

                string FID = TB_UnitGroupDal.Instance.Insert(model);

                return FID.IsGuid() ? null : "保存失败！";
            }
            else
            {
                return TB_UnitGroupDal.Instance.UpdateWhatWhere(new
                {
                    FNUMBER = model.FUNITID,
                    FNAME = model.FUNITNAME,
                    //FDEFALTUNITID = model.FDEFALTUNITID,
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
