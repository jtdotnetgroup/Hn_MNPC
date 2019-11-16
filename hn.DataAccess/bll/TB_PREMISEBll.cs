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
    public class TB_PREMISEBll
    {
        public static TB_PREMISEBll Instance
        {
            get { return SingletonProvider<TB_PREMISEBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return TB_PREMISEDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddPREMISE(TB_PREMISEModel model)
        {
            string uid = "0";
            string msg = "经营场所添加失败！";

            if (HasRepetitionCode(model))
            {
                msg = "经营场所编号重复！";
            }
            else
            {
                uid = TB_PREMISEDal.Instance.Insert(model);
                if (uid != "")
                {
                    msg = "经营场所添加成功！";
                }
            }

            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public string EditPREMISE(TB_PREMISEModel d)
        {
            int k=-1;
            string msg = "经营场所编辑失败!";

            if (HasRepetitionCode(d))
            {
                msg = "经营场所编号重复！";
            }
            else
            {
                var oldBrand = TB_PREMISEDal.Instance.Get(d.FID);
                if (oldBrand==null)
                {
                    msg= "经营场所编辑失败!";
                }
                else
                {
                    k = TB_PREMISEDal.Instance.Update(d);
                    if (k > 0)
                    {
                        msg = "编辑经营场所成功。";
                    }
                }
            }

            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        /// <summary>
        /// 判断编号重复
        /// </summary>
        private bool HasRepetitionCode(TB_PREMISEModel model)
        {
            var list=TB_PREMISEDal.Instance.GetAll().ToList();
            foreach (var item in list)
            {
                if (item.FCODE== model.FCODE)
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
