using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//排班表
		public class Scheduling
	{
   		     
      	/// <summary>
		/// 日期
        /// </summary>		
		private int _day;
        public int Day
        {
            get{ return _day; }
            set{ _day = value; }
        }        
		/// <summary>
		/// 本、次月类别
        /// </summary>		
		private int _monthtype;
        public int MonthType
        {
            get{ return _monthtype; }
            set{ _monthtype = value; }
        }        
		/// <summary>
		/// 公证员ID
        /// </summary>		
		private int _managerid;
        public int ManagerID
        {
            get{ return _managerid; }
            set{ _managerid = value; }
        }        
		   
	}
}

