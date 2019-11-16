using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace hn.AutoSyncLib.Common
{
    public class OracleDBHelper
    {
        public  DbConnection conn { get; set; }

        private static OracleClientFactory factory =new OracleClientFactory();

        public OracleDBHelper(string conStr)
        {
            conn = factory.CreateConnection();
            conn.ConnectionString = conStr;
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, object> pars)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var cmd = factory.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                foreach (var key in pars.Keys)
                {
                    cmd.Parameters.Add(new OracleParameter(key, pars[key]));
                }

                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Out.WriteLineAsync(e.Message);
                Console.Out.WriteLineAsync("SQL:" + sql);
                throw;
            }

        }

        public object ExecuteScalar(string sql, Dictionary<string, object> pars)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var cmd = factory.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                foreach (var key in pars.Keys)
                {
                    cmd.Parameters.Add(new OracleParameter(key, pars[key]));
                }

                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {

                Console.Out.WriteLineAsync(e.Message);
                Console.Out.WriteLineAsync("SQL:" + sql);
                throw;
            }
        }

        public string GetInsertSql<T>() 
        {
            var t = typeof(T);

            var pis = t.GetProperties().ToList();

            var tableAttr= t.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(TableAttribute));
            var tableName = tableAttr.ConstructorArguments.First().Value;

            var strbuilder = new StringBuilder();
            strbuilder.AppendFormat("INSERT INTO {0} ",tableName);

            string fields = "(";
            string values = "VALUES (";

            pis.ForEach(p =>
            {
                string fieldName = p.Name;

                var column = p.CustomAttributes.SingleOrDefault(o => o.AttributeType == typeof(ColumnAttribute));

                if (column != null)
                {
                    fieldName = column.ConstructorArguments.First().Value.ToString();
                }

                fields += fieldName;


                values += ":" + fieldName;

                if (p == pis.Last())
                {
                    fields+= ")";
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

        public string GetUpdateSql<T>(string where)
        {
            var t = typeof(T);

            var pis = t.GetProperties().ToList();

            var tableAttr = t.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(TableAttribute));
            var tableName = tableAttr.ConstructorArguments.First().Value;

            var strbuilder = new StringBuilder();
            strbuilder.AppendFormat("UPDATE {0} SET ", tableName);

            string fields = "";

            pis.ForEach(p =>
            {
                string fieldName = p.Name;

                var column = p.CustomAttributes.SingleOrDefault(o => o.AttributeType == typeof(ColumnAttribute));

                if (column != null)
                {
                    fieldName = column.ConstructorArguments.First().Value.ToString();
                }

                fields += fieldName+"=:"+fieldName;

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

        public bool Update<T>(T obj,string where)
        {
            var sql = GetUpdateSql<T>(where);
            var cmd = GetCommand(sql, obj);
            cmd.Connection = conn;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.Out.WriteLineAsync(e.Message);
                Console.Out.WriteLineAsync("SQL:" + sql);
                throw ;
            }

        }

        public bool Insert<T>(T obj)
        {
            var sql = GetInsertSql<T>();
            var cmd = GetCommand(sql, obj);
            cmd.Connection = conn;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.Out.WriteLineAsync(e.Message);
                Console.Out.WriteLineAsync("SQL:" + sql);
                throw;
            }
        }

        private DbCommand GetCommand<T>(string sql, T par)
        {
            var cmd = factory.CreateCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            var t = typeof(T);
            var pis = t.GetProperties().ToList();

            pis.ForEach(p =>
            {
                var value = p.GetValue(par);
                if (value == null)
                    value = "";

                cmd.Parameters.Add(new OracleParameter(":" + p.Name, value));
            });

            return cmd;
        }
    }
}