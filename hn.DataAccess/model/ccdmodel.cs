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
    [TableName("ccd")]
    [Description("ccd")]


    public class ccdModel
    {

        public string autoid { get; set; }
        public string pzhm { get; set; }

        public string rq { get; set; }
        public string khhm { get; set; }
        public string khmc { get; set; }
        public string cppz { get; set; }
        public string cpgg { get; set; }
        public string cpxh { get; set; }

        public string cpdj { get; set; }

        public string dw { get; set; }

        public string ks { get; set; }
        public string sl { get; set; }
        public string dj { get; set; }
        public string je { get; set; }
        public string db { get; set; }
      
        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }

  
}
