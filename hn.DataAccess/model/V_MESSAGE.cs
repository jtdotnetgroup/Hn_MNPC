using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("V_MESSAGE")]
    [Description("消息发送")]
    public class V_MESSAGEMODEL
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime FDATE { get; set; }

        /// <summary>
        /// 时间字符串
        /// </summary>
        [DbField(false)]
        public string FDATESTRING
        {
            get
            {
                return FDATE.ToDateTimeString();
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string FTITLE { get; set; }

        /// <summary>
        /// 发送人ID
        /// </summary>
        public string FSENDERID { get; set; }

        /// <summary>
        /// 接收人ID
        /// </summary>
        public string FRECEIVERID { get; set; }

        /// <summary>
        /// 阅读标识
        /// </summary>
        public int FSTATE { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public byte[] FCONTENT { get; set; }


        /// <summary>
        /// 内容字符串
        /// </summary>
        [DbField(false)]
        public string FCONTENTSTRING
        {
            get
            {
                return FCONTENT.ToString();
            }
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public int FTYPE { get; set; }

        /// <summary>
        /// 消息子类型
        /// </summary>
        public int FSUBTYPE { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string FBILLNO { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string FRECEIVERNAME { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string FSENDERNAME { get; set; }
    }
}
