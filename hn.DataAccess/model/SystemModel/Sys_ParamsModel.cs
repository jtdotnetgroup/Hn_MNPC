using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using hn.Core.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Model
{
    [TableName("SYS_PARAMS")]
    [Description("系统参数表")]
    public class SYS_PARAMSMODEL
    {
        /// <summary>
        /// 内码ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public string FKEY { get; set; }

        /// <summary>
        /// 项目值
        /// </summary>
        public string FVALUE { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string FDESCRIPTION { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }




        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
