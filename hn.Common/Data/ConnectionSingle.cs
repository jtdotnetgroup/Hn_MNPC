using hn.Common.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace hn.Common.Data
{
    public class ConnectionSingle
    {

        string cs = SqlEasy.connString; //数据库连接字符串

        static ConnectionSingle instance;

        static object lockObject = new object();

        OracleConnection conn;

        public OracleConnection Conn
        {
            get
            {
                return conn;
            }

        }

        private ConnectionSingle()
        {
            conn = new OracleConnection();
            conn.ConnectionString = cs;
        }


        public static ConnectionSingle GetInstance()
        {
            lock ( lockObject)
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        instance = new ConnectionSingle();
                    }
                }

                return instance;
            }
        }
    }
}
