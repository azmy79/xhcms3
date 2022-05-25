using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.DAL
{
    public class Scheduling_Custom
    {
        /// <summary>
        /// 绑定排班数据
        /// </summary>
        /// <param name="list">排班对象集合</param>
        /// <returns></returns>
        public bool BindScheduling(List<DTcms.Model.Scheduling> list)
        {
            var ret = false;
            try
            {
                var sqlStr = "delete Scheduling";
                if (list != null && list.Count > 0)
                    list.ForEach(p =>
                    {
                        sqlStr += " insert into  Scheduling(Day,MonthType,ManagerID)values(" + p.Day + "," + p.MonthType + "," + p.ManagerID + ") ";
                    });
                DTcms.DBUtility.DbHelperSQL.ExecuteSql(sqlStr);
                ret = true;
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
    }
}
