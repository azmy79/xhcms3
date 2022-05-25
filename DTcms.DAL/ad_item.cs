using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 广告条数据访问类
    /// </summary>
    public class ad_item
    {

        private string databaseprefix; //数据库表名前缀
        public ad_item(string _databaseprefix) {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "ad_item");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "ad_item");
            if (strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ad_item model) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "ad_item(");
            strSql.Append("ad_id,title,tag,start_time,end_time,ad_url,link_url,remarks,is_lock,add_time,sort_id)");
            strSql.Append(" values (");
            strSql.Append("@ad_id,@title,@tag,@start_time,@end_time,@ad_url,@link_url,@remarks,@is_lock,@add_time,@sort_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ad_id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@tag", SqlDbType.NVarChar,50),
					new SqlParameter("@start_time", SqlDbType.DateTime),
					new SqlParameter("@end_time", SqlDbType.DateTime),
					new SqlParameter("@ad_url", SqlDbType.NVarChar,250),
					new SqlParameter("@link_url", SqlDbType.NVarChar,250),
					new SqlParameter("@remarks", SqlDbType.NVarChar,-1),
					new SqlParameter("@is_lock", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@sort_id", SqlDbType.Int,4)};
            parameters[0].Value = model.ad_id;
            parameters[1].Value = model.title;
            parameters[2].Value = model.tag;
            parameters[3].Value = model.start_time;
            parameters[4].Value = model.end_time;
            parameters[5].Value = model.ad_url;
            parameters[6].Value = model.link_url;
            parameters[7].Value = model.remarks;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.sort_id;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.ad_item model) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "ad_item set ");
            strSql.Append("ad_id=@ad_id,");
            strSql.Append("title=@title,");
            strSql.Append("tag=@tag,");
            strSql.Append("start_time=@start_time,");
            strSql.Append("end_time=@end_time,");
            strSql.Append("ad_url=@ad_url,");
            strSql.Append("link_url=@link_url,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("sort_id=@sort_id");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@ad_id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@tag", SqlDbType.NVarChar,50),
					new SqlParameter("@start_time", SqlDbType.DateTime),
					new SqlParameter("@end_time", SqlDbType.DateTime),
					new SqlParameter("@ad_url", SqlDbType.NVarChar,250),
					new SqlParameter("@link_url", SqlDbType.NVarChar,250),
					new SqlParameter("@remarks", SqlDbType.NVarChar,-1),
					new SqlParameter("@is_lock", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.ad_id;
            parameters[1].Value = model.title;
            parameters[2].Value = model.tag;
            parameters[3].Value = model.start_time;
            parameters[4].Value = model.end_time;
            parameters[5].Value = model.ad_url;
            parameters[6].Value = model.link_url;
            parameters[7].Value = model.remarks;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.sort_id;
            parameters[11].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id) {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "ad_item ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "ad_item ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ad_item GetModel(int id) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from " + databaseprefix + "ad_item ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            //Model.ad_item model = new Model.ad_item();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds.Tables[0].Rows.Count > 0 ? DataRowToModel(ds.Tables[0].Rows[0]) : null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ad_item DataRowToModel(DataRow row) {
            if (row == null)
                return null;

            Model.ad_item model = new Model.ad_item();
            if (row["id"] != null && row["id"].ToString() != "") {
                model.id = int.Parse(row["id"].ToString());
            }
            if (row["ad_id"] != null && row["ad_id"].ToString() != "") {
                model.ad_id = int.Parse(row["ad_id"].ToString());
            }
            if (row["title"] != null) {
                model.title = row["title"].ToString();
            }
            if (row["tag"] != null) {
                model.tag = row["tag"].ToString();
            }
            if (row["start_time"] != null && row["start_time"].ToString() != "") {
                model.start_time = DateTime.Parse(row["start_time"].ToString());
            }
            if (row["end_time"] != null && row["end_time"].ToString() != "") {
                model.end_time = DateTime.Parse(row["end_time"].ToString());
            }
            if (row["ad_url"] != null) {
                model.ad_url = row["ad_url"].ToString();
            }
            if (row["link_url"] != null) {
                model.link_url = row["link_url"].ToString();
            }
            if (row["remarks"] != null) {
                model.remarks = row["remarks"].ToString();
            }
            if (row["is_lock"] != null && row["is_lock"].ToString() != "") {
                model.is_lock = int.Parse(row["is_lock"].ToString());
            }
            if (row["add_time"] != null && row["add_time"].ToString() != "") {
                model.add_time = DateTime.Parse(row["add_time"].ToString());
            }
            if (row["sort_id"] != null && row["sort_id"].ToString() != "") {
                model.sort_id = int.Parse(row["sort_id"].ToString());
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "ad_item ");
            if (strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0) {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + "ad_item ");
            if (strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM " + databaseprefix + "ad_item ");
            if (strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null) {
                return 0;
            } else {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim())) {
                strSql.Append("order by T." + orderby);
            } else {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from " + databaseprefix + "ad_item T ");
            if (!string.IsNullOrEmpty(strWhere.Trim())) {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
 