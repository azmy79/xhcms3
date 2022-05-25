using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.BLL {
	 	//BidSourceFile
		public partial class BidSourceFile
	{
   		     
		private readonly DTcms.DAL.BidSourceFile dal=new DTcms.DAL.BidSourceFile();
		public BidSourceFile()
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
		public int  Add(DTcms.Model.BidSourceFile model)
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
		public bool Update(DTcms.Model.BidSourceFile model)
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
		public DTcms.Model.BidSourceFile GetModel(int ID)
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
		public List<DTcms.Model.BidSourceFile> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.BidSourceFile> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.BidSourceFile> modelList = new List<DTcms.Model.BidSourceFile>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.BidSourceFile model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.BidSourceFile();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																if(dt.Rows[n]["BidID"].ToString()!="")
				{
					model.BidID=int.Parse(dt.Rows[n]["BidID"].ToString());
				}
																																				model.Title= dt.Rows[n]["Title"].ToString();
																																model.Path= dt.Rows[n]["Path"].ToString();
																																												if(dt.Rows[n]["NeedTranslation"].ToString()!="")
				{
					if((dt.Rows[n]["NeedTranslation"].ToString()=="1")||(dt.Rows[n]["NeedTranslation"].ToString().ToLower()=="true"))
					{
					model.NeedTranslation= true;
					}
					else
					{
					model.NeedTranslation= false;
					}
				}
																				model.Memo= dt.Rows[n]["Memo"].ToString();
																												if(dt.Rows[n]["TranslationPrice"].ToString()!="")
				{
					model.TranslationPrice=decimal.Parse(dt.Rows[n]["TranslationPrice"].ToString());
				}
																																if(dt.Rows[n]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
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
        public List<DTcms.Model.BidSourceFile> GetModelList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var ds = GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount); 
            return DataTableToList(ds.Tables[0]);
        }
#endregion
   
	}
}