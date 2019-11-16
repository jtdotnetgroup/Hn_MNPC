using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.Common.Cls_query
{
    public class QueryOrder
    {
        public string brand { set; get; }
        public string famount { set; get; }
        public string P_BillNo { set; get; }

        public DateTime startTime { set; get; }

        public DateTime endTime { set; get; }

        public string t_status { set; get; }

        public bool bClose { set; get; }
    }



    public class P_QueryOrder {
        public string areaid { set; get; }
        public string brand { set; get; }
        public string address { set; get; }
        public string P_BillNo { set; get; }
        public DateTime startTime { set; get; }
        public DateTime endTime { set; get; }

        public string t_status { set; get; }
    }

}
