using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.BLL {
	 	//申办-申办业务
		public partial class Bid_BidBusiness
	{
   		     
		private readonly DTcms.DAL.Bid_BidBusiness dal=new DTcms.DAL.Bid_BidBusiness();
		public Bid_BidBusiness()
		{}
		
		#region  Method
		
				/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int BidID,int BidBusinessID)
		{
			return dal.Exists(BidID,BidBusinessID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool  Add(DTcms.Model.Bid_BidBusiness model)
		{
			return dal.Add(model);	
		}
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int BidID,int BidBusinessID, string strValue)
        {
            
            return dal.UpdateField(BidID,BidBusinessID, strValue);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Bid_BidBusiness model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int BidID,int BidBusinessID)
		{
			
			return dal.Delete(BidID,BidBusinessID);
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
		public DTcms.Model.Bid_BidBusiness GetModel(int BidID,int BidBusinessID)
		{
			return dal.GetModel(BidID,BidBusinessID);
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
		public List<DTcms.Model.Bid_BidBusiness> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.Bid_BidBusiness> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.Bid_BidBusiness> modelList = new List<DTcms.Model.Bid_BidBusiness>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.Bid_BidBusiness model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.Bid_BidBusiness();					
													if(dt.Rows[n]["BidID"].ToString()!="")
				{
					model.BidID=int.Parse(dt.Rows[n]["BidID"].ToString());
				}
																																if(dt.Rows[n]["BidBusinessID"].ToString()!="")
				{
					model.BidBusinessID=int.Parse(dt.Rows[n]["BidBusinessID"].ToString());
				}
																																if(dt.Rows[n]["CertificateStyleID"].ToString()!="")
				{
					model.CertificateStyleID=int.Parse(dt.Rows[n]["CertificateStyleID"].ToString());
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
        public List<DTcms.Model.Bid_BidBusiness> GetModelList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var ds = GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount); 
            return DataTableToList(ds.Tables[0]);
        }
#endregion
   
	}
}