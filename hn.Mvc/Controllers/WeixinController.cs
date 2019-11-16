using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hn.Core.Dal;
using hn.Core.Model;
using hn.Core.Bll;
using hn.Common;
using hn.Core;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
using hn.DataAccess.dal;
using hn.DataAccess.model;
using System.Text;
using System.IO;
using System.Data;
using hn.Common.Data;
using System.Configuration;

namespace hn.Mvc.Controllers
{
    public class WeixinController : Controller
    {

        /// <summary>
        /// 审批列表接口
        /// </summary>
        /// <returns></returns>
        public string GetAuditList()
        {
            try
            {
                //用户编号
                string user = Request["user"];
                //关键字
                string keyword = Request["keyword"];
                //状态 0:待审核 1:审核通过 空:全部
                string status = Request["status"];
                //页码
                string page = Request["page"];
                //每页显示行数
                int pagesize = Request["pagesize"].ToInt(10);

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT T2.*,T1.FSTATUS AUDIT_STATUS");
                sql.AppendLine("  FROM ICPRBILLAUDIT T1");
                sql.AppendLine(" INNER JOIN V_ICPRBILL T2");
                sql.AppendLine("    ON T1.FBILLID = T2.FID");
                sql.AppendFormat(" WHERE T1.FAUDITOR = '{0}'", user);
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql.AppendFormat("   AND (T2.FPREMISEBRANDNAME || T2.FTYPENAME || T2.FBILLERNAME) LIKE '%{0}%'", keyword);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    sql.AppendFormat("   AND T1.FSTATUS = {0}", status);
                }

                sql.AppendFormat("   AND T1.FSTATUS !=-1 ");

                LogHelper.WriteLog(sql.ToString());

                //查询数据
                DataTable table = DbUtils.Query(sql.ToString());

                //分页
                var pagedata = table.AsEnumerable().Skip((page.ToInt() - 1) * pagesize).Take(pagesize).ToList();

                List<object> list = new List<object>();
                foreach (DataRow row in pagedata)
                {

                    var auditLog = ICPRBILLAUDITDal.Instance.GetJson2(row["FID"].ToStr()).ToList();

                    list.Add(new
                    {
                        FID = row["FID"], //单据ID
                        FBILLNO = row["FBILLNO"], //单据编号
                        FBILLERNAME = row["FBILLERNAME"],//申请人
                        FBILLDATE = row["FBILLDATE"],//申请日期
                        FPREMISENAME = row["FPREMISENAME"],//经营场所
                        FTYPENAME = row["FTYPENAME"],//计划类型
                        FRECEIVINGADDR = row["FRECEIVINGADDR"],//收货方地址
                        FDELIVERYADDR = row["FDELIVERYADDR"],//发货地点
                        FSTATUS = row["AUDIT_STATUS"], //状态
                        AuditLogs = auditLog
                    });
                }

                return JSONhelper.ToJson(new ResultClass()
                {
                    errCode = 0,
                    data = list
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new ResultClass()
                {
                    errCode = -1,
                    errMsg = ex.Message
                });
            }
        }

