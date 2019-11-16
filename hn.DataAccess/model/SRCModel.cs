using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.Model
{
    [TableName("SRC")]
    [Description("厂家代码表")]
    public class SRCModel
    {
        //内码ID
        public string FID { get; set; }
        public string FPRODUCTID { get; set; }
        public string FSRCNAME { get; set; }
        public string FSRCCODE { get; set; }
        public string FSRCMODEL { get; set; }
        public string FUNIT { get; set; }
        public string FORDERUNIT { get; set; }
        public decimal FRATE { get; set; }
        public decimal FWEIGHT { get; set; }
    }
}
