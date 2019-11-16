using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace hn.Client
{
    public class Post
    {
        private static CookieContainer cookie = new CookieContainer();

        public static string SendMsg(string name, string postDataStr)
        {
            return HttpPost(Global.WebUrl + name, postDataStr);
        }

        public static string HttpPost(string Url, string postDataStr)
        {
            //HMTL返回字符串
            string resultString;
            //参数字符串数组-编码之后
            byte[] postDateByteArray = Encoding.UTF8.GetBytes(postDataStr.Replace("+", "%2B")); //转化

            //设置请求参数
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Url));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDateByteArray.Length;
            request.Timeout = 10000;

            //请求参数流
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postDateByteArray, 0, postDateByteArray.Length);//写入参数
            requestStream.Close();

            //发送请求
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //获取返回值流
            StreamReader resultSrteam = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

            resultString = resultSrteam.ReadToEnd();
            resultSrteam.Close();
            response.Close();
            requestStream.Close();

            return resultString;
        }

        public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }
    }
}
