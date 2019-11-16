using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Converters;
using hn.Common;
using System.Web.Mvc;
namespace hn.Core
{
    public class RequestParamModel<T> where T:class
    {
        private FormCollection _context;
        public  RequestParamModel(){}
        public RequestParamModel(FormCollection context)
        {
            this._context = context;
        }

        public FormCollection CurrentContext
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        [DefaultValue(0)]
        public string FID { get; set; }

        /// <summary>
        /// 批量处理多个ID，格式：1,2,3,4,5......
        /// </summary>
        public string KeyIds { get; set; }


        /// <summary>
        /// 实体JSON
        /// </summary>
        public string JsonEntity
        {
            get;
            set;
        }
        /// <summary>
        /// 实体类
        /// </summary>
        public T Entity {
            get
            {
                var errors = new List<string>();
                return string.IsNullOrEmpty(JsonEntity) ? null : JsonConvert.DeserializeObject<T>(JsonEntity, new JsonSerializerSettings
                {
                    Error = delegate(object obj, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                    {
                        errors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    },
                    Converters = { new IsoDateTimeConverter() }

                });
            }
        }

        public string Request(string key)
        {
            return _context[key];
        }

        /// <summary>
        /// 页索引
        /// </summary>
        public int Pageindex
        {
            get
            {
                int pageindex;
                int.TryParse(_context["page"], out pageindex);
                if (pageindex == 0)
                    pageindex = 1;
                return pageindex;
            }
        }

        /// <summary>
        /// grid 排序字段
        /// </summary>
        public string Sort
        {
            get { return _context["sort"]; }
        }

        /// <summary>
        /// grid 排序方式 asc || desc
        /// </summary>
        public string Order
        {
            get { return _context["order"]; }
        }


        /// <summary>
        /// 页尺寸
        /// </summary>
        public int Pagesize
        {
            get
            {
                int pagesize;
                int.TryParse(_context["rows"], out pagesize);
                if (pagesize == 0)
                    pagesize = 20;
                return pagesize;
            }
        }

        private string _filter = "";
        /// <summary>
        /// 筛选条件
        /// </summary>
        public string Filter
        {
            get
            {
                if (_context == null)
                {
                    return _filter;
                }
                return PublicMethod.GetString(_context["filter"]);
            }
            set
            {
                _filter = value;
            }
        }

        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }
        public string Data6 { get; set; }

    }

    public class EntityData<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
    }
}
