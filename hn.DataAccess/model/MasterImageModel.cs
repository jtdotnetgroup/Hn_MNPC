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
    [TableName("TB_MASTER_IMAGE")]
    [Description("技师图片")]
    public class MasterImageModel
    {
        public string FID { get; set; }
        public string MASTER_ID { get; set; }
        public string IMAGE_URL { get; set; }
        
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
