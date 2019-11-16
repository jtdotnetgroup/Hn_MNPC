using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using hn.AutoSyncLib.Common;
using hn.AutoSyncLib.Model;
using Newtonsoft.Json;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace hn.AutoSyncLib
{

    public  class BaseRequest<T> where T:new()
    {
        protected static T Instance { get; set; }

        protected static object lockobj =new object();

        public MC_Request_BaseParams pars { get; set; }

        protected  string conStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        protected OracleDBHelper helper { get; set; }

        public int pageSize { get; set; }

        public BaseRequest(int pageSize=1000)
        {
            this.pageSize = 1000;
            this.helper=new OracleDBHelper(conStr);
        }

        public static T GetInstance()
        {
            lock (lockobj)
            {
                if (Instance == null)
                {
                    lock (lockobj)
                    {
                        Instance = new T();
                    }
                }

                return Instance;
            }
        }



        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <typeparam name="T1">返回结果的类型</typeparam>
        /// <typeparam name="T2">请求参数类型</typeparam>
        /// <param name="pars">请求参数</param>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public virtual async Task<T1> Request<T1, T2>(T2 pars,string url= "https://tms.monalisagroup.com.cn/mapi/doAction") where T1 : MC_Request_BaseResult where T2 : MC_Request_BaseParams
        {
            HttpClient client = new HttpClient();
            client.Timeout=new TimeSpan(0,0,10,0);
            HttpContent content = new FormUrlEncodedContent(pars.ModelToDic<T2>());
            try
            {
                var data = await client.PostAsync(url, content);
                string jsonStr = await data.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T1>(jsonStr);

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

       

       

    }
}