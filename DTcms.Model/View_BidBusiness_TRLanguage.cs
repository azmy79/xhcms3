using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//View_BidBusiness_TRLanguage
		public class View_BidBusiness_TRLanguage
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// Name
        /// </summary>		
		private string _name;
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }        
		/// <summary>
		/// Sort
        /// </summary>		
		private int _sort;
        public int Sort
        {
            get{ return _sort; }
            set{ _sort = value; }
        }        
		/// <summary>
		/// IsTop
        /// </summary>		
		private bool _istop;
        public bool IsTop
        {
            get{ return _istop; }
            set{ _istop = value; }
        }        
		/// <summary>
		/// NotaryPrice
        /// </summary>		
		private decimal _notaryprice;
        public decimal NotaryPrice
        {
            get{ return _notaryprice; }
            set{ _notaryprice = value; }
        }        
		/// <summary>
		/// CopyPrice
        /// </summary>		
		private decimal _copyprice;
        public decimal CopyPrice
        {
            get{ return _copyprice; }
            set{ _copyprice = value; }
        }        
		/// <summary>
		/// TRLanguageID
        /// </summary>		
		private int _trlanguageid;
        public int TRLanguageID
        {
            get{ return _trlanguageid; }
            set{ _trlanguageid = value; }
        }        
		/// <summary>
		/// TRLanguageName
        /// </summary>		
		private string _trlanguagename;
        public string TRLanguageName
        {
            get{ return _trlanguagename; }
            set{ _trlanguagename = value; }
        }        
		/// <summary>
		/// TRPrice
        /// </summary>		
		private decimal _trprice;
        public decimal TRPrice
        {
            get{ return _trprice; }
            set{ _trprice = value; }
        }        
		   
	}
}

