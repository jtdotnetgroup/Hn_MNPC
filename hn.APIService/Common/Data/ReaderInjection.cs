using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Omu.ValueInjecter;

namespace hn.APIService
{
    public class ReaderInjection : KnownSourceValueInjection<IDataReader>
    {
        protected override void Inject(IDataReader source, object target)
        {
            for (var i = 0; i < source.FieldCount; i++)
            {
                var activeTarget = target.GetProps().GetByName(source.GetName(i), true);
                if (activeTarget == null) continue;

                var value = source.GetValue(i);
                if (value == DBNull.Value) continue;

                if (activeTarget.PropertyType.Name == "Int32")
                {
                    activeTarget.SetValue(target, PublicMethod.GetInt(value));
                }
                else if (activeTarget.PropertyType.Name == "Boolean")
                {
                    activeTarget.SetValue(target, PublicMethod.GetBool(value));
                }
                else
                {
                    activeTarget.SetValue(target, value);
                }
            }
        }
    }

    public class DataRowInjection : KnownSourceValueInjection<DataRow>
    {
        protected override void Inject(DataRow source, object target)
        {
            for (var i = 0; i < source.Table.Columns.Count; i++)
            {
                var activeTarget = target.GetProps().GetByName(source.Table.Columns[i].ToString(), true);
                if (activeTarget == null) continue;

                var value = source.ItemArray[i];
                if (value == DBNull.Value) continue;

                activeTarget.SetValue(target, value);
            }
        }
    }

    public static class ModelCopy
    {
        /// <summary>  
        /// 拷贝简单属性和公共字段  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="source"></param>  
        /// <param name="target"></param>  
        public static void CopyTo<T>(this object source, T target) where T : class
        {
            if (source == null)
            {
                return;
            }

            if (target == null)
            {
                throw new ApplicationException("target 未实例化！");
            }

            var properties = target.GetType().GetProperties();
            foreach (var targetPro in properties)
            {
                try
                {
                    //判断源对象是否存在与目标属性名字对应的源属性  
                    if (source.GetType().GetProperty(targetPro.Name) == null)
                    {
                        continue;
                    }
                    //判断是否枚举集合  
                    if (targetPro.PropertyType.IsGenericType && targetPro.PropertyType.GetGenericArguments()[0].IsEnum)
                    {
                        continue;
                    }
                    // 判断是否数组  
                    else if (targetPro.PropertyType.IsArray)
                    {
                        continue;
                    }
                    // 判断是否IList  
                    else if (targetPro.PropertyType.IsGenericType && targetPro.PropertyType.GetInterface("System.Collections.IEnumerable") != null)
                    {
                        continue;
                    }

                    var propertyValue = source.GetType().GetProperty(targetPro.Name).GetValue(source, null);
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType().IsEnum)
                        {
                            continue;
                        }

                        target.GetType().InvokeMember(targetPro.Name, System.Reflection.BindingFlags.SetProperty, null, target, new object[] { propertyValue });
                    }
                }
                catch (Exception ex)
                {

                }
            }

            //返回所有公共字段  
            var targetFields = target.GetType().GetFields();
            foreach (var filed in targetFields)
            {
                try
                {
                    var tfield = source.GetType().GetField(filed.Name);
                    if (null == tfield)
                    {
                        //如果源对象中不包含这个公共字段则不处理  
                        continue;
                    }
                    //类型不一致不处理  
                    if (filed.FieldType.FullName != tfield.FieldType.FullName)
                    {
                        continue;
                    }
                    var fieldValue = tfield.GetValue(source);
                    if (fieldValue != null)
                    {
                        target.GetType().InvokeMember(filed.Name, System.Reflection.BindingFlags.SetField, null, target, new object[] { fieldValue });
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
