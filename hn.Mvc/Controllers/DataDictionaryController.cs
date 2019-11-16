using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core;
using hn.Core.Model;
using hn.Common;
using Omu.ValueInjecter;
using hn.Core.Bll;
using hn.Core.Dal;
using hn.DataAccess.Dal;
using System.Data;
using hn.DataAccess.Bll;
using hn.Mvc.Models;
using hn.DataAccess.Model;

namespace hn.Mvc.Controllers
{
    public class DataDictionaryController : BaseController
    {
        //
        // GET: /DataDictionary/

        public ActionResult Index()
        {
            ViewBag.ToolBar = BuildToolbar();
            return View();
        }

        #region 数据字典分类

        /// <summary>
        /// 获取数据字典分类
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string List(int page = 1, int rows = 15, string parentID = null, string parentCode = null, string sort = null, string order = null)
        {
            UserBll.Instance.CheckUserOnlingState();

            //var rpm = GetRpm(context);
            return SYS_SUBDICSBLL.Instance.GetEasyUIJson(page, rows, parentID, parentCode, sort, order);
        }

        [HttpPost]
        public JsonResult GetInfoBySysDics(string id)
        {
            var entity = SYS_DICSBLL.Instance.GetInfoByID(id);
            return Json(entity, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 新增数据字典分类
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSysDics()
        {
            return View();
        }

        [HttpPost]
        public string AddSysDics(SYS_DICSMODEL entity)
        {
            UserBll.Instance.CheckUserOnlingState();
            //var rpm = GetRpm(context);

            //var dc = new SysDics
            //{
            //    FClassCode = rpm.Request("code"),
            //    FClassName = rpm.Request("title"),
            //    //Sortnum = PublicMethod.GetInt(rpm.Request("sortnum")),
            //    FRemark = rpm.Request("remark")
            //};

            string result = SYS_DICSBLL.Instance.Add(entity.FCLASSCODE, entity.FCLASSNAME, entity.FSYSDEFAULT, SysVisitor.Instance.UserId, entity.FREMARK);

            if (result.IsGuid())
            {
                return new JsonMessage { Success = true, Data = result, Message = "保存数据字典完成！" }.ToString();
            }
            else
            {
                return new JsonMessage { Success = false, Data = result, Message = result }.ToString();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSysDics()
        {
            return View();
        }

        [HttpPost]
        public string EditSysDics(SYS_DICSMODEL entity)
        {
            UserBll.Instance.CheckUserOnlingState();

            entity.FUPDATERID = SysVisitor.Instance.UserId;

            string result = SYS_DICSBLL.Instance.Update(entity);

            if (result.IsNullOrEmpty())
            {
                return new JsonMessage { Success = true, Data = result, Message = "修改数据字典完成！" }.ToString();
            }
            else
            {
                return new JsonMessage { Success = false, Data = result, Message = result }.ToString();
            }
            //var msg = "修改成功。";
            //if (!string.IsNullOrEmpty(result))
            //    msg = result;
            //return new JsonMessage { Success = string.IsNullOrEmpty(result), Data = result, Message = msg }.ToString();
        }

        /// <summary>
        /// 删除主字典
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public string DelSysDics(FormCollection context)
        {

            UserBll.Instance.CheckUserOnlingState();
            var rpm = GetRpm(context);

            var id = PublicMethod.GetString(rpm.Request("id"));

            //获取子类型:Dics
            var listSubCode = SYS_DICSBLL.Instance.GetIDByCode(id);
            if (listSubCode != null && listSubCode.Count() > 0)
            {
                #region 删除数据字典大分类之前,要检查是否被使用（检索方式：检查所有用到数据字典的表，检查所有记录工作量貌似太大）—— 2017年3月1日10:40:26 kt

                //foreach (var subCode in listSubCode)
                //{
                //    //客户档案表：行业 客户等级
                //    int num = TB_CustomerDal.Instance.CountWhere("FTrade like '" + subCode.Code + "' or FCustLevel like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }
                //    //员工信息表：职务  技能等级
                //    num += TB_EmployeeDal.Instance.CountWhere("FDuty like '" + subCode.Code + "' or FLevel like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }
                //    //电梯设备档案表：梯型 重要性 梯号FLiftNumber
                //    num += TB_MachineDal.Instance.CountWhere("FType like '" + subCode.Code + "' or FImportance like '" + subCode.Code + "' or FLiftNumber like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }
                //    //合同主表：付款方式 违约金比例 合同类型
                //    num += ICContractDal.Instance.CountWhere("FPayMethod like '" + subCode.Code + "' or FDefaultRate like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }

                //    //保养项目表：保养周期 适用梯型
                //    num += TB_MaintenanceDal.Instance.CountWhere("FClasss like '" + subCode.Code + "' or FSuitable like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }

                //    //维修项目表：适用梯型
                //    num += TB_RepairItemDal.Instance.CountWhere("FSuitable like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }

                //    //保养工单表：工作标准 回访方式
                //    num += ICCareBillDal.Instance.CountWhere("FStandard like '" + subCode.Code + "' or FVisitType like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }

                //    //维修工单表：紧急程度
                //    num += ICRepairBillDal.Instance.CountWhere("FUrgency like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }

                //    //派工记录表：拒绝类型
                //    num += ICWorkBillEntryDal.Instance.CountWhere("FRefuseType like '" + subCode.Code + "'");
                //    if (num > 0)
                //    {
                //        return new JsonMessage { Success = false, Data = "-2" }.ToString();
                //    }
                //}

                #endregion
            }

            int k = SYS_DICSBLL.Instance.Delete(id);
            //var k = 0;
            var msg = "删除成功。";
            if (k <= 0)
                msg = "删除失败。";
            return new JsonMessage
            {
                Success = true,
                Data = k.ToString(),
                Message = msg
            }.ToString();
        }

        #endregion



        #region 数据字典子类

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public string Add(SYS_SUBDICSMODEL entity)
        {
            UserBll.Instance.CheckUserOnlingState();

            string result = SYS_SUBDICSBLL.Instance.Add(
                entity.FCLASSID,
                entity.FNUMBER,
                entity.FNAME,
                entity.FSTATUS,
                SysVisitor.Instance.UserId,
                entity.FREMARK,
                entity.FPARENTID
            );

            if (result.IsGuid())
            {
                return new JsonMessage { Success = true, Data = result, Message = "保存数据字典完成！" }.ToString();
            }
            else
            {
                return new JsonMessage { Success = false, Data = result, Message = result }.ToString();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public string Edit(SYS_SUBDICSMODEL entity)
        {
            UserBll.Instance.CheckUserOnlingState();

            var result = SYS_SUBDICSBLL.Instance.Update(
                entity.FID,
                entity.FNUMBER,
                entity.FNAME,
                entity.FSTATUS,
                SysVisitor.Instance.UserId,
                entity.FREMARK,
                entity.FPARENTID,
                entity.FCLASSID
            );

            if (result.IsNullOrEmpty())
            {
                return new JsonMessage { Success = true, Data = result, Message = "修改数据字典完成！" }.ToString();
            }
            else
            {
                return new JsonMessage { Success = false, Data = result, Message = result }.ToString();
            }
        }

        [HttpPost]
        public string Delete(string id)
        {
            UserBll.Instance.CheckUserOnlingState();

            var result = SYS_SUBDICSBLL.Instance.Delete(id);
            var msg = "删除成功。";
            if (result <= 0)
                msg = "删除失败";
            return new JsonMessage { Success = result != 0, Data = result.ToString(), Message = msg }.ToString();
        }

        [HttpPost]
        public string SubDicsTreeGrid(string parentID)
        {
            IEnumerable<SYS_SUBDICSMODEL> dicList = SYS_SUBDICSBLL.Instance.GetByClassID(parentID).ToList();

            object list = dicList.Where(l => l.FPARENTID.IsNullOrEmpty()).Select(p => new {
                FID = p.FID,
                FCLASSID = p.FCLASSID,
                FCREATETIME = p.FCREATETIME,
                FNAME = p.FNAME,
                FCREATOR = p.FCREATOR,
                FNUMBER = p.FNUMBER,
                FPARENTID = p.FPARENTID,
                FREMARK = p.FREMARK,
                FSTATUS = p.FSTATUS,
                FSTATUSNAME = p.FSTATUSNAME,
                FUPDATER = p.FUPDATER,
                FUPDATETIME = p.FUPDATETIME,
                children = dicList.Where(s => s.FPARENTID == p.FID).Select(s => new
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
                    FUPDATETIME = s.FUPDATETIME
                })
            });

            return JSONhelper.ToJson(list);
        }

        #endregion

        /// <summary>
        /// 校验数据的有效性
        /// </summary>
        /// <param name="model"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool CheckData(Dic model, out string result)
        {
            result = "";
            List<Dic> list = DicDal.Instance.CheckCode(model.Code, model.FID);
            if (list != null && list.Count > 0)
            {
                result = "编码已存在，请重新输入！";
                return false;
            }

            return true;
        }



        private RequestParamModel<Dic> GetRpm(FormCollection context)
        {
            var json = context["json"];
            var rpm = new RequestParamModel<Dic>(context) { CurrentContext = context, Action = Request["action"] };
            if (!string.IsNullOrEmpty(json))
            {
                rpm = JSONhelper.ConvertToObject<RequestParamModel<Dic>>(json);
                rpm.CurrentContext = context;
            }

            return rpm;
        }

        //[HttpPost]
        //public string GetDic(FormCollection collection)
        //{

        //    string categoryId = Request["categoryId"];
        //    if (categoryId == "")
        //    {
        //        categoryId = collection["categoryId"];
        //    }

        //    return DicBll.Instance.GetDicListByCode(categoryId);
        //}

        ///// <summary>
        ///// 获取客户分类数据字典
        ///// </summary>
        ///// <param name="collection"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public string GetDicCombotree(FormCollection collection)
        //{
        //    string categoryId = Request["categoryId"];
        //    if (categoryId == "")
        //    {
        //        categoryId = collection["categoryId"];
        //    }

        //    var nodes = DicBll.Instance.GetDicIListByCode(categoryId);

        //    var treeNodes = from n in nodes
        //                    select
        //                        new { id = n.FID, text = n.Title, children = n.children };

        //    return "";
        //}


        /// <summary>
        /// 检测编码的有效性
        /// </summary>
        /// <param name="num"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CheckCode(string code, string id)
        {
            try
            {
                UserBll.Instance.CheckUserOnlingState();

                List<Dic> list = DicDal.Instance.CheckCode(code, id);
                if (list != null && list.Count > 0)
                {
                    return JSONhelper.ToJson(new { errCode = -1, errMsg = "编码已存在，请重新输入！" });
                }

                return JSONhelper.ToJson(new { errCode = 0 });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new { errCode = -1, errMsg = ex.Message });
            }
        }

    }
}
