using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.BLL
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
            return new DTcms.DAL.Scheduling_Custom().BindScheduling(list);
        }
    }
}
