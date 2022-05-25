using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//申办
		public partial class Bid
	{
				public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Bid");
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
		public int Add(DTcms.Model.Bid model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Bid(");			
            strSql.Append("Number,UserID,PurposeID,CountryID,CnName,EnName,Sex,Tel,Address,AddTime,Price,CopyCount,Status,Birthday,CartType,CartNum");
			strSql.Append(") values (");
            strSql.Append("@Number,@UserID,@PurposeID,@CountryID,@CnName,@EnName,@Sex,@Tel,@Address,@AddTime,@Price,@CopyCount,@Status,@Birthday,@CartType,@CartNum");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@Number", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                        new SqlParameter("@PurposeID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CountryID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@EnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Sex", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Tel", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Address", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CopyCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4) ,            
                        new SqlParameter("@Birthday", SqlDbType.DateTime) ,            
                        new SqlParameter("@CartType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CartNum", SqlDbType.VarChar,255)             
              
            };
			            
            parameters[0].Value = model.Number;                        
            parameters[1].Value = model.UserID;                        
            parameters[2].Value = model.PurposeID;                        
            parameters[3].Value = model.CountryID;                        
            parameters[4].Value = model.CnName;                        
            parameters[5].Value = model.EnName;                        
            parameters[6].Value = model.Sex;                        
            parameters[7].Value = model.Tel;                        
            parameters[8].Value = model.Address;                        
            parameters[9].Value = model.AddTime;                        
            parameters[10].Value = model.Price;                        
            parameters[11].Value = model.CopyCount;                        
            parameters[12].Value = model.Status;                        
            parameters[13].Value = model.Birthday;                        
            parameters[14].Value = model.CartType;                        
            parameters[15].Value = model.CartNum;                        
			   
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
            strSql.Append("update Bid set " + strValue);
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
		public bool Update(DTcms.Model.Bid model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Bid set ");
			                                                
            strSql.Append(" Number = @Number , ");                                    
            strSql.Append(" UserID = @UserID , ");                                    
            strSql.Append(" PurposeID = @PurposeID , ");                                    
            strSql.Append(" CountryID = @CountryID , ");                                    
            strSql.Append(" CnName = @CnName , ");                                    
            strSql.Append(" EnName = @EnName , ");                                    
            strSql.Append(" Sex = @Sex , ");                                    
            strSql.Append(" Tel = @Tel , ");                                    
            strSql.Append(" Address = @Address , ");                                    
            strSql.Append(" AddTime = @AddTime , ");                                    
            strSql.Append(" Price = @Price , ");                                    
            strSql.Append(" CopyCount = @CopyCount , ");                                    
            strSql.Append(" Status = @Status , ");                                    
            strSql.Append(" Birthday = @Birthday , ");                                    
            strSql.Append(" CartType = @CartType , ");                                    
            strSql.Append(" CartNum = @CartNum  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@Number", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UserID", SqlDbType.Int,4) ,            
                        new SqlParameter("@PurposeID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CountryID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@EnName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Sex", SqlDbType.Bit,1) ,            
                        new SqlParameter("@Tel", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Address", SqlDbType.VarChar,255) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@Price", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CopyCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@Status", SqlDbType.Int,4) ,            
                        new SqlParameter("@Birthday", SqlDbType.DateTime) ,            
                        new SqlParameter("@CartType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CartNum", SqlDbType.VarChar,255)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.Number;                        
            parameters[2].Value = model.UserID;                        
            parameters[3].Value = model.PurposeID;                        
            parameters[4].Value = model.CountryID;                        
            parameters[5].Value = model.CnName;                        
            parameters[6].Value = model.EnName;                        
            parameters[7].Value = model.Sex;                        
            parameters[8].Value = model.Tel;                        
            parameters[9].Value = model.Address;                        
            parameters[10].Value = model.AddTime;                        
            parameters[11].Value = model.Price;                        
            parameters[12].Value = model.CopyCount;                        
            parameters[13].Value = model.Status;                        
            parameters[14].Value = model.Birthday;                        
            parameters[15].Value = model.CartType;                        
            parameters[16].Value = model.CartNum;                        
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
			strSql.Append("delete from Bid ");
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
			strSql.Append("delete from Bid ");
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
		public DTcms.Model.Bid GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, Number, UserID, PurposeID, CountryID, CnName, EnName, Sex, Tel, Address, AddTime, Price, CopyCount, Status, Birthday, CartType, CartNum  ");			
			strSql.Append("  from Bid ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			DTcms.Model.Bid model=new DTcms.Model.Bid();
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
																																if(ds.Tables[0].Rows[0]["PurposeID"].ToString()!="")
				{
					model.PurposeID=int.Parse(ds.Tables[0].Rows[0]["PurposeID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CountryID"].ToString()!="")
				{
					model.CountryID=int.Parse(ds.Tables[0].Rows[0]["CountryID"].ToString());
				}
																																				model.CnName= ds.Tables[0].Rows[0]["CnName"].ToString();
																																model.EnName= ds.Tables[0].Rows[0]["EnName"].ToString();
																																												if(ds.Tables[0].Rows[0]["Sex"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Sex"].ToString()=="1")||(ds.Tables[0].Rows[0]["Sex"].ToString().ToLower()=="true"))
					{
					model.Sex= true;
					}
					else
					{
					model.Sex= false;
					}
				}
																				model.Tel= ds.Tables[0].Rows[0]["Tel"].ToString();
																																model.Address= ds.Tables[0].Rows[0]["Address"].ToString();
																												if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CopyCount"].ToString()!="")
				{
					model.CopyCount=int.Parse(ds.Tables[0].Rows[0]["CopyCount"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CartType"].ToString()!="")
				{
					model.CartType=int.Parse(ds.Tables[0].Rows[0]["CartType"].ToString());
				}
																																				model.CartNum= ds.Tables[0].Rows[0]["CartNum"].ToString();
																										
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
			strSql.Append(" FROM Bid ");
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
			strSql.Append(" FROM Bid ");
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
            strSql.Append("select * FROM Bid ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
   
	}
}

