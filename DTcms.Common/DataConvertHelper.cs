using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace DTcms.Common
{
    /// <summary>
    /// 类型转换帮助类
    /// </summary>
    public class DataConvertHelper
    {
        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">表格</param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            // 定义集合   
            var ts = new List<T>();
            if (dt == null || dt.Rows.Count == 0) return ts;
            // 获得此模型的类型  
            var type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                var t = new T();
                // 获得此模型的公共属性     
                var propertys = type.GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列   
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter     
                        if (!pi.CanWrite) continue;
                        var value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
