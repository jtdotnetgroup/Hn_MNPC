using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Core.Dal;
using hn.Core.Model;
using hn.Common.Provider;
using hn.Common.Data;

namespace hn.Core.Bll
{
    public class ButtonBll
    {
        public static ButtonBll Instance
        {
            get { return SingletonProvider<ButtonBll>.Instance; }
        }

        /// <summary>
        /// 判断按钮是否存在
        /// </summary>
        /// <param name="title">按钮名称</param>
        /// <param name="code">编码</param>
        /// <param name="FID">按钮ID</param>
        /// <returns></returns>
        private bool HasButton(Button b)
        {
            var btns = ButtonDal.Instance.GetAll();
            foreach (Button button in btns)
            {
                if ((button.ButtonText == b.ButtonText || button.ButtonTag == b.ButtonTag) && b.FID != null && button.FID != b.FID)
                {
                    return true;
                }
            }

            return false;
            //var count = btns.Select(n => (n.ButtonText == b.ButtonText || n.ButtonTag == b.ButtonTag) && n.FID != b.FID).Count();

            //return count > 0;
        }

        public string AddButton(Button b)
        {
            if(HasButton(b))
                return new JsonMessage { Success = false, Data = "0", Message = "按钮名称或编码已存存！" }.ToString();

            string k = ButtonDal.Instance.Insert(b);
            var msg = "添加成功。";
            if (k == "")
                msg = "添加失败。";
            else
            {
                LogBll<Button> log = new LogBll<Button>();
                b.FID = k;
                log.AddLog(b);
            }
            return new JsonMessage {Success = true, Data = k.ToString(), Message = msg}.ToString();
        }

        public string EditButton(Button b)
        {
            if (HasButton(b))
                return new JsonMessage { Success = false, Data = "0", Message = "按钮名称或编码已存存！" }.ToString();

            var oldBtn = ButtonDal.Instance.Get(b.FID);
            int k = ButtonDal.Instance.Update(b);
            var msg = "修改成功。";
            if (k <= 0)
                msg = "修改失败。";
            else
            {
                LogBll<Button> log = new LogBll<Button>();
                log.UpdateLog(oldBtn, b);
            }
            return new JsonMessage { Success = true, Data = k.ToString(), Message = msg }.ToString();
        }

        public string DelButton(string btnId)
        {
            var btn = ButtonDal.Instance.Get(btnId);
            int k = ButtonDal.Instance.Delete(btnId);

            var msg = "删除成功。";
            if (k <= 0)
                msg = "删除失败。";
            else
            {
                LogBll<Button> log = new LogBll<Button>();
                log.DeleteLog(btn);
            }

            return new JsonMessage { Success = true, Data = k.ToString(), Message = msg }.ToString();
        }

    }
}
