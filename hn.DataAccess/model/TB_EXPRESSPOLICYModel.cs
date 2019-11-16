using hn.Common;
using hn.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace hn.DataAccess.Model
{
    [TableName("TB_EXPRESSPOLICY")]
    [Description("运费政策")]
    public class TB_EXPRESSPOLICYModel
    {
        //内码ID
        public string FID { get; set; }
        public string FNAME { get; set; }
        public decimal FWEIGHTFEE { get; set; }
        public decimal FVOLUMEFEE { get; set; }
        public string FSTARTTIME { get; set; }
        public string FDEADLINE { get; set; }
        public string FDESC { get; set; }
    }
}
