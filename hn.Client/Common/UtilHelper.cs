using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;

namespace hn.Client
{
    public class UtilHelper
    {
        /// <summary>
        /// 过滤特殊在字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FilterString(string value)
        {
            return ConvertSQL(value);
        }

        public static string FormatDate(object value, string format)
        {
            return DateTime.Parse(Convert.ToString(value)).ToString(format);
        }

        /// <summary>
        /// 自动拼接单引号
        /// </summary>
        /// <param name="i_value"></param>
        /// <returns></returns>
        public static string AllAgreeSql(string i_value)
        {
            i_value = ConvertSQL(i_value);
            return string.Format("'{0}'", i_value.Trim());
        }

        /// <summary>
        /// 自动拼接单引号
        /// </summary>
        /// <param name="i_value"></param>
        /// <returns></returns>
        public static string AllAgreeSql(object i_value)
        {
            i_value = ConvertSQL(ToStr(i_value));
            return string.Format("'{0}'", i_value);
        }

        public static bool IsNullOrEmpty(object value)
        {
            return string.IsNullOrEmpty(UtilHelper.ToStr(value));
        }

        public static string ConvertSQL(string v)
        {
            v = ToStr(v);
            v = v.Trim();
            v = v.Replace("'", "''");

            return v;
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(object value)
        {
            string str = Convert.ToString(value);
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }


        public static int ToInt(object value)
        {
            if (value == null) return 0;
            if (Convert.ToString(value) == string.Empty) return 0;

            if (!IsNumeric(value)) return 0;

            return Convert.ToInt32(value);
        }

        public static string ToStr(object v)
        {
            return Convert.ToString(v);
        }

        public static decimal ToDec(object value)
        {
            if (value == null) return decimal.Zero;
            if (Convert.ToString(value) == string.Empty) return decimal.Zero;

            return Convert.ToDecimal(value);
        }

        public static double ToDlb(object value)
        {
            if (value == null) return 0;
            if (Convert.ToString(value) == string.Empty) return 0;

            return Convert.ToDouble(value);
        }

        public static string Base64Encoder(object value)
        {
            byte[] bytes = Encoding.Default.GetBytes(ToStr(value));
            return Convert.ToBase64String(bytes);
        }

        public static string Base64Decoder(object value)
        {
            try
            {
                byte[] outputb = Convert.FromBase64String(ToStr(value));
                return Encoding.Default.GetString(outputb);
            }
            catch
            {
                return "";
            }
        }

        public static bool Bool(object v)
        {
            string s = Convert.ToString(v).ToLower();
            if (s == "true" || s == "1")
            {
                return true;
            }
            else if (s == "false" || s == "0")
            {
                return false;
            }

            return false;
        }

        public static bool IsDate(string value)
        {
            try
            {
                DateTime.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDate(object value)
        {
            try
            {
                DateTime.Parse(ToStr(value));
                return true;
            }
            catch
            {
                return false;
            }
        }



        public static string CheckHtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"</div>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex10 = new System.Text.RegularExpressions.Regex(@"<div>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex11 = new System.Text.RegularExpressions.Regex(@"\<span[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex12 = new System.Text.RegularExpressions.Regex(@"\<p[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, ""); //过滤frameset
            html = regex10.Replace(html, ""); //过滤frameset
            html = regex11.Replace(html, ""); //过滤frameset
            html = regex12.Replace(html, ""); //过滤frameset

            html = html.Replace("<b>", "");
            html = html.Replace("</b>", "");
            html = html.Replace("</span>", "");
            html = html.Replace("<span>", "");
            //html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }

        public static bool DataSetIsNull(DataSet ds)
        {
            if ((((ds != null) && (ds.Tables.Count != 0)) && (ds.Tables[0].Rows.Count != 0)) && (ds.Tables[0].Columns.Count != 0))
            {
                return false;
            }
            return true;
        }

        public static DataRowCollection GetDataSetRows(DataSet ds)
        {
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows;
            }
            return null;
        }

        public static bool DataTableIsNull(DataTable dt)
        {
            if (((dt != null) && (dt.Columns.Count != 0)) && (dt.Rows.Count != 0))
            {
                return false;
            }
            return true;
        }

        public static string GetArrayStr(List<int> list)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == (list.Count - 1))
                {
                    builder.Append(list[i].ToString());
                }
                else
                {
                    builder.Append(list[i]);
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }

        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] strArray = str.Split(new char[] { speater });
            foreach (string str2 in strArray)
            {
                if (!string.IsNullOrEmpty(str2) && (str2 != speater.ToString()))
                {
                    string item = str2;
                    if (toLower)
                    {
                        item = str2.ToLower();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public static string LeftB(string stTarget, int iByteSize, string space)
        {
            System.Text.Encoding sjis = System.Text.Encoding.GetEncoding("gb2312");
            int TempLen = sjis.GetByteCount(stTarget);
            if (((iByteSize < 1)
                        || (stTarget.Length < 1)))
            {
                return "";
            }
            if ((TempLen <= iByteSize))
            {

                return stTarget;
            }
            byte[] tempByt = sjis.GetBytes(stTarget);
            string strTemp = sjis.GetString(tempByt, 0, iByteSize);
            if (strTemp.EndsWith("・"))
            {
                strTemp = sjis.GetString(tempByt, 0, (iByteSize - 1)) + space;
            }

            return strTemp;
        }
    }
}