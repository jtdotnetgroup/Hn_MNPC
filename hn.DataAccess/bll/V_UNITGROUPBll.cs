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
    public class V_UNITGROUPBll
    {


        public static V_UNITGROUPBll Instance
        {
            get { return SingletonProvider<V_UNITGROUPBll>.Instance; }
        }


        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return V_UNITGROUPDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }



        //判断编号是否存在
        public bool isSerialNumber(string categoryname, string depid = "")
        {
            var UnitGroupsList = V_UNITGROUPDal.Instance.GetAll().ToList();
            return UnitGroupsList.Any(n => n.FUNITID == categoryname && n.FID != depid);
        }

        //判断编号是否存在
        public bool isFID(string categoryname)
        {

            var UnitGroupsList = V_UNITGROUPDal.Instance.GetAll().ToList();

            return UnitGroupsList.Any(n => n.FID == categoryname);
        }

        public string GetAllBT_UnitGroupModel()
        {

            V_UNITGROUPDal Dal = new V_UNITGROUPDal();

            return JSONhelper.ToJson(Dal.GetChildren());

        }

        //删除
        public JsonMessage DelectBT_UnitGroupModel(String FID)
        {
            int k = 0;
            string msg = "删除失败";

            if (isFID(FID))
            {
                k = V_UNITGROUPDal.Instance.Delete(FID);
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
        public JsonMessage EditBT_UnitGroupModel(V_UNITGROUPModel model)
        {
            string msg = "修改失败！";
            int k = 0;
            if (isFID(model.FID))

                k = V_UNITGROUPDal.Instance.Update(model);
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
        public JsonMessage AddNewBT_UnitGroupModel(V_UNITGROUPModel model)
        {
            string k = "";
            string msg = "添加失败！";
            if (isSerialNumber(model.FUNITID))
                msg = "编号已存在！";

            else
            {
                k = V_UNITGROUPDal.Instance.Insert(model);
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
        public string Save(V_UNITGROUPModel model)
        {

            #region 检测

            V_UNITGROUPModel temp = V_UNITGROUPDal.Instance.GetWhere(new { FNUMBER = model.FUNITID }).FirstOrDefault();

            if (temp != null && temp.FID != model.FID)
            {
                return "编号已存在！";
            }

            #endregion

            if (model.FID.IsNullOrEmpty())
            {
                model.FUPDATETIME = DateTime.Now;

                string FID = V_UNITGROUPDal.Instance.Insert(model);

                return FID.IsGuid() ? null : "保存失败！";
            }
            else
            {
                return V_UNITGROUPDal.Instance.UpdateWhatWhere(new
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
