﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Common;
using hn.Common.Data;
namespace hn.Core.Model
{
    [TableName("Sys_DicCategory")]
    public class DicCategory
    {
        public string FID { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public int Sortnum { get; set; }

        public string Remark { get; set; }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }

    }
}
