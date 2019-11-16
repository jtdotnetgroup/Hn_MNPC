using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace hn.Client.Service.Common
{
    public class DBHelper
    {
        private  string  conStr=ConfigurationManager.AppSettings["DbConnection"];
        private static OracleConnection conn;

        private static DBHelper instance { get; set; }

        private static object lockobj = new object();

        private DBHelper(OracleConnection conn)
        {
            conn = conn;
        }

        public OracleConnection GetConnection()
        {
            return conn;
        }

        public static DBHelper GetInstance(OracleConnection conn)
        {
            lock (lockobj)
            {
                if (instance == null)
                {
                    lock (lockobj)
                    {
                        instance = new DBHelper(conn);
                    }
                }

                return instance;
            }
        }

        public OracleTransaction getTransaction()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();

            }

            return conn.BeginTransaction();
        }

        public int ExecuteNoneQuery(string sql)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            OracleCommand cmd = new OracleCommand(sql, conn);
            return cmd.ExecuteNonQuery();
        }
        public int ExecuteNoneQuery(string sql, OracleTransaction tran)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            OracleCommand cmd = new OracleCommand(sql,conn);
            cmd.Transaction = tran;
            return cmd.ExecuteNonQuery();
        }

        public Object ExecuteScalar(string sql)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            OracleCommand cmd = new OracleCommand(sql, conn);
            return cmd.ExecuteScalar();
        }

        public Object ExecuteScalar(string sql,OracleTransaction tran)
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.Transaction = tran;
            return cmd.ExecuteScalar();
        }
    }
}
