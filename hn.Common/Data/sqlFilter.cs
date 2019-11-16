using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Common.Data.Filter;

namespace hn.Common.Data
{
    public class sqlFilter
    {
        public string groupOp { get; set; }
        public IList<FilterRule> rules { get; set; }

        public sqlFilter(string _group, FilterRule rule)
        {
            this.groupOp = _group;
            this.rules = new List<FilterRule>();
            rules.Add(rule);
        }


        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
