using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//翻译语言
		public class TRLanguage
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
		/// 默认价格
        /// </summary>		
		private decimal _defaultprice;
        public decimal DefaultPrice
        {
            get{ return _defaultprice; }
            set{ _defaultprice = value; }
        }        
		   
	}
}

