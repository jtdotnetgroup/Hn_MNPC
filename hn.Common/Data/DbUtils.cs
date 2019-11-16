using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Omu.ValueInjecter;
using System.Configuration;
using hn.Common.Data.SqlServer;
using System.Data.OracleClient;


namespace hn.Common.Data
{
    public static class DbUtils
    {
        static string cs = SqlEasy.connString; //数据库连接字符串

        static OracleConnection con = new OracleConnection(cs);


        public static IEnumerable<T> GetWhere<T>(object where) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=:{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);
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

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(cs);
        }

        public static IEnumerable<T> GetWhereStr<T>(string where) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where 1=1 " + where;

                    LogHelper.WriteLog(cmd.CommandText);
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

        public static IEnumerable<T> GetWhereStr<T>(string where, string order) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where 1=1 " + where + " order by " + order;
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
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=:{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);
                    cmd.InjectFrom<SetParamsValues>(where);
                    conn.Open();

                    return PublicMethod.GetInt(cmd.ExecuteScalar());
                }
            }
        }

        public static int Delete<T>(string id)
        {
            using (var conn = new OracleConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where FID=:FID";

                cmd.InjectFrom<SetParamsValues>(new { FID = id });
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int BatchDelete<T>(string IDS)
        {
            using (var conn = new OracleConnection(cs))
            using (var cmd = conn.CreateCommand())
            {

                StringBuilder commandText = new StringBuilder();
                commandText.Append("delete from " + TableConvention.Resolve(typeof(T)) + " where FID in (");

                if (IDS.IsNullOrEmpty())
                {
                    return 0;
                }

                string[] value = IDS.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (value.Length <= 0)
                {
                    return 0;
                }

                int count = 0;
                foreach (var item in value)
                {
                    string name = "FID" + count;
                    cmd.Parameters.AddWithValue(name, item);

                    if (count > 0)
                    {
                        commandText.Append(",:" + name);
                    }
                    else
                    {
                        commandText.Append(":" + name);
                    }

                    count++;
                }
                commandText.Append(")");

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = commandText.ToString();

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int DeleteWhere<T>(object where)
        {
            using (var conn = new OracleConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where "
                    .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=:{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);

                cmd.InjectFrom<SetParamsValues>(where);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 用事务进行删除，其中失败则回滚
        /// </summary>
        /// <param name="commandTextList"></param>
        /// <returns></returns>
        public static int DeleteWithTransaction(List<string> commandTextList, params object[] parameters)
        {

            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    OracleTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//创建事务对象

                    cmd.CommandType = CommandType.Text;

                    cmd.Transaction = transaction;
                    cmd.Connection = conn;

                    int count = 0;

                    try
                    {
                        foreach (var item in commandTextList)
                        {
                            cmd.CommandText = item;
                            count += cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return count;
                    }
                    catch (OracleException ex)
                    {
                        transaction.Rollback();

                        throw ex;
                    }
                }
            }

        }


        //public static int Delete<T>(string ids)
        //{ 
        //    using (var conn = new OracleConnection(cs))
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where FID=(',' + cast(FID AS varchar(50)) + ',',',''  + :FID + '',') > 0";

        //        cmd.InjectFrom<SetParamsValues>(new { FID = ids});
        //        conn.Open();
        //        return cmd.ExecuteNonQuery();
        //    }
        //}

        ///<returns> the id of the inserted object </returns>
        public static string Insert(object o)
        {
            string s = "";
            try
            {
                using (var conn = new OracleConnection(cs))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into " + TableConvention.Resolve(o) + " ("
                            .InjectFrom(new FieldsBy().IgnoreFields(""), o) + ") values("
                            .InjectFrom(new FieldsBy().IgnoreFields("").SetFormat(":{0}"), o)
                            + ")";

                        cmd.InjectFrom(new SetParamsValues().IgnoreFields(""), o);
                        string fid = Guid.NewGuid().ToString();
                        cmd.Parameters.Add(new OracleParameter("FID", OracleType.VarChar) { Value = fid });
                        s = cmd.CommandText;
                        conn.Open();
                        LogHelper.WriteLog(cmd.CommandText);
                        cmd.ExecuteScalar();
                        return fid;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
                return ex.ToString();
                throw ex;
            }
        }

        public static int InsertWithFID(object o, string FID)
        {
            try
            {
                using (var conn = new OracleConnection(cs))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into " + TableConvention.Resolve(o) + " ("
                            .InjectFrom(new FieldsBy().IgnoreFields(""), o) + ") values("
                            .InjectFrom(new FieldsBy().IgnoreFields("").SetFormat(":{0}"), o)
                            + ")";

                        cmd.InjectFrom(new SetParamsValues().IgnoreFields(""), o);
                        cmd.Parameters.Add(new OracleParameter("FID", OracleType.VarChar) { Value = FID });

                        conn.Open();

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertObj<T>(T row)
        {
            string sql = "";
            try
            {
                var conn = ConnectionSingle.GetInstance().Conn;
                sql = GetInsertSql<T>();

                var cmd = GetCommand<T>(sql, row, conn);

                var result= cmd.ExecuteNonQuery();

                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                LogHelper.WriteLog(sql);
                throw;
            }
        }

        public static int InsertOrUpdate<T>(T row, params object[] updateWhere)
        {
            string sql = "";
            try
            {
                using (var conn = ConnectionSingle.GetInstance().Conn)
                {
                    sql = updateWhere == null ? GetInsertSql<T>() : GetUpdateSql<T>(updateWhere[0].ToString());
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var cmd = GetCommand<T>(sql, row, conn);

                    return cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                LogHelper.WriteLog(sql);
                throw;
            }




        }


        public static int UpdateWithWhere<T>(T row, string where)
        {
            try
            {

                var conn = ConnectionSingle.GetInstance().Conn;
                var sql = GetUpdateSql<T>(where);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var cmd = GetCommand(sql, row, conn);

                var result= cmd.ExecuteNonQuery();
                conn.Close();
                return result;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw;
            }

        }

        private static OracleCommand GetCommand<T>(string sql, T obj, OracleConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            var cmd = new OracleCommand(sql, conn);

            var t = typeof(T);
            var pis = t.GetProperties().ToList();

            pis.ForEach(p =>
            {
                var value = p.GetValue(obj, null);
                if (value == null)
                    value = "";

                cmd.Parameters.Add(new OracleParameter(":" + p.Name, value));
            });
            return cmd;
        }

        private static string GetInsertSql<T>()
        {
            var t = typeof(T);

            var pis = t.GetProperties().ToList();

            var tableName = TableConvention.Resolve(t);

            var strbuilder = new StringBuilder();
            strbuilder.AppendFormat("INSERT INTO {0} ", tableName);

            string fields = "(";
            string values = "VALUES (";

            pis.ForEach(p =>
            {
                string fieldName = p.Name;

                //var column = p.CustomAttributes.SingleOrDefault(o => o.AttributeType == typeof(ColumnAttribute));

                //if (column != null)
                //{
                //    fieldName = column.ConstructorArguments.First().Value.ToString();
                //}

                fields += fieldName;


                values += ":" + fieldName;

                if (p == pis.Last())
                {
                    fields += ")";
                    values += ")";
                }
                else
                {
                    fields += ",";
                    values += ",";
                }
            });

            strbuilder.Append(fields);
            strbuilder.Append(values);

            return strbuilder.ToString();
        }

        private static string GetUpdateSql<T>(string where)
        {
            var t = typeof(T);

            var pis = t.GetProperties().ToList();

            //var tableAttr = t.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(TableAttribute));
            var tableName = TableConvention.Resolve(t);

            var strbuilder = new StringBuilder();
            strbuilder.AppendFormat("UPDATE {0} SET ", tableName);

            string fields = "";

            pis.ForEach(p =>
            {
                string fieldName = p.Name;

                //var column = p.CustomAttributes.SingleOrDefault(o => o.AttributeType == typeof(ColumnAttribute));

                //if (column != null)
                //{
                //    fieldName = column.ConstructorArguments.First().Value.ToString();
                //}

                fields += fieldName + "=:" + fieldName;

                if (p == pis.Last())
                {
                    fields += " WHERE 1=1 ";
                }
                else
                {
                    fields += ",";
                }
            });

            strbuilder.Append(fields);
            strbuilder.Append(where);
            return strbuilder.ToString();
        }

        //public static int Insert(object o, string IgnoreFields)
        //{
        //    string[] strarr = { };
        //    if (!string.IsNullOrEmpty(IgnoreFields))
        //        strarr = IgnoreFields.Split(',');
        //    using (var conn = new OracleConnection(cs))
        //    using (var cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
        //            .InjectFrom(new FieldsBy().IgnoreFields(strarr), o) + ") values("
        //            .InjectFrom(new FieldsBy().IgnoreFields(strarr).SetFormat("{0}"), o)
        //            + ") ";

        //        cmd.InjectFrom(new SetParamsValues().IgnoreFields(strarr), o);

        //        conn.Open();
        //        return Convert.ToInt32(cmd.ExecuteNonQuery());
        //    }
        //}



        public static int Update(object o)
        {
            try
            {
                using (var conn = new OracleConnection(cs))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                        .InjectFrom(new FieldsBy().IgnoreFields("fid").SetFormat("{0}=:{0}"), o)
                        + " where FID =:FID";

                    cmd.InjectFrom<SetParamsValues>(o);

                    conn.Open();

                    LogHelper.WriteLog(cmd.CommandText);
                    int i = Convert.ToInt32(cmd.ExecuteNonQuery());
                    LogHelper.WriteLog(" lines =" + i);
                    return i;
                }
            }
            catch (Exception ee)
            {
                LogHelper.WriteLog(ee.ToString());
                return 0;
            }
        }
        public static int Update(object o, OracleConnection conn, OracleTransaction tran)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                        .InjectFrom(new FieldsBy().IgnoreFields("fid").SetFormat("{0}=:{0}"), o)
                        + " where FID =:FID";

                    cmd.InjectFrom<SetParamsValues>(o);

                    cmd.Transaction = tran;

                    LogHelper.WriteLog(cmd.CommandText);
                    int i = Convert.ToInt32(cmd.ExecuteNonQuery());
                    LogHelper.WriteLog(" lines =" + i);
                    return i;
                }
            }
            catch (Exception ee)
            {
                LogHelper.WriteLog(ee.ToString());

                tran.Rollback();

                return 0;
            }
        }

        public static int Update(object o, params string[] fields)
        {
            using (var conn = new OracleConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                    .InjectFrom(new FieldsBy().IgnoreFields(fields).SetFormat("{0}=:{0}"), o)
                    + " where FID =:FID";

                // cmd.InjectFrom<SetParamsValues>(o);
                List<string> list = new List<string>();
                foreach (string field in fields)
                {
                    if (field.ToUpper() != "FID")
                    {
                        list.Add(field);
                    }
                }
                cmd.InjectFrom(new SetParamsValues().IgnoreFields(list.ToArray()), o);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteNonQuery());
            }
        }

        public static int UpdateWhatWhere<T>(object what, object where)
        {
            string sql = "";
            try
            {
                using (var conn = new OracleConnection(cs))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    sql = "update " + TableConvention.Resolve(typeof(T)) + " set "
                        .InjectFrom(new FieldsBy().SetFormat("{0}=:{0}"), what)
                        + " where ".InjectFrom(new FieldsBy()
                        .SetFormat("{0}=:wp{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);

                    cmd.CommandText = sql;
                    cmd.InjectFrom<SetParamsValues>(what);
                    cmd.InjectFrom(new SetParamsValues().Prefix("wp"), where);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(sql);

                throw;
            }

        }

        public static int UpdateWhatWhere<T>(object what, object where, OracleConnection conn, OracleTransaction tran)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(typeof(T)) + " set "
                    .InjectFrom(new FieldsBy().SetFormat("{0}=:{0}"), what)
                    + " where ".InjectFrom(new FieldsBy()
                    .SetFormat("{0}=:wp{0}")
                    .SetNullFormat("{0} is null")
                    .SetGlue("and"),
                    where);

                cmd.Transaction = tran;

                cmd.InjectFrom<SetParamsValues>(what);
                cmd.InjectFrom(new SetParamsValues().Prefix("wp"), where);

                return cmd.ExecuteNonQuery();
            }
        }


        public static int InsertNoIdentity(object o)
        {
            using (var conn = new OracleConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields("fid"), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields("fid").SetFormat("{0}"), o) + ")";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <returns>rows affected</returns>
        public static int ExecuteNonQuerySp(string sp, object parameters)
        {
            using (var conn = new OracleConnection(cs))
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
            using (var conn = new OracleConnection(cs))
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

        public static int ExecuteNonQuery(string commendText)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = commendText;
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(string sql, object parameters) where T : new()
        {
            using (var conn = new OracleConnection(cs))
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
            using (var conn = new OracleConnection(cs))
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
            using (var conn = new OracleConnection(cs))
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
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T));
                    conn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static PT GetExecuteScalarWhere<T, PT>(string field, object where)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("select {0} from {1} ", TableConvention.Resolve(typeof(T))) + " where ".InjectFrom(new FieldsBy()
                    .SetFormat("{0}=:{0}")
                    .SetNullFormat("{0} is null")
                    .SetGlue("and"),
                    where);
                    cmd.InjectFrom<SetParamsValues>(where);

                    conn.Open();

                    return (PT)Convert.ChangeType(cmd.ExecuteScalar(), typeof(PT));
                }
            }
        }

        public static object GetExecuteScalarWhere<T>(string field, object where)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("select {0} from {1} ", field, TableConvention.Resolve(typeof(T))) + " where ".InjectFrom(new FieldsBy()
                    .SetFormat("{0}=:{0}")
                    .SetNullFormat("{0} is null")
                    .SetGlue("and"),
                    where);

                    cmd.InjectFrom<SetParamsValues>(where);

                    conn.Open();

                    return cmd.ExecuteScalar();
                }
            }
        }

        public static object GetExecuteScalarWhere(string sql, object where)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    if (where != null)
                    {
                        sql += " where ".InjectFrom(new FieldsBy()
                        .SetFormat("{0}=:{0}")
                        .SetNullFormat("{0} IS NULL")
                        .SetGlue(" and "), where);
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    conn.Open();

                    return cmd.ExecuteScalar();

                }
            }
        }

        public static int CountBySQL(string sql)
        {
            try
            {
                var conn = ConnectionSingle.GetInstance().Conn;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    var result= Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                LogHelper.WriteLog("SQL:" + sql);
                throw;
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
            using (var conn = new OracleConnection(cs))
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
            using (var conn = new OracleConnection(cs))
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

        public static IEnumerable<T> GetList<T>(string sql) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
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

        public static IEnumerable<string> GetColumnList<T>(string fields)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("select {0} from {1}", TableConvention.Resolve(typeof(T)));
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return (string)Convert.ChangeType(dr.GetValue(0), typeof(string));
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> GetListByFristColumn<T>(string sql)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return (T)Convert.ChangeType(dr.GetValue(0), typeof(T));
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> GetWhere<T>(string where, object parameters) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = " SELECT * FROM " + TableConvention.Resolve(typeof(T)) + " where  " + where;
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
            try
            {
                using (var conn = new OracleConnection(cs))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = pcp.Sp_PagerName;
                        cmd.InjectFrom(new SetParamsValues().IgnoreFields("sp_pagername"), pcp);

                        OracleParameter para = new OracleParameter("v_cursor", OracleType.Cursor);
                        para.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(para);


                        //OracleParameter outputPara1 = new OracleParameter("PAGE_NOW_N_OUT", OracleType.Number);
                        //outputPara1.Direction = ParameterDirection.Output;
                        //cmd.Parameters.Add(outputPara1);

                        OracleParameter outputPara2 = new OracleParameter("PAGE_SUM_N_OUT", OracleType.Number);
                        outputPara2.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputPara2);

                        //OracleParameter outputPara3 = new OracleParameter("PAGE_DATA_EVERYCOUNT_N_OUT", OracleType.Number);
                        //outputPara3.Direction = ParameterDirection.Output;
                        //cmd.Parameters.Add(outputPara3);

                        OracleParameter outputPara4 = new OracleParameter("PAGE_DATA_SUM_COUNT_N_OUT", OracleType.Number);
                        outputPara4.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputPara4);

                        conn.Open();

                        using (var da = new OracleDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            cmd.Parameters.Clear();
                            recordCount = PublicMethod.GetInt(outputPara4.Value);
                            conn.Close();
                            return ds.Tables[0];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetBillNo(string billType, string ruleid)
        {
            try
            {
                using (var conn = new OracleConnection(cs))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GetBillNO";

                        OracleParameter paraBillType = new OracleParameter("BillType", OracleType.VarChar);
                        paraBillType.Direction = ParameterDirection.Input;
                        paraBillType.Value = billType;
                        cmd.Parameters.Add(paraBillType);


                        OracleParameter paraRuleID = new OracleParameter("RuleID", OracleType.VarChar);
                        paraRuleID.Direction = ParameterDirection.Input;
                        paraRuleID.Value = ruleid;
                        cmd.Parameters.Add(paraRuleID);


                        OracleParameter paraBillNO = new OracleParameter("BillNO", OracleType.VarChar, 100);
                        paraBillNO.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paraBillNO);

                        conn.Open();

                        string billNO = "";
                        using (var da = new OracleDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            cmd.Parameters.Clear();
                            billNO = PublicMethod.GetString(paraBillNO.Value);
                            conn.Close();
                        }

                        return billNO;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<T> GetPage<T>(int page, int pageSize) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    var name = TableConvention.Resolve(typeof(T));

                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = string.Format(@"SELECT *
                      FROM (SELECT ROWNUM AS rowno, t.*
                              FROM {0} t
                             WHERE  ROWNUM <= ( {1} * {2} )) table_alias
                     WHERE table_alias.rowno >= (({1} - 1) * {2}) ", name, page, pageSize);
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

        public static T Get<T>(string FID) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where FID = '" + FID + "'";
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

        public static T GetByID<T>(string FID) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where ID = '" + FID + "'";
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

        public static IEnumerable<T> GetByIDList<T>(IEnumerable<string> IDList) where T : new()
        {
            if (IDList == null || IDList.Count() <= 0)
            {
                yield break;
            }

            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    var name = TableConvention.Resolve(typeof(T));

                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = string.Format("select * from {0} where FID in ({1})", TableConvention.Resolve(typeof(T)), string.Join(",", IDList.Select(l => '\'' + l + '\'')));
                    conn.Open();

                    LogHelper.WriteLog(cmd.CommandText);

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

        public static IEnumerable<T> Query<T>(string sql) where T : new()
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = sql;
                    LogHelper.WriteLog(sql);
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

        public static DataTable Query(string sql)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (var da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        return ds.Tables[0];
                    }
                }
            }
        }


        public static DataTable Query(string sql, object parameters)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();

                    using (var da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        return ds.Tables[0];
                    }
                }
            }
        }


        public static DataTable Query(string sql, params OracleParameter[] cmdParms)
        {
            using (var conn = new OracleConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    //cmd.CommandType = CommandType.Text;
                    //cmd.CommandText = sql;
                    //conn.Open();

                    PrepareCommand(cmd, conn, null, sql, cmdParms);

                    using (var da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        return ds.Tables[0];
                    }
                }
            }
        }



        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (OracleParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static IEnumerable<T> ToModel<T>(DataTable table) where T : new()
        {
            foreach (DataRow row in table.Rows)
            {
                var o = new T();
                o.InjectFrom<ReaderInjection>(row);
                yield return o;
            }



        }


        #region 事务方法

        public static string GetInsertCommandText(object o)
        {
            return "insert into " + TableConvention.Resolve(o) + " ("
                 .InjectFrom(new FieldsBy().IgnoreFields(""), o) + ") values("
                 .InjectFrom(new FieldsBy().IgnoreFields("").SetFormat(":{0}"), o)
                 + ")";
        }

        /// <summary>
        /// 获取默认ID删除命令字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetDeleteCommandText<T>(string ID)
        {
            return "delete from " + TableConvention.Resolve(typeof(T)) + " where FID=:FID";

        }

        /// <summary>
        /// 获取删除命令字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetDeleteWhereCommandText<T>(object where)
        {
            return "delete from " + TableConvention.Resolve(typeof(T)) + " where "
                                .InjectFrom(new FieldsBy()
                                    .SetFormat("{0}=:{0}")
                                    .SetNullFormat("{0} is null")
                                    .SetGlue("and"),
                                    where);
        }

        /// <summary>
        /// 真*事务插入方ifcc
        /// </summary>
        /// <param name="sql">SQL语句，例如：INSERT INTO ABC(A,B,C) values(@A,@B,@C) </param>
        /// <param name="values">插入的值，字典类型，键格式为:@XXX，防SQL注入攻击</param>
        /// <returns>插入了多少行</returns>
        public static int InsertWithTranscation(string sql, Dictionary<string, object> values)
        {
            using (OracleConnection conn = new OracleConnection(cs))
            {
                var tran = conn.BeginTransaction();

                var cmd = GetCommand(sql, conn, tran, values);

                var result = cmd.ExecuteNonQuery();

                tran.Commit();

                return result;
            }
        }

        #endregion

        private static OracleCommand GetCommand(string sql, OracleConnection conn, OracleTransaction tx = null, Dictionary<string, object> values = null)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);

            if (tx != null)
            {
                cmd.Transaction = tx;
            }

            if (values == null)
            {
                return cmd;
            }

            foreach (string key in values.Keys)
            {
                OracleParameter param = new OracleParameter(key, values[key]);
                cmd.Parameters.Add(param);
            }

            return cmd;
        }


        /// <summary>
        /// 根据实体对象返回SQL语句
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="obj">实体对象</param>
        /// <param name="operate">操作类型</param>
        /// <returns></returns>
        public static string GetSqlWithObject<T>(T obj, Operate operate)
        {
            var t = typeof(T);
            string tableName = t.Name;
            var pis = t.GetProperties().ToList();

            string sql = "";
            //根据操作类型定义SQL语句
            switch (operate)
            {
                case Operate.Select:
                    {
                        //暂时只实现查全字段
                        sql += "SELECT * FROM " + tableName + " ";
                        return sql;
                    }
                case Operate.Insert:
                    {
                        sql += "INSERT INTO " + tableName;
                        string fieldStr = " (";
                        string values = " values (";
                        pis.ForEach(p =>
                        {
                            var value = p.GetValue(obj, null);
                            if (value != null)
                            {
                                fieldStr += p.Name;
                                values += ":" + p.Name;

                                if (pis.Last() != p)
                                {
                                    fieldStr += ",";
                                    values += ",";
                                }
                                else
                                {
                                    fieldStr += ")";
                                    values += ")";
                                }
                            }



                        });

                        sql += fieldStr + values;

                        break;
                    }
                case Operate.Update:
                    {
                        sql += "UPDATE " + tableName + " SET ";
                        pis.ForEach(p =>
                        {
                            var value = p.GetValue(obj, null);
                            string fieldName = p.Name.Trim();

                            if (value != null)
                            {
                                sql += fieldName + "=:" + fieldName + ",";
                            }
                        });


                        sql = sql.Substring(0, sql.Length - 1);


                        break;
                    }
                case Operate.Delete:
                    {
                        sql += "DELETE FROM " + tableName + " ";
                        break;
                    }
            }
            sql += " ";
            return sql;
        }

        public static OracleCommand GetCommand<T>(T obj, Operate operate)
        {
            OracleConnection conn = new OracleConnection(cs);
            conn.Open();
            var tx = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Transaction = tx;
            return cmd;
        }

        public static void GetParams<T>(T obj, OracleCommand cmd)
        {
            if (obj == null)
            {
                throw new Exception("参数对象能为空");
            }

            var t = typeof(T);
            var pis = t.GetProperties().ToList();

            if (pis.Count == 0)
            {
                throw new Exception("参数对象能为空");
            }


            pis.ForEach(p =>
            {
                var value = p.GetValue(obj, null);
                if (value != null)
                {
                    cmd.Parameters.Add(new OracleParameter(p.Name, value));
                }
            });


        }
    }
}
