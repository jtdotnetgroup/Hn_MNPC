using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace hn.Common.Data.Checker
{
    public class DataChecker
    {
        public static bool CheckObj<T>(T obj)
        {
            var t = typeof(T);
            var pis = t.GetProperties().ToList();

            pis.ForEach(p =>
            {
                string fieldName = p.Name;
                var value = p.GetValue(obj, null);

                if (IsRequired(p) && value == null||string.IsNullOrEmpty(value.ToStr()))
                {
                    throw new Exception(string.Format("【{0}】字段不能为空",fieldName));
                }

            });

            return true;
        }

        private static bool IsRequired(PropertyInfo p)
        {
            var attList= p.GetCustomAttributes(true).ToList();

            var count= attList.Where(pi => pi is RequiredAttribute).Count();

            if (count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
