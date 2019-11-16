using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("TB_MESSAGE")]
    [Description("消息发送")]
    public class TB_MESSAGEMODEL
    {
        /// <summary>
        /// FID
        /// </summary>
        public string FID { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime FDATE { get; set; }
        /// <summary>
        /// 发送题目
        /// </summary>
        public string FTITLE { get; set; }
        /// <summary>
        /// 发送题目
        /// </summary>
        public string FSENDERID { get; set; }
        /// <summary>
        /// 接收人ID
        /// </summary>
        public string FRECEIVERID { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public int FSTATE { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public byte[] FCONTENT { get; set; }
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

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }

    }
}
