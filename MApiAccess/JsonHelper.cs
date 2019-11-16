using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MApiAccess
{
    public class JsonHelper
    {


        /// <summary>
        /// IList转Json
        /// </summary>
        /// <param name="dt">IList</param>
        /// <param name="dtName">json名</param>
        /// <returns></returns>
        public static string ListToJson<T>(IList list)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    sb.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        sb.Append("\"");
                        sb.Append(pi[j].Name.ToString());
                        sb.Append("\":\"");
                        if (pi[j].GetValue(list[i], null) != null && pi[j].GetValue(list[i], null) != DBNull.Value && pi[j].GetValue(list[i], null).ToString() != "")
                        {
                            sb.Append(pi[j].GetValue(list[i], null)).Replace("\\", "/");
                        }
                        else
                        {
                            sb.Append("");
                        }
                        sb.Append("\",");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            return JsonCharFilter(sb.ToString());

        }

        /// <summary>  
        /// Json特符字符过滤
        /// </summary>  
        /// <param name="sourceStr">要过滤的源字符串</param>  
        /// <returns>返回过滤的字符串</returns>  
        private static string JsonCharFilter(string sourceStr)
        {
            return sourceStr;
        }
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string json = "";
                if (t != null)
                {
                    sb.Append("{");
                    PropertyInfo[] properties = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in properties)
                    {
                        sb.Append("\"" + pi.Name.ToString() + "\"");
                        sb.Append(":");
                        sb.Append("\"" + pi.GetValue(t, null) + "\"");
                        sb.Append(",");
                    }
                    json = sb.ToString().TrimEnd(',');
                    json += "}";
                }
                return json;
            }
            catch (Exception ex)
            {

                return "";
            }
        }
        /// <summary>
        /// IList转Json
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DropToJson<T>(IList list, string jsonName, long ItemTotal)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"ItemCount\":" + ItemTotal + ",");
                sb.Append("\"CountNum\":" + list.Count + ",");
                sb.Append("\"Items\": [");
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] pi = obj.GetType().GetProperties();
                        sb.Append("{");
                        for (int j = 0; j < pi.Length; j++)
                        {
                            sb.Append("\"");
                            sb.Append(pi[j].Name.ToString());
                            sb.Append("\":\"");
                            if (pi[j].GetValue(list[i], null) != null && pi[j].GetValue(list[i], null) != DBNull.Value && pi[j].GetValue(list[i], null).ToString() != "")
                            {
                                sb.Append(pi[j].GetValue(list[i], null)).Replace("\\", "/");
                            }
                            else
                            {
                                sb.Append("");
                            }
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                sb.Append("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {

                return "";
            }
        }

        /// <summary>反序列化</summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jText">json字符串</param>
        /// <returns>类型数据</returns>
        public static T ToObject<T>(string jText)
        {
            return (T)JsonConvert.DeserializeObject(jText, typeof(T));
        }


    }
}
