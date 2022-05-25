using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//BidSourceFile
		public partial class BidSourceFile
	{
				public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BidSourceFile");
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
		public int Add(DTcms.Model.BidSourceFile model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BidSourceFile(");			
            strSql.Append("BidID,Title,Path,NeedTranslation,Memo,TranslationPrice,AddTime");
			strSql.Append(") values (");
            strSql.Append("@BidID,@Title,@Path,@NeedTranslation,@Memo,@TranslationPrice,@AddTime");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@Path", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@NeedTranslation", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Memo", SqlDbType.NVarChar,2000) ,            
                        new SqlParameter("@TranslationPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.BidID;                        
            parameters[1].Value = model.Title;                        
            parameters[2].Value = model.Path;                        
            parameters[3].Value = model.NeedTranslation;                        
            parameters[4].Value = model.Memo;                        
            parameters[5].Value = model.TranslationPrice;                        
            parameters[6].Value = model.AddTime;                        
			   
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
            strSql.Append("update BidSourceFile set " + strValue);
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
		public bool Update(DTcms.Model.BidSourceFile model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BidSourceFile set ");
			                                                
            strSql.Append(" BidID = @BidID , ");                                    
            strSql.Append(" Title = @Title , ");                                    
            strSql.Append(" Path = @Path , ");                                    
            strSql.Append(" NeedTranslation = @NeedTranslation , ");                                    
            strSql.Append(" Memo = @Memo , ");                                    
            strSql.Append(" TranslationPrice = @TranslationPrice , ");                                    
            strSql.Append(" AddTime = @AddTime  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@BidID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Title", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@Path", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@NeedTranslation", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Memo", SqlDbType.NVarChar,2000) ,            
                        new SqlParameter("@TranslationPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.BidID;                        
            parameters[2].Value = model.Title;                        
            parameters[3].Value = model.Path;                        
            parameters[4].Value = model.NeedTranslation;                        
            parameters[5].Value = model.Memo;                        
            parameters[6].Value = model.TranslationPrice;                        
            parameters[7].Value = model.AddTime;                        
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
			strSql.Append("delete from BidSourceFile ");
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
			strSql.Append("delete from BidSourceFile ");
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
		public DTcms.Model.BidSourceFile GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, BidID, Title, Path, NeedTranslation, Memo, TranslationPrice, AddTime  ");			
			strSql.Append("  from BidSourceFile ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			DTcms.Model.BidSourceFile model=new DTcms.Model.BidSourceFile();
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
																																				model.Title= ds.Tables[0].Rows[0]["Title"].ToString();
																																model.Path= ds.Tables[0].Rows[0]["Path"].ToString();
																																												if(ds.Tables[0].Rows[0]["NeedTranslation"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["NeedTranslation"].ToString()=="1")||(ds.Tables[0].Rows[0]["NeedTranslation"].ToString().ToLower()=="true"))
					{
					model.NeedTranslation= true;
					}
					else
					{
					model.NeedTranslation= false;
					}
				}
																				model.Memo= ds.Tables[0].Rows[0]["Memo"].ToString();
																												if(ds.Tables[0].Rows[0]["TranslationPrice"].ToString()!="")
				{
					model.TranslationPrice=decimal.Parse(ds.Tables[0].Rows[0]["TranslationPrice"].ToString());
				}
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
			strSql.Append(" FROM BidSourceFile ");
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
			strSql.Append(" FROM BidSourceFile ");
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
            strSql.Append("select * FROM BidSourceFile ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

