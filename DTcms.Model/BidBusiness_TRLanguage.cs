using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//申办业务-翻译语言费用
		public class BidBusiness_TRLanguage
	{
   		     
      	/// <summary>
		/// 申办业务ID
        /// </summary>		
		private int _bidbusinessid;
        public int BidBusinessID
        {
            get{ return _bidbusinessid; }
            set{ _bidbusinessid = value; }
        }        
		/// <summary>
		/// 翻译语言ID
        /// </summary>		
		private int _trlanguageid;
        public int TRLanguageID
        {
            get{ return _trlanguageid; }
            set{ _trlanguageid = value; }
        }        
		/// <summary>
		/// 价格
        /// </summary>		
		private decimal _price;
        public decimal Price
        {
            get{ return _price; }
            set{ _price = value; }
        }        
		   
	}
}

