using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IniHelper
    {
        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public static bool WriteString(string filename, string Section, string Ident, string Value)
        {
            if (!WritePrivateProfileString(Section, Ident, Value, filename))
            {
                return false;
                //throw (new ApplicationException("写Ini文件出错"));
                //return false;
            }
            return true;
        }

        /// <summary>
        /// 读取INI文件指定
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static string ReadString(string filename, string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), filename);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            //s = s.Substring(0, bufLen);
            s = LeftB(s, bufLen, " ");
            return s.Trim();
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
