using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using hn.Common;
using hn.Common.Data;
using hn.Common.Data.Filter;
using hn.Common.Provider;
using hn.Core.Dal;
using hn.Core.Model;
using System.Data.SqlClient;
using hn.Common.Data.SqlServer;
using hn.Core;
using hn.Core.Bll;
using hn.DataAccess.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Bll
{
    public class MessageBll
    {
        public static MessageBll Instance
        {
            get { return SingletonProvider<MessageBll>.Instance; }
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return MessageDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }

        public string GetListJson(int pageindex, int pagesize, string driverids, string filterJson, string sort, string order)
        {
            string where = FilterTranslator.ToSql(filterJson);

            DataTable table = MessageDal.Instance.GetList(pageindex, pagesize, driverids, where, sort, order);
            return JSONhelper.FormatJSONForEasyuiDataGrid(MessageDal.Instance.GetCount(driverids,where), table);
        }

        public string BatchSend(string FReceiverIDList = null, string title = null, string content = null)
        {
            if (FReceiverIDList.IsNullOrEmpty())
            {
                return "发送人不能为空！";
            }

            if (title.IsNullOrEmpty())
            {
                return "标题不能为空！";
            }

            if (content.IsNullOrEmpty())
            {
                return "内容不能为空！";
            }

            MessageModel model = new MessageModel();

            model.FSenderID = SysVisitor.Instance.UserId;
            model.FState = 0;
            model.FType = Constant.TB_MESSAGE_FTYPE.系统消息.ToInt();
            model.FDate = DateTime.Now;
            model.FTitle = title;
            model.FContent = content.ToByte();

            foreach (var item in FReceiverIDList.SplitWithoutSpace())
            {
                model.FReceiverID = item;
                MessageDal.Instance.Insert(model);
            }

            return null;
        }
    }

}
