using hn.Common.Provider;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.bll
{
    public class ProductBll
    {
        public static ProductBll Instance
        {
            get { return SingletonProvider<ProductBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return ProductDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        /// <summary>
        /// 根据id获取一条商品资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductModel GetInfo(string id)
        {
            return ProductDal.Instance.Get(id);
        }

        //public string GetNumber(string categoryNo)
        //{
        //    return categoryNo + "." + SysVisitor.GetNewSeq("FGoodNo");
        //}

        ///// <summary>
        ///// 判断用户名是否存在
        ///// </summary>
        ///// <param name="FPRODUCTCODE">商品代码</param>
        ///// <returns></returns>
        //public bool HasGoodsNo(string FPRODUCTCODE)
        //{
        //    return ProductDal.Instance.CountWhere(new { FPRODUCTCODE = FPRODUCTCODE }) > 0;
        //}

        //public string AddGoods(ProductModel u)
        //{
        //    string uid = "0";
        //    string msg = "商品添加失败！";
        //    if (HasGoodsNo(u.FPRODUCTCODE))
        //    {
        //        uid = "-2";
        //        msg = "商品已存在。";
        //    }
        //    else
        //    {
        //        u.FSYNCTIME = DateTime.Now;
        //        u.FUPDATETIME = DateTime.Now;
        //        uid = ProductDal.Instance.Insert(u);
        //        if (uid != "")
        //        {
        //            msg = "添加商品成功！";
        //            LogBll<ProductModel> log = new LogBll<ProductModel>();
        //            u.FID = uid;
        //            log.AddLog(u);
        //        }
        //    }
        //    return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        //}

        //public string EditGoods(ProductModel u)
        //{
        //    int k;
        //    string msg = "商品编辑失败。";
        //    if (HasGoodsNo(u.FPRODUCTCODE))
        //    {
        //        k = -2;
        //        msg = "商品已存在。";
        //    }
        //    else
        //    {
        //        var oldGoods = ProductDal.Instance.Get(u.FID);
        //        k = ProductDal.Instance.Update(u);
        //        if (k > 0)
        //        {
        //            msg = "编辑商品成功。";
        //            LogBll<ProductModel> log = new LogBll<ProductModel>();
        //            log.AddLog(u);
        //        }
        //    }
        //    return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        //}

        public string DeleteAllBat(string ids)
        {
            string[] array = ids.Split(',');
            foreach (string id in array)
            {
                ProductDal.Instance.Delete(id);
            }

            return new JsonMessage { Data = "1", Message = "", Success = true }.ToString();
        }


        public string Add(ProductModel model)
        {
            if (hasGoodsCode(model.FPRODUCTCODE))
            {
                return "商品代码已存在！";
            }

           // model.FSYNCTIME = DateTime.Now;
            model.FUPDATETIME = DateTime.Now;

            return ProductDal.Instance.Insert(model);
        }


        public string Update(ProductModel model)
        {
            ProductModel temp = GetByCode(model.FPRODUCTCODE);

            if(temp != null && temp.FID != model.FID)
            {
                return "商品代码已存在！";
            }

            return ProductDal.Instance.UpdateWhatWhere(new
            {
                FBRANDID = model.FBRANDID,
                FPRODUCTTYPE = model.FPRODUCTTYPE,
                FPRODUCTCODE = model.FPRODUCTCODE,
                FPRODUCTNAME = model.FPRODUCTNAME,
                FMODEL = model.FMODEL,
                //FBASICUNIT = model.FBASICUNIT,
                FPKGFORMAT = model.FPKGFORMAT,
                //FCATEGORYID = model.FCATEGORYID,
                FWEIGHT = model.FWEIGHT,
                FVOLUME = model.FVOLUME,
                FSTATUS = model.FSTATUS,
                FSQUARE = model.FSQUARE,
                FSRCNAME = model.FSRCNAME,

                FSRCCODE = model.FSRCCODE,
                FSRCMODEL = model.FSRCMODEL,
                FSRCUNIT = model.FSRCUNIT,
                FORDERUNIT = model.FORDERUNIT,
                FRATE = model.FRATE,
                FGROUP_NO = model.FGROUP_NO,
                FGROUPNAME = model.FGROUPNAME,
                FGROUPMODEL = model.FGROUPMODEL,
                FGROUPUNIT = model.FGROUPUNIT,

                FUPDATETIME = DateTime.Now,
                //FSRCNUMBER = model.FSRCNUMBER,
                //FSRCNAME = model.FSRCNAME,
                //FSRCMODEL = model.FSRCMODEL,
                //FREMARK = model.FREMARK,
                //FOFTENUNIT = model.FOFTENUNIT,
            }, new
            {
                FID = model.FID,
            }) > 0 ? null:"保存商品资料失败！";
        }

        public ProductModel GetByCode(string FPRODUCTCODE)
        {
            return ProductDal.Instance.GetWhere(new { FPRODUCTCODE = FPRODUCTCODE }).FirstOrDefault();
        }

        /// <summary>
        /// 商品代码是否存在
        /// </summary>
        /// <param name="FPRODUCTCODE"></param>
        /// <returns></returns>
        private bool hasGoodsCode(string FPRODUCTCODE)
        {
            return ProductDal.Instance.CountWhere(new { FPRODUCTCODE = FPRODUCTCODE }) > 0;
        }
    }
}
