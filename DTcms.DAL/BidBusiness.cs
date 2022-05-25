using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//申办业务
		public partial class BidBusiness
	{
				public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BidBusiness");
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
		public int Add(DTcms.Model.BidBusiness model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BidBusiness(");			
            strSql.Append("Name,Sort,IsTop,NotaryPrice,CopyPrice");
			strSql.Append(") values (");
            strSql.Append("@Name,@Sort,@IsTop,@NotaryPrice,@CopyPrice");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@Name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Sort", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsTop", SqlDbType.Bit,1) ,            
                        new SqlParameter("@NotaryPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CopyPrice", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.Name;                        
            parameters[1].Value = model.Sort;                        
            parameters[2].Value = model.IsTop;                        
            parameters[3].Value = model.NotaryPrice;                        
            parameters[4].Value = model.CopyPrice;                        
			   
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
            strSql.Append("update BidBusiness set " + strValue);
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
		public bool Update(DTcms.Model.BidBusiness model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update BidBusiness set ");
			                                                
            strSql.Append(" Name = @Name , ");                                    
            strSql.Append(" Sort = @Sort , ");                                    
            strSql.Append(" IsTop = @IsTop , ");                                    
            strSql.Append(" NotaryPrice = @NotaryPrice , ");                                    
            strSql.Append(" CopyPrice = @CopyPrice  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Sort", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsTop", SqlDbType.Bit,1) ,            
                        new SqlParameter("@NotaryPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CopyPrice", SqlDbType.Decimal,9)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.Name;                        
            parameters[2].Value = model.Sort;                        
            parameters[3].Value = model.IsTop;                        
            parameters[4].Value = model.NotaryPrice;                        
            parameters[5].Value = model.CopyPrice;                        
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
			strSql.Append("delete from BidBusiness ");
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
			strSql.Append("delete from BidBusiness ");
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
		public DTcms.Model.BidBusiness GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, Name, Sort, IsTop, NotaryPrice, CopyPrice  ");			
			strSql.Append("  from BidBusiness ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			DTcms.Model.BidBusiness model=new DTcms.Model.BidBusiness();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.Name= ds.Tables[0].Rows[0]["Name"].ToString();
																												if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
																																																if(ds.Tables[0].Rows[0]["IsTop"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsTop"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsTop"].ToString().ToLower()=="true"))
					{
					model.IsTop= true;
					}
					else
					{
					model.IsTop= false;
					}
				}
																if(ds.Tables[0].Rows[0]["NotaryPrice"].ToString()!="")
				{
					model.NotaryPrice=decimal.Parse(ds.Tables[0].Rows[0]["NotaryPrice"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CopyPrice"].ToString()!="")
				{
					model.CopyPrice=decimal.Parse(ds.Tables[0].Rows[0]["CopyPrice"].ToString());
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
			strSql.Append(" FROM BidBusiness ");
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
			strSql.Append(" FROM BidBusiness ");
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
            strSql.Append("select * FROM BidBusiness ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

