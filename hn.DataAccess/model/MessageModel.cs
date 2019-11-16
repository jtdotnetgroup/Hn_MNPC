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
    [TableName("TB_MESSAGE")]
    [Description("消息记录")]
    public class MessageModel
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime FDate { get; set; }

        /// <summary>
        /// 时间字符串
        /// </summary>
        [DbField(false)]
        public string FDateString
        {
            get
            {
                return FDate.ToDateTimeString();
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string FTitle { get; set; }

        /// <summary>
        /// 发送人ID
        /// </summary>
        public string FSenderID { get; set; }

        /// <summary>
        /// 接收人ID
        /// </summary>
        public string FReceiverID { get; set; }

        /// <summary>
        /// 阅读标识
        /// </summary>
        public int FState { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public byte[] FContent { get; set; }


        /// <summary>
        /// 内容字符串
        /// </summary>
        [DbField(false)]
        public string FContentString
        {
            get
            {
                return FContent.ToString();
            }
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public int FType { get; set; }

        /// <summary>
        /// 消息子类型
        /// </summary>
        public int FSubType { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string FBillNo { get; set; }

    }
}
