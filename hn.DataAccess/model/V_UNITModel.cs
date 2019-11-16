using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.Model
{
    [TableName("V_UNIT")]
    [Description("计量单位")]
    public class V_UNITModel
    {
        //内码ID
        public string FID { get; set; }
        //单位组ID
        public string FGROUPID { get; set; }
        //编号
        public string FNUMBER { get; set; }
        //名称
        public string FNAME { get; set; }

        /// <summary>
        /// 是否默认选中
        /// </summary>
        public int FDEFAULT { get; set; }

        /// <summary>
        /// 是否默认选中
        /// </summary>
        [DbField(false)]
        public string FDEFAULTNAME
        {
            get
            {
                return FDEFAULT == 1 ? "是" : "否";
            }
        }
        //换算率
        public decimal FRATE { get; set; }
        //时间
        public DateTime FUPDATETIME { get; set; }
        //备注
        public string FREMARK { get; set; }

        /// <summary>
        /// 单位组名称
        /// </summary>
        public string FGROUPNAME { get; set; }


        
    }
}
