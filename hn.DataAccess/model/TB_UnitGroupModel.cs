using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.model
{
    [TableName("TB_UNITGROUP")]
    [Description("计量单位组")]
    public class TB_UnitGroupModel
    {
        //内码ID
        public string FID { get; set; }
        //商品资料ID
        public string FPRODUCTID { get; set;}
        //与主单位换算率
        public decimal FRATE { get; set; }
        //单位ID
        public string FUNITID { get; set; }
        //单位名称
        public string FUNITNAME { get; set; }
        //更新时间
        public DateTime FUPDATETIME { get; set; }
        //备注
        public string FREMARK { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
