using System;
using System.Collections.Generic;
using System.Text;

namespace hn.Common
{
    public class Key
    {
        public static readonly string AESkey=  "5EF58FDCC49547113CB1B29EF590C783";//加密所需32位密匙 hxl_apple@163.com
        public static readonly string DESkey = "1QAZ@wsx";

        /// <summary>
        /// SESSION键
        /// </summary>
        public enum SessionKeys
        {
            /// <summary>
            /// 登录用户ID
            /// </summary>
            UserID,
            /// <summary>
            /// 用户登录唯一ID
            /// </summary>
            UserUniqueID,
            /// <summary>
            /// 登录用户
            /// </summary>
            User,
            /// <summary>
            /// 验证码
            /// </summary>
            ValidateCode,
            /// <summary>
            /// 是否要检查验证码
            /// </summary>
            IsValidateCode,
            /// <summary>
            /// 主题
            /// </summary>
            Theme,
            /// <summary>
            /// 根路径
            /// </summary>
            BaseUrl
        }
       
    }
}
