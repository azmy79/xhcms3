using DTcms.Common;
using DTcms.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DTcms.DAL
{
    public class feedback
    {
        private string databaseprefix;

        public feedback(string _databaseprefix)
        {
            this.databaseprefix = _databaseprefix;
        }

        public int Add(Model.feedback model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into " + this.databaseprefix + "feedback(");
            builder.Append("title,content,user_name,user_tel,user_qq,user_email,add_time,is_lock,UserID,MsgType)");
            builder.Append(" values (");
            builder.Append("@title,@content,@user_name,@user_tel,@user_qq,@user_email,@add_time,@is_lock,@UserID,@MsgType)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar, 100), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@user_name", SqlDbType.NVarChar, 50), new SqlParameter("@user_tel", SqlDbType.NVarChar, 30), new SqlParameter("@user_qq", SqlDbType.NVarChar, 30), new SqlParameter("@user_email", SqlDbType.NVarChar, 100), new SqlParameter("@add_time", SqlDbType.DateTime), new SqlParameter("@is_lock", SqlDbType.TinyInt, 1), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@MsgType", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.title;
            cmdParms[1].Value = model.content;
            cmdParms[2].Value = model.user_name;
            cmdParms[3].Value = model.user_tel;
            cmdParms[4].Value = model.user_qq;
            cmdParms[5].Value = model.user_email;
            cmdParms[6].Value = model.add_time;
            cmdParms[7].Value = model.is_lock;
            cmdParms[8].Value = model.UserID;
            cmdParms[9].Value = model.MsgType;
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
            builder.Append("delete from " + this.databaseprefix + "feedback ");
            builder.Append(" where id=@id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from " + this.databaseprefix + "feedback");
            builder.Append(" where id=@id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(int Top, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" id,title,content,user_name,user_tel,user_qq,user_email,add_time,reply_content,reply_time,is_lock,UserID,MsgType ");
            builder.Append(" FROM " + this.databaseprefix + "feedback ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by add_time desc");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM " + this.databaseprefix + "feedback");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(builder.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, builder.ToString(), filedOrder));
        }

        public Model.feedback GetModel(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 id,title,content,user_name,user_tel,user_qq,user_email,add_time,reply_content,reply_time,is_lock,UserID,MsgType from " + this.databaseprefix + "feedback ");
            builder.Append(" where id=@id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            cmdParms[0].Value = id;
            Model.feedback feedback = new Model.feedback();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                if ((set.Tables[0].Rows[0]["id"] != null) && (set.Tables[0].Rows[0]["id"].ToString() != ""))
                {
                    feedback.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["title"] != null) && (set.Tables[0].Rows[0]["title"].ToString() != ""))
                {
                    feedback.title = set.Tables[0].Rows[0]["title"].ToString();
                }
                if ((set.Tables[0].Rows[0]["content"] != null) && (set.Tables[0].Rows[0]["content"].ToString() != ""))
                {
                    feedback.content = set.Tables[0].Rows[0]["content"].ToString();
                }
                if ((set.Tables[0].Rows[0]["user_name"] != null) && (set.Tables[0].Rows[0]["user_name"].ToString() != ""))
                {
                    feedback.user_name = set.Tables[0].Rows[0]["user_name"].ToString();
                }
                if ((set.Tables[0].Rows[0]["user_tel"] != null) && (set.Tables[0].Rows[0]["user_tel"].ToString() != ""))
                {
                    feedback.user_tel = set.Tables[0].Rows[0]["user_tel"].ToString();
                }
                if ((set.Tables[0].Rows[0]["user_qq"] != null) && (set.Tables[0].Rows[0]["user_qq"].ToString() != ""))
                {
                    feedback.user_qq = set.Tables[0].Rows[0]["user_qq"].ToString();
                }
                if ((set.Tables[0].Rows[0]["user_email"] != null) && (set.Tables[0].Rows[0]["user_email"].ToString() != ""))
                {
                    feedback.user_email = set.Tables[0].Rows[0]["user_email"].ToString();
                }
                if ((set.Tables[0].Rows[0]["add_time"] != null) && (set.Tables[0].Rows[0]["add_time"].ToString() != ""))
                {
                    feedback.add_time = DateTime.Parse(set.Tables[0].Rows[0]["add_time"].ToString());
                }
                if ((set.Tables[0].Rows[0]["reply_content"] != null) && (set.Tables[0].Rows[0]["reply_content"].ToString() != ""))
                {
                    feedback.reply_content = set.Tables[0].Rows[0]["reply_content"].ToString();
                }
                if ((set.Tables[0].Rows[0]["reply_time"] != null) && (set.Tables[0].Rows[0]["reply_time"].ToString() != ""))
                {
                    feedback.reply_time = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["reply_time"].ToString()));
                }
                if ((set.Tables[0].Rows[0]["is_lock"] != null) && (set.Tables[0].Rows[0]["is_lock"].ToString() != ""))
                {
                    feedback.is_lock = int.Parse(set.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
                {
                    feedback.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
                }
                if ((set.Tables[0].Rows[0]["MsgType"] != null) && (set.Tables[0].Rows[0]["MsgType"].ToString() != ""))
                {
                    feedback.MsgType = int.Parse(set.Tables[0].Rows[0]["MsgType"].ToString());
                }
                return feedback;
            }
            return null;
        }

        public bool Update(Model.feedback model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update " + this.databaseprefix + "feedback set ");
            builder.Append("title=@title,");
            builder.Append("content=@content,");
            builder.Append("user_name=@user_name,");
            builder.Append("user_tel=@user_tel,");
            builder.Append("user_qq=@user_qq,");
            builder.Append("user_email=@user_email,");
            builder.Append("add_time=@add_time,");
            builder.Append("reply_content=@reply_content,");
            builder.Append("reply_time=@reply_time,");
            builder.Append("is_lock=@is_lock,");
            builder.Append("UserID=@UserID,");
            builder.Append("MsgType=@MsgType");
            builder.Append(" where id=@id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar, 100), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@user_name", SqlDbType.NVarChar, 50), new SqlParameter("@user_tel", SqlDbType.NVarChar, 30), new SqlParameter("@user_qq", SqlDbType.NVarChar, 30), new SqlParameter("@user_email", SqlDbType.NVarChar, 100), new SqlParameter("@add_time", SqlDbType.DateTime), new SqlParameter("@reply_content", SqlDbType.NText), new SqlParameter("@reply_time", SqlDbType.DateTime), new SqlParameter("@is_lock", SqlDbType.TinyInt, 1), new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@MsgType", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.title;
            cmdParms[1].Value = model.content;
            cmdParms[2].Value = model.user_name;
            cmdParms[3].Value = model.user_tel;
            cmdParms[4].Value = model.user_qq;
            cmdParms[5].Value = model.user_email;
            cmdParms[6].Value = model.add_time;
            cmdParms[7].Value = model.reply_content;
            cmdParms[8].Value = model.reply_time;
            cmdParms[9].Value = model.is_lock;
            cmdParms[10].Value = model.id;
            cmdParms[11].Value = model.UserID;
            cmdParms[12].Value = model.MsgType;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public void UpdateField(int id, string strValue)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update " + this.databaseprefix + "feedback set " + strValue);
            builder.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}

