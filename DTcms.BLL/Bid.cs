using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.BLL {
	 	//申办
		public partial class Bid
	{
   		     
		private readonly DTcms.DAL.Bid dal=new DTcms.DAL.Bid();
		public Bid()
		{}
		
		#region  Method
		
				/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.Bid model)
		{
			return dal.Add(model);	
		}
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int ID, string strValue)
        {
            
            return dal.UpdateField(ID, strValue);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Bid model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string pkIdlist )
		{
			return dal.DeleteList(pkIdlist);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.Bid GetModel(int ID)
		{
			return dal.GetModel(ID);
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.Bid> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.Bid> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.Bid> modelList = new List<DTcms.Model.Bid>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.Bid model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.Bid();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																				model.Number= dt.Rows[n]["Number"].ToString();
																												if(dt.Rows[n]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
				}
																																if(dt.Rows[n]["PurposeID"].ToString()!="")
				{
					model.PurposeID=int.Parse(dt.Rows[n]["PurposeID"].ToString());
				}
																																if(dt.Rows[n]["CountryID"].ToString()!="")
				{
					model.CountryID=int.Parse(dt.Rows[n]["CountryID"].ToString());
				}
																																				model.CnName= dt.Rows[n]["CnName"].ToString();
																																model.EnName= dt.Rows[n]["EnName"].ToString();
																																												if(dt.Rows[n]["Sex"].ToString()!="")
				{
					if((dt.Rows[n]["Sex"].ToString()=="1")||(dt.Rows[n]["Sex"].ToString().ToLower()=="true"))
					{
					model.Sex= true;
					}
					else
					{
					model.Sex= false;
					}
				}
																				model.Tel= dt.Rows[n]["Tel"].ToString();
																																model.Address= dt.Rows[n]["Address"].ToString();
																												if(dt.Rows[n]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
				}
																																if(dt.Rows[n]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(dt.Rows[n]["Price"].ToString());
				}
																																if(dt.Rows[n]["CopyCount"].ToString()!="")
				{
					model.CopyCount=int.Parse(dt.Rows[n]["CopyCount"].ToString());
				}
																																if(dt.Rows[n]["Status"].ToString()!="")
				{
					model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
				}
																																if(dt.Rows[n]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
				}
																																if(dt.Rows[n]["CartType"].ToString()!="")
				{
					model.CartType=int.Parse(dt.Rows[n]["CartType"].ToString());
				}
																																				model.CartNum= dt.Rows[n]["CartNum"].ToString();
																						
				
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
		
		/// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public List<DTcms.Model.Bid> GetModelList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var ds = GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount); 
            return DataTableToList(ds.Tables[0]);
        }
#endregion
   
	}
}