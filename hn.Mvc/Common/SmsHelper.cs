using hn.Common;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.Mvc
{
    public class SmsHelper
    {
        public static void SendSms(string phone,string content)
        { 
            YianjuSms.yianjuSms sms = new YianjuSms.yianjuSms();
            sms.content = content;  //  内容
            sms.mobile = phone;  //  手机号
            sms.senderCode = "yianju";  //  帐号
            YianjuSms.YianjuSmsServiceService service = new YianjuSms.YianjuSmsServiceService();
            YianjuSms.response res1 = service.sendSms(sms);
            if(res1.message == "发送成功。")
            {
                SmsModel model = new SmsModel();
                model.PHONE = phone;
                model.CONTENT = content;
                model.SEND_CODE = "yianju";
                model.SEND_TIME = DateTime.Now;
                SmsDal.Instance.Insert(model);
            }
            else
            {
                LogHelper.WriteLog(string.Format("phone={0},content={1},resmeesage={2}",phone,content,res1.message));
            }
        }
    }
}
