using hn.Common;
using hn.Common.Provider;
using hn.Core.Dal;
using hn.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.Core.Bll
{
    public class SysDicsBll
    {
        public static SysDicsBll Instance
        {
            get { return SingletonProvider<SysDicsBll>.Instance; }
        }

        private bool Exists(string categoryid, string dicCode, string dicid = "0")
        {
            List<SysDics> list = SysDicsDal.Instance.GetAll().ToList();
            return list.Any(n => n.FClassCode == dicCode && n.FID != dicid && n.FClassIdent == categoryid);
        }

        public string Add(SysDics entity)
        {
            if (Exists(entity.FClassCode, entity.FClassIdent))
            {
                return ""; //字典编码已存在
            }

            string i = SysDicsDal.Instance.Insert(entity);
            if (i != "")
            {
                LogBll<SysDics> log = new LogBll<SysDics>();
                entity.FID = i;
                log.AddLog(entity);
            }
            return i;
        }

        public int Update(SysDics entity)
        {
            if (Exists(entity.FClassCode, entity.FClassIdent, entity.FID))
            {
                return 0; //字典编码已存在
            }

            SysDics oldDic = SysDicsDal.Instance.Get(entity.FID);


            int i = SysDicsDal.Instance.Update(entity);
            if (i > 0)
            {

                LogBll<SysDics> log = new LogBll<SysDics>();
                log.UpdateLog(oldDic, entity);
            }
            return i;
        }

        public string DicCategoryJson(string FClassIdent)
        {
            return JSONhelper.ToJson(SysDicsDal.Instance.GetListByCategoryCode(FClassIdent).ToList().OrderBy(n => n.FID)
                                    .Select(n => new
                                    {
                                        id = n.FID,
                                        text = n.FClassName + " [" + n.FClassCode + "]",
                                        iconCls = "icon-bullet_green",
                                        attributes = new { n.FID, n.FRemark, n.FClassCode }
                                    })
                                 );
        }

        public string GetDicListByCode(string code)
        {
            var dicList = SysDicsDal.Instance.GetListByCategoryCode(code);
            return JSONhelper.ToJson(dicList.ToList());
        }

        public SysDics GetByCode(string code)
        {
            return SysDicsDal.Instance.GetWhere(new { FCLASSCODE = code }).FirstOrDefault();
        }

        public string GetDicListBy(string categoryId)
        {
            var dicList = SysDicsDal.Instance.GetListBy(categoryId);
            var list = dicList as List<SysDics> ?? dicList.ToList();
            return JSONhelper.ToJson(list);
        }

        public int Delete(string dicid)
        {
            SysDics d = SysDicsDal.Instance.Get(dicid);
            if (d != null)
            {
                int i = SysDicsDal.Instance.Delete(dicid);
                if (i > 0)
                {
                    LogBll<SysDics> log = new LogBll<SysDics>();
                    log.DeleteLog(d);
                }
                return i;
            }
            return 0; //参数错误
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return SysDicsDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

    }
}
