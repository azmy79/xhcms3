using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//申办业务-证件类型
		public partial class BidBusiness_DocumentType
	{
				public bool Exists(int BidBusinessID,int DocumentTypeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BidBusiness_DocumentType");
			strSql.Append(" where ");
			                                       strSql.Append(" BidBusinessID = @BidBusinessID and  ");
                                                                   strSql.Append(" DocumentTypeID = @DocumentTypeID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4),
					new SqlParameter("@DocumentTypeID", SqlDbType.Int,4)			};
			parameters[0].Value = BidBusinessID;
			parameters[1].Value = DocumentTypeID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DTcms.Model.BidBusiness_DocumentType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BidBusiness_DocumentType(");			
            strSql.Append("BidBusinessID,DocumentTypeID");
			strSql.Append(") values (");
            strSql.Append("@BidBusinessID,@DocumentTypeID");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@BidBusinessID", SqlDbType.Int,4) ,            
                        new SqlParameter("@DocumentTypeID", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.BidBusinessID;                        
            parameters[1].Value = model.DocumentTypeID;                        
			           return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
            			
		}
		
		
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int BidBusinessID,int DocumentTypeID, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BidBusiness_DocumentType set " + strValue);
            strSql.Append(" where BidBusinessID=@BidBusinessID and DocumentTypeID=@DocumentTypeID ");
            			SqlParameter[] parameters = {
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4),
					new SqlParameter("@DocumentTypeID", SqlDbType.Int,4)			};
			parameters[0].Value = BidBusinessID;
			parameters[1].Value = DocumentTypeID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
        }
		
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.BidBusiness_DocumentType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BidBusiness_DocumentType set ");
			                        
            strSql.Append(" BidBusinessID = @BidBusinessID , ");                                    
            strSql.Append(" DocumentTypeID = @DocumentTypeID  ");            			
			strSql.Append(" where BidBusinessID=@BidBusinessID and DocumentTypeID=@DocumentTypeID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@BidBusinessID", SqlDbType.Int,4) ,            
                        new SqlParameter("@DocumentTypeID", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.BidBusinessID;                        
            parameters[1].Value = model.DocumentTypeID;                        
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
		public bool Delete(int BidBusinessID,int DocumentTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from BidBusiness_DocumentType ");
			strSql.Append(" where BidBusinessID=@BidBusinessID and DocumentTypeID=@DocumentTypeID ");
						SqlParameter[] parameters = {
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4),
					new SqlParameter("@DocumentTypeID", SqlDbType.Int,4)			};
			parameters[0].Value = BidBusinessID;
			parameters[1].Value = DocumentTypeID;


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
			strSql.Append("delete from BidBusiness_DocumentType ");
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
		public DTcms.Model.BidBusiness_DocumentType GetModel(int BidBusinessID,int DocumentTypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BidBusinessID, DocumentTypeID  ");			
			strSql.Append("  from BidBusiness_DocumentType ");
			strSql.Append(" where BidBusinessID=@BidBusinessID and DocumentTypeID=@DocumentTypeID ");
						SqlParameter[] parameters = {
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4),
					new SqlParameter("@DocumentTypeID", SqlDbType.Int,4)			};
			parameters[0].Value = BidBusinessID;
			parameters[1].Value = DocumentTypeID;

			
			DTcms.Model.BidBusiness_DocumentType model=new DTcms.Model.BidBusiness_DocumentType();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["BidBusinessID"].ToString()!="")
				{
					model.BidBusinessID=int.Parse(ds.Tables[0].Rows[0]["BidBusinessID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DocumentTypeID"].ToString()!="")
				{
					model.DocumentTypeID=int.Parse(ds.Tables[0].Rows[0]["DocumentTypeID"].ToString());
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
			strSql.Append(" FROM BidBusiness_DocumentType ");
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
			strSql.Append(" FROM BidBusiness_DocumentType ");
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
            strSql.Append("select * FROM BidBusiness_DocumentType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

