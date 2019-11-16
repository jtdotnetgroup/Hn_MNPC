using System;
using System.Net.Http;
using hn.AutoSyncLib.Model;
using hn.Common;

namespace hn.AutoSyncLib
{

    public  class BaseRequest<T> where T:new()
    {
        protected static T Instance { get; set; }

        protected static object lockobj =new object();

        public MC_Request_BaseParams pars { get; set; }

        public int pageSize { get; set; }

        public BaseRequest(int pageSize=1000)
        {
            this.pageSize = 1000;
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
        public virtual T1 Request<T1, T2>(T2 pars,string url= "https://tms.monalisagroup.com.cn/mapi/doAction") where T1 : MC_Request_BaseResult where T2 : MC_Request_BaseParams
        {
            HttpClient client = new HttpClient();
            client.Timeout=new TimeSpan(0,0,10,0);
            HttpContent content = new FormUrlEncodedContent(pars.ModelToDic<T2>());
            try
            {
                var data = client.PostAsync(url, content).Result;
                string jsonStr = data.Content.ReadAsStringAsync().Result;

                var result =JSONhelper.ConvertToObject <T1>(jsonStr);

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

       

       

    }
}