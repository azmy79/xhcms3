using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.BLL {
	 	//申办业务-证件类型
		public partial class BidBusiness_DocumentType
	{
   		     
		private readonly DTcms.DAL.BidBusiness_DocumentType dal=new DTcms.DAL.BidBusiness_DocumentType();
		public BidBusiness_DocumentType()
		{}
		
		#region  Method
		
				/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int BidBusinessID,int DocumentTypeID)
		{
			return dal.Exists(BidBusinessID,DocumentTypeID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool  Add(DTcms.Model.BidBusiness_DocumentType model)
		{
			return dal.Add(model);	
		}
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int BidBusinessID,int DocumentTypeID, string strValue)
        {
            
            return dal.UpdateField(BidBusinessID,DocumentTypeID, strValue);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.BidBusiness_DocumentType model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int BidBusinessID,int DocumentTypeID)
		{
			
			return dal.Delete(BidBusinessID,DocumentTypeID);
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
		public DTcms.Model.BidBusiness_DocumentType GetModel(int BidBusinessID,int DocumentTypeID)
		{
			return dal.GetModel(BidBusinessID,DocumentTypeID);
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
		public List<DTcms.Model.BidBusiness_DocumentType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.BidBusiness_DocumentType> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.BidBusiness_DocumentType> modelList = new List<DTcms.Model.BidBusiness_DocumentType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.BidBusiness_DocumentType model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.BidBusiness_DocumentType();					
													if(dt.Rows[n]["BidBusinessID"].ToString()!="")
				{
					model.BidBusinessID=int.Parse(dt.Rows[n]["BidBusinessID"].ToString());
				}
																																if(dt.Rows[n]["DocumentTypeID"].ToString()!="")
				{
					model.DocumentTypeID=int.Parse(dt.Rows[n]["DocumentTypeID"].ToString());
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
        public List<DTcms.Model.BidBusiness_DocumentType> GetModelList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var ds = GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount); 
            return DataTableToList(ds.Tables[0]);
        }
#endregion
   
	}
}