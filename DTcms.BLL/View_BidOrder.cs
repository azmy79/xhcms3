using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.BLL {
	 	//View_BidOrder
		public partial class View_BidOrder
	{
   		     
		private readonly DTcms.DAL.View_BidOrder dal=new DTcms.DAL.View_BidOrder();
		public View_BidOrder()
		{}
		
		#region  Method
		
		
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
		public List<DTcms.Model.View_BidOrder> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.View_BidOrder> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.View_BidOrder> modelList = new List<DTcms.Model.View_BidOrder>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.View_BidOrder model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.View_BidOrder();					
																	model.OrderNo= dt.Rows[n]["OrderNo"].ToString();
																												if(dt.Rows[n]["PaymentStatus"].ToString()!="")
				{
					model.PaymentStatus=int.Parse(dt.Rows[n]["PaymentStatus"].ToString());
				}
																																if(dt.Rows[n]["OrderStatus"].ToString()!="")
				{
					model.OrderStatus=int.Parse(dt.Rows[n]["OrderStatus"].ToString());
				}
																																if(dt.Rows[n]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(dt.Rows[n]["UserID"].ToString());
				}
																																if(dt.Rows[n]["PaymentTime"].ToString()!="")
				{
					model.PaymentTime=DateTime.Parse(dt.Rows[n]["PaymentTime"].ToString());
				}
																																if(dt.Rows[n]["OrderAmount"].ToString()!="")
				{
					model.OrderAmount=decimal.Parse(dt.Rows[n]["OrderAmount"].ToString());
				}
																																if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																				model.Number= dt.Rows[n]["Number"].ToString();
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
																																model.BidBusiness= dt.Rows[n]["BidBusiness"].ToString();
																																model.PurposeName= dt.Rows[n]["PurposeName"].ToString();
																																model.CountryName= dt.Rows[n]["CountryName"].ToString();
																																model.TRLanguage= dt.Rows[n]["TRLanguage"].ToString();
																						
				
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
        public List<DTcms.Model.View_BidOrder> GetModelList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var ds = GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount); 
            return DataTableToList(ds.Tables[0]);
        }
#endregion
   
	}
}