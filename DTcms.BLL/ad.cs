using System;
using System.Data;

namespace DTcms.BLL
{
    /// <summary>
    /// 业务逻辑类Adbanner 的摘要说明。
    /// </summary>
    public class ad
    {
        private readonly Model.siteconfig _siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.ad dal;
        public ad() {
            dal = new DAL.ad(_siteConfig.sysdatabaseprefix);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id) {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 返回查询数据总数（分页用到）
        /// </summary>
        public int GetCount(string strWhere) {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.ad model) {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.ad model) {
            return dal.Update(model);
        }

        public void UpdateField(int id, string strValue) {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id) {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ad GetModel(int id) {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere) {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount) {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int top, string strWhere, string filedOrder) {
            return dal.GetList(top, strWhere, filedOrder);
        }
    }
}
