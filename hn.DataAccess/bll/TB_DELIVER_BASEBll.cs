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
    public class TB_DELIVER_BASEBll
    {
        public static TB_DELIVER_BASEBll Instance
        {
            get { return SingletonProvider<TB_DELIVER_BASEBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_DELIVER_BASEDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(TB_DELIVER_BASEModel model)
        {
            string uid = "0";
            string msg = "厂家发货基地添加失败！";

            if (HasRepetitionName(model))
            {
                msg = "厂家发货基地编号重复！";
            }
            else
            {
                uid = TB_DELIVER_BASEDal.Instance.Insert(model);
                if (uid != "")
                {
                    msg = "厂家发货基地添加成功！";
                }
            }

            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public string Edit(TB_DELIVER_BASEModel d)
        {
            int k=-1;
            string msg = "厂家发货基地编辑失败!";

            if (HasRepetitionName(d))
            {
                msg = "厂家发货基地名称重复！";
            }
            else
            {
                var oldBrand = TB_DELIVER_BASEDal.Instance.Get(d.FID);
                if (oldBrand==null)
                {
                    msg= "厂家发货基地编辑失败!";
                }
                else
                {
                    k = TB_DELIVER_BASEDal.Instance.Update(d);
                    if (k > 0)
                    {
                        msg = "编辑厂家发货基地成功。";
                    }
                }
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        /// <summary>
        /// 判断编号重复
        /// </summary>
        private bool HasRepetitionName(TB_DELIVER_BASEModel model)
        {
            var list=TB_DELIVER_BASEDal.Instance.GetAll().ToList();
            foreach (var item in list)
            {
                if (item.FBASEA_NAME == model.FBASEA_NAME)
                {
                    if (model.FID!="0")//编辑
                    {
                        if (model.FID!= item.FID)
                        {
                            return true;
                        }
                    }
                    else//新增
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
