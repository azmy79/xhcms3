using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//预约
		public partial class Appointment
	{
				public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Appointment");
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
		public int Add(DTcms.Model.Appointment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Appointment(");			
            strSql.Append("Number,UserID,ManagerID,Date,Name,Contact,Content,AddTime");
			strSql.Append(") values (");
            strSql.Append("@Number,@UserID,@ManagerID,@Date,@Name,@Contact,@Content,@AddTime");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@Number", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                        new SqlParameter("@ManagerID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Date", SqlDbType.DateTime) ,            
                        new SqlParameter("@Name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Contact", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Content", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.Number;                        
            parameters[1].Value = model.UserID;                        
            parameters[2].Value = model.ManagerID;                        
            parameters[3].Value = model.Date;                        
            parameters[4].Value = model.Name;                        
            parameters[5].Value = model.Contact;                        
            parameters[6].Value = model.Content;                        
            parameters[7].Value = model.AddTime;                        
			   
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
            strSql.Append("update Appointment set " + strValue);
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
		public bool Update(DTcms.Model.Appointment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Appointment set ");
			                                                
            strSql.Append(" Number = @Number , ");                                    
            strSql.Append(" UserID = @UserID , ");                                    
            strSql.Append(" ManagerID = @ManagerID , ");                                    
            strSql.Append(" Date = @Date , ");                                    
            strSql.Append(" Name = @Name , ");                                    
            strSql.Append(" Contact = @Contact , ");                                    
            strSql.Append(" Content = @Content , ");                                    
            strSql.Append(" AddTime = @AddTime  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Number", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                        new SqlParameter("@ManagerID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Date", SqlDbType.DateTime) ,            
                        new SqlParameter("@Name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Contact", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Content", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.Number;                        
            parameters[2].Value = model.UserID;                        
            parameters[3].Value = model.ManagerID;                        
            parameters[4].Value = model.Date;                        
            parameters[5].Value = model.Name;                        
            parameters[6].Value = model.Contact;                        
            parameters[7].Value = model.Content;                        
            parameters[8].Value = model.AddTime;                        
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
			strSql.Append("delete from Appointment ");
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
			strSql.Append("delete from Appointment ");
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
		public DTcms.Model.Appointment GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, Number, UserID, ManagerID, Date, Name, Contact, Content, AddTime  ");			
			strSql.Append("  from Appointment ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			DTcms.Model.Appointment model=new DTcms.Model.Appointment();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.Number= ds.Tables[0].Rows[0]["Number"].ToString();
																												if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["ManagerID"].ToString()!="")
				{
					model.ManagerID=int.Parse(ds.Tables[0].Rows[0]["ManagerID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Date"].ToString()!="")
				{
					model.Date=DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
				}
																																				model.Name= ds.Tables[0].Rows[0]["Name"].ToString();
																																model.Contact= ds.Tables[0].Rows[0]["Contact"].ToString();
																																model.Content= ds.Tables[0].Rows[0]["Content"].ToString();
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
			strSql.Append(" FROM Appointment ");
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
			strSql.Append(" FROM Appointment ");
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
            strSql.Append("select * FROM Appointment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

