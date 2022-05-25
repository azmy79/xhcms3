using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//申办-翻译语言
		public class Bid_TRLanguage
	{
   		     
      	/// <summary>
		/// 申办ID
        /// </summary>		
		private int _bidid;
        public int BidID
        {
            get{ return _bidid; }
            set{ _bidid = value; }
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
		   
	}
}

