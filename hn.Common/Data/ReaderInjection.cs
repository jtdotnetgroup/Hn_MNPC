using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Omu.ValueInjecter;

namespace hn.Common.Data
{
    public class ReaderInjection : KnownSourceValueInjection<IDataReader>
    {
        protected override void Inject(IDataReader source, object target)
        {
            for (var i = 0; i < source.FieldCount; i++)
            {
                var activeTarget = target.GetProps().GetByName(source.GetName(i),true);
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
                else if (activeTarget.PropertyType.Name=="String")
                {
                    activeTarget.SetValue(target, value.ToString());
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

                if (activeTarget.PropertyType.Name == "Int32")
                {
                    activeTarget.SetValue(target, PublicMethod.GetInt(value));
                }
                else if (activeTarget.PropertyType.Name == "Boolean")
                {
                    activeTarget.SetValue(target, PublicMethod.GetBool(value));
                }
                else if (activeTarget.PropertyType.Name == "String")
                {
                    activeTarget.SetValue(target, PublicMethod.GetString(value));
                }
                else
                {
                    activeTarget.SetValue(target, value);
                }
            }
        }
    }
}
