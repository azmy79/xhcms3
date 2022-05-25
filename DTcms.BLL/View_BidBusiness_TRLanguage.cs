using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.BLL {
	 	//View_BidBusiness_TRLanguage
		public partial class View_BidBusiness_TRLanguage
	{
   		     
		private readonly DTcms.DAL.View_BidBusiness_TRLanguage dal=new DTcms.DAL.View_BidBusiness_TRLanguage();
		public View_BidBusiness_TRLanguage()
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
		public List<DTcms.Model.View_BidBusiness_TRLanguage> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.View_BidBusiness_TRLanguage> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.View_BidBusiness_TRLanguage> modelList = new List<DTcms.Model.View_BidBusiness_TRLanguage>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.View_BidBusiness_TRLanguage model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.View_BidBusiness_TRLanguage();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																				model.Name= dt.Rows[n]["Name"].ToString();
																												if(dt.Rows[n]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
				}
																																																if(dt.Rows[n]["IsTop"].ToString()!="")
				{
					if((dt.Rows[n]["IsTop"].ToString()=="1")||(dt.Rows[n]["IsTop"].ToString().ToLower()=="true"))
					{
					model.IsTop= true;
					}
					else
					{
					model.IsTop= false;
					}
				}
																if(dt.Rows[n]["NotaryPrice"].ToString()!="")
				{
					model.NotaryPrice=decimal.Parse(dt.Rows[n]["NotaryPrice"].ToString());
				}
																																if(dt.Rows[n]["CopyPrice"].ToString()!="")
				{
					model.CopyPrice=decimal.Parse(dt.Rows[n]["CopyPrice"].ToString());
				}
																																if(dt.Rows[n]["TRLanguageID"].ToString()!="")
				{
					model.TRLanguageID=int.Parse(dt.Rows[n]["TRLanguageID"].ToString());
				}
																																				model.TRLanguageName= dt.Rows[n]["TRLanguageName"].ToString();
																												if(dt.Rows[n]["TRPrice"].ToString()!="")
				{
					model.TRPrice=decimal.Parse(dt.Rows[n]["TRPrice"].ToString());
				}
																										
				
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
        public List<DTcms.Model.View_BidBusiness_TRLanguage> GetModelList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var ds = GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount); 
            return DataTableToList(ds.Tables[0]);
        }
#endregion
   
	}
}