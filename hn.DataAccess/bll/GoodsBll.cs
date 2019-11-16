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
    public class GoodsBll
    {
        public static GoodsBll Instance
        {
            get { return SingletonProvider<GoodsBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return GoodsDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetNumber(string categoryNo)
        {
            return categoryNo + "." + SysVisitor.GetNewSeq("FGoodNo");
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="Goodsname">用户名</param>
        /// <param name="GoodsId">用户Id,默认是添加，否则为修改</param>
        /// <returns></returns>
        public bool HasGoodsNo(string name, string id = "0")
        {
            var Goodss = from n in GoodsDal.Instance.GetAll()
                          where n.GOODS_NAME == name && n.FID != id
                          select n;
            return Goodss.Any();
        }

        public string AddGoods(GoodsModel u)
        {
            string uid = "0";
            string msg = "商品添加失败！";
            if (HasGoodsNo(u.GOODS_NAME, u.FID))
            {
                uid = "-2";
                msg = "商品已存在。";
            }
            else
            {
                uid = GoodsDal.Instance.Insert(u);
                if (uid != "")
                {
                    msg = "添加商品成功！";
                    LogBll<GoodsModel> log = new LogBll<GoodsModel>();
                    u.FID = uid;
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = uid.ToString(), Message = msg, Success = uid != "" }.ToString();
        }

        public string EditGoods(GoodsModel u)
        {
            int k;
            string msg = "商品编辑失败。";
            if (HasGoodsNo(u.GOODS_NAME, u.FID))
            {
                k = -2;
                msg = "商品已存在。";
            }
            else
            {
                var oldGoods = GoodsDal.Instance.Get(u.FID);
                k = GoodsDal.Instance.Update(u);
                if (k > 0)
                {
                    msg = "编辑商品成功。";
                    LogBll<GoodsModel> log = new LogBll<GoodsModel>();
                    log.AddLog(u);
                }
            }
            return new JsonMessage { Data = k.ToString(), Message = msg, Success = k > 0 }.ToString();
        }

        public string DeleteAllBat(string ids)
        {
            string[] array = ids.Split(',');
            foreach (string id in array)
            {
                GoodsDal.Instance.Delete(id);
            }

            return new JsonMessage { Data = "1", Message = "", Success = true }.ToString();
        }
    }

}