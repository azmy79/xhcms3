using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.BLL {
	 	//排班表
		public partial class Scheduling
	{
   		     
		private readonly DTcms.DAL.Scheduling dal=new DTcms.DAL.Scheduling();
		public Scheduling()
		{}
		
		#region  Method
		
				/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Day,int MonthType,int ManagerID)
		{
			return dal.Exists(Day,MonthType,ManagerID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool  Add(DTcms.Model.Scheduling model)
		{
			return dal.Add(model);	
		}
		
		/// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int Day,int MonthType,int ManagerID, string strValue)
        {
            
            return dal.UpdateField(Day,MonthType,ManagerID, strValue);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Scheduling model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Day,int MonthType,int ManagerID)
		{
			
			return dal.Delete(Day,MonthType,ManagerID);
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
		public DTcms.Model.Scheduling GetModel(int Day,int MonthType,int ManagerID)
		{
			return dal.GetModel(Day,MonthType,ManagerID);
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
		public List<DTcms.Model.Scheduling> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.Scheduling> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.Scheduling> modelList = new List<DTcms.Model.Scheduling>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.Scheduling model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.Scheduling();					
													if(dt.Rows[n]["Day"].ToString()!="")
				{
					model.Day=int.Parse(dt.Rows[n]["Day"].ToString());
				}
																																if(dt.Rows[n]["MonthType"].ToString()!="")
				{
					model.MonthType=int.Parse(dt.Rows[n]["MonthType"].ToString());
				}
																																if(dt.Rows[n]["ManagerID"].ToString()!="")
				{
					model.ManagerID=int.Parse(dt.Rows[n]["ManagerID"].ToString());
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
        public List<DTcms.Model.Scheduling> GetModelList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            var ds = GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount); 
            return DataTableToList(ds.Tables[0]);
        }
#endregion
   
	}
}