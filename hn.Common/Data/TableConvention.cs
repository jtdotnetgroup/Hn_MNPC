using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.Common.Data
{
    public static class TableConvention
    {

        public static string Resolve(Type t)
        {
            string _tablename = "";
            TableNameAttribute tableName;
            var name = t.Name;
            foreach(Attribute  attr in t.GetCustomAttributes(true))
            {
                tableName = attr as TableNameAttribute;
                if(tableName!=null)
                    _tablename = tableName.Name;
            }

            if (string.IsNullOrEmpty(_tablename))
            {
                if (name.EndsWith("s"))
                    _tablename = t.Name + "es";
                _tablename = t.Name + "s";
            }

            switch (_tablename)
            {
                case "MBP_TMS_DRIVER":
                case "TMS_ADDRESS":
                case "TMS_ITEM":
                case "TMS_PLATFORM":
                case "TMS_SALES_CUSTOMER":
                case "TMS_SCHEDULE":
                case "TMS_TRANS_ORDER":
                case "TMS_TRANS_ORDER_DETAIL":
                case "TMS_TRANS_TASK":
                case "TMS_TRANS_TASK_DETAIL":
                case "TMS_VEHICLE":
                case "TMS_VEHICLE_TYPE":
                case "USERS":

                case "V_TMS_SCHEDULE":
                case "V_TMS_TRANS_TASK":
                    _tablename = System.Configuration.ConfigurationManager.AppSettings["tmsUser"] +"."+ _tablename;
                    break;
            }

            return _tablename;
        }

        public static string Resolve(object o)
        {
            return Resolve(o.GetType());
        }
    }
}
