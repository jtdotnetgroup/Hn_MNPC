using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using hn.APIService.SqlServer;
using Omu.ValueInjecter;

namespace hn.APIService
{
    public static class DbUtils
    {
        static string cs = SqlEasy.connString; //数据库连接字符串

        public static IEnumerable<T> GetWhere<T>(object where, string orderby = "") where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=@{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where) + (orderby != "" ? " order by " + orderby : "");
                    cmd.InjectFrom<SetParamsValues>(where);
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        public static int CountWhere<T>(object where) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=@{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);
                    cmd.InjectFrom<SetParamsValues>(where);
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 查询满足条件的记录数，可以模糊查询 条件传字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int CountWhere<T>(string where) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T)) + " where " + where;

                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }


        public static int Delete<T>(int id)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where FID=@FID";

                cmd.InjectFrom<SetParamsValues>(new { FID = id });
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int DeleteWhere<T>(object where)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where "
                    .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=@{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);

                cmd.InjectFrom<SetParamsValues>(where);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        public static int Delete<T>(string ids)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where charindex(',' + cast(FID AS varchar(50)) + ',',','  + @FID + ',') > 0";

                cmd.InjectFrom<SetParamsValues>(new { FID = ids });
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        ///<returns> the id of the inserted object </returns>
        public static int Insert(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields("fid"), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields("fid").SetFormat("@{0}"), o)
                    + ") select @@identity";

                cmd.InjectFrom(new SetParamsValues().IgnoreFields("fid"), o);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int InsertEx(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy(), o) + ") values("
                    .InjectFrom(new FieldsBy().SetFormat("@{0}"), o)
                    + ")";

                cmd.InjectFrom(new SetParamsValues(), o);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int Insert(object o, string IgnoreFields)
        {
            string[] strarr = { };
            if (!string.IsNullOrEmpty(IgnoreFields))
                strarr = IgnoreFields.Split(',');
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields(strarr), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields(strarr).SetFormat("@{0}"), o)
                    + ") ";

                cmd.InjectFrom(new SetParamsValues().IgnoreFields(strarr), o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
        }

        public static int Update(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                    .InjectFrom(new FieldsBy().IgnoreFields("fid").SetFormat("{0}=@{0}"), o)
                    + " where FID = @FID";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
        }

        public static int Update(object o, params string[] fields)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                    .InjectFrom(new FieldsBy().IgnoreFields(fields).SetFormat("{0}=@{0}"), o)
                    + " where FID = @FID";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
        }

        public static int UpdateWhatWhere<T>(object what, object where)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(typeof(T)) + " set "
                    .InjectFrom(new FieldsBy().SetFormat("{0}=@{0}"), what)
                    + " where "
                    .InjectFrom(new FieldsBy()
                    .SetFormat("{0}=@wp{0}")
                    .SetNullFormat("{0} is null")
                    .SetGlue("and"),
                    where);

                cmd.InjectFrom<SetParamsValues>(what);
                cmd.InjectFrom(new SetParamsValues().Prefix("wp"), where);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        public static int InsertNoIdentity(object o)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields("fid"), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields("fid").SetFormat("@{0}"), o) + ")";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <returns>rows affected</returns>
        public static int ExecuteNonQuerySp(string sp, object parameters)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int ExecuteNonQuery(string commendText, object parameters)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commendText;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(string sql, object parameters) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                }
            }
        }


        public static IEnumerable<T> ExecuteReaderSp<T>(string sp, object parameters) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                }
            }
        }

        public static IEnumerable<T> ExecuteReaderSpValueType<T>(string sp, object parameters)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            yield return (T)dr.GetValue(0);
                        }
                }
            }
        }

        public static int Count<T>()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T));
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static int GetPageCount(int pageSize, int count)
        {
            var pages = count / pageSize;
            if (count % pageSize > 0) pages++;
            return pages;
        }

        public static IEnumerable<T> GetAll<T>() where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T));
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> GetList<T>(string sql, object parameters) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        public static DataTable GetPageWithSp(ProcCustomPage pcp, out int recordCount)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = pcp.Sp_PagerName;
                    cmd.InjectFrom(new SetParamsValues().IgnoreFields("sp_pagername"), pcp);

                    SqlParameter outputPara = new SqlParameter("@RecordCount", SqlDbType.Int);
                    outputPara.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputPara);

                    conn.Open();

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                        recordCount = PublicMethod.GetInt(outputPara.Value);
                        conn.Close();
                        return ds.Tables[0];
                    }
                }
            }
        }


        public static IEnumerable<T> GetPage<T>(int page, int pageSize) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    var name = TableConvention.Resolve(typeof(T));

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format(@"with result as(select *, ROW_NUMBER() over(order by FID desc) nr
                            from {0}
                    )
                    select  * 
                    from    result
                    where   nr  between (({1} - 1) * {2} + 1)
                            and ({1} * {2}) ", name, page, pageSize);
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                    }
                }
            }
        }

        //public static DataTable GetPage(string sql, int page, int pageSize, string filterJson, string sort, string order, out int recordCount)
        //{
        //    using (var conn = new SqlConnection(cs))
        //    {
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.Text;

        //            recordCount = 0;
        //            string strWhere = "where 1 = 1";
        //            if (!string.IsNullOrEmpty(filterJson))
        //            {
        //                strWhere += filterJson;
        //            }
        //            cmd.CommandText = string.Format(@"select count(*) AS Count from ({0} {1}) AS T1", sql, strWhere) ;
        //            LogHelper.WriteLog(cmd.CommandText);
        //            conn.Open();
        //            using (var da = new SqlDataAdapter(cmd))
        //            {
        //                DataSet ds = new DataSet();
        //                da.Fill(ds);
        //                if(ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //                {
        //                    foreach (DataRow row in ds.Tables[0].Rows)
        //                    {
        //                        recordCount = row["Count"].ToInt();
        //                        LogHelper.WriteLog("【配件总数】：" + recordCount);
        //                        break;
        //                    }
        //                }
        //                conn.Close();
        //            }

        //            cmd.CommandText = string.Format(@"with result as(select *, ROW_NUMBER() over(order by {3} {4}) nr
        //                    from ({0})
        //            )
        //            select  * 
        //            from    result
        //            where   nr  between (({1} - 1) * {2} + 1)
        //                    and ({1} * {2}) ", sql, page, pageSize, sort, order);
        //            conn.Open();

        //            using (var da = new SqlDataAdapter(cmd))
        //            {
        //                DataSet ds = new DataSet();
        //                da.Fill(ds);
        //                conn.Close();
        //                return ds.Tables[0];
        //            }
        //            return null;
        //        }
        //    }
        //}

        public static T Get<T>(long FID) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where FID = " + FID;
                conn.Open();

                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        var o = new T();
                        o.InjectFrom<ReaderInjection>(dr);
                        return o;
                    }
            }
            return default(T);
        }

        public static DataTable Query(string sql)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                     
                        conn.Close();
                        return ds.Tables[0];
                    }
                }
            }
        }

       

    }
}
