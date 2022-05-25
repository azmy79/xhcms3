using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//申办业务
		public class BidBusiness
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 名称
        /// </summary>		
		private string _name;
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }        
		/// <summary>
		/// 排序
        /// </summary>		
		private int _sort;
        public int Sort
        {
            get{ return _sort; }
            set{ _sort = value; }
        }        
		/// <summary>
		/// 是否推荐显示
        /// </summary>		
		private bool _istop;
        public bool IsTop
        {
            get{ return _istop; }
            set{ _istop = value; }
        }        
		/// <summary>
		/// 公证费用
        /// </summary>		
		private decimal _notaryprice;
        public decimal NotaryPrice
        {
            get{ return _notaryprice; }
            set{ _notaryprice = value; }
        }        
		/// <summary>
		/// 副本费用
        /// </summary>		
		private decimal _copyprice;
        public decimal CopyPrice
        {
            get{ return _copyprice; }
            set{ _copyprice = value; }
        }        
		   
	}
}

