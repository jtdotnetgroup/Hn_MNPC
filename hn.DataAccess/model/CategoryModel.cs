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
    [TableName("TB_CATEGORY")]
    [Description("分类表")]
    public class CategoryModel
    {
        public string FID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string PARENT_ID { get; set; }
        public string CATEGORY_NUMBER { get; set; }
        public string TYPE { get; set; }

        [DbField(false)]
        public IEnumerable<CategoryModel> children
        {
            get { return CategoryDal.Instance.GetChildren(FID); }
        }

        public override string ToString()
        {
            return JSONhelper.ToJson(this);
        }
    }
}
