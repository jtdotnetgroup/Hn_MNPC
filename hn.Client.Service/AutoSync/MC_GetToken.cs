using System;
using hn.AutoSyncLib.Model;
using hn.Client.Service;

namespace hn.AutoSyncLib
{
    public class MC_GetToken:BaseRequest<MC_GetToken>
    {
        private  MC_getToken_Result token { get; set; }

        public  MC_getToken_Result Token
        {
            get
            {
                CheckToken();
                return token;
            }

            set { token = value; }
        }

        private void   Request(string url = "https://tms.monalisagroup.com.cn/mapi/doAction")
        {
           token = Request<MC_getToken_Result, MC_getToken_Params>(new MC_getToken_Params(), url);
        }

        private void CheckToken()
        {
            if (token == null)
            {
                 LogHelper.WriteLog("token过期，更新token");
                 Request();
                return;
            }

            var endDate = DateTime.Parse(token.tokenInfo.endDate);
            if (endDate <= DateTime.Now)
            {
                 Request();
            }
        }
    }
}