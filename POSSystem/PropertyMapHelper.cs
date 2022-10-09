using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace POSSystem
{
    public class PropertyMapHelper
    {
        public static void Map(Type type, DataRow row, PropertyInfo prop, object entity)
        {
            String columnName = prop.Name;

            if (!String.IsNullOrWhiteSpace(columnName) && row.Table.Columns.Contains(columnName))
            {
                var propertyValue = row[columnName];
                if (propertyValue != DBNull.Value)
                {
                    ParsePrimitive(prop, entity, row[columnName]);
                }
            }
        }

        public static void ParsePrimitive(PropertyInfo prop, object entity, object value)
        {
            if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(entity, value.ToString().Trim(), null);
            }
            else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, int.Parse(value.ToString()), null);
                }
            }
        }
    }
}