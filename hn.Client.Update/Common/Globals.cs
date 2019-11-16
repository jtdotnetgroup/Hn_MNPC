using System.Windows.Forms;

namespace hn.Client.Update
{
    public class Globals
    {
        /// <summary>
        /// 版本信息
        /// </summary>
        public readonly static string Version = "V2.1.0";

        /// <summary>
        /// 内部版本号
        /// </summary>
        public readonly static int VersionInterior = 10;


        public readonly static string ConfigPath = Application.StartupPath + @"\Config.ini";

        public readonly static string UpdatePath = Application.StartupPath + "\\Update.txt";


        /// <summary>
        /// 云端IP
        /// </summary>
        public static string IP = "";

        /// <summary>
        /// 云端端口
        /// </summary>
        public static string Port = "";

 
    }
}
