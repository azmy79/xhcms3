using System.Data;

namespace DTcms.BLL
{
    public class link
    {
        private readonly DAL.link dal;
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();

        public link() {
            dal = new DAL.link(siteConfig.sysdatabaseprefix);
        }

        public int Add(Model.link model) {
            return dal.Add(model);
        }

        public bool Delete(int id) {
            return dal.Delete(id);
        }

        public bool Exists(int id) {
            return dal.Exists(id);
        }

        public DataSet GetList(string strWhere) {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere) {
            return dal.GetList(Top, strWhere);
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount) {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        public Model.link GetModel(int id) {
            return dal.GetModel(id);
        }

        public bool Update(Model.link model) {
            return dal.Update(model);
        }

        public void UpdateField(int id, string strValue) {
            dal.UpdateField(id, strValue);
        }
    }
}

