using DTcms.Common;
using DTcms.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DTcms.DAL
{
    public class link
    {
        private string databaseprefix;

        public link(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        public int Add(Model.link model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into " + databaseprefix + "link(");
            builder.Append("title,user_name,user_tel,email,site_url,img_url,is_image,sort_id,is_red,is_lock,add_time)");
            builder.Append(" values (");
            builder.Append("@title,@user_name,@user_tel,@email,@site_url,@img_url,@is_image,@sort_id,@is_red,@is_lock,@add_time)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar, 0xff), new SqlParameter("@user_name", SqlDbType.NVarChar, 50), new SqlParameter("@user_tel", SqlDbType.NVarChar, 20), new SqlParameter("@email", SqlDbType.NVarChar, 50), new SqlParameter("@site_url", SqlDbType.NVarChar, 0xff), new SqlParameter("@img_url", SqlDbType.NVarChar, 0xff), new SqlParameter("@is_image", SqlDbType.Int, 4), new SqlParameter("@sort_id", SqlDbType.Int, 4), new SqlParameter("@is_red", SqlDbType.TinyInt, 1), new SqlParameter("@is_lock", SqlDbType.TinyInt, 1), new SqlParameter("@add_time", SqlDbType.DateTime) };
            cmdParms[0].Value = model.title;
            cmdParms[1].Value = model.user_name;
            cmdParms[2].Value = model.user_tel;
            cmdParms[3].Value = model.email;
            cmdParms[4].Value = model.site_url;
            cmdParms[5].Value = model.img_url;
            cmdParms[6].Value = model.is_image;
            cmdParms[7].Value = model.sort_id;
            cmdParms[8].Value = model.is_red;
            cmdParms[9].Value = model.is_lock;
            cmdParms[10].Value = model.add_time;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from " + databaseprefix + "link ");
            builder.Append(" where id=@id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from " + databaseprefix + "link");
            builder.Append(" where id=@id ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,title,user_name,user_tel,email,site_url,img_url,is_image,sort_id,is_red,is_lock,add_time ");
            builder.Append(" FROM " + databaseprefix + "link ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" id,title,user_name,user_tel,email,site_url,img_url,is_image,sort_id,is_red,is_lock,add_time ");
            builder.Append(" FROM " + databaseprefix + "link ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by sort_id asc,add_time desc");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM " + databaseprefix + "link");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(builder.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, builder.ToString(), filedOrder));
        }

        public Model.link GetModel(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 id,title,user_name,user_tel,email,site_url,img_url,is_image,sort_id,is_red,is_lock,add_time from " + databaseprefix + "link ");
            builder.Append(" where id=@id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            Model.link link = new Model.link();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                if ((set.Tables[0].Rows[0]["id"] != null) && (set.Tables[0].Rows[0]["id"].ToString() != ""))
                {
                    link.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["title"] != null) && (set.Tables[0].Rows[0]["title"].ToString() != ""))
                {
                    link.title = set.Tables[0].Rows[0]["title"].ToString();
                }
                if ((set.Tables[0].Rows[0]["user_name"] != null) && (set.Tables[0].Rows[0]["user_name"].ToString() != ""))
                {
                    link.user_name = set.Tables[0].Rows[0]["user_name"].ToString();
                }
                if ((set.Tables[0].Rows[0]["user_tel"] != null) && (set.Tables[0].Rows[0]["user_tel"].ToString() != ""))
                {
                    link.user_tel = set.Tables[0].Rows[0]["user_tel"].ToString();
                }
                if ((set.Tables[0].Rows[0]["email"] != null) && (set.Tables[0].Rows[0]["email"].ToString() != ""))
                {
                    link.email = set.Tables[0].Rows[0]["email"].ToString();
                }
                if ((set.Tables[0].Rows[0]["site_url"] != null) && (set.Tables[0].Rows[0]["site_url"].ToString() != ""))
                {
                    link.site_url = set.Tables[0].Rows[0]["site_url"].ToString();
                }
                if ((set.Tables[0].Rows[0]["img_url"] != null) && (set.Tables[0].Rows[0]["img_url"].ToString() != ""))
                {
                    link.img_url = set.Tables[0].Rows[0]["img_url"].ToString();
                }
                if ((set.Tables[0].Rows[0]["is_image"] != null) && (set.Tables[0].Rows[0]["is_image"].ToString() != ""))
                {
                    link.is_image = int.Parse(set.Tables[0].Rows[0]["is_image"].ToString());
                }
                if ((set.Tables[0].Rows[0]["sort_id"] != null) && (set.Tables[0].Rows[0]["sort_id"].ToString() != ""))
                {
                    link.sort_id = int.Parse(set.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["is_red"] != null) && (set.Tables[0].Rows[0]["is_red"].ToString() != ""))
                {
                    link.is_red = int.Parse(set.Tables[0].Rows[0]["is_red"].ToString());
                }
                if ((set.Tables[0].Rows[0]["is_lock"] != null) && (set.Tables[0].Rows[0]["is_lock"].ToString() != ""))
                {
                    link.is_lock = int.Parse(set.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if ((set.Tables[0].Rows[0]["add_time"] != null) && (set.Tables[0].Rows[0]["add_time"].ToString() != ""))
                {
                    link.add_time = DateTime.Parse(set.Tables[0].Rows[0]["add_time"].ToString());
                }
                return link;
            }
            return null;
        }

        public bool Update(Model.link model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update " + databaseprefix + "link set ");
            builder.Append("title=@title,");
            builder.Append("user_name=@user_name,");
            builder.Append("user_tel=@user_tel,");
            builder.Append("email=@email,");
            builder.Append("site_url=@site_url,");
            builder.Append("img_url=@img_url,");
            builder.Append("is_image=@is_image,");
            builder.Append("sort_id=@sort_id,");
            builder.Append("is_red=@is_red,");
            builder.Append("is_lock=@is_lock,");
            builder.Append("add_time=@add_time");
            builder.Append(" where id=@id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar, 0xff), new SqlParameter("@user_name", SqlDbType.NVarChar, 50), new SqlParameter("@user_tel", SqlDbType.NVarChar, 20), new SqlParameter("@email", SqlDbType.NVarChar, 50), new SqlParameter("@site_url", SqlDbType.NVarChar, 0xff), new SqlParameter("@img_url", SqlDbType.NVarChar, 0xff), new SqlParameter("@is_image", SqlDbType.Int, 4), new SqlParameter("@sort_id", SqlDbType.Int, 4), new SqlParameter("@is_red", SqlDbType.TinyInt, 1), new SqlParameter("@is_lock", SqlDbType.TinyInt, 1), new SqlParameter("@add_time", SqlDbType.DateTime), new SqlParameter("@id", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.title;
            cmdParms[1].Value = model.user_name;
            cmdParms[2].Value = model.user_tel;
            cmdParms[3].Value = model.email;
            cmdParms[4].Value = model.site_url;
            cmdParms[5].Value = model.img_url;
            cmdParms[6].Value = model.is_image;
            cmdParms[7].Value = model.sort_id;
            cmdParms[8].Value = model.is_red;
            cmdParms[9].Value = model.is_lock;
            cmdParms[10].Value = model.add_time;
            cmdParms[11].Value = model.id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public void UpdateField(int id, string strValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update " + databaseprefix + "link set " + strValue);
            builder.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}

