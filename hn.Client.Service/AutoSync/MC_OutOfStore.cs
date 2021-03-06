﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hn.AutoSyncLib.Model;
using Newtonsoft.Json;
using hn.Common;
using hn.Common.Data;

namespace hn.AutoSyncLib
{
    public class MC_OutOfStore : BaseRequest<MC_OutOfStore>
    {
        public MC_OutOfStore_Params parm { get; set; }

        public bool RequestAndWriteData(MC_getToken_Result token, string rq1, string rq2, int pagesize=1000, int pageindex = 1)
        {
            
            var startDate = DateTime.Parse(rq1);
            var endDate = DateTime.Parse(rq2);

            //if (startDate == DateTime.Now.Date)
            //{
            //    Console.Out.WriteLineAsync("时间未到出仓单暂时不同步");
            //    return false;
            //}

            LogHelper.WriteLog("出仓单开始同步");

            string parJson = "";
            const string logstr = "参数：{0}\r\n返回结果：总条数【{1}】，当前页共【{2}】条记录\r\n异常：{3}";

            try
            {
                do
                {
                    rq1 = startDate.ToString("yyyy/MM/dd");
                    rq2 = startDate.AddDays(1) <= endDate ? startDate.AddDays(1).ToString("yyyy/MM/dd") : rq1;

                    var pagecount = 0;
                    var total = 0;
                    pageindex = 1;

                    do
                    {

                        var pars = new MC_OutOfStore_Params(token.token, rq1, rq2, pagesize, pageindex);

                        parJson = JsonConvert.SerializeObject(pars);
                        
                        var result =  
                            Request<MC_OutOfStore_Result, MC_OutOfStore_Params>(
                                pars);

                        total = result.TotalCount;

                        var msg = string.Format(logstr, parJson, total,
                            result.resultInfo.Count, "");

                        LogHelper.WriteLog(msg);

                        if (pagecount == 0)
                        {
                            pagecount = total / pagesize;

                            if (total % pagesize > 0)
                            {
                                pagecount++;
                            }
                        }
                        //写入数据库
                        if (result.resultInfo.Count > 0)
                        {
                            //并发写入
                            foreach (var row in result.resultInfo.AsParallel())
                            {

                                string sql = "SELECT COUNT(*) FROM MN_CKD WHERE 1=1";

                                row.ComputeFID();

                                var where = " AND FID='" + row.FID+"'";
                                sql += where;

                                var count = DbUtils.CountBySQL(sql);

                                var data = new MC_OutofStore_ResultInfo(row);

                                if (Convert.ToInt32(count) > 0)
                                {
                                    DbUtils.UpdateWithWhere<MC_OutofStore_ResultInfo>(data, where);
                                    continue;
                                }

                                DbUtils.InsertObj(data);
                            }
                        }
                        pageindex++;
                    } while (pageindex <= pagecount);

                    startDate = startDate.AddDays(2);

                } while (startDate <= endDate);


                return true;
            }
            catch (Exception e)
            {
                var msg = string.Format(logstr, parJson, 0,
                    0, e);

                LogHelper.WriteLog(msg);
                return false;
            }

        }

        public bool SyncData_EveryDate(MC_getToken_Result token)
        {
            string sql = "SELECT MAX(RQ) FROM MN_CKD";

            var dbdate = DbUtils.GetExecuteScalarWhere(sql, null).ToString();

            var startDate =string.IsNullOrEmpty(dbdate)?
                DateTime.Parse("2019/05/01").ToString("yyyy/MM/dd")
                : DateTime.Parse(dbdate).Date.ToString("yyyy/MM/dd");

            var endDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy/MM/dd");


            return RequestAndWriteData(token, startDate, endDate, pageSize);

        }

    }
}
