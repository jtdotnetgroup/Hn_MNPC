using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hn.AutoSyncLib.Common;
using hn.AutoSyncLib.Model;
using Newtonsoft.Json;

namespace hn.AutoSyncLib
{
    public class MC_PickUpGoods:BaseRequest<MC_PickUpGoods>, ISync
    {
        public async Task<bool> RequestAndWriteData(MC_getToken_Result token, string rq1, string rq2, int pagesize=1000, int pageindex = 1)
        {

            var startDate = DateTime.Parse(rq1);
            var endDate = DateTime.Parse(rq2);

            //if (startDate == DateTime.Now.Date)
            //{
            //    await Console.Out.WriteLineAsync("时间未到提货单暂时不同步");
            //    return false;
            //}

            LogHelper.LogInfo("开始同步");
            await Console.Out.WriteLineAsync("提货单开始同步");

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

                        var pars = new MC_PickUpGoods_Params(token.token, rq1, rq2, pagesize, pageindex);

                        parJson = JsonConvert.SerializeObject(pars);

                        
                        var result =
                            Request<MC_PickUpGoods_Result, MC_PickUpGoods_Params>(
                                new MC_PickUpGoods_Params(token.token, rq1, rq2, pagesize, pageindex)).Result;

                        total = result.TotalCount;

                        var msg = string.Format(logstr, parJson, total,
                            result.resultInfo.Count, "");

                        LogHelper.LogInfo(msg);
                        await Console.Out.WriteLineAsync(msg);

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
                                string sql = "SELECT COUNT(*) FROM MN_THD WHERE AUTOID=:AUTOID";
                                var par=new Dictionary<string,object>();
                                par.Add(":AUTOID",row.autoId);

                                var count = helper.ExecuteScalar(sql, par);

                                if (Convert.ToInt32( count) > 0)
                                {
                                    string where = "AND AUTOID=:AUTOID";
                                    helper.Update(row, where);
                                    continue;
                                }

                                helper.Insert(row);
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

                return false;
            }

        }

        public async Task<bool> SyncData_EveryDate(MC_getToken_Result token)
        {
            string sql = "SELECT MAX(RQ) FROM MN_THD";

            var dbdate = helper.ExecuteScalar(sql, new Dictionary<string, object>()).ToString();

            var startDate = string.IsNullOrEmpty(dbdate) ?
                DateTime.Parse("2019/05/01").ToString("yyyy/MM/dd")
                : DateTime.Parse(dbdate).Date.ToString("yyyy/MM/dd");

            var endDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy/MM/dd");

            int pageindex = 1;

            var result= await RequestAndWriteData(token, startDate, endDate, pageSize);
            Call_MN_THD_Update();
            
            return result;
        }

        public void Call_MN_THD_Update()
        {
            helper.ExecuteNonQuery("call mn_thd_update()", new Dictionary<string, object>());
        }
    }
}