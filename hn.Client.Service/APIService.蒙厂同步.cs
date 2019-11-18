using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hn.AutoSyncLib;
using hn.Common;

namespace hn.Client.Service
{
    public partial class APIService : IAPIService
    {
        /// <summary>
        /// 同步指定时间的所有提货单和出仓单数据
        /// </summary>
        /// <returns></returns>
        public bool Sync_Today_THD(string rq1,string rq2)
        {

            try
            {
                var token = MC_GetToken.GetInstance().Token;
                var result= MC_PickUpGoods.GetInstance().RequestAndWriteData(token, rq1, rq2)&&MC_OutOfStore.GetInstance().RequestAndWriteData(token,rq1,rq2);
                if (result)
                {
                    MC_PickUpGoods.GetInstance().Call_MN_THD_Update();
                }

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("同步失败");
                LogHelper.WriteLog(ex);
                return false;
            }
        }

    }
}