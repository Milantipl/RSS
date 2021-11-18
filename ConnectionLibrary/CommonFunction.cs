using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary
{
   public static class CommonFunction
    {
       public static List<T> ToListof<T>(this DataTable dt)
       {
           const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
           var columnNames = dt.Columns.Cast<DataColumn>()
               .Select(c => c.ColumnName)
               .ToList();
           var objectProperties = typeof(T).GetProperties(flags);
           var targetList = dt.AsEnumerable().Select(dataRow =>
           {
               var instanceOfT = Activator.CreateInstance<T>();

               foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
               {
                   properties.SetValue(instanceOfT, dataRow[properties.Name], null);
               }
               return instanceOfT;
           }).ToList();

           return targetList;
       }
       /*Converts List To DataTable*/

       public static DataTable ToDataTable<TSource>(this IList<TSource> data, string tableName)
       {
           try
           {

               var dataTable = new DataTable(typeof(TSource).Name);
               var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);

               if (typeof(TSource).Name == "String")
               {
                   var datavalue = string.Empty;
                   var ColumnName = "Randomnumber";
                   dataTable.Columns.Add(Convert.ToString(ColumnName), ColumnName.GetType());
                   foreach (var item in data)
                   {
                       for (int i = 0; i < props.Length; i++)
                       {
                           dataTable.Rows.Add(Convert.ToString(item).Split('\\').LastOrDefault());
                       }
                   }
               }
               if (typeof(TSource).Name == "KeyValuePair`2")
               {
                   var datavalue = string.Empty;
                   foreach (var item in data)
                   {
                       var ColumnName = props[0].GetValue(item, null);
                       var Value = props[1].GetValue(item, null);
                       dataTable.Columns.Add(Convert.ToString(ColumnName), ColumnName.GetType());
                       if (dataTable.Rows.Count == 0)
                       {
                           dataTable.Rows.Add();
                       }
                       if (dataTable.Rows.Count == 1)
                       {
                           dataTable.Rows[0][Convert.ToString(ColumnName)] = Convert.ToString(Value);
                       }
                   }
               }
               else if (typeof(TSource).Name != "String")
               {
                   foreach (var prop in props)
                   {
                       dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                   }
                   foreach (TSource item in data)
                   {
                       var values = new object[props.Length];
                       for (int i = 0; i < props.Length; i++)
                       {
                           values[i] = props[i].GetValue(item, null);
                       }
                       dataTable.Rows.Add(values);
                   }
               }

               dataTable.TableName = tableName;
               return dataTable;

           }
           catch (Exception)
           {

               return null;
           }
       }
    }
}
