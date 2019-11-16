using hn.Common;
using hn.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace hn.Mvc
{
    public class OmsHelper
    {
        /// <summary>
        /// 预约成功
        /// </summary>
        public const string HDDL_STATUS_ORDERED = "30";
        /// <summary>
        /// 预约失败
        /// </summary>
        public const string HDDL_STATUS_ORDERED_FAILURE = "35";
        /// <summary>
        /// 拒单01
        /// </summary>
        public const string HDDL_STATUS_REJECT = "40";
        /// <summary>
        /// 已签到
        /// </summary>
        public const string HDDL_STATUS_SIGN = "80";
        /// <summary>
        /// 已完成
        /// </summary>
        public const string HDDL_STATUS_FINISHED = "90";

        /// <summary>
        /// APP工单状态数据同步到OMS
        /// </summary>
        /// <param name="paramer"></param>
        public static string PostDataToOms(OmsParamer paramer)
        {
            try
            {
                paramer.FormatNullValue();

                string jsonData = JSONhelper.ToJson(paramer).Replace(@"\n", ""); ;
                LogHelper.WriteLog("OMS:开始同步工单状态到OMS系统，同步的数据：" + jsonData);

                if (!string.IsNullOrEmpty(paramer.dispatchNo))
                {
                    HttpItem item = new HttpItem();
                    item.URL = SysVisitor.Instance.OmsHost;
                    item.Method = "POST";
                    item.Postdata = StringHelper.ToBase64("data=" + jsonData);
                    //  LogHelper.WriteLog("data=" + item.Postdata);
                    hn.Common.HttpResult request = HttpHelper.Instance.GetHtml(item);
                    // LogHelper.WriteLog(request.Html);
                    LogHelper.WriteLog("OMS:结束同步工单状态到OMS系统，接收到返回数据：" + request.Html);
                    return request.Html;
                }
                else {
                    return "";
                }

               
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return ex.Message;
            }
        }


    }

    public class OmsParamer
    {
        public string dispatchNo { get; set; }
        public string operateTime { get; set; }
        public string operatePerson { get; set; }
        public string operateStatus { get; set; }
        public string operateRemark { get; set; }
        public string bookTime { get; set; }
        public string signLat { get; set; }
        public string signLng { get; set; }
        public string signAddress { get; set; }
        public string signType { get; set; }

        public List<OmsImageParamer> imageInfo { get; set; }
        public string Str1 { get; set; }
        public string Str2 { get; set; }
        public string Str3 { get; set; }

        public string ToPostData()
        {
            List<string> list = new List<string>();
            Type type = this.GetType();
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(this, null);
                if (obj != null)
                {
                    list.Add(string.Format("{0}={1}", i.Name, PublicMethod.GetString(obj)));
                }
                string name = i.Name;
            }

            return string.Join("&", list.ToArray());
        }

        public void FormatNullValue()
        {
            dispatchNo = PublicMethod.GetString(dispatchNo);
            operateTime = PublicMethod.GetString(operateTime);
            operatePerson = PublicMethod.GetString(operatePerson);
            operateStatus = PublicMethod.GetString(operateStatus);
            operateRemark = PublicMethod.GetString(operateRemark);
            bookTime = PublicMethod.GetString(bookTime);
            signLat = PublicMethod.GetString(signLat);
            signLng = PublicMethod.GetString(signLng);
            signAddress = PublicMethod.GetString(signAddress);
            signType = PublicMethod.GetString(signType);
            Str1 = PublicMethod.GetString(Str1);
            Str2 = PublicMethod.GetString(Str2);
            Str3 = PublicMethod.GetString(Str3);

            if (imageInfo == null)
            {
                imageInfo = new List<OmsImageParamer>();
                imageInfo.Add(new OmsImageParamer() { fileContent = "", fileName = "", havaPhoto  = "" });
            }
        }
    }

    public class OmsImageParamer
    {
        [DefaultValue("")]
        public string havaPhoto { get; set; }
        [DefaultValue("")]
        public string fileContent { get; set; }
        [DefaultValue("")]
        public string fileName { get; set; }
    }
}