using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//排班表
		public partial class Scheduling
	{
				public bool Exists(int Day,int MonthType,int ManagerID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Scheduling");
			strSql.Append(" where ");
			                                       strSql.Append(" Day = @Day and  ");
                                                                   strSql.Append(" MonthType = @MonthType and  ");
                                                                   strSql.Append(" ManagerID = @ManagerID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@Day", SqlDbType.Int,4),
					new SqlParameter("@MonthType", SqlDbType.Int,4),
					new SqlParameter("@ManagerID", SqlDbType.Int,4)			};
			parameters[0].Value = Day;
			parameters[1].Value = MonthType;
			parameters[2].Value = ManagerID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DTcms.Model.Scheduling model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheduling(");			
            strSql.Append("Day,MonthType,ManagerID");
			strSql.Append(") values (");
            strSql.Append("@Day,@MonthType,@ManagerID");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@Day", SqlDbType.Int,4) ,            
                        new SqlParameter("@MonthType", SqlDbType.Int,4) ,            
                        new SqlParameter("@ManagerID", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.Day;                        
            parameters[1].Value = model.MonthType;                        
            parameters[2].Value = model.ManagerID;                        
			           return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
            			
		}
		
		
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int Day,int MonthType,int ManagerID, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Scheduling set " + strValue);
            strSql.Append(" where Day=@Day and MonthType=@MonthType and ManagerID=@ManagerID ");
            			SqlParameter[] parameters = {
					new SqlParameter("@Day", SqlDbType.Int,4),
					new SqlParameter("@MonthType", SqlDbType.Int,4),
					new SqlParameter("@ManagerID", SqlDbType.Int,4)			};
			parameters[0].Value = Day;
			parameters[1].Value = MonthType;
			parameters[2].Value = ManagerID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters)>0;
        }
		
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Scheduling model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheduling set ");
			                        
            strSql.Append(" Day = @Day , ");                                    
            strSql.Append(" MonthType = @MonthType , ");                                    
            strSql.Append(" ManagerID = @ManagerID  ");            			
			strSql.Append(" where Day=@Day and MonthType=@MonthType and ManagerID=@ManagerID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@Day", SqlDbType.Int,4) ,            
                        new SqlParameter("@MonthType", SqlDbType.Int,4) ,            
                        new SqlParameter("@ManagerID", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.Day;                        
            parameters[1].Value = model.MonthType;                        
            parameters[2].Value = model.ManagerID;                        
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
		public bool Delete(int Day,int MonthType,int ManagerID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Scheduling ");
			strSql.Append(" where Day=@Day and MonthType=@MonthType and ManagerID=@ManagerID ");
						SqlParameter[] parameters = {
					new SqlParameter("@Day", SqlDbType.Int,4),
					new SqlParameter("@MonthType", SqlDbType.Int,4),
					new SqlParameter("@ManagerID", SqlDbType.Int,4)			};
			parameters[0].Value = Day;
			parameters[1].Value = MonthType;
			parameters[2].Value = ManagerID;


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
			strSql.Append("delete from Scheduling ");
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
		public DTcms.Model.Scheduling GetModel(int Day,int MonthType,int ManagerID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Day, MonthType, ManagerID  ");			
			strSql.Append("  from Scheduling ");
			strSql.Append(" where Day=@Day and MonthType=@MonthType and ManagerID=@ManagerID ");
						SqlParameter[] parameters = {
					new SqlParameter("@Day", SqlDbType.Int,4),
					new SqlParameter("@MonthType", SqlDbType.Int,4),
					new SqlParameter("@ManagerID", SqlDbType.Int,4)			};
			parameters[0].Value = Day;
			parameters[1].Value = MonthType;
			parameters[2].Value = ManagerID;

			
			DTcms.Model.Scheduling model=new DTcms.Model.Scheduling();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Day"].ToString()!="")
				{
					model.Day=int.Parse(ds.Tables[0].Rows[0]["Day"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["MonthType"].ToString()!="")
				{
					model.MonthType=int.Parse(ds.Tables[0].Rows[0]["MonthType"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["ManagerID"].ToString()!="")
				{
					model.ManagerID=int.Parse(ds.Tables[0].Rows[0]["ManagerID"].ToString());
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
			strSql.Append(" FROM Scheduling ");
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
			strSql.Append(" FROM Scheduling ");
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
            strSql.Append("select * FROM Scheduling ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

