using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//View_Bid
		public class View_Bid
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
		/// Number
        /// </summary>		
		private string _number;
        public string Number
        {
            get{ return _number; }
            set{ _number = value; }
        }        
		/// <summary>
		/// UserID
        /// </summary>		
		private int _userid;
        public int UserID
        {
            get{ return _userid; }
            set{ _userid = value; }
        }        
		/// <summary>
		/// PurposeID
        /// </summary>		
		private int _purposeid;
        public int PurposeID
        {
            get{ return _purposeid; }
            set{ _purposeid = value; }
        }        
		/// <summary>
		/// CountryID
        /// </summary>		
		private int _countryid;
        public int CountryID
        {
            get{ return _countryid; }
            set{ _countryid = value; }
        }        
		/// <summary>
		/// CnName
        /// </summary>		
		private string _cnname;
        public string CnName
        {
            get{ return _cnname; }
            set{ _cnname = value; }
        }        
		/// <summary>
		/// EnName
        /// </summary>		
		private string _enname;
        public string EnName
        {
            get{ return _enname; }
            set{ _enname = value; }
        }        
		/// <summary>
		/// Sex
        /// </summary>		
		private bool _sex;
        public bool Sex
        {
            get{ return _sex; }
            set{ _sex = value; }
        }        
		/// <summary>
		/// Tel
        /// </summary>		
		private string _tel;
        public string Tel
        {
            get{ return _tel; }
            set{ _tel = value; }
        }        
		/// <summary>
		/// Address
        /// </summary>		
		private string _address;
        public string Address
        {
            get{ return _address; }
            set{ _address = value; }
        }        
		/// <summary>
		/// AddTime
        /// </summary>		
		private DateTime _addtime;
        public DateTime AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		/// <summary>
		/// Price
        /// </summary>		
		private decimal _price;
        public decimal Price
        {
            get{ return _price; }
            set{ _price = value; }
        }        
		/// <summary>
		/// CopyCount
        /// </summary>		
		private int _copycount;
        public int CopyCount
        {
            get{ return _copycount; }
            set{ _copycount = value; }
        }        
		/// <summary>
		/// Status
        /// </summary>		
		private int _status;
        public int Status
        {
            get{ return _status; }
            set{ _status = value; }
        }        
		/// <summary>
		/// Birthday
        /// </summary>		
		private DateTime _birthday;
        public DateTime Birthday
        {
            get{ return _birthday; }
            set{ _birthday = value; }
        }        
		/// <summary>
		/// CartType
        /// </summary>		
		private int _carttype;
        public int CartType
        {
            get{ return _carttype; }
            set{ _carttype = value; }
        }        
		/// <summary>
		/// CartNum
        /// </summary>		
		private string _cartnum;
        public string CartNum
        {
            get{ return _cartnum; }
            set{ _cartnum = value; }
        }        
		/// <summary>
		/// BidBusiness
        /// </summary>		
		private string _bidbusiness;
        public string BidBusiness
        {
            get{ return _bidbusiness; }
            set{ _bidbusiness = value; }
        }        
		/// <summary>
		/// PurposeName
        /// </summary>		
		private string _purposename;
        public string PurposeName
        {
            get{ return _purposename; }
            set{ _purposename = value; }
        }        
		/// <summary>
		/// CountryName
        /// </summary>		
		private string _countryname;
        public string CountryName
        {
            get{ return _countryname; }
            set{ _countryname = value; }
        }        
		   
	}
}

