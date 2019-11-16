using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Core.Dal;
using hn.Core.Model;
using hn.Common;
using hn.Common.Provider;

namespace hn.Core.Bll
{
    public class DicBll
    {
        public static DicBll Instance
        {
            get { return SingletonProvider<DicBll>.Instance; } 
        }

        public string DicCategoryJson()
        {
            return JSONhelper.ToJson(SysDicsDal.Instance.GetAll().ToList().OrderBy(n => n.FID)
                                    .Select(n => new
                                     {
                                         id = n.FID, 
                                         text = n.FClassName +" ["+n.FClassCode+"]", 
                                         iconCls = "icon-bullet_green", 
                                         attributes = new { n.FID,n.FRemark ,n.FClassCode}
                                     })
                                 );
        }


        public string GetDicListBy(string categoryId)
        {
            var dicList = DicDal.Instance.GetListBy(categoryId);
            var list = dicList as List<Dic> ?? dicList.ToList();
            return list.Any() ? JSONhelper.ToJson(list.FindAll(n => n.ParentId == "0")) : "[]";
        }

        public string GetDicListByCode(string code)
        {
            var dicList = DicDal.Instance.GetListByCategoryCode(code);
            return JSONhelper.ToJson(dicList.ToList());
        }

        private bool HasDicCode(string categoryid, string dicCode, string dicid = "0")
        {
            List<Dic> list = DicDal.Instance.GetAll().ToList();
            return list.Any(n => n.Code == dicCode && n.FID!=dicid && n.CategoryId == categoryid);
        }

        public string Add(Dic d)
        {
            if(HasDicCode(d.CategoryId, d.Code))
            {
                return ""; //字典编码已存在
            }

            string i= DicDal.Instance.Insert(d);
            if (i != "")
            {
                LogBll<Dic> log = new LogBll<Dic>();
                d.FID = i;
                log.AddLog(d);
            }
            return i;
        }

        public int Edit(Dic d)
        {
            if (HasDicCode(d.CategoryId,d.Code,d.FID))
            {
                return 0; //字典编码已存在
            }

            Dic oldDic = DicDal.Instance.Get(d.FID);


            int i = DicDal.Instance.Update(d);
            if(i >0)
            {

                LogBll<Dic> log = new LogBll<Dic>();
                log.UpdateLog(oldDic,d);
            }
            return i;
        }

        public int Delete(string dicid)
        {
            Dic d = DicDal.Instance.Get(dicid);
            if(d!=null)
            {
                if(d.children.Any())
                {
                    return 2; //有子节点不能删除
                }
                
                int i =  DicDal.Instance.Delete(dicid);
                if(i >0)
                {
                    LogBll<Dic> log = new LogBll<Dic>();
                    log.DeleteLog(d);
                }
                return i;
            }
            return 0; //参数错误
        }

        public IList<Dic> GetDicIListByCode(string code)
        {
            return DicDal.Instance.GetListByCategoryCode(code).ToList();
        }
    }
}
