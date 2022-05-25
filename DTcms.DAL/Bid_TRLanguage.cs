using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//申办-翻译语言
		public partial class Bid_TRLanguage
	{
				public bool Exists(int BidID,int TRLanguageID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Bid_TRLanguage");
			strSql.Append(" where ");
			                                       strSql.Append(" BidID = @BidID and  ");
                                                                   strSql.Append(" TRLanguageID = @TRLanguageID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@TRLanguageID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = TRLanguageID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DTcms.Model.Bid_TRLanguage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Bid_TRLanguage(");			
            strSql.Append("BidID,TRLanguageID");
			strSql.Append(") values (");
            strSql.Append("@BidID,@TRLanguageID");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@TRLanguageID", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.BidID;                        
            parameters[1].Value = model.TRLanguageID;                        
			           return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
            			
		}
		
		
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int BidID,int TRLanguageID, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Bid_TRLanguage set " + strValue);
            strSql.Append(" where BidID=@BidID and TRLanguageID=@TRLanguageID ");
            			SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@TRLanguageID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = TRLanguageID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
        }
		
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Bid_TRLanguage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Bid_TRLanguage set ");
			                        
            strSql.Append(" BidID = @BidID , ");                                    
            strSql.Append(" TRLanguageID = @TRLanguageID  ");            			
			strSql.Append(" where BidID=@BidID and TRLanguageID=@TRLanguageID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@TRLanguageID", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.BidID;                        
            parameters[1].Value = model.TRLanguageID;                        
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int BidID,int TRLanguageID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Bid_TRLanguage ");
			strSql.Append(" where BidID=@BidID and TRLanguageID=@TRLanguageID ");
						SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@TRLanguageID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = TRLanguageID;


			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string pkIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Bid_TRLanguage ");
			strSql.Append(" where ID in ("+pkIdlist+ ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.Bid_TRLanguage GetModel(int BidID,int TRLanguageID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BidID, TRLanguageID  ");			
			strSql.Append("  from Bid_TRLanguage ");
			strSql.Append(" where BidID=@BidID and TRLanguageID=@TRLanguageID ");
						SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@TRLanguageID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = TRLanguageID;

			
			DTcms.Model.Bid_TRLanguage model=new DTcms.Model.Bid_TRLanguage();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["BidID"].ToString()!="")
				{
					model.BidID=int.Parse(ds.Tables[0].Rows[0]["BidID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["TRLanguageID"].ToString()!="")
				{
					model.TRLanguageID=int.Parse(ds.Tables[0].Rows[0]["TRLanguageID"].ToString());
				}
																														
				return model;
			}
			else
			{
				return null;
			}
		}
		
				
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM Bid_TRLanguage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM Bid_TRLanguage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM Bid_TRLanguage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

