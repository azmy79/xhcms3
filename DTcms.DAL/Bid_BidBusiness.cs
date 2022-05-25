using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//申办-申办业务
		public partial class Bid_BidBusiness
	{
				public bool Exists(int BidID,int BidBusinessID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Bid_BidBusiness");
			strSql.Append(" where ");
			                                       strSql.Append(" BidID = @BidID and  ");
                                                                   strSql.Append(" BidBusinessID = @BidBusinessID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = BidBusinessID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DTcms.Model.Bid_BidBusiness model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Bid_BidBusiness(");			
            strSql.Append("BidID,BidBusinessID,CertificateStyleID");
			strSql.Append(") values (");
            strSql.Append("@BidID,@BidBusinessID,@CertificateStyleID");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BidBusinessID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CertificateStyleID", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.BidID;                        
            parameters[1].Value = model.BidBusinessID;                        
            parameters[2].Value = model.CertificateStyleID;                        
			           return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
            			
		}
		
		
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int BidID,int BidBusinessID, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Bid_BidBusiness set " + strValue);
            strSql.Append(" where BidID=@BidID and BidBusinessID=@BidBusinessID ");
            			SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = BidBusinessID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
        }
		
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Bid_BidBusiness model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Bid_BidBusiness set ");
			                        
            strSql.Append(" BidID = @BidID , ");                                    
            strSql.Append(" BidBusinessID = @BidBusinessID , ");                                    
            strSql.Append(" CertificateStyleID = @CertificateStyleID  ");            			
			strSql.Append(" where BidID=@BidID and BidBusinessID=@BidBusinessID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BidBusinessID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CertificateStyleID", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.BidID;                        
            parameters[1].Value = model.BidBusinessID;                        
            parameters[2].Value = model.CertificateStyleID;                        
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
		public bool Delete(int BidID,int BidBusinessID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Bid_BidBusiness ");
			strSql.Append(" where BidID=@BidID and BidBusinessID=@BidBusinessID ");
						SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = BidBusinessID;


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
			strSql.Append("delete from Bid_BidBusiness ");
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
		public DTcms.Model.Bid_BidBusiness GetModel(int BidID,int BidBusinessID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BidID, BidBusinessID, CertificateStyleID  ");			
			strSql.Append("  from Bid_BidBusiness ");
			strSql.Append(" where BidID=@BidID and BidBusinessID=@BidBusinessID ");
						SqlParameter[] parameters = {
					new SqlParameter("@BidID", SqlDbType.Int,4),
					new SqlParameter("@BidBusinessID", SqlDbType.Int,4)			};
			parameters[0].Value = BidID;
			parameters[1].Value = BidBusinessID;

			
			DTcms.Model.Bid_BidBusiness model=new DTcms.Model.Bid_BidBusiness();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["BidID"].ToString()!="")
				{
					model.BidID=int.Parse(ds.Tables[0].Rows[0]["BidID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["BidBusinessID"].ToString()!="")
				{
					model.BidBusinessID=int.Parse(ds.Tables[0].Rows[0]["BidBusinessID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CertificateStyleID"].ToString()!="")
				{
					model.CertificateStyleID=int.Parse(ds.Tables[0].Rows[0]["CertificateStyleID"].ToString());
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
			strSql.Append(" FROM Bid_BidBusiness ");
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
			strSql.Append(" FROM Bid_BidBusiness ");
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
            strSql.Append("select * FROM Bid_BidBusiness ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

