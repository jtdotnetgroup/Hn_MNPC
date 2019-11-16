using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using hl;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using System.Linq;
using System.Collections;
using hn.Common.Data.SqlServer;
using hn.Common.Data.SqlServer;
using System.Data.OracleClient;
namespace hn.Common
{
    public class PublicMethod
    {

        #region 获取某表中的总记录数

        /// <summary>
        /// 获取某表中的总记录数
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public static int GetRecordCount(string tablename)
        {
            string s = "select count(*) from {0}";
            s = string.Format(s, tablename);
            return Convert.ToInt32(SqlEasy.ExecuteScalar(s));
        }

        public static int GetRecordCount(string tablename, string where)
        {
            string s = "select count(*) from {0} ";
            s = string.Format(s, tablename);
            if (!string.IsNullOrEmpty(where))
                s += " where " + where;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlEasy.connString, CommandType.Text, s));
        }

        public static int GetRecordCount(string connString,string tablename, string where)
        {
            string s = "select count(*) from {0} ";
            s = string.Format(s, tablename);
            if (!string.IsNullOrEmpty(where))
                s += " where " + where;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connString, CommandType.Text, s));
        }


        #endregion

        #region 根据条件获取指定表中的数据

        /// <summary>
        /// 根据条件获取指定表中的数据
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string tablename, string where)
        {
            string s = "select * from " + tablename;
            if (where != "")
                s += " where " + where;

            return SqlHelper.ExecuteDataset(SqlEasy.connString, CommandType.Text, s).Tables[0];
        }


        #endregion

        #region 根据ID 获取一行数据

        /// <summary>
        /// 根据主键Id,获取一行数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名称</param>
        /// <param name="value">值</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public static DataRow GetADataRow(string tableName, string keyName, string value, out string msg)
        {
            try
            {
                string s = "select * from @table where @keyname=:value";
                OracleParameter[] sp ={new OracleParameter("table",tableName),
                    new OracleParameter("keyname",keyName),
                    new OracleParameter("value",value)
                };

                DataTable dt =SqlEasy.ExecuteDataTable(s,sp);

                if (dt.Rows.Count > 0)
                {
                    msg = "OK";
                    return dt.Rows[0];
                }
                else
                {
                    msg = "";
                    return null;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 由Object取值
        /// <summary>
        /// 取得Int值,如果为Null 则返回０
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInt(object obj)
        {
            if (obj != null)
            {
                int i;
                int.TryParse(obj.ToString(), out i);
                return i;
            }
            else
                return 0;
        }

        public static double GetDouble(object obj)
        {
            if (obj != null)
            {
                double d;
                double.TryParse(obj.ToString(), out d);
                return d;
            }
            else
                return 0;
        }

        public static float GetFloat(object obj)
        {
            if (obj != null)
            {
                float f;
                float.TryParse(obj.ToString(), out f);
                return f;
            }
            else
                return 0;
        }


        /// <summary>
        /// 取得Int值,如果不成功则返回指定exceptionvalue值
        /// </summary>
        /// <param name="obj">要计算的值</param>
        /// <param name="exceptionvalue">异常时的返回值</param>
        /// <returns></returns>
        public static int GetInt(object obj, int exceptionvalue)
        {            
            if (obj == null)
                return exceptionvalue;
            if (string.IsNullOrEmpty(obj.ToString()))
                return exceptionvalue;
            int i=exceptionvalue;
            try{i = Convert.ToInt32(obj);}
            catch{i = exceptionvalue;}
            return i;
        }

        /// <summary>
        /// 取得Decima值,如果不成功则返回指定exceptionvalue值
        /// </summary>
        /// <param name="obj">要计算的值</param>
        /// <param name="exceptionvalue">异常时的返回值</param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj, int exceptionvalue)
        {
            if (obj == null)
                return exceptionvalue;
            if (string.IsNullOrEmpty(obj.ToString()))
                return exceptionvalue;
            decimal i = exceptionvalue;
            try { i = Convert.ToDecimal(obj); }
            catch { i = exceptionvalue; }
            return i;
        }

        /// <summary>
        /// 取得byte值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte Getbyte(object obj)
        {
            if (obj.ToString() != "")
                return byte.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 获得Long值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long GetLong(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return long.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Long值,如果不成功则返回指定exceptionvalue值
        /// </summary>
        /// <param name="obj">要计算的值</param>
        /// <param name="exceptionvalue">异常时的返回值</param>
        /// <returns></returns>
        public static long GetLong(object obj, long exceptionvalue)
        {
            if (obj == null)
                return exceptionvalue;
            if (string.IsNullOrEmpty(obj.ToString()))
                return exceptionvalue;
            long i = exceptionvalue;
            try { i = Convert.ToInt64(obj); }
            catch { i = exceptionvalue; }
            return i;
        }

        /// <summary>
        /// 取得Decimal值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return decimal.Parse(obj.ToString());
            else
                return 0;
        }

        /// <summary>
        /// 取得Guid值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Guid GetGuid(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return new Guid(obj.ToString());
            else
                return Guid.Empty;
        }

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            
            if (obj!=null && obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return new DateTime(2011,1,1);
        }

        /// <summary>
        /// 取得bool值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetBool(object obj)
        {
            if (obj != null)
            {
                if (GetInt(obj) == 1)
                {
                    return true;
                }
                else
                {
                    bool flag;
                    bool.TryParse(obj.ToString(), out flag);
                    return flag;
                }

            }
            else
                return false;
        }

        /// <summary>
        /// 取得byte[]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Byte[] GetByte(object obj)
        {
            if (obj.ToString() != "")
            {
                return (Byte[])obj;
            }
            else
                return null;
        }

        /// <summary>
        /// 取得string值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetString(object obj)
        {
            if (obj != null && obj != DBNull.Value)
                return obj.ToString();
            else
                return "";
        }
        #endregion

        #region  增加Sql参数
        /// <summary>
        /// 增加sql参数并返回
        /// </summary>
        /// <param name="arguments">参数列表(格式:@name,@sex,@email......)</param>
        /// <param name="param">参数对应值</param>
        /// <returns></returns>
        public static OracleParameter[] AddSqlParameters(string arguments,params object[] param)
        {
            string[] args = arguments.Split(',');
            if (args.Length == 0)
                throw new ArgumentNullException("arguments","参数个数为空!");
            if(args.Length!=param.Length)
                throw new ArgumentNullException("arguments", "参数个数与赋值参数个数不相等!");
            OracleParameter[] para = new OracleParameter[args.Length];
            for (int i = 0; i < para.Length; i++)
            {
                para[i] = new OracleParameter(args[i],param[i]);
            }
            return para;
        }

        /// <summary>
        /// 增加赋值sql参数并返回
        /// </summary>
        /// <param name="sqls">sql赋值可变对象</param>
        /// <param name="arguments">参数列表(格式:@name,@sex,@email......)</param>
        /// <param name="param">参数对应值</param>
        /// <returns></returns>
        public static List<OracleParameter> AddSqlParameters(ref StringBuilder sqls,string arguments, params object[] param)
        {
            string[] args = arguments.Split(',');
            if (args.Length == 0)
                throw new ArgumentNullException("arguments", "参数个数为空!");
            if (args.Length != param.Length)
                throw new ArgumentNullException("arguments", "参数个数与赋值参数个数不相等!");
            List<OracleParameter> para = new List<OracleParameter>();
            for (int i = 0; i < args.Length; i++)
            {
                if (param[i] == null)
                    continue;
                if (param[i] is int && GetInt(param[i], -1) == -1)
                    continue;
                if (param[i] is string && string.IsNullOrEmpty(param[i].ToString()))
                    continue;
                if (param[i] is DateTime && GetDateTime(param[i]).Date == DateTime.MinValue.Date)
                    continue;
                sqls.Append(string.Format("[{0}]={1},", args[i].TrimStart('@'),args[i]));
                para.Add(new OracleParameter(args[i], param[i]));
            }
            return para;
        }

        /// <summary>
        /// 赋值时给指定泛型及可变串追加指定字段及指定Int值
        /// </summary>
        /// <param name="para">sql参数泛型</param>
        /// <param name="sqls">sql可变字段</param>
        /// <param name="field">字段名称</param>
        /// <param name="fieldvalue">字段值</param>
        public static void AddSetIntSqlParameter(List<OracleParameter> para, StringBuilder sqls, string field, int fieldvalue)
        {
            if (PublicMethod.GetInt(fieldvalue, -1) > -1)
            {
                sqls.Append(string.Format("[{0}]=:{0},",field));
                para.Add(new OracleParameter(string.Format("{0}",field), fieldvalue));
            }
        }

        /// <summary>
        /// 赋值时给指定泛型及可变串追加指定字段及指定String值
        /// </summary>
        /// <param name="para">sql参数泛型</param>
        /// <param name="sqls">sql可变字段</param>
        /// <param name="field">字段名称</param>
        /// <param name="fieldvalue">字段值</param>
        public static void AddSetStringSqlParameter(List<OracleParameter> para, StringBuilder sqls, string field, string fieldvalue)
        {
            if (!string.IsNullOrEmpty(fieldvalue))
            {
                sqls.Append(string.Format("[{0}]=:{0},", field));
                para.Add(new OracleParameter(string.Format("{0}", field), fieldvalue));
            }
        }

        /// <summary>
        /// 赋值时给指定泛型及可变串追加指定字段及指定日期String值
        /// </summary>
        /// <param name="para">sql参数泛型</param>
        /// <param name="sqls">sql可变字段</param>
        /// <param name="field">字段名称</param>
        /// <param name="fieldvalue">字段值</param>
        public static void AddSetDateSqlParameter(List<OracleParameter> para, StringBuilder sqls, string field, DateTime fieldvalue)
        {
            if (!string.IsNullOrEmpty(fieldvalue.ToString()))
            {
                if (PublicMethod.GetDateTime(fieldvalue) != DateTime.MinValue)
                {
                    sqls.Append(string.Format("[{0}]=:{0},", field));
                    para.Add(new OracleParameter(string.Format("{0}", field), fieldvalue));
                }
            }
        }

        /// <summary>
        /// 指定条件时给指定泛型及可变串追加指定字段及指定Int值
        /// </summary>
        /// <param name="para">sql参数泛型</param>
        /// <param name="sqls">sql可变字段</param>
        /// <param name="field">字段名称</param>
        /// <param name="fieldvalue">字段值</param>
        public static void AddWhereIntSqlParameter(List<OracleParameter> para, StringBuilder sqls, string field, int fieldvalue)
        {
            if (PublicMethod.GetInt(fieldvalue, -1) > -1)
            {
                sqls.Append(string.Format("[{0}]=:{0} and ", field));
                para.Add(new OracleParameter(string.Format("{0}", field), fieldvalue));
            }
        }

        /// <summary>
        /// 指定条件时给指定泛型及可变串追加指定字段及指定String值
        /// </summary>
        /// <param name="para">sql参数泛型</param>
        /// <param name="sqls">sql可变字段</param>
        /// <param name="field">字段名称</param>
        /// <param name="fieldvalue">字段值</param>
        public static void AddWhereStringSqlParameter(List<OracleParameter> para, StringBuilder sqls, string field, string fieldvalue)
        {
            if (!string.IsNullOrEmpty(fieldvalue))
            {
                sqls.Append(string.Format("[{0}]=:{0} and ", field));
                para.Add(new OracleParameter(string.Format("{0}", field), fieldvalue));
            }
        }
        #endregion


        #region 获取指定表中指定字段的最大值
        /// <summary>
        /// 获取指定表中指定字段的最大值
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="field">字段</param>
        /// <returns>Return Type:Int</returns>
        public static int GetMaxID(string tableName, string field)
        {
            string s = "select Max(@field) from @tablename";
            OracleParameter[] para = { new OracleParameter("field",field),new OracleParameter("tablename",tableName)};
            object obj = SqlHelper.ExecuteScalar(SqlEasy.connString, CommandType.Text, s,para);
            int i = Convert.ToInt32(obj == DBNull.Value ? "0" : obj);
            return i;
        }
        #endregion

        #region 获取客户端IP地址

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }


            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        #endregion

        #region 获取页中 列表选中的项
        /// <summary>
        /// 获取页中 列表选中的项
        /// </summary>
        /// <param name="chklist">CheckBoxList ID</param>
        /// <returns></returns>
        public static List<string> GetCheckedItemList(CheckBoxList chklist)
        {
            List<string> list = new List<string>();
            foreach (ListItem li in chklist.Items)
            {
                if (li.Selected)
                    list.Add(li.Value);
            }
            return list;
        }


        #endregion

        //判断Session是否有效
        public static void CheckSession()
        {
            //测试的时候注释掉
            try
            {
                //if (System.Web.HttpContext.Current.Session["UserName"] == null)
                //{
                //    System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Default.aspx'</script>");
                //}
            }
            catch
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Default.aspx'</script>");
            }
        }


        #region DataTable To List


        /// <summary>
        /// DataTable To List
        /// </summary>
        /// <typeparam name="TType">object type</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableToObjectList<T>(DataTable dt) where T : new()
        {
            DataColumnCollection columns = dt.Columns;
            int columncount = columns.Count;            
            List<T> result = new List<T>();    //声明一个要返回的对象泛型
            Type type = typeof(T);
            
            PropertyInfo[] propertys = type.GetProperties(BindingFlags.IgnoreCase|BindingFlags.Instance|BindingFlags.Public|BindingFlags.SetProperty);   //获取参数对象属性集合
            foreach (DataRow r in dt.Rows)
            {
                T t = new T();
                for (int i = 0; i < propertys.Length; i++)
                {
                    DataColumn column = columns[propertys[i].Name];
                    if (column != null && r[column] != null)
                    {
                        if (propertys[i].PropertyType == typeof(int))
                            propertys[i].SetValue(t, PublicMethod.GetInt(r[column]), null);
                        if (propertys[i].PropertyType == typeof(string))
                            propertys[i].SetValue(t, r[column].ToString(), null);
                        if (propertys[i].PropertyType == typeof(DateTime))
                            propertys[i].SetValue(t, PublicMethod.GetDateTime(r[column]), null);
                    }
                }
                result.Add(t);                
            }            
            return result;
        }

        /// <summary>
        /// 泛型集合转换成DATATABLE
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        
        #endregion

        /// <summary>
        /// 获取指定字符串中的指定字符的个数
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="value">要查找的字符串</param>
        /// <returns></returns>
        public static int GetCharLength(string source,string value)
        {            
            Regex reg = new Regex(value);
            return reg.Matches(source).Count;
        }

        /// <summary>
        /// 给指定字符串前面增加指定值
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="value">要增加的字符串</param>
        /// <returns></returns>
        public static string CharBeforeAppend(string source, string value)
        {
            return source.Insert(0, value);
        }

        /// <summary>
        /// 给指定字符串前面增加指定个数的指定值
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="value">要增加的字符串</param>
        /// <param name="length">要增加的个数</param>
        /// <returns></returns>
        public static string CharBeforeAppend(string source, string value, int length)
        {
            for (int i = 0; i < length; i++)
            {
                source = source.Insert(0, value);
            }
            return source;
        }

        /// <summary>
        /// 合并指定表并返回
        /// </summary>
        /// <param name="dt">原始表</param>
        /// <param name="DataTables">可变表参</param>
        /// <returns></returns>
        public static DataTable MergeDataTable(DataTable dt,params DataTable[] DataTables)
        {
            if (DataTables.Length == 0)
                return dt;            
            foreach (DataTable table in DataTables)
                dt.Merge(table);
            return dt;
        }

        //获得Session中的值
        public static string GetSessionValue(string SessionKey)
        {
            //测试时候使用，不掉线
            try
            {
                return System.Web.HttpContext.Current.Session[SessionKey].ToString();
            }
            catch
            {
               // System.Web.HttpContext.Current.Response.Write("<script>alert('登录信息安全时限过期，请重新登录！');top.location='../Default.aspx'</script>");
                return "NoLogin";

                //System.Web.HttpContext.Current.Session["UserID"] = "32";
                //System.Web.HttpContext.Current.Session["UserName"] = "admin";
                //System.Web.HttpContext.Current.Session["JiaoSe"] = "单位领导";
                //System.Web.HttpContext.Current.Session["Department"] = "开发部";
                //System.Web.HttpContext.Current.Session["TrueName"] = "张为龙";
                //System.Web.HttpContext.Current.Session["ZhiWei"] = "部门主管";
                //System.Web.HttpContext.Current.Session["QuanXian"] = hn.DBUtility.DbHelperSQL.GetSHSL("select top 1 QuanXian from ERPJiaoSe where JiaoSeName='单位领导'");
                //return System.Web.HttpContext.Current.Session[SessionKey].ToString();                
            }
        }

        //设置Session中的值
        public static void SetSessionValue(string SessionKey, string ValueStr)
        {
            System.Web.HttpContext.Current.Session[SessionKey] = ValueStr;
        }

        //绑定字符串分隔开的到CheckBoxList
        public static void BindDDL(CheckBoxList MyDDL, string FenGeStr)
        {
            MyDDL.Items.Clear();
            string[] MyRange = FenGeStr.Split('|');
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    string OldNameStr = hn.DBUtility.DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");

                    ListItem MyListItem = new ListItem();
                    MyListItem.Text = OldNameStr;
                    MyListItem.Value = MyRange[i].ToString();
                    MyDDL.Items.Add(MyListItem);
                }
            }
        }

        //判断Str1是否是在Str2这个长的字符串中
        public static bool StrIFIn(string Str1, string Str2)
        {
            if (Str2.IndexOf(Str1) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //判断文件是否在允许的范围内
        public static bool IfOkFile(string DirName)
        {
            bool ReturnIF = true;
            try
            {
                string FileExd = DirName.Split('.')[1].ToString();
                string JKL = hn.DBUtility.DbHelperSQL.GetSHSL("select FileType from ERPSystemSetting where FileType like '%|" + FileExd + "|%'");
                if (JKL.Length < 1)
                {
                    ReturnIF = false;
                }
            }
            catch
            {
                ReturnIF = false;
            }
            return ReturnIF;
        }

        //上传文件
        public static string UploadFileIntoDir(FileUpload MyFile, string DirName)
        {
            if (IfOkFile(DirName) == true)
            {
                string ReturnStr = string.Empty;
                if (MyFile.FileContent.Length > 0)
                {
                    MyFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath("../UploadFile/") + DirName);


                    //将原文件名与现在文件名写入ERPSaveFileName表中
                    string NowName = DirName;
                    string OldName = MyFile.FileName;
                    string SqlTempStr = "insert into ERPSaveFileName(NowName,OldName) values ('" + NowName + "','" + OldName + "')";
                    hn.DBUtility.DbHelperSQL.ExecuteSQL(SqlTempStr);


                    return DirName;
                }
                else
                {
                    return ReturnStr;
                }
            }
            else
            {
                if (MyFile.FileName.Length > 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('不允许上传此类型文件！');</script>");
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 将传入的用户名列表，按照工作委托的要求，自动置换成受委托后的用户名字符串，然后返回
        /// </summary>
        /// <param name="UserList"></param>
        /// <returns></returns>
        public static string WorkWeiTuoUserList(string UserList)
        {
            string ReturnList = "";
            string[] UserArray = UserList.Split(',');
            for (int i = 0; i < UserArray.Length; i++)
            {
                if (UserArray[i].ToString().Trim().Length > 0)
                {
                    string WeiTuoUser = hn.DBUtility.DbHelperSQL.GetSHSL("select top 1 ToUser from ERPNWorkFlowWT where FromUser='" + UserArray[i].ToString() + "'");
                    if (WeiTuoUser.Trim().Length > 0)
                    {
                        if (ReturnList.Trim().Length > 0)
                        {
                            if (StrIFIn("," + WeiTuoUser + ",", "," + ReturnList + ",") == false)
                            {
                                ReturnList = ReturnList + "," + WeiTuoUser;
                            }
                        }
                        else
                        {
                            ReturnList = WeiTuoUser;
                        }
                    }
                    else
                    {
                        if (ReturnList.Trim().Length > 0)
                        {
                            if (StrIFIn("," + UserArray[i].ToString() + ",", "," + ReturnList + ",") == false)
                            {
                                ReturnList = ReturnList + "," + UserArray[i].ToString();
                            }
                        }
                        else
                        {
                            ReturnList = UserArray[i].ToString();
                        }
                    }
                }
            }
            return ReturnList;
        }

        //得到文件列表
        public static string GetWenJian(string WenJianList, string DirStr)
        {
            string[] MyRange = WenJianList.Split('|');
            string MyReturn = string.Empty;
            for (int i = 0; i < MyRange.Length; i++)
            {
                if (MyRange[i].ToString().Trim().Length > 0)
                {
                    if (MyReturn.Trim().Length > 0)
                    {
                        if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                        {
                            MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                        }
                        else
                        {
                            string OldNameStr = hn.DBUtility.DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                            if (OldNameStr.Trim().Length <= 0)
                            {
                                OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                            }
                            MyReturn = MyReturn + "&nbsp;&nbsp;&nbsp;&nbsp;<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + OldNameStr + "</a>&nbsp;<a href='../DsoFramer/ReadFile.aspx?FilePath=" + MyRange[i].ToString() + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>";
                        }
                    }
                    else
                    {
                        if (MyRange[i].ToString().IndexOf("MailAttachments/") >= 0)
                        {
                            MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + MyRange[i].ToString().Replace("MailAttachments/", "") + "</a>";
                        }
                        else
                        {
                            string OldNameStr = hn.DBUtility.DbHelperSQL.GetSHSL("select OldName from ERPSaveFileName where NowName='" + MyRange[i].ToString().Replace("MailAttachments/", "") + "'");
                            if (OldNameStr.Trim().Length <= 0)
                            {
                                OldNameStr = MyRange[i].ToString().Replace("MailAttachments/", "");
                            }
                            MyReturn = "<img src=../images/ico_clip.gif /><a target=\"_blank\" href='" + DirStr + MyRange[i].ToString() + "'>" + OldNameStr + "</a>&nbsp;<a href='../DsoFramer/ReadFile.aspx?FilePath=" + MyRange[i].ToString() + "' target='_blank'><img border=0 src=../images/Button/ReadFile.gif /></a>";
                        }
                    }
                }
            }
            if (MyReturn.ToString().Trim().Length <= 0)
            {
                MyReturn = MyReturn + "无文件！";
            }
            return MyReturn;
        }

        //在RowDataBound事件时使用
        public static void GridViewRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#E4F4FF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
            }
        }

        //判断GridView里面被选中的ID
        public static string CheckCbx(GridView GVData, string CheckBoxName, string LabID)
        {
            string str = "";
            for (int i = 0; i < GVData.Rows.Count; i++)
            {
                GridViewRow row = GVData.Rows[i];
                CheckBox Chk = (CheckBox)row.FindControl(CheckBoxName);
                Label LabVis = (Label)row.FindControl(LabID);
                if (Chk.Checked == true)
                {
                    if (str == "")
                    {
                        str = LabVis.Text.ToString();
                    }
                    else
                    {
                        str = str + "," + LabVis.Text.ToString();
                    }
                }
            }
            return str;
        }


    }
   
}
