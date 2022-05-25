using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;
using DTcms.Common;
using DTcms.DBUtility;//请先添加引用

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类Advertising。
    /// </summary>
    public class ad
    {

        private string databaseprefix; //数据库表名前缀
        public ad(string _databaseprefix) {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "ad");
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
            strSql.Append(" from " + databaseprefix + "ad");
            if (strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.ad model) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "ad(");
            strSql.Append("title,adtype,remarks,num,price,width,height,target,sort_id)");
            strSql.Append(" values (");
            strSql.Append("@title,@adtype,@remarks,@num,@price,@width,@height,@target,@sort_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@title", SqlDbType.NVarChar, 100),
                    new SqlParameter("@adtype", SqlDbType.Int, 4),
                    new SqlParameter("@remarks", SqlDbType.NVarChar, -1),
                    new SqlParameter("@num", SqlDbType.Int, 4),
                    new SqlParameter("@price", SqlDbType.Int, 4),
                    new SqlParameter("@width", SqlDbType.Int, 4),
                    new SqlParameter("@height", SqlDbType.Int, 4),
                    new SqlParameter("@target", SqlDbType.NVarChar, 50),
                    new SqlParameter("@sort_id", SqlDbType.Int, 4)
                };
            parameters[0].Value = model.title;
            parameters[1].Value = model.adtype;
            parameters[2].Value = model.remarks;
            parameters[3].Value = model.num;
            parameters[4].Value = model.price;
            parameters[5].Value = model.width;
            parameters[6].Value = model.height;
            parameters[7].Value = model.target;
            parameters[8].Value = model.sort_id;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.ad model) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "ad set ");
            strSql.Append("title=@title,");
            strSql.Append("adtype=@adtype,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("num=@num,");
            strSql.Append("price=@price,");
            strSql.Append("width=@width,");
            strSql.Append("height=@height,");
            strSql.Append("target=@target,");
            strSql.Append("sort_id=@sort_id");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@title", SqlDbType.NVarChar, 100),
                    new SqlParameter("@adtype", SqlDbType.Int, 4),
                    new SqlParameter("@remarks", SqlDbType.NVarChar, -1),
                    new SqlParameter("@num", SqlDbType.Int, 4),
                    new SqlParameter("@price", SqlDbType.Int, 4),
                    new SqlParameter("@width", SqlDbType.Int, 4),
                    new SqlParameter("@height", SqlDbType.Int, 4),
                    new SqlParameter("@target", SqlDbType.NVarChar, 50),
                    new SqlParameter("@sort_id", SqlDbType.Int, 4),
                    new SqlParameter("@id", SqlDbType.Int, 4)
                };
            parameters[0].Value = model.title;
            parameters[1].Value = model.adtype;
            parameters[2].Value = model.remarks;
            parameters[3].Value = model.num;
            parameters[4].Value = model.price;
            parameters[5].Value = model.width;
            parameters[6].Value = model.height;
            parameters[7].Value = model.target;
            parameters[8].Value = model.sort_id;
            parameters[9].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "ad set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "ad ");
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
            strSql.Append("delete from " + databaseprefix + "ad ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0) {
                return true;
            } else {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ad GetModel(int id) {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,adtype,remarks,num,price,width,height,target,sort_id from " + databaseprefix + "ad ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            //Model.ad model = new Model.ad();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds.Tables[0].Rows.Count > 0 ? DataRowToModel(ds.Tables[0].Rows[0]) : null;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ad DataRowToModel(DataRow row) {
            if (row == null)
                return null;

            var model = new Model.ad();
            if (row["id"] != null && row["id"].ToString() != "") {
                model.id = int.Parse(row["id"].ToString());
            }
            if (row["title"] != null) {
                model.title = row["title"].ToString();
            }
            if (row["adtype"] != null && row["adtype"].ToString() != "") {
                model.adtype = int.Parse(row["adtype"].ToString());
            }
            if (row["remarks"] != null) {
                model.remarks = row["remarks"].ToString();
            }
            if (row["num"] != null && row["num"].ToString() != "") {
                model.num = int.Parse(row["num"].ToString());
            }
            if (row["price"] != null && row["price"].ToString() != "") {
                model.price = int.Parse(row["price"].ToString());
            }
            if (row["width"] != null && row["width"].ToString() != "") {
                model.width = int.Parse(row["width"].ToString());
            }
            if (row["height"] != null && row["height"].ToString() != "") {
                model.height = int.Parse(row["height"].ToString());
            }
            if (row["target"] != null) {
                model.target = row["target"].ToString();
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
            strSql.Append("select id,title,adtype,remarks,num,price,width,height,target,sort_id ");
            strSql.Append(" FROM " + databaseprefix + "ad ");
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
            strSql.Append(" id,title,adtype,remarks,num,price,width,height,target,sort_id ");
            strSql.Append(" FROM " + databaseprefix + "ad ");
            if (strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "ad");
            //if (channel_id > 0) {
            //    strSql.Append(" where channel_id=" + channel_id);
            //}
            //if (category_id > 0) {
            //    if (channel_id > 0) {
            //        strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            //    } else {
            //        strSql.Append(" where category_id in (select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            //    }
            //}
            //if (strWhere.Trim() != "") {
            //    if (channel_id > 0 || category_id > 0) {
            //        strSql.Append(" and " + strWhere);
            //    } else {
            //        strSql.Append(" where " + strWhere);
            //    }
            //}
            strSql.Append(" where " + strWhere);

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM " + databaseprefix + "ad ");
            if (strWhere.Trim() != "") {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            return obj == null ? 0 : Convert.ToInt32(obj);
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
            strSql.Append(")AS Row, T.*  from " + databaseprefix + "ad T ");
            if (!string.IsNullOrEmpty(strWhere.Trim())) {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}