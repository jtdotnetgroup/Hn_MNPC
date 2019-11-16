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
    [TableName("TB_CITY")]
    [Description("城市市表")]
    public class CityModel
    {
        public decimal CITYID { get; set; }
        public string CITYNAME { get; set; }
        public string ZIPCODE { get; set; }
        public decimal PROVINCEID { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
