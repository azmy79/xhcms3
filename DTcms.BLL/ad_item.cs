using System;
using System.Data;

namespace DTcms.BLL
{
    /// <summary>
    /// 广告位业务逻辑类
    /// </summary>
    public class ad_item
    {
        private readonly Model.siteconfig _siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.ad_item dal;

        public ad_item() {
            dal = new DAL.ad_item(_siteConfig.sysdatabaseprefix);
        }

        #region  成员方法
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
        public bool Add(Model.ad_item model) {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.ad_item model) {
          return  dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id) {

            dal.Delete(id);
        }

        //5*1*a*s*p*x
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ad_item GetModel(int id) {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere) {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int top, string strWhere, string filedOrder) {
            return dal.GetList(top, strWhere, filedOrder);
        }

        #endregion  成员方法
    }
}
