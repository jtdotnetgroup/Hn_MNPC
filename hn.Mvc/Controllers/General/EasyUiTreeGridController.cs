using hn.Common;
using hn.DataAccess;
using hn.DataAccess.bll;
using hn.DataAccess.Bll;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hn.Mvc.Controllers
{
    public class EasyUiTreeGridController : Controller
    {

        #region 组织架构

        [HttpPost]
        public string TB_ORGANIZATION(bool showDisable = true)
        {
            string parentid = string.IsNullOrEmpty(Request["parentid"]) ? "0" : Request["parentid"];
            var datalist = V_ORGANIZATIONDal.Instance.GetWhere(new { FPARENTALID = parentid });

            List<object> list = new List<object>();
            foreach (var item in datalist)
            {

                if (!showDisable && item.FSTATUS != 1)
                {
                    continue;
                }

                list.Add(new
                {
                    FID = item.FID,
                    FPARENTALID = item.FPARENTALID,
                    FSTATUS = item.FSTATUS,
                    FORGCODE = item.FORGCODE,
                    FORGNAME = item.FORGNAME,
                    FHEADER = item.FHEADER,
                    FHEADERNAME = item.FHEADERNAME,
                    FTYPE = item.FTYPE,
                    FATTRIBUTE1 = item.FATTRIBUTE1,
                    FATTRIBUTE2 = item.FATTRIBUTE2,
                    FATTRIBUTE3 = item.FATTRIBUTE3,
                    FATTRIBUTE4 = item.FATTRIBUTE4,
                    FATTRIBUTE5 = item.FATTRIBUTE5,
                    FATTRIBUTE6 = item.FATTRIBUTE6,
                    FATTRIBUTE7 = item.FATTRIBUTE7,
                    FATTRIBUTE8 = item.FATTRIBUTE8,
                    FATTRIBUTE9 = item.FATTRIBUTE9,
                    FATTRIBUTE10 = item.FATTRIBUTE10,

                    FSTATUSNAME = item.FSTATUSNAME,
                    FTYPENAME = item.FTYPENAME,
                    //children = new List<object>()
                    children = GetChilren(item.FID),
                    state = "closed",

                    //children = TB_ORGANIZATIONRecursion(dataList, item.FID, showDisable),
                    //selected = item.FID == selectID ? "selected" : null,
                });
            }

            //List<V_ORGANIZATIONModel> dataList = V_ORGANIZATIONBll.Instance.GetAll().ToList();

            //List<object> list = new List<object>();

            //foreach (var item in dataList.Where(d => d.FPARENTALID == "0"))
            //{

            //    if (!showDisable && item.FSTATUS != 1)
            //    {
            //        continue;
            //    }

            //    list.Add(new
            //    {
            //        FID = item.FID,
            //        FPARENTALID = item.FPARENTALID,
            //        FSTATUS = item.FSTATUS,
            //        FORGCODE = item.FORGCODE,
            //        FORGNAME = item.FORGNAME,
            //        FHEADER = item.FHEADER,
            //        FHEADERNAME = item.FHEADERNAME,
            //        FTYPE = item.FTYPE,
            //        FATTRIBUTE1 = item.FATTRIBUTE1,
            //        FATTRIBUTE2 = item.FATTRIBUTE2,
            //        FATTRIBUTE3 = item.FATTRIBUTE3,
            //        FATTRIBUTE4 = item.FATTRIBUTE4,
            //        FATTRIBUTE5 = item.FATTRIBUTE5,
            //        FATTRIBUTE6 = item.FATTRIBUTE6,
            //        FATTRIBUTE7 = item.FATTRIBUTE7,
            //        FATTRIBUTE8 = item.FATTRIBUTE8,
            //        FATTRIBUTE9 = item.FATTRIBUTE9,
            //        FATTRIBUTE10 = item.FATTRIBUTE10,

            //        FSTATUSNAME = item.FSTATUSNAME,
            //        FTYPENAME = item.FTYPENAME,

            //        children = TB_ORGANIZATIONRecursion(dataList, item.FID, showDisable),
            //        //selected = item.FID == selectID ? "selected" : null,
            //    });
            //}

            return JSONhelper.ToJson(list);

            //return TB_OrganizationBll.Instance.GetOrganizationTreegridData();
        }

        /// <summary>
        /// 不获取销区数据
        /// </summary>
        /// <param name="showDisable"></param>
        /// <returns></returns>
        [HttpPost]
        public string TB_ORGANIZATIONNoSaleArea(bool showDisable = true,string keywords = null)
        {
            string parentid = string.IsNullOrEmpty(Request["parentid"]) ? "0" : Request["parentid"];
            List<V_ORGANIZATIONModel> datalist = null;

            if (!string.IsNullOrEmpty(keywords))
            {
                datalist = V_ORGANIZATIONDal.Instance.GetAll().ToList();
            }
            else
            {
                datalist = V_ORGANIZATIONDal.Instance.GetWhere(new { FPARENTALID = parentid }).ToList();
            }

            List<object> list = new List<object>();
            foreach (var item in datalist)
            {
                if (!showDisable && item.FSTATUS != 1)
                {
                    continue;
                }
                if (item.FTYPE!=1)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(keywords) || item.FORGNAME.Contains(keywords) || item.FORGCODE.Contains(keywords))
                {
                    list.Add(new
                    {
                        FID = item.FID,
                        FPARENTALID = item.FPARENTALID,
                        FSTATUS = item.FSTATUS,
                        FORGCODE = item.FORGCODE,
                        FORGNAME = item.FORGNAME,
                        FHEADER = item.FHEADER,
                        FHEADERNAME = item.FHEADERNAME,
                        FTYPE = item.FTYPE,
                        FSTATUSNAME = item.FSTATUSNAME,
                        FTYPENAME = item.FTYPENAME,
                        state = "closed",

                    });
                }
            }
            return JSONhelper.ToJson(list);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="isNoSaleArea">是否不需要包含一级二级销区数据</param>
        /// <returns></returns>
        private object GetChilren(string parentid,bool isNoSaleArea=false)
        {
            var datalist = V_ORGANIZATIONDal.Instance.GetWhere(new { FPARENTALID = parentid });

            List<object> list = new List<object>();
            foreach (var item in datalist)
            {
                if (isNoSaleArea&& item.FTYPE!=1)
                {
                    continue;
                }
                list.Add(new
                {
                    FID = item.FID,
                    FPARENTALID = item.FPARENTALID,
                    FSTATUS = item.FSTATUS,
                    FORGCODE = item.FORGCODE,
                    FORGNAME = item.FORGNAME,
                    FHEADER = item.FHEADER,
                    FHEADERNAME = item.FHEADERNAME,
                    FTYPE = item.FTYPE,
                    FATTRIBUTE1 = item.FATTRIBUTE1,
                    FATTRIBUTE2 = item.FATTRIBUTE2,
                    FATTRIBUTE3 = item.FATTRIBUTE3,
                    FATTRIBUTE4 = item.FATTRIBUTE4,
                    FATTRIBUTE5 = item.FATTRIBUTE5,
                    FATTRIBUTE6 = item.FATTRIBUTE6,
                    FATTRIBUTE7 = item.FATTRIBUTE7,
                    FATTRIBUTE8 = item.FATTRIBUTE8,
                    FATTRIBUTE9 = item.FATTRIBUTE9,
                    FATTRIBUTE10 = item.FATTRIBUTE10,

                    FSTATUSNAME = item.FSTATUSNAME,
                    FTYPENAME = item.FTYPENAME,
                    state = "closed",
                    //children = GetChilren(item.FID)
                });
            }

            return list;
        }

        private object TB_ORGANIZATIONRecursion(IEnumerable<V_ORGANIZATIONModel> all, string FPARENTALID, bool showDisable)
        {
            List<object> list = new List<object>();

            foreach (var item in all.Where(a => a.FPARENTALID == FPARENTALID))
            {
                if (!showDisable && item.FSTATUS != 1)
                {
                    continue;
                }

                list.Add(new
                {
                    FID = item.FID,
                    FPARENTALID = item.FPARENTALID,
                    FSTATUS = item.FSTATUS,
                    FORGCODE = item.FORGCODE,
                    FORGNAME = item.FORGNAME,
                    FHEADER = item.FHEADER,
                    FHEADERNAME = item.FHEADERNAME,
                    FTYPE = item.FTYPE,
                    FATTRIBUTE1 = item.FATTRIBUTE1,
                    FATTRIBUTE2 = item.FATTRIBUTE2,
                    FATTRIBUTE3 = item.FATTRIBUTE3,
                    FATTRIBUTE4 = item.FATTRIBUTE4,
                    FATTRIBUTE5 = item.FATTRIBUTE5,
                    FATTRIBUTE6 = item.FATTRIBUTE6,
                    FATTRIBUTE7 = item.FATTRIBUTE7,
                    FATTRIBUTE8 = item.FATTRIBUTE8,
                    FATTRIBUTE9 = item.FATTRIBUTE9,
                    FATTRIBUTE10 = item.FATTRIBUTE10,

                    FSTATUSNAME = item.FSTATUSNAME,
                    FTYPENAME = item.FTYPENAME,

                    children = TB_ORGANIZATIONRecursion(all, item.FID, showDisable),
                    //selected = item.FID == selectID ? "selected" : null,
                });
            }

            return list;
        }

        #endregion

        /// <summary>
        /// 数据字典
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpPost]
        public string SYS_SUBDICS(string parentID = null)
        {
            IEnumerable<SYS_SUBDICSMODEL> dataList = SYS_SUBDICSBLL.Instance.GetByClassID(parentID).OrderBy(m=>m.FNUMBER).ToList();

            List<object> list = new List<object>();

            foreach (var item in dataList.Where(d => (d.FPARENTID.IsNullOrEmpty() || d.FPARENTID == "0")))
            {
                list.Add(new
                {
                    FID = item.FID,
                    FCLASSID = item.FCLASSID,
                    FCREATETIME = item.FCREATETIME,
                    FNAME = item.FNAME,
                    FCREATOR = item.FCREATOR,
                    FNUMBER = item.FNUMBER,
                    FPARENTID = item.FPARENTID,
                    FREMARK = item.FREMARK,
                    FSTATUS = item.FSTATUS,
                    FSTATUSNAME = item.FSTATUSNAME,
                    FUPDATER = item.FUPDATER,
                    FUPDATETIME = item.FUPDATETIME,
                    children = dataList.Where(s => s.FPARENTID == item.FID).Select(s => new
                    {
                        FID = s.FID,
                        FCLASSID = s.FCLASSID,
                        FCREATETIME = s.FCREATETIME,
                        FNAME = s.FNAME,
                        FCREATOR = s.FCREATOR,
                        FNUMBER = s.FNUMBER,
                        FPARENTID = s.FPARENTID,
                        FREMARK = s.FREMARK,
                        FSTATUS = s.FSTATUS,
                        FSTATUSNAME = s.FSTATUSNAME,
                        FUPDATER = s.FUPDATER,
                        FUPDATETIME = s.FUPDATETIME,
                    })
                });
            }

            return JSONhelper.ToJson(list);
        }

        /// <summary>
        /// 数据字典
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpPost]
        public string SYS_SUBDICSByCategory(string category = null,string keywords = null)
        {
            IEnumerable<SYS_SUBDICSMODEL> dataList = SYS_SUBDICSBLL.Instance.GetByCode(category).ToList();

            List<object> list = new List<object>();

            foreach (var item in dataList.Where(d => (d.FPARENTID.IsNullOrEmpty() || d.FPARENTID == "0")))
            {
                if (string.IsNullOrEmpty(keywords) || item.FNAME.Contains(keywords))
                {
                    if (item.FSTATUS == 0) continue;
                    list.Add(new
                    {
                        FID = item.FID,
                        FCLASSID = item.FCLASSID,
                        FCREATETIME = item.FCREATETIME,
                        FNAME = item.FNAME,
                        FCREATOR = item.FCREATOR,
                        FNUMBER = item.FNUMBER,
                        FPARENTID = item.FPARENTID,
                        FREMARK = item.FREMARK,
                        FSTATUS = item.FSTATUS,
                        FSTATUSNAME = item.FSTATUSNAME,
                        FUPDATER = item.FUPDATER,
                        FUPDATETIME = item.FUPDATETIME,
                        children = dataList.Where(s => s.FPARENTID == item.FID).Select(s => new
                        {
                            FID = s.FID,
                            FCLASSID = s.FCLASSID,
                            FCREATETIME = s.FCREATETIME,
                            FNAME = s.FNAME,
                            FCREATOR = s.FCREATOR,
                            FNUMBER = s.FNUMBER,
                            FPARENTID = s.FPARENTID,
                            FREMARK = s.FREMARK,
                            FSTATUS = s.FSTATUS,
                            FSTATUSNAME = s.FSTATUSNAME,
                            FUPDATER = s.FUPDATER,
                            FUPDATETIME = s.FUPDATETIME,
                        })
                    });
                }
            }

            return JSONhelper.ToJson(list);
        }
    }
}