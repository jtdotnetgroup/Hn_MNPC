using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace hn.Client
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
        public static void WriteString(string filename, string Section, string Ident, string Value)
        {
            if (!WritePrivateProfileString(Section, Ident, Value, filename))
            {

                throw (new ApplicationException("写Ini文件出错"));
            }
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
            s = UtilHelper.LeftB(s, bufLen, " ");
            return s.Trim();
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static int ReadInteger(string filename, string Section, string Ident, int Default)
        {
            string intStr = ReadString(filename, Section, Ident, Convert.ToString(Default));
            try
            {
                return Convert.ToInt32(intStr);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Default;
            }
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public static void WriteInteger(string filename, string Section, string Ident, int Value)
        {
            WriteString(filename, Section, Ident, Value.ToString());
        }

        /// <summary>
        /// 读布尔
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public bool ReadBool(string filename, string Section, string Ident, bool Default)
        {
            try
            {
                return Convert.ToBoolean(ReadString(filename, Section, Ident, Convert.ToString(Default)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Default;
            }
        }

        /// <summary>
        /// 写Bool
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <param name="Value"></param>
        public static void WriteBool(string filename, string Section, string Ident, bool Value)
        {
            WriteString(filename, Section, Ident, Convert.ToString(Value));
        }

        /// <summary>
        /// 从Ini文件中，将指定的Section名称中的所有Ident添加到列表中
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Idents"></param>
        public static void ReadSection(string filename, string Section, StringCollection Idents)
        {
            Byte[] Buffer = new Byte[16384];
            //Idents.Clear();

            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0),
             filename);
            //对Section进行解析
            GetStringsFromBuffer(Buffer, bufLen, Idents);
        }

        private static void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }

        /// <summary>
        /// 从Ini文件中，读取所有的Sections的名称
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="SectionList"></param>
        public static void ReadSections(string filename, StringCollection SectionList)
        {
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
            byte[] Buffer = new byte[65535];
            int bufLen = 0;
            bufLen = GetPrivateProfileString(null, null, null, Buffer,
             Buffer.GetUpperBound(0), filename);
            GetStringsFromBuffer(Buffer, bufLen, SectionList);
        }

        /// <summary>
        /// 读取指定的Section的所有Value到列表中
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Values"></param>
        public static void ReadSectionValues(string filename, string Section, NameValueCollection Values)
        {
            StringCollection KeyList = new StringCollection();
            ReadSection(filename, Section, KeyList);
            Values.Clear();
            foreach (string key in KeyList)
            {
                Values.Add(key, ReadString(filename, Section, key, ""));

            }
        }

        /// <summary>
        /// 清除某个Section
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        public static void EraseSection(string filename, string Section)
        {
            if (!WritePrivateProfileString(Section, null, null, filename))
            {

                throw (new ApplicationException("无法清除Ini文件中的Section"));
            }
        }

        /// <summary>
        /// 删除某个Section下的键
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        public static void DeleteKey(string filename, string Section, string Ident)
        {
            WritePrivateProfileString(Section, Ident, null, filename);
        }

        /// <summary>
        /// Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件
        /// 在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile
        /// 执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
        /// </summary>
        /// <param name="filename"></param>
        public static void UpdateFile(string filename)
        {
            WritePrivateProfileString(null, null, null, filename);
        }

        /// <summary>
        /// 检查某个Section下的某个键值是否存在
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Ident"></param>
        /// <returns></returns>
        public static bool ValueExists(string filename, string Section, string Ident)
        {
            StringCollection Idents = new StringCollection();
            ReadSection(filename, Section, Idents);
            return Idents.IndexOf(Ident) > -1;
        }
    }
}
