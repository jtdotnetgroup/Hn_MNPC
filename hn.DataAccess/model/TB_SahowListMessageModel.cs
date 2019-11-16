using hn.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    //显示列表字段
    public class TB_SahowListMessageModel
    {
        //发送ID
        public string FID { get; set; }
        //发送时间
        public DateTime FDate { get; set; }
        //发送题目
        public string FTitle { get; set; }
        //发送人ID
        public string FSenderID { get; set; }
        //发送人名字
        public string FSenderName { get; set; }
        //接收人ID
        public string FReceiverID { get; set; }
        //接收人名字
        public string FReceiverName { get; set; }
        //发送状态
        public int FState { get; set; }
        //发送内容
        public string FContent { get; set; }
        //消息类型
        public int FType { get; set; }
        //消息子类型
        public int FSubType { get; set; }
        //订单号
        public string FBillNo { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
