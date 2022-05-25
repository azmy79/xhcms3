using System.Data;

namespace DTcms.BLL
{
    public class feedback
    {
        private readonly DAL.feedback dal;
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();

        public feedback() {
            this.dal = new DAL.feedback(this.siteConfig.sysdatabaseprefix);
        }

        public int Add(Model.feedback model) {
            return this.dal.Add(model);
        }

        public bool Delete(int id) {
            return this.dal.Delete(id);
        }

        public bool Exists(int id) {
            return this.dal.Exists(id);
        }

        public DataSet GetList(int Top, string strWhere) {
            return this.dal.GetList(Top, strWhere);
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount) {
            return this.dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        public Model.feedback GetModel(int id) {
            return this.dal.GetModel(id);
        }

        public bool Update(Model.feedback model) {
            return this.dal.Update(model);
        }

        public void UpdateField(int id, string strValue) {
            this.dal.UpdateField(id, strValue);
        }
    }
}

