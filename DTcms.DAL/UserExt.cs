using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//用户拓展表
		public partial class UserExt
	{
				public bool Exists(int UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from UserExt");
			strSql.Append(" where ");
			                                       strSql.Append(" UserID = @UserID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = UserID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DTcms.Model.UserExt model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UserExt(");			
            strSql.Append("UserID,CnName,EnName,CartType,CartNum,CRAddress");
			strSql.Append(") values (");
            strSql.Append("@UserID,@CnName,@EnName,@CartType,@CartNum,@CRAddress");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@EnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CartType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CartNum", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@CRAddress", SqlDbType.VarChar,255)             
              
            };
			            
            parameters[0].Value = model.UserID;                        
            parameters[1].Value = model.CnName;                        
            parameters[2].Value = model.EnName;                        
            parameters[3].Value = model.CartType;                        
            parameters[4].Value = model.CartNum;                        
            parameters[5].Value = model.CRAddress;                        
			           return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
            			
		}
		
		
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int UserID, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserExt set " + strValue);
            strSql.Append(" where UserID=@UserID ");
            			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = UserID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
        }
		
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.UserExt model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UserExt set ");
			                        
            strSql.Append(" UserID = @UserID , ");                                    
            strSql.Append(" CnName = @CnName , ");                                    
            strSql.Append(" EnName = @EnName , ");                                    
            strSql.Append(" CartType = @CartType , ");                                    
            strSql.Append(" CartNum = @CartNum , ");                                    
            strSql.Append(" CRAddress = @CRAddress  ");            			
			strSql.Append(" where UserID=@UserID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@EnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CartType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CartNum", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@CRAddress", SqlDbType.VarChar,255)             
              
            };
						            
            parameters[0].Value = model.UserID;                        
            parameters[1].Value = model.CnName;                        
            parameters[2].Value = model.EnName;                        
            parameters[3].Value = model.CartType;                        
            parameters[4].Value = model.CartNum;                        
            parameters[5].Value = model.CRAddress;                        
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
		public bool Delete(int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UserExt ");
			strSql.Append(" where UserID=@UserID ");
						SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = UserID;


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
			strSql.Append("delete from UserExt ");
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
		public DTcms.Model.UserExt GetModel(int UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID, CnName, EnName, CartType, CartNum, CRAddress  ");			
			strSql.Append("  from UserExt ");
			strSql.Append(" where UserID=@UserID ");
						SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)			};
			parameters[0].Value = UserID;

			
			DTcms.Model.UserExt model=new DTcms.Model.UserExt();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
																																				model.CnName= ds.Tables[0].Rows[0]["CnName"].ToString();
																																model.EnName= ds.Tables[0].Rows[0]["EnName"].ToString();
																												if(ds.Tables[0].Rows[0]["CartType"].ToString()!="")
				{
					model.CartType=int.Parse(ds.Tables[0].Rows[0]["CartType"].ToString());
				}
																																				model.CartNum= ds.Tables[0].Rows[0]["CartNum"].ToString();
																																model.CRAddress= ds.Tables[0].Rows[0]["CRAddress"].ToString();
																										
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
			strSql.Append(" FROM UserExt ");
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
			strSql.Append(" FROM UserExt ");
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
            strSql.Append("select * FROM UserExt ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

