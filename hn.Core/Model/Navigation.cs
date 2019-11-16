using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using hn.Common.Data;
using hn.Core.Dal;

namespace hn.Core.Model
{
    [TableName("sys_Navigations")]
    [Description("导航菜单")]
    public class Navigation
    {
        public string FID { get; set; }
        [Description("菜单名称")]
        public string NavTitle { get; set; }
        [Description("链接地址")]
        public string Linkurl { get; set; }
        [Description("排序")]
        public int Sortnum { get; set; }
        [Description("图标CSS")]
        public string iconCls { get; set; }
        [Description("图标URL")]
        public string iconUrl { get; set; }
        [Description("是否显示")]
        public bool IsVisible { get; set; }
        [Description("父ID")]
        public string ParentID { get; set; }
        [Description("菜单标识")]
        public string NavTag { get; set; }
        [Description("大图标路径")]
        public string BigImageUrl { get; set; }

        [DbField(false)]
        public IEnumerable<Navigation> children
        {
            get
            {
                return NavigationDal.Instance.GetList(FID);
            }
        }

        [DbField(false)]
        public IEnumerable<Button> Buttons
        {
            get { return ButtonDal.Instance.GetButtonsBy(FID); }
        }

    }
}
