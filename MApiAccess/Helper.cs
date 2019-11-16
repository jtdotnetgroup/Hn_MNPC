
using hn.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MApiAccess
{
    public static class Helper
    {

        public static string url = "https://tms.monalisagroup.com.cn/mapi/doAction";
        public static string PostData(string strPostData)
        {
            LogHelper.WriteLog("start postData=");
            LogHelper.WriteLog(strPostData);
            LogHelper.WriteLog("end postData!");
            HttpWebRequest r = HttpWebRequest.Create(url) as HttpWebRequest;
            r.Method = "POST";
            r.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            r.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            r.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            string postData = strPostData;
            byte[] data = Encoding.UTF8.GetBytes(postData);
            r.ContentLength = data.Length;
            Stream newStream = r.GetRequestStream();
            // Send the data.  
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            System.Net.HttpWebResponse response = r.GetResponse() as System.Net.HttpWebResponse;
            string tempresult = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
            return tempresult;
        }
        public static string getMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        public static string getProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}={1}&", name, value);
                }
              
            }
            return tStr;
        }
    }
}
