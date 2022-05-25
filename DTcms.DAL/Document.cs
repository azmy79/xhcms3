using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//证件
		public partial class Document
	{
				public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Document");
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
		public int Add(DTcms.Model.Document model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Document(");			
            strSql.Append("BidID,DocumentTypeID,Path,AddTime");
			strSql.Append(") values (");
            strSql.Append("@BidID,@DocumentTypeID,@Path,@AddTime");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@DocumentTypeID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Path", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.BidID;                        
            parameters[1].Value = model.DocumentTypeID;                        
            parameters[2].Value = model.Path;                        
            parameters[3].Value = model.AddTime;                        
			   
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
            strSql.Append("update Document set " + strValue);
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
		public bool Update(DTcms.Model.Document model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Document set ");
			                                                
            strSql.Append(" BidID = @BidID , ");                                    
            strSql.Append(" DocumentTypeID = @DocumentTypeID , ");                                    
            strSql.Append(" Path = @Path , ");                                    
            strSql.Append(" AddTime = @AddTime  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@DocumentTypeID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Path", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.BidID;                        
            parameters[2].Value = model.DocumentTypeID;                        
            parameters[3].Value = model.Path;                        
            parameters[4].Value = model.AddTime;                        
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
			strSql.Append("delete from Document ");
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
			strSql.Append("delete from Document ");
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
		public DTcms.Model.Document GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, BidID, DocumentTypeID, Path, AddTime  ");			
			strSql.Append("  from Document ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			DTcms.Model.Document model=new DTcms.Model.Document();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["BidID"].ToString()!="")
				{
					model.BidID=int.Parse(ds.Tables[0].Rows[0]["BidID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DocumentTypeID"].ToString()!="")
				{
					model.DocumentTypeID=int.Parse(ds.Tables[0].Rows[0]["DocumentTypeID"].ToString());
				}
																																				model.Path= ds.Tables[0].Rows[0]["Path"].ToString();
																												if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
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
			strSql.Append(" FROM Document ");
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
			strSql.Append(" FROM Document ");
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
            strSql.Append("select * FROM Document ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

