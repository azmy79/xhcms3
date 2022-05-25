using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//CertificateStyle
		public partial class CertificateStyle
	{
				public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CertificateStyle");
			strSql.Append(" where ");
			                                       strSql.Append(" ID = @ID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.CertificateStyle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CertificateStyle(");			
            strSql.Append("BidBusinessID,Title,ImgUrl,Memo,Sort");
			strSql.Append(") values (");
            strSql.Append("@BidBusinessID,@Title,@ImgUrl,@Memo,@Sort");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@BidBusinessID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@ImgUrl", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Sort", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.BidBusinessID;                        
            parameters[1].Value = model.Title;                        
            parameters[2].Value = model.ImgUrl;                        
            parameters[3].Value = model.Memo;                        
            parameters[4].Value = model.Sort;                        
			   
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                    
            	return Convert.ToInt32(obj);
                                                                  
			}			   
            			
		}
		
		
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int ID, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CertificateStyle set " + strValue);
            strSql.Append(" where ID=@ID");
            			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
        }
		
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.CertificateStyle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CertificateStyle set ");
			                                                
            strSql.Append(" BidBusinessID = @BidBusinessID , ");                                    
            strSql.Append(" Title = @Title , ");                                    
            strSql.Append(" ImgUrl = @ImgUrl , ");                                    
            strSql.Append(" Memo = @Memo , ");                                    
            strSql.Append(" Sort = @Sort  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BidBusinessID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@ImgUrl", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Memo", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@Sort", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.BidBusinessID;                        
            parameters[2].Value = model.Title;                        
            parameters[3].Value = model.ImgUrl;                        
            parameters[4].Value = model.Memo;                        
            parameters[5].Value = model.Sort;                        
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CertificateStyle ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;


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
			strSql.Append("delete from CertificateStyle ");
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
		public DTcms.Model.CertificateStyle GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, BidBusinessID, Title, ImgUrl, Memo, Sort  ");			
			strSql.Append("  from CertificateStyle ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			DTcms.Model.CertificateStyle model=new DTcms.Model.CertificateStyle();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["BidBusinessID"].ToString()!="")
				{
					model.BidBusinessID=int.Parse(ds.Tables[0].Rows[0]["BidBusinessID"].ToString());
				}
																																				model.Title= ds.Tables[0].Rows[0]["Title"].ToString();
																																model.ImgUrl= ds.Tables[0].Rows[0]["ImgUrl"].ToString();
																																model.Memo= ds.Tables[0].Rows[0]["Memo"].ToString();
																												if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
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
			strSql.Append(" FROM CertificateStyle ");
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
			strSql.Append(" FROM CertificateStyle ");
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
            strSql.Append("select * FROM CertificateStyle ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