        /// <summary>
        /// 单据详情查询接口
        /// </summary>
        /// <returns></returns>
        public string GetBillInfo()
        {
            try
            {
                //用户编号
                string user = Request["user"];
                //单据ID
                string billid = Request["billid"];
                //页码
                string page = Request["page"];
                //每页显示行数
                int pagesize = Request["pagesize"].ToInt(10);

                V_ICPRBILLMODEL model = V_ICPRBILLDAL.Instance.Get(billid);
                if (model != null)
                {
                    var entry = V_ICPRBILLENTRYDAL.Instance.GetWhere(new { FPLANID = billid }).ToList();
                    //分页
                    var pagedata = entry.Skip((page.ToInt() - 1) * pagesize).Take(pagesize).ToList();

                    var entrylist = new List<object>();
                    foreach (V_ICPRBILLENTRYMODEL entryModel in pagedata)
                    {
                        entrylist.Add(new
                        {
                            FID = entryModel.FID,
                            FENTRYID = entryModel.FENTRYID,//行号
                            FPRODUCTCODE = entryModel.FPRODUCTCODE,//商品代码
                            FPRODUCTNAME = entryModel.FPRODUCTNAME,//商品名称
                            FMODEL = entryModel.FMODEL,//规格型号
                            FCOLORNO = entryModel.FCOLORNO,//色号
                            FASKQTY = entryModel.FASKQTY,//主单位申请数量
                            FUNITNAME = entryModel.FUNITNAME,//主单位
                            FORDERUNITQTY = entryModel.FORDERUNITQTY,//采购单位申请数量
                            FORDERUNIT = entryModel.FORDERUNIT,//采购单位
                            FNEEDDATE = entryModel.FNEEDDATESTR,//要求发货时间
                            FREMARK = entryModel.FREMARK, //销区备注
                            FSTATUS = entryModel.FSTATUS //单据状态
                        });
                    }

                    var auditdata = ICPRBILLAUDITDal.Instance.GetWhere(new { FBILLID = billid, FAUDITOR = user }).ToList();
                    if (auditdata.Count > 0)
                    {
                        return JSONhelper.ToJson(new ResultClass()
                        {
                            errCode = 0,
                            data = new
                            {
                                FID = model.FID,//单据ID
                                FBILLNO = model.FBILLNO,//单据编号
                                FBILLERNAME = model.FBILLERNAME,//申请人
                                FBILLDATE = model.FBILLDATE,//申请日期
                                FPREMISENAME = model.FPREMISENAME,//经营场所
                                FTYPENAME = model.FTYPENAME,//计划类型
                                FRECEIVINGADDR = model.FRECEIVINGADDR,//收货方地址
                                FDELIVERYADDR = model.FDELIVERYADDR,//发货地点
                                FTRANSNAME = model.FTRANSNAME,//运输方式
                                FSIGN_MAIN = model.SIGN_MAIN,//签约主体
                                FPROJECTNAME = model.FPROJECTNAME,//工程名称
                                FWEIGHT = model.FWEIGHT,//重量合计
                                FSTATUS = model.FSTATUS, //单据状态
                                FAUDITSTATUS = auditdata[0].FSTATUS, //审核状态
                                FENTRYCOUNT = entry.Count,//明细记录总数
                                Entrys = entrylist
                            }
                        });
                    }


                }

                return JSONhelper.ToJson(new ResultClass()
                {
                    errCode = -1,
                    errMsg = "数据不存在！"
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new ResultClass()
                {
                    errCode = -1,
                    errMsg = ex.Message
                });
            }
        }

        /// <summary>
        /// 审批操作返回信息接口
        /// </summary>
        /// <returns></returns>
        public string UpdateAudit()
        {
            try
            {
                //用户编号
                string user = Request["user"];
                //单据ID
                string billid = Request["billid"];
                //审核状态  1：审核通过 2：审核不通过
                string status = Request["status"];
                //审批意见
                string remark = Request["remark"];

                //V_ICPRBILLMODEL model = V_ICPRBILLDAL.Instance.Get(billid);
                //if (model != null)
                //{
                //    var flows = V_ROUTINGDal.Instance.GetWhere(new { FBRANDID = model.FBRANDID, FPREMISEID = model.FPREMISEID, FTYPE = model.FTYPEID }).ToList();
                //    if (flows.Count > 0)
                //    {
                //        //生成审批记录
                //        ICPRBILLAUDITModel audit = new ICPRBILLAUDITModel();
                //        audit.FBILLID = billid;
                //        audit.FBILLTYPE = 1;
                //        audit.FAUDIT_TIME = DateTime.Now;
                //        audit.FFLOWID = flows[0].FID;
                //        audit.FAUDITOR = user;
                //        audit.FSTATUS = status.ToInt();
                //        ICPRBILLAUDITDal.Instance.Insert(audit);

                //        if(flows[0].FAPPROVER_USERNAME1 == user && flows[0].FNODECOUNT > 1)
                //        {
                //            //发送微信模板消息
                //            SendWxtTmpMessage(flows[0].FAPPROVER_USERNAME2);
                //        }
                //        else if (flows[0].FAPPROVER_USERNAME2 == user && flows[0].FNODECOUNT > 2)
                //        {
                //            //发送微信模板消息
                //            SendWxtTmpMessage(flows[0].FAPPROVER_USERNAME3);
                //        }
                //        else
                //        {
                //            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = 2 },new { FID = model.FID });
                //            ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new { FSTATUS = 2 }, new { FPLANID = model.FID });
                //        }
                //    }
                //}

                //更新审核状态
                var result = ICPRBILLAUDITDal.Instance.UpdateWhatWhere(new { FAUDIT_TIME = DateTime.Now, FSTATUS = status, FREMARK = remark }, new { FAUDITOR = user, FBILLID = billid });
                if (result > 0)
                {
                    if (status == "1") // 审核通过
                    {
                        //查询发起审核的其他用户
                        var audituser = ICPRBILLAUDITDal.Instance.GetWhereStr(string.Format(" and FBILLID='{0}' and FSTATUS<=0", billid)).OrderBy(m => m.FSORT).ToList();
                        if (audituser.Count() > 0)
                        {
                            ICPRBILLMODEL billModel = ICPRBILLDAL.Instance.Get(billid);
                            //发送微信模板消息
                            SendWxtTmpMessage(audituser[0].FAUDITOR, billModel.FBILLNO, billModel.FID);

                            audituser[0].FSTATUS = 0;

                            ICPRBILLAUDITDal.Instance.Update(audituser[0]);
                        }
                        else
                        {
                            ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = 3, FCHECKERID = SysVisitor.Instance.UserId, FCHECKDATE = DateTime.Now }, new { FID = billid });
                            ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new { FSTATUS = 3 }, new { FPLANID = billid });
                        }
                    }
                    else if (status == "2") //审核不通过
                    {
                        ICPRBILLDAL.Instance.UpdateWhatWhere(new { FSTATUS = 4, FCHECKERID = SysVisitor.Instance.UserId, FCHECKDATE = DateTime.Now }, new { FID = billid });
                        ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new { FSTATUS = 4 }, new { FPLANID = billid });
                    }

