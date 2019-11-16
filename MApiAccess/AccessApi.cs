using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MApiModel;
using System.Net.Http;
using hn.Common;

namespace MApiAccess
{
    public class AccessApi
    {
        public MApiModel.recToken.Rootobject AccessApi1(MApiModel.api1.Rootobject api1)
        {
            string strParam = Helper.getProperties<MApiModel.api1.Rootobject>(api1);
            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recToken.Rootobject>(result);
        }


        public MApiModel.recApi2.Rootobject AccessApi2(MApiModel.api2.Rootobject api2)
        {
            string strParam = Helper.getProperties<MApiModel.api2.Rootobject>(api2);
            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recApi2.Rootobject>(result);
        }

        public MApiModel.recApi3.Rootobject AccessApi3(MApiModel.api3.Rootobject api3)
        {
            // string strParam = Helper.getProperties<MApiModel.api3.Rootobject>(api3);
            List<string> listData = new List<string>();
            string strData = "";
            for (int i = 0; i < api3.data.Length; i++)
            {
                strData += JsonHelper.ObjectToJson(api3.data[i]);
                if (i != api3.data.Length - 1) strData += ",";
            }



            string strParam = "comid="+api3.comid+"&action=" + api3.action + "&token=" + api3.token + "&data=[" + strData + "]";

            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recApi3.Rootobject>(result);
        }

        public MApiModel.recApi4.Rootobject AccessApi4(MApiModel.api4.Rootobject api4)
        {
            string strParam = Helper.getProperties<MApiModel.api4.Rootobject>(api4);
            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recApi4.Rootobject>(result);
        }

        public MApiModel.recApi5.Rootobject AccessApi5(MApiModel.api5.Rootobject api5)
        {
            string strParam = Helper.getProperties<MApiModel.api5.Rootobject>(api5);
            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recApi5.Rootobject>(result);
        }

        public MApiModel.recApi6.Rootobject AccessApi6(MApiModel.api6.Rootobject api6)
        {
            string strParam = Helper.getProperties<MApiModel.api6.Rootobject>(api6);
            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recApi6.Rootobject>(result);
        }

        public MApiModel.recApi7.Rootobject AccessApi7(MApiModel.api7.Rootobject api7)
        {
            string strParam = Helper.getProperties<MApiModel.api7.Rootobject>(api7);
            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recApi7.Rootobject>(result);
        }
        public MApiModel.recApi8.Rootobject AccessApi8(MApiModel.api8.Rootobject api7)
        {
            string strParam = Helper.getProperties<MApiModel.api8.Rootobject>(api7);
            string result = Helper.PostData(strParam);
            hn.Common.LogHelper.WriteLog(result);
            return JsonHelper.ToObject<MApiModel.recApi8.Rootobject>(result);
        }

        public MApiModel.recApi24.Rootobject AccessApi24(MApiModel.api24.Rootobject api7)
        {
            string strParam = Helper.getProperties<MApiModel.api24.Rootobject>(api7);
            string result = Helper.PostData(strParam);
            hn.Common.LogHelper.WriteLog(result);
            return JsonHelper.ToObject<MApiModel.recApi24.Rootobject>(result);
        }

        public MApiModel.recApi9.Rootobject AccessApi9(MApiModel.api9.Rootobject api9)
        {
            string strParam = Helper.getProperties<MApiModel.api9.Rootobject>(api9);
            string result = Helper.PostData(strParam);
            hn.Common.LogHelper.WriteLog(result);
            return JsonHelper.ToObject<MApiModel.recApi9.Rootobject>(result);
        }
        public MApiModel.recApi12.Rootobject AccessApi12(MApiModel.api12.Rootobject api12)
        {

            List<string> listData = new List<string>();
            string strData = "";
            for (int i = 0; i < api12.data.Length; i++)
            {
                strData += JsonHelper.ObjectToJson(api12.data[i]);
                if (i != api12.data.Length - 1) strData += ",";
            }


            string strParam = "comid=" + api12.comid + "&action=" + api12.action + "&token=" + api12.token + "&data=[" + strData + "]";

            LogHelper.WriteLog("POSTDATA:" + strParam);

            string result = Helper.PostData(strParam);
            return JsonHelper.ToObject<MApiModel.recApi12.Rootobject>(result);

            //var url = "https://tms.monalisagroup.com.cn/mapi/doAction";

            //HttpClient client = new HttpClient();
            //client.Timeout = new TimeSpan(0, 0, 10, 0);
            //HttpContent content = new FormUrlEncodedContent(api12.ModelToDic<MApiModel.api12.Rootobject>());
            //try
            //{
            //    var data = client.PostAsync(url, content).Result;
            //    string jsonStr = data.Content.ReadAsStringAsync().Result;

            //    var result = JSONhelper.ConvertToObject<MApiModel.recApi12.Rootobject>(jsonStr);

            //    return result;

            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

    }
}
