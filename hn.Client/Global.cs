using hn.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MApiModel;
using System.Drawing;

namespace hn.Client
{
    public class Global
    {

        //访问蒙厂的TOKEN
        public static MApiModel.recToken.Rootobject MToken;

     

        public static DataTable TableMarketArea;

        public static User LoginUser;

        public static string WcfUrl = "";



        public static string IniUrl
        {
            get
            {
                return Application.StartupPath + @"\Config.ini";
            }
        }

        public class BufferData
        {
            public static object ProductData;
        }

        /// <summary>
        /// Web版地址
        /// </summary>
        public static string WebUrl = "";

        /// <summary>
        /// 版本信息
        /// </summary>
        public readonly static string Version = "V1.0.36";

        /// <summary>
        /// 内部版本号
        /// </summary>
        public readonly static int VersionInterior = 37;

        public readonly static string UpdatePath = Application.StartupPath + "\\Update.txt";
    }
}