                    return JSONhelper.ToJson(new ResultClass()
                    {
                        errCode = 0
                    });
                }

                return JSONhelper.ToJson(new ResultClass()
                {
                    errCode = -1,
                    errMsg = "数据不存在！"
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return JSONhelper.ToJson(new ResultClass()
                {
                    errCode = -1,
                    errMsg = ex.Message
                });
            }
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool SendWxtTmpMessage(string user, string billno, string billid)
        {
            try
            {
                HttpItem item = new HttpItem();
                item.URL = ConfigurationManager.AppSettings["WxTmpMessageUrl"];
                item.Encoding = Encoding.UTF8;
                item.Method = "POST";
                item.Accept = "application/json";
                item.ContentType = "application/json;charset=utf-8";
                item.PostEncoding = Encoding.UTF8;

                var postdata = new
                {
                    agentid = 68,
                    touser = user,
                    title = "采购申请审批",
                    url = "http://wx.4006002222.com/qyweb/purchaseExamine/purchaseDetail/purchaseDetail.html?fid=" + billid,
                    description = "待审批单号:" + billno,
                    picurl = ""
                };

                item.Postdata = JSONhelper.ToJson(postdata);
                HttpResult result = HttpHelper.Instance.GetHtml(item);
                if (!string.IsNullOrEmpty(result.Html))
                {
                    WxResultClass rs = JSONhelper.ConvertToObject<WxResultClass>(result.Html);
                    if (rs.status == "00000")
                    {
                        return true;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
            }
        }

        public class ResultClass
        {
            public int errCode;
            public string errMsg;
            public object data;
        }


        public class WxResultClass
        {
            public string status;
            public string response;
            public string message;
            public object date;
        }
    }
}
